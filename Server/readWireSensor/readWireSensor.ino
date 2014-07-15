void setup()
{
  Serial.begin(9800);
}

void loop()
{
  Serial.print("Moisture sensor value");
  Serial.println(analogRead(5));
  delay(1000);
}
