using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace HouseholdAppliancesSalesSystem
{
    public partial class OtherProducts : Form
    {

        private string strProductModel;
        static string xmlPath;
        static XmlDocument xml;
        private string strUserName;
        int x1 = 0;
        int y1 = 0;
        int x2 = 0;
        int y2 = 0;
        int x3;
        int y3;

        public OtherProducts()
        {
            InitializeComponent();
        }

        public void setUserName(string strUserName)
        {
            this.strUserName = strUserName;
        }

        public void setProductModel(string strProductModel)
        {
            this.strProductModel = strProductModel;
        }

        private void OtherProducts_Load(object sender, EventArgs e)
        {
            xmlPath = "C:\\xmlFile\\" + this.strUserName + "\\OtherProducts.xml";
            //初始化 XML类 
            xml = new XmlDocument();
            //找到路径
            xml.Load(xmlPath);
            //找到主节点
            XmlNode root = xml.SelectSingleNode("Tables");
            XmlNodeList xmlNodeList = root.ChildNodes;
            if (!string.IsNullOrEmpty(this.strProductModel))
            {
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    if (string.Equals(this.strProductModel, xmlNode.Attributes.GetNamedItem("productModel").Value))
                    {
                        this.text1.Text = xmlNode.Attributes.GetNamedItem("text1").Value;
                        this.text2.Text = xmlNode.Attributes.GetNamedItem("text2").Value;
                        this.text3.Text = xmlNode.Attributes.GetNamedItem("text3").Value;
                        this.text4.Text = xmlNode.Attributes.GetNamedItem("text4").Value;
                        this.text5.Text = xmlNode.Attributes.GetNamedItem("text5").Value;
                        this.text6.Text = xmlNode.Attributes.GetNamedItem("text6").Value;
                        this.text7.Text = xmlNode.Attributes.GetNamedItem("text7").Value;
                        this.text8.Text = xmlNode.Attributes.GetNamedItem("text8").Value;
                        this.text9.Text = xmlNode.Attributes.GetNamedItem("text9").Value;
                        this.text10.Text = xmlNode.Attributes.GetNamedItem("text10").Value;
                        this.text11.Text = xmlNode.Attributes.GetNamedItem("text11").Value;
                        this.text12.Text = xmlNode.Attributes.GetNamedItem("text12").Value;
                        this.text13.Text = xmlNode.Attributes.GetNamedItem("text13").Value;
                        this.text14.Text = xmlNode.Attributes.GetNamedItem("text14").Value;
                        this.text15.Text = xmlNode.Attributes.GetNamedItem("text15").Value;
                        this.attribute1.Text = xmlNode.Attributes.GetNamedItem("attribute1").Value;
                        this.attribute2.Text = xmlNode.Attributes.GetNamedItem("attribute2").Value;
                        this.attribute3.Text = xmlNode.Attributes.GetNamedItem("attribute3").Value;
                        this.attribute4.Text = xmlNode.Attributes.GetNamedItem("attribute4").Value;
                        this.attribute5.Text = xmlNode.Attributes.GetNamedItem("attribute5").Value;
                        this.attribute6.Text = xmlNode.Attributes.GetNamedItem("attribute6").Value;
                        this.attribute7.Text = xmlNode.Attributes.GetNamedItem("attribute7").Value;
                        this.attribute8.Text = xmlNode.Attributes.GetNamedItem("attribute8").Value;
                        this.attribute9.Text = xmlNode.Attributes.GetNamedItem("attribute9").Value;
                        this.attribute10.Text = xmlNode.Attributes.GetNamedItem("attribute10").Value;
                        this.attribute11.Text = xmlNode.Attributes.GetNamedItem("attribute11").Value;
                        this.attribute12.Text = xmlNode.Attributes.GetNamedItem("attribute12").Value;
                        this.attribute13.Text = xmlNode.Attributes.GetNamedItem("attribute13").Value;
                        this.attribute14.Text = xmlNode.Attributes.GetNamedItem("attribute14").Value;
                        this.attribute15.Text = xmlNode.Attributes.GetNamedItem("attribute15").Value;
                        this.textBox3.Text = xmlNode.Attributes.GetNamedItem("productModel").Value;
                        this.textBox2.Text = xmlNode.Attributes.GetNamedItem("remarks").Value;
                        if (!string.IsNullOrEmpty(xmlNode.Attributes.GetNamedItem("picture").Value))
                        {
                            if (File.Exists("C:\\xmlFile\\" + this.strUserName + "\\picture\\" + xmlNode.Attributes.GetNamedItem("picture").Value))
                            {
                                this.pictureBox1.Image = Image.FromFile("C:\\xmlFile\\" + this.strUserName + "\\picture\\" + xmlNode.Attributes.GetNamedItem("picture").Value);
                                this.pictureBox1.AccessibleName = xmlNode.Attributes.GetNamedItem("picture").Value;
                            }
                        }
                        break;
                    }
                }
            }
            List<ListItem> items = new List<ListItem>();//添加项的集合
            ListItem listItem = new ListItem("", "");
            items.Add(listItem);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                listItem = new ListItem(xmlNode.Attributes.GetNamedItem("productModel").Value, xmlNode.Attributes.GetNamedItem("productModel").Value);
                items.Add(listItem);
            }
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Id";
            this.comboBox1.DataSource = items;
            this.comboBox1.SelectedIndex = 0;

            x3 = this.pictureBox1.Location.X;
            y3 = this.pictureBox1.Location.Y;

        }

        public class ListItem : System.Object
        {
            private string id = string.Empty;

            public string Id
            {
                get { return id; }
                set { id = value; }
            }
            private string name = string.Empty;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public ListItem(string sid, string sname)
            {
                this.Id = sid;
                this.Name = sname;
            }

            public override string ToString()
            {
                return this.Name;
            }
        }

        private void OtherProducts_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            if (1 == fc.Count)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox3.Text))
            {
                MessageBox.Show("“型号”的值不能为空！", "提示");
                return;
            }
            //找到主节点
            XmlNode root = xml.SelectSingleNode("Tables");
            XmlNodeList xmlNodeList = root.ChildNodes;
            Boolean insflg = false;
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (string.Equals(this.textBox3.Text, xmlNode.Attributes.GetNamedItem("productModel").Value))
                {
                    xmlNode.Attributes.GetNamedItem("text1").InnerText = this.text1.Text;
                    xmlNode.Attributes.GetNamedItem("text2").InnerText = this.text2.Text;
                    xmlNode.Attributes.GetNamedItem("text3").InnerText = this.text3.Text;
                    xmlNode.Attributes.GetNamedItem("text4").InnerText = this.text4.Text;
                    xmlNode.Attributes.GetNamedItem("text5").InnerText = this.text5.Text;
                    xmlNode.Attributes.GetNamedItem("text6").InnerText = this.text6.Text;
                    xmlNode.Attributes.GetNamedItem("text7").InnerText = this.text7.Text;
                    xmlNode.Attributes.GetNamedItem("text8").InnerText = this.text8.Text;
                    xmlNode.Attributes.GetNamedItem("text9").InnerText = this.text9.Text;
                    xmlNode.Attributes.GetNamedItem("text10").InnerText = this.text10.Text;
                    xmlNode.Attributes.GetNamedItem("text11").InnerText = this.text11.Text;
                    xmlNode.Attributes.GetNamedItem("text12").InnerText = this.text12.Text;
                    xmlNode.Attributes.GetNamedItem("text13").InnerText = this.text13.Text;
                    xmlNode.Attributes.GetNamedItem("text14").InnerText = this.text14.Text;
                    xmlNode.Attributes.GetNamedItem("text15").InnerText = this.text15.Text;
                    xmlNode.Attributes.GetNamedItem("attribute1").InnerText = this.attribute1.Text;
                    xmlNode.Attributes.GetNamedItem("attribute2").InnerText = this.attribute2.Text;
                    xmlNode.Attributes.GetNamedItem("attribute3").InnerText = this.attribute3.Text;
                    xmlNode.Attributes.GetNamedItem("attribute4").InnerText = this.attribute4.Text;
                    xmlNode.Attributes.GetNamedItem("attribute5").InnerText = this.attribute5.Text;
                    xmlNode.Attributes.GetNamedItem("attribute6").InnerText = this.attribute6.Text;
                    xmlNode.Attributes.GetNamedItem("attribute7").InnerText = this.attribute7.Text;
                    xmlNode.Attributes.GetNamedItem("attribute8").InnerText = this.attribute8.Text;
                    xmlNode.Attributes.GetNamedItem("attribute9").InnerText = this.attribute9.Text;
                    xmlNode.Attributes.GetNamedItem("attribute10").InnerText = this.attribute10.Text;
                    xmlNode.Attributes.GetNamedItem("attribute11").InnerText = this.attribute11.Text;
                    xmlNode.Attributes.GetNamedItem("attribute12").InnerText = this.attribute12.Text;
                    xmlNode.Attributes.GetNamedItem("attribute13").InnerText = this.attribute13.Text;
                    xmlNode.Attributes.GetNamedItem("attribute14").InnerText = this.attribute14.Text;
                    xmlNode.Attributes.GetNamedItem("attribute15").InnerText = this.attribute15.Text;
                    xmlNode.Attributes.GetNamedItem("picture").InnerText = this.pictureBox1.AccessibleName.ToString();
                    xmlNode.Attributes.GetNamedItem("remarks").InnerText = this.textBox2.Text;
                    insflg = true;
                    break;
                }
            }
            if (!insflg)
            {
                XmlNode node = xml.CreateElement("Record");

                XmlAttribute elemLoad1 = xml.CreateAttribute("text1");
                elemLoad1.InnerText = this.text1.Text;
                XmlAttribute elemLoad2 = xml.CreateAttribute("text2");
                elemLoad2.InnerText = this.text2.Text;
                XmlAttribute elemLoad3 = xml.CreateAttribute("text3");
                elemLoad3.InnerText = this.text3.Text;
                XmlAttribute elemLoad4 = xml.CreateAttribute("text4");
                elemLoad4.InnerText = this.text4.Text;
                XmlAttribute elemLoad5 = xml.CreateAttribute("text5");
                elemLoad5.InnerText = this.text5.Text;
                XmlAttribute elemLoad6 = xml.CreateAttribute("text6");
                elemLoad6.InnerText = this.text6.Text;
                XmlAttribute elemLoad7 = xml.CreateAttribute("text7");
                elemLoad7.InnerText = this.text7.Text;
                XmlAttribute elemLoad8 = xml.CreateAttribute("text8");
                elemLoad8.InnerText = this.text8.Text;
                XmlAttribute elemLoad9 = xml.CreateAttribute("text9");
                elemLoad9.InnerText = this.text9.Text;
                XmlAttribute elemLoad10 = xml.CreateAttribute("text10");
                elemLoad10.InnerText = this.text10.Text;
                XmlAttribute elemLoad11 = xml.CreateAttribute("text11");
                elemLoad11.InnerText = this.text11.Text;
                XmlAttribute elemLoad12 = xml.CreateAttribute("text12");
                elemLoad12.InnerText = this.text12.Text;
                XmlAttribute elemLoad13 = xml.CreateAttribute("text13");
                elemLoad13.InnerText = this.text13.Text;
                XmlAttribute elemLoad14 = xml.CreateAttribute("text14");
                elemLoad14.InnerText = this.text14.Text;
                XmlAttribute elemLoad15 = xml.CreateAttribute("text15");
                elemLoad15.InnerText = this.text15.Text;
                XmlAttribute elemLoad16 = xml.CreateAttribute("attribute1");
                elemLoad16.InnerText = this.attribute1.Text;
                XmlAttribute elemLoad17 = xml.CreateAttribute("attribute2");
                elemLoad17.InnerText = this.attribute2.Text;
                XmlAttribute elemLoad18 = xml.CreateAttribute("attribute3");
                elemLoad18.InnerText = this.attribute3.Text;
                XmlAttribute elemLoad19 = xml.CreateAttribute("attribute4");
                elemLoad19.InnerText = this.attribute4.Text;
                XmlAttribute elemLoad20 = xml.CreateAttribute("attribute5");
                elemLoad20.InnerText = this.attribute5.Text;
                XmlAttribute elemLoad21 = xml.CreateAttribute("attribute6");
                elemLoad21.InnerText = this.attribute6.Text;
                XmlAttribute elemLoad22 = xml.CreateAttribute("attribute7");
                elemLoad22.InnerText = this.attribute7.Text;
                XmlAttribute elemLoad23 = xml.CreateAttribute("attribute8");
                elemLoad23.InnerText = this.attribute8.Text;
                XmlAttribute elemLoad24 = xml.CreateAttribute("attribute9");
                elemLoad24.InnerText = this.attribute9.Text;
                XmlAttribute elemLoad25 = xml.CreateAttribute("attribute10");
                elemLoad25.InnerText = this.attribute10.Text;
                XmlAttribute elemLoad26 = xml.CreateAttribute("attribute11");
                elemLoad26.InnerText = this.attribute11.Text;
                XmlAttribute elemLoad27 = xml.CreateAttribute("attribute12");
                elemLoad27.InnerText = this.attribute12.Text;
                XmlAttribute elemLoad28 = xml.CreateAttribute("attribute13");
                elemLoad28.InnerText = this.attribute13.Text;
                XmlAttribute elemLoad29 = xml.CreateAttribute("attribute14");
                elemLoad29.InnerText = this.attribute14.Text;
                XmlAttribute elemLoad30 = xml.CreateAttribute("attribute15");
                elemLoad30.InnerText = this.attribute15.Text;
                XmlAttribute elemLoad31 = xml.CreateAttribute("productModel");
                elemLoad31.InnerText = this.textBox3.Text;
                XmlAttribute elemLoad33 = xml.CreateAttribute("picture");
                elemLoad33.InnerText = this.pictureBox1.AccessibleName.ToString();
                XmlAttribute elemLoad32 = xml.CreateAttribute("remarks");
                elemLoad32.InnerText = this.textBox2.Text;

                node.Attributes.Append(elemLoad1);
                node.Attributes.Append(elemLoad2);
                node.Attributes.Append(elemLoad3);
                node.Attributes.Append(elemLoad4);
                node.Attributes.Append(elemLoad5);
                node.Attributes.Append(elemLoad6);
                node.Attributes.Append(elemLoad7);
                node.Attributes.Append(elemLoad8);
                node.Attributes.Append(elemLoad9);
                node.Attributes.Append(elemLoad10);
                node.Attributes.Append(elemLoad11);
                node.Attributes.Append(elemLoad12);
                node.Attributes.Append(elemLoad13);
                node.Attributes.Append(elemLoad14);
                node.Attributes.Append(elemLoad15);
                node.Attributes.Append(elemLoad16);
                node.Attributes.Append(elemLoad17);
                node.Attributes.Append(elemLoad18);
                node.Attributes.Append(elemLoad19);
                node.Attributes.Append(elemLoad20);
                node.Attributes.Append(elemLoad21);
                node.Attributes.Append(elemLoad22);
                node.Attributes.Append(elemLoad23);
                node.Attributes.Append(elemLoad24);
                node.Attributes.Append(elemLoad25);
                node.Attributes.Append(elemLoad26);
                node.Attributes.Append(elemLoad27);
                node.Attributes.Append(elemLoad28);
                node.Attributes.Append(elemLoad29);
                node.Attributes.Append(elemLoad30);
                node.Attributes.Append(elemLoad31);
                node.Attributes.Append(elemLoad32);
                node.Attributes.Append(elemLoad33);

                root.AppendChild(node);
            }
            xml.Save("C:\\xmlFile\\" + this.strUserName + "\\OtherProducts.xml");
            MessageBox.Show("保存成功", "提示");
            Boolean openFlg = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form f in fc)
            {
                if ("ProductClassification" == f.Name)
                {
                    openFlg = true;
                    break;
                }
            }
            if (!openFlg)
            {
                ProductClassification productClassification = new ProductClassification();
                productClassification.setUserName(this.strUserName);
                productClassification.Show();
            }
            List<ListItem> items = new List<ListItem>();//添加项的集合
            ListItem listItem = new ListItem("", "");
            items.Add(listItem);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                listItem = new ListItem(xmlNode.Attributes.GetNamedItem("productModel").Value, xmlNode.Attributes.GetNamedItem("productModel").Value);
                items.Add(listItem);
            }
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Id";
            this.comboBox1.DataSource = items;
            this.comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.comboBox1.Text))
            {
                //找到主节点
                XmlNode root = xml.SelectSingleNode("Tables");

                XmlNodeList xmlNodeList = root.ChildNodes;
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    if (string.Equals(this.comboBox1.Text, xmlNode.Attributes.GetNamedItem("productModel").Value))
                    {
                        this.text1.Text = xmlNode.Attributes.GetNamedItem("text1").Value;
                        this.text2.Text = xmlNode.Attributes.GetNamedItem("text2").Value;
                        this.text3.Text = xmlNode.Attributes.GetNamedItem("text3").Value;
                        this.text4.Text = xmlNode.Attributes.GetNamedItem("text4").Value;
                        this.text5.Text = xmlNode.Attributes.GetNamedItem("text5").Value;
                        this.text6.Text = xmlNode.Attributes.GetNamedItem("text6").Value;
                        this.text7.Text = xmlNode.Attributes.GetNamedItem("text7").Value;
                        this.text8.Text = xmlNode.Attributes.GetNamedItem("text8").Value;
                        this.text9.Text = xmlNode.Attributes.GetNamedItem("text9").Value;
                        this.text10.Text = xmlNode.Attributes.GetNamedItem("text10").Value;
                        this.text11.Text = xmlNode.Attributes.GetNamedItem("text11").Value;
                        this.text12.Text = xmlNode.Attributes.GetNamedItem("text12").Value;
                        this.text13.Text = xmlNode.Attributes.GetNamedItem("text13").Value;
                        this.text14.Text = xmlNode.Attributes.GetNamedItem("text14").Value;
                        this.text15.Text = xmlNode.Attributes.GetNamedItem("text15").Value;
                        this.attribute1.Text = xmlNode.Attributes.GetNamedItem("attribute1").Value;
                        this.attribute2.Text = xmlNode.Attributes.GetNamedItem("attribute2").Value;
                        this.attribute3.Text = xmlNode.Attributes.GetNamedItem("attribute3").Value;
                        this.attribute4.Text = xmlNode.Attributes.GetNamedItem("attribute4").Value;
                        this.attribute5.Text = xmlNode.Attributes.GetNamedItem("attribute5").Value;
                        this.attribute6.Text = xmlNode.Attributes.GetNamedItem("attribute6").Value;
                        this.attribute7.Text = xmlNode.Attributes.GetNamedItem("attribute7").Value;
                        this.attribute8.Text = xmlNode.Attributes.GetNamedItem("attribute8").Value;
                        this.attribute9.Text = xmlNode.Attributes.GetNamedItem("attribute9").Value;
                        this.attribute10.Text = xmlNode.Attributes.GetNamedItem("attribute10").Value;
                        this.attribute11.Text = xmlNode.Attributes.GetNamedItem("attribute11").Value;
                        this.attribute12.Text = xmlNode.Attributes.GetNamedItem("attribute12").Value;
                        this.attribute13.Text = xmlNode.Attributes.GetNamedItem("attribute13").Value;
                        this.attribute14.Text = xmlNode.Attributes.GetNamedItem("attribute14").Value;
                        this.attribute15.Text = xmlNode.Attributes.GetNamedItem("attribute15").Value;
                        this.textBox3.Text = xmlNode.Attributes.GetNamedItem("productModel").Value;
                        if (!string.IsNullOrEmpty(xmlNode.Attributes.GetNamedItem("picture").Value))
                        {
                            if (File.Exists("C:\\xmlFile\\" + this.strUserName + "\\picture\\" + xmlNode.Attributes.GetNamedItem("picture").Value))
                            {
                                this.pictureBox1.Image = Image.FromFile("C:\\xmlFile\\" + this.strUserName + "\\picture\\" + xmlNode.Attributes.GetNamedItem("picture").Value);
                                this.pictureBox1.AccessibleName = xmlNode.Attributes.GetNamedItem("picture").Value;
                            }
                            else
                            {
                                this.pictureBox1.Image = null;
                                this.pictureBox1.AccessibleName = "";
                            }
                        }
                        else
                        {
                            this.pictureBox1.Image = null;
                            this.pictureBox1.AccessibleName = "";
                        }
                        this.textBox2.Text = xmlNode.Attributes.GetNamedItem("remarks").Value;
                        break;
                    }
                }
            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (null == this.pictureBox1.Image)
            {
                return;
            }

            if (PictureBoxSizeMode.StretchImage != this.pictureBox1.SizeMode)
            {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pictureBox1.Location = new Point(x3, y3);

            }
            else
            {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                x1 = MousePosition.X;
                y1 = MousePosition.Y;
                x2 = this.pictureBox1.Location.X;
                y2 = this.pictureBox1.Location.Y;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (PictureBoxSizeMode.StretchImage != this.pictureBox1.SizeMode)
            {
                x1 = MousePosition.X;
                y1 = MousePosition.Y;
                x2 = this.pictureBox1.Location.X;
                y2 = this.pictureBox1.Location.Y;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (PictureBoxSizeMode.StretchImage != this.pictureBox1.SizeMode && x1 != 0)
            {
                int dx = MousePosition.X - x1;
                int dy = MousePosition.Y - y1;
                this.pictureBox1.Location = new Point(x2 + dx, y2 + dy);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (PictureBoxSizeMode.StretchImage != this.pictureBox1.SizeMode)
            {
                int dx = MousePosition.X - x1;
                int dy = MousePosition.Y - y1;
                this.pictureBox1.Location = new Point(x2 + dx, y2 + dy);
                x1 = 0;
                y1 = 0;
                x2 = 0;
                y2 = 0; ;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "jpg";
            ofd.Filter = "Jpg文件(*.jpg)|*.jpg|Bmp文件(*.bmp)|*.bmp|Gif文件(*.gif)|*.gif";
            ofd.Title = "选择背景图片";
            ofd.Multiselect = false;
            ofd.ShowDialog();
            if (ofd.FileName.Trim() != string.Empty)
            {
                try
                {
                    string strPath;
                    this.pictureBox1.Image = Image.FromFile(ofd.FileName.Trim());
                    string sourceFileName = ofd.FileName.Trim();
                    if (string.IsNullOrEmpty(this.pictureBox1.AccessibleName))
                    {
                        System.DateTime currentTime = new System.DateTime();
                        currentTime = System.DateTime.Now;
                        string strTime = currentTime.ToString("yyyyMMddHHmmss");
                        strPath = "C:\\xmlFile\\" + this.strUserName + "\\picture\\" + strTime + Path.GetExtension(sourceFileName);
                    }
                    else
                    {
                        strPath = "C:\\xmlFile\\" + this.strUserName + "\\picture\\" + this.pictureBox1.AccessibleName;
                    }
                    File.Copy(sourceFileName, strPath, true);
                    this.pictureBox1.AccessibleName = Path.GetFileName(strPath);
                }
                catch
                {
                    MessageBox.Show("装入图像文件失败，请检查文件格式是否正确或文件是否已经损坏。", "程序出错",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}