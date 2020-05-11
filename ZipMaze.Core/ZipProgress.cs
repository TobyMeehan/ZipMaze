using System;
using System.Collections.Generic;
using System.Text;

namespace ZipMaze.Core
{
    public class ZipProgress
    {
        public ZipProgress(int totalFiles, int processedFiles)
        {
            _totalFiles = totalFiles;
            _processedFiles = processedFiles;
        }

        private int _totalFiles;
        private int _processedFiles;

        public int PercentageProgress => (int)((decimal)_processedFiles / (decimal)_totalFiles * 100m);
    }
}
