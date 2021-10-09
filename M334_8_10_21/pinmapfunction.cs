using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M334_8_10_21
{
    public class pinmapfunction
    {
        #region Maincontrol
        // Tong so 11 bien
        bool power;             //Bt Power

        bool SW1;               //SW1 Switch mode Oil, Water
        bool SW2;               //SW2 Switch mode on Energy
        bool SW3;               //SW3 Switch mode Pump

        bool checklight;        //Bt check all light
        bool oilafterfil;       //Bt check oil after fil

        bool rswleft;           //Rotate SW position left
        bool rswright;          //Rotate SW position right
        bool rswmid;            //Rotate SW position middle

        bool callbehindcabin;   //Bt call KMO   Gọi khoang máy sau
        bool callheadcabin;     //Bt call HMO   Gọi khoang máy trước
        bool wheelhouse;        //Bt ходоб рубка
        #endregion

        #region Main show
        // Hien thi len dong ho 9 bien + 1 bien thoi gian
        int temperature_water_in;   //Temperature water input
        int temperature_water_out;  //Temperature water out
        int temperature_oil_in;     //Temperature oil in
        int temperature_oil_out;    //Temperature oil output
        int reverse_air_pressure;   //Reverse air pressure
        int hydraulics;             //Pressure hydraulics
        int pressurefuel;           //Pressure fuel
        int pressureptk;            //Pressure ptk
        int vloilafterfil;          //Value oil after filt
        int vloilbeforefil;         //Value oil before filt

        int time_hours;
        int time_minute;
        int time_second;
        int time_month;

        int sig_main_pump;          //Lamp main pump
        int sig_remote_pump;        //Lamp remote pump
        int sig_mainhas_pressure;   //Lamp main has pressure
        int sig_mainno_pressure;    //Lamp main has no pressure
        int sig_mainKMO;            //Lamp main KMO
        int sig_mainHMO;            //Lamp main HMO
        int sig_mainOK;             //Lamp main has Power
        int sig_main_hobbyshirt;    //Lamp main Ходоб рубка
        #endregion

        //machine machineleft = new machine()
        //machine machinemid = new machine();
        //machine machine_right = new machine();
    }
    public class machine
    {
        #region Signal start
        bool sw_start_auto;             //SW choose mode start. Lựa chọn chế độ khởi động
        bool bt_start;                  //Button start
        bool bt_on_preminary_pump;      //Bt bật Bơm sơ bộ
        bool bt_off_preminary_pump;     //Bt tắt bơm sơ bộ
        bool bt_on_low_airpressure;     //Bật quay áp thấp
        bool bt_on_hig_airpressure;     //Bở van khí khởi động (Bật khí cao áp)

        bool sig_count_rotate;          //Tín hiệu đếm đủ số vòng quay. 
        #endregion

        #region Signal and controll speed
        bool sig_higspeed;              //Signal high speed
        bool sig_goahead;               //Signal up
        bool sig_gobehind;              //Signal down
        bool sig_park;                  //Signal park at position

        bool sig_nopressure;            //Signal no pressure

        bool bt_up;                     //Bt Up
        bool bt_down;                   //Bt Down
        bool bt_quickdown;              //Bt giảm nhanh
        bool bt_estop;                  //Bt Emergency Stop
        #endregion

        #region Signal controll pump
        bool sw_zero_fuel_supply;   //SW zero fuel supply
        bool sig_mpa;               //Signal MPA

        bool sig_pumping_out;       //Signal Pumping out
        bool sig_oil_isnot_pumpingout;  //Signal Oil is not pumping out
        bool sw_pumpout;            //Bơm hút dầu nhờn
        #endregion

        #region Signal Alarm and protect
        // 8 Den canh bao
        bool sig_pressure_oil;          //Canh bao ap suat dau
        bool sig_temperature_oil;       //Canh bao nhiet do dau
        bool sig_pressure_water;        //Canh bao ap suat nuoc
        bool sig_temperature_water;     //Canh bao nhiet do nuoc
        bool sig_protect_on;            //Den bao ve bat tat
        bool sig_phi_s1;                //Canh bao mạt sắt 1
        bool sig_phi_s2;                //Canh bao mạt sắt 2
        bool sig_phi_s3;                //Canh bao mạt sắt 3

        bool bt_burn;                   //Nut nhan đốt mạt sắt
        bool sw_protect;                //SW bảo vệ Auto/Manual
        #endregion

        #region Signal After start to main show
        // 3 giá trị
        int vl_temperature_gas;     //Nhiệt độ 
        int vl_speed_engine;        //Tốc độ vòng quay động cơ
        int vl_mainlineoilpressure; //Áp suất đường dẫn chính
        #endregion  
        public machine(bool bt_hydraulic_oil_pump, bool bt_preliminary_pump)
        {

        }
    }
}
