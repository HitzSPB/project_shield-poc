using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace TeamTwo.Customer.Management.Test.IntegrationTestSetup
{
  public class CosmosDatabaseContainerFixture : IDisposable
  {
    public string ContainerId { get; }
    public string CosmodDatabaseIp { get; }
    public int CosmosDatabasePort { get; }
    public string CosmosDatabaseBindPath = $"{Path.GetTempPath()}{Directory.CreateDirectory(Guid.NewGuid().ToString())}";


    public CosmosDatabaseContainerFixture()
    {
      try
      {
        var stringBuilder = new StringBuilder();
        var dockerImage = "mcr.microsoft.com/cosmosdb/windows/azure-cosmos-emulator";
        var certificatePath = $"{CosmosDatabaseBindPath}\\importcert.ps1";

        Directory.CreateDirectory(CosmosDatabaseBindPath);

        var cosmosDatabaseParameters = new string[] {"-d", "--memory 2GB",
          $"--mount \"type=bind,source={CosmosDatabaseBindPath},destination=C:\\CosmosDB.Emulator\\bind-mount",
        "--isolation=hyperv", "--interactive", "--tty", "-p 8081:8081","-p 8900:8900","-p 8901:8901","-p 8902:8902","-p 10250:10250",
        "-p 10251:10251","-p 10252:10252","-p 10253:10253","-p 10254:10254","-p 10255:10255","-p 10256:10256","-p 10250:10250"};
        stringBuilder.Append("docker run");
        foreach (var item in cosmosDatabaseParameters)
        {
          stringBuilder.Append(" " + item);
        }
        stringBuilder.Append(" " + dockerImage);
        ContainerId = RunCmdCommandWithStringResult(stringBuilder.ToString());
        var ipQueryResult = RunCmdCommandWithStringResult("docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' " + ContainerId);

        CosmodDatabaseIp = SetupDatabaseIp(ipQueryResult);
        WaitForCertificateFileToExist(certificatePath);
        RunCmdCommandWithStringResult($"& {certificatePath}");

        TestHelper.CosmosDatabaseAccessKey = GetAccessKey($"{CosmosDatabaseBindPath}\\Diagnostics\\Transcript.log");
        TestHelper.CosmosDatabaseUri = new Uri($"https://{CosmodDatabaseIp}:{CosmosDatabasePort}/");

      }
      catch
      {
        if (!string.IsNullOrWhiteSpace(ContainerId))
          StopContainer(false);

        throw;
      }
    }

    private string GetAccessKey(string filePath)
    {
      var transcriptData = "";
      using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      using (var sr = new StreamReader(fs, Encoding.UTF8))
      {
        transcriptData = sr.ReadToEnd();
      }
      var regex = @"^Key\s+:\s+Key\s+Value\s+:\s+([^\s]+)";
      var r = new Regex(regex, RegexOptions.Multiline);
      Match m = r.Match(transcriptData);
      return m.Groups[1].Value;
    }

    private void WaitForCertificateFileToExist(string filePath)
    {
      const int waitTime = 5500;
      const int attempts = 70;

      for (var i = 1; i < attempts; i++)
      {
        if (File.Exists(filePath))
        {
          return;
        }
        else
        {
          Debug.WriteLine($"Certificate file does not exist yet, trying again. Attempt number: {i}");
          Debug.WriteLine($"Sleeping {waitTime}ms");
          Thread.Sleep(waitTime);
        }
      }
    }

    private string SetupDatabaseIp(string ipQueryResult)
    {
      var cleanIp = ipQueryResult?.Replace("'", "");
      return cleanIp;
    }

    private string RunCmdCommandWithStringResult(string command)
    {
      return RunCmdCommandWithStringResult(command, 0);
    }

    private string RunCmdCommandWithStringResult(string command, int expectedExitCode)
    {
      return RunCmdCommandWithStringResult(command, new[] { expectedExitCode });
    }

    private string RunCmdCommandWithStringResult(string command, int[] expectedExitCodes)
    {
      var process = new Process
      {
        StartInfo = new ProcessStartInfo("powershell", $"/c {command}")
        {
          UseShellExecute = false,
          RedirectStandardOutput = true,
          RedirectStandardError = true
        },
        EnableRaisingEvents = true
      };

      string result = string.Empty, errors = string.Empty;
      process.OutputDataReceived += (send, args) => result += args.Data + Environment.NewLine;
      process.ErrorDataReceived += (send, args) => errors += args.Data + Environment.NewLine;

      process.Start();
      process.BeginOutputReadLine();
      process.BeginErrorReadLine();
      process.WaitForExit();

      if (!expectedExitCodes.Contains(process.ExitCode) || !string.IsNullOrEmpty(errors))
      {
        throw new Exception($@"CMD command failed!
ExitCode: {process.ExitCode}
ExpectedExitCode: {string.Join("|", expectedExitCodes)}
Errors: {errors}
Command: {command}");
      }
      return result.Trim();
    }

    private void StopContainer(bool throwException)
    {
      try
      {
        RunCmdCommandWithStringResult($"docker stop {ContainerId}");
      }
      catch
      {
        if (throwException)
        {
          throw;
        }
      }
    }

    private bool _disposedValue;

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          StopContainer(true);
        }
        _disposedValue = true;
      }
    }

    void IDisposable.Dispose()
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
