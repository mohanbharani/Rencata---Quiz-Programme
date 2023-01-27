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
    public class Question
    {
        public string text { get; set; }
        public List<string> answer { get; set; }
        public bool multiselect { get; set; }
        public string category { get; set; }
        public List<string> Options { get; set; }
        public bool asked { get; set; }
        public bool answered { get; set; }
        public List<string> answeredbyuser { get; set; } = new List<string>();
        public string teamname { get; set; }
        public int id { get; set; } = 0;
        public bool ischeckedanswer { get; set; }
        public List<string> musicpath { get; set; }
    }

    public class User
    {
        public int QuestionId { get; set; }
        public bool asked { get; set; }
        public bool answered { get; set; }
        public List<string> answeredbyuser { get; set; } = new List<string>();
        public string teamname { get; set; }
        
    }
    public class Quiz
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
    }
    public class Teams
    {
        public string TeamName { get; set; }
        public List<string> Members { get; set; }
    }
}
