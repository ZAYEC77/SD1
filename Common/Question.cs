using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ExpertSystem
{
    public class Question
    {
        public int ID { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public List<Question> Children = new List<Question>();
        public Question()
        {
            Text = "";
            Title = "";
        }
        public void AddChild(Question q)
        {
            q.ID = Core.NextID;
            this.Children.Add(q);
            q.Parent = this;
        }
        public Question(XmlNode e, Question parent)
        {
            Parent = parent;
            ID = Core.NextID;
            Title = e.Attributes["title"].Value;
            Text = e.Attributes["text"].Value;
            if (e.ChildNodes.Count > 0)
            {
                foreach (XmlNode item in e.ChildNodes)
                {
                    Children.Add(new Question(item, this));
                }
            }
        }
        public Question Parent { get; set; }
        public String Error { get; set; }
        public bool Validation(bool first)
        {
            return true;
        }
        public void OutputTree(TreeView tree, TreeNode parent)
        {
            TreeNode me = new MyTreeNode() { ID = this.ID};
            if (parent == null)
            {
                me.Text = this.Text;
                tree.Nodes.Add(me);
            }
            else
            {
                me.Text = this.Title + " / " + this.Text;
                parent.Nodes.Add(me);
            }
            foreach (Question q in this.Children)
            {
                q.OutputTree(tree, me);
            }
        }


        public XmlNode GetXmlTree()
        {
            XmlNode node = Core.CreateElement(this.Title, this.Text);
            foreach (Question item in this.Children)
            {
                node.AppendChild(item.GetXmlTree());
            }
            return node;
        }

        public Question GetSelected(int id)
        {
            if (this.ID == id)
            {
                return this;
            }
            foreach (Question item in this.Children)
            {
                if (item.GetSelected(id) != null)
                {
                    return item.GetSelected(id);
                }
            }
            return null;
        }

        public void RemoveMe()
        {
            while (this.Children.Count > 0)
            {
                Children.First().RemoveMe();
            }
            if (Parent != null)
            {
                Parent.Children.Remove(this);
            }
        }
    }
}
