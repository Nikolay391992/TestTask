using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace Project2
{
    public static class Insert_DGV
    {
        public static void Add_worker(DataGridView dataGridView1, SQLiteConnection m_dbConn)//Отображение всех рабочих в DGV1
        {
            try
            {
                DataTable dTable = new DataTable();
                String sqlQuery = "SELECT * FROM Worker";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();

                    for (int i = 0; i < dTable.Rows.Count; i++)
                        dataGridView1.Rows.Add(dTable.Rows[i].ItemArray);
                }
                else
                    MessageBox.Show("База данных пустая!");

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                m_dbConn.Close();
                dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            }
        }
        public static void Add_worker(DataGridView dataGridView2, SQLiteConnection m_dbConn, string Id_work, TextBox textBox4)//Отображение всех подчиненных в DGV2
        {
            try
            {
                DataTable dTable2 = new DataTable();
                String sqlQuery2;
                sqlQuery2 = "WITH RECURSIVE tree(id_subotdinate, level) AS( " +
                                        "SELECT cp.id_subotdinate, 1 " +
                                        "FROM head_subordinate cp " +
                                        "WHERE cp.id_head = ' " + Id_work + "'" +
                                        "UNION ALL " +
                                        "SELECT ch.id_subotdinate AS new_name, tree.level + 1 AS new_lvl " +
                                        "FROM head_subordinate ch " +
                                        "INNER JOIN tree ON ch.id_head = tree.id_subotdinate ORDER BY new_name ASC) " +
                                        "SELECT id_subotdinate,('Подчиненный ' || level || ' уровня') as level2,name,date_nachalo,group_worker,rate FROM tree,worker " +
                                        "where id_subotdinate = id_worker; ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery2, m_dbConn);
                adapter.Fill(dTable2);
                dataGridView2.Rows.Clear();
                if (dTable2.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable2.Rows.Count; i++)
                        dataGridView2.Rows.Add(dTable2.Rows[i].ItemArray);
                }
                else
                {
                    MessageBox.Show("У сотрудника нет подчиненных");
                }

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                textBox4.Text = "";
                dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
                m_dbConn.Close();
            }
        }
    }
}
