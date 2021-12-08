using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M334_8_10_21;

namespace M334_8_10_21
{
    class Params
    {
        public static double COUNT_HYDRAULICS_PUMP = 50;
        public static double COUNT_DELAY_START = 100;                   //
        public static double COUNT_MAX_PREMINARY = 50;                  //4s
        public static double COUNT_MAX_LOWPRESSURE = 100;               //8s
        public static double COUNT_READY_FOR_SPEED = 10;                //1s
        public static double COUNT_SPEED_ENGINE = 75;
        public static double COUNT_TEMPERATURE_ENGINE = 50;
        public static double COUNT_STEP_ENGINE = 1;
        public static double COUNT_SPEED_REVERS = 80;
        public static double COUNT_TEMP_OIL_ENGINE = 100;               //Thông số chưa biết chắc chắn
        public static double COUNT_TEMP_WATER_ENGINE = 80;              //Chỉnh sau
        public static double COUNT_QUICKDOWN;

        //public static int COUNT_HYDRAULICS_PUMP = 50;
        //public static int COUNT_HYDRAULICS_PUMP = 50;
        //public static int COUNT_HYDRAULICS_PUMP = 50;
        //public static int COUNT_HYDRAULICS_PUMP = 50;
        //public static int COUNT_HYDRAULICS_PUMP = 50;
        public Params() { }
    }
}
