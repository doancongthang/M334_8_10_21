using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusRTU;


namespace M334_8_10_21
{
    public class Machine
    {
        #region Signal start
        bool sw_start_auto;             //SW choose mode start. Lựa chọn chế độ khởi động
        bool btn_start;                 //Button start
        bool btn_on_preminary_pump;     //Bt bật Bơm sơ bộ
        bool btn_off_preminary_pump;    //Bt tắt bơm sơ bộ
        bool btn_on_low_airpressure;    //Bật quay áp thấp
        bool btn_on_hig_airpressure;    //Mở van khí khởi động (Bật khí cao áp)

        
        bool sig_vnd;                   //Tín hiệu khí thấp áp
        bool sig_vvd;                   //Tín hiệu khí cao áp
        bool sig_mpa;                   //Signal MPA    Áp suất dầu nhờn đạt 4KG/cm2
        bool sig_count_rotate;          //Tín hiệu đếm đủ số vòng quay. 
        bool sig_starting_forbidden;    //Tín hiệu cấm khởi động
        bool sig_upper_oil;             //Tín hiệu cao áp suất dầu nhờn
        bool sig_oil_supply;            //Tín hiệu cung cấp dầu.
        #endregion

        #region Signal and controll speed
        bool sig_higspeed;              //Signal high speed
        bool sig_goahead;               //Signal up
        bool sig_gobehind;              //Signal down
        bool sig_park;                  //Signal park at position

        bool sig_nopressure;            //Signal no pressure

        bool btn_up;                     //Bt Up
        bool btn_down;                   //Bt Down
        bool btn_quickdown;              //Bt giảm nhanh
        bool btn_estop;                  //Bt Emergency Stop
        #endregion

        #region Signal controll pump
        bool sw_zero_fuel_supply;       //SW zero fuel supply

        bool sig_pumping_out;           //Signal Pumping out
        bool sig_oil_isnot_pumpingout;  //Signal Oil is not pumping out
        bool sw_pumpout;                //Bơm hút dầu nhờn
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

        bool btn_burn;                  //Nut nhan đốt mạt sắt
        bool sw_protect;                //SW bảo vệ Auto/Manual
        #endregion

        #region Signal After start to main show
        // 3 giá trị
        int vl_temperature_gas;     //Nhiệt độ 
        int vl_speed_engine;        //Tốc độ vòng quay động cơ
        int vl_mainlineoilpressure; //Áp suất đường dẫn chính
        #endregion

        #region Khối chức năng
        public bool startauto()
        {
            return sw_start_auto;
        }
        public bool startmanual()
        {
            return btn_start;
        }
        public bool controlspeed()
        {
            return sig_nopressure;
        }
        public int presenttomain()
        {
            return vl_speed_engine;        //Tốc độ vòng quay động cơ
        }
        #endregion
    }
}
