using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Arduino_Csharp_Serial_Communication_Control_a_Servo.Repository
{
  public static class AppHelper
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["Automated Desk"].ConnectionString;

    }
}
