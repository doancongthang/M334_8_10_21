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
        private Machine mc1;
        private Machine mc2;
        private Machine mc3;
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
            //while (true)
            //{
            //    Console.WriteLine(stateMachine);
            //    Console.WriteLine(Orionsystem.SW_power);
            //    switch (stateMachine)  // KIEM TRA CAC DIEU KIEN
            //    {
            //        case StateMachine.MACHINE_OFF:
            //            if (Orionsystem.SW_power == true)
            //            {
            //                stateMachine = StateMachine.MACHINE_ON;
            //            }
            //            break;
            //        case StateMachine.MACHINE_ON:
            //            if (Orionsystem.btn_checklight == false)
            //                stateMachine = StateMachine.TEST;
            //            break;
            //        //case StateMachine.HYDRAULICS_PUMP:
            //        //    //await Task.Delay(5000);             //Waiting Pump
            //        //    stateMachine = StateMachine.READY_HYDRAULICS_PUPM;
            //        //    break;
            //        //case StateMachine.READY_HYDRAULICS_PUPM:
            //        //    stateMachine = StateMachine.MACHINE_ON;
            //        //    break;
            //        //case StateMachine.W_OFFTEST:

            //        //    break;
            //    }
            //    switch (stateMachine)  // ACTIONS
            //    {
            //        case StateMachine.MACHINE_OFF:
            //            mc1.offmachine();
            //            mc2.offmachine();
            //            mc3.offmachine();
            //            Orionsystem.off_orion();
            //            break;
            //        //case StateMachine.MACHINE_ON:
            //        //    //stateMachine = StateMachine.MACHINE_OFF;
            //        //    break;
            //        //case StateMachine.HYDRAULICS_PUMP:

            //        //    break;
            //        //case StateMachine.READY_HYDRAULICS_PUPM:

            //        //    break;
            //        case StateMachine.W_OFFTEST:
            //            mc1.off_all_sig();
            //            mc2.off_all_sig();
            //            mc3.off_all_sig();
            //            Orionsystem.off_all_sig_main();
            //            if (Orionsystem.btn_checklight == false)
            //            {
            //                stateMachine = StateMachine.TEST;
            //            }
            //            break;
            //        case StateMachine.TEST:
            //            mc1.on_all_sig();
            //            mc2.on_all_sig();
            //            mc3.on_all_sig();
            //            Orionsystem.on_all_sig_main();
            //            if(Orionsystem.btn_checklight == true)
            //            {
            //                stateMachine = StateMachine.W_OFFTEST;
            //            }    
            //            break;
            //    }
            //    await Task.Delay(100);
            //}
            //*/
            while(true)
            {
                Console.WriteLine("Trang thai nut nhan:" + Orionsystem.btn_checklight);
                await Task.Delay(100);
                if (Orionsystem.btn_checklight == false)
                {
                    mc1.on_all_sig();
                    mc2.on_all_sig();
                    mc3.on_all_sig();
                    Orionsystem.on_all_sig_main();
                }
            }    
        }

        public void Subcribe()
        {
            Task control = Task.Run(() => loopUpdateActionsStateMachine());  // Khởi chạy loop services
        }
    }
}
