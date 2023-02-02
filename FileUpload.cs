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
using Newtonsoft.Json;
using System.Diagnostics;

namespace Rencata.Quiz.Programme
{


    public partial class FileUpload : Form
    {
        public delegate void FileUploadDataHandler(object sender, FileUploadDataEventArgs e);
        public event FileUploadDataHandler FileUploadData;

        public string formTitle { get; set; } = "Select file";
        public string filePath { get; set; }
        public string fileFilter { get; set; } = "All files (*.*)|*.*";
        public bool fileMultiSelect { get; set; } = false;
        public List<Participants> lstParticipation { get; set; }

        bool isClosing = false;
        bool isPastQuizS = false;
        public FileUpload()
        {
            InitializeComponent();
            this.Text = !string.IsNullOrWhiteSpace(formTitle) ? formTitle : "Select file";
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }
        private bool ValidateJson(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            try
            {
                dynamic jsonData = JsonConvert.DeserializeObject(jsonString);
                return true;
                // The JSON file is valid
            }
            catch (JsonReaderException)
            {
                // The JSON file is not valid
                MessageBox.Show("The selected file is not a valid JSON file.");
            }

            return false;
        }
        private void FileUpload_Load(object sender, EventArgs e)
        {
            //RetrieveSavedData();
            btnNext.Enabled = false;
        }


        private void FileUpload_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosing)
                return;

            DialogResult result = MessageBox.Show("Do you want to close the application?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                isClosing = true;
                FileUploadDataEventArgs args = new FileUploadDataEventArgs(lstParticipation, filePath, isPastQuizS, isClosing);
                FileUploadData(this, args);
                //Application.Exit();
                // Perform action if user clicks "Yes"
            }
            else if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            string filePath = richTextBox1.Text;
            if (!string.IsNullOrWhiteSpace(filePath))
            {

                isClosing = true;
                FileUploadDataEventArgs args = new FileUploadDataEventArgs(lstParticipation, filePath, isPastQuizS);
                FileUploadData(this, args);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select the valid quiz file.", "Select file");
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "JSON Files (*.json)|*.json";
            openFileDialog2.Title = "Select file";
            openFileDialog2.Multiselect = fileMultiSelect;
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                if (ValidateJson(openFileDialog2.FileName))
                {
                    richTextBox1.Text = openFileDialog2.FileName;
                    btnNext.Enabled = true;
                    //this.Close();
                }
            }
        }
        public class FileUploadDataEventArgs : EventArgs
        {
            public string Data { get; set; }
            public bool isClose { get; set; }
            public List<Participants> Participants { get; set; }
            public bool isPastQuizS { get; set; }

            public FileUploadDataEventArgs(List<Participants> participants, string data, bool isPastQuiz = false, bool mClose = false)
            {
                Data = data;
                isClose = mClose;
                Participants = participants;
                isPastQuizS = isPastQuiz;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isPastQuizS = checkBox1.Checked;

            if (isPastQuizS)
            {
                string filePath = String.Format("{0}/{1}", Application.StartupPath, "quiz/quizData.json"); ;
                richTextBox2.Visible = true;
                richTextBox2.Text = filePath;
                if (File.Exists(filePath))
                {
                    var quizFile = System.IO.File.ReadAllText(String.Format("{0}/{1}/{2}", Application.StartupPath, "quiz", "quizData.json"));
                    lstParticipation = JsonConvert.DeserializeObject<List<Participants>>(quizFile);
                }
            }
            else
            {
                lstParticipation = null;
                richTextBox2.Visible = false;
            }
        }
    }
}
