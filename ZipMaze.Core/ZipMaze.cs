using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZipMaze.Core
{
    public class ZipMaze
    {
        public static async Task<int> Create(string path, string tempPath, IProgress<ZipProgress> progress, int totalFiles, int processedFiles = 0)
        {
            tempPath ??= Path.Combine(Path.GetTempPath(), "ZipMaze", Path.GetRandomFileName(), Path.GetFileName(path));
            Directory.CreateDirectory(tempPath);

            foreach (var file in Directory.EnumerateFileSystemEntries(path))
            {
                string relativePath = Path.GetRelativePath(path, file);

                if (Directory.Exists(file))
                {
                    processedFiles =  await Create(file, Path.Combine(tempPath, relativePath), progress, totalFiles, processedFiles);
                }

                if (File.Exists(file))
                {
                    string dest = Path.Combine(tempPath, relativePath);

                    await AsyncFile.CopyAsync(file, dest, true);
                    File.SetAttributes(dest, FileAttributes.Normal);

                    processedFiles++;

                    progress.Report(new ZipProgress(totalFiles, processedFiles));
                }
            }

            string folderName = Path.GetFileName(tempPath);
            string parentDirectory = Directory.GetParent(tempPath).FullName;
            string zipFileName = Path.Combine(parentDirectory, $"{folderName}.zip");

            ZipFile.CreateFromDirectory(tempPath, Path.Combine(parentDirectory, $"{folderName}.zip"));

            Directory.Delete(tempPath, true);

            if (processedFiles == totalFiles)
            {
                string dest = Path.Combine(Directory.GetParent(path).FullName, $"{folderName}.zip");

                File.Move(zipFileName, dest);
            }

            return processedFiles;
        }
    }
}
