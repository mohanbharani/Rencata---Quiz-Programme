using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Rencata.Quiz.Programme
{
    public class QuizQuestion
    {
        public int id { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public List<Options> Options { get; set; }
    }

    public class Options
    {
        public string Option { get; set; }
        public bool IsAnswer { get; set; } = false;
    }

    public class QuizDetails
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool isShowedAnswer { get; set; }
        public int cRound { get; set; }
        public int cQuestion { get; set; }
        public int cParticipant { get; set; }
    }
    public class QuizStorage
    {
        public bool QuizSubmitted { get; set; }
        public List<QuizDetails> participant { get; set; }
    }
    public class QuizQuestions
    {
        public List<QuizQuestion> Questions { get; set; }
    }
    public class Participants
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OverallScore { get; set; }
        public List<Answer> Answer { get; set; }
    }
    public class Answer
    {
        public int QuestionId { get; set; }
        public List<string> Answered { get; set; }
        public bool isCorrect { get; set; }
        public bool isShowedAnswer { get; set; }
    }
    public class QuizConfiguration
    {
        public int TotalRound { get; set; }
        public int TotalParticipant { get; set; }
        public int QuestioninEachRound { get; set; }
    }
}
