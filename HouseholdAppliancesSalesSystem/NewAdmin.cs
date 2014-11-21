using System;
using System.Windows.Forms;
using System.Xml;
using System.IO;


namespace HouseholdAppliancesSalesSystem
{
    public partial class NewAdmin : Form
    {
        static string xmlPath;
        static XmlDocument xml;

        public NewAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox3.Text) || string.IsNullOrEmpty(this.textBox2.Text) || string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("请先输入账号和密码！", "提示");
                return;
            }
            if (!Directory.Exists("C:\\xmlFile"))//如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory("C:\\xmlFile");
            }

            if (File.Exists("C:\\xmlFile\\Seller.xml"))
            {
                xmlPath = "C:\\xmlFile\\Seller.xml";
                //初始化 XML类 
                xml = new XmlDocument();
                //找到路径
                xml.Load(xmlPath);
                //找到主节点
                XmlNode root = xml.SelectSingleNode("Tables");
                XmlNodeList xmlNodeList = root.ChildNodes;
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    if (string.Equals(this.textBox1.Text, xmlNode.Attributes.GetNamedItem("username").Value))
                    {
                        MessageBox.Show("账号已经存在！", "提示");
                        return;
                    }
                }
            }

            if (!string.Equals(this.textBox2.Text, this.textBox3.Text))
            {
                MessageBox.Show("两次输入的密码不一致！", "提示");
                return;
            }
            if (!File.Exists("C:\\xmlFile\\Seller.xml"))
            {
                //初始化 XML类 
                xml = new XmlDocument();
                XmlDeclaration xmlDecl;
                xmlDecl = xml.CreateXmlDeclaration("1.0", "gb2312", null);
                xml.AppendChild(xmlDecl);

                XmlElement xmlEle = xml.CreateElement("", "Tables", "");
                xml.AppendChild(xmlEle);

                //找到主节点
                XmlNode root = xml.SelectSingleNode("Tables");

                XmlElement xmlElement = xml.CreateElement("Record");

                XmlAttribute elemLoad1 = xml.CreateAttribute("username");
                elemLoad1.InnerText = this.textBox1.Text;
                XmlAttribute elemLoad2 = xml.CreateAttribute("password");
                elemLoad2.InnerText = this.textBox2.Text;

                xmlElement.Attributes.Append(elemLoad1);
                xmlElement.Attributes.Append(elemLoad2);

                root.AppendChild(xmlElement);
                xml.Save("C:\\xmlFile\\Seller.xml");
            }
            else
            {
                xml = new XmlDocument();
                xml.Load("C:\\xmlFile\\Seller.xml");
                //找到主节点
                XmlNode root = xml.SelectSingleNode("Tables");
                XmlElement xmlElement = xml.CreateElement("Record");
                XmlAttribute elemLoad1 = xml.CreateAttribute("username");
                elemLoad1.InnerText = this.textBox1.Text;
                XmlAttribute elemLoad2 = xml.CreateAttribute("password");
                elemLoad2.InnerText = this.textBox2.Text;

                xmlElement.Attributes.Append(elemLoad1);
                xmlElement.Attributes.Append(elemLoad2);

                root.AppendChild(xmlElement);
                xml.Save("C:\\xmlFile\\Seller.xml");
            }

            string filePath = "C:\\xmlFile\\" + this.textBox1.Text;
            Directory.CreateDirectory(filePath + "\\picture");

            this.NewXML(filePath + "\\SmokeMachineSalesData.xml");
            this.NewXML(filePath + "\\OtherProductsSalesData.xml");
            this.NewXML(filePath + "\\StovesSalesData.xml");
            this.NewXML(filePath + "\\OtherProducts.xml");
            this.NewXML(filePath + "\\SmokeMachine.xml");
            this.NewXML(filePath + "\\Stoves.xml");

            ProductClassification productClassification = new ProductClassification();
            productClassification.setUserName(this.textBox3.Text);
            productClassification.Show();
            this.Close();
        }

        private void NewXML(string loadPath)
        {
            //初始化 XML类 
            xml = new XmlDocument();
            XmlDeclaration xmlDecl;
            xmlDecl = xml.CreateXmlDeclaration("1.0", "gb2312", null);
            xml.AppendChild(xmlDecl);

            XmlElement xmlEle = xml.CreateElement("", "Tables", "");
            xml.AppendChild(xmlEle);

            xml.Save(loadPath);
        }

        private void NewAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            if (1 == fc.Count)
            {
                Application.Exit();
            }
        }
    }
}
