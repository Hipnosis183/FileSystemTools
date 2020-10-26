using System;
using System.IO;

namespace DirectoryLister
{
    class Program
    {
        private enum Params { Help, Files, Full };

        private static string[] Argums = { "-h", "-i", "-f" };

        private static string HelpMessage = "\nUsage: DirectoryLister.exe -param1 -param2 ...\n\n" +
                                            "Parameters:\n\n -h : Help\n -i : Include Files\n -f : Get Full Path";

        static void Main(string[] Args)
        {
            if (CheckArguments(Args, Argums[(int)Params.Help]))
            {
                Console.WriteLine(HelpMessage);
            }

            else
            {
                if (File.Exists("Directories.txt"))
                {
                    File.Delete("Directories.txt");
                }
                
                ListFolders(Args);

                if (CheckArguments(Args, Argums[(int)Params.Files]))
                {
                    ListFiles(Args);
                }
            }
        }

        private static bool CheckArguments(string[] Args, string Arg)
        {
            if (Args.Length != 0)
            {
                foreach (string i in Args)
                {
                    if (i == Arg)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void ListFolders(string[] Args)
        {
            string[] Directories = Directory.GetDirectories(Directory.GetCurrentDirectory());

            if (Directories.Length != 0)
            {
                using (StreamWriter Stream = File.CreateText("Directories.txt"))
                {
                    foreach (string Folder in Directories)
                    {
                        Stream.WriteLine(CheckArguments(Args, Argums[(int)Params.Full]) ? Folder : Path.GetFileName(Folder));
                    }
                }
            }
        }

        private static void ListFiles(string[] Args)
        {
            string[] Files = Directory.GetFiles(Directory.GetCurrentDirectory());

            if (Files.Length != 0)
            {
                using (StreamWriter Stream = File.Exists("Directories.txt") ? File.AppendText("Directories.txt") : File.CreateText("Directories.txt"))
                {
                    foreach (string File in Files)
                    {
                        Stream.WriteLine(CheckArguments(Args, Argums[(int)Params.Full]) ? File : Path.GetFileName(File));
                    }
                }
            }
        }
    }
}