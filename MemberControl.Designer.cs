namespace Rencata.Quiz.Programme
{
    partial class MemberControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rdobtn = new System.Windows.Forms.RadioButton();
            this.lblcorrect = new System.Windows.Forms.Label();
            this.lblwrong = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rdobtn
            // 
            this.rdobtn.AutoSize = true;
            this.rdobtn.Location = new System.Drawing.Point(31, 3);
            this.rdobtn.Name = "rdobtn";
            this.rdobtn.Size = new System.Drawing.Size(85, 17);
            this.rdobtn.TabIndex = 0;
            this.rdobtn.TabStop = true;
            this.rdobtn.Text = "radioButton1";
            this.rdobtn.UseVisualStyleBackColor = true;
            // 
            // lblcorrect
            // 
            this.lblcorrect.AutoSize = true;
            this.lblcorrect.Location = new System.Drawing.Point(197, 7);
            this.lblcorrect.Name = "lblcorrect";
            this.lblcorrect.Size = new System.Drawing.Size(35, 13);
            this.lblcorrect.TabIndex = 1;
            this.lblcorrect.Text = "label1";
            // 
            // lblwrong
            // 
            this.lblwrong.AutoSize = true;
            this.lblwrong.Location = new System.Drawing.Point(289, 7);
            this.lblwrong.Name = "lblwrong";
            this.lblwrong.Size = new System.Drawing.Size(35, 13);
            this.lblwrong.TabIndex = 2;
            this.lblwrong.Text = "label2";
            // 
            // MemberControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblwrong);
            this.Controls.Add(this.lblcorrect);
            this.Controls.Add(this.rdobtn);
            this.Name = "MemberControl";
            this.Size = new System.Drawing.Size(326, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdobtn;
        private System.Windows.Forms.Label lblcorrect;
        private System.Windows.Forms.Label lblwrong;
    }
}
