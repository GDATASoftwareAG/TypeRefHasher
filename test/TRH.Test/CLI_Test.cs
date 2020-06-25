using System;
using System.Diagnostics;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace TRH.Test
{
    public class CliTest
    {
        private readonly ITestOutputHelper _output;

        public CliTest(ITestOutputHelper output) 
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
                ? Path.GetFullPath("../../../../../artifacts/trh")
                : Path.GetFullPath("../../../../../artifacts/trh.exe");

        [Fact]
        public void PeNet_GivenADotNetFile_ReturnsTrh()
        {
            var peFile = new PeNet.PeFile("Binaries/NetCoreConsole.dll");

            Assert.Equal("9b435fef12d55da7073890330a9a4d7f6e02194aa63e6093429db574407458ba", peFile.TypeRefHash);
        }

        [Fact]
        public void Cli_GivenADotNetFile_ReturnsTrh()
        {
            var cli = GetCliTool();
            _output.WriteLine($"CLI tool path: {cli}");
            var file = "Binaries/NetCoreConsole.dll";

            var result = RunCli(cli, file);

            Assert.Equal("1defec485ab3060a9201f35d69cfcdec4b70b84a2b71c83b53795ca30d1ae8be", result);
        }
    }
}
