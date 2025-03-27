#include <Servo.h>

Servo myServo;

char c;
String dataIn;
int degree;

void setup() {
  // put your setup code here, to run once:
    Serial.begin(57600);
    Serial.println("Control a Servo");

    myServo.attach(9);
}

void loop() {
  // put your main code here, to run repeatedly:
  
    Receive_Serial_Data();
    myServo.write(degree);
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

void Receive_Serial_Data()
{
    while(Serial.available()>0)
      {
          c = Serial.read();
          if(c=='\n') {break;}
          else        {dataIn+=c;}
      }
     if(c=='\n')
      {
          degree = dataIn.toInt();
          c=0;
          dataIn="";
      }
}
