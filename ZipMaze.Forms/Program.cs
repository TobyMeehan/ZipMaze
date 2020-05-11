using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZipMaze.Core;

namespace ZipMaze.Forms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!TryGetDirectory(out string folderPath))
            {
                MessageBox.Show("Selected folder does not exist.", "Zip Maze Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (MessageBox.Show($"Create zip maze from '{folderPath}'?", "Zip Maze", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string folderName = Path.GetFileName(folderPath);
            int totalFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories).Length;

            Progress<ZipProgress> progress = new Progress<ZipProgress>();

            Task progressTask = Core.ZipMaze.Create(folderPath, null, progress, totalFiles);

            Application.Run(new ProgressForm(folderName, progressTask, progress));

            MessageBox.Show("Zip maze complete.", "Zip Maze", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static bool TryGetDirectory(out string folderPath)
        {
            folderPath = Environment.GetCommandLineArgs().FirstOrDefault(x => Directory.Exists(x));

            return folderPath != null;
        }
    }
}
