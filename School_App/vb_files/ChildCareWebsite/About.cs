using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolApp
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Form1 form1 = new Form1();
            this.MainMenuStrip = form1.GetMenuStrip();
            this.Controls.Add(form1.GetMenuStrip());
        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}
