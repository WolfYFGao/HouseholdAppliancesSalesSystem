using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing;

namespace HouseholdAppliancesSalesSystem
{
    public partial class Stoves : Form
    {
        private string strUserName;
        private string strProductModel;
        static string xmlPath;
        static XmlDocument xml;
        int x1 = 0;
        int y1 = 0;
        int x2 = 0;
        int y2 = 0;
        int x3;
        int y3;

        public Stoves()
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

        private void Stoves_Load(object sender, EventArgs e)
        {
            xmlPath = "C:\\xmlFile\\" + this.strUserName + "\\Stoves.xml";
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
                        //备注
                        this.textBox1.Text = xmlNode.Attributes.GetNamedItem("remarks").Value;
                        //零售价
                        this.textBox2.Text = xmlNode.Attributes.GetNamedItem("retailprice").Value;
                        //批发价
                        this.textBox3.Text = xmlNode.Attributes.GetNamedItem("wholesaleprice").Value;
                        //熄保
                        this.textBox4.Text = xmlNode.Attributes.GetNamedItem("extinguishing").Value;
                        //型号
                        this.textBox5.Text = xmlNode.Attributes.GetNamedItem("productModel").Value;
                        //盖材质
                        this.textBox6.Text = xmlNode.Attributes.GetNamedItem("coverMaterial").Value;
                        //面板材质
                        this.textBox7.Text = xmlNode.Attributes.GetNamedItem("panelMaterial").Value;
                        //面板尺寸
                        this.textBox8.Text = xmlNode.Attributes.GetNamedItem("panelSize").Value;
                        //板材厚度
                        this.textBox9.Text = xmlNode.Attributes.GetNamedItem("plateThickness").Value;
                        //开孔尺寸
                        this.textBox10.Text = xmlNode.Attributes.GetNamedItem("holeSize").Value;
                        //探针
                        this.textBox11.Text = xmlNode.Attributes.GetNamedItem("probe").Value;
                        if (!string.IsNullOrEmpty(xmlNode.Attributes.GetNamedItem("picture").Value))
                        {
                            if (File.Exists("C:\\xmlFile\\" + this.strUserName + "\\picture\\" + xmlNode.Attributes.GetNamedItem("picture").Value))
                            {
                                //图片
                                this.pictureBox1.Image = Image.FromFile("C:\\xmlFile\\" + this.strUserName + "\\picture\\" + xmlNode.Attributes.GetNamedItem("picture").Value);
                                this.pictureBox1.AccessibleName = xmlNode.Attributes.GetNamedItem("picture").Value;
                            }
                        }
                        //进价
                        this.textBox13.Text = xmlNode.Attributes.GetNamedItem("purchaseprice").Value;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox5.Text))
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
                if (string.Equals(this.textBox5.Text, xmlNode.Attributes.GetNamedItem("productModel").Value))
                {
                    //备注
                    xmlNode.Attributes.GetNamedItem("remarks").InnerText = this.textBox1.Text;
                    //零售价
                    xmlNode.Attributes.GetNamedItem("retailprice").InnerText = this.textBox2.Text;
                    //批发价
                    xmlNode.Attributes.GetNamedItem("wholesaleprice").InnerText = this.textBox3.Text;
                    //熄保
                    xmlNode.Attributes.GetNamedItem("extinguishing").InnerText = this.textBox4.Text;
                    //型号
                    xmlNode.Attributes.GetNamedItem("productModel").InnerText = this.textBox5.Text;
                    //盖材质
                    xmlNode.Attributes.GetNamedItem("coverMaterial").InnerText = this.textBox6.Text;
                    //面板材质
                    xmlNode.Attributes.GetNamedItem("panelMaterial").InnerText = this.textBox7.Text;
                    //面板尺寸
                    xmlNode.Attributes.GetNamedItem("panelSize").InnerText = this.textBox8.Text;
                    //板材厚度
                    xmlNode.Attributes.GetNamedItem("plateThickness").InnerText = this.textBox9.Text;
                    //开孔尺寸
                    xmlNode.Attributes.GetNamedItem("holeSize").InnerText = this.textBox10.Text;
                    //探针
                    xmlNode.Attributes.GetNamedItem("probe").InnerText = this.textBox11.Text;
                    //图片
                    xmlNode.Attributes.GetNamedItem("picture").InnerText = this.pictureBox1.AccessibleName.ToString();
                    //进价
                    xmlNode.Attributes.GetNamedItem("purchaseprice").InnerText = this.textBox13.Text;
                    insflg = true;
                    break;
                }
            }
            if (!insflg)
            {
                XmlNode node = xml.CreateElement("Record");
                XmlAttribute elemLoad1 = xml.CreateAttribute("productModel");
                elemLoad1.InnerText = this.textBox5.Text;
                XmlAttribute elemLoad2 = xml.CreateAttribute("retailprice");
                elemLoad2.InnerText = this.textBox2.Text;
                XmlAttribute elemLoad3 = xml.CreateAttribute("wholesaleprice");
                elemLoad3.InnerText = this.textBox3.Text;
                XmlAttribute elemLoad4 = xml.CreateAttribute("extinguishing");
                elemLoad4.InnerText = this.textBox4.Text;
                XmlAttribute elemLoad5 = xml.CreateAttribute("coverMaterial");
                elemLoad5.InnerText = this.textBox6.Text;
                XmlAttribute elemLoad6 = xml.CreateAttribute("panelMaterial");
                elemLoad6.InnerText = this.textBox7.Text;
                XmlAttribute elemLoad7 = xml.CreateAttribute("panelSize");
                elemLoad7.InnerText = this.textBox8.Text;
                XmlAttribute elemLoad8 = xml.CreateAttribute("plateThickness");
                elemLoad8.InnerText = this.textBox9.Text;
                XmlAttribute elemLoad9 = xml.CreateAttribute("holeSize");
                elemLoad9.InnerText = this.textBox10.Text;
                XmlAttribute elemLoad10 = xml.CreateAttribute("probe");
                elemLoad10.InnerText = this.textBox11.Text;
                XmlAttribute elemLoad11 = xml.CreateAttribute("purchaseprice");
                elemLoad11.InnerText = this.textBox13.Text;
                XmlAttribute elemLoad12 = xml.CreateAttribute("remarks");
                elemLoad12.InnerText = this.textBox1.Text;
                XmlAttribute elemLoad13 = xml.CreateAttribute("picture");
                elemLoad13.InnerText = this.pictureBox1.AccessibleName.ToString();

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
                root.AppendChild(node);
            }
            xml.Save("C:\\xmlFile\\" + this.strUserName + "\\Stoves.xml");
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
                        //备注
                        this.textBox1.Text = xmlNode.Attributes.GetNamedItem("remarks").Value;
                        //零售价
                        this.textBox2.Text = xmlNode.Attributes.GetNamedItem("retailprice").Value;
                        //批发价d
                        this.textBox3.Text = xmlNode.Attributes.GetNamedItem("wholesaleprice").Value;
                        //熄保
                        this.textBox4.Text = xmlNode.Attributes.GetNamedItem("extinguishing").Value;
                        //型号
                        this.textBox5.Text = xmlNode.Attributes.GetNamedItem("productModel").Value;
                        //盖材质
                        this.textBox6.Text = xmlNode.Attributes.GetNamedItem("coverMaterial").Value;
                        //面板材质
                        this.textBox7.Text = xmlNode.Attributes.GetNamedItem("panelMaterial").Value;
                        //面板尺寸
                        this.textBox8.Text = xmlNode.Attributes.GetNamedItem("panelSize").Value;
                        //板材厚度
                        this.textBox9.Text = xmlNode.Attributes.GetNamedItem("plateThickness").Value;
                        //开孔尺寸
                        this.textBox10.Text = xmlNode.Attributes.GetNamedItem("holeSize").Value;
                        //探针
                        this.textBox11.Text = xmlNode.Attributes.GetNamedItem("probe").Value;
                        if (!string.IsNullOrEmpty(xmlNode.Attributes.GetNamedItem("picture").Value))
                        {
                            if (File.Exists("C:\\xmlFile\\" + this.strUserName + "\\picture\\" + xmlNode.Attributes.GetNamedItem("picture").Value))
                            {
                                //图片
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
                        //进价
                        this.textBox13.Text = xmlNode.Attributes.GetNamedItem("purchaseprice").Value;
                        break;
                    }
                }
            }
        }

        private void Stoves_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            if (1 == fc.Count)
            {
                Application.Exit();
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
                    this.pictureBox1.Image = Image.FromFile(ofd.FileName.Trim());
                    string sourceFileName = ofd.FileName.Trim();
                    System.DateTime currentTime = new System.DateTime();
                    currentTime = System.DateTime.Now;
                    string strTime = currentTime.ToString("yyyyMMddHHmmss");
                    string strPath = "C:\\xmlFile\\" + this.strUserName + "\\picture\\" + strTime + Path.GetExtension(sourceFileName); ;
                    File.Copy(sourceFileName, strPath, true);
                    this.pictureBox1.AccessibleName = strTime + Path.GetExtension(sourceFileName);
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
