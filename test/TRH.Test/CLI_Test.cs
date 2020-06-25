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

            var (exitCode, msg) = RunCli(cli, file);

            Assert.Equal(1, exitCode);
            Assert.Equal("File not found.", msg);
        }

        [Fact]
        public void Cli_GivenNoPeFile_ReturnsError()
        {
            var cli  = GetCliTool();
            var file = "Binaries/notPeFile.txt";

            var (exitCode, msg) = RunCli(cli, file);

            Assert.Equal(1, exitCode);
            Assert.Equal("Not a valid PE file.", msg);
        }

        [Fact]
        public void Cli_GivenAPeFile_ReturnsError()
        {
            var cli  = GetCliTool();
            var file = "Binaries/firefox_x86.exe";

            var (exitCode, msg) = RunCli(cli, file);

            Assert.Equal(1, exitCode);
            Assert.Equal("Not a valid .NET binary.", msg);
        }

        [Fact]
        public void Cli_GivenADotNetFile_ReturnsTrh()
        {
            var cli = GetCliTool();
            var file = "Binaries/NetCoreConsole.dll";

            var (exitCode, msg) = RunCli(cli, file);

            Assert.Equal(0, exitCode);
            Assert.Equal("9b435fef12d55da7073890330a9a4d7f6e02194aa63e6093429db574407458ba", msg);
        }

        static (int, string) RunCli(string cli, string param)
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

            return (process.ExitCode, (err + output).Trim());
        }

        private static string GetCliTool() 
            => Environment.OSVersion.Platform == PlatformID.Unix
                ? Path.GetFullPath("../../../../../artifacts/trh")
                : Path.GetFullPath("../../../../../artifacts/trh.exe");
    }
}
