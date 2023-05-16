using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class RenameForm : Form
    {
        private string oldPath;
        public RenameForm(string FilePath)
        {
            InitializeComponent();
            oldPath = FilePath;
            FileCatalog.Text = Path.GetDirectoryName(FilePath);
            newName.Text = Path.GetFileNameWithoutExtension(FilePath);
            Ext.Text = Path.GetExtension(FilePath);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            string newPath = FileCatalog.Text + "\\" + newName.Text + Ext.Text;
            if (!File.Exists(newPath))
            {
                File.Move(oldPath, newPath);
            }
            Close();
        }
    }
}
