using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using M334_8_10_21;

namespace M334_8_10_21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        //public M334_8_10_21.ModbusClient modbusClient;
        public delegate void ThreadStart();

        Machine machineleft = new Machine();
        Machine machinemidl = new Machine();
        Machine machineright = new Machine();
        connection ketnoi = new connection();
        connection ketnoi2 = new connection();
        connection ketnoi3 = new connection();
        public MainWindow()
        {
            InitializeComponent();

            //modbusClient = new M334_8_10_21.ModbusClient();
            //modbusClient.ReceiveDataChanged += new M334_8_10_21.ModbusClient.ReceiveDataChangedHandler(UpdateReceiveData);
            //modbusClient.SendDataChanged += new M334_8_10_21.ModbusClient.SendDataChangedHandler(UpdateSendData);
            //modbusClient.ConnectedChanged += new M334_8_10_21.ModbusClient.ConnectedChangedHandler(UpdateConnectedChanged);
            //modbusClient.LogFileFilename = "logFiletxt.txt";
            //connection();

            ketnoi.deviceconnect();
            while (true)
            {
                //ketnoi.selectid(1);
                //ketnoi.on_all_larm();
                //Thread.Sleep(500);
                //ketnoi.off_all_larm();
                //Thread.Sleep(500);


                //ketnoi.selectid(2);
                //ketnoi.on_all_larm();
                //Thread.Sleep(500);
                //ketnoi.off_all_larm();
                //Thread.Sleep(500);

                //ketnoi.selectid(3);
                //ketnoi.on_all_larm();
                //Thread.Sleep(500);
                //ketnoi.off_all_larm();
                //Thread.Sleep(500);

                ketnoi.selectid(1);
                ketnoi.on_all_larm();
                ketnoi.selectid(2);
                ketnoi.on_all_larm();
                ketnoi.selectid(3);
                ketnoi.on_all_larm();
                Thread.Sleep(1000);

                ketnoi.selectid(1);
                ketnoi.off_all_larm();
                ketnoi.selectid(2);
                ketnoi.off_all_larm();
                ketnoi.selectid(3);
                ketnoi.off_all_larm();
                Thread.Sleep(1000);

            }


            bool a = true;
            bool b = false;

            bool check = machineleft.startauto(a,b);
        }
        //delegate void UpdateReceiveDataCallback();
        void UpdateReceiveData(object sender)
        {
            //if (textBox1.InvokeRequired)
            {
                //UpdateReceiveDataCallback d = new UpdateReceiveDataCallback(updateReceiveTextBox);
                //this.Invoke(d, new object[] {  });
            }
            //else
            {
                //textBox1.AppendText(receiveData);
            }
        }
        private void UpdateConnectedChanged(object sender)
        {
            //if (modbusClient.Connected)
            {

            }
            //else
            {

            }
        }
        private void angle_Click1(object sender, RoutedEventArgs e)
        {

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Bug_OnLoaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            second.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, animation);

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
            //try
            //{
            //    if (!modbusClient.Connected)
            //    {
            //        btConnect_Click(null, null);
            //    }
            //    bool[] serverResponse = modbusClient.ReadDiscreteInputs(int.Parse(txtStartingAddressInput.Text) - 1, int.Parse(txtNumberOfValuesInput.Text));
            //    lsbAnswerFromServer.Items.Clear();
            //    for (int i = 0; i < serverResponse.Length; i++)
            //    {
            //        lsbAnswerFromServer.Items.Add(serverResponse[i]);
            //    }
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message, "Exception Reading values from Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btFC05_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btFC15_Click(object sender, RoutedEventArgs e)
        {

        }

        //public void connection()
        //{
            //modbusClient.Connect();
            //modbusClient.Baudrate = 9600;
            //modbusClient.UnitIdentifier = 1;
            //modbusClient.Port = 3;
        //}
    }
}
