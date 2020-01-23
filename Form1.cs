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
    
    public partial class Form1 : Form
    {
        private String dbFileName = "sqlite.db";
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
       
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)//вход в систему
        {
            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;

            m_sqlCmd.CommandText = "SELECT * FROM user where name= @name and password =@password ";
            m_sqlCmd.Parameters.AddWithValue("@name", textBox1.Text);
            m_sqlCmd.Parameters.AddWithValue("@password", textBox2.Text);

            SQLiteDataReader r = m_sqlCmd.ExecuteReader();
            try
            {    
                string line = String.Empty;
                r.Read();
                if (r.HasRows)
                {

                    line = r["id_user"].ToString() + ", "
                         + r["name"] + ", "
                         + r["password"] + ", "
                         + r["privilege"];
                    if (line.Contains("WriteRead"))
                    {

                        Form form_super = new Form2();//Открывается формы супер-пользователя
                        form_super.Show(); // 
                        this.Hide();
                    }
                    else
                    {

                        Form form_user = new Form3(this.textBox1);
                        form_user.Show(); //Открывается форма пользователя
                        form_user.Text = "Пользователь " + textBox1.Text;
                        this.Hide();
                    }
                }
                else
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    MessageBox.Show("Неверное имя пользователя или пароль");
                }
                

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                r.Close();
                m_dbConn.Close();
            }

        }

       
    }
}
