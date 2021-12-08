using System;
using System.Collections.Generic;
using System.IO;

namespace DirectoryRenamer
{
    class Program
    {
        private enum Params { Help, Files, Folders };

        private static string[] Argums = { "-h", "-e", "-f" };

        private static string[] Arguments = new string[2];

        private static string HelpMessage = "\nUsage: DirectoryRenamer.exe [-param1 -param2 ...] \"find\" \"replace\"\n\n" +
                                            "Parameters:\n\n -h : Help\n -e : Exclude Files\n -f : Exclude Folders";

        private static List<string> Files;
        private static List<string> Directories;

        static void Main(string[] Args)
        {
            if (CheckArguments(Args, Argums[(int)Params.Help]) || GetArguments(Args))
            {
                Console.WriteLine(HelpMessage);
            }

            else
            {
                if (!CheckArguments(Args, Argums[(int)Params.Folders]))
                {
                    RenameFolders();
                }

                if (!CheckArguments(Args, Argums[(int)Params.Files]))
                {
                    RenameFiles();
                }

                if (Files == null)
                {
                    Console.WriteLine("\nSuccessfully renamed " + Directories.Count + " directories.");
                }
                else if (Directories == null)
                {
                    Console.WriteLine("\nSuccessfully renamed " + Files.Count + " files.");
                }
                else
                {
                    Console.WriteLine("\nSuccessfully renamed " + Directories.Count + " directories and " + Files.Count + " files.");
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

        private static bool GetArguments(string[] Args)
        {
            int i = 0;

            foreach (string Arg in Args)
            {
                if (!Arg.StartsWith("-"))
                {
                    Arguments[i] = Arg;
                    i++;

                    if (Arguments.Length == i)
                    {
                        break;
                    }
                }
            }

            foreach (string Arg in Arguments)
            {
                if (Arg == null)
                {
                    return true;
                }
            }

            return false;
        }

        private static void RenameFolders()
        {
            Directories = new List<string>(Directory.GetDirectories(Directory.GetCurrentDirectory(), Arguments[0], SearchOption.AllDirectories));

            if (Directories.Count > 0)
            {
                Console.WriteLine();

                foreach (string Path in Directories)
                {
                    int Index = Path.LastIndexOf(Arguments[0]);
                    string Result = Path.Remove(Index, Arguments[0].Length).Insert(Index, Arguments[1]);

                    Directory.Move(Path, Result);

                    Console.WriteLine(Path);
                    Console.WriteLine(" --> " + Result);
                }
            }
        }

        private static void RenameFiles()
        {
            Files = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory(), Arguments[0] + ".*", SearchOption.AllDirectories));

            if (Files.Count > 0)
            {
                Console.WriteLine();

                foreach (string Path in Files)
                {
                    int Index = Path.LastIndexOf(Arguments[0]);
                    string Result = Path.Remove(Index, Arguments[0].Length).Insert(Index, Arguments[1]);

                    File.Move(Path, Result);

                    Console.WriteLine(Path);
                    Console.WriteLine(" --> " + Result);
                }
            }
        }
    }
}