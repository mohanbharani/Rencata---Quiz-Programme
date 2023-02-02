namespace Rencata.Quiz.Programme
{
    partial class FileUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileUpload));
            this.btnNext = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(98)))), ((int)(((byte)(150)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(263, 253);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(150, 50);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Add Details";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(41)))), ((int)(((byte)(67)))));
            this.richTextBox1.Location = new System.Drawing.Point(163, 74);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(250, 35);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(98)))), ((int)(((byte)(150)))));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowse.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(443, 74);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(84, 35);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            this.richTextBox2.Enabled = false;
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(41)))), ((int)(((byte)(67)))));
            this.richTextBox2.Location = new System.Drawing.Point(163, 177);
            this.richTextBox2.Multiline = false;
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(250, 35);
            this.richTextBox2.TabIndex = 13;
            this.richTextBox2.Text = "";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(41)))), ((int)(((byte)(67)))));
            this.checkBox1.Location = new System.Drawing.Point(163, 134);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(258, 19);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "If you want to continue previous quiz";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // FileUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(236)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(627, 341);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Question\'s File";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileUpload_FormClosing);
            this.Load += new System.EventHandler(this.FileUpload_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnBrowse;
        protected System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}