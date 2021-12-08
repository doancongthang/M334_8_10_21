using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using M334_8_10_21;

namespace M334_8_10_21.Services
{
    enum StateMachine
    {
        MACHINE_OFF,
        MACHINE_ON,
        HYDRAULICS_PUMP,
        READY_HYDRAULICS_PUPM,
        W_OFFTEST,
        TEST,
        CONTROLSPEED,
        CONTROLSPEED2,
        CONTROLSPEED3
    }
    enum STMC
    {
        IDLE,
        PROCESS_AUTO_W,
        PROCESS_AUTO_START,
        PRESSURE_PREMINARY_PUMP,
        READY_HIGH_PRESURE,
        START_OK,

        PROCESS_MANUAL,
        PROCESS_MANUAL_START,
        MANUAL_PRESSURE_PREMINARY_PUMP,
        MANUAL_READY_HIGH_PRESURE,

        MACHINEUP,
        PROCESS_MACHINE_UP,
        MACHINEHIGHSPEED,
        MACHINEDOWN,
        QUICKDOWN,
        REVERSE,
        STOP_POSITION
    }
    class LogicServices
    {
        StateMachine stateMachine;
        STMC stateMc1, stateMc2, stateMc3;
        public Machine mc1;
        public Machine mc2;
        public Machine mc3;

        public double countdown_hydraulics_pump = 0;
        public double coundown_preminary_pump = 0;
        public double coundown_speed_engine = 0;
        public double coundown_increte_pump = 0;
        public double coundown_temp_engine = 0;

        public double countdown_hydraulics_pump2 = 0;
        public double coundown_preminary_pump2 = 0;
        public double coundown_speed_engine2 = 0;
        public double coundown_increte_pump2 = 0;
        public double coundown_temp_engine2 = 0;

        public double countdown_hydraulics_pump3 = 0;
        public double coundown_preminary_pump3 = 0;
        public double coundown_speed_engine3 = 0;
        public double coundown_increte_pump3 = 0;
        public double coundown_temp_engine3 = 0;

        public LogicServices(Machine _mc1, Machine _mc2, Machine _mc3)
        {
            mc1 = _mc1;
            mc2 = _mc2;
            mc3 = _mc3;
            stateMachine = StateMachine.MACHINE_OFF;
            stateMc1 = STMC.IDLE;
            stateMc2 = STMC.IDLE;
            stateMc3 = STMC.IDLE;

            Thread delay1Thread = new Thread(Timer1Second);
            //Thread delay2Thread = new Thread(Timer2Second);
            //Thread delay3Thread = new Thread(Timer3Second);

            delay1Thread.Start();
            //delay2Thread.Start();
            //delay3Thread.Start();
        }

        private async void loopUpdateActionsStateMachine()
        {
            while (true)
            {
                Console.WriteLine(stateMachine);
                switch (stateMachine)  // KIEM TRA CAC DIEU KIEN
                {
                    case StateMachine.MACHINE_OFF:
                        if (Orionsystem.SW_power == true)
                        {
                            stateMachine = StateMachine.MACHINE_ON;
                        }
                        break;
                    case StateMachine.MACHINE_ON:
                        if (Orionsystem.SW_power == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (Orionsystem.btn_checklight == false)
                            stateMachine = StateMachine.TEST;
                        countdown_hydraulics_pump = Params.COUNT_HYDRAULICS_PUMP;
                        stateMachine = StateMachine.HYDRAULICS_PUMP;
                        //stateMachine = StateMachine.HYDRAULICS_PUMP;
                        break;
                    case StateMachine.HYDRAULICS_PUMP:
                        if (Orionsystem.btn_checklight == false & stateMachine == StateMachine.MACHINE_ON)
                            stateMachine = StateMachine.TEST;
                        if (Orionsystem.btn_checklight == false & Orionsystem.SW_power == true)
                        {
                            stateMachine = StateMachine.TEST;
                        }
                        if (Orionsystem.SW_power == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (countdown_hydraulics_pump == 0)
                        {
                            stateMachine = StateMachine.READY_HYDRAULICS_PUPM;
                        }
                        break;
                    case StateMachine.READY_HYDRAULICS_PUPM:
                        if (Orionsystem.SW_power == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (Orionsystem.btn_checklight == false)
                            stateMachine = StateMachine.TEST;
                        break;
                    case StateMachine.W_OFFTEST:
                        //stateMachine = StateMachine.HYDRAULICS_PUMP;
                        if (Orionsystem.btn_checklight == false & Orionsystem.SW_power == true)
                        {
                            stateMachine = StateMachine.TEST;
                        }
                        if (Orionsystem.SW_power == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (countdown_hydraulics_pump == 0)
                        {
                            stateMachine = StateMachine.READY_HYDRAULICS_PUPM;
                        }
                        if (countdown_hydraulics_pump != 0)
                        {
                            stateMachine = StateMachine.HYDRAULICS_PUMP;
                        }
                        break;
                    case StateMachine.TEST:
                        if (Orionsystem.btn_checklight == true & Orionsystem.SW_power == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (Orionsystem.btn_checklight == true)
                        {
                            stateMachine = StateMachine.W_OFFTEST;
                        }
                        //Moi them vao
                        break;
                }
                //***********************************************************************************************************//
                switch (stateMachine)  // ACTIONS
                {
                    case StateMachine.MACHINE_OFF:
                        while (Orionsystem.SW_power == false)
                        {
                            Orionsystem.sig_mainhas_pressure = false;
                            Orionsystem.sig_mainno_pressure = false;
                            mc1.offmachine();
                            mc2.offmachine();
                            mc3.offmachine();
                            Orionsystem.off_orion();
                        }
                        break;
                    case StateMachine.MACHINE_ON:
                        //stateMachine = StateMachine.MACHINE_OFF;
                        Orionsystem.sig_mainhas_pressure = false;
                        Orionsystem.sig_mainno_pressure = true;
                        mc1.sig_nopressure = true;
                        mc2.sig_nopressure = true;
                        mc3.sig_nopressure = true;
                        mc1.sig_oil_no_pump = true;
                        mc2.sig_oil_no_pump = true;
                        mc3.sig_oil_no_pump = true;
                        mc1.sig_park = true;
                        mc2.sig_park = true;
                        mc3.sig_park = true;
                        break;
                    case StateMachine.HYDRAULICS_PUMP:
                        Orionsystem.sig_mainno_pressure = true;
                        mc1.sig_oil_no_pump = true;
                        mc2.sig_oil_no_pump = true;
                        mc3.sig_oil_no_pump = true;
                        mc1.sig_park = true;
                        mc2.sig_park = true;
                        mc3.sig_park = true;
                        Orionsystem.vl_hydraulics = (int)((Params.COUNT_HYDRAULICS_PUMP * 0.3 - countdown_hydraulics_pump * 0.3));
                        break;
                    case StateMachine.READY_HYDRAULICS_PUPM:
                        Orionsystem.sig_mainhas_pressure = true;
                        Orionsystem.sig_mainno_pressure = false;
                        mc1.sig_nopressure = false;
                        mc2.sig_nopressure = false;
                        mc3.sig_nopressure = false;

                        //mc1.sig_park = true;
                        //mc2.sig_park = true;
                        //mc3.sig_park = true;
                        break;
                    case StateMachine.W_OFFTEST:
                        Orionsystem.sig_mainhas_pressure = true;
                        mc1.off_all_sig();
                        mc2.off_all_sig();
                        mc3.off_all_sig();
                        Orionsystem.off_all_sig_main();
                        break;
                    case StateMachine.TEST:
                        while (Orionsystem.btn_checklight == false)
                        {
                            mc1.on_all_sig();
                            mc2.on_all_sig();
                            mc3.on_all_sig();
                            Orionsystem.on_all_sig_main();
                        }
                        break;
                }
                await Task.Delay(1);
            }
        }
        private async void parallel1_updateMachine()
        {
            while (true)
            {
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    stateMc1 = STMC.IDLE;
                    stateMc2 = STMC.IDLE;
                    stateMc3 = STMC.IDLE;
                }
                if (stateMachine == StateMachine.READY_HYDRAULICS_PUPM)
                {
                    btn_only();
                    #region SW State
                    switch (stateMc1)
                    {
                        case STMC.IDLE:
                            if (mc1.sw_start_auto == true)
                            {
                                stateMc1 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc1.sw_start_auto == false)
                            {
                                stateMc1 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_W:
                            if (mc1.btn_start == false)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  // 4s
                                stateMc1 = STMC.PROCESS_AUTO_START;
                            }
                            //Mới thêm vào
                            if (mc1.sw_start_auto == false)
                            {
                                stateMc1 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc1.vl_mainlineoilpressure = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump) * 0.1);
                            if (coundown_preminary_pump == 0)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump = Params.COUNT_MAX_PREMINARY;
                                stateMc1 = STMC.PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc1.vl_mainlineoilpressure = ((Params.COUNT_MAX_LOWPRESSURE - coundown_increte_pump) * 0.1);
                            if (coundown_preminary_pump == 0)
                            {
                                stateMc1 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            if (coundown_preminary_pump == 0)
                            {
                                mc1.sig_vnd = false;
                                mc1.sig_count_rotate = false;
                            }
                            if (coundown_speed_engine == 0)
                            {
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                        case STMC.START_OK:
                            Console.WriteLine("Da khoi dong xong 1");
                            //stateMachine = StateMachine.CONTROLSPEED;
                            break;
                        //***********************************************//
                        //Các điều kiện chuyển Manual
                        case STMC.PROCESS_MANUAL:
                            if (mc1.sw_start_auto == true)
                                stateMc1 = STMC.PROCESS_AUTO_W;
                            if (mc1.btn_on_preminary_pump == false)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                                stateMc1 = STMC.PROCESS_MANUAL_START;
                            }
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            mc1.vl_mainlineoilpressure = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump) * 0.1);
                            if (mc1.btn_on_low_airpressure == false)
                                stateMc1 = STMC.MANUAL_PRESSURE_PREMINARY_PUMP;
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            if (mc1.btn_on_hig_airpressure == false)
                            {
                                coundown_speed_engine = Params.COUNT_SPEED_ENGINE;      //Tang toc dong co
                                coundown_temp_engine = Params.COUNT_TEMPERATURE_ENGINE; //Tăng nhiệt độ động cơ;
                                stateMc1 = STMC.MANUAL_READY_HIGH_PRESURE;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            if (coundown_speed_engine == 0)  // 
                            {
                                stateMc1 = STMC.START_OK;
                            }
                            //Khởi động xong thì qua điều khiển tốc độ
                            break;
                    }
                    #endregion
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    #region Action State
                    switch (stateMc1)
                    {
                        case STMC.IDLE:
                            mc1.sig_park = true;
                            break;

                        case STMC.PROCESS_AUTO_W:
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc1.sig_mpa = true;
                            mc1.vl_mainlineoilpressure = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump) * 0.1);
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc1.sig_vnd = true;
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_count_rotate = !mc1.sig_count_rotate;
                            }
                            if (mc1.sw_start_auto == false)
                            {

                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            mc1.sig_vnd = false;
                            mc1.sig_count_rotate = false;
                            mc1.sig_vvd = true;
                            mc1.vl_temperature_gas = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine));
                            mc1.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine));
                            break;
                        case STMC.START_OK:
                            mc1.sig_mpa = false;
                            mc1.sig_vvd = false;
                            mc1.sig_vnd = false;
                            mc1.sig_count_rotate = false;
                            break;

                        case STMC.PROCESS_MANUAL:
                            mc1.sig_mpa = false;
                            mc1.sig_vnd = false;
                            mc1.sig_vvd = false;
                            mc1.sig_count_rotate = false;
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            if (mc1.btn_on_preminary_pump == false)
                                mc1.sig_mpa = true;
                            if (mc1.btn_off_preminary_pump == false)                //Nút nhấn bị lỗi chưa fix. Đang dùng nút off của máy 2.
                                mc1.sig_mpa = false;
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            mc1.sig_vnd = true;
                            coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_count_rotate = !mc1.sig_count_rotate;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            mc1.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine));
                            mc1.vl_temperature_gas = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine));
                            mc1.sig_mpa = true;
                            mc1.sig_vvd = true;
                            mc1.sig_vnd = false;
                            mc1.sig_count_rotate = false;
                            break;
                    }
                    #endregion
                }
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    mc1.offmachine();
                    mc2.offmachine();
                    mc3.offmachine();
                    Orionsystem.off_orion();
                }
                //if (stateMachine == StateMachine.CONTROLSPEED)
                {
                    btn_only();
                    Console.WriteLine(stateMc1);
                    switch (stateMc1)
                    {
                        case STMC.START_OK:
                            if (mc1.btn_estop == false)
                            {
                                mc1.offmachine();
                                mc1.park();
                                mc1.sig_nopressure = true;
                                stateMc1 = STMC.IDLE;
                            }
                            if (mc1.btn_up == false)
                            {
                                stateMc1 = STMC.MACHINEUP;
                            }
                            if (mc1.btn_down == false)
                            {
                                stateMc1 = STMC.MACHINEDOWN;
                            }
                            if (mc1.btn_down == false)
                            {
                                coundown_speed_engine = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc1 = STMC.REVERSE;
                            }
                            if (mc1.sig_gobehind == true & mc1.btn_up == false)
                            {
                                coundown_speed_engine = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc1 = STMC.STOP_POSITION;
                            }
                            //if(mc1.vl_speed_engine <=75)
                            //{
                            //    mc1.sig_gobehind = false;
                            //    mc1.sig_park = true;
                            //}    
                            break;
                        case STMC.MACHINEUP:
                            coundown_speed_engine = Params.COUNT_STEP_ENGINE;           //Tăng tốc dần
                            mc1.sig_park = false;
                            mc1.sig_gobehind = false;
                            if (mc1.btn_up == false)
                            {
                                stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            //Nếu dừng
                            if (mc1.btn_estop == false)
                            {
                                mc1.offmachine();
                                mc1.park();
                                mc1.sig_nopressure = true;
                                stateMc1 = STMC.IDLE;
                            }

                            if (mc1.btn_down == false)
                            {
                                coundown_speed_engine = Params.COUNT_STEP_ENGINE;           //Giảm tốc dần
                                stateMc1 = STMC.MACHINEDOWN;
                            }
                            if (mc1.vl_speed_engine < 80)
                            {
                                mc1.sig_goahead = true;
                                mc1.sig_highspeed = false;
                                //mc1.sig_park = true;
                                //mc1.vl_speed_engine = 75;
                                //stateMc1 = STMC.START_OK;
                            }
                            if (mc1.vl_speed_engine < 75)
                            {
                                mc1.sig_goahead = false;
                                mc1.sig_highspeed = false;
                                mc1.sig_park = true;
                                mc1.vl_speed_engine = 75;
                                stateMc1 = STMC.START_OK;
                            }
                            if (mc1.btn_down == false)
                            {
                                mc1.sig_goahead = false;
                                mc1.sig_highspeed = false;
                                mc1.sig_park = false;
                                //mc1.sig_gobehind = true;
                            }


                            if (mc1.btn_quickdown == false)
                            {
                                coundown_speed_engine = mc1.vl_speed_engine - 80;
                                Params.COUNT_QUICKDOWN = mc1.vl_speed_engine - 80;
                                stateMc1 = STMC.QUICKDOWN;
                            }
                            break;
                        case STMC.PROCESS_MACHINE_UP:
                            mc1.vl_speed_engine = mc1.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine) / 10);
                            mc1.sig_goahead = true;
                            if (mc1.vl_speed_engine > 100)
                            {
                                mc1.sig_goahead = false;
                                mc1.sig_highspeed = true;
                            }

                            if (coundown_speed_engine == 0)
                            {
                                stateMc1 = STMC.MACHINEUP;
                            }
                            if (mc1.btn_up == false)
                            {
                                //mc2.vl_speed_engine = mc2.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine2) / 10);
                                stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            break;
                        case STMC.MACHINEDOWN:
                            mc1.vl_speed_engine = mc1.vl_speed_engine - ((Params.COUNT_STEP_ENGINE - coundown_speed_engine) / 10);
                            //if (mc1.vl_speed_engine < 100)
                            //{
                            //    mc1.sig_goahead = true;
                            //    mc1.sig_highspeed = false;
                            //    stateMc1 = STMC.MACHINEUP;
                            //}
                            if (mc1.vl_speed_engine > 80)
                            {
                                mc1.sig_goahead = true;
                                mc1.sig_park = false;
                                mc1.sig_highspeed = false;
                                stateMc1 = STMC.MACHINEUP;
                                //stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc1.vl_speed_engine > 100)
                            {
                                mc1.sig_goahead = false;
                                mc1.sig_highspeed = true;
                                stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc1.vl_speed_engine < 80)
                            {
                                mc1.sig_goahead = false;
                                mc1.sig_highspeed = false;
                                mc1.sig_park = true;
                                stateMc1 = STMC.MACHINEUP;
                                //mc1.vl_speed_engine = 80;
                            }
                            if (mc1.btn_up == false)     //Đang dùng nút nhấn mc2
                            {
                                stateMc1 = STMC.MACHINEUP;
                            }
                            if (mc1.btn_down == false)
                            {
                                stateMc1 = STMC.MACHINEDOWN;
                            }

                            break;
                        case STMC.REVERSE:
                            mc1.vl_speed_engine = mc1.vl_speed_engine + ((Params.COUNT_SPEED_REVERS - coundown_speed_engine) / 10);
                            mc1.sig_gobehind = true;
                            mc1.sig_park = false;
                            if (mc1.vl_speed_engine >= 80)
                            {
                                mc1.sig_gobehind = true;
                                stateMc1 = STMC.START_OK;
                            }
                            //stateMc1 = STMC.REVERSE;
                            break;
                        case STMC.STOP_POSITION:

                            mc1.vl_speed_engine = mc1.vl_speed_engine - ((Params.COUNT_SPEED_REVERS - coundown_speed_engine) / 10);
                            if (mc1.vl_speed_engine < 75)                    //Đang sai phần cứng. cần sửa lại nút nhấn Up1
                            {
                                //coundown_speed_engine = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                mc1.sig_park = true;
                                mc1.sig_highspeed = false;
                                mc1.sig_goahead = false;
                                mc1.sig_gobehind = false;
                                mc1.vl_speed_engine = 75;
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                        case STMC.QUICKDOWN:
                            mc1.vl_speed_engine = mc1.vl_speed_engine - ((Params.COUNT_QUICKDOWN - coundown_speed_engine) / 10);
                            if (mc1.vl_speed_engine <= 79)
                            {
                                mc1.sig_park = true;
                                mc1.sig_highspeed = false;
                                mc1.sig_goahead = false;
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                    }
                }
                await Task.Delay(1);
                //Console.WriteLine(stateMc1);
            }
        }
        private async void parallel2_updateMachine()
        {
            while (true)
            {
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    stateMc2 = STMC.IDLE;
                    stateMc2 = STMC.IDLE;
                    stateMc3 = STMC.IDLE;
                }
                if (stateMachine == StateMachine.READY_HYDRAULICS_PUPM)
                {
                    btn_only();
                    #region SW State
                    switch (stateMc2)
                    {
                        case STMC.IDLE:
                            if (mc2.sw_start_auto == true)
                            {
                                stateMc2 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc2.sw_start_auto == false)
                            {
                                stateMc2 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_W:
                            if (mc2.btn_start == false)
                            {
                                coundown_preminary_pump2 = Params.COUNT_MAX_PREMINARY;  // 4s
                                stateMc2 = STMC.PROCESS_AUTO_START;
                            }
                            //Mới thêm vào
                            if (mc2.sw_start_auto == false)
                            {
                                stateMc2 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc2.vl_mainlineoilpressure = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump2) * 0.1);
                            if (coundown_preminary_pump2 == 0)
                            {
                                coundown_preminary_pump2 = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump2 = Params.COUNT_MAX_PREMINARY;
                                stateMc2 = STMC.PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc2.vl_mainlineoilpressure = ((Params.COUNT_MAX_LOWPRESSURE - coundown_increte_pump2) * 0.1);
                            if (coundown_preminary_pump2 == 0)
                            {
                                stateMc2 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump2 = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine2 = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine2 = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            if (coundown_preminary_pump2 == 0)
                            {
                                mc2.sig_vnd = false;
                                mc2.sig_count_rotate = false;
                            }
                            if (coundown_speed_engine2 == 0)
                            {
                                stateMc2 = STMC.START_OK;
                            }
                            break;
                        case STMC.START_OK:
                            Console.WriteLine("Da khoi dong xong 2");
                            //stateMachine = StateMachine.CONTROLSPEED;
                            break;
                        //***********************************************//
                        //Các điều kiện chuyển Manual
                        case STMC.PROCESS_MANUAL:
                            if (mc2.sw_start_auto == true)
                                stateMc2 = STMC.PROCESS_AUTO_W;
                            if (mc2.btn_on_preminary_pump == false)
                            {
                                coundown_preminary_pump2 = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                                stateMc2 = STMC.PROCESS_MANUAL_START;
                            }
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            mc2.vl_mainlineoilpressure = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump2) * 0.1);
                            if (mc2.btn_on_low_airpressure == false)
                                stateMc2 = STMC.MANUAL_PRESSURE_PREMINARY_PUMP;
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            if (mc2.btn_on_hig_airpressure == false)
                            {
                                coundown_speed_engine2 = Params.COUNT_SPEED_ENGINE;      //Tang toc dong co
                                coundown_temp_engine2 = Params.COUNT_TEMPERATURE_ENGINE; //Tăng nhiệt độ động cơ;
                                stateMc2 = STMC.MANUAL_READY_HIGH_PRESURE;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            if (coundown_speed_engine2 == 0)  // 
                            {
                                stateMc2 = STMC.START_OK;
                            }
                            //Khởi động xong thì qua điều khiển tốc độ
                            break;
                    }
                    #endregion
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    #region Action State
                    switch (stateMc2)
                    {
                        case STMC.IDLE:
                            mc2.sig_park = true;
                            break;

                        case STMC.PROCESS_AUTO_W:
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc2.sig_mpa = true;
                            mc2.vl_mainlineoilpressure = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump2) * 0.1);
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc2.sig_vnd = true;
                            if (coundown_preminary_pump2 % 5 == 0)
                            {
                                mc2.sig_count_rotate = !mc2.sig_count_rotate;
                            }
                            if (mc2.sw_start_auto == false)
                            {

                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            mc2.sig_vnd = false;
                            mc2.sig_count_rotate = false;
                            mc2.sig_vvd = true;
                            mc2.vl_temperature_gas = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine2));
                            mc2.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine2));
                            break;
                        case STMC.START_OK:
                            mc2.sig_mpa = false;
                            mc2.sig_vvd = false;
                            mc2.sig_vnd = false;
                            mc2.sig_count_rotate = false;
                            break;

                        case STMC.PROCESS_MANUAL:
                            mc2.sig_mpa = false;
                            mc2.sig_vnd = false;
                            mc2.sig_vvd = false;
                            mc2.sig_count_rotate = false;
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            if (mc2.btn_on_preminary_pump == false)
                                mc2.sig_mpa = true;
                            if (mc2.btn_off_preminary_pump == false)                //Nút nhấn bị lỗi chưa fix. Đang dùng nút off của máy 2.
                                mc2.sig_mpa = false;
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            mc2.sig_vnd = true;
                            coundown_preminary_pump2 = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                            if (coundown_preminary_pump2 % 5 == 0)
                            {
                                mc2.sig_count_rotate = !mc2.sig_count_rotate;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            mc2.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine2));
                            mc2.vl_temperature_gas = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine2));
                            mc2.sig_mpa = true;
                            mc2.sig_vvd = true;
                            mc2.sig_vnd = false;
                            mc2.sig_count_rotate = false;
                            break;
                    }
                    #endregion
                }
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    mc2.offmachine();
                    mc2.offmachine();
                    mc3.offmachine();
                    Orionsystem.off_orion();
                }
                //if (stateMachine == StateMachine.CONTROLSPEED)
                {
                    btn_only();
                    switch (stateMc2)
                    {
                        case STMC.START_OK:
                            if (mc2.btn_estop == false)
                            {
                                mc2.offmachine();
                                mc2.park();
                                mc2.sig_nopressure = true;
                                stateMc2 = STMC.IDLE;
                            }
                            if (mc2.btn_up == false)
                            {
                                stateMc2 = STMC.MACHINEUP;
                            }
                            if (mc2.btn_down == false)
                            {
                                stateMc2 = STMC.MACHINEDOWN;
                            }
                            if (mc2.btn_down == false)
                            {
                                coundown_speed_engine2 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc2 = STMC.REVERSE;
                            }
                            if (mc2.sig_gobehind == true & mc2.btn_up == false)
                            {
                                coundown_speed_engine2 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc2 = STMC.STOP_POSITION;
                            }
                            //if(mc2.vl_speed_engine <=75)
                            //{
                            //    mc2.sig_gobehind = false;
                            //    mc2.sig_park = true;
                            //}    
                            break;
                        case STMC.MACHINEUP:
                            coundown_speed_engine2 = Params.COUNT_STEP_ENGINE;           //Tăng tốc dần
                            mc2.sig_park = false;
                            mc2.sig_gobehind = false;
                            if (mc2.btn_up == false)
                            {
                                stateMc2 = STMC.PROCESS_MACHINE_UP;
                            }
                            //Nếu dừng
                            if (mc2.btn_estop == false)
                            {
                                mc2.offmachine();
                                mc2.park();
                                //mc2.sig_nopressure = true;
                                stateMc2 = STMC.IDLE;
                            }

                            if (mc2.btn_down == false)
                            {
                                coundown_speed_engine2 = Params.COUNT_STEP_ENGINE;           //Giảm tốc dần
                                stateMc2 = STMC.MACHINEDOWN;
                            }
                            if (mc2.vl_speed_engine < 80)
                            {
                                mc2.sig_goahead = true;
                                mc2.sig_park = false;
                                mc2.sig_highspeed = false;
                                //mc2.sig_park = true;
                                //mc2.vl_speed_engine = 75;
                                //stateMc2 = STMC.START_OK;
                            }
                            if (mc2.vl_speed_engine < 75)
                            {
                                mc2.sig_goahead = false;
                                mc2.sig_highspeed = false;
                                mc2.sig_park = true;
                                mc2.vl_speed_engine = 75;
                                stateMc2 = STMC.START_OK;
                            }
                            if (mc2.btn_down == false)
                            {
                                mc2.sig_goahead = false;
                                mc2.sig_highspeed = false;
                                mc2.sig_park = false;
                                //mc2.sig_gobehind = true;
                            }


                            if (mc2.btn_quickdown == false)
                            {
                                coundown_speed_engine2 = mc2.vl_speed_engine - 80;
                                Params.COUNT_QUICKDOWN = mc2.vl_speed_engine - 80;
                                stateMc2 = STMC.QUICKDOWN;
                            }
                            break;
                        case STMC.PROCESS_MACHINE_UP:
                            mc2.vl_speed_engine = mc2.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine2) / 10);
                            mc2.sig_goahead = true;
                            if (mc2.vl_speed_engine > 100)
                            {
                                mc2.sig_goahead = false;
                                mc2.sig_highspeed = true;
                            }

                            if (coundown_speed_engine2 == 0)
                            {
                                stateMc2 = STMC.MACHINEUP;
                            }
                            if (mc2.btn_up == false)
                            {
                                //mc2.vl_speed_engine = mc2.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine2) / 10);
                                stateMc2 = STMC.PROCESS_MACHINE_UP;
                            }
                            break;
                        case STMC.MACHINEDOWN:
                            mc2.vl_speed_engine = mc2.vl_speed_engine - ((Params.COUNT_STEP_ENGINE - coundown_speed_engine2) / 10);
                            //if (mc2.vl_speed_engine < 100)
                            //{
                            //    mc2.sig_goahead = true;
                            //    mc2.sig_highspeed = false;
                            //    stateMc2 = STMC.MACHINEUP;
                            //}
                            if (mc2.vl_speed_engine > 80)
                            {
                                mc2.sig_goahead = true;
                                mc2.sig_highspeed = false;
                                stateMc2 = STMC.MACHINEUP;
                                //stateMc2 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc2.vl_speed_engine > 100)
                            {
                                mc2.sig_goahead = false;
                                mc2.sig_highspeed = true;
                                stateMc2 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc2.vl_speed_engine < 80)
                            {
                                mc2.sig_goahead = false;
                                mc2.sig_highspeed = false;
                                mc2.sig_park = true;
                                stateMc2 = STMC.MACHINEUP;
                                //mc2.vl_speed_engine = 80;
                            }
                            if (mc2.btn_up == false)     //Đang dùng nút nhấn mc2
                            {
                                stateMc2 = STMC.MACHINEUP;
                            }
                            if (mc2.btn_down == false)
                            {
                                stateMc2 = STMC.MACHINEDOWN;
                            }

                            break;
                        case STMC.REVERSE:
                            mc2.vl_speed_engine = mc2.vl_speed_engine + ((Params.COUNT_SPEED_REVERS - coundown_speed_engine2) / 10);
                            mc2.sig_gobehind = true;
                            mc2.sig_park = false;
                            if (mc2.vl_speed_engine >= 80)
                            {
                                mc2.sig_gobehind = true;
                                stateMc2 = STMC.START_OK;
                            }
                            //stateMc2 = STMC.REVERSE;
                            break;
                        case STMC.STOP_POSITION:

                            mc2.vl_speed_engine = mc2.vl_speed_engine - ((Params.COUNT_SPEED_REVERS - coundown_speed_engine2) / 10);
                            if (mc2.vl_speed_engine < 75)                    //Đang sai phần cứng. cần sửa lại nút nhấn Up1
                            {
                                //coundown_speed_engine2 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                mc2.sig_park = true;
                                mc2.sig_highspeed = false;
                                mc2.sig_goahead = false;
                                mc2.sig_gobehind = false;
                                mc2.vl_speed_engine = 75;
                                stateMc2 = STMC.START_OK;
                            }
                            break;
                        case STMC.QUICKDOWN:
                            mc2.vl_speed_engine = mc2.vl_speed_engine - ((Params.COUNT_QUICKDOWN - coundown_speed_engine2) / 10);
                            if (mc2.vl_speed_engine <= 78)
                            {
                                mc2.sig_park = true;
                                mc2.sig_highspeed = false;
                                mc2.sig_goahead = false;
                                stateMc2 = STMC.START_OK;
                            }
                            break;
                    }
                }
                await Task.Delay(1);
                //Console.WriteLine(stateMc2);
            }
        }
        private async void parallel3_updateMachine()
        {
            while (true)
            {
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    stateMc3 = STMC.IDLE;
                    //stateMc3 = STMC.IDLE;
                    //stateMc3 = STMC.IDLE;
                }
                if (stateMachine == StateMachine.READY_HYDRAULICS_PUPM)
                {
                    btn_only();
                    #region SW State
                    switch (stateMc3)
                    {
                        case STMC.IDLE:
                            if (mc3.sw_start_auto == true)
                            {
                                stateMc3 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc3.sw_start_auto == false)
                            {
                                stateMc3 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_W:
                            if (mc3.btn_start == false)
                            {
                                coundown_preminary_pump3 = Params.COUNT_MAX_PREMINARY;  // 4s
                                stateMc3 = STMC.PROCESS_AUTO_START;
                            }
                            //Mới thêm vào
                            if (mc3.sw_start_auto == false)
                            {
                                stateMc3 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc3.vl_mainlineoilpressure = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump3) * 0.1);
                            if (coundown_preminary_pump3 == 0)
                            {
                                coundown_preminary_pump3 = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump3 = Params.COUNT_MAX_PREMINARY;
                                stateMc3 = STMC.PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc3.vl_mainlineoilpressure = ((Params.COUNT_MAX_LOWPRESSURE - coundown_increte_pump3) * 0.1);
                            if (coundown_preminary_pump3 == 0)
                            {
                                stateMc3 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump3 = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine3 = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine3 = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            if (coundown_preminary_pump3 == 0)
                            {
                                mc3.sig_vnd = false;
                                mc3.sig_count_rotate = false;
                            }
                            if (coundown_speed_engine3 == 0)
                            {
                                stateMc3 = STMC.START_OK;
                            }
                            break;
                        case STMC.START_OK:
                            Console.WriteLine("Da khoi dong xong");
                            //stateMachine = StateMachine.CONTROLSPEED1;
                            break;
                        //***********************************************//
                        //Các điều kiện chuyển Manual
                        case STMC.PROCESS_MANUAL:
                            if (mc3.sw_start_auto == true)
                                stateMc3 = STMC.PROCESS_AUTO_W;
                            if (mc3.btn_on_preminary_pump == false)
                            {
                                coundown_preminary_pump3 = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                                stateMc3 = STMC.PROCESS_MANUAL_START;
                            }
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            mc3.vl_mainlineoilpressure = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump3) * 0.1);
                            if (mc3.btn_on_low_airpressure == false)
                                stateMc3 = STMC.MANUAL_PRESSURE_PREMINARY_PUMP;
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            if (mc3.btn_on_hig_airpressure == false)
                            {
                                coundown_speed_engine3 = Params.COUNT_SPEED_ENGINE;      //Tang toc dong co
                                coundown_temp_engine3 = Params.COUNT_TEMPERATURE_ENGINE; //Tăng nhiệt độ động cơ;
                                stateMc3 = STMC.MANUAL_READY_HIGH_PRESURE;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            if (coundown_speed_engine3 == 0)  // 
                            {
                                stateMc3 = STMC.START_OK;
                            }
                            //Khởi động xong thì qua điều khiển tốc độ
                            break;
                    }
                    #endregion
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    #region Action State
                    switch (stateMc3)
                    {
                        case STMC.IDLE:
                            mc3.sig_park = true;
                            break;

                        case STMC.PROCESS_AUTO_W:
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc3.sig_mpa = true;
                            mc3.vl_mainlineoilpressure = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump3) * 0.1);
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc3.sig_vnd = true;
                            if (coundown_preminary_pump3 % 5 == 0)
                            {
                                mc3.sig_count_rotate = !mc3.sig_count_rotate;
                            }
                            if (mc3.sw_start_auto == false)
                            {

                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            mc3.sig_vnd = false;
                            mc3.sig_count_rotate = false;
                            mc3.sig_vvd = true;
                            mc3.vl_temperature_gas = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine3));
                            mc3.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine3));
                            break;
                        case STMC.START_OK:
                            mc3.sig_mpa = false;
                            mc3.sig_vvd = false;
                            mc3.sig_vnd = false;
                            mc3.sig_count_rotate = false;
                            break;

                        case STMC.PROCESS_MANUAL:
                            mc3.sig_mpa = false;
                            mc3.sig_vnd = false;
                            mc3.sig_vvd = false;
                            mc3.sig_count_rotate = false;
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            if (mc3.btn_on_preminary_pump == false)
                                mc3.sig_mpa = true;
                            if (mc3.btn_off_preminary_pump == false)                //Nút nhấn bị lỗi chưa fix. Đang dùng nút off của máy 2.
                                mc3.sig_mpa = false;
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            mc3.sig_vnd = true;
                            coundown_preminary_pump3 = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                            if (coundown_preminary_pump3 % 5 == 0)
                            {
                                mc3.sig_count_rotate = !mc3.sig_count_rotate;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            mc3.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine3));
                            mc3.vl_temperature_gas = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine3));
                            mc3.sig_mpa = true;
                            mc3.sig_vvd = true;
                            mc3.sig_vnd = false;
                            mc3.sig_count_rotate = false;
                            break;
                    }
                    #endregion
                }
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    mc3.offmachine();
                    mc3.offmachine();
                    mc3.offmachine();
                    Orionsystem.off_orion();
                }
                //if (stateMachine == StateMachine.CONTROLSPEED1)
                {
                    btn_only();
                    switch (stateMc3)
                    {
                        case STMC.START_OK:
                            if (mc3.btn_estop == false)
                            {
                                mc3.offmachine();
                                mc3.park();
                                stateMc3 = STMC.IDLE;
                            }
                            if (mc3.btn_up == false)                    //Đang sai phần cứng. cần sửa lại nút nhấn Up1
                            {
                                stateMc3 = STMC.MACHINEUP;
                            }
                            if (mc3.btn_down == false)
                            {
                                stateMc3 = STMC.MACHINEDOWN;
                            }
                            if (mc3.btn_down == false)
                            {
                                coundown_speed_engine3 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc3 = STMC.REVERSE;
                            }
                            if (mc3.sig_gobehind == true & mc3.btn_up == false)
                            {
                                coundown_speed_engine3 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc3 = STMC.STOP_POSITION;
                            }
                            //if(mc3.vl_speed_engine <=75)
                            //{
                            //    mc3.sig_gobehind = false;
                            //    mc3.sig_park = true;
                            //}    
                            break;
                        case STMC.MACHINEUP:
                            coundown_speed_engine3 = Params.COUNT_STEP_ENGINE;           //Tăng tốc dần
                            mc3.sig_park = false;
                            mc3.sig_gobehind = false;
                            if (mc3.btn_up == false)
                            {
                                stateMc3 = STMC.PROCESS_MACHINE_UP;
                            }
                            //Nếu dừng
                            if (mc3.btn_estop == false)
                            {
                                mc3.offmachine();
                                mc3.park();
                                stateMc3 = STMC.IDLE;
                            }

                            if (mc3.btn_down == false)
                            {
                                coundown_speed_engine3 = Params.COUNT_STEP_ENGINE;           //Giảm tốc dần
                                stateMc3 = STMC.MACHINEDOWN;
                            }
                            if (mc3.vl_speed_engine < 80)
                            {
                                mc3.sig_goahead = true;
                                mc3.sig_highspeed = false;
                                //mc3.sig_park = true;
                                //mc3.vl_speed_engine = 75;
                                //stateMc3 = STMC.START_OK;
                            }
                            if (mc3.vl_speed_engine < 75)
                            {
                                mc3.sig_goahead = false;
                                mc3.sig_highspeed = false;
                                mc3.sig_park = true;
                                mc3.vl_speed_engine = 75;
                                stateMc3 = STMC.START_OK;
                            }
                            if (mc3.btn_down == false)
                            {
                                mc3.sig_goahead = false;
                                mc3.sig_highspeed = false;
                                mc3.sig_park = false;
                                //mc3.sig_gobehind = true;
                            }


                            if (mc3.btn_quickdown == false)
                            {
                                coundown_speed_engine3 = mc3.vl_speed_engine - 80;
                                Params.COUNT_QUICKDOWN = mc3.vl_speed_engine - 80;
                                stateMc3 = STMC.QUICKDOWN;
                            }
                            break;
                        case STMC.PROCESS_MACHINE_UP:
                            mc3.vl_speed_engine = mc3.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine3) / 10);
                            mc3.sig_goahead = true;
                            if (mc3.vl_speed_engine > 100)
                            {
                                mc3.sig_goahead = false;
                                mc3.sig_highspeed = true;
                            }

                            if (coundown_speed_engine3 == 0)
                            {
                                stateMc3 = STMC.MACHINEUP;
                            }
                            if (mc3.btn_up == false)
                            {
                                //mc2.vl_speed_engine = mc2.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine2) / 10);
                                stateMc3 = STMC.PROCESS_MACHINE_UP;
                            }
                            break;
                        case STMC.MACHINEDOWN:
                            mc3.vl_speed_engine = mc3.vl_speed_engine - ((Params.COUNT_STEP_ENGINE - coundown_speed_engine3) / 10);
                            //if (mc3.vl_speed_engine < 100)
                            //{
                            //    mc3.sig_goahead = true;
                            //    mc3.sig_highspeed = false;
                            //    stateMc3 = STMC.MACHINEUP;
                            //}
                            if (mc3.vl_speed_engine > 80)
                            {
                                mc3.sig_goahead = true;
                                mc3.sig_highspeed = false;
                                stateMc3 = STMC.MACHINEUP;
                                //stateMc3 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc3.vl_speed_engine > 100)
                            {
                                mc3.sig_goahead = false;
                                mc3.sig_highspeed = true;
                                stateMc3 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc3.vl_speed_engine < 80)
                            {
                                mc3.sig_goahead = false;
                                mc3.sig_highspeed = false;
                                mc3.sig_park = true;
                                stateMc3 = STMC.MACHINEUP;
                                //mc3.vl_speed_engine = 80;
                            }
                            if (mc3.btn_up == false)     //Đang dùng nút nhấn mc3
                            {
                                stateMc3 = STMC.MACHINEUP;
                            }
                            if (mc3.btn_down == false)
                            {
                                stateMc3 = STMC.MACHINEDOWN;
                            }

                            break;
                        case STMC.REVERSE:
                            mc3.vl_speed_engine = mc3.vl_speed_engine + ((Params.COUNT_SPEED_REVERS - coundown_speed_engine3) / 10);
                            mc3.sig_gobehind = true;
                            mc3.sig_park = false;
                            if (mc3.vl_speed_engine >= 80)
                            {
                                mc3.sig_gobehind = true;
                                stateMc3 = STMC.START_OK;
                            }
                            //stateMc3 = STMC.REVERSE;
                            break;
                        case STMC.STOP_POSITION:

                            mc3.vl_speed_engine = mc3.vl_speed_engine - ((Params.COUNT_SPEED_REVERS - coundown_speed_engine3) / 10);
                            if (mc3.vl_speed_engine < 75)                    //Đang sai phần cứng. cần sửa lại nút nhấn Up1
                            {
                                //coundown_speed_engine32 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                mc3.sig_park = true;
                                mc3.sig_highspeed = false;
                                mc3.sig_goahead = false;
                                mc3.sig_gobehind = false;
                                mc3.vl_speed_engine = 75;
                                stateMc3 = STMC.START_OK;
                            }
                            break;
                        case STMC.QUICKDOWN:
                            mc3.vl_speed_engine = mc3.vl_speed_engine - ((Params.COUNT_QUICKDOWN - coundown_speed_engine3) / 10);
                            if (mc3.vl_speed_engine <= 78)
                            {
                                mc3.sig_park = true;
                                mc3.sig_highspeed = false;
                                mc3.sig_goahead = false;
                                stateMc3 = STMC.START_OK;
                            }
                            break;
                    }
                }
                await Task.Delay(1);
                //Console.WriteLine(stateMc3);
            }
        }
        public void btn_only()
        {
            //***********Tương tác độc lập SW
            if (Orionsystem.SW_power == false)
            {
                stateMachine = StateMachine.MACHINE_OFF;
            }
            {
                #region btn for machine
                if (mc1.sw_pumpout == true)
                {
                    mc1.sig_otka = true;
                    mc1.sig_oil_no_pump = false;
                }
                if (mc1.sw_pumpout == false)
                {
                    mc1.sig_otka = false;
                    mc1.sig_oil_no_pump = true;
                }

                if (mc2.sw_pumpout == true)
                {
                    mc2.sig_otka = true;
                    mc2.sig_oil_no_pump = false;
                }
                if (mc2.sw_pumpout == false)
                {
                    mc2.sig_otka = false;
                    mc2.sig_oil_no_pump = true;
                }

                if (mc3.sw_pumpout == true)
                {
                    mc3.sig_otka = true;
                    mc3.sig_oil_no_pump = false;
                }
                if (mc3.sw_pumpout == false)
                {
                    mc3.sig_otka = false;
                    mc3.sig_oil_no_pump = true;
                }
                //***************************************//
                if (mc1.sw_protect == true)
                {
                    mc1.sig_protect_on = true;
                }
                if (mc1.sw_protect == false)
                {
                    mc1.sig_protect_on = false;
                }

                if (mc2.sw_protect == true)
                {
                    mc2.sig_protect_on = true;
                }
                if (mc2.sw_protect == false)
                {
                    mc2.sig_protect_on = false;
                }

                if (mc3.sw_protect == true)
                {
                    mc3.sig_protect_on = true;
                }
                if (mc3.sw_protect == false)
                {
                    mc3.sig_protect_on = false;
                }

                //***************************************//
                if (mc1.sw_zero_fuel_supply == true)
                {
                    mc1.sig_pumping_MPA = true;
                }
                if (mc1.sw_zero_fuel_supply == false)
                {
                    mc1.sig_pumping_MPA = false;
                }

                if (mc2.sw_zero_fuel_supply == true)
                {
                    mc2.sig_pumping_MPA = true;
                }
                if (mc2.sw_zero_fuel_supply == false)
                {
                    mc2.sig_pumping_MPA = false;
                }

                if (mc3.sw_zero_fuel_supply == true)
                {
                    mc3.sig_pumping_MPA = true;
                }
                if (mc3.sw_zero_fuel_supply == false)
                {
                    mc3.sig_pumping_MPA = false;
                }
                #endregion
                //***************************************//
                #region btn_main
                if (Orionsystem.btn_callheadcabin == false)
                {
                    Orionsystem.sig_mainHMO = true;
                }
                if (Orionsystem.btn_callheadcabin == true)
                {
                    Orionsystem.sig_mainHMO = false;
                }

                if (Orionsystem.btn_callbehindcabin == false)
                {
                    Orionsystem.sig_mainKMO = true;
                }
                if (Orionsystem.btn_callbehindcabin == true)
                {
                    Orionsystem.sig_mainKMO = false;
                }

                if (Orionsystem.btn_wheelhouse == false)
                {
                    Orionsystem.sig_main_hobbyshirt = true;
                }
                if (Orionsystem.btn_wheelhouse == true)
                {
                    Orionsystem.sig_main_hobbyshirt = false;
                }
                if (Orionsystem.SW3 == true)
                {
                    Orionsystem.sig_remote_pump = true;
                    Orionsystem.sig_main_pump = false;
                }
                if (Orionsystem.SW3 == false)
                {
                    Orionsystem.sig_remote_pump = false;
                    Orionsystem.sig_main_pump = true;
                }
                #endregion
                #region
                #endregion
            }
        }
        private void Timer1Second()
        {
            while (true)
            {
                if (countdown_hydraulics_pump > 0)
                    countdown_hydraulics_pump--;
                if (coundown_preminary_pump > 0)
                    coundown_preminary_pump--;
                if (coundown_increte_pump > 0)
                    coundown_increte_pump--;
                if (coundown_speed_engine > 0)
                    coundown_speed_engine--;
                if (coundown_temp_engine > 0)
                    coundown_temp_engine--;
                // await Task.Delay(100);
                if (countdown_hydraulics_pump2 > 0)
                    countdown_hydraulics_pump2--;
                if (coundown_preminary_pump2 > 0)
                    coundown_preminary_pump2--;
                if (coundown_increte_pump2 > 0)
                    coundown_increte_pump2--;
                if (coundown_speed_engine2 > 0)
                    coundown_speed_engine2--;
                if (coundown_temp_engine2 > 0)
                    coundown_temp_engine2--;
                // await Task.Delay(100);
                if (countdown_hydraulics_pump3 > 0)
                    countdown_hydraulics_pump3--;
                if (coundown_preminary_pump3 > 0)
                    coundown_preminary_pump3--;
                if (coundown_increte_pump3 > 0)
                    coundown_increte_pump3--;
                if (coundown_speed_engine3 > 0)
                    coundown_speed_engine3--;
                if (coundown_temp_engine3 > 0)
                    coundown_temp_engine3--;
                Thread.Sleep(100);
            }
        }
        private void Timer2Second()
        {
            while (true)
            {
                if (countdown_hydraulics_pump2 > 0)
                    countdown_hydraulics_pump2--;
                if (coundown_preminary_pump2 > 0)
                    coundown_preminary_pump2--;
                if (coundown_increte_pump2 > 0)
                    coundown_increte_pump2--;
                if (coundown_speed_engine2 > 0)
                    coundown_speed_engine2--;
                if (coundown_temp_engine2 > 0)
                    coundown_temp_engine2--;
                // await Task.Delay(100);
                Thread.Sleep(100);
            }
        }
        private void Timer3Second()
        {
            while (true)
            {
                if (countdown_hydraulics_pump3 > 0)
                    countdown_hydraulics_pump3--;
                if (coundown_preminary_pump3 > 0)
                    coundown_preminary_pump3--;
                if (coundown_increte_pump3 > 0)
                    coundown_increte_pump3--;
                if (coundown_speed_engine3 > 0)
                    coundown_speed_engine3--;
                if (coundown_temp_engine3 > 0)
                    coundown_temp_engine3--;
                // await Task.Delay(100);
                Thread.Sleep(100);
            }
        }
        public void Subcribe()
        {
            Task control = Task.Run(() => loopUpdateActionsStateMachine());  // Khởi chạy loop services
                                                                             //  Task timer1s = Task.Run(() => Timer1Second());
            Task stateEachMachine1 = Task.Run(() => parallel1_updateMachine());
            Task stateEachMachine2 = Task.Run(() => parallel2_updateMachine());
            Task stateEachMachine3 = Task.Run(() => parallel3_updateMachine());
        }
    }
}
