using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Arduino_Csharp_Serial_Communication_Control_a_Servo.Repository;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Diagnostics.Eventing.Reader;

namespace Arduino_Csharp_Serial_Communication_Control_a_Servo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            timer1.Enabled = true;
            textBox1.Focus();



        }

        private void comboBox_portLists_DropDown(object sender, EventArgs e)
        {
            string[] portLists = SerialPort.GetPortNames();
            comboBox_portLists.Items.Clear();
            comboBox_portLists.Items.AddRange(portLists);
        }

        private void button_open_Click(object sender, EventArgs e)
        {

            try
            {

                serialPort1.PortName = comboBox_portLists.Text;
                serialPort1.BaudRate = 57600;
                serialPort1.Open();

                string str_degree = "0";

                serialPort1.Write(str_degree + "\n");
                //label_degree.Text = "Degree = " + str_degree + "°";
                //trackBar_degree.Value = Convert.ToInt32(str_degree);

                MessageBox.Show("Success Connected to Arduino Board");
                //groupBox_servoPosition.Enabled = true;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

    

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBox1.Text != "") {

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Details where ID='"+textBox1.Text+"'", AppHelper.ConnectionString);
                DataSet ds = new DataSet();
                da.Fill(ds, "Details");
               
             
                using (SqlConnection con = new SqlConnection(AppHelper.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Details WHERE ID ='" + textBox1.Text + "' "))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            cmd.CommandType = CommandType.Text;

                            sdr.Read();

                            //string sname = sdr["Name"].ToString();
                            if (sdr.HasRows)
                            {
                               
                                    string statID = sdr["ID"].ToString();
                                    string statName = sdr["Name"].ToString();
                                    string stat = sdr["Status"].ToString();
                                    if (stat.Equals("Logged    "))
                                    {


                                        //start update
                                        using (SqlConnection sqlConnection = new SqlConnection(AppHelper.ConnectionString))
                                        {
                                            using (SqlCommand sqlCmdEdit = new SqlCommand("Update dbo.Details set Status='Out'  where ID='" + textBox1.Text + "'  ", sqlConnection))
                                            {
                                                sqlConnection.Open();
                                                SqlDataReader reader = sqlCmdEdit.ExecuteReader();
                                                sqlConnection.Close();


                                                SqlDataAdapter das = new SqlDataAdapter("SELECT * FROM Details where ID='" + textBox1.Text + "'", AppHelper.ConnectionString);
                                                DataSet dss = new DataSet();
                                                das.Fill(dss, "Details");

                                                EventDataGridView.DataSource = dss.Tables["Details"].DefaultView;
                                                EventDataGridView.Update();
                                                EventDataGridView.Refresh();
                                                if (serialPort1.IsOpen)
                                                {
                                                    //string str_degree = "180";

                                                    serialPort1.Write(180 + "\n");
                                                    //label_degree.Text = "Degree = " + str_degree + "°";
                                                    //trackBar_degree.Value = Convert.ToInt32(str_degree);
                                                    //trackBar_degree.Value = 100;
                                                }


                                            }
                                        }
                                        //end update


                                        //add data


                                        using (SqlConnection sqlConnection = new SqlConnection(AppHelper.ConnectionString))
                                        {
                                            sqlConnection.Open();
                                            DateTime currentDate = DateTime.Now;
                                            using (SqlCommand sqlCmd = new SqlCommand("Insert into tbllogsheet(ID,Name,TimeIn)values('" + textBox1.Text + "','" + statName + "', '" + currentDate + "')", sqlConnection))

                                            {

                                                sqlCmd.ExecuteNonQuery();


                                            }
                                            sqlConnection.Close();
                                        }
                                        //end add data
                                    }
                                    else
                                    {


                                        //start update
                                        using (SqlConnection sqlConnection = new SqlConnection(AppHelper.ConnectionString))
                                        {
                                            using (SqlCommand sqlCmdEdit = new SqlCommand("Update dbo.Details set Status='Logged'  where ID='" + textBox1.Text + "'  ", sqlConnection))
                                            {
                                                sqlConnection.Open();
                                                SqlDataReader reader = sqlCmdEdit.ExecuteReader();
                                                sqlConnection.Close();
                                                SqlDataAdapter dass = new SqlDataAdapter("SELECT * FROM Details where ID='" + textBox1.Text + "'", AppHelper.ConnectionString);
                                                DataSet dsss = new DataSet();
                                                dass.Fill(dsss, "Details");

                                                EventDataGridView.DataSource = dsss.Tables["Details"].DefaultView;
                                                EventDataGridView.Update();
                                                EventDataGridView.Refresh();


                                                if (serialPort1.IsOpen)
                                                {
                                                  
                                                    serialPort1.Write(0 + "\n");
                                                }
                                            }


                                        }
                                        using (SqlConnection sqlConnection = new SqlConnection(AppHelper.ConnectionString))
                                        {
                                            sqlConnection.Open();
                                            DateTime currentDate = DateTime.Now;
                                            using (SqlCommand sqlCmd = new SqlCommand("Insert into tbllogsheet(ID,Name,TimeOut)values('" + textBox1.Text + "','" + statName + "', '" + currentDate + "')", sqlConnection))

                                            {

                                                sqlCmd.ExecuteNonQuery();


                                            }
                                            sqlConnection.Close();
                                        }
                                        //end update
                                    }

                                    textBox1.Text = "";
                                }
                            
                            else
                            {
                                textBox1.Text = "";
                                MessageBox.Show("ID not Found Please Register!");

                                ;
                            }
                        }
                        
                       
                    }
                }
                
               
            }
            



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Write(0 + "\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Write(90 + "\n");
        }
    }

}
