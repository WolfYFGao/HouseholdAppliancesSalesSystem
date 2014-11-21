using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace HouseholdAppliancesSalesSystem
{
    public partial class StovesSalesData : Form
    {
        private string strUserName;
        static string xmlPath;
        static XmlDocument xml;
        static int maxID;
        static int count;
        static Dictionary<string, string> insertMap = new Dictionary<string, string>();
        static Dictionary<string, string> updateMap = new Dictionary<string, string>();
        static Dictionary<string, string> deleteMap = new Dictionary<string, string>();

        public StovesSalesData()
        {
            InitializeComponent();
        }

        public void setUserName(string strUserName)
        {
            this.strUserName = strUserName;
        }

        private void StovesSalesData_Load(object sender, EventArgs e)
        {
            xmlPath = "C:\\xmlFile\\" + this.strUserName + "\\StovesSalesData.xml";

            //初始化 XML类 
            xml = new XmlDocument();
            //找到路径
            xml.Load(xmlPath);
            //找到主节点
            XmlNode root = xml.SelectSingleNode("Tables");

            DataTable dt = new DataTable("Record"); //建立一张叫做fcstXML的表
            //创建列
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("姓名", typeof(string)));
            dt.Columns.Add(new DataColumn("电话", typeof(string)));
            dt.Columns.Add(new DataColumn("销售价格", typeof(string)));
            dt.Columns.Add(new DataColumn("安装日期", typeof(string)));
            dt.Columns.Add(new DataColumn("产品型号", typeof(string)));
            dt.Columns.Add(new DataColumn("产品名称", typeof(string)));
            dt.Columns.Add(new DataColumn("件数", typeof(string)));
            dt.Columns.Add(new DataColumn("代理商", typeof(string)));
            dt.Columns.Add(new DataColumn("地址", typeof(string)));
            dt.Columns.Add(new DataColumn("备注", typeof(string)));
            maxID = 0;
            int i = 0;
            XmlNodeList xmlNodeList = root.ChildNodes;
            int m = xmlNodeList.Count;
            count = 0;
            for (int j = m - 1; j >= 0; j--)
            {
                XmlNode item = xmlNodeList[j];
                try
                {
                    i = Convert.ToInt32(item.Attributes.GetNamedItem("id").Value);
                }
                catch (Exception)
                {
                    i = 0;
                }

                if (maxID < i)
                {
                    maxID = i;
                }
                this.CreatRecord(item, dt);

                if (!string.IsNullOrEmpty(item.Attributes.GetNamedItem("number").Value))
                {
                    count = count + Convert.ToInt32(item.Attributes.GetNamedItem("number").Value);
                }
            }
            this.textBox5.Text = count.ToString();

            DataSet xmlDs = new DataSet();

            dt.DefaultView.Sort = "id DESC";
            xmlDs.Tables.Add(dt.DefaultView.ToTable());
            this.GridView1.DataSource = xmlDs.Tables[0];
            this.GridView1.Columns[0].Visible = false;

            DataTable xmlDab = xmlDs.Tables[0];
            string strItem1 = "";
            string strItem2 = "";
            string strItem3 = "";
            string strItem4 = "";
            this.comboBox1.Items.Add("");
            this.comboBox2.Items.Add("");
            this.comboBox3.Items.Add("");
            this.comboBox4.Items.Add("");
            for (int j = 0; j < xmlDab.Rows.Count; j++)
            {
                strItem1 = xmlDab.Rows[j][1].ToString();
                strItem2 = xmlDab.Rows[j][5].ToString();
                strItem3 = xmlDab.Rows[j][2].ToString();
                strItem4 = xmlDab.Rows[j][9].ToString();
                //去除重複項目
                if (!this.comboBox1.Items.Contains(strItem1))
                {
                    this.comboBox1.Items.Add(strItem1);
                }
                if (!this.comboBox2.Items.Contains(strItem2))
                {
                    this.comboBox2.Items.Add(strItem2);
                }
                if (!this.comboBox3.Items.Contains(strItem3))
                {
                    this.comboBox3.Items.Add(strItem3);
                }
                if (!this.comboBox4.Items.Contains(strItem4))
                {
                    this.comboBox4.Items.Add(strItem4);
                }
            }

            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = " ";

            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = " ";
            insertMap = new Dictionary<string, string>();
            updateMap = new Dictionary<string, string>();
            deleteMap = new Dictionary<string, string>();
        }

        private void CreatRecord(XmlNode item, DataTable dt)
        {
            //创建一个新行
            DataRow row = dt.NewRow();
            //读取结点数据，并填充数据行
            row["id"] = item.Attributes.GetNamedItem("id").Value;
            row["姓名"] = item.Attributes.GetNamedItem("name").Value;
            row["电话"] = item.Attributes.GetNamedItem("telphone").Value;
            row["销售价格"] = item.Attributes.GetNamedItem("sellingPrice").Value;
            row["安装日期"] = item.Attributes.GetNamedItem("installationDate").Value;
            row["产品型号"] = item.Attributes.GetNamedItem("productModel").Value;
            row["产品名称"] = item.Attributes.GetNamedItem("ProductName").Value;
            row["件数"] = item.Attributes.GetNamedItem("number").Value;
            row["代理商"] = item.Attributes.GetNamedItem("agent").Value;
            row["地址"] = item.Attributes.GetNamedItem("adress").Value;
            row["备注"] = item.Attributes.GetNamedItem("remarks").Value;
            dt.Rows.Add(row);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.comboBox2.Text))
            {
                MessageBox.Show("请选择产品型号！", "提示");
                return;
            }
            if (this.checkforInterface("Stoves"))
            {
                Stoves stoves = new Stoves();
                stoves.setProductModel(this.comboBox2.Text);
                stoves.setUserName(this.strUserName);
                stoves.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //找到主节点
            XmlNode root = xml.SelectSingleNode("Tables");
            XmlNodeList xmlNodeList;
            foreach (string key in deleteMap.Keys)
            {
                if (!String.IsNullOrEmpty(key))
                {
                    xmlNodeList = root.ChildNodes;
                    int i = xmlNodeList.Count;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        XmlNode xmlNode = xmlNodeList[j];
                        if (String.Equals(key, xmlNode.Attributes.GetNamedItem("id").Value))
                        {
                            root.RemoveChild(xmlNode);
                            break;
                        }
                    }
                }
            }
            deleteMap.Clear();

            foreach (string key in updateMap.Keys)
            {
                if (string.IsNullOrEmpty(updateMap[key]))
                {
                    continue;
                }
                xmlNodeList = root.ChildNodes;
                int m = xmlNodeList.Count;
                int n = 0;
                int i = this.GridView1.Rows.Count;

                for (int j = 0; j < i; j++)
                {
                    if (String.Equals(key, this.GridView1[0, j].Value.ToString()))
                    {
                        if (string.IsNullOrEmpty(this.GridView1[1, j].Value.ToString()) ||
                            string.IsNullOrEmpty(this.GridView1[2, j].Value.ToString()) ||
                            string.IsNullOrEmpty(this.GridView1[5, j].Value.ToString()) ||
                            string.IsNullOrEmpty(this.GridView1[7, j].Value.ToString()) ||
                            string.IsNullOrEmpty(this.GridView1[9, j].Value.ToString()))
                        {
                            MessageBox.Show("“姓名”、“电话”、“产品型号”、“件数”、“地址”这些项目不能为空！", "提示");
                            return;
                        }
                        n = j;
                        break;
                    }
                }

                for (int j = m - 1; j >= 0; j--)
                {
                    XmlNode xmlNode = xmlNodeList[j];
                    if (String.Equals(key, xmlNode.Attributes.GetNamedItem("id").Value))
                    {
                        xmlNode.Attributes.GetNamedItem("name").InnerText = this.GridView1[1, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("telphone").InnerText = this.GridView1[2, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("sellingPrice").InnerText = this.GridView1[3, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("installationDate").InnerText = this.GridView1[4, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("productModel").InnerText = this.GridView1[5, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("ProductName").InnerText = this.GridView1[6, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("number").InnerText = this.GridView1[7, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("agent").InnerText = this.GridView1[8, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("adress").InnerText = this.GridView1[9, n].Value.ToString();
                        xmlNode.Attributes.GetNamedItem("remarks").InnerText = this.GridView1[10, n].Value.ToString();
                        break;
                    }
                }
            }
            updateMap.Clear();

            foreach (string key in insertMap.Keys)
            {
                if (string.IsNullOrEmpty(insertMap[key]))
                {
                    continue;
                }
                int i = this.GridView1.Rows.Count;
                for (int j = 0; j < i; j++)
                {
                    if (String.Equals(key, this.GridView1[0, j].Value.ToString()))
                    {
                        if (string.IsNullOrEmpty(this.GridView1[1, j].Value.ToString()) ||
                           string.IsNullOrEmpty(this.GridView1[2, j].Value.ToString()) ||
                           string.IsNullOrEmpty(this.GridView1[5, j].Value.ToString()) ||
                           string.IsNullOrEmpty(this.GridView1[7, j].Value.ToString()) ||
                           string.IsNullOrEmpty(this.GridView1[9, j].Value.ToString()))
                        {
                            MessageBox.Show("“姓名”、“电话”、“产品型号”、“件数”、“地址”这些项目不能为空！", "提示");
                            return;
                        }
                        break;
                    }
                }
            }

            foreach (string key in insertMap.Keys)
            {
                if (string.IsNullOrEmpty(insertMap[key]))
                {
                    continue;
                }
                int i = this.GridView1.Rows.Count;
                for (int j = 0; j < i; j++)
                {
                    if (String.Equals(key, this.GridView1[0, j].Value.ToString()))
                    {
                        XmlNode node = xml.CreateElement("Record");
                        XmlAttribute elemLoad1 = xml.CreateAttribute("id");
                        elemLoad1.InnerText = this.GridView1[0, j].Value.ToString();
                        XmlAttribute elemLoad2 = xml.CreateAttribute("name");
                        elemLoad2.InnerText = this.GridView1[1, j].Value.ToString();
                        XmlAttribute elemLoad3 = xml.CreateAttribute("telphone");
                        elemLoad3.InnerText = this.GridView1[2, j].Value.ToString();
                        XmlAttribute elemLoad4 = xml.CreateAttribute("sellingPrice");
                        elemLoad4.InnerText = this.GridView1[3, j].Value.ToString();
                        XmlAttribute elemLoad5 = xml.CreateAttribute("installationDate");
                        elemLoad5.InnerText = this.GridView1[4, j].Value.ToString();
                        XmlAttribute elemLoad6 = xml.CreateAttribute("productModel");
                        elemLoad6.InnerText = this.GridView1[5, j].Value.ToString();
                        XmlAttribute elemLoad7 = xml.CreateAttribute("ProductName");
                        elemLoad7.InnerText = this.GridView1[6, j].Value.ToString();
                        XmlAttribute elemLoad8 = xml.CreateAttribute("number");
                        elemLoad8.InnerText = this.GridView1[7, j].Value.ToString();
                        XmlAttribute elemLoad9 = xml.CreateAttribute("agent");
                        elemLoad9.InnerText = this.GridView1[8, j].Value.ToString();
                        XmlAttribute elemLoad10 = xml.CreateAttribute("adress");
                        elemLoad10.InnerText = this.GridView1[9, j].Value.ToString();
                        XmlAttribute elemLoad11 = xml.CreateAttribute("remarks");
                        elemLoad11.InnerText = this.GridView1[10, j].Value.ToString();

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
                        root.AppendChild(node);
                        break;
                    }
                }
            }
            insertMap.Clear();
            xml.Save("C:\\xmlFile\\" + this.strUserName + "\\StovesSalesData.xml");
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

            DataTable xmlDab = (DataTable)this.GridView1.DataSource;
            string strItem1 = "";
            string strItem2 = "";
            string strItem3 = "";
            string strItem4 = "";
            for (int j = 0; j < xmlDab.Rows.Count; j++)
            {
                strItem1 = xmlDab.Rows[j][1].ToString();
                strItem2 = xmlDab.Rows[j][5].ToString();
                strItem3 = xmlDab.Rows[j][2].ToString();
                strItem4 = xmlDab.Rows[j][9].ToString();
                //去除重複項目
                if (!this.comboBox1.Items.Contains(strItem1))
                {
                    this.comboBox1.Items.Add(strItem1);
                }
                if (!this.comboBox2.Items.Contains(strItem2))
                {
                    this.comboBox2.Items.Add(strItem2);
                }
                if (!this.comboBox3.Items.Contains(strItem3))
                {
                    this.comboBox3.Items.Add(strItem3);
                }
                if (!this.comboBox4.Items.Contains(strItem4))
                {
                    this.comboBox4.Items.Add(strItem4);
                }
            }
        }

        private void StovesSalesData_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            if (1 == fc.Count)
            {
                Application.Exit();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //找到主节点
            XmlNode root = xml.SelectSingleNode("Tables");

            DataTable dt = new DataTable("Record"); //建立一张叫做fcstXML的表
            //创建列
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("姓名", typeof(string)));
            dt.Columns.Add(new DataColumn("电话", typeof(string)));
            dt.Columns.Add(new DataColumn("销售价格", typeof(string)));
            dt.Columns.Add(new DataColumn("安装日期", typeof(string)));
            dt.Columns.Add(new DataColumn("产品型号", typeof(string)));
            dt.Columns.Add(new DataColumn("产品名称", typeof(string)));
            dt.Columns.Add(new DataColumn("件数", typeof(string)));
            dt.Columns.Add(new DataColumn("代理商", typeof(string)));
            dt.Columns.Add(new DataColumn("地址", typeof(string)));
            dt.Columns.Add(new DataColumn("备注", typeof(string)));
            XmlNodeList xmlNodeList = root.ChildNodes;
            int m = xmlNodeList.Count;
            for (int j = m - 1; j >= 0; j--)
            {
                XmlNode item = xmlNodeList[j];
                if ((string.IsNullOrEmpty(this.comboBox1.Text) ||
                    string.Equals(this.comboBox1.Text, item.Attributes.GetNamedItem("name").Value))
                    && (string.IsNullOrEmpty(this.comboBox2.Text) ||
                    string.Equals(this.comboBox2.Text, item.Attributes.GetNamedItem("productModel").Value))
                    && (string.IsNullOrEmpty(this.comboBox3.Text) ||
                    string.Equals(this.comboBox3.Text, item.Attributes.GetNamedItem("telphone").Value))
                    && (string.IsNullOrEmpty(this.comboBox4.Text) ||
                    string.Equals(this.comboBox4.Text, item.Attributes.GetNamedItem("adress").Value))
                    && (string.IsNullOrEmpty(this.dateTimePicker1.Text.Trim()) ||
                    0 >= string.Compare(this.dateTimePicker1.Text.Trim(), item.Attributes.GetNamedItem("installationDate").Value))
                    && (string.IsNullOrEmpty(this.dateTimePicker2.Text.Trim()) ||
                    0 <= string.Compare(this.dateTimePicker2.Text.Trim(), item.Attributes.GetNamedItem("installationDate").Value)))
                {
                    this.CreatRecord(item, dt);
                }
            }

            DataSet xmlDs = new DataSet();

            dt.DefaultView.Sort = "id DESC";
            xmlDs.Tables.Add(dt.DefaultView.ToTable());
            this.GridView1.DataSource = xmlDs.Tables[0];
            this.GridView1.Columns[0].Visible = false;
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

        private void GridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string value = this.GridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (e.ColumnIndex == 4)
                {
                    if (8 == value.Length)
                    {
                        value = value.Substring(0, 4) + "-" + value.Substring(4, 2) + "-" + value.Substring(6, 2);
                        this.GridView1[e.ColumnIndex, e.RowIndex].Value = value;
                    }
                    else if (10 == value.Length)
                    {
                        value = value.Substring(0, 4) + "-" + value.Substring(5, 2) + "-" + value.Substring(8, 2);
                        this.GridView1[e.ColumnIndex, e.RowIndex].Value = value;
                    }
                    else
                    {
                        MessageBox.Show("“安装日期”的日期格式输入不正确", "提示");
                    }
                }
                string strID = this.GridView1[0, e.RowIndex].Value.ToString();
                if (String.IsNullOrEmpty(strID))
                {
                    insertMap.Add((++maxID).ToString(), value);
                    this.GridView1[0, e.RowIndex].Value = maxID.ToString();
                }
                else
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        if (insertMap.ContainsKey(strID))
                        {
                            insertMap[strID] = this.GridView1[1, e.RowIndex].Value.ToString() +
                                   this.GridView1[2, e.RowIndex].Value.ToString() +
                                   this.GridView1[3, e.RowIndex].Value.ToString() +
                                   this.GridView1[4, e.RowIndex].Value.ToString() +
                                   this.GridView1[5, e.RowIndex].Value.ToString() +
                                   this.GridView1[6, e.RowIndex].Value.ToString() +
                                   this.GridView1[7, e.RowIndex].Value.ToString() +
                                   this.GridView1[8, e.RowIndex].Value.ToString() +
                                   this.GridView1[9, e.RowIndex].Value.ToString();
                        }
                        else
                        {
                            if (updateMap.ContainsKey(strID))
                            {
                                updateMap[strID] = this.GridView1[1, e.RowIndex].Value.ToString() +
                                    this.GridView1[2, e.RowIndex].Value.ToString() +
                                    this.GridView1[3, e.RowIndex].Value.ToString() +
                                    this.GridView1[4, e.RowIndex].Value.ToString() +
                                    this.GridView1[5, e.RowIndex].Value.ToString() +
                                    this.GridView1[6, e.RowIndex].Value.ToString() +
                                    this.GridView1[7, e.RowIndex].Value.ToString() +
                                    this.GridView1[8, e.RowIndex].Value.ToString() +
                                    this.GridView1[9, e.RowIndex].Value.ToString();
                            }
                            else
                            {
                                updateMap.Add(strID, this.GridView1[1, e.RowIndex].Value.ToString() +
                                   this.GridView1[2, e.RowIndex].Value.ToString() +
                                   this.GridView1[3, e.RowIndex].Value.ToString() +
                                   this.GridView1[4, e.RowIndex].Value.ToString() +
                                   this.GridView1[5, e.RowIndex].Value.ToString() +
                                   this.GridView1[6, e.RowIndex].Value.ToString() +
                                   this.GridView1[7, e.RowIndex].Value.ToString() +
                                   this.GridView1[8, e.RowIndex].Value.ToString() +
                                   this.GridView1[9, e.RowIndex].Value.ToString());
                            }
                        }
                    }
                    else
                    {
                        if (insertMap.ContainsKey(strID))
                        {
                            insertMap[strID] = value;
                        }
                        else
                        {
                            if (updateMap.ContainsKey(strID))
                            {
                                updateMap[strID] = value;
                            }
                            else
                            {
                                updateMap.Add(strID, value);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("系统错误！", "错误");
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<int, string> dictionary = new Dictionary<int, string>();
                int i = this.GridView1.SelectedCells.Count;
                if (MessageBox.Show("确定要删除选中的信息吗？删除后将不可恢复！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int m = 0; m < i; m++)
                    {
                        if (!dictionary.ContainsKey(this.GridView1.SelectedCells[m].RowIndex))
                        {
                            dictionary.Add(this.GridView1.SelectedCells[m].RowIndex, "");
                        }
                    }
                    foreach (KeyValuePair<int, string> a in dictionary)
                    {
                        string strID = this.GridView1[0, a.Key].Value.ToString();
                        deleteMap.Add(strID, "");
                        this.GridView1.Rows.RemoveAt(a.Key);
                        if (insertMap.ContainsKey(strID))
                        {
                            insertMap.Remove(strID);
                        }
                        if (updateMap.ContainsKey(strID))
                        {
                            updateMap.Remove(strID);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("系统错误！", "错误");
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 < this.GridView1.SelectedCells.Count)
            {
                DataTable dataTable = (DataTable)this.GridView1.DataSource;
                DataRow row = dataTable.NewRow();
                dataTable.Rows.InsertAt(row, this.GridView1.SelectedCells[0].RowIndex + 1);
            }
        }
    }
}
