﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace university_scheduler
{
    public partial class addCourseForm : Form
    {
        public addCourseForm()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void addCourseForm_Load(object sender, EventArgs e)
        {
            termCombo.SelectedIndex = 0;
        }
        

        private void selectResource_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hopa");
            viewResourcesForm resForm = new viewResourcesForm();
            resForm.saveResourceBTN.Visible = true;
            resForm.deleteResourceBTN.Visible = false;
            resForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            DialogResult dialogresult = resForm.ShowDialog();
        }
    }
}
