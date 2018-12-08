#include <Mouse.h>
#include <Wire.h>
#include <TroykaIMU.h>
#include <math.h>
#include <stdlib.h>

Accelerometer accel;

const int deadZone=3;

const float acceleration=1.3;

void setup() {
  Serial.begin(9600);
  accel.begin();
  accel.setRange(RANGE_4G);
  Mouse.begin();
}

void loop() {
  int m0=analogRead(A0);
  int m1=analogRead(A1);

  int y = round(accel.readAX());
  int x = round(accel.readAY())-1;
  
  if(x<=deadZone && x>=-deadZone)
    x=0;
  if(y<=deadZone && y>=-deadZone)
    y=0;
  
  if(x>0) x-=deadZone;
  if(x<0) x+=deadZone;
  if(y>0) y-=deadZone;
  if(y<0) y+=deadZone;

  if (m0<65){
    Mouse.press(MOUSE_LEFT);
  }else{
    Mouse.release(MOUSE_LEFT);
  }

  if (m1<35){
    Mouse.press(MOUSE_RIGHT);
  }else{
    Mouse.release(MOUSE_RIGHT);
  }

  Mouse.move(x*acceleration,y*acceleration);
  Serial.println(String(x)+";"+String(y)+";"+String(m0)+";"+String(m1));
  
  delay(2);
}
