namespace SchoolApp
{
    partial class PreKinder
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
            // PreKinder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.prekinder;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1040, 545);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PreKinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Programs (Pre-Kinder)";
            Load += PreKinder_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}