using SchoolApp.Properties;

namespace SchoolApp
{
    public partial class Form1 : Form
    {
        static class Program
        {
            [STAThread]
            static void Main()
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }

        public Form1()
        {
            InitializeComponent();
        }
        public MenuStrip GetMenuStrip()
        {
            return menuStrip1;
        }

        private void pRESCHOOLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PreSchool preschoolForm = new PreSchool();
            preschoolForm.Show();
        }

        private void KindertoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PreKinder prekinderForm = new PreKinder();
            prekinderForm.Show();
        }

        private void gardentoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kinder kinderForm = new Kinder();
            kinderForm.Show();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 homeform = new Form1();
            homeform.Show();
        }


        private void eNROLLMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Enroll enrollform = new Enroll();
            enrollform.Show();
        }

        private void aBOUTUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            About aboutform = new About();
            aboutform.Show();
        }

        private void cONTACTUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Contact contactform = new Contact();
            contactform.Show();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            this.Hide();

            switch (picturecount)
            {
                case 0:
                    PreSchool preschoolForm = new PreSchool();
                    preschoolForm.Show();
                    break;
                case 1:
                    PreKinder prekinderForm = new PreKinder();
                    prekinderForm.Show();
                    break;
                case 2:
                    Kinder kinderForm = new Kinder();
                    kinderForm.Show();
                    break;
                case 3:
                    About aboutform = new About();
                    aboutform.Show();
                    break;
                case 4:
                    Contact contactform = new Contact();
                    contactform.Show();
                    break;
            }
        }

        int picturecount = 0;

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (picturecount)
            {
                case 0:
                    this.BackgroundImage = Resources.homepagenew1;
                    btnEnroll.Text = "Enroll Now";
                    break;
                case 1:
                    this.BackgroundImage = Resources.homepage2;
                    break;
                 case 2:
                    this.BackgroundImage = Resources.homepage3;
                    break;
                 case 3:
                    btnEnroll.Text = "About Us";
                    this.BackgroundImage = Resources.homepage4;
                    break;
                case 4:
                    btnEnroll.Text = "Contact Us";
                    this.BackgroundImage = Resources.homepage5;
                    picturecount = 0;
                    return;
            }

            picturecount += 1;
        }
    }


}
