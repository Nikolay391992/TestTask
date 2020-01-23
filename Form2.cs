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
    public partial class Form2 : Form
    {
        private String dbFileName = "sqlite.db";
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        private int id_work;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //загрузили список сотрудников
            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_sqlCmd = new SQLiteCommand();

            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;

            Insert_DGV.Add_worker( dataGridView1,m_dbConn);//вставка всех сотрудников в DGV1
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (m_dbConn.State == ConnectionState.Closed)
            {
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;
            }
            string Id_work = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();

            Insert_DGV.Add_worker(dataGridView2, m_dbConn,Id_work,textBox4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Дта окончания не может быть меньше даты начала
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("Дата окончания меньше даты начала ");
                return;
            }
            if (dateTimePicker2.Value > DateTime.Now)
            {
                MessageBox.Show("Дата окончания больше текущей даты ");
                return;
            }
            //алгоритм вычисления з.п. для - Employee
            if (dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString()== "Employee")
            {
                float FinalSalary_Employee = SalaryWorker.CalcEmployeeSalary(dataGridView1, dateTimePicker1, dateTimePicker2);
                textBox4.Text = FinalSalary_Employee.ToString();
                return;

            }
            //алгоритм вычисления з.п. для -  Manager
            if (dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString() == "Manager")
            {
                float FinalSalary = SalaryWorker.CalcManagerSalary(dataGridView1,dataGridView2, dateTimePicker1, dateTimePicker2);
                textBox4.Text = FinalSalary.ToString();
                return;

            }
            //алгоритм вычисления з.п. для - Salesman
            if (dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString() == "Salesman") 
            {
                float FinalSalary = SalaryWorker.CalcSalesmanSalary(dataGridView1, dataGridView2, dateTimePicker1, dateTimePicker2);
                textBox4.Text = FinalSalary.ToString();
                return;
            }

           

            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form Add_worker = new Form4(this.dataGridView1);    
            Add_worker.ShowDialog();
        }
    }
}
