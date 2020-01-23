using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace Project2
{
    static public class SalaryWorker
    {
        public static float EmployeePercent = 0.03f;
        public static float EmployeeLimit = 0.3f;

        public static float ManagerPercent = 0.05f;
        public static float ManagerLimit = 0.4f;
        public static float ManagerPercent_subordinate = 0.005f;

        public static float SalesmanPercent = 0.01f;
        public static float SalesmanLimit = 0.35f;
        public static float SalesmanPercent_subordinate = 0.003f;

        public static float CalcEmployeeSalary(DataGridView dataGridView1,DateTimePicker dateTimePicker1, DateTimePicker dateTimePicker2)
        {
            try
            {
                //считывание с таблицы даты поступелния на работу
                string date_begin = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                //Преобразование в дату в нужном формате
                DateTime dtdate_begin = DateTime.ParseExact(date_begin, "yyyy-MM-dd", null);

                DateTime dt_start = dateTimePicker1.Value;
                DateTime dt_finish = dateTimePicker2.Value;
                //если дата поступления больше даты начала
                if (dt_start < dtdate_begin)
                {
                    dt_start = dtdate_begin;
                }

                float sum = 0;

                int CountMounth = Math.Abs((dt_finish.Month - dt_start.Month) + 12 * (dt_finish.Year - dt_start.Year));
                int CountDay = Convert.ToInt32((dt_finish - dt_start).TotalDays);
                int CountYear = CountDay / 365;

                float rate = Convert.ToSingle(dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString());
                sum = rate * CountMounth;//вычисление з.п. по ставке

                float bonus = CountYear * rate * 12 * EmployeePercent;//надбавка за каждый год работы

                if (bonus > EmployeeLimit * sum)
                {
                    bonus = EmployeeLimit * sum;
                }

                sum = sum + bonus;//итоговая зарплата

                return sum;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }



        }
        public static float CalcManagerSalary(DataGridView dataGridView1, DataGridView dataGridView2, DateTimePicker dateTimePicker1, DateTimePicker dateTimePicker2)
        {
            try
            { 
            string date_begin = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            DateTime dtdate_begin = DateTime.ParseExact(date_begin, "yyyy-MM-dd", null);//поступление на работу

            DateTime dt_start = dateTimePicker1.Value;
            DateTime dt_finish = dateTimePicker2.Value;
            //если дата поступления больше даты начала
            if (dt_start < dtdate_begin)
            {
                dt_start = dtdate_begin;
            }

            float sum = 0;

            int CountMounth = Math.Abs((dt_finish.Month - dt_start.Month) + 12 * (dt_finish.Year - dt_start.Year));
            int CountDay = Convert.ToInt32((dt_finish - dt_start).TotalDays);
            int CountYear = CountDay / 365;

            float rate = Convert.ToSingle(dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString());
            sum = rate * CountMounth;//вычисление з.п. по ставке

            float bonus = CountYear * rate * 12 * ManagerPercent;

            if (bonus > ManagerLimit * sum)
            {
                bonus = ManagerLimit * sum;
            }

            sum = sum + bonus;

            //произведен расчет без сотрудников
            float sum_subordinate = 0;
            if (dataGridView2.Rows.Count > 0)
            {

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    if (dataGridView2[1, i].Value.ToString().Contains("1"))
                    {

                        string date_begin_subordinate = dataGridView2[3, i].Value.ToString();
                        DateTime dtdate_begin_subordinate = DateTime.ParseExact(date_begin_subordinate, "yyyy-MM-dd", null);//поступление на работу

                        DateTime dt_start_podchinennogo = dateTimePicker1.Value;
                        DateTime dt_finish_podchinennogo = dateTimePicker2.Value;
                        //если дата поступления больше даты начала
                        if (dt_start_podchinennogo < dtdate_begin_subordinate)
                        {
                            dt_start_podchinennogo = dtdate_begin_subordinate;
                        }

                        int CountMounth_subordinate = Math.Abs((dt_finish_podchinennogo.Month - dt_start_podchinennogo.Month) + 12 * (dt_finish_podchinennogo.Year - dt_start_podchinennogo.Year));
                        int CountDay_subordinate = Convert.ToInt32((dt_finish_podchinennogo - dt_start_podchinennogo).TotalDays);
                        int CountYear_subordinate = CountDay_subordinate / 365;

                        float rate_subordinate = Convert.ToSingle(dataGridView2[5, i].Value.ToString());
                        sum_subordinate = sum_subordinate + rate_subordinate * CountMounth_subordinate;//вычисление з.п. по ставке
                    }
                }
            }
            sum = sum + sum_subordinate * ManagerPercent_subordinate;
            return sum;
             }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public static float CalcSalesmanSalary(DataGridView dataGridView1, DataGridView dataGridView2, DateTimePicker dateTimePicker1, DateTimePicker dateTimePicker2)
        {

            string date_begin = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            DateTime dtdate_begin = DateTime.ParseExact(date_begin, "yyyy-MM-dd", null);//поступление на работу

            DateTime dt_start = dateTimePicker1.Value;
            DateTime dt_finish = dateTimePicker2.Value;
            //если дата поступления больше даты начала
            if (dt_start < dtdate_begin)
            {
                dt_start = dtdate_begin;
            }

            float sum = 0;

            int CountMounth = Math.Abs((dt_finish.Month - dt_start.Month) + 12 * (dt_finish.Year - dt_start.Year));
            int CountDay = Convert.ToInt32((dt_finish - dt_start).TotalDays);
            int CountYear = CountDay / 365;

            float rate = Convert.ToSingle(dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString());
            sum = rate * CountMounth;//вычисление з.п. по ставке

            float bonus = CountYear * rate * 12 * SalesmanPercent;

            if (bonus > SalesmanLimit * sum)
            {
                bonus = SalesmanLimit * sum;
            }

            sum = sum + bonus;

            float sum_subordinate = 0;
            if (dataGridView2.Rows.Count > 0)
            {

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                        string date_begin_subordinate = dataGridView2[3, i].Value.ToString();
                        DateTime dtdate_begin_subordinate = DateTime.ParseExact(date_begin_subordinate, "yyyy-MM-dd", null);//поступление на работу

                        DateTime dt_start_podchinennogo = dateTimePicker1.Value;
                        DateTime dt_finish_podchinennogo = dateTimePicker2.Value;
                        //если дата поступления больше даты начала
                        if (dt_start_podchinennogo < dtdate_begin_subordinate)
                        {
                            dt_start_podchinennogo = dtdate_begin_subordinate;
                        }

                        int CountMounth_subordinate = Math.Abs((dt_finish_podchinennogo.Month - dt_start_podchinennogo.Month) + 12 * (dt_finish_podchinennogo.Year - dt_start_podchinennogo.Year));
                        int CountDay_subordinate = Convert.ToInt32((dt_finish_podchinennogo - dt_start_podchinennogo).TotalDays);
                        int CountYear_subordinate = CountDay_subordinate / 365;

                        float rate_subordinate = Convert.ToSingle(dataGridView2[5, i].Value.ToString());
                        sum_subordinate = sum_subordinate + rate_subordinate * CountMounth_subordinate;//вычисление з.п. по ставке
                    
                }

            }
            sum = sum + sum_subordinate * SalesmanPercent_subordinate;
            return sum;
        }
    }
}
