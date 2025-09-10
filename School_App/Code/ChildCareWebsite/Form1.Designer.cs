namespace SchoolApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            homeToolStripMenuItem = new ToolStripMenuItem();
            pROGRAMSToolStripMenuItem = new ToolStripMenuItem();
            pRESCHOOLToolStripMenuItem = new ToolStripMenuItem();
            KindertoolStripMenuItem1 = new ToolStripMenuItem();
            gardentoolStripMenuItem2 = new ToolStripMenuItem();
            sERVICESToolStripMenuItem = new ToolStripMenuItem();
            eNROLLMENTToolStripMenuItem = new ToolStripMenuItem();
            mOREToolStripMenuItem = new ToolStripMenuItem();
            aBOUTUSToolStripMenuItem = new ToolStripMenuItem();
            cONTACTUSToolStripMenuItem = new ToolStripMenuItem();
            eXITToolStripMenuItem = new ToolStripMenuItem();
            btnNext = new Button();
            btnEnroll = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.LightBlue;
            menuStrip1.Dock = DockStyle.Bottom;
            menuStrip1.ForeColor = Color.DarkBlue;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { homeToolStripMenuItem, pROGRAMSToolStripMenuItem, sERVICESToolStripMenuItem, mOREToolStripMenuItem });
            menuStrip1.Location = new Point(0, 501);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1040, 44);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // homeToolStripMenuItem
            // 
            homeToolStripMenuItem.BackColor = Color.LightCoral;
            homeToolStripMenuItem.Font = new Font("Tahoma", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            homeToolStripMenuItem.ForeColor = Color.White;
            homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            homeToolStripMenuItem.Size = new Size(110, 40);
            homeToolStripMenuItem.Text = "HOME";
            homeToolStripMenuItem.Click += homeToolStripMenuItem_Click;
            // 
            // pROGRAMSToolStripMenuItem
            // 
            pROGRAMSToolStripMenuItem.BackColor = Color.LightGreen;
            pROGRAMSToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pRESCHOOLToolStripMenuItem, KindertoolStripMenuItem1, gardentoolStripMenuItem2 });
            pROGRAMSToolStripMenuItem.Font = new Font("Tahoma", 18F);
            pROGRAMSToolStripMenuItem.ForeColor = Color.DarkGreen;
            pROGRAMSToolStripMenuItem.Name = "pROGRAMSToolStripMenuItem";
            pROGRAMSToolStripMenuItem.Size = new Size(183, 40);
            pROGRAMSToolStripMenuItem.Text = "PROGRAMS";
            // 
            // pRESCHOOLToolStripMenuItem
            // 
            pRESCHOOLToolStripMenuItem.BackColor = Color.LightGreen;
            pRESCHOOLToolStripMenuItem.ForeColor = Color.DarkGreen;
            pRESCHOOLToolStripMenuItem.Name = "pRESCHOOLToolStripMenuItem";
            pRESCHOOLToolStripMenuItem.Size = new Size(323, 40);
            pRESCHOOLToolStripMenuItem.Text = "PRE-SCHOOL";
            pRESCHOOLToolStripMenuItem.Click += pRESCHOOLToolStripMenuItem_Click;
            // 
            // KindertoolStripMenuItem1
            // 
            KindertoolStripMenuItem1.BackColor = Color.LightGreen;
            KindertoolStripMenuItem1.ForeColor = Color.DarkGreen;
            KindertoolStripMenuItem1.Name = "KindertoolStripMenuItem1";
            KindertoolStripMenuItem1.Size = new Size(323, 40);
            KindertoolStripMenuItem1.Text = "PRE-KINDER";
            KindertoolStripMenuItem1.Click += KindertoolStripMenuItem1_Click;
            // 
            // gardentoolStripMenuItem2
            // 
            gardentoolStripMenuItem2.BackColor = Color.LightGreen;
            gardentoolStripMenuItem2.ForeColor = Color.DarkGreen;
            gardentoolStripMenuItem2.Name = "gardentoolStripMenuItem2";
            gardentoolStripMenuItem2.Size = new Size(323, 40);
            gardentoolStripMenuItem2.Text = "KINDERGARDEN";
            gardentoolStripMenuItem2.Click += gardentoolStripMenuItem2_Click;
            // 
            // sERVICESToolStripMenuItem
            // 
            sERVICESToolStripMenuItem.BackColor = Color.LightYellow;
            sERVICESToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { eNROLLMENTToolStripMenuItem });
            sERVICESToolStripMenuItem.Font = new Font("Tahoma", 18F);
            sERVICESToolStripMenuItem.ForeColor = Color.DarkOrange;
            sERVICESToolStripMenuItem.Name = "sERVICESToolStripMenuItem";
            sERVICESToolStripMenuItem.Size = new Size(163, 40);
            sERVICESToolStripMenuItem.Text = "SERVICES";
            // 
            // eNROLLMENTToolStripMenuItem
            // 
            eNROLLMENTToolStripMenuItem.BackColor = Color.LightPink;
            eNROLLMENTToolStripMenuItem.ForeColor = Color.DarkRed;
            eNROLLMENTToolStripMenuItem.Name = "eNROLLMENTToolStripMenuItem";
            eNROLLMENTToolStripMenuItem.Size = new Size(298, 40);
            eNROLLMENTToolStripMenuItem.Text = "ENROLL NOW!";
            eNROLLMENTToolStripMenuItem.Click += eNROLLMENTToolStripMenuItem_Click;
            // 
            // mOREToolStripMenuItem
            // 
            mOREToolStripMenuItem.BackColor = Color.LightSkyBlue;
            mOREToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aBOUTUSToolStripMenuItem, cONTACTUSToolStripMenuItem, eXITToolStripMenuItem });
            mOREToolStripMenuItem.Font = new Font("Tahoma", 18F);
            mOREToolStripMenuItem.ForeColor = Color.DarkBlue;
            mOREToolStripMenuItem.Name = "mOREToolStripMenuItem";
            mOREToolStripMenuItem.Size = new Size(109, 40);
            mOREToolStripMenuItem.Text = "MORE";
            // 
            // aBOUTUSToolStripMenuItem
            // 
            aBOUTUSToolStripMenuItem.BackColor = Color.LightSkyBlue;
            aBOUTUSToolStripMenuItem.ForeColor = Color.DarkBlue;
            aBOUTUSToolStripMenuItem.Name = "aBOUTUSToolStripMenuItem";
            aBOUTUSToolStripMenuItem.Size = new Size(281, 40);
            aBOUTUSToolStripMenuItem.Text = "ABOUT US";
            aBOUTUSToolStripMenuItem.Click += aBOUTUSToolStripMenuItem_Click;
            // 
            // cONTACTUSToolStripMenuItem
            // 
            cONTACTUSToolStripMenuItem.BackColor = Color.LightSkyBlue;
            cONTACTUSToolStripMenuItem.ForeColor = Color.DarkBlue;
            cONTACTUSToolStripMenuItem.Name = "cONTACTUSToolStripMenuItem";
            cONTACTUSToolStripMenuItem.Size = new Size(281, 40);
            cONTACTUSToolStripMenuItem.Text = "CONTACT US";
            cONTACTUSToolStripMenuItem.Click += cONTACTUSToolStripMenuItem_Click;
            // 
            // eXITToolStripMenuItem
            // 
            eXITToolStripMenuItem.BackColor = Color.LightSkyBlue;
            eXITToolStripMenuItem.ForeColor = Color.DarkBlue;
            eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            eXITToolStripMenuItem.Size = new Size(281, 40);
            eXITToolStripMenuItem.Text = "EXIT";
            eXITToolStripMenuItem.Click += eXITToolStripMenuItem_Click;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.LightBlue;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Tahoma", 14F, FontStyle.Bold);
            btnNext.ForeColor = Color.DarkBlue;
            btnNext.Location = new Point(928, 426);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(91, 59);
            btnNext.TabIndex = 1;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // btnEnroll
            // 
            btnEnroll.FlatStyle = FlatStyle.Flat;
            btnEnroll.Font = new Font("Tahoma", 14F, FontStyle.Bold);
            btnEnroll.ForeColor = Color.Black;
            btnEnroll.Location = new Point(61, 336);
            btnEnroll.Name = "btnEnroll";
            btnEnroll.Size = new Size(182, 57);
            btnEnroll.TabIndex = 2;
            btnEnroll.Text = "Enroll Now";
            btnEnroll.UseVisualStyleBackColor = true;
            btnEnroll.Click += btnEnroll_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.homepagenew1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1040, 545);
            Controls.Add(btnEnroll);
            Controls.Add(btnNext);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TREESCOOLERS (Home)";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem homeToolStripMenuItem;
        private ToolStripMenuItem pROGRAMSToolStripMenuItem;
        private ToolStripMenuItem sERVICESToolStripMenuItem;
        private ToolStripMenuItem cHATWITHUSToolStripMenuItem;
        private ToolStripMenuItem eNROLLMENTToolStripMenuItem;
        private ToolStripMenuItem mOREToolStripMenuItem;
        private ToolStripMenuItem aBOUTUSToolStripMenuItem;
        private ToolStripMenuItem cONTACTUSToolStripMenuItem;
        private ToolStripMenuItem pRESCHOOLToolStripMenuItem;
        private ToolStripMenuItem KindertoolStripMenuItem1;
        private ToolStripMenuItem gardentoolStripMenuItem2;
        private ToolStripMenuItem eXITToolStripMenuItem;
        private Button btnNext;
        private Button btnEnroll;
    }
}
