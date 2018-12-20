using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            string root = @"c:\\";
            PrintContainingFilesAndFolders(root);
            Console.ReadKey();
        }

        private static void PrintContainingFilesAndFolders(string dir)
        {
            DirectoryInfo root = new DirectoryInfo(dir);
            PrintFilePaths(root);

            IEnumerable<DirectoryInfo> dirs = new DirectoryInfo[0];
            try
            {
                dirs = root.EnumerateDirectories();

            }
            catch (Exception) { }

            foreach (var d in dirs)
            {
                Console.WriteLine(d.FullName);
                PrintContainingFilesAndFolders(d.FullName);
            }
        }

        private static void PrintFilePaths(DirectoryInfo root)
        {
            IEnumerable<FileInfo> files = new FileInfo[0];
            try
            {
                files = root.EnumerateFiles();
            }
            catch (Exception) { }
            foreach (var f in files) Console.WriteLine(f.FullName);
        }
    }
}
