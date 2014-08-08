/// @dir input_demo
/// Demo for the input plug - read 16 analog input channels once a second.
// 2010-04-19 <jc@wippler.nl> http://opensource.org/licenses/mit-license.php

#include <JeeLib.h>

InputPlug input (1);
//MilliTimer sendTimer;
char payload [15];
int a;
byte needToSend;

void setup () {
    Serial.begin(57600);  
    input.mode2(INPUT);
     rf12_initialize(1, RF12_868MHZ, 33);
}

void loop () {
   delay(1000);
   // Serial.println("aaaa");
      a=input.anaRead();   
      if (rf12_recvDone() && rf12_crc == 0) {
        delay(100);
      }
       if (rf12_canSend()) {
          itoa(a,payload,10);  
		  // A represents the id of the jeenode , it can take values from 'A' to ']'
       payload[14]='A';  
         //    Serial.println(payload);   
        rf12_sendStart(0, payload, sizeof payload);  
    delay(1000);
      }
      
       for (byte j = 0; j < 1; ++j)
     Sleepy::loseSomeTime(60000); 
}
