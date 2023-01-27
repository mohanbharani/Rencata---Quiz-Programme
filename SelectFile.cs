using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Policy;

namespace Rencata.Quiz.Programme
{
    public partial class FileUpload : Form
    {
        public static string filePath;
        public FileUpload()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox1.Text = openFileDialog1.FileName;
                button2.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            filePath = textBox1.Text;
            this.Hide();
        }

        private void FileUpload_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }
    }
}
