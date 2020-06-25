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
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return (err + output).Trim();
        }

        private static string GetCliTool() 
            => Environment.OSVersion.Platform == PlatformID.Unix
                ? Path.GetFullPath("./artifacts/trh")
                : Path.GetFullPath("./artifacts/trh.exe");


        [Fact]
        public void Test1()
        {

            var cli = GetCliTool();
            Assert.Equal("dfdfd", cli);
            //var file = "test/TRH.Test/Binaries/NetCoreConsole.dll";



            var result = RunCli(cli, null);

            //Assert.Equal("c4bc255f816ae338fba805256b078bb023d339d2b80dc84a21444367539038cb", result);
        }
    }
}
