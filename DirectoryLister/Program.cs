using System;
using System.Collections.Generic;
using System.IO;

namespace DirectoryLister
{
    class Program
    {
        private enum Params { Help, Files, Full, Natural };

        private static string[] Argums = { "-h", "-e", "-f", "-n" };

        private static string Output = "Directories.txt";

        private static string HelpMessage = "\nUsage: DirectoryLister.exe -param1 -param2 ...\n\n" +
                                            "Parameters:\n\n -h : Help\n -e : Exclude Files\n -f : Get Full Path\n -n : Disable Natural Sorting";

        static void Main(string[] Args)
        {
            if (CheckArguments(Args, Argums[(int)Params.Help]))
            {
                Console.WriteLine(HelpMessage);
            }

            else
            {
                if (File.Exists(Output))
                {
                    File.Delete(Output);
                }
                
                ListFolders(Args);

                if (!CheckArguments(Args, Argums[(int)Params.Files]))
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
            List<string> Directories = new List<string>(Directory.GetDirectories(Directory.GetCurrentDirectory()));

            if (!CheckArguments(Args, Argums[(int)Params.Natural]))
            {
                Directories.Sort(new Classes.NaturalStringComparer());
            }

            if (Directories.Count != 0)
            {
                using (StreamWriter Stream = File.CreateText(Output))
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
            List<string> Files = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory()));

            if (!CheckArguments(Args, Argums[(int)Params.Natural]))
            {
                Files.Sort(new Classes.NaturalStringComparer());
            }

            if (Files.Count != 0)
            {
                using (StreamWriter Stream = File.Exists(Output) ? File.AppendText(Output) : File.CreateText(Output))
                {
                    foreach (string File in Files)
                    {
                        if (File != System.Reflection.Assembly.GetExecutingAssembly().Location)
                        {
                            Stream.WriteLine(CheckArguments(Args, Argums[(int)Params.Full]) ? File : Path.GetFileName(File));
                        }                        
                    }
                }
            }
        }
    }
}