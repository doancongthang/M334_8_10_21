using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using M334_8_10_21;

namespace M334_8_10_21
{
    public class connection
    {
        //public M334_8_10_21.ModbusClient modbusClient;
        ModbusClient modbusClient = new ModbusClient();
        public void deviceconnect(string comport)
        {
            //try
            {
                if (modbusClient.Connected)
                    modbusClient.Disconnect();

                //if (cbbSelctionModbus.SelectedIndex == 0)
                //{

                //    modbusClient.IPAddress = txtIpAddressInput.Text;
                //    modbusClient.Port = int.Parse(txtPortInput.Text);
                //    modbusClient.SerialPort = null;
                //    //modbusClient.receiveDataChanged += new EasyModbus.ModbusClient.ReceiveDataChanged(UpdateReceiveData);
                //    //modbusClient.sendDataChanged += new EasyModbus.ModbusClient.SendDataChanged(UpdateSendData);
                //    //modbusClient.connectedChanged += new EasyModbus.ModbusClient.ConnectedChanged(UpdateConnectedChanged);

                //    modbusClient.Connect();
                //}
                if (1 == 1)
                {
                    modbusClient.SerialPort = comport;
                    //
                    Console.WriteLine("ĐÃ KẾT NỐI");
                    //

                    //modbusClient.UnitIdentifier = 1;      //ID Mobbus
                    modbusClient.UnitIdentifier = 2;        //ID Mobbus
                    //modbusClient.UnitIdentifier = 3;      //ID Mobbus
                    modbusClient.Baudrate = 9600;
                    //if (cbbParity.SelectedIndex == 0)
                    //    modbusClient.Parity = System.IO.Ports.Parity.Even;
                    //if (cbbParity.SelectedIndex == 1)
                    //    modbusClient.Parity = System.IO.Ports.Parity.Odd;
                    //if (cbbParity.SelectedIndex == 2)
                        modbusClient.Parity = System.IO.Ports.Parity.None;

                    //if (cbbStopbits.SelectedIndex == 0)
                    //    modbusClient.StopBits = System.IO.Ports.StopBits.One;
                    //if (cbbStopbits.SelectedIndex == 1)
                    //    modbusClient.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    //if (cbbStopbits.SelectedIndex == 2)
                        modbusClient.StopBits = System.IO.Ports.StopBits.One;

                    modbusClient.Connect();
                }
            }
            //catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, "Unable to connect to Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public void selectid(byte id)
        {
            modbusClient.UnitIdentifier = id;
            bool[] result2 = modbusClient.ReadDiscreteInputs(0, 100);
        }
    }
}
