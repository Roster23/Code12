using Arduino_Csharp_Serial_Communication_Control_a_Servo.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Arduino_Csharp_Serial_Communication_Control_a_Servo
{
    public partial class Timelogs: Form
    {
        public Timelogs()
        {
            InitializeComponent();
        }

        private void Timelogs_Load(object sender, EventArgs e)
        {


            using (SqlConnection sqlConnection = new SqlConnection(AppHelper.ConnectionString))
            {
                sqlConnection.Open();



                SqlDataAdapter das = new SqlDataAdapter("SELECT * FROM tbllogsheet ", AppHelper.ConnectionString);
                DataSet dss = new DataSet();
                das.Fill(dss, "tbllogsheet");

                EventDataGridView.DataSource = dss.Tables["tbllogsheet"].DefaultView;
                EventDataGridView.Update();
                EventDataGridView.Refresh();

                sqlConnection.Close();
            }
        }
    }
}
