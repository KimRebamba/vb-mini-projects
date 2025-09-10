namespace SchoolApp
{
    partial class Enroll
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblChildName;
        private TextBox txtChildName;
        private Label lblAge;
        private TextBox txtAge;
        private Label lblParentName;
        private TextBox txtParentName;
        private Label lblContact;
        private TextBox txtContact;
        private Label lblProgram;
        private ComboBox cmbProgram;
        private Button btnConfirm;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblChildName = new Label();
            txtChildName = new TextBox();
            lblAge = new Label();
            txtAge = new TextBox();
            lblParentName = new Label();
            txtParentName = new TextBox();
            lblContact = new Label();
            txtContact = new TextBox();
            lblProgram = new Label();
            cmbProgram = new ComboBox();
            btnConfirm = new Button();
            SuspendLayout();
            // 
            // lblChildName
            // 
            lblChildName.AutoSize = true;
            lblChildName.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblChildName.Location = new Point(50, 50);
            lblChildName.Name = "lblChildName";
            lblChildName.Size = new Size(120, 24);
            lblChildName.TabIndex = 0;
            lblChildName.Text = "Child's Name:";
            // 
            // txtChildName
            // 
            txtChildName.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtChildName.Location = new Point(200, 50);
            txtChildName.Name = "txtChildName";
            txtChildName.Size = new Size(300, 32);
            txtChildName.TabIndex = 1;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblAge.Location = new Point(50, 100);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(48, 24);
            lblAge.TabIndex = 2;
            lblAge.Text = "Age:";
            // 
            // txtAge
            // 
            txtAge.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtAge.Location = new Point(200, 100);
            txtAge.Name = "txtAge";
            txtAge.Size = new Size(300, 32);
            txtAge.TabIndex = 3;
            // 
            // lblParentName
            // 
            lblParentName.AutoSize = true;
            lblParentName.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblParentName.Location = new Point(50, 150);
            lblParentName.Name = "lblParentName";
            lblParentName.Size = new Size(130, 24);
            lblParentName.TabIndex = 4;
            lblParentName.Text = "Parent's Name:";
            // 
            // txtParentName
            // 
            txtParentName.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtParentName.Location = new Point(200, 150);
            txtParentName.Name = "txtParentName";
            txtParentName.Size = new Size(300, 32);
            txtParentName.TabIndex = 5;
            // 
            // lblContact
            // 
            lblContact.AutoSize = true;
            lblContact.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblContact.Location = new Point(50, 200);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(80, 24);
            lblContact.TabIndex = 6;
            lblContact.Text = "Contact:";
            // 
            // txtContact
            // 
            txtContact.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtContact.Location = new Point(200, 200);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(300, 32);
            txtContact.TabIndex = 7;
            // 
            // lblProgram
            // 
            lblProgram.AutoSize = true;
            lblProgram.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblProgram.Location = new Point(50, 250);
            lblProgram.Name = "lblProgram";
            lblProgram.Size = new Size(90, 24);
            lblProgram.TabIndex = 8;
            lblProgram.Text = "Program:";
            // 
            // cmbProgram
            // 
            cmbProgram.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProgram.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cmbProgram.FormattingEnabled = true;
            cmbProgram.Items.AddRange(new object[] { "Preschool", "Pre-Kinder", "Kindergarten" });
            cmbProgram.Location = new Point(200, 250);
            cmbProgram.Name = "cmbProgram";
            cmbProgram.Size = new Size(300, 32);
            cmbProgram.TabIndex = 9;
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnConfirm.Location = new Point(200, 300);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(150, 40);
            btnConfirm.TabIndex = 10;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // Enroll
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(lblChildName);
            Controls.Add(txtChildName);
            Controls.Add(lblAge);
            Controls.Add(txtAge);
            Controls.Add(lblParentName);
            Controls.Add(txtParentName);
            Controls.Add(lblContact);
            Controls.Add(txtContact);
            Controls.Add(lblProgram);
            Controls.Add(cmbProgram);
            Controls.Add(btnConfirm);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Enroll";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TREESCOOLERS (Enroll)";
            ResumeLayout(false);
            PerformLayout();
        }

       
    }
}

