using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZipMaze.Core;

namespace ZipMaze.Forms
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public ProgressForm(string folderName, Task progressTask, Progress<ZipProgress> progress)
        {
            InitializeComponent();
            InitialiseComponent(folderName);

            progress.ProgressChanged += Progress_ProgressChanged;
            _progressTask = progressTask;
        }

        private Task _progressTask;

        private void Progress_ProgressChanged(object sender, ZipProgress e)
        {
            Invoke(new Action(() => { _progressBar.Value = e.PercentageProgress;  }));
        }

        private async void ProgressForm_Load(object sender, EventArgs e)
        {
            await _progressTask;

            Close();
        }

        #region Designer code while .NET Core lacks its own
        /// <summary>
        /// For use while .NET Core does not have a reliable designer.
        /// </summary>
        private void InitialiseComponent(string folderName)
        {
            SuspendLayout();

            _progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left| AnchorStyles.Right;
            _progressBar.Location = new Point(12, 12);
            _progressBar.Name = "Progress";
            _progressBar.Size = new Size(415, 37);
            _progressBar.TabIndex = 0;
            _progressBar.Maximum = 100;
            _progressBar.Minimum = 0;
            _progressBar.Value = 0;

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 61);
            Controls.Add(_progressBar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Name = "ZipMaze";
            Text = $"ZipMaze {folderName}";
            Load += ProgressForm_Load;

            ResumeLayout(false);
        }

        private ProgressBar _progressBar = new ProgressBar();

        #endregion
    }
}
