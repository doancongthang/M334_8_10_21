using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        TEST
    }

    enum STMC
    {
        IDLE,
        PROCESS_AUTO_START,
        PROCESS_MANUAL
    }

    class LogicServices
    {
        StateMachine stateMachine;
        STMC stateMc1, stateMc2, stateMc3;
        public Machine mc1;
        public Machine mc2;
        public Machine mc3;

        public int countdown_hydraulics_pump = 0;
        public int sec = 0;

        public LogicServices(Machine _mc1, Machine _mc2, Machine _mc3)
        {
            mc1 = _mc1;
            mc2 = _mc2;
            mc3 = _mc3;
            stateMachine = StateMachine.MACHINE_OFF;
            stateMc1 = STMC.IDLE;
            stateMc2 = STMC.IDLE;
            stateMc3 = STMC.IDLE;
        }

        private async void loopUpdateActionsStateMachine()
        {
            ///*
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
                        if (Orionsystem.btn_checklight == false)
                            stateMachine = StateMachine.TEST;
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
                        //if (Orionsystem.btn_checklight == true)
                        //{
                        //    stateMachine = StateMachine.HYDRAULICS_PUMP;
                        //}
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
                        mc1.sig_park = true;
                        mc2.sig_park = true;
                        mc3.sig_park = true;
                        break;
                    case StateMachine.HYDRAULICS_PUMP:
                        Orionsystem.sig_mainno_pressure = true;
                        Orionsystem.vl_hydraulics = (int)((Params.COUNT_HYDRAULICS_PUMP - countdown_hydraulics_pump) * 0.3);
                        break;
                    case StateMachine.READY_HYDRAULICS_PUPM:
                        Orionsystem.sig_mainhas_pressure = true;
                        Orionsystem.sig_mainno_pressure = false;
                        break;
                    case StateMachine.W_OFFTEST:
                        Orionsystem.sig_mainhas_pressure = true;
                        mc1.startauto();
                        mc2.startauto();
                        mc3.startauto();

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
                    switch (stateMc1)
                    {
                        case STMC.IDLE:
                            if (mc1.sw_start_auto == true && mc1.btn_start == false)
                                stateMc1 = STMC.PROCESS_AUTO_START;
                            if (mc1.sw_start_auto == false)
                                stateMc1 = STMC.PROCESS_MANUAL;
                            break;
                        case STMC.PROCESS_MANUAL:
                            if (mc1.sw_start_auto == true)
                                stateMc1 = STMC.PROCESS_AUTO_START;

                            break;
                        case STMC.PROCESS_AUTO_START:
                                mc1.sig_mpa = true;
                                sec = 10;
                                mc1.sig_vvd = true;
                                sec = 10;
                                mc1.sig_vnd = true;
                                sec = 10;
                                mc1.sig_count_rotate = true;
                            break;
                    }
                }
                await Task.Delay(1);
            }
        }
        private async void Timer1Second()
        {
            while (true)
            {
                if (countdown_hydraulics_pump > 0)
                    countdown_hydraulics_pump--;
                await Task.Delay(100);
            }
        }
        private async void TimerSecond()
        {
            while (true)
            {
                if (sec > 0)
                    sec--;
                await Task.Delay(100);
            }
        }
        public void Subcribe()
        {
            Task control = Task.Run(() => loopUpdateActionsStateMachine());  // Khởi chạy loop services
            Task timer1s = Task.Run(() => Timer1Second());
            Task timer2 = Task.Run(() => TimerSecond());
            Task stateEachMachine1 = Task.Run(() => parallel1_updateMachine());
        }
    }
}
