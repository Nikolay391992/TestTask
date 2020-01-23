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
    public partial class Form3 : Form
    {
        private String dbFileName = "sqlite.db";
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        private TextBox textBox_from_main;
        public Form3(TextBox textBox_from_main1)
        {
            InitializeComponent();
            textBox_from_main = textBox_from_main1;
            textBox2.Text = "Список подчиненных сотрудника " + textBox_from_main1.Text;
            button1.Text = "Рассчитать для сотрудника " + textBox_from_main1.Text;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_sqlCmd = new SQLiteCommand();
            if(m_dbConn.State == ConnectionState.Closed)
            { 
                m_dbConn.Open(); 
            }
                            
            m_sqlCmd.Connection = m_dbConn;
            Insert_DGV.Add_worker(dataGridView1, m_dbConn);//вставка всех сотрудников в DGV1
            if (m_dbConn.State == ConnectionState.Closed)
            {
                m_dbConn.Open();
            }

            string sqlExpression = "SELECT id_worker FROM worker where name ='" + textBox_from_main.Text + "'";
            SQLiteCommand command = new SQLiteCommand(sqlExpression, m_dbConn);
            object result = command.ExecuteScalar();

            string Id_work = Convert.ToString(result);
            Insert_DGV.Add_worker(dataGridView2, m_dbConn, Id_work, textBox6);
        }

        
    }
}
