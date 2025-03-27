using Arduino_Csharp_Serial_Communication_Control_a_Servo.Models;
using Arduino_Csharp_Serial_Communication_Control_a_Servo.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduino_Csharp_Serial_Communication_Control_a_Servo
{
    public partial class Details: Form
    {
        public Details()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //add data


            using (SqlConnection sqlConnection = new SqlConnection(AppHelper.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCmd = new SqlCommand("Insert into Details(ID,Name,Course,Address)values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')", sqlConnection))

                {

                    sqlCmd.ExecuteNonQuery();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";


                    MessageBox.Show("Details Successfully Saved!");

                    SqlDataAdapter das = new SqlDataAdapter("SELECT * FROM Details ", AppHelper.ConnectionString);
                    DataSet dss = new DataSet();
                    das.Fill(dss, "Details");

                    EventDataGridView.DataSource = dss.Tables["Details"].DefaultView;
                    EventDataGridView.Update();
                    EventDataGridView.Refresh();
                }
                sqlConnection.Close();
            }
            //end add data
        }

       


        private DataTable GetInventory()
        {

            DataTable dbInventory = new DataTable();
            using (SqlConnection db = new SqlConnection(AppHelper.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Details", db))
                {
                    db.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dbInventory.Load(reader);

                }
            }
            return dbInventory;

        }

        private void Details_Load(object sender, EventArgs e)
        {

            EventDataGridView.DataSource = GetInventory();
            //int numberOfRows = EventDataGridView.Rows.Count;
            //textBox1.Text = numberOfRows.ToString();

            EventDataGridView.Columns[0].DefaultCellStyle.Format = "n2";
            EventDataGridView.Columns[1].DefaultCellStyle.Format = "n2";
            EventDataGridView.Columns[2].DefaultCellStyle.Format = "n2";
            EventDataGridView.Columns[3].DefaultCellStyle.Format = "n2";

        }

        private void EventDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}
