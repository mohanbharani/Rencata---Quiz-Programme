using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Bson;
using System.Dynamic;
using System.Collections;
using System.Reflection.Emit;
using System.Net;
using System.Security.AccessControl;
using Label = System.Windows.Forms.Label;
using System.Media;
using AxWMPLib;
using WMPLib;

namespace Rencata.Quiz.Programme
{
    public partial class QuizProgramme : Form
    {
        private Random random;
        private Quiz quiz;
        private List<Question> askedQuestions = new List<Question>();
        private List<Teams> teamsandMembers = new List<Teams>();
        private int index = 0;
        private int backCount = 0;
        private bool isback = false;
        private int questionID = 0;

        private List<Question> AssignedQuest = new List<Question>();
        public QuizProgramme()
        {
            InitializeComponent();

            random = new Random();

        }

        private void Quiz_Load(object sender, EventArgs e)
        {
            try
            {
                btnStop.Enabled = false;
                btnPause.Enabled = false;
                btnResume.Enabled = false;
                Test();
                axWindowsMediaPlayer1.Visible = false;
                panel1.Visible = false;
                string Json = System.IO.File.ReadAllText(@"C:\Users\bharani.kumar\Repo\Rencata - Quiz Programme\Rencata - Quiz Programme\asssets\qamodel.json");
                string team = System.IO.File.ReadAllText(@"C:\Users\bharani.kumar\Repo\Rencata - Quiz Programme\Rencata - Quiz Programme\asssets\Teamandmember.json");
                quiz = JsonConvert.DeserializeObject<Quiz>(Json);
                teamsandMembers = JsonConvert.DeserializeObject<List<Teams>>(team);
                axWindowsMediaPlayer1.settings.autoStart = true;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                if (string.IsNullOrWhiteSpace(Json))
                {
                    FileUpload fileUpload = new FileUpload();
                    this.Visible = false;
                    fileUpload.BringToFront();
                    fileUpload.Show(this);
                }
                else
                {
                    GenerateTeamControl(teamsandMembers);
                    bool isValidQuiz = quiz != null && quiz.Questions.Count > 0;
                    if (isValidQuiz)
                    {
                        NextQuiz(quiz);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select file.");
                //this.Close();
                //throw;
            }
        }
        async void Test()
        {

            
        }
        private void btnNext_Click(object sender, EventArgs e)
        {

            if (isback && backCount != 0)
            {
                label2.Text = backCount.ToString();
                for (int i = (backCount + 1); i <= askedQuestions.Count; i++)
                {
                    RemoveControls("option");
                    var question = askedQuestions[askedQuestions.Count - backCount];
                    richTextBox1.Text = question.text;
                    var textbo = richTextBox1.Location;
                    GenerateControl(question);
                    backCount--;
                    isback = backCount == 0 ? false : isback;
                    break;
                }
            }
            else
            {
                NextQuiz(quiz);
            }
        }

        #region Private Method

        private void NextQuiz(Quiz quiz)
        {
            try
            {
                RemoveControls("option");
                index = random.Next(0, quiz.Questions.Count);
                var question = quiz.Questions[index];
                richTextBox1.Text = question.text;
                richTextBox1.Text = question.text;
                var textbo = richTextBox1.Location;

                if (askedQuestions.Count < quiz.Questions.Count)
                {
                    if (!question.asked)
                    {
                        GenerateControl(question);
                        question.id = ++questionID;
                        askedQuestions.Add(question);
                    }
                    else
                    {
                        NextQuiz(quiz);
                    }
                    question.asked = true;
                }
                else
                {
                    MessageBox.Show("No Questions available");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void checkBoxHandler(object sender, EventArgs e)
        {
            int currentIndex = 0;
            if (isback && backCount != 0)
            {
                for (int i = (backCount + 1); i <= askedQuestions.Count; i++)
                {
                    currentIndex = askedQuestions.Count - i;
                    break;
                }
            }
            else
            {
                currentIndex = askedQuestions.Count - 1;
            }

            var question = askedQuestions[currentIndex]; //quiz.Questions[index];
            if (!question.ischeckedanswer)
            {
                question.answeredbyuser.Clear();
                var checkedContainer = flowLayoutPanel1.Controls.OfType<CheckBox>().Where(c => c.Checked).ToList();
                checkedContainer.ForEach(r =>
                {
                    if (!question.answeredbyuser.Contains(r.Text))
                        question.answeredbyuser.Add(r.Text.Substring(3));
                });
            }
            else
            {
                RemoveControls("option");
                var questions = askedQuestions[currentIndex];
                GenerateControl(questions);
                MessageBox.Show("Already, answered the question. Please dont select again.", "Already answered");

            }

        }
        void radioButtonHandler(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            int currentIndex = 0;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    if (isback && backCount != 0)
                    {
                        for (int i = (backCount + 1); i <= askedQuestions.Count; i++)
                        {
                            currentIndex = askedQuestions.Count - i;
                            break;
                        }
                    }
                    else
                    {
                        currentIndex = askedQuestions.Count - 1;
                    }

                    var question = askedQuestions[currentIndex]; //quiz.Questions[index];
                    if (!question.ischeckedanswer)
                    {
                        question.answeredbyuser.Clear();
                        var checkedContainer = flowLayoutPanel1.Controls.OfType<RadioButton>().Where(c => c.Checked).ToList();
                        checkedContainer.ForEach(r =>
                        {
                            if (!question.answeredbyuser.Contains(r.Text))
                                question.answeredbyuser.Add(r.Text.Substring(3));
                        });
                    }
                    else
                    {
                        RemoveControls("option");
                        var questions = askedQuestions[currentIndex];
                        GenerateControl(questions);
                        MessageBox.Show("Already, answered the question. Please dont select again.", "Already answered");
                    }
                }
            }
        }


        private List<T> GetControl<T>() where T : Control
        {
            return flowLayoutPanel1.Controls.OfType<T>().ToList();
        }

        private T CreateControl<T>(params Action<T>[] actions) where T : Control, new()
        {
            T control = new T();
            if (actions != null)
                foreach (var action in actions)
                    action(control);
            return control;
        }
        private void RemoveControls(string ControlName)
        {
            int controlCount = flowLayoutPanel1.Controls.Count;
            for (int i = 0; i <= controlCount; i++)
            {
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c.Name == ControlName)
                    {
                        flowLayoutPanel1.Controls.Remove(c);
                        controlCount--;
                    }
                }
            }

        }
        void radioTeamButtonHandler(object sender, EventArgs e)
        {

        }
        void radioMemberButtonHandler(object sender, EventArgs e)
        {

        }
        private void GenerateTeamControl(List<Teams> teamsandMembers)
        {
            if (panel1.Visible)
            {
                int pteam = 0;
                int pmemberY = 0;

                foreach (var item in teamsandMembers)
                {

                    var rdoBtn = CreateControl<RadioButton>(
                        radioBtn => radioBtn.Text = item.TeamName,
                        radioBtn => radioBtn.Name = "Team",
                        radioBtn => radioBtn.AutoSize = false,
                        radioBtn => radioBtn.Size = new Size(100, 20),
                        radioBtn => radioBtn.Location = new Point(20, 25),
                        radioBtn => radioBtn.Font = new Font("Microsoft Sans Serif", 12),
                        radioBtn => radioBtn.CheckedChanged += new EventHandler(this.radioTeamButtonHandler));
                    flowLayoutPanel2.Controls.Add(rdoBtn);
                    MemberControl[] mc = new MemberControl[item.Members.Count];
                    int i = 0;
                    foreach (var member in item.Members)
                    {
                        mc[i] = new MemberControl();
                        mc[i].lblCorrect = "0";
                        mc[i].lblWrong = "0";
                        mc[i].rdoText = member;
                        flowLayoutPanel2.Controls.Add(mc[i]);
                        pmemberY++;
                    }
                    pteam++;

                }
            }
            else
            {
                int pteam = 0;
                int pmemberY = 0;

                foreach (var item in teamsandMembers)
                {

                    var rdoTeamBtn = CreateControl<RadioButton>(
                        radioBtn => radioBtn.Text = item.TeamName,
                        radioBtn => radioBtn.Name = "Team",
                        radioBtn => radioBtn.AutoSize = false,
                        radioBtn => radioBtn.Size = new Size(350, 20),
                        radioBtn => radioBtn.Font = new Font("Microsoft Sans Serif", 12),
                        radioBtn => radioBtn.CheckedChanged += new EventHandler(this.radioTeamButtonHandler));
                    flowLayoutPanel3.Controls.Add(rdoTeamBtn);
                    foreach (var member in item.Members)
                    {

                        var rdoMemberBtn = CreateControl<RadioButton>(
                            radioBtn => radioBtn.Text = member + " (A: 5 W: 3)",
                            radioBtn => radioBtn.Name = "Member",
                            radioBtn => radioBtn.AutoSize = false,
                            radioBtn => radioBtn.Size = new Size(350, 20),
                            radioBtn => radioBtn.Location = new Point(100, 20),
                            radioBtn => radioBtn.Font = new Font("Microsoft Sans Serif", 11),
                            radioBtn => radioBtn.CheckedChanged += new EventHandler(this.radioTeamButtonHandler));
                        flowLayoutPanel3.Controls.Add(rdoMemberBtn);
                    }
                        pteam++;

                }
            }
            
        }
        
        private void GenerateControl(Question question, bool isShowAnswer = false, bool isAnswer = false)
        {
            lblshowanswer.Visible = false;
            lblanswer.Text = "";
            lblanswer.Visible = false;
            lblpoint.Visible = false;
            
            string right = "\u2714";
            string wrong = "\u274C";
            string showAnswer = "";
            Color optionColor = Color.Black;
            var charcode = (int)'a';

            if (!question.multiselect)
            {
                int i = 0;
                foreach (var item in question.Options)
                {
                    bool isChecked = false;
                    bool isNotCA = false;
                    if (isShowAnswer || question.ischeckedanswer)
                    {
                        isShowAnswer = isShowAnswer || question.ischeckedanswer;
                        isChecked = question.answer.Contains(item);
                        showAnswer = isChecked ? right : wrong;
                        isNotCA = question.answeredbyuser.Contains(item) || question.answer.Contains(item);
                        optionColor = GetOptionColor(isShowAnswer, isChecked, isNotCA);
                    }
                    int xLocation = richTextBox1.Height + (i * 100);
                    string optionText = String.Format("{0}) {1}", (char)(charcode + i), item);
                    var rdoBtn = CreateControl<RadioButton>(
                        radioBtn => radioBtn.Text = !isNotCA ? optionText : !isShowAnswer ? optionText : $"{optionText} {showAnswer}",
                        radioBtn => radioBtn.Name = "option",
                        radioBtn => radioBtn.AutoSize = false,
                        //radioBtn => radioBtn.Enabled = !isShowAnswer,
                        radioBtn => radioBtn.ForeColor = optionColor,
                        radioBtn => radioBtn.Size = new Size(richTextBox1.Width - 50, 75),
                        radioBtn => radioBtn.Font = new Font("Microsoft Sans Serif", 12),
                        radioBtn => radioBtn.Checked = question.answeredbyuser.Contains(item),
                        radioBtn => radioBtn.Location = new Point(xLocation, 200 + 100),
                        radioBtn => radioBtn.CheckedChanged += new EventHandler(this.radioButtonHandler));
                    flowLayoutPanel1.Controls.Add(rdoBtn);
                    i++;
                }
            }
            else
            {
                int i = 0;
                foreach (var item in question.Options)
                {
                    bool isChecked = false;
                    bool isNotCA = false;
                    if (isShowAnswer || question.ischeckedanswer)
                    {
                        isShowAnswer = isShowAnswer || question.ischeckedanswer;
                        isChecked = question.answer.Contains(item);
                        showAnswer = isChecked ? right : wrong;
                        isNotCA = question.answeredbyuser.Contains(item) || question.answer.Contains(item);
                        optionColor = GetOptionColor(isShowAnswer, isChecked, isNotCA);
                    }
                    int xLocation = richTextBox1.Height + (i * 100);
                    string optionText = String.Format("{0}) {1}", (char)(charcode + i), item);
                    CheckBox chkBox = CreateControl<CheckBox>(
                        checkBox => checkBox.Text = !isNotCA ? optionText : !isShowAnswer ? optionText : $"{optionText} {showAnswer}",
                        checkBox => checkBox.Name = "option",
                        checkBox => checkBox.AutoSize = false,
                        //checkBox => checkBox.Enabled = !isShowAnswer,
                        checkBox => checkBox.ForeColor = optionColor,
                        checkBox => checkBox.Size = new Size(richTextBox1.Width - 50, 75),
                        checkBox => checkBox.Font = new Font("Microsoft Sans Serif", 12),
                        checkBox => checkBox.Checked = question.answeredbyuser.Contains(item),
                        checkBox => checkBox.Location = new Point(xLocation, 200 + 100),
                        checkBox => checkBox.CheckedChanged += new EventHandler(this.checkBoxHandler));

                    flowLayoutPanel1.Controls.Add(chkBox);
                    i++;
                }
            }

            if(askedQuestions.Count == quiz.Questions.Count)
            {
                btnNext.Visible = false;
            }
        }

        private static Color GetOptionColor(bool isShowAnswer, bool isAnswer, bool isNotCA)
        {
            return !isShowAnswer || !isNotCA ? Color.Black : isAnswer ? Color.Green : Color.Red;
        }
        #endregion

        private void btnBack_Click(object sender, EventArgs e)
        {
            if ((backCount + 1) != askedQuestions.Count)
            {
                backCount++;
                isback = true;
                for (int i = (askedQuestions.Count - (backCount + 1)); i >= 0; i--)
                {
                    RemoveControls("option");
                    var question = askedQuestions[i];
                    richTextBox1.Text = question.text;
                    var textbo = richTextBox1.Location;
                    GenerateControl(askedQuestions[i]);
                    break;
                }
            }
        }

        private void Check_Click(object sender, EventArgs e)
        {
            bool? isAnswered;
            int currentIndex = 0;
            if (isback && backCount != 0)
            {
                for (int i = (backCount + 1); i <= askedQuestions.Count; i++)
                {
                    currentIndex = askedQuestions.Count - i;
                    break;
                }
            }
            else
            {
                currentIndex = askedQuestions.Count - 1;
            }

            var question = askedQuestions[currentIndex]; //quiz.Questions[index];
            if (question.multiselect)
            {
                isAnswered = flowLayoutPanel1.Controls.OfType<CheckBox>().Where(c => c.Checked).ToList().Count > 0;
            }
            else
            {
                isAnswered = flowLayoutPanel1.Controls.OfType<RadioButton>().Where(c => c.Checked).ToList().Count > 0;
            }

            if (isAnswered.HasValue && isAnswered.Value)
            {
                RemoveControls("option");
                question.ischeckedanswer = true;
                GenerateControl(question, true);

                lblshowanswer.Visible = true;
                lblanswer.Text = "";
                lblanswer.Visible = true;
                lblpoint.Visible = false;

                var charcode = (int)'a';
                char[] answers = new char[question.answer.Count];

                for (int i = 0; i < question.answer.Count; i++)
                {
                    for (int j = 0; j < question.Options.Count; j++)
                    {
                        if (question.answer[i] == question.Options[j])
                        {
                            answers[i] = (char)(charcode + j);
                            break;
                        }
                    }
                }
                lblanswer.Text = $"Correct answer is {string.Join(", ", answers)}";
            }
            else
            {
                MessageBox.Show("Please select the correct answer.", "Choose the answer");
            }
        }

        private void AnswerControls(string ControlName, Question question)
        {
            int controlCount = flowLayoutPanel1.Controls.Count;
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c.Name == ControlName)
                {
                    c.BackColor = Color.GreenYellow;
                }
            }
        }
        private void richTextBox1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            richTextBox.Height = e.NewRectangle.Height < 250 ? e.NewRectangle.Height + 10 : 250;
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Visible = false;

            //axWindowsMediaPlayer1.URL = @"C:\Windows\Media\alaram05.wav";
            axWindowsMediaPlayer1.URL = @"C:\Users\bharani.kumar\Repo\Rencata - Quiz Programme\Rencata - Quiz Programme\asssets\watr-fluid-10149.mp3";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.Ctlcontrols.play();
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
            btnResume.Enabled = false;
            btnStop.Enabled = true;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnResume.Enabled = false;  
        }
        private void btnResume_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = true;
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
            btnResume.Enabled = false;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = true;
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            btnResume.Enabled = true;
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void axWindowsMediaPlayer2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Enabled  = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = @"C:\Users\bharani.kumar\Repo\Rencata - Quiz Programme\Rencata - Quiz Programme\asssets\watr-fluid-10149.mp3";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }


        /*
public void GetNonSelectedItems<T>() where T : Control
{
// TODO
// 

int currentIndex = 0;
if (isback && backCount != 0)
{
for (int i = (backCount + 1); i <= askedQuestions.Count; i++)
{
currentIndex = askedQuestions.Count - i;
break;
}
}
else
{
currentIndex = askedQuestions.Count - 1;
}
bool str = false;
var question = askedQuestions[currentIndex]; //quiz.Questions[index];
if (!question.ischeckedanswer)
{
question.answeredbyuser.Clear();
//var checkedContainer = flowLayoutPanel1.Controls.OfType<T>().ToList().Where(c => c.Checked as T).ToList();

var checkedContainer = from r in this.Controls.OfType<Control>()
select r;
foreach (Control control in checkedContainer)
{
if (control is CheckBox)
{
CheckBox cc = (CheckBox)control;
if (cc.Checked)
{
if (!question.answeredbyuser.Contains(cc.Text))
question.answeredbyuser.Add(cc.Text);
}
}
if (control is RadioButton)
{
RadioButton rc = (RadioButton)control;
if (rc.Checked)
{
if (!question.answeredbyuser.Contains(rc.Text))
question.answeredbyuser.Add(rc.Text);
}
}
}


}
}
*/
    }
}
