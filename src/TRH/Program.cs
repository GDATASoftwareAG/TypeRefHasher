using System;
using PeNet;
using File = System.IO.File;

namespace TRH
{
    class Program
    {
        private const int Error = 1;
        private const int Success = 0;

        public static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: trh pathtofile");
                return Error;
            }

            var file = args[0];
            if (!File.Exists(file))
            {
                Console.Error.WriteLine("File not found.");
                return Error;
            }

            if (!PeFile.TryParse(file, out var peFile))
            {
                Console.Error.WriteLine("Not a valid PE file.");
                return Error;
            }

            if (!peFile!.IsDotNet)
            {
                Console.Error.WriteLine("Not a valid .NET binary.");
                return Error;
            }

            try
            {
                Console.WriteLine(peFile.TypeRefHash);
            }
            catch (Exception) 
            {
                Console.Error.WriteLine("Cannot compute TypeRefHash.");
                return Error;
            }

            return Success;
        }
    }
}
