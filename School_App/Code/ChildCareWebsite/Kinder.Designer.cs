namespace SchoolApp
{
    partial class Kinder
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
            SuspendLayout();
            // 
            // Kinder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.kindergarden;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1040, 545);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Kinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Programs (Kinder)";
            Load += Kinder_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}