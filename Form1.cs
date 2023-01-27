using Newtonsoft.Json;
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
    public partial class Form1 : Form
    {
        public List<Teams> teamsandMembers;
        public Form1()
        {
            InitializeComponent();
            //comments
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string team = System.IO.File.ReadAllText(@"c:\Users\Bharanikumar.Eswar\Teamandmember.json");
            teamsandMembers = JsonConvert.DeserializeObject<List<Teams>>(team);


            treeView1.Parent.Text = "test";
        }
    }
}
