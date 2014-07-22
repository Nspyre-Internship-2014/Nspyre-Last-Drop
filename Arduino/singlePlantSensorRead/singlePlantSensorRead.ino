void setup()
{
  Serial.begin(9800);
}

void loop()
{  
  Serial.println(analogRead(5));
  delay(1000);
}
