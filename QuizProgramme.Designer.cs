using System.Drawing;

namespace Rencata.Quiz.Programme
{
    partial class QuizProgramme
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuizProgramme));
            this.Back = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.Check = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CountedQ = new System.Windows.Forms.Label();
            this.lblshowanswer = new System.Windows.Forms.Label();
            this.lblanswer = new System.Windows.Forms.Label();
            this.lblpointTitlte = new System.Windows.Forms.Label();
            this.lblpoint = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnResume = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Back
            // 
            this.Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back.Location = new System.Drawing.Point(1241, 801);
            this.Back.Margin = new System.Windows.Forms.Padding(10);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(150, 75);
            this.Back.TabIndex = 3;
            this.Back.Text = "Previous";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(1080, 801);
            this.btnNext.Margin = new System.Windows.Forms.Padding(10);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(150, 75);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // Check
            // 
            this.Check.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Check.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Check.Location = new System.Drawing.Point(918, 801);
            this.Check.Margin = new System.Windows.Forms.Padding(10);
            this.Check.Name = "Check";
            this.Check.Size = new System.Drawing.Size(150, 75);
            this.Check.TabIndex = 5;
            this.Check.Text = "Show";
            this.Check.UseVisualStyleBackColor = true;
            this.Check.Click += new System.EventHandler(this.Check_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.richTextBox1);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 65);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1382, 723);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(96)))), ((int)(((byte)(189)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1374, 96);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            this.richTextBox1.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.richTextBox1_ContentsResized);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Choose the answer :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(1158, 13);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(30, 30);
            this.btnPause.TabIndex = 37;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(428, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            // 
            // CountedQ
            // 
            this.CountedQ.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CountedQ.AutoSize = true;
            this.CountedQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountedQ.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.CountedQ.Location = new System.Drawing.Point(404, 22);
            this.CountedQ.Name = "CountedQ";
            this.CountedQ.Size = new System.Drawing.Size(274, 33);
            this.CountedQ.TabIndex = 11;
            this.CountedQ.Text = "Quiz\'s Programme";
            this.CountedQ.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblshowanswer
            // 
            this.lblshowanswer.AutoSize = true;
            this.lblshowanswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblshowanswer.ForeColor = System.Drawing.Color.Blue;
            this.lblshowanswer.Location = new System.Drawing.Point(17, 801);
            this.lblshowanswer.Name = "lblshowanswer";
            this.lblshowanswer.Size = new System.Drawing.Size(83, 20);
            this.lblshowanswer.TabIndex = 16;
            this.lblshowanswer.Text = "Answer : ";
            // 
            // lblanswer
            // 
            this.lblanswer.AutoSize = true;
            this.lblanswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblanswer.Location = new System.Drawing.Point(18, 833);
            this.lblanswer.Name = "lblanswer";
            this.lblanswer.Size = new System.Drawing.Size(50, 16);
            this.lblanswer.TabIndex = 17;
            this.lblanswer.Text = "label1";
            // 
            // lblpointTitlte
            // 
            this.lblpointTitlte.AutoSize = true;
            this.lblpointTitlte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpointTitlte.ForeColor = System.Drawing.Color.Blue;
            this.lblpointTitlte.Location = new System.Drawing.Point(702, 801);
            this.lblpointTitlte.Name = "lblpointTitlte";
            this.lblpointTitlte.Size = new System.Drawing.Size(69, 20);
            this.lblpointTitlte.TabIndex = 18;
            this.lblpointTitlte.Text = "Points :";
            // 
            // lblpoint
            // 
            this.lblpoint.AutoSize = true;
            this.lblpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpoint.Location = new System.Drawing.Point(703, 836);
            this.lblpoint.Name = "lblpoint";
            this.lblpoint.Size = new System.Drawing.Size(50, 16);
            this.lblpoint.TabIndex = 19;
            this.lblpoint.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = " Team and Members";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Wrong";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(290, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Correct";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(377, 704);
            this.flowLayoutPanel2.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(1398, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 723);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flowLayoutPanel3);
            this.panel2.Location = new System.Drawing.Point(1398, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(395, 724);
            this.panel2.TabIndex = 30;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoScroll = true;
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 4);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(389, 720);
            this.flowLayoutPanel3.TabIndex = 0;
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(1277, 12);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(30, 30);
            this.btnResume.TabIndex = 34;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(1796, 69);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(11, 48);
            this.axWindowsMediaPlayer1.TabIndex = 31;
            this.axWindowsMediaPlayer1.Enter += new System.EventHandler(this.axWindowsMediaPlayer1_Enter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Rencata.Quiz.Programme.Properties.Resources.icons8_play_32;
            this.pictureBox3.Location = new System.Drawing.Point(21, 13);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 33);
            this.pictureBox3.TabIndex = 39;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Rencata.Quiz.Programme.Properties.Resources.playandpause__1_;
            this.pictureBox2.Location = new System.Drawing.Point(1377, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(47, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Rencata.Quiz.Programme.Properties.Resources.playandpause;
            this.pictureBox1.Location = new System.Drawing.Point(1266, -107);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnStop.BackgroundImage = global::Rencata.Quiz.Programme.Properties.Resources.output_onlinepngtools;
            this.btnStop.Image = global::Rencata.Quiz.Programme.Properties.Resources.output_onlinepngtools;
            this.btnStop.Location = new System.Drawing.Point(1003, 25);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(30, 30);
            this.btnStop.TabIndex = 33;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = global::Rencata.Quiz.Programme.Properties.Resources.playandpause;
            this.btnPlay.Location = new System.Drawing.Point(918, 5);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(30, 30);
            this.btnPlay.TabIndex = 32;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // timerProgressBar
            // 
            this.timerProgressBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.timerProgressBar.AnimationSpeed = 500;
            this.timerProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.timerProgressBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.timerProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.timerProgressBar.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(96)))), ((int)(((byte)(189)))));
            this.timerProgressBar.InnerMargin = 2;
            this.timerProgressBar.InnerWidth = -1;
            this.timerProgressBar.Location = new System.Drawing.Point(889, 4);
            this.timerProgressBar.MarqueeAnimationSpeed = 20000;
            this.timerProgressBar.Name = "timerProgressBar";
            this.timerProgressBar.OuterColor = System.Drawing.Color.Transparent;
            this.timerProgressBar.OuterMargin = 0;
            this.timerProgressBar.OuterWidth = 1;
            this.timerProgressBar.ProgressColor = System.Drawing.Color.Navy;
            this.timerProgressBar.ProgressWidth = 8;
            this.timerProgressBar.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.timerProgressBar.Size = new System.Drawing.Size(65, 60);
            this.timerProgressBar.StartAngle = 270;
            this.timerProgressBar.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.timerProgressBar.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.timerProgressBar.SubscriptText = "";
            this.timerProgressBar.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.timerProgressBar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.timerProgressBar.SuperscriptText = "";
            this.timerProgressBar.TabIndex = 38;
            this.timerProgressBar.TextMargin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.timerProgressBar.Value = 68;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(57, 7);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(243, 48);
            this.axWindowsMediaPlayer1.TabIndex = 31;
            this.axWindowsMediaPlayer1.Enter += new System.EventHandler(this.axWindowsMediaPlayer1_Enter);
            // 
            // QuizProgramme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(96)))), ((int)(((byte)(189)))));
            this.ClientSize = new System.Drawing.Size(1805, 886);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblpoint);
            this.Controls.Add(this.lblpointTitlte);
            this.Controls.Add(this.lblanswer);
            this.Controls.Add(this.lblshowanswer);
            this.Controls.Add(this.CountedQ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.Check);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.Back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "QuizProgramme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quiz";
            this.Load += new System.EventHandler(this.Quiz_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button Check;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CountedQ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblshowanswer;
        private System.Windows.Forms.Label lblanswer;
        private System.Windows.Forms.Label lblpointTitlte;
        private System.Windows.Forms.Label lblpoint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

