using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Project2
{
    public partial class Form4 : Form
    {
        private String dbFileName = "sqlite.db";
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        public DataGridView dataGridView1;
        public Form parentform;

        public Form4(DataGridView dataGridView_main)
        {
            InitializeComponent();
           // parentform = parent;
            dataGridView1 = dataGridView_main;


        }
        private void Form4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Manager");
            comboBox1.Items.Add("Salesman");
            comboBox1.Items.Add("Employee");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_sqlCmd = new SQLiteCommand();
            if (m_dbConn.State == ConnectionState.Closed)
            {
                m_dbConn.Open();
            }

            m_sqlCmd.Connection = m_dbConn;
            m_sqlCmd.CommandText= "INSERT INTO worker(name, date_nachalo, group_worker , rate)"+
                                  "VALUES(@name, @date_nachalo, @group_worker, @rate);";
            m_sqlCmd.Parameters.AddWithValue("@name", textBox1.Text);
            m_sqlCmd.Parameters.AddWithValue("@date_nachalo", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            m_sqlCmd.Parameters.AddWithValue("@group_worker", comboBox1.Text);
            m_sqlCmd.Parameters.AddWithValue("@rate", textBox2.Text);
            m_sqlCmd.ExecuteNonQuery();
            MessageBox.Show("Сотрудник добавлен");

            Insert_DGV.Add_worker(dataGridView1, m_dbConn);

            this.Close();



        }

        
    }
}
