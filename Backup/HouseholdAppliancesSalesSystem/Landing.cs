using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace HouseholdAppliancesSalesSystem
{
    public partial class Landing : Form
    {
        static string xmlPath;
        static XmlDocument xml;
        public Landing()
        {
            InitializeComponent();
        }

        private void Landing_Load(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text) || string.IsNullOrEmpty(this.textBox2.Text))
            {
                MessageBox.Show("账号和密码不能为空！", "提示");
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
                    if (string.Equals(this.textBox1.Text, xmlNode.Attributes.GetNamedItem("username").Value))
                    {
                        userFlg = true;
                    }
                    if (userFlg)
                    {
                        if (string.Equals(this.textBox2.Text, xmlNode.Attributes.GetNamedItem("password").Value))
                        {
                            passwordFlg = true;
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
                MessageBox.Show("密码不正确！", "提示");
                return;
            }

            this.Visible = false;
            ProductClassification productClassification = new ProductClassification();
            productClassification.setUserName(this.textBox1.Text);
            productClassification.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ChangePassword changePassword = new ChangePassword();
            changePassword.setUserName(this.textBox1.Text);
            changePassword.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            NewAdmin newAdmin = new NewAdmin();
            newAdmin.Show();
        }

        private void Landing_Activated(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }
    }
}
