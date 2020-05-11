using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ZipMaze.Core
{
    public class AsyncFile
    {
        /// <summary>
        /// Performs System.IO.File.Copy() asynchronously.
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static Task CopyAsync(string sourceFileName, string destFileName, bool overwrite = false)
        {
            return Task.Run(() => File.Copy(sourceFileName, destFileName, overwrite));
        }
    }
}
