using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExpertSystem;

namespace Client
{
    public partial class MainForm : Form
    {
        protected Question Selected { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "XML Expert file| *.xml";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Core.LoadFile(d.FileName);
                BeginTest();
            }
        }

        private void BeginTest()
        {
            Selected = Core.GetSelected(0);
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            buttonBack.Enabled = (Selected.Parent != null);
            button1.Visible = (Selected.Children.Count == 0);
            textBox1.Text = Selected.Text;
            listBox1.Visible = (Selected.Children.Count != 0);
            listBox1.Items.Clear();
            foreach (Question item in Selected.Children)
            {
                listBox1.Items.Add(item.Title);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;
            Selected = Selected.Children[listBox1.SelectedIndex];
            ShowQuestion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Selected = Selected.Parent;
            ShowQuestion();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void довідкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.OpenHelp();
        }
    }
}
