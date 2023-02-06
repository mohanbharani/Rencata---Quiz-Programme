using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rencata.Quiz.Programme
{
    public partial class FinalScore : Form
    {
        public string Score { get; set; }
        public List<Participants> Participants
        {
            get;set;
        }
        public FinalScore()
        {
            InitializeComponent();
        }

        private void FinalScore_Load(object sender, EventArgs e)
        {
            //label1.Text = Score;
            int index = 0;
            List<int> randomNumbers = Enumerable.Range(1, 100).OrderBy(x => Guid.NewGuid()).ToList();

            // For ascending order
            randomNumbers.Sort();
            var result = Participants.OrderByDescending(item => item.OverallScore).ToList();
            for (int i =0;i< result.Count; i++)
            {
                string str = string.Format("{0}/{1}", result[i].Answer.Where(a => a.isCorrect).ToList().Count, result[i].Answer.Count);
                GenerateResult(String.Format("{0}  ({1})", result[i].Name, str), Convert.ToString( i +1), result[i].OverallScore);
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FinalScore_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void GenerateResult(string participantName, string index, int score)
        {
            FlowLayoutPanel fPanel = new FlowLayoutPanel();
            fPanel.FlowDirection = FlowDirection.LeftToRight;
            fPanel.AutoSize = true;
            fPanel.Size = new Size(510, 25);
            fPanel.Name = "fPanel_" + index;
            fPanel.BackColor = Color.Transparent;
            var SnoLabel = GenerateLabel(String.Format("{0}", index), index, "Sno", 20);
            var labelMember = GenerateLabel(participantName, index, "Member", 400);  
            var scoreLabel = GenerateLabel(String.Format("{0}", score), index, "score", 50);

            fPanel.Controls.Add(SnoLabel);
            fPanel.Controls.Add(labelMember);
            fPanel.Controls.Add(scoreLabel);
            
            flowLayoutPanel1.Controls.Add(fPanel);
        }

        private Label GenerateLabel(string text, string index, string name,int size)
        {
            return CreateControl<Label>(
                                label => label.Text = text,
                                label => label.Name = string.Format("{0}_{1}", name, index),
                                label => label.AutoSize = false,
                                label => label.Size = new Size(size, 50),
                                label => label.ForeColor = Color.White,
                                label => label.Padding = new Padding(0, 10, 0, 0),
                                label => label.Location = new Point(100, 20),
                                label => label.Font = new Font("Lucida san", 10, FontStyle.Bold));
        }

        private T CreateControl<T>(params Action<T>[] actions) where T : Control, new()
        {
            T control = new T();
            if (actions != null)
                foreach (var action in actions)
                    action(control);
            return control;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();
            Application.Exit();
        }
    }
}
