using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class StudentList : Form
    {
        public StudentList()
        {
            InitializeComponent();
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            this.reportViewer1.LocalReport.EnableExternalImages = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
            //QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(textBox1.Text, QRCoder.QRCodeGenerator.ECCLevel.Q);
            //QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
            //Bitmap bmp = qRCode.GetGraphic(7);
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    bmp.Save(ms, ImageFormat.Bmp);

            //}
        }
    }
}
