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

        Machine machineleft = new Machine();
        Machine machinemidl = new Machine();
        Machine machineright = new Machine();
        public MainWindow()
        {
            InitializeComponent();
            modbusClient = new ModbusRTU.ModbusClient();
            modbusClient.ReceiveDataChanged += new ModbusRTU.ModbusClient.ReceiveDataChangedHandler(UpdateReceiveData);
            //modbusClient.SendDataChanged += new ModbusRTU.ModbusClient.SendDataChangedHandler(UpdateSendData);
            //modbusClient.ConnectedChanged += new ModbusRTU.ModbusClient.ConnectedChangedHandler(UpdateConnectedChanged);
            modbusClient.LogFileFilename = "logFiletxt.txt";

            modbusClient.Baudrate = 9600;
            modbusClient.UnitIdentifier = 2;
        }
        void UpdateReceiveData(object sender)
        {

        }
        delegate void UpdateReceiveDataCallback();

        private void btDisconnect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btConnect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btFC02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!modbusClient.Connected)
                {
                    btConnect_Click(null, null);
                }
                bool[] serverResponse = modbusClient.ReadDiscreteInputs(int.Parse(txtStartingAddressInput.Text) - 1, int.Parse(txtNumberOfValuesInput.Text));
                lsbAnswerFromServer.Items.Clear();
                for (int i = 0; i < serverResponse.Length; i++)
                {
                    lsbAnswerFromServer.Items.Add(serverResponse[i]);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Exception Reading values from Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btFC05_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btFC15_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
