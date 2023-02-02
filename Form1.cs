using AxWMPLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using WMPLib;
using System.IO;
using static Rencata.Quiz.Programme.StartTest;
using System.Threading;
using System.Media;
using System.Collections.Specialized;

namespace Rencata.Quiz.Programme
{
    public partial class Form1 : Form
    {

        private Bitmap _playBitmap;
        private int _currentSong = 0;
        private bool _isPlaying = true;
        private List<string> songs;

        private Random random = new Random();
        private QuizQuestions quiz;
        private List<QuizDetails> quizDetail = new List<QuizDetails>();
        //private Participants participants = new Participants();
        


        private string right = "\u2714";
        private string wrong = "\u274C";
        int TotalRound = 1;
        int Totalparticipant = 1;
        int eachRound = 1;
        int round = 1;
        int question = 1;
        int participant = 1;
        private int index = 0;
        private int backCount = 0;
        private bool isback = false;
        private int _currentIndex = 0;
        private string showAnswer = "";

        
        string quizFile = "";
        //FileUpload fileUpload = new FileUpload();
        StartTest startTest = new StartTest();

        
        bool isQuizComplete = false;

        private int secondsToWait = Convert.ToInt32(ConfigurationManager.AppSettings["secondsToWait"]);
        private string bgMusic = ConfigurationManager.AppSettings["music"];
        private DateTime startTime;


        //Find QuizDetails and question using ID
        private bool isApplicationExit = false;
        private string saveFolderName = "quiz";
        private string saveFileName = "quizData.json";
        bool isClose = false;
        public bool isSubmit = false;



        public List<Participants> lstParticipant = new List<Participants>();
        public QuizConfiguration quizConfig = new QuizConfiguration();
        private bool _playMusic = false;
        private static readonly object _lock = new object();
        private int TimeOutShowAnswerMusic = ConfigurationManager.AppSettings["TimeOutShowAnswerMusic"] == null ? 5000 : Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutShowAnswerMusic"]);
        private string ShowAnswerMusic = ConfigurationManager.AppSettings["ShowAnswerMusic"] == null ? @"C:\\Windows\\Media\\Ring02.wav" : ConfigurationManager.AppSettings["ShowAnswerMusic"];
        public Form1()
        {
            InitializeComponent();
            //label2.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Italic);
            //label2.ForeColor = Color.White;
            _playBitmap = global::Rencata.Quiz.Programme.Properties.Resources.pause_button_25;
            btnPreviouc.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //quizFile = System.IO.File.ReadAllText(@"c:\Users\Bharanikumar.Eswar\qamodel.json");
            //quiz = JsonConvert.DeserializeObject<QuizQuestion>(quizFile);
            //string team = System.IO.File.ReadAllText(@"c:\Users\Bharanikumar.Eswar\Teamandmember.json");

            //participants = JsonConvert.DeserializeObject<List<Participants>>(team);

            if (string.IsNullOrWhiteSpace(quizFile))
            {
                OpenStartTestForm();
                if (!isApplicationExit)
                {
                    //if (quiz == null || participants.Members == null)
                    
                    if (quiz == null || lstParticipant == null || lstParticipant.Count == 0)
                    {
                        isClose = true;
                        Application.Exit();
                    }
                    else
                    {
                        //RetrieveSavedData();
                        GenerateTeamControl();
                        
                        initQuiz();

                        axWindowsMediaPlayer1.Visible = false;
                        songs = new List<string>();
                        string[] musci = bgMusic.Split(',');
                        if (musci.Length > 0)
                        {
                            foreach (var song in musci)
                            {
                                if (!string.IsNullOrWhiteSpace(song))
                                    songs.Add(song);
                            }
                                
                        }
                        //var backgroundMusicManager = ConfigurationManager.GetSection("BackgroundMusicManager") as NameValueCollection;
                        //songs = new List<string>();
                        //if (backgroundMusicManager != null)
                        //{
                        //    foreach (var serverKey in backgroundMusicManager.AllKeys)
                        //    {
                        //        string serverValue = backgroundMusicManager.GetValues(serverKey).FirstOrDefault();
                        //        songs.Add(serverValue);
                        //    }
                        //}
                        //songs = new List<string>()
                        //    {
                        //        @"C:\Users\Bharanikumar.Eswar\Downloads\wemida-heartbeat-voting-quizzing-voting-background-music-16574.mp3",
                        //            @"C:\Users\Bharanikumar.Eswar\Downloads\bella-ciao-guitar-ahmadmousavipour-11996.mp3"
                        //    };
                        PlayMusic();
                    }
                    Answer answer = new Answer() { Answered = new List<string>() };
                }
                else
                {
                    Application.Exit();
                }
            }
        }
        private async void RetrieveSavedData()
        {
            var quizFile = System.IO.File.ReadAllText(String.Format("{0}/{1}/{2}", Application.StartupPath, "quiz", "quizData.json"));
            var lstParticipant = JsonConvert.DeserializeObject<List<Participants>>(quizFile);

            var quizconfigFile = System.IO.File.ReadAllText(String.Format("{0}/{1}/{2}", Application.StartupPath, "quiz", "quizConfiguration.json"));
            var quizConfig = JsonConvert.DeserializeObject<QuizConfiguration>(quizconfigFile);

            quizDetail = new List<QuizDetails>();

            int currentParticipant = 1;
            int currentRound = 1;
            int currentQuestion = 1;

            int TotalRound = quizConfig.TotalRound;
            int Totalparticipant = quizConfig.TotalParticipant;
            int eachRound = quizConfig.QuestioninEachRound;

            var quizDetails = lstParticipant.SelectMany((cPar, i) =>
                                                        cPar.Answer
                                                            .Where(cRoun => cRoun != null)
                                                            .Select((cRoun, k) => new
                                                            {
                                                                Id = cPar.Id,
                                                                QuestionId = cRoun.QuestionId,
                                                                isShowedAnswer = cRoun.isShowedAnswer,
                                                                cRound = (k / eachRound) + 1,
                                                                cParticipant = i + 1,
                                                                cQuestion = k + 1
                                                            })).ToList();

            foreach (var participant in quizDetails)
            {
                quizDetail.Add(new QuizDetails() { Id = participant.Id, QuestionId = participant.QuestionId, isShowedAnswer = participant.isShowedAnswer });
                currentParticipant = participant.cParticipant;
                currentQuestion = participant.cQuestion / eachRound;
                currentRound = participant.cRound;

            }

            if(quizDetail.Count > 1)
            {
                btnPreviouc.Enabled = true;
            }




            round = currentRound;
            question = currentQuestion == 0 ? 1 : currentQuestion;
            participant = currentParticipant;
            if (currentQuestion == 0)
            {
                round = currentRound + 1;
                participant = 1;
            }

            //r1 u2 e2
            
        }

        private void startTest_Data(object sender, StartTestDataEventArgs e)
        {
            isApplicationExit = e.isClose;
            if (isApplicationExit)
            {
                Application.Exit();
            }
            else
            {
                string fileName = e.fileName;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    quizFile = System.IO.File.ReadAllText(fileName);
                    quiz = JsonConvert.DeserializeObject<QuizQuestions>(quizFile);
                    
                }

                if (e.lstParticipants != null)
                {
                    //participants = e.participants;
                    lstParticipant = e.lstParticipants;
                    //Totalparticipant = participants.Members.Count;
                    //RetrieveSavedData();
                }
                quizConfig = e.quizConfiguration;
                TotalRound = quizConfig.TotalRound;
                Totalparticipant = quizConfig.TotalParticipant;
                eachRound = quizConfig.QuestioninEachRound;
            }
        }

        private void SaveQuizFile()
        {

        }
        private void RemoveQuizFile()
        {

        }
        private void OpenStartTestForm()
        {
            startTest = new StartTest();
            startTest.StartTestData += startTest_Data;
            startTest.ShowDialog();
        }

        #region Init Quiz Program
        //private bool validateQuizFormat(string fileName)
        //{
        //    try
        //    {
        //        quizFile = System.IO.File.ReadAllText(fileName);
        //        quiz = JsonConvert.DeserializeObject<QuizQuestion>(quizFile);
        //        if (quiz == null || quiz.QuizQuestion == null || quiz.QuizQuestion.Count <= 0)
        //        {
        //            throw new JsonReaderException();
        //        }
        //        return true;
        //    }
        //    catch (JsonReaderException)
        //    {
        //        fileUpload.Visible = false;
        //        fileUpload.Close();
        //        MessageBox.Show("The selected file is not a valid QuizQuestion file format.");
        //    }
        //    return false;
        //}
        #endregion
        #region Create Dynamic Control
        private T CreateControl<T>(params Action<T>[] actions) where T : Control, new()
        {
            T control = new T();
            if (actions != null)
                foreach (var action in actions)
                    action(control);
            return control;
        }

        #endregion
        private void richTextBox1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            richTextBox.Height = e.NewRectangle.Height < 250 ? e.NewRectangle.Height + 10 : 250;
        }
        private int GetTotalQuestion()
        {
            int totalNumberOfQuestions = 0;
            foreach (var participant in lstParticipant)
            {
                totalNumberOfQuestions += participant.Answer.Count;
            }
            return totalNumberOfQuestions;
        }

        public Answer GetAlreadyAskedQuestion(int id)
        {
            Answer answer = null;
            foreach (var participant in lstParticipant)
            {
                if (participant.Answer != null)
                {
                    foreach (var item in participant.Answer)
                    {
                        if (item.QuestionId == id)
                        {
                            answer = item;
                            break;
                        }
                    }
                }
                if (answer != null)
                {
                    break;
                }
            }
            return answer;
        }
        private void NextQuiz(QuizQuestions quiz, int participantIndex = 0)
        {
            try
            {
                RemoveControls("option");
                index = random.Next(0, quiz.Questions.Count);
                var question = quiz.Questions[index];
                richTextBox1.Text = question.Question;
                if (quizDetail.Count < quiz.Questions.Count)
                {
                    Answer answer = GetAlreadyAskedQuestion(question.id);
                    if (answer ==  null)
                    {
                        btnNext.Enabled = false;
                        btnShowAnswer.Enabled = true;
                        Timer_Start();

                        //lstParticipant[participantIndex].Answer[0].QuestionId = question.id;
                        lstParticipant[participantIndex - 1].Answer.Add(
                            new Answer()
                            {
                                QuestionId = question.id
                            });
                        quizDetail.Add(
                            new QuizDetails()
                            {
                                QuestionId = question.id,
                                //ParticipantName = lstParticipant[participantIndex - 1].Name,
                                Id = lstParticipant[participantIndex - 1].Id
                                //,isAssigned = true
                            });
                        GenerateControl(question);

                        HighlightParticipant(participantIndex - 1);
                        
                    }
                    else
                    {
                        NextQuiz(quiz, participant);
                    }
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

        private void HighlightParticipant(int index)
        {
            foreach(Control item in flowLayoutPanel2.Controls)
            {
                string[] Name = item.Name.Split('_');
                int outdx;
                bool isValid = int.TryParse(Name[1], out outdx);
                if (isValid && index == outdx)
                {
                    item.BackColor = Color.FromArgb(240, 248, 255);
                    item.Controls[1].ForeColor = Color.FromArgb(9, 41, 67);
                }
                else
                {
                    item.BackColor = Color.FromArgb(32, 98, 150);
                    item.Controls[1].ForeColor = Color.FromArgb(237, 245, 253);
                }
            }
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
        public bool IsQuestionAnswerd(int id)
        {
            int QuestionId = 0;
            lstParticipant.ForEach(a =>
            {
                QuestionId = a.Answer.Where(s => s.QuestionId == id).FirstOrDefault().QuestionId;
            });// flowLayoutPanel1.Controls[id];
            return QuestionId == id;
        }
        public QuizDetails GetParticipantFromQuiz(int id)
        {
            return quizDetail.Where(x => x.QuestionId == id).FirstOrDefault() ?? new QuizDetails();
        }
        private void GenerateControl(QuizQuestion question, bool isShowAnswer = false, bool isAnswer = false)
        {
            lblshowanswer.Visible = false;
            lblanswer.Text = "";
            lblanswer.Visible = false;
            lblpoint.Visible = false;
            lblpointTitlte.Visible = false;
            var charcode = (int)'A';
            Color optionColor = Color.FromArgb(9, 41, 67);
            int i = 0;

            
            var sanswer = GetAlreadyAskedQuestion(question.id);
            int id = GetId(question.id);
            string result = "";
            if(id > 0)
            {
                var score = lstParticipant.Where(a => a.Id == id).FirstOrDefault();
                if (score?.Answer?.Count > 0)
                    result = string.Format("{0}/{1}", score.Answer.Where(a => a.isCorrect).ToList().Count, score.Answer.Count);
            }
            foreach (var item in question.Options)
            {
                bool isChecked = false;
                bool isNotCA = false;
                var participant = GetParticipantFromQuiz(question.id);
                //if (isShowAnswer || participant.isShowedAnswer)
                if (isShowAnswer || sanswer.isShowedAnswer)
                {
                    isShowAnswer = isShowAnswer || sanswer.isShowedAnswer;
                    //isShowAnswer = isShowAnswer || participant.isShowedAnswer;
                    isChecked = item.IsAnswer; // question.answer.Contains(item);
                    showAnswer = isChecked ? right : wrong;
                    isNotCA = (sanswer != null && sanswer.Answered != null) ? sanswer.Answered.Contains(item.Option) || item.IsAnswer : false;

                    //isNotCA = participant.answeredbyparticipant.Contains(item.Option) || item.IsAnswer;
                    optionColor = GetOptionColor(isShowAnswer, isChecked, isNotCA);
                }
                string optionText = String.Format("{0}) {1}", (char)(charcode + i), item.Option);
                bool isOptionChecked = (sanswer != null && sanswer.Answered != null) ? sanswer.Answered.Contains(item.Option) : false;
                //bool isOptionChecked = participant.answeredbyparticipant.Contains(item.Option);
                if (string.Compare(question.Type, "R", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    var rdoBtn = CreateRadioButton(optionText, optionColor, isNotCA, isShowAnswer, isOptionChecked);
                    flowLayoutPanel1.Controls.Add(rdoBtn);
                }
                else
                {
                    var chkBox = CreateCheckBox(optionText, optionColor, isNotCA, isShowAnswer, isOptionChecked);
                    flowLayoutPanel1.Controls.Add(chkBox);
                }
                i++;
            }
            btnShowAnswer.Enabled = !isShowAnswer;
            //btnNext.Enabled = isShowAnswer;
        }
        private RadioButton CreateRadioButton(string optionText, Color optionColor, bool isNotCA, bool isShowAnswer, bool isOptionChecked)
        {
            return CreateControl<RadioButton>(
                radioBtn => radioBtn.Text = !isNotCA ? optionText : !isShowAnswer ? optionText : $"{optionText} {showAnswer}",
                radioBtn => radioBtn.Name = "option",
                radioBtn => radioBtn.AutoSize = false,
                //radioBtn => radioBtn.Enabled = !isShowAnswer,
                radioBtn => radioBtn.ForeColor = optionColor,
                radioBtn => radioBtn.Size = new Size(richTextBox1.Width - 50, 75),
                radioBtn => radioBtn.Font = new Font("Lucida Sans", 12),
                radioBtn => radioBtn.Checked = isOptionChecked,
                radioBtn => radioBtn.CheckedChanged += new EventHandler(this.radioButtonHandler));
        }
        private CheckBox CreateCheckBox(string optionText, Color optionColor, bool isNotCA, bool isShowAnswer, bool isOptionChecked)
        {
            return CreateControl<CheckBox>(
                checkBox => checkBox.Text = !isNotCA ? optionText : !isShowAnswer ? optionText : $"{optionText} {showAnswer}",
                checkBox => checkBox.Name = "option",
                checkBox => checkBox.AutoSize = false,
                //checkBox => checkBox.Enabled = !isShowAnswer,
                checkBox => checkBox.ForeColor = optionColor,
                checkBox => checkBox.Size = new Size(richTextBox1.Width - 50, 75),
                checkBox => checkBox.Font = new Font("Lucida Sans", 12),
                checkBox => checkBox.Checked = isOptionChecked,
                checkBox => checkBox.CheckedChanged += new EventHandler(this.checkBoxHandler));
        }
        private static Color GetOptionColor(bool isShowAnswer, bool isAnswer, bool isNotCA)
        {
            return !isShowAnswer || !isNotCA ? Color.Black : isAnswer ? Color.Green : Color.Red;
        }
        void checkBoxHandler(object sender, EventArgs e)
        {
            HandleControlEvent<CheckBox>();
        }
        void radioButtonHandler(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb != null)
                if (rb.Checked)
                    HandleControlEvent<RadioButton>();
        }
        public List<T> CheckedControls<T>()
        {
            var checkedControls = new List<T>();
            var lstControls = flowLayoutPanel1.Controls.OfType<T>().ToList();
            foreach (var control in lstControls)
            {
                if (control is CheckBox)
                {
                    if ((control as CheckBox).Checked)
                        checkedControls.Add(control);
                }
                else if (control is RadioButton)
                {
                    if ((control as RadioButton).Checked)
                        checkedControls.Add(control);
                }
                else
                {
                    throw new ArgumentException("Control must be a CheckBox or a RadioButton");
                }
            }
            return checkedControls;
        }
        private void HandleControlEvent<T>() where T : Control
        {
            timer1.Stop();
            QuizQuestion question = GetCurrentQuestion();


            Answer answer = GetAlreadyAskedQuestion(question.id);

            var currentParticipantQuiz = GetParticipantFromQuiz(question.id);
            //if (!currentParticipantQuiz.isShowedAnswer)
            if (!answer.isShowedAnswer)
            {
                var checkedContainer = CheckedControls<T>();

                

                if (answer.Answered == null)
                    answer.Answered = new List<string>();
                else
                    answer.Answered.Clear();

                checkedContainer.ForEach(r =>
                {
                    string option = r.Text.Substring(3);
                    if (!answer.Answered.Contains(option))
                        answer.Answered.Add(option);
                });


                UpdateParticipantAnswer(participant, question);

                //currentParticipantQuiz.answeredbyparticipant.Clear();
                //checkedContainer.ForEach(r =>
                //{
                //    if (!currentParticipantQuiz.answeredbyparticipant.Contains(r.Text))
                //    {
                //        currentParticipantQuiz.answeredbyparticipant.Add(r.Text.Substring(3));
                //    }
                        
                //});
                

                //var isCorrect = question.Options.Where(a => a.IsAnswer).Select(a => a.Option).ToList();
                //currentParticipantQuiz.checkAnswer = isCorrect.All(item => currentParticipantQuiz.answeredbyparticipant.Contains(item));
                //currentParticipantQuiz.checkAnswer = question.answer.All(item => currentParticipantQuiz.answeredbyparticipant.Contains(item));
                
            }
            else
            {
                RemoveControls("option");
                var questions = quiz.Questions.FirstOrDefault(x => x.id == quizDetail[_currentIndex].QuestionId); ;
                GenerateControl(questions);
                MessageBox.Show("Already, answered the question. Please dont select again.", "Already answered");
            }
        }
        public void UpdateParticipantAnswer(int participant, QuizQuestion question)
        {
            //Answer answered = null;
            //foreach (var participant in lstParticipant)
            //{
            //    if (participant.Answer != null)
            //    {
            //        foreach (var answer in participant.Answer)
            //        {
            //            if (answer.QuestionId == id)
            //            {
            //                answered = answer;
            //                break;
            //            }
            //        }
            //    }
            //    if (answered != null)
            //    {
            //        break;
            //    }
            //}

            //var answer = lstParticipant[participant - 1].Answer.Where(a => a.QuestionId == question.id).FirstOrDefault();
            var answer = GetAlreadyAskedQuestion(question.id);
            if (answer != null)
            {
                var isCorrect = question.Options.Where(a => a.IsAnswer).Select(a => a.Option).ToList();
                answer.isCorrect = isCorrect.All(item => answer.Answered.Contains(item)) && !answer.Answered.Any(item => !isCorrect.Contains(item));
            }
        }
        public Answer GetParticipantAnswer(int id)
        {
            return lstParticipant[participant - 1].Answer.Where(a => a.QuestionId == id).FirstOrDefault();
        }

        private QuizQuestion GetCurrentQuestion()
        {
            _currentIndex = 0;
            if (isback && backCount != 0)
            {
                for (int i = (backCount + 1); i <= quizDetail.Count; i++)
                {
                    _currentIndex = quizDetail.Count - i;
                    break;
                }
            }
            else
            {
                _currentIndex = quizDetail.Count - 1;
            }

            return quiz.Questions.FirstOrDefault(x => x.id == quizDetail[_currentIndex].QuestionId);// askedQuestions[_currentIndex];
        }
        private void nextQuestion()
        {
            if (isback && backCount != 0)
            {
                QuizQuestion question = null;
                int nextIndex = 0;
                for (int i = (backCount + 1); i <= quizDetail.Count; i++)
                {
                    nextIndex = quizDetail.Count - backCount;
                    question = GetQuestion(nextIndex);
                    backCount--;
                    isback = backCount == 0 ? false : isback;
                    break;
                }

                Answer answer = GetAlreadyAskedQuestion(question.id);
                //if (question != null && GetParticipantFromQuiz(question.id).isShowedAnswer)
                if (question != null && answer.isShowedAnswer)
                {
                    GenerateAnswer(question);
                }
            }
            else
            {
                initQuiz();
            }
        }

        private void initQuiz()
        {
            lblRounds.Text = String.Format("Round: {0}/{1} :: participant: {2}/{3} :: Question: {4}/{5}", round, TotalRound, participant, Totalparticipant, question, eachRound);

            if (!isQuizComplete)
            {
                NextQuiz(quiz, participant);
            }
            if (question < eachRound)
            {
                question++;
            }
            else
            {
                if (participant < Totalparticipant)
                {
                    participant++;
                    question = 1;
                }
                else
                {
                    if (round < TotalRound)
                    {
                        round++;
                        participant = 1;
                        question = 1;
                    }
                    else
                    {
                        isQuizComplete = true;
                        btnNext.Text = "Submit";
                        //MessageBox.Show("QuizQuestion completed!");
                        //Close();
                    }
                }
            }
        }
        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (!isQuizComplete || isback)
            {
                lblRounds.Text = String.Format("Round: {0}/{1} :: participant: {2}/{3} :: Question: {4}/{5}", round, TotalRound, participant, Totalparticipant, question, eachRound);
                nextQuestion();
                btnPreviouc.Enabled = true;
                if (isQuizComplete && !isback)
                    btnNext.Text = "Submit";
            }
            else
            {
                
                DialogResult result = MessageBox.Show("Are you sure to submit the quiz?", "Submit Quiz", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    await Submit();
                }
            }
            await SaveAsync();
        }
        async Task SaveAsync()
        {
            //int pIndex = 0;
            //foreach (var participant in quizDetail)
            //{
            //    var part = lstParticipant.Where(x => x.Id == participant.Id).FirstOrDefault();

            //    if (part.Answer == null)
            //        part.Answer = new List<Answer>();

            //    Answer answer = new Answer() 
            //    { 
            //        QuestionId= participant.Id,
            //        Answered =  participant.answeredbyparticipant,
            //        isCorrect = participant.checkAnswer.HasValue ? participant.checkAnswer.Value : false
            //    };
            //    part.Answer.Add(answer);

            //    pIndex++;
            //}
            string json = JsonConvert.SerializeObject(lstParticipant);
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
        private async Task Submit()
        {
            //DialogResult result = MessageBox.Show("Are you sure to submit the quiz?", "Submit QuizQuestion", MessageBoxButtons.YesNo);

            //if(result == DialogResult.Yes)
            //{
                
            //}

            axWindowsMediaPlayer1.Ctlcontrols.stop();
            isSubmit = true;
            
            await SaveAsync();
            var list = lstParticipant;
            foreach(var result in lstParticipant)
            {
                foreach(var answer in result.Answer)
                {
                    result.OverallScore = answer.isCorrect ? (result.OverallScore + 1) : result.OverallScore;
                }
            }

            FinalScore fs = new FinalScore();
            fs.Participants = lstParticipant;
            
            fs.Show();
            //isClose = true;
            this.Visible = false;
        }
        private void btnPreviouc_Click(object sender, EventArgs e)
        {
            btnNext.Text = "Next";
            btnNext.Enabled = true;
            QuizQuestion question = null;
            if ((backCount + 1) != quizDetail.Count)
            {
                backCount++;
                isback = true;
                for (int i = (quizDetail.Count - (backCount + 1)); i >= 0; i--)
                {
                    question = GetQuestion(i);
                    break;
                }
                Answer answer = GetAlreadyAskedQuestion(question.id);
                //if (question != null && GetParticipantFromQuiz(question.id).isShowedAnswer)
                if (question != null && answer.isShowedAnswer)
                {
                    GenerateAnswer(question);
                }
            }
            else
            {
                btnPreviouc.Enabled = false;
            }
        }
        private void btnShowAnswer_Click(object sender, EventArgs e)
        {
            bool isPaused = false;
            bool? isAnswered;
            var question = GetCurrentQuestion(); //quiz.QuizQuestion[index];
            if (string.Compare(question.Type, "C", StringComparison.OrdinalIgnoreCase) == 0)
            {
                isAnswered = flowLayoutPanel1.Controls.OfType<CheckBox>().Where(c => c.Checked).ToList().Count > 0;
            }
            else
            {
                isAnswered = flowLayoutPanel1.Controls.OfType<RadioButton>().Where(c => c.Checked).ToList().Count > 0;
            }
            if (isAnswered.HasValue && isAnswered.Value == false)
            {
                MessageBox.Show("Please select the correct answer.", "Choose the answer");
            }
            else
            {
                using (SoundPlayer player = new SoundPlayer(ShowAnswerMusic))
                {
                    if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying)
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                        isPaused = true;
                    }
                    btnShowAnswer.Enabled = false;
                    //btnPreviouc.Enabled = false;
                    player.Play();
                    Thread.Sleep(TimeOutShowAnswerMusic);  // Delay the thread for 5 seconds (5000 milliseconds)
                }
                if (isPaused && axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused)
                    axWindowsMediaPlayer1.Ctlcontrols.play();


                if (isAnswered.HasValue && isAnswered.Value)
                {
                    RemoveControls("option");
                    var answer = GetAlreadyAskedQuestion(question.id);
                    var update = GetParticipantFromQuiz(question.id);
                    update.isShowedAnswer = true;
                    answer.isShowedAnswer = true;
                    GenerateControl(question, true);
                    GenerateAnswer(question);
                    btnNext.Enabled = true;
                }
            }

            //btnNext.Enabled = true;
            
            //else
            //{
            //    MessageBox.Show("Please select the correct answer.", "Choose the answer");
            //}
        }
        public int GetId(int id)
        {
            int ids = 0;
            foreach (var participant in lstParticipant)
            {
                if (participant.Answer != null)
                {
                    foreach (var item in participant.Answer)
                    {
                        if (item.QuestionId == id)
                        {
                            ids = participant.Id;
                            break;
                        }
                    }
                }
                if (ids >0)
                {
                    break;
                }
            }
            return ids;
        }
        private QuizQuestion GetQuestion(int questionIndex)
        {
            QuizQuestion question;
            RemoveControls("option");
            question = quiz.Questions.FirstOrDefault(q => q.id == quizDetail[questionIndex].QuestionId);
            richTextBox1.Text = question.Question;
            GenerateControl(question);
            btnNext.Enabled = quizDetail[questionIndex].isShowedAnswer;
            return question;
        }
        private void GenerateAnswer(QuizQuestion question)
        {
            lblshowanswer.Visible = true;
            lblanswer.Text = "";
            lblanswer.Visible = true;
            lblpoint.Visible = false;
            lblpointTitlte.Visible = false;

            var charcode = (int)'A';
            var lstAnswer = question.Options.Where(a => a.IsAnswer).ToList();
            char[] answers = new char[lstAnswer.Count];

            for (int i = 0; i < lstAnswer.Count; i++)
            {
                for (int j = 0; j < question.Options.Count; j++)
                {
                    //if (question.answer[i] == question.Options[j])
                    if (lstAnswer[i].Option == question.Options[j].Option)
                    {
                        answers[i] = (char)(charcode + j);
                        break;
                    }
                }
            }
            lblanswer.Text = $"Correct answer is {string.Join(", ", answers)}";
        }

        #region Team and MEmber adding/creating
        private void GenerateTeamControl()
        {
            int pIndex=0;
            //foreach (var member in participants.Members)
            foreach (var member in lstParticipant)
            {
                FlowLayoutPanel fPanel = new FlowLayoutPanel();
                fPanel.FlowDirection = FlowDirection.LeftToRight;
                fPanel.AutoSize = true;
                fPanel.Size = new Size(320, 30);
                fPanel.Name = "fPanel_" + pIndex;
                fPanel.BackColor = Color.Transparent;
                PictureBox peopleIconBox = GeneratePictureBox(index, 30);
                var labelMember = CreateControl<Label>(
                    label => label.Text = member.Name,
                    label => label.Name = "Member_"+ pIndex,
                    label => label.AutoSize = false,
                    label => label.Size = new Size(270, 30),
                    label => label.Padding = new Padding(0, 10, 0, 0),
                    label => label.ForeColor = Color.FromArgb(237, 245, 253),
                    label => label.Location = new Point(100, 20),
                    label => label.Font = new Font("Lucida san", 12, FontStyle.Bold));

                fPanel.Controls.Add(peopleIconBox);   
                fPanel.Controls.Add(labelMember);
                flowLayoutPanel2.Controls.Add(fPanel);
                //flowLayoutPanel2.Controls.Add(labelMember);
                pIndex++;
            }
        }
        private PictureBox GeneratePictureBox(int index, int size, string action = "No Action")
        {
            Bitmap bitMap = new Bitmap(global::Rencata.Quiz.Programme.Properties.Resources.icons8_juggler_30);

            var peopleIconBox = CreateControl<PictureBox>(
                                pb => pb.Image = bitMap,
                                pb => pb.Name = "PeopeIconBox_" + index,
                                pb => pb.AutoSize = false,
                                pb => pb.Size = new Size(30,30),
                                //label => label.ForeColor = Color.FromArgb(237, 245, 253),
                                pb => pb.Location = new Point(100, 20));
            return peopleIconBox;
        }
        #endregion

        #region Music Player
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                MusicAction(false, "Paused");
            }
            else
            {
                MusicAction(true);
            }
        }
        private void PlayMusic()
        {
            if (songs.Count > 0)
            {
                axWindowsMediaPlayer1.settings.volume = 10;
                axWindowsMediaPlayer1.URL = songs[_currentSong];
                this.btnPlay.Image = _isPlaying ? _playBitmap : global::Rencata.Quiz.Programme.Properties.Resources.circled_play_25;
                axWindowsMediaPlayer1.settings.autoStart = true;
                label2.Text = String.Format("{0} is playing...", axWindowsMediaPlayer1.currentMedia.name.Substring(0, axWindowsMediaPlayer1.currentMedia.name.Length > 15 ? 15 : axWindowsMediaPlayer1.currentMedia.name.Length));
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else
            {
                label2.Text = "No Audio file";
                btnPlay.Enabled = false;
                btnStop.Enabled = false;
                btnMute.Enabled = false;
                btnVolumeDown.Enabled = false;
                btnVolumeUp.Enabled = false;
            }
        }
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPPlayState.wmppsMediaEnded)
            {
                BeginInvoke(new Action(() =>
                {
                    _currentSong++;
                    if (_currentSong >= songs.Count)
                        _currentSong = 0;
                    axWindowsMediaPlayer1.URL = songs[_currentSong];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    label2.Text = String.Format("{0} is playing...", axWindowsMediaPlayer1.currentMedia.name.Substring(0, axWindowsMediaPlayer1.currentMedia.name.Length > 15 ? 15 : axWindowsMediaPlayer1.currentMedia.name.Length));
                }));
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            MusicAction(false, "Stopped");
        }
        private void MusicAction(bool playing, string ShowingMsg = "")
        {
            _isPlaying = playing;
            this.btnPlay.Image = !playing ? global::Rencata.Quiz.Programme.Properties.Resources.circled_play_25 : global::Rencata.Quiz.Programme.Properties.Resources.pause_button_25;
            label2.Text = !_isPlaying ? ShowingMsg : String.Format("{0} is playing...", axWindowsMediaPlayer1.currentMedia.name.Substring(0, axWindowsMediaPlayer1.currentMedia.name.Length > 15 ? 15 : axWindowsMediaPlayer1.currentMedia.name.Length)); ;
            switch (ShowingMsg)
            {
                case "Stopped":
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    break;
                case "Paused":
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    break;
                default:
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    break;
            }
        }
        private void btnVolumeUp_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume += 1;
        }

        private void btnVolumeDown_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume -= 1;
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.mute = !axWindowsMediaPlayer1.settings.mute;
        }
        #endregion

        #region Timer

        private void Timer_Start()
        {
            timer1.Interval = 1000;//1s
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);
            startTime = DateTime.Now;
            circularProgressBar1.Maximum = secondsToWait;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds;
            int remainingSeconds = secondsToWait - elapsedSeconds;

            if (remainingSeconds >= 0)
            {
                circularProgressBar1.Text = Convert.ToString(remainingSeconds);
                circularProgressBar1.Value = remainingSeconds;

                if (remainingSeconds == 0)
                {
                    timer1.Enabled = false;
                    //nextQuestion();
                }
            }
        }

        #endregion

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (quiz == null || lstParticipant == null || lstParticipant.Count == 0)
            {
                isClose = true;
            }
            if (isClose)
                return;

            DialogResult result = MessageBox.Show("Do you want to exist the quiz?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                await SaveAsync();
                isClose = true;
                Application.Exit();
                // Perform action if user clicks "Yes"
            }
            else if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private async void btnEndQuiz_Click(object sender, EventArgs e)
        {
            bool isPaused = false;
            if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                isPaused = true;
            }
            DialogResult result = MessageBox.Show("Make sure do you want end quiz now?", "Confirmation", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                await Submit();

            }else if(result == DialogResult.No)
            {
                if (isPaused && axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    isPaused = false;
                }
            }
            
        }
    }
}
