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
    class LogicServices
    {
        StateMachine stateMachine;
        public Machine mc1;
        public Machine mc2;
        public Machine mc3;
        public LogicServices(Machine _mc1, Machine _mc2, Machine _mc3)
        {
            mc1 = _mc1;
            mc2 = _mc2;
            mc3 = _mc3;
            stateMachine = StateMachine.MACHINE_OFF;
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
                        else
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
                        stateMachine = StateMachine.READY_HYDRAULICS_PUPM;
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

                        mc1.startauto();
                        mc2.startauto();
                        mc3.startauto();
                        break;
                    case StateMachine.HYDRAULICS_PUMP:
                        Orionsystem.sig_mainno_pressure = true;
                        await Task.Delay(3000);             //Waiting Pump
                        break;
                    case StateMachine.READY_HYDRAULICS_PUPM:
                        Orionsystem.sig_mainhas_pressure = true;
                        Orionsystem.sig_mainno_pressure = false;
                        mc1.startauto();
                        mc2.startauto();
                        mc3.startauto();
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
                await Task.Delay(10);
            }


            //*/

            //for test button
            //while(true)
            //{
            //    Console.WriteLine("Trang thai nut nhan:" + Orionsystem.btn_checklight);
            //    if (Orionsystem.btn_checklight == true)
            //    {
            //        //mc1.on_all_sig();
            //        //mc2.on_all_sig();
            //        //mc3.on_all_sig();
            //        //Orionsystem.on_all_sig_main();

            //        mc1.off_all_sig();
            //        mc2.off_all_sig();
            //        mc3.off_all_sig();
            //        Orionsystem.off_all_sig_main();
            //    }
            //    if (Orionsystem.btn_checklight == false)
            //    {
            //        mc1.on_all_sig();
            //        mc2.on_all_sig();
            //        mc3.on_all_sig();
            //        Orionsystem.on_all_sig_main();


            //        //mc1.off_all_sig();
            //        //mc2.off_all_sig();
            //        //mc3.off_all_sig();
            //        //Orionsystem.off_all_sig_main();
            //    }
            //    await Task.Delay(10);
            //}    
        }

        public void Subcribe()
        {
            Task control = Task.Run(() => loopUpdateActionsStateMachine());  // Khởi chạy loop services
        }
    }
}
