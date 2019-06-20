using System;
using System.IO;
using System.Linq;

namespace _Framework.Scripts.Extensions
{
    public static class IoExtensions
    {
        public static long DirectorySize(this string directoryPath)
        {
            return Directory
                .GetFiles(directoryPath, "*", SearchOption.AllDirectories)
                .Sum(t => new FileInfo(t).Length);
        }
        
        public static void CopyDirectory(this string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            Copy(diSource, diTarget);
        }

        public static void Copy(this DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                Copy(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}