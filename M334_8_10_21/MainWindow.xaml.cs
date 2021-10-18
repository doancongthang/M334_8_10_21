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

            //modbusClient.Baudrate = 9600;
            //modbusClient.UnitIdentifier = 2;
        }
        void UpdateReceiveData(object sender)
        {

        }
        delegate void UpdateReceiveDataCallback();

        private void button_Click(object sender, RoutedEventArgs e)
        {
            machineleft.startmanual();
            machinemidl.startmanual();
            machineright.startmanual();
        }
    }
}
