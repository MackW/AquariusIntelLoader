using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
namespace AquarisBasicMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextFile.CreateFile(textBox1.Text, textBox2.Text + "\\" + txtAppName.Text + ".txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CAQ.CreateCAQ(textBox1.Text, textBox2.Text, txtAppName.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox2.Text;
            folderBrowserDialog1.ShowDialog();
            textBox2.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = textBox1.Text;
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }


        private void txtAppName_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = txtAppName.Text.Trim().Length > 0;
        }
    }
}
