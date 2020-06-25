using System;
using System.Diagnostics;
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
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return (err + output).Trim();
        }

        [Fact]
        public void Test1()
        {
            var cli =
                @"C:\Users\Stefa\source\repos\TypeRefHasher\src\TRH\bin\Release\netcoreapp3.1\win-x64\native\TRH.exe";

            var file = @"C:\Users\Stefa\source\repos\PeNet\test\PeNet.Test\Binaries\NetFrameworkConsole.exe";

            var result = RunCli(cli, file);

            Assert.Equal("c4bc255f816ae338fba805256b078bb023d339d2b80dc84a21444367539038cb", result);
        }
    }
}
