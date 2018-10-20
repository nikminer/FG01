#include <Wire.h>
#include <TroykaIMU.h>
#include <math.h>
#include <stdlib.h>

Accelerometer accel;

void setup() {
  Serial.begin(9600);
  accel.begin();
  // 2g — по умолчанию, 4g, 8g
  accel.setRange(RANGE_4G);

  pinMode(6,OUTPUT);
  pinMode(7,INPUT);
  digitalWrite(6,HIGH);
  pinMode(5,OUTPUT);
  pinMode(4,INPUT);
  digitalWrite(5,HIGH);
}

void loop() {
  int m0=digitalRead(7);
  int m1=digitalRead(4);
  int x = round(accel.readAX());
  int y = round(accel.readAY())-1;
  
  if(x<=2 && x>=-2)
    x=0;
  if(y<=2 && y>=-2)
    y=0;
  
  if(x>0) x-=2;
  if(x<0) x+=2;
  if(y>0) y-=2;
  if(y<0) y+=2;
  Serial.println(String(x)+";"+String(y)+";"+String(m0)+";"+String(m1));
  //delay(3);
}
