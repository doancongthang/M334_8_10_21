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
        CONTROLSPEED
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
        MACHINEHIGHSPEED,
        MACHINEDOWN,
    }

    class LogicServices
    {
        StateMachine stateMachine;
        STMC stateMc1, stateMc2, stateMc3;
        public Machine mc1;
        public Machine mc2;
        public Machine mc3;

        public int countdown_hydraulics_pump = 0;
        public int coundown_preminary_pump = 0;
        public int coundown_speed_engine = 0;

        public LogicServices(Machine _mc1, Machine _mc2, Machine _mc3)
        {
            mc1 = _mc1;
            mc2 = _mc2;
            mc3 = _mc3;
            stateMachine = StateMachine.MACHINE_OFF;
            stateMc1 = STMC.IDLE;
            stateMc2 = STMC.IDLE;
            stateMc3 = STMC.IDLE;

            Thread delay100mdThread = new Thread(Timer1Second);
            delay100mdThread.Start();
        }

        private async void loopUpdateActionsStateMachine()
        {
            //***********************************************************************************************************//
            while (true)
            {
                Console.WriteLine(stateMachine);
                Console.WriteLine(Orionsystem.SW_power);
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
                        Orionsystem.sig_mainhas_pressure = false;
                        Orionsystem.sig_mainno_pressure = false;
                        mc1.offmachine();
                        mc2.offmachine();
                        mc3.offmachine();
                        Orionsystem.off_orion();
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
                        mc3.sig_oil_no_pump= true;
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
                        Orionsystem.vl_hydraulics = (int)((Params.COUNT_HYDRAULICS_PUMP - countdown_hydraulics_pump) * 0.3);
                        break;
                    case StateMachine.READY_HYDRAULICS_PUPM:
                        Orionsystem.sig_mainhas_pressure = true;
                        Orionsystem.sig_mainno_pressure = false;
                        mc1.sig_nopressure = false;
                        mc2.sig_nopressure = false;
                        mc3.sig_nopressure = false;
                        mc1.sig_park = true;
                        mc2.sig_park = true;
                        mc3.sig_park = true;
                        break;
                    case StateMachine.W_OFFTEST:
                        Orionsystem.sig_mainhas_pressure = true;

                        mc1.off_all_sig();
                        mc2.off_all_sig();
                        mc3.off_all_sig();
                        Orionsystem.off_all_sig_main();
                        break;
                    case StateMachine.TEST:
                        mc1.on_all_sig();
                        mc2.on_all_sig();
                        mc3.on_all_sig();
                        Orionsystem.on_all_sig_main();
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
                    stateMc1 = STMC.IDLE;
                    stateMc2 = STMC.IDLE;
                    stateMc3 = STMC.IDLE;
                }   
                if (stateMachine == StateMachine.READY_HYDRAULICS_PUPM)
                {
                    //State start///////////////////////////////////////////////////////////////////////////
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
                            if(mc1.btn_start == false)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  // 4s
                                stateMc1 = STMC.PROCESS_AUTO_START;
                            }    
                            break;
                        case STMC.PROCESS_AUTO_START:
                            //if (mc1.sw_start_auto == false)
                            //    stateMc1 = STMC.PROCESS_MANUAL;
                            if (coundown_preminary_pump == 0)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                                stateMc1 = STMC.PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            if (coundown_preminary_pump == 0)
                            {
                                stateMc1 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            if (coundown_preminary_pump == 0)
                            {
                                mc1.sig_vnd = false;
                                mc1.sig_count_rotate = false;
                            }
                            if(coundown_speed_engine == 0)
                            {
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                        case STMC.START_OK:
                            if(mc1.btn_estop == false)
                            {
                                mc1.offmachine();
                            }
                            stateMachine = StateMachine.CONTROLSPEED;
                            break;




                        case STMC.PROCESS_MANUAL:
                            if (mc1.sw_start_auto == true)
                                stateMc1 = STMC.PROCESS_AUTO_W;
                            if (mc1.btn_on_preminary_pump == false)
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  // 4s
                            stateMc1 = STMC.PROCESS_MANUAL_START;
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            if (mc1.btn_on_low_airpressure == false)
                                stateMc1 = STMC.MANUAL_PRESSURE_PREMINARY_PUMP;
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            if (mc1.btn_on_hig_airpressure == false)
                                stateMc1 = STMC.MANUAL_READY_HIGH_PRESURE;
                            coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;   // 1s
                            coundown_speed_engine = Params.COUNT_SPEED_ENGINE;      //Tang toc dong co
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            if(coundown_preminary_pump == 0)  // 
                            {
                                coundown_speed_engine = Params.COUNT_SPEED_ENGINE;
                            }    
                            stateMc1 = STMC.START_OK;
                            //Khởi động xong thì qua điều khiển tốc độ
                            break;
                    }
                    //Action start///////////////////////////////////////////////////////////////////////////
                    switch (stateMc1)
                    {
                        case STMC.IDLE:
                            break;


                        case STMC.PROCESS_AUTO_W:
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc1.sig_mpa = true;
                            mc1.vl_mainlineoilpressure = (int)((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump) * 0.1);
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc1.sig_vnd = true;
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_count_rotate = !mc1.sig_count_rotate;
                            }
                            if(mc1.sw_start_auto == false)
                            {

                            }    
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            mc1.sig_vnd = false;
                            mc1.sig_count_rotate = false;
                            mc1.sig_vvd = true;
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
                            if (mc2.btn_off_preminary_pump == false)        //Nút nhấn bị lỗi chưa fix. Đang dùng nút off của máy 2.
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
                            mc1.sig_mpa = true;
                            mc1.sig_vvd = true;
                            mc1.sig_vnd = false;
                            mc1.sig_count_rotate = false;
                            mc1.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine));
                            break;
                    }
                    //***********Tương tác độc lập SW
                    //if (stateMachine == StateMachine.MACHINE_ON)
                    {
                        #region btn for one machine
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
                        #endregion
                        #region
                        #endregion
                    }
                }
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    mc1.offmachine();
                    mc2.offmachine();
                    mc3.offmachine();
                    Orionsystem.off_orion();
                }    
                if (stateMachine == StateMachine.CONTROLSPEED)
                {
                    switch (stateMc1)
                    {
                        case STMC.MACHINEUP:
                                mc1.vl_speed_engine = 800;
                            mc1.sig_park = false;
                                mc1.sig_goahead = true;
                            break;
                        case STMC.MACHINEDOWN:
                            break;
                        case STMC.MACHINEHIGHSPEED:
                            break;
                    }
                }    
                await Task.Delay(1);
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
                if (coundown_speed_engine > 0)
                    coundown_speed_engine--;
                // await Task.Delay(100);
                Thread.Sleep(100);
            }
        }

        public void Subcribe()
        {
            Task control = Task.Run(() => loopUpdateActionsStateMachine());  // Khởi chạy loop services
          //  Task timer1s = Task.Run(() => Timer1Second());
            Task stateEachMachine1 = Task.Run(() => parallel1_updateMachine());
        }
    }
}
