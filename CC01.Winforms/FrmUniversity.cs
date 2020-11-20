using CC01.BLL;
using CC01.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FrmUniversity : Form
    {
        University olduniversity;
        UniversityBLO universityBLO;
        private Action callback;

        public FrmUniversity()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            universityBLO = new UniversityBLO(ConfigurationManager.AppSettings["DbFolder"]);

        }

        private void FrmUniversity_Load(object sender, EventArgs e)
        {
            loadData();
        }
        public FrmUniversity(Action callback) : this()
        {
            this.callback = callback;
        }

        public FrmUniversity(University university, Action callback) : this(callback)
        {
            this.olduniversity = university;
            txtName.Text = university.Name;
            txtTel.Text = university.Tel.ToString();
            pictureBox1.Image = university.Logo != null ? Image.FromStream(new MemoryStream(university.Logo)) : null;
            txtEmail.Text = university.Email;

        }



        private void loadData()
        {
            string value = txtSearch.Text;
            var universities = universityBLO.GetBy(
                x =>
                x.Name.ToLower().Contains(value) ||
                x.Tel.ToString().Contains(value)
                ).OrderBy(x => x.Name).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = universities;
            dataGridView1.ClearSelection();
        }


        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                University newuniversity = new University(
                txtName.Text,
                int.Parse(txtTel.Text),
                !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.olduniversity.Logo,
                txtEmail.Text
                );

                UniversityBLO universityBLO = new UniversityBLO(ConfigurationManager.AppSettings["DbFolder"]);

                if (this.olduniversity == null)
                    universityBLO.CreateUniversity(newuniversity);
                else
                    universityBLO.EditUniversity(olduniversity, newuniversity);

                MessageBox.Show(
                    "Save done!",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
                txtEmail.Clear();
                txtName.Clear();
                txtTel.Clear();
                pictureBox1.ImageLocation = null;
                loadData();

            }
            catch (TypingException ex)
            {
                MessageBox.Show(
                ex.Message,
                "Typing error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );

            }
            catch (DuplicateNameException ex)
            {
                MessageBox.Show(
                ex.Message,
                "Duplicate error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );

            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(
                ex.Message,
                "key not found error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );

            }
            catch (Exception ex)
            {
                ex.WriteToFile();

                MessageBox.Show(
                "An error occured please try again",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );

            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a Picture";
            ofd.Filter = "Image files|*.jpg; *.jpeg; *.png; *.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
                pictureBox1.ImageLocation = ofd.FileName;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new FrmUniversity
                        (
                            dataGridView1.SelectedRows[i].DataBoundItem as University,
                            loadData
                        );
                    this.Close();
                    f.Show();
                    f.WindowState = FormWindowState.Maximized;
                }

            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Confirmation ? ",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        universityBLO.DeleteUniversity(dataGridView1.SelectedRows[i].DataBoundItem as University);
                    }
                    loadData();


                }

            }

        }
        private void checkForm()
        {
            string text = string.Empty;
            txtName.BackColor = Color.White;
            txtTel.BackColor = Color.White;
            txtEmail.BackColor = Color.White;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                text += "- University Name can't be empty !\n";
                txtName.BackColor = Color.LightPink;

            }

            if (string.IsNullOrWhiteSpace(txtTel.Text))
            {
                text += "- University telephone can't be empty !\n";

            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                text += "- Email can't be empty !\n";
                txtTel.BackColor = Color.LightPink;

            }
            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = null;
        }
    }
}
