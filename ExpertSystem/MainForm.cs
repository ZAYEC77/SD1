using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ExpertSystem
{
    public partial class MainForm : Form
    {
        public Question Selected { get; set; }
        public MainForm()
        {
            InitializeComponent(); 
            Test();
        }

        private void Test()
        {
            Core.LoadFile(@"../../../file.xml");
            Core.OutputTree(treeView);
        }

        public void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Core.LoadFile(d.FileName);
                Core.OutputTree(treeView);
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void ShowSelected()
        {
            textBoxText.Text = Selected.Text;
            textBoxTitle.Text = Selected.Title;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void AddItem()
        {
            Question q = new Question();
            q.Text = textBoxText.Text;
            q.Title = textBoxTitle.Text;
            Selected.AddChild(q);
            Core.OutputTree(treeView);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemoveItem();
        }

        private void RemoveItem()
        {
            Selected.RemoveMe();
            Core.OutputTree(treeView);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MyTreeNode n = e.Node as MyTreeNode;
            Selected = Core.GetSelected(n.ID);
            ShowSelected();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void EditItem()
        {
            Selected.Text = textBoxText.Text;
            Selected.Title = textBoxTitle.Text;
            Core.OutputTree(treeView);
        }

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Core.SaveFile(d.FileName);
            }
        }
    }
}
