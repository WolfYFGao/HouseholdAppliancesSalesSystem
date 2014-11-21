using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HouseholdAppliancesSalesSystem
{
    public partial class ProductClassification : Form
    {

        private string strUserName;
        public ProductClassification()
        {
            InitializeComponent();
        }

        public void setUserName(string strUserName)
        {
            this.strUserName = strUserName;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.checkforInterface("SmokeMachineSalesData"))
            {
                SmokeMachineSalesData smokeMachineSalesData = new SmokeMachineSalesData();
                smokeMachineSalesData.setUserName(this.strUserName);
                smokeMachineSalesData.Show();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.checkforInterface("StovesSalesData"))
            {
                StovesSalesData stovesSalesData = new StovesSalesData();
                stovesSalesData.setUserName(this.strUserName);
                stovesSalesData.Show();
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.checkforInterface("OtherProductsSalesData"))
            {
                OtherProductsSalesData otherProductsSalesData = new OtherProductsSalesData();
                otherProductsSalesData.setUserName(this.strUserName);
                otherProductsSalesData.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.checkforInterface("SmokeMachine"))
            {
                SmokeMachine smokeMachine = new SmokeMachine();
                smokeMachine.setUserName(this.strUserName);
                smokeMachine.Show();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.checkforInterface("Stoves"))
            {
                Stoves stoves = new Stoves();
                stoves.setUserName(this.strUserName);
                stoves.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.checkforInterface("OtherProducts"))
            {
                OtherProducts otherProducts = new OtherProducts();
                otherProducts.setUserName(this.strUserName);
                otherProducts.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private Boolean checkforInterface(string strName)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form f in fc)
            {
                if (strName == f.Name)
                {
                    return false;
                }
            }
            return true;
        }

        private void ProductClassification_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            if (1 == fc.Count)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strTemp = "";
            DriveInfo[] drivers = DriveInfo.GetDrives();
            foreach (DriveInfo info in drivers)
            {
                if (info.DriveType == DriveType.Removable)
                {
                    strTemp = info.Name.ToString();
                    break;
                }
            }
            if (string.IsNullOrEmpty(strTemp))
            {
                MessageBox.Show("请先插入U盘！", "提示");
                return;
            }
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string strTime = currentTime.ToString("yyyyMMddHHmmss");
            string strPath = strTemp + strTime + "\\" + this.strUserName;
          
            if (MessageBox.Show("备份文件将存储在“" + strPath + "”下", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Directory.Exists(strPath + "\\picture") == false)//如果不存在就创建file文件夹 
                {
                    Directory.CreateDirectory(strPath + "\\picture");
                }
                string[] strFiles = Directory.GetFiles("C:\\xmlFile\\" + this.strUserName);
                //循环拷贝文件
                for (int i = 0; i < strFiles.Length; i++)
                {
                    //取得拷贝的文件名，只取文件名，地址截掉。
                    string strFileName = Path.GetFileName(strFiles[i]);
                    //开始拷贝文件,true表示覆盖同名文件
                    File.Copy(strFiles[i], strPath + "\\" + strFileName, true);
                }
                string[] strFiles1 = Directory.GetFiles("C:\\xmlFile\\" + this.strUserName + "\\picture");
                //循环拷贝文件
                for (int i = 0; i < strFiles1.Length; i++)
                {
                    //取得拷贝的文件名，只取文件名，地址截掉。
                    string strFileName = Path.GetFileName(strFiles1[i]);
                    //开始拷贝文件,true表示覆盖同名文件
                    File.Copy(strFiles1[i], strPath + "\\picture\\" + strFileName, true);
                }
            }
        }
    }
}
