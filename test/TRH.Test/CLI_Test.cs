using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace TRH.Test
{
    public class CliTest
    {
        [Fact]
        public void Cli_GivenWrongPath_ReturnsError()
        {
            var cli  = GetCliTool();
            var file = "Binaries/doesNotExist.exe";

            var result = RunCli(cli, file);

            Assert.Equal("File not found.", result);
        }

        [Fact]
        public void Cli_GivenNoPeFile_ReturnsError()
        {
            var cli  = GetCliTool();
            var file = "Binaries/notPeFile.txt";

            var result = RunCli(cli, file);

            Assert.Equal("Not a valid PE file.", result);
        }

        [Fact]
        public void Cli_GivenAPeFile_ReturnsError()
        {
            var cli  = GetCliTool();
            var file = "Binaries/firefox_x86.exe";

            var result = RunCli(cli, file);

            Assert.Equal("Not a valid .NET binary.", result);
        }

        [Fact]
        public void Cli_GivenADotNetFile_ReturnsTrh()
        {
            var cli  = GetCliTool();
            var file = "Binaries/NetCoreConsole.dll";

            var result = RunCli(cli, file);

            Assert.Equal("9b435fef12d55da7073890330a9a4d7f6e02194aa63e6093429db574407458ba", result);
        }

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
            var output = process.StandardOutput.ReadToEnd();
            var err = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return (err + output).Trim();
        }

        private static string GetCliTool() 
            => Environment.OSVersion.Platform == PlatformID.Unix
                ? Path.GetFullPath("../../../../../artifacts/trh")
                : Path.GetFullPath("../../../../../artifacts/trh.exe");
    }
}
