using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;


using System.Configuration;
using System.Collections;
using System.Web;
using System.IO; 


namespace HouseholdAppliancesSalesSystem
{
    public partial class ChangePassword : Form
    {
        static string xmlPath;
        static XmlDocument xml;
        private string strUserName;

        public ChangePassword()
        {
            InitializeComponent();
        }

        public void setUserName(string strUserName)
        {
            this.strUserName = strUserName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox3.Text) || string.IsNullOrEmpty(this.textBox2.Text))
            {
                MessageBox.Show("请先输入账号和密码！", "提示");
                return;
            }

            Boolean userFlg = false;
            Boolean passwordFlg = false;

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
                    userFlg = false;
                    passwordFlg = false;
                    if (string.Equals(this.textBox3.Text, xmlNode.Attributes.GetNamedItem("username").Value))
                    {
                        userFlg = true;
                    }
                    if (userFlg)
                    {
                        if (string.Equals(this.textBox2.Text, xmlNode.Attributes.GetNamedItem("password").Value))
                        {
                            passwordFlg = true;
                            xmlNode.Attributes.GetNamedItem("password").InnerText = this.textBox1.Text;
                        }
                        break;
                    }
                }
            }
            if (!userFlg)
            {
                MessageBox.Show("账号不存在！", "提示");
                return;
            }
            if (!passwordFlg)
            {
                MessageBox.Show("当前密码不正确！", "提示");
                return;
            }
            if (!string.Equals(this.textBox1.Text, this.textBox4.Text))
            {
                MessageBox.Show("两次输入的新密码不一致！", "提示");
                return;
            }
            xml.Save("C:\\xmlFile\\Seller.xml");

            ProductClassification productClassification = new ProductClassification();
            productClassification.setUserName(this.textBox3.Text);
            productClassification.Show();
            this.Close();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            this.textBox3.Text = this.strUserName;
        }

        private void ChangePassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            if (1 == fc.Count)
            {
                Application.Exit();
            }
        }

    }
}
