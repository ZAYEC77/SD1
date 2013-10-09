using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Windows.Forms;

namespace ExpertSystem
{
    public static class Core 
    {
        static int id = 0;
        static public int NextID { get{ return Core.id++;}}
        static Question Root;
        static XmlDocument doc = new XmlDocument();
        static XmlNode XmlRoot
        {
            get { return Core.doc.GetElementsByTagName("tree")[0]; }
            set 
            {
                Core.doc.GetElementsByTagName("tree")[0].RemoveAll();
                Core.doc.GetElementsByTagName("tree")[0].AppendChild(value);
            }
        }
        public static void LoadFile(String filePath)
        {            
            try
            {
                Core.id = 0;
                Core.doc.Load(filePath);
                Core.Root = new Question(Core.XmlRoot.FirstChild, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }           
        }

        public static void SaveFile(String filePath)
        {
            try
            {
                Core.XmlRoot = Core.Root.GetXmlTree();
                Core.doc.Save(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static void AddQuestion(Question q)
        {
            if ((q.Parent == null)&&(Core.Root != null))
            {
                throw new Exception("Виберіть предка для запитання");
            }
        }

        public static void OutputTree(TreeView tree)
        {
            tree.Nodes.Clear();
            Core.Root.OutputTree(tree, null);
        }

        public static XmlNode CreateElement(string title, string text)
        {
            XmlNode node = Core.doc.CreateElement("element");
            XmlAttribute attr1 = doc.CreateAttribute("title");
            attr1.Value = title;
            XmlAttribute attr2 = doc.CreateAttribute("text");
            attr2.Value = text;
            node.Attributes.Append(attr1);
            node.Attributes.Append(attr2);
            return node;
        }

        public static Question GetSelected(int id)
        {
            return Core.Root.GetSelected(id);
        }
    }
}
