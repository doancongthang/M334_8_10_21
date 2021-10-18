using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusRTU;

namespace M334_8_10_21
{
    public class Pinmapfunction
    {
        #region Maincontrol
        // Tong so 11 bien
        public bool power;             //Bt Power

        public bool SW1;               //SW1 Switch mode Oil, Water
        public bool SW2;               //SW2 Switch mode on Energy
        public bool SW3;               //SW3 Switch mode Pump

        public bool checklight;        //Bt check all light
        public bool oilafterfil;       //Bt check oil after fil

        public bool rswleft;           //Rotate SW position left
        public bool rswright;          //Rotate SW position right
        public bool rswmid;            //Rotate SW position middle

        public bool callbehindcabin;   //Bt call KMO   Gọi khoang máy sau
        public bool callheadcabin;     //Bt call HMO   Gọi khoang máy trước
        public bool wheelhouse;        //Bt ходоб рубка
        #endregion

        #region Main show
        // Hien thi len dong ho 9 bien + 1 bien thoi gian                      
        public int temperature_water_in;   //Temperature water input           
        public int temperature_water_out;  //Temperature water out             
        public int temperature_oil_in;     //Temperature oil in                
        public int temperature_oil_out;    //Temperature oil output            
        public int reverse_air_pressure;   //Reverse air pressure              
        public int hydraulics;             //Pressure hydraulics               
        public int pressurefuel;           //Pressure fuel                     
        public int pressureptk;            //Pressure ptk                      
        public int vloilafterfil;          //Value oil after filt              
        public int vloilbeforefil;         //Value oil before filt             

        public int time_hours;
        public int time_minute;
        public int time_second;
        public int time_month;

        public int sig_main_pump;          //Lamp main pump                    
        public int sig_remote_pump;        //Lamp remote pump                  
        public int sig_mainhas_pressure;   //Lamp main has pressure            
        public int sig_mainno_pressure;    //Lamp main has no pressure         
        public int sig_mainKMO;            //Lamp main KMO                     
        public int sig_mainHMO;            //Lamp main HMO                     
        public int sig_mainOK;             //Lamp main has Power               
        public int sig_main_hobbyshirt;    //Lamp main Ходоб рубка
        #endregion
    }
}

