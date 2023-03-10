using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Rencata.Quiz.Programme.FileUpload;

namespace Rencata.Quiz.Programme
{
    public partial class StartTest : Form
    {
        public delegate void StartTestDataHandler(object sender, StartTestDataEventArgs e);
        public event StartTestDataHandler StartTestData;

        public class StartTestDataEventArgs : EventArgs
        {
            public string fileName { get; set; }
            public List<Participants> lstParticipants { get; set; }
            public bool isClose { get; set; }
            public int TotalParticipants = 0;
            public int TotalRound = 0;
            public int EachRQuestion = 0;
            public QuizConfiguration quizConfiguration { get; set; }

            public StartTestDataEventArgs(string data, List<Participants> qParticipants, QuizConfiguration QuizConfiguration, bool mClose = false)
            {
                fileName = data;
                isClose = mClose;
                lstParticipants = qParticipants;
                quizConfiguration = QuizConfiguration;
            }
        }

        private string participant;
        private FileUpload fileUpload;
        string fileName = String.Empty;
        bool isApplicationExit;
        QuizQuestions quiz = new QuizQuestions();
        private QuizConfiguration quizConfiguration = new QuizConfiguration();
        bool isClosing = false;
        int TotalParticipants = 0;
        int TotalRound = 0;
        int EachRQuestion = 0;
        int TotalQuestions = 0;
        public List<Participants> lstParticipants { get; set; }
        public int pIndex = 0;
        private bool isPastQuiz { get; set; }

        public string saveFolderName = "quiz";
        public string saveFileName = "quizConfiguration.json";

        public StartTest()
        {
            InitializeComponent();
        }
        private void StartTest_Load(object sender, EventArgs e)
        {
            OpenFileUploadForm();
            if (isApplicationExit)
            {
                Application.Exit();
            }
            btnRemove.Enabled = false;
            btnStartTest.Enabled = false;
            if (lstParticipants == null)
                lstParticipants = new List<Participants>();
            quizConfiguration = new QuizConfiguration();
            LoadConfigData();
            cbNoofParticipant.SelectedIndex = 0;

            if (isPastQuiz)
            {
                string configFileName = String.Format("{0}/{1}/{2}", Application.StartupPath, "quiz", saveFileName);
                if (File.Exists(configFileName))
                {
                    
                    var quizFile = System.IO.File.ReadAllText(configFileName);
                    quizConfiguration = JsonConvert.DeserializeObject<QuizConfiguration>(quizFile);
                    TotalParticipants = quizConfiguration.TotalParticipant == 0 ? 0 : quizConfiguration.TotalParticipant - 1;
                    TotalRound = quizConfiguration.TotalRound == 0 ? 0 : quizConfiguration.TotalRound - 1;
                    EachRQuestion = quizConfiguration.QuestioninEachRound == 0 ? 0 : quizConfiguration.QuestioninEachRound - 1;

                    textBox2.Text = quizConfiguration.Timer == 0 ? "5" : Convert.ToString(quizConfiguration.Timer);
                    if(quizConfiguration.BackgroundMusic != null && quizConfiguration.BackgroundMusic.Count > 0)
                    {
                        foreach (var music in quizConfiguration.BackgroundMusic)
                        {
                            listView1.Items.Add(music);
                        }
                    }
                    

                    cbNoofParticipant.SelectedIndex = TotalParticipants;
                }

                if(lstParticipants != null && lstParticipants.Count > 0)
                {
                    pIndex = 0;
                    foreach (var item in lstParticipants)
                    {
                        GenerateParticipant(item.Name, ++pIndex);
                    }
                    DisableAllControl();
                }
               
            }
        }
        async Task SaveConfigurationAsync()
        {
            string json = JsonConvert.SerializeObject(quizConfiguration);
            string directory = Path.Combine(Application.StartupPath, saveFolderName);
            string filePath = Path.Combine(directory, saveFileName);
            await Task.Run(() =>
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(filePath, json);
            });
        }
        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            if (lstParticipants == null || lstParticipants.Count <= 0)
            {
                MessageBox.Show("Please add atleast one participant.", "Add Participant");
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you like to proceed the quiz?", "Start Test", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    quizConfiguration.Timer = !string.IsNullOrWhiteSpace(textBox2.Text) ? Convert.ToInt32(textBox2.Text) : 0 ;
                    if (quizConfiguration.BackgroundMusic == null)
                        quizConfiguration.BackgroundMusic = new List<string>();
                    quizConfiguration.BackgroundMusic.Clear();
                    foreach (ListViewItem item in listView1.Items)
                    {
                        quizConfiguration.BackgroundMusic.Add(item.Text);
                    }
                    await SaveConfigurationAsync();
                    StartTestDataEventArgs args = new StartTestDataEventArgs(fileName, lstParticipants, quizConfiguration, isApplicationExit);
                    StartTestData?.Invoke(this, args);
                    isClosing = true;
                    this.Close();
                }
            }
        }
        private void OpenFileUploadForm()
        {
            fileUpload = new FileUpload();
            fileUpload.FileUploadData += fileUpload_Data;
            fileUpload.ShowDialog();
        }

        private void fileUpload_Data(object sender, FileUploadDataEventArgs e)
        {

            fileName = e.Data;
            isApplicationExit = e.isClose;
            isClosing = isApplicationExit;
            if (!isApplicationExit)
            {
                if (string.IsNullOrWhiteSpace(fileName) || !validateQuizFormat(fileName))
                {
                    this.Visible = false;
                    OpenFileUploadForm();
                    cbEachRoundQ.Enabled = false;
                }

                if (e.Participants != null)
                {
                    lstParticipants = e.Participants;
                }
                isPastQuiz = e.isPastQuizS;
            }
        }
        private bool validateQuizFormat(string fileName)
        {
            try
            {
                string quizFile = System.IO.File.ReadAllText(fileName);
                quiz = JsonConvert.DeserializeObject<QuizQuestions>(quizFile);
                if (quiz == null || quiz.Questions == null || quiz.Questions.Count <= 0)
                {
                    throw new JsonReaderException();
                }
                TotalQuestions = quiz.Questions.Count;

                return true;
            }
            catch (JsonReaderException)
            {
                fileUpload.Visible = false;
                fileUpload.Close();
                MessageBox.Show("The selected file is not a valid Quiz file format.");
            }
            return false;
        }
        private void GenerateParticipant(string participant, int index)
        {
            FlowLayoutPanel fPanel = new FlowLayoutPanel();
            fPanel.FlowDirection = FlowDirection.LeftToRight;
            fPanel.AutoSize = true;
            fPanel.Size = new Size(285, 25);
            fPanel.Name = "fPanel_" + index;
            fPanel.BackColor = Color.Transparent;
            PictureBox peopleIconBox = GeneratePictureBox(index, 30);

            var labelMember = CreateControl<Label>(
                    label => label.Text = participant,
                    label => label.Name = "Member_" + index,
                    label => label.AutoSize = false,
                    label => label.Size = new Size(250, 30),
                    label => label.Padding = new Padding(0, 10, 0, 0),
                    label => label.Location = new Point(100, 20),
                    label => label.Font = new Font("Lucida san", 10, FontStyle.Bold));
            PictureBox pictureBox = GeneratePictureBox(index, 25, "delete");
            pictureBox.Click += new EventHandler(removeParticipant);

            fPanel.Controls.Add(peopleIconBox);
            fPanel.Controls.Add(labelMember);
            fPanel.Controls.Add(pictureBox);
            flowLayoutPanel1.Controls.Add(fPanel);
        }

        private PictureBox GeneratePictureBox(int index, int size, string action = "No Action")
        {
            Bitmap bitMap = new Bitmap(global::Rencata.Quiz.Programme.Properties.Resources.icons8_juggler_30);
            if (string.Compare(action, "delete", StringComparison.OrdinalIgnoreCase) == 0)
            {
                bitMap = new Bitmap(global::Rencata.Quiz.Programme.Properties.Resources.icons8_trash_25);
            }

            var peopleIconBox = CreateControl<PictureBox>(
                                pb => pb.Image = bitMap,
                                pb => pb.Name = "PeopeIconBox_" + index,
                                pb => pb.AutoSize = false,
                                pb => pb.Enabled = !isPastQuiz,
                                pb => pb.Size = new Size(size, size),
                                //label => label.ForeColor = Color.FromArgb(237, 245, 253),
                                pb => pb.Location = new Point(100, 20));
            return peopleIconBox;
        }

        void removeParticipant(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            if (pictureBox != null)
            {
                int index = int.Parse(((string)pictureBox.Name).Split('_').Last());

                Participants removeParticipant = lstParticipants.Where(a => a.Id == index).FirstOrDefault();
                int i = lstParticipants.FindIndex(a => a.Id == index);
                lstParticipants.Remove(removeParticipant);

                //participants.Members.RemoveAt(index);
                
                //participants.Members.RemoveAt(index);
                flowLayoutPanel1.Controls.RemoveAt(i);
                //if (TotalParticipants.HasValue && participants?.Members?.Count == TotalParticipants)
                if (quizConfiguration.TotalParticipant == lstParticipants?.Count )
                {
                    btnStartTest.Enabled = true;
                }
                else
                {
                    btnStartTest.Enabled = false;
                }
            }

            //int index = (pic)
        }
        private T CreateControl<T>(params Action<T>[] actions) where T : Control, new()
        {
            T control = new T();
            if (actions != null)
                foreach (var action in actions)
                    action(control);
            return control;
        }

        private void StartTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosing)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Do you want to close the application?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //if (!isClosing)
                //{
                //    StartTestDataEventArgs args = new StartTestDataEventArgs(fileName, participants, isClosing);
                //    StartTestData(this, args);
                //}
                isClosing = true;
                StartTestDataEventArgs args = new StartTestDataEventArgs(fileName, lstParticipants,quizConfiguration, isClosing);
                StartTestData(this, args);
                // Perform action if user clicks "Yes"
            }
            else if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbRounds_SelectedIndexChanged(object sender, EventArgs e)
        {
            quizConfiguration.TotalRound = (int)cbRounds.SelectedItem;

            //TotalRound = (int)cbRounds.SelectedItem;
            //cbNoofParticipant.Enabled = true;
            //QuizQuestionEachRoundCalculation(TotalRound);
            //cbEachRoundQ.Enabled = true;
            QuizQuestionEachRoundCalculation();
        }

        private void cbNoofParticipant_SelectedIndexChanged(object sender, EventArgs e)
        {
            quizConfiguration.TotalParticipant = (int)cbNoofParticipant.SelectedItem;
            //TotalParticipants = (int)cbNoofParticipant.SelectedItem;
            if(lstParticipants != null && lstParticipants.Count > 0)
            {

            }
            //cbRounds.se
            QuizRoundCalculation();
        }

        private void QuizRoundCalculation()
        {
            int totalQuestions = TotalQuestions;
            int participants = quizConfiguration.TotalParticipant;
            int totalRounds = (int)Math.Floor((double)totalQuestions / participants);

            cbRounds.Items.Clear();
            
            for (int i = 0; i < totalRounds; i++)
            {
                cbRounds.Items.Add(i + 1);
            }
            label6.Text = string.Format("Maximum {0} rounds allowed", totalRounds);
            if (cbRounds.Items.Count > 0)
                cbRounds.SelectedIndex = TotalRound;
        }
        private void QuizQuestionEachRoundCalculation()
        {
            int totalQuestions = TotalQuestions / quizConfiguration.TotalParticipant;
            //int participants = TotalParticipants.HasValue ? TotalParticipants.Value : 1;
            int questionsPerRound = totalQuestions / quizConfiguration.TotalRound;

            cbEachRoundQ.Items.Clear();
            
            for (int i = 0; i < questionsPerRound; i++)
            {
                cbEachRoundQ.Items.Add(i + 1);
            }
            label7.Text = string.Format("Maximum {0} question allowed in each round", questionsPerRound);
            if (cbEachRoundQ.Items.Count > 0)
                cbEachRoundQ.SelectedIndex = EachRQuestion;
        }

        private void btnAddParticipant_Click_1(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                participant = textBox1.Text.Trim();
                    //if(participants.Members.Count < TotalParticipants.Value)
                    if(lstParticipants.Count < quizConfiguration.TotalParticipant)
                    {
                        var addParticipant = new Participants() { Name = participant, Id = ++pIndex, Answer = new List<Answer>() };
                        lstParticipants.Add(addParticipant);
                        //participants.Members.Add(participant);
                        int index = lstParticipants.FindIndex(a => a == addParticipant);
                        //int index = participants.Members.FindIndex(a => a == participant);
                        GenerateParticipant(participant, pIndex);
                        textBox1.Text = "";
                        textBox1.Focus();
                        btnStartTest.Enabled = lstParticipants.Count == quizConfiguration.TotalParticipant ? true : false;
                        //btnStartTest.Enabled = participants.Members.Count == TotalParticipants.Value ? true : false;
                    }
                    else
                    {
                        MessageBox.Show("Maximum participants are added");
                    }
                
            }
            else
            {
                MessageBox.Show("Please enter valid participant.");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                //if(TotalParticipants.HasValue && participants?.Members?.Count == TotalParticipants)
                if (lstParticipants.Count == quizConfiguration.TotalParticipant)
                {
                    btnStartTest.Enabled = true;
                }
            }
            else
            {
                btnStartTest.Enabled = false;
            }
            
        }

        private void cbEachRoundQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            quizConfiguration.QuestioninEachRound = (int)cbEachRoundQ.SelectedItem;
            //EachRQuestion = (int)cbEachRoundQ.SelectedItem;
        }

        private void LoadConfigData()
        {
            cbNoofParticipant.Items.Clear();
            for(int i =0;i< 10; i++)
            {
                cbNoofParticipant.Items.Add(i + 1);
            }
        }

        private void btnBrowseMusic_Click(object sender, EventArgs e)
        {
            quizConfiguration.BackgroundMusic = new List<string>();
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "Music (.mp3)|*.mp3";
            openFileDialog2.Title = "Select file";
            openFileDialog2.Multiselect = true;
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {

                foreach(var music in openFileDialog2.FileNames)
                {
                    ListViewItem item = new ListViewItem(music);
                    listView1.Items.Add(item);
                }
            }
        }

        private void btnShowAnswerMusic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "Music (.wav)|*.wav";
            openFileDialog2.Title = "Select file";
            openFileDialog2.Multiselect = false;
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                  quizConfiguration.ShowAnswerMusic = openFileDialog2.FileName;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                foreach (ListViewItem listviewitem in listView1.SelectedItems)
                {
                    listView1.Items.Remove(listviewitem);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                btnRemove.Enabled= false;
                return;
            }
            else
            {
                btnRemove.Enabled= true;
            }
            //int intselectedindex = listView1.SelectedIndices[0];
            //if (intselectedindex >= 0)
            //{
            //    String text = listView1.Items[intselectedindex].Text;

            //    //do something
            //    //MessageBox.Show(listView1.Items[intselectedindex].Text); 
            //}
        }

        private void DisableAllControl()
        {
            cbNoofParticipant.Enabled= false;   
            cbRounds.Enabled= false;
            cbEachRoundQ.Enabled = false;
            textBox1.Enabled= false;
            btnAddParticipant.Enabled= false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
            if (e.Handled)
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    int second = Convert.ToInt32(textBox2.Text);
                    if(second > 60)
                    {
                        //MessageBox.Show("please");
                    }
                }
                
            }
        }
    }
}
 