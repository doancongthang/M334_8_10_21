using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M334_8_10_21;
using System.Threading;
using M334_8_10_21;

namespace M334_8_10_21.Services
{
    class ModbusServices
    {
        private ModbusClient modbusClient;
        string receiveData = null;
        byte boardId = 1;
        private Machine mc1;
        private Machine mc2;
        private Machine mc3;
        public ModbusServices(Machine _mc1, Machine _mc2, Machine _mc3)
        {
            mc1 = _mc1;
            mc2 = _mc2;
            mc3 = _mc3;
            modbusClient = new M334_8_10_21.ModbusClient();
            modbusClient.SerialPort = "COM15";
            modbusClient.Baudrate = 9600;
            modbusClient.Parity = System.IO.Ports.Parity.None;
            modbusClient.StopBits = System.IO.Ports.StopBits.One;
            modbusClient.UnitIdentifier = 1;
            //modbusClient.ReceiveDataChanged += new ModbusClient.ReceiveDataChangedHandler(UpdateReceiveData);
            //modbusClient.SendDataChanged += new EasyModbus.ModbusClient.SendDataChangedHandler(UpdateSendData);
            //modbusClient.ConnectedChanged += new EasyModbus.ModbusClient.ConnectedChangedHandler(UpdateConnectedChanged);
            //modbusClient.ReceiveDataChanged += new ModbusClient.ReceiveDataChangedHandler(updatedata);
            modbusClient.LogFileFilename = "logFiletxt.txt";

        }
        private async void loopUpdateModbus()
        {
            ///*
            while (true)
            {
                //Console.WriteLine("Update modbus");
                if (modbusClient.Connected)
                {
                    try
                    {
                        switch (boardId)
                        {
                            case 1:
                                modbusClient.UnitIdentifier = 1;
                                bool[] result = modbusClient.ReadDiscreteInputs(0, 30);
                                //                              = result[0];
                                //                              = result[1];
                                //                              = result[2];
                                //                              = result[3];
                                mc1.sw_protect = result[4];
                                mc2.sw_protect = result[5];
                                Orionsystem.btn_wheelhouse = result[6];
                                Orionsystem.SW1 = result[7];
                                mc3.sw_protect = result[8];
                                Orionsystem.btn_callbehindcabin = result[9];
                                Orionsystem.SW2 = result[10];
                                Orionsystem.btn_callheadcabin = result[11];
                                Orionsystem.SW3 = result[12];
                                Orionsystem.rswright = result[13];
                                Orionsystem.SW_power = result[14];
                                mc2.btn_burn = result[15];
                                //                              = result[16];
                                Orionsystem.rswmid = result[17];
                                Orionsystem.sw_oilafterfil = result[18];
                                mc1.btn_burn = result[19];
                                //                              = result[20];
                                //                              = result[21];
                                //                              = result[22];
                                mc1.sw_pumpout = result[23];
                                mc3.sw_zero_fuel_supply = result[24];
                                mc3.btn_on_preminary_pump = result[25];
                                mc1.btn_start = result[26];
                                mc3.btn_down = result[26];
                                mc3.btn_off_preminary_pump = result[28];
                                mc1.btn_on_hig_airpressure = result[29];
                                //                              = result[30];
                                break;
                            case 2:
                                modbusClient.UnitIdentifier = 2;
                                bool[] result2 = modbusClient.ReadDiscreteInputs(0, 30);
                                mc2.btn_estop = result2[0];
                                mc1.sw_zero_fuel_supply = result2[1];
                                mc1.btn_estop = result2[2];
                                mc3.btn_estop = result2[3];
                                mc2.sw_pumpout = result2[4];
                                mc3.sw_pumpout = result2[5];
                                Orionsystem.btn_checklight = result2[6];
                                mc3.btn_up = result2[7];
                                mc2.btn_up = result2[8];
                                mc2.sw_zero_fuel_supply = result2[9];
                                mc1.btn_quickdown = result2[10];
                                mc2.btn_on_hig_airpressure = result2[11];
                                mc1.sw_start_auto = result2[12];
                                mc1.btn_down = result2[13];
                                mc2.sw_start_auto = result2[14];
                                mc3.sw_start_auto = result2[15];
                                mc2.btn_quickdown = result2[16];
                                mc3.btn_quickdown = result2[17];
                                mc2.btn_down = result2[18];
                                mc1.btn_up = result2[19];
                                mc1.btn_on_preminary_pump = result2[20];
                                mc2.btn_on_preminary_pump = result2[21];
                                mc3.btn_start = result2[22];
                                mc1.btn_on_low_airpressure = result2[23];
                                mc2.btn_start = result2[24];
                                mc2.btn_off_preminary_pump = result2[25];
                                mc3.btn_on_hig_airpressure = result2[26];
                                mc2.btn_off_preminary_pump = result2[28];
                                mc3.btn_on_low_airpressure = result2[29];
                                //                              = result2[30];
                                break;
                            case 3:
                                modbusClient.UnitIdentifier = 3;
                                bool[] result3 = modbusClient.ReadDiscreteInputs(0, 30);
                                break;
                        }

                        //Console.WriteLine("Day la doc trang thai");

                    }
                    catch (Exception exc)
                    {
                        //Console.WriteLine("Error read");
                    }
                }
                boardId++;
                if (boardId > 3) boardId = 1;
                await Task.Delay(200);
            }
            //*/
        }

        private async void loopUpdateCoilsModbus()
        {
            ///*
            while (true)
            {
                //Console.WriteLine("Update light status");
                if (modbusClient.Connected)
                {
                    try
                    {
                        switch (boardId)
                        {
                            case 1:
                                modbusClient.UnitIdentifier = 1;
                                bool[] serverResponse1 = modbusClient.ReadCoils(0, 30);

                                modbusClient.WriteSingleCoil(0, mc2.sig_oil_supply);
                                modbusClient.WriteSingleCoil(1, mc2.sig_vvd);
                                modbusClient.WriteSingleCoil(2, mc2.sig_vnd);
                                modbusClient.WriteSingleCoil(3, mc2.sig_mpa);
                                modbusClient.WriteSingleCoil(4, mc1.sig_mpa);
                                modbusClient.WriteSingleCoil(5, mc1.sig_vvd);
                                modbusClient.WriteSingleCoil(6, mc1.sig_vvd);
                                modbusClient.WriteSingleCoil(7, mc1.sig_oil_supply);
                                modbusClient.WriteSingleCoil(8, mc1.sig_upper_oil);
                                modbusClient.WriteSingleCoil(9, mc1.sig_starting_forbidden);
                                modbusClient.WriteSingleCoil(10, mc2.sig_upper_oil);
                                modbusClient.WriteSingleCoil(11, mc2.sig_starting_forbidden);
                                modbusClient.WriteSingleCoil(12, mc2.sig_count_rotate);
                                modbusClient.WriteSingleCoil(13, mc3.sig_count_rotate);
                                modbusClient.WriteSingleCoil(14, mc3.sig_starting_forbidden);
                                modbusClient.WriteSingleCoil(15, mc3.sig_upper_oil);
                                modbusClient.WriteSingleCoil(16, mc3.sig_oil_supply);
                                modbusClient.WriteSingleCoil(17, mc3.sig_vvd);
                                modbusClient.WriteSingleCoil(18, mc3.sig_vvd);
                                modbusClient.WriteSingleCoil(19, mc3.sig_mpa);
                                modbusClient.WriteSingleCoil(20, mc3.sig_pumping_MPA);
                                modbusClient.WriteSingleCoil(21, mc2.sig_pumping_MPA);
                                modbusClient.WriteSingleCoil(22, mc1.sig_pumping_MPA);
                                modbusClient.WriteSingleCoil(23, mc3.sig_oil_no_pump);
                                modbusClient.WriteSingleCoil(24, mc2.sig_oil_no_pump);
                                modbusClient.WriteSingleCoil(25, mc1.sig_oil_no_pump);
                                modbusClient.WriteSingleCoil(26, mc3.sig_pumping_MPA);
                                modbusClient.WriteSingleCoil(27, mc2.sig_pumping_MPA);
                                modbusClient.WriteSingleCoil(28, mc1.sig_pumping_MPA);
                                modbusClient.WriteSingleCoil(29, mc1.sig_count_rotate);
                                break;
                            case 2:
                                modbusClient.UnitIdentifier = 2;
                                bool[] serverResponse2 = modbusClient.ReadCoils(0, 30);
                                modbusClient.WriteSingleCoil(0, Orionsystem.sig_mainKMO);
                                modbusClient.WriteSingleCoil(1, Orionsystem.sig_mainOK);
                                modbusClient.WriteSingleCoil(2, mc3.sig_phi_s3);
                                modbusClient.WriteSingleCoil(3, mc3.sig_phi_s2);
                                modbusClient.WriteSingleCoil(4, mc3.sig_phi_s1);
                                modbusClient.WriteSingleCoil(5, mc3.sig_protect_on);
                                modbusClient.WriteSingleCoil(6, mc3.sig_temperature_water);
                                modbusClient.WriteSingleCoil(7, mc3.sig_pressure_water);
                                modbusClient.WriteSingleCoil(8, mc3.sig_temperature_oil);
                                modbusClient.WriteSingleCoil(9, mc3.sig_pressure_oil);
                                modbusClient.WriteSingleCoil(10, mc1.sig_park);
                                modbusClient.WriteSingleCoil(11, mc3.sig_goahead);
                                modbusClient.WriteSingleCoil(12, mc2.sig_goahead);
                                modbusClient.WriteSingleCoil(13, mc1.sig_goahead);
                                modbusClient.WriteSingleCoil(14, mc3.sig_highspeed);
                                modbusClient.WriteSingleCoil(15, mc2.sig_highspeed);
                                modbusClient.WriteSingleCoil(16, mc1.sig_highspeed);
                                modbusClient.WriteSingleCoil(17, mc3.sig_nopressure);
                                modbusClient.WriteSingleCoil(18, mc2.sig_nopressure);
                                modbusClient.WriteSingleCoil(19, mc1.sig_nopressure);
                                modbusClient.WriteSingleCoil(21, Orionsystem.sig_mainno_pressure);
                                modbusClient.WriteSingleCoil(20, Orionsystem.sig_mainhas_pressure);
                                modbusClient.WriteSingleCoil(22, mc2.sig_phi_s3);
                                modbusClient.WriteSingleCoil(23, mc2.sig_phi_s2);
                                modbusClient.WriteSingleCoil(24, mc2.sig_phi_s1);
                                modbusClient.WriteSingleCoil(25, mc1.sig_protect_on);
                                modbusClient.WriteSingleCoil(26, mc1.sig_temperature_oil);
                                modbusClient.WriteSingleCoil(27, mc1.sig_pressure_water);
                                modbusClient.WriteSingleCoil(28, mc1.sig_temperature_oil);
                                modbusClient.WriteSingleCoil(29, mc1.sig_pressure_oil);
                                break;
                            case 3:
                                modbusClient.UnitIdentifier = 3;
                                bool[] serverResponse3 = modbusClient.ReadCoils(0, 30);
                                modbusClient.WriteSingleCoil(0, Orionsystem.sig_main_hobbyshirt);
                                modbusClient.WriteSingleCoil(1, Orionsystem.sig_mainHMO);
                                modbusClient.WriteSingleCoil(2, mc2.sig_pressure_oil);
                                modbusClient.WriteSingleCoil(3, mc2.sig_temperature_oil);
                                modbusClient.WriteSingleCoil(4, mc2.sig_pressure_water);
                                modbusClient.WriteSingleCoil(5, mc2.sig_temperature_water);
                                modbusClient.WriteSingleCoil(6, mc2.sig_protect_on);
                                modbusClient.WriteSingleCoil(7, mc2.sig_phi_s1);
                                modbusClient.WriteSingleCoil(8, mc2.sig_phi_s2);
                                modbusClient.WriteSingleCoil(9, mc2.sig_phi_s3);
                                //modbusClient.WriteSingleCoil(10, );        //Undefine device
                                //modbusClient.WriteSingleCoil(11, );        //Undefine device
                                //modbusClient.WriteSingleCoil(12, );        //Undefine device
                                //modbusClient.WriteSingleCoil(13, );        //Undefine device
                                //modbusClient.WriteSingleCoil(14, );        //Undefine device
                                //modbusClient.WriteSingleCoil(15, );        //Undefine device
                                //modbusClient.WriteSingleCoil(16, );        //Undefine device
                                //modbusClient.WriteSingleCoil(17, );        //Undefine device
                                //modbusClient.WriteSingleCoil(18, );        //Undefine device
                                //modbusClient.WriteSingleCoil(19, );        //Undefine device
                                //modbusClient.WriteSingleCoil(20, );        //Undefine device
                                //modbusClient.WriteSingleCoil(21, );        //Undefine device
                                //modbusClient.WriteSingleCoil(22, );        //Undefine device
                                modbusClient.WriteSingleCoil(23, Orionsystem.sig_main_pump);
                                modbusClient.WriteSingleCoil(24, Orionsystem.sig_remote_pump);
                                modbusClient.WriteSingleCoil(25, mc2.sig_park);
                                modbusClient.WriteSingleCoil(26, mc3.sig_park);
                                modbusClient.WriteSingleCoil(27, mc1.sig_gobehind);
                                modbusClient.WriteSingleCoil(28, mc2.sig_gobehind);
                                modbusClient.WriteSingleCoil(29, mc3.sig_gobehind);
                                break;

                            default:
                                break;
                        }
                    }
                    catch (Exception exc)
                    {
                        //Console.WriteLine("Error write coils");
                    }
                }

                boardId++;
                if (boardId > 3) boardId = 1;
                await Task.Delay(200);
            }
            //*/
        }

        public void UpdateReceiveData(object sender)
        {
            receiveData = "Rx: " + BitConverter.ToString(modbusClient.receiveData).Replace("-", " ");
            //Console.WriteLine("Nhận dữ liệu");
        }

        public void Subcribe()
        {
            //Task control = Task.Run(() => loopUpdateModbus());              // Khởi chạy loop services
            //Task controlLight = Task.Run(() => loopUpdateCoilsModbus());    // Khởi chạy loop services
            //Task blinktask = new Task(delegate { blink(); });
            //blinktask.Start();
            //Task bl = Task.Run(() => blink());
            Task control1 = Task.Run(() => updatedata());              // Khởi chạy loop services
        }
        public async void Connect()
        {
            try
            {
                if (modbusClient.Connected)
                {
                    modbusClient.Disconnect();
                }
                modbusClient.Connect();
            }
            catch (Exception exc)
            {
                //Console.WriteLine("Unable to connect to Server");
            }
        }
        public async void blink()
        {
            //Task blinktask = new Task(delegate { blink(); });
            //blinktask.Start();
            while (true)
            {
                switch (modbusClient.UnitIdentifier)
                {
                    case 1:
                        on_all_larm();
                        //Thread.Sleep(100);
                        await Task.Delay(100);
                        break;
                    case 2:
                        on_all_larm();
                        //Thread.Sleep(100);
                        await Task.Delay(100);
                        break;
                    case 3:
                        on_all_larm();
                        //Thread.Sleep(100);
                        await Task.Delay(100);
                        break;
                        //Thread.Sleep(100);
                        await Task.Delay(200);
                }
                switch (modbusClient.UnitIdentifier)
                {
                    case 1:
                        off_all_larm();
                        //Thread.Sleep(100);
                        await Task.Delay(100);
                        break;
                    case 2:
                        off_all_larm();
                        //Thread.Sleep(100);
                        await Task.Delay(100);
                        break;
                    case 3:
                        off_all_larm();
                        //Thread.Sleep(100);
                        await Task.Delay(100);
                        break;
                        //Thread.Sleep(100);
                        await Task.Delay(200);
                }
                modbusClient.UnitIdentifier++;
                if (modbusClient.UnitIdentifier > 3) modbusClient.UnitIdentifier = 1;

                for (modbusClient.UnitIdentifier = 1; modbusClient.UnitIdentifier <= 3; modbusClient.UnitIdentifier++)
                {
                    off_all_larm();
                    //Thread.Sleep(100);
                    await Task.Delay(200);
                }
                for (modbusClient.UnitIdentifier = 1; modbusClient.UnitIdentifier <= 3; modbusClient.UnitIdentifier++)
                {
                    on_all_larm();
                    //Thread.Sleep(100);
                    await Task.Delay(200);
                }
            }
        }
        public async void updatedata( )
        {
            while (true)
            {
                //Console.WriteLine("Update light status");
                if (modbusClient.Connected)
                {
                    try
                    {
                        switch (boardId)
                        {
                            case 1:
                                modbusClient.UnitIdentifier = 1;
                                bool[] result = modbusClient.ReadDiscreteInputs(0, 30);
                                //                              = result[0];
                                //                              = result[1];
                                //                              = result[2];
                                //                              = result[3];
                                mc1.sw_protect = result[4];
                                mc2.sw_protect = result[5];
                                Orionsystem.btn_wheelhouse = result[6];
                                Orionsystem.SW1 = result[7];
                                mc3.sw_protect = result[8];
                                Orionsystem.btn_callbehindcabin = result[9];
                                Orionsystem.SW2 = result[10];
                                Orionsystem.btn_callheadcabin = result[11];
                                Orionsystem.SW3 = result[12];
                                Orionsystem.rswright = result[13];
                                Orionsystem.SW_power = result[14];
                                mc2.btn_burn = result[15];
                                //                              = result[16];
                                Orionsystem.rswmid = result[17];
                                Orionsystem.sw_oilafterfil = result[18];
                                mc1.btn_burn = result[19];
                                //                              = result[20];
                                //                              = result[21];
                                //                              = result[22];
                                mc1.sw_pumpout = result[23];
                                mc3.sw_zero_fuel_supply = result[24];
                                mc3.btn_on_preminary_pump = result[25];
                                mc1.btn_start = result[26];
                                mc3.btn_down = result[27];
                                mc3.btn_off_preminary_pump = result[28];
                                mc1.btn_on_hig_airpressure = result[29];
                                //                              = result[30];
                                //**********************************************************************************************//
                                //modbusClient.WriteSingleCoil(0, mc2.sig_oil_supply);
                                //modbusClient.WriteSingleCoil(1, mc2.sig_vvd);
                                //modbusClient.WriteSingleCoil(2, mc2.sig_vnd);
                                //modbusClient.WriteSingleCoil(3, mc2.sig_mpa);
                                //modbusClient.WriteSingleCoil(4, mc1.sig_mpa);
                                //modbusClient.WriteSingleCoil(5, mc1.sig_vvd);
                                //modbusClient.WriteSingleCoil(6, mc1.sig_vvd);
                                //modbusClient.WriteSingleCoil(7, mc1.sig_oil_supply);
                                //modbusClient.WriteSingleCoil(8, mc1.sig_upper_oil);
                                //modbusClient.WriteSingleCoil(9, mc1.sig_starting_forbidden);
                                //modbusClient.WriteSingleCoil(10, mc2.sig_upper_oil);
                                //modbusClient.WriteSingleCoil(11, mc2.sig_starting_forbidden);
                                //modbusClient.WriteSingleCoil(12, mc2.sig_count_rotate);
                                //modbusClient.WriteSingleCoil(13, mc3.sig_count_rotate);
                                //modbusClient.WriteSingleCoil(14, mc3.sig_starting_forbidden);
                                //modbusClient.WriteSingleCoil(15, mc3.sig_upper_oil);
                                //modbusClient.WriteSingleCoil(16, mc3.sig_oil_supply);
                                //modbusClient.WriteSingleCoil(17, mc3.sig_vvd);
                                //modbusClient.WriteSingleCoil(18, mc3.sig_vvd);
                                //modbusClient.WriteSingleCoil(19, mc3.sig_mpa);
                                //modbusClient.WriteSingleCoil(20, mc3.sig_pumping_MPA);
                                //modbusClient.WriteSingleCoil(21, mc2.sig_pumping_MPA);
                                //modbusClient.WriteSingleCoil(22, mc1.sig_pumping_MPA);
                                //modbusClient.WriteSingleCoil(23, mc3.sig_oil_no_pump);
                                //modbusClient.WriteSingleCoil(24, mc2.sig_oil_no_pump);
                                //modbusClient.WriteSingleCoil(25, mc1.sig_oil_no_pump);
                                //modbusClient.WriteSingleCoil(26, mc3.sig_pumping_MPA);
                                //modbusClient.WriteSingleCoil(27, mc2.sig_pumping_MPA);
                                //modbusClient.WriteSingleCoil(28, mc1.sig_pumping_MPA);
                                //modbusClient.WriteSingleCoil(29, mc1.sig_count_rotate);
                                //**********************************************************************************************//
                                bool[] coilsToSend1 = new bool[30];
                                coilsToSend1[0] = mc2.sig_oil_supply;
                                coilsToSend1[1] = mc2.sig_vvd;
                                coilsToSend1[2] = mc2.sig_vnd;
                                coilsToSend1[3] = mc2.sig_mpa;
                                coilsToSend1[4] = mc1.sig_mpa;
                                coilsToSend1[5] = mc1.sig_vnd;
                                coilsToSend1[6] = mc1.sig_vvd;
                                coilsToSend1[7] = mc1.sig_oil_supply;
                                coilsToSend1[8] = mc1.sig_upper_oil;
                                coilsToSend1[9] = mc1.sig_starting_forbidden;
                                coilsToSend1[10] = mc2.sig_upper_oil;
                                coilsToSend1[11] = mc2.sig_starting_forbidden;
                                coilsToSend1[12] = mc2.sig_count_rotate;
                                coilsToSend1[13] = mc3.sig_count_rotate;
                                coilsToSend1[14] = mc3.sig_starting_forbidden;
                                coilsToSend1[15] = mc3.sig_upper_oil;
                                coilsToSend1[16] = mc3.sig_oil_supply;
                                coilsToSend1[17] = mc3.sig_vvd;
                                coilsToSend1[18] = mc3.sig_vnd;
                                coilsToSend1[19] = mc3.sig_mpa;
                                coilsToSend1[20] = mc3.sig_pumping_MPA;
                                coilsToSend1[21] = mc2.sig_pumping_MPA;
                                coilsToSend1[22] = mc1.sig_pumping_MPA;
                                coilsToSend1[23] = mc3.sig_oil_no_pump;
                                coilsToSend1[24] = mc2.sig_oil_no_pump;
                                coilsToSend1[25] = mc1.sig_oil_no_pump;
                                coilsToSend1[26] = mc3.sig_otka;
                                coilsToSend1[27] = mc2.sig_otka;
                                coilsToSend1[28] = mc1.sig_otka;
                                coilsToSend1[29] = mc1.sig_count_rotate;
                                modbusClient.WriteMultipleCoils(0, coilsToSend1);
                                int[] gausdwin8inch = new int[6];
                                gausdwin8inch[0] = Orionsystem.vl_temperature_oil_out;
                                gausdwin8inch[1] = Orionsystem.vl_temperature_oil_in;
                                gausdwin8inch[2] = Orionsystem.vl_pressureptk;
                                gausdwin8inch[3] = Orionsystem.vl_reverse_air_pressure;
                                gausdwin8inch[4] = Orionsystem.vl_pressurefuel;
                                gausdwin8inch[5] = Orionsystem.vl_hydraulicpressure_gidravl;
                                modbusClient.WriteMultipleRegisters(0, gausdwin8inch);
                                break;
                            case 2:
                                modbusClient.UnitIdentifier = 2;
                                bool[] result2 = modbusClient.ReadDiscreteInputs(0, 30);
                                mc2.btn_estop = result2[0];
                                mc1.sw_zero_fuel_supply = result2[1];
                                mc1.btn_estop = result2[2];
                                mc3.btn_estop = result2[3];
                                mc2.sw_pumpout = result2[4];
                                mc3.sw_pumpout = result2[5];
                                Orionsystem.btn_checklight = result2[6];
                                mc3.btn_up = result2[7];
                                mc2.btn_up = result2[8];
                                mc2.sw_zero_fuel_supply = result2[9];
                                mc1.btn_quickdown = result2[10];
                                mc2.btn_on_hig_airpressure = result2[11];
                                mc1.sw_start_auto = result2[12];
                                mc1.btn_down = result2[13];
                                mc2.sw_start_auto = result2[14];
                                mc3.sw_start_auto = result2[15];
                                mc2.btn_quickdown = result2[16];
                                mc3.btn_quickdown = result2[17];
                                mc2.btn_down = result2[18];
                                mc1.btn_up = result2[19];
                                mc1.btn_on_preminary_pump = result2[20];
                                mc2.btn_on_preminary_pump = result2[21];
                                mc3.btn_start = result2[22];
                                mc1.btn_on_low_airpressure = result2[23];
                                mc2.btn_start = result2[24];
                                mc1.btn_off_preminary_pump = result2[25];
                                mc3.btn_on_hig_airpressure = result2[26];
                                mc2.btn_on_low_airpressure = result2[27];
                                mc2.btn_off_preminary_pump = result2[28];
                                mc3.btn_on_low_airpressure = result2[29];
                                //                              = result2[30];
                                //**********************************************************************************************//
                                //modbusClient.WriteSingleCoil(0, Orionsystem.sig_mainKMO);
                                //modbusClient.WriteSingleCoil(1, Orionsystem.sig_mainOK);
                                //modbusClient.WriteSingleCoil(2, mc3.sig_phi_s3);
                                //modbusClient.WriteSingleCoil(3, mc3.sig_phi_s2);
                                //modbusClient.WriteSingleCoil(4, mc3.sig_phi_s1);
                                //modbusClient.WriteSingleCoil(5, mc3.sig_protect_on);
                                //modbusClient.WriteSingleCoil(6, mc3.sig_temperature_water);
                                //modbusClient.WriteSingleCoil(7, mc3.sig_pressure_water);
                                //modbusClient.WriteSingleCoil(8, mc3.sig_temperature_oil);
                                //modbusClient.WriteSingleCoil(9, mc3.sig_pressure_oil);
                                //modbusClient.WriteSingleCoil(10, mc1.sig_park);
                                //modbusClient.WriteSingleCoil(11, mc3.sig_goahead);
                                //modbusClient.WriteSingleCoil(12, mc2.sig_goahead);
                                //modbusClient.WriteSingleCoil(13, mc1.sig_goahead);
                                //modbusClient.WriteSingleCoil(14, mc3.sig_highspeed);
                                //modbusClient.WriteSingleCoil(15, mc2.sig_highspeed);
                                //modbusClient.WriteSingleCoil(16, mc1.sig_highspeed);
                                //modbusClient.WriteSingleCoil(17, mc3.sig_nopressure);
                                //modbusClient.WriteSingleCoil(18, mc2.sig_nopressure);
                                //modbusClient.WriteSingleCoil(19, mc1.sig_nopressure);
                                //modbusClient.WriteSingleCoil(21, Orionsystem.sig_mainno_pressure);
                                //modbusClient.WriteSingleCoil(20, Orionsystem.sig_mainhas_pressure);
                                //modbusClient.WriteSingleCoil(22, mc2.sig_phi_s3);
                                //modbusClient.WriteSingleCoil(23, mc2.sig_phi_s2);
                                //modbusClient.WriteSingleCoil(24, mc2.sig_phi_s1);
                                //modbusClient.WriteSingleCoil(25, mc1.sig_protect_on);
                                //modbusClient.WriteSingleCoil(26, mc1.sig_temperature_oil);
                                //modbusClient.WriteSingleCoil(27, mc1.sig_pressure_water);
                                //modbusClient.WriteSingleCoil(28, mc1.sig_temperature_oil);
                                //modbusClient.WriteSingleCoil(29, mc1.sig_pressure_oil);
                                //**********************************************************************************************//
                                bool[] coilsToSend2 = new bool[30];
                                coilsToSend2[0] = Orionsystem.sig_mainKMO;
                                coilsToSend2[1] = Orionsystem.sig_mainOK;
                                coilsToSend2[2] = mc3.sig_phi_s3;
                                coilsToSend2[3] = mc3.sig_phi_s2;
                                coilsToSend2[4] = mc3.sig_phi_s1;
                                coilsToSend2[5] = mc3.sig_protect_on;
                                coilsToSend2[6] = mc3.sig_temperature_water;
                                coilsToSend2[7] = mc3.sig_pressure_water;
                                coilsToSend2[8] = mc3.sig_temperature_oil;
                                coilsToSend2[9] = mc3.sig_pressure_oil;
                                coilsToSend2[10] = mc1.sig_park;
                                coilsToSend2[11] = mc3.sig_goahead;
                                coilsToSend2[12] = mc2.sig_goahead;
                                coilsToSend2[13] = mc1.sig_goahead;
                                coilsToSend2[14] = mc3.sig_highspeed;
                                coilsToSend2[15] = mc2.sig_highspeed;
                                coilsToSend2[16] = mc1.sig_highspeed;
                                coilsToSend2[17] = mc3.sig_nopressure;
                                coilsToSend2[18] = mc2.sig_nopressure;
                                coilsToSend2[19] = mc1.sig_nopressure;
                                coilsToSend2[20] = Orionsystem.sig_mainhas_pressure;        //2 Trạng thái này đang ngược nhau
                                coilsToSend2[21] = Orionsystem.sig_mainno_pressure;         //2 Trạng thái này đang ngược nhau
                                coilsToSend2[22] = mc2.sig_phi_s3;
                                coilsToSend2[23] = mc2.sig_phi_s2;
                                coilsToSend2[24] = mc2.sig_phi_s1;
                                coilsToSend2[25] = mc1.sig_protect_on;
                                coilsToSend2[26] = mc1.sig_temperature_oil;
                                coilsToSend2[27] = mc1.sig_pressure_water;
                                coilsToSend2[28] = mc1.sig_temperature_oil;
                                coilsToSend2[29] = mc1.sig_pressure_oil;
                                modbusClient.WriteMultipleCoils(0, coilsToSend2);
                                int[] gausdwin7inch = new int[6];
                                gausdwin7inch[0] = Orionsystem.vl_time_hours;
                                gausdwin7inch[1] = Orionsystem.vl_time_minute;
                                gausdwin7inch[2] = Orionsystem.vl_time_second;
                                gausdwin7inch[3] = Orionsystem.vl_time_month;
                                gausdwin7inch[4] = Orionsystem.vl_hydraulics;
                                gausdwin7inch[5] = 20;
                                modbusClient.WriteMultipleRegisters(0, gausdwin7inch);
                                break;
                            case 3:
                                modbusClient.UnitIdentifier = 3;
                                bool[] result3 = modbusClient.ReadDiscreteInputs(0, 30);
                                //**********************************************************************************************//
                                //modbusClient.WriteSingleCoil(0, Orionsystem.sig_main_hobbyshirt);
                                //modbusClient.WriteSingleCoil(1, Orionsystem.sig_mainHMO);
                                //modbusClient.WriteSingleCoil(2, mc2.sig_pressure_oil);
                                //modbusClient.WriteSingleCoil(3, mc2.sig_temperature_oil);
                                //modbusClient.WriteSingleCoil(4, mc2.sig_pressure_water);
                                //modbusClient.WriteSingleCoil(5, mc2.sig_temperature_water);
                                //modbusClient.WriteSingleCoil(6, mc2.sig_protect_on);
                                //modbusClient.WriteSingleCoil(7, mc2.sig_phi_s1);
                                //modbusClient.WriteSingleCoil(8, mc2.sig_phi_s2);
                                //modbusClient.WriteSingleCoil(9, mc2.sig_phi_s3);
                                ////modbusClient.WriteSingleCoil(10, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(11, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(12, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(13, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(14, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(15, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(16, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(17, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(18, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(19, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(20, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(21, );        //Undefine device
                                ////modbusClient.WriteSingleCoil(22, );        //Undefine device
                                //modbusClient.WriteSingleCoil(23, Orionsystem.sig_main_pump);
                                //modbusClient.WriteSingleCoil(24, Orionsystem.sig_remote_pump);
                                //modbusClient.WriteSingleCoil(25, mc2.sig_park);
                                //modbusClient.WriteSingleCoil(26, mc3.sig_park);
                                //modbusClient.WriteSingleCoil(27, mc1.sig_gobehind);
                                //modbusClient.WriteSingleCoil(28, mc2.sig_gobehind);
                                //modbusClient.WriteSingleCoil(29, mc3.sig_gobehind);
                                //**********************************************************************************************//
                                bool[] coilsToSend3 = new bool[30];
                                coilsToSend3[0] = Orionsystem.sig_main_hobbyshirt;
                                coilsToSend3[1] = Orionsystem.sig_mainHMO;
                                coilsToSend3[2] = mc2.sig_pressure_oil;
                                coilsToSend3[3] = mc2.sig_temperature_oil;
                                coilsToSend3[4] = mc2.sig_pressure_water;
                                coilsToSend3[5] = mc2.sig_temperature_water;
                                coilsToSend3[6] = mc2.sig_protect_on;
                                coilsToSend3[7] = mc2.sig_phi_s1;
                                coilsToSend3[8] = mc2.sig_phi_s2;
                                coilsToSend3[9] = mc2.sig_phi_s3;
                                //coilsToSend3[10] = , );        //Undefine device
                                //coilsToSend3[11] = , );        //Undefine device
                                //coilsToSend3[12] = , );        //Undefine device
                                coilsToSend3[13] = mc3.sig_count_rotate;        //Undefine device
                                //coilsToSend3[14] = , );        //Undefine device
                                //coilsToSend3[15] = , );        //Undefine device
                                //coilsToSend3[16] = , );        //Undefine device
                                //coilsToSend3[17] = , );        //Undefine device
                                //coilsToSend3[18] = , );        //Undefine device
                                //coilsToSend3[19] = , );        //Undefine device
                                //coilsToSend3[20] = , );        //Undefine device
                                //coilsToSend3[21] = , );        //Undefine device
                                //coilsToSend3[22] = , );        //Undefine device
                                coilsToSend3[23] = Orionsystem.sig_main_pump;
                                coilsToSend3[24] = Orionsystem.sig_remote_pump;
                                coilsToSend3[25] = mc2.sig_park;
                                coilsToSend3[26] = mc3.sig_park;
                                coilsToSend3[27] = mc1.sig_gobehind;
                                coilsToSend3[28] = mc2.sig_gobehind;
                                coilsToSend3[29] = mc3.sig_gobehind;
                                modbusClient.WriteMultipleCoils(0, coilsToSend3);
                                break;
                        }
                    }
                    catch (Exception exc)
                    {
                        //Console.WriteLine("Error write coils");
                    }
                }
                boardId++;
                if (boardId > 3) boardId = 1;
                await Task.Delay(1);
            }
        }
        public void on_all_larm()
        {
            try
            {
                if (!modbusClient.Connected)
                {

                }
                bool[] coilsToSend = new bool[30];
                for (int i = 0; i < 30; i++)
                {
                    coilsToSend[i] = true;
                }
                modbusClient.WriteMultipleCoils(0, coilsToSend);
                //modbusClient.WriteSingleCoil(0, true);
            }
            catch (Exception exc)
            {

            }
        }
        public void off_all_larm()
        {
            try
            {
                if (!modbusClient.Connected)
                {
                }
                bool[] coilsToSend = new bool[30];
                for (int i = 0; i < 30; i++)
                {
                    coilsToSend[i] = false;
                }
                modbusClient.WriteMultipleCoils(0, coilsToSend);
                //modbusClient.WriteSingleCoil(0, true);
                //bool[] result2 = modbusClient.ReadDiscreteInputs(0, 100);

            }
            catch (Exception exc)
            {

            }
        }
    }
}

