using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace TRH.Test
{
    public class CLI_Test
    {
        static string RunCli(string cli, string param)
        {
            Process process = new Process();
            process.StartInfo.FileName = cli;
            process.StartInfo.Arguments = param;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //* Read the output (or the error)
            var output = process.StandardOutput.ReadToEnd();
            var err = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return (err + output).Trim();
        }

        private static string GetCliTool() 
            => Environment.OSVersion.Platform == PlatformID.Unix
                ? Path.GetFullPath("../../../../artifacts/trh")
                : Path.GetFullPath("../../../../artifacts/trh.exe");


        [Fact]
        public void GivenADotNetFile_TrhGetsReturned()
        {

            var cli = GetCliTool();
            var file = "Binaries/NetCoreConsole.dll";

            var result = RunCli(cli, file);

            Assert.Equal("c4bc255f816ae338fba805256b078bb023d339d2b80dc84a21444367539038cb", result);
        }
    }
}
