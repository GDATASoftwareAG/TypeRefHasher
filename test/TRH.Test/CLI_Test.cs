using System;
using System.Diagnostics;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace TRH.Test
{
    public class CLI_Test
    {
        private readonly ITestOutputHelper _output;

        public CLI_Test(ITestOutputHelper output) 
            => _output = output;

        static string RunCli(string cli, string param)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName               = cli,
                    Arguments              = param,
                    UseShellExecute        = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true
                }
            };
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
            _output.WriteLine($"CLI tool path: {cli}");
            var file = "Binaries/NetCoreConsole.dll";

            var result = RunCli(cli, file);

            Assert.Equal("c4bc255f816ae338fba805256b078bb023d339d2b80dc84a21444367539038cb", result);
        }
    }
}
