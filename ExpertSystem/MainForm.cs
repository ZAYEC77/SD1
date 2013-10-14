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
        public MyTreeNode SelectedNode
        {
            get
            {
                return Core.GetSelectedNode(Selected.ID, treeView.Nodes);
            }
        }
        public MainForm()
        {
            InitializeComponent();
            MakeNew();
        }

        public void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "XML Expert file| *.xml";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Core.LoadFile(d.FileName);
                Core.OutputTree(treeView);
                if (Core.Root != null)
                {
                    treeView.SelectedNode = treeView.Nodes[0];
                }
                textBox1.Text = Core.Author;
            }
        }


        private void ShowSelected()
        {
            textBoxText.Text = Selected.Text;
            textBoxTitle.Text = Selected.Title;
            listBox.Items.Clear();
            foreach (Question item in Selected.Children)
            {
                listBox.Items.Add(item.Title);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void AddItem()
        {
            if (textBoxTitle.Text == "")
            { 
                MessageBox.Show("Введіть дані");
                return;
            }
            Question q = new Question();
            q.Text = textBoxText.Text;
            q.Title = textBoxTitle.Text;
            if (Selected != null)
            {
                Selected.AddChild(q);
            }
            else
            {
                MessageBox.Show("Спочатку виберіть батьківський елемент");
            }
            Core.OutputTree(treeView);
            ShowSelectedInTree();
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
            ShowSelectedInTree();
        }

        private void ShowSelectedInTree()
        {
            treeView.SelectedNode = SelectedNode;
            treeView.Update();
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
            d.Filter = "XML Expert file| *.xml";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Core.SaveFile(d.FileName);
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {
                Selected = Selected.Children[listBox.SelectedIndex];
                ShowSelected();
                ShowSelectedInTree();
            }
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void довідкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Модуль експертної системи:\n\"Діагностика персонального копм'ютера\"");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Selected.Parent != null)
            {
                Selected = Selected.Parent;
                ShowSelected();
                ShowSelectedInTree();
            }
        }

        private void додатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void новийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeNew();
        }

        private void MakeNew()
        {
            Core.LoadFile("new.xml");
            Core.OutputTree(treeView);
            if (Core.Root != null)
            {
                treeView.SelectedNode = treeView.Nodes[0];
            }
            textBox1.Text = Core.Author;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Core.Author = textBox1.Text;
        }
    }
}
