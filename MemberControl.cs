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
    public partial class MemberControl : UserControl
    {
        public MemberControl()
        {
            InitializeComponent();
        }

        private string _lblCorrect;
        private string _lblwrong;
        private string _rdoText;

        public string lblCorrect
        {
            get { return _lblCorrect; }
            set { _lblCorrect = value; lblcorrect.Text = value; }
        }

        public string lblWrong
        {
            get { return _lblwrong; }
            set { _lblwrong = value; lblwrong.Text = value; }
        }

        public string rdoText
        {
            get { return _rdoText; }
            set { 
                _rdoText = value; 
                rdobtn.Text = value;
                rdobtn.Name = "member";
                }
        }
    }
}
