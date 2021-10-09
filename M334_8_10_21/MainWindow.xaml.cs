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
using ModbusRTU;

namespace M334_8_10_21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ModbusRTU.ModbusClient modbusClient;
        public MainWindow()
        {
            InitializeComponent();
            modbusClient = new ModbusRTU.ModbusClient();
            modbusClient.ReceiveDataChanged += new ModbusRTU.ModbusClient.ReceiveDataChangedHandler(UpdateReceiveData);
            modbusClient.SendDataChanged += new ModbusRTU.ModbusClient.SendDataChangedHandler(UpdateSendData);
            modbusClient.ConnectedChanged += new ModbusRTU.ModbusClient.ConnectedChangedHandler(UpdateConnectedChanged);
            modbusClient.LogFileFilename = "logFiletxt.txt";

            //modbusClient.Baudrate = 9600;
            //modbusClient.UnitIdentifier = 2;
        }
        void UpdateReceiveData(object sender)
        {
            receiveData = "Rx: " + BitConverter.ToString(modbusClient.receiveData).Replace("-", " ") + System.Environment.NewLine;
            Thread thread = new Thread(updateReceiveTextBox);
            thread.Start();
        }
        delegate void UpdateReceiveDataCallback();

        private void button_Click(object sender, RoutedEventArgs e)
        {
            cbbSelectComPort.SelectedIndex = 0;
            cbbParity.SelectedIndex = 0;
            cbbStopbits.SelectedIndex = 0;
            if (cbbSelectComPort.SelectedText == "")
                cbbSelectComPort.SelectedItem.ToString();
            txtIpAddress.Visible = false;
            txtIpAddressInput.Visible = false;
            txtPort.Visible = false;sx
            txtPortInput.Visible = false;
            txtCOMPort.Visible = true;
            cbbSelectComPort.Visible = true;
            txtSlaveAddress.Visible = true;
            txtSlaveAddressInput.Visible = true;
            lblBaudrate.Visible = true;
            lblParity.Visible = true;
            lblStopbits.Visible = true;
            txtBaudrate.Visible = true;
            cbbParity.Visible = true;
            cbbStopbits.Visible = true;
        }
    }
}
