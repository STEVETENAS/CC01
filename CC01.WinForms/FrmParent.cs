using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FrmParent : Form
    {
        public FrmParent()
        {
            InitializeComponent();
        }

        private void newUniversityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new FrmUniversity();
            f.MdiParent = this;
            f.Show();
            f.WindowState = FormWindowState.Maximized;
        }

        private void newStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new FrmStudent();
            f.MdiParent = this;
            f.Show();
            f.WindowState = FormWindowState.Maximized;
        }
    }
}
