using CC01.BO;
using CC01.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FrmStudent : Form
    {
        private Action callback;
        private Student oldStudent;
        private StudentBLO studentBLO;
        University u = new University();

        public FrmStudent()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            studentBLO = new StudentBLO(ConfigurationManager.AppSettings["DbFolder"]);

        }
            public FrmStudent(Action callback) : this()
        {
            this.callback = callback;
        }  

        public FrmStudent(Student student, Action callback) : this(callback)
        {
            this.oldStudent = student;
            txtFirstName.Text = student.FirstName;
            txtLastName.Text = student.LastName;
            dateTimePicker1.Text = student.BornOn.ToString();
            txtAt.Text = student.BornAt;
            txtTel.Text = student.TelS.ToString();
            pictureBox1.Image = student.Photo != null ? Image.FromStream(new MemoryStream(student.Photo)) : null;
            txtEmail.Text = student.EmailS;

            if (student.Sexe.ToString() == "Male")
                radioButton1.Checked = true;
            else if (student.Sexe.ToString() == "Female")
                radioButton2.Checked = true;
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }

            //radioButton1.Checked = (student.Sexe.ToString() == "Male") ? true : false;
            //radioButton2.Checked = (student.Sexe.ToString() == "Female") ? true : false;
        }

    private void loadData()
        {
            string value = txtSearch.Text;
            var students = studentBLO.GetBy(
                x =>
                x.FirstName.ToLower().Contains(value) ||
                x.LastName.ToLower().Contains(value) ||
                x.BornAt.ToLower().Contains(value)
                ).OrderBy(x => x.FirstName).ToArray();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
            dataGridView1.ClearSelection();
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a Picture";
            ofd.Filter = "Image files|*.jpg; *.jpeg; *.png; *.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
                pictureBox1.ImageLocation = ofd.FileName;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();
                string sex;

                if (radioButton1.Checked)
                    sex = radioButton1.Text;
                else if (radioButton2.Checked)
                    sex = radioButton2.Text;
                else
                    sex = "";



                Student newStudent = new Student(
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtEmail.Text,
                    int.Parse(txtTel.Text),
                    sex,
                    Convert.ToDateTime(dateTimePicker1.Value),
                    txtAt.Text,
                    !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldStudent?.Photo,
                    u
                    );


                StudentBLO studentBLO = new StudentBLO(ConfigurationManager.AppSettings["DbFolder"]);

                if (this.oldStudent == null)
                    studentBLO.CreateStudent(newStudent);
                else
                    studentBLO.EditStudent(oldStudent, newStudent);


                MessageBox.Show(
                    "Save Done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
                               

                if (callback != null)
                    callback();

                //if (oldStudent != null)
                //    Close();

                txtFirstName.Clear();
                txtLastName.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                txtAt.Clear();
                dateTimePicker1.Value = DateTime.Now;
                pictureBox1.ImageLocation = null;
                txtTel.Clear();
                txtEmail.Clear();
                txtFirstName.Focus();
                loadData();

                //QRCodeGenerator qr = new QRCodeGenerator();
                //QRCodeData data = qr.CreateQrCode(oldStudent.Matricule, QRCodeGenerator.ECCLevel.Q);
                //QRCode code = new QRCode(data);


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

        private void GetQRCode()
        {
        }

        private void checkForm()
        {
            string text = string.Empty;
            txtFirstName.BackColor = Color.White;
            txtLastName.BackColor = Color.White;

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                text += "- FirstName or LastName can't be empty !\n";
                txtFirstName.BackColor = Color.LightPink;
                txtLastName.BackColor = Color.LightPink;

            }

            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                text += "- Sex can't be empty !\n";

            }

            if (string.IsNullOrWhiteSpace(txtTel.Text))
            {
                text += "- Telephone can't be empty !\n";
                txtTel.BackColor = Color.LightPink;

            }
            if (string.IsNullOrWhiteSpace(txtAt.Text))
            {
                text += "- Place of birth can't be empty !\n";
                txtAt.BackColor = Color.LightPink;

            }
            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        private void picRemover_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = null;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {

                    Form f = new FrmStudent
                        (
                            dataGridView1.SelectedRows[i].DataBoundItem as Student,
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
                        studentBLO.DeleteStudent(dataGridView1.SelectedRows[i].DataBoundItem as Student);
                    }
                    loadData();


                }

            }

        }


        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }

        private void FrmStudent_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            List<StudentListPrint> items = new List<StudentListPrint>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++) 
            {
                Student s = dataGridView1.Rows[i].DataBoundItem as Student;
                items.Add
                  (
                      new StudentListPrint
                      (
                          s.FirstName,
                          s.LastName,
                          s.BornOn,
                          s.BornAt,
                          s.Photo,
                          s.Sexe,
                          s.EmailS,
                          s.TelS,
                          u
                      )
                  );
            }
            Form f = new StudentList("StudentsCard.rdlc", items);
            f.Show();

        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(sender, e);
        }
    }
}
