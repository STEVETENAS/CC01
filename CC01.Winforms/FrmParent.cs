using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WInform
{
    public partial class FrmParent : Form
    {
        public FrmParent()
        {
            InitializeComponent();
        }

        private void newSchoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new FrmUniversity();
            f.Parent = this;
            f.Show();
            f.WindowState = FormWindowState.Maximized;

        }

        private void newStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form s = new FrmStudent();
            s.Parent = this;
            s.Show();
            s.WindowState = FormWindowState.Maximized;
        }
    }
}
