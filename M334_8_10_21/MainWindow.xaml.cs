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
using System.IO.Ports;
using M334_8_10_21.Services;

namespace M334_8_10_21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        //public M334_8_10_21.ModbusClient modbusClient;
        public delegate void ThreadStart();

        //Machine machineleft = new Machine();
        //Machine machinemidl = new Machine();
        //Machine machineright = new Machine();
        //connection ketnoi = new connection();
        public static readonly DependencyProperty SpinSpeedProperty = DependencyProperty.Register("SpinSpeed", typeof(TimeSpan), typeof(MainWindow), new PropertyMetadata(default(TimeSpan)));
        public static readonly DependencyProperty AngleProperty1 = DependencyProperty.Register("Angle1", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty2 = DependencyProperty.Register("Angle2", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty3 = DependencyProperty.Register("Angle3", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty4 = DependencyProperty.Register("Angle4", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty5 = DependencyProperty.Register("Angle5", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty6 = DependencyProperty.Register("Angle6", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty7 = DependencyProperty.Register("Angle7", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty8 = DependencyProperty.Register("Angle8", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty9 = DependencyProperty.Register("Angle9", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty10 = DependencyProperty.Register("Angle10", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty11 = DependencyProperty.Register("Angle11", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty AngleProperty12 = DependencyProperty.Register("tempmc1", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty13 = DependencyProperty.Register("tempmc2", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty14 = DependencyProperty.Register("tempmc3", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty xRpmProperty1 = DependencyProperty.Register("xRpm1", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty xRpmProperty2 = DependencyProperty.Register("xRpm2", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty xRpmProperty3 = DependencyProperty.Register("xRpm3", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty xxRpmProperty1 = DependencyProperty.Register("xxRpm1", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty xxRpmProperty2 = DependencyProperty.Register("xxRpm2", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty xxRpmProperty3 = DependencyProperty.Register("xxRpm3", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty AngleProperty_press_mpa1 = DependencyProperty.Register("press_mpa1", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty_press_mpa2 = DependencyProperty.Register("press_mpa2", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty AngleProperty_press_mpa3 = DependencyProperty.Register("press_mpa3", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public int i = 270, i_new = 0, stt_ang = 0, stt_ang2 = 0, val_ang1;
        public Machine mc1 = new Machine();
        public Machine mc2 = new Machine();
        public Machine mc3 = new Machine();
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            //SpinSpeed = TimeSpan.FromMilliseconds(200);

            dispatcherTimer.Start();
            ModbusServices mb = new ModbusServices(mc1, mc2, mc3);
            LogicServices logic = new LogicServices(mc1, mc2, mc3);
            mb.Connect();
            //mb.Subcribe();
            logic.Subcribe();
            mb.Subcribe();

            //mb.updatedata();
            //mb.blink();
            tempmc1 = 145;      //Value 0;  Max: 400
            tempmc2 = 145;      //Value 0;
            tempmc3 = 145;      //Value 0;

            xRpm1 = 270;        //Value 0;
            xRpm2 = 270;        //Value 0;
            xRpm3 = 270;        //Value 0;

            //xxRpm1 = 270  + 36;
            press_mpa2 = 215;   //Value 0;
            press_mpa3 = 215;   //Value 0;
            press_mpa1 = 215;   //Value 0;  max 335

            //xRpm1 = 360 + 270;
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
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        delegate void UpdateReceiveDataCallback();
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            angleinput1.Clear();
            var random = new Random();
            int num = random.Next(0, 360);

            tempmc1 = mc1.vl_temperature_gas * 2.7 + 145;
            tempmc2 = mc2.vl_temperature_gas * 2.7 + 145;
            tempmc3 = mc3.vl_temperature_gas * 2.7 + 145;


            #region Toc do dong co <1000
            if (mc1.vl_speed_engine < 100)
            {
                xRpm1 = mc1.vl_speed_engine * 3.6 + 270;
                xxRpm1 = mc1.vl_speed_engine / 2.7 + 270;
            }
            if (mc1.vl_speed_engine == 99)
            {
                xRpm1 = (mc1.vl_speed_engine + 1) * 3.6 + 270;
                xxRpm1 = (mc1.vl_speed_engine + 1) / 2.7 + 270;
            }

            if (mc2.vl_speed_engine < 100)
            {
                xRpm2 = mc2.vl_speed_engine * 3.6 + 270;
                xxRpm2 = mc2.vl_speed_engine / 2.7 + 270;
            }
            if (mc2.vl_speed_engine == 99)
            {
                xRpm2 = (mc2.vl_speed_engine + 1) * 3.6 + 270;
                xxRpm2 = (mc2.vl_speed_engine + 1) / 2.7 + 270;
            }

            if (mc3.vl_speed_engine < 100)
            {
                xRpm3 = mc3.vl_speed_engine * 3.6 + 270;
                xxRpm3 = (mc3.vl_speed_engine / 2.7) + 270;
            }
            if (mc3.vl_speed_engine == 99)
            {
                xRpm3 = (mc3.vl_speed_engine + 1) * 3.6 + 270;
                xxRpm3 = (mc3.vl_speed_engine + 1) /2.7 + 270;
            }
            #endregion

            #region Toc do dong co > 1000
            if (mc1.vl_speed_engine > 101)
            {
                xRpm1 = mc1.vl_speed_engine * 3.6 + 270;
                xxRpm1 = (mc1.vl_speed_engine) / 2.7 + 270;
            }
            if (mc2.vl_speed_engine > 101)
            {
                xRpm2 = mc2.vl_speed_engine * 3.6 + 270;
                xxRpm2 = (mc2.vl_speed_engine) / 2.7 + 270;
            }
            if (mc3.vl_speed_engine > 101)
            {
                xRpm3 = mc3.vl_speed_engine * 3.6 + 270;
                xxRpm3 = (mc3.vl_speed_engine) / 2.7 + 270;
            }
            #endregion

            //xxRpm1 = mc1.vl_speed_engine * 2.7 + 270;
            //xxRpm2 = mc2.vl_speed_engine * 2.7 + 270;
            //xxRpm3 = mc3.vl_speed_engine * 2.7 + 270;

            press_mpa1 = 215 + mc1.vl_mainlineoilpressure * 8;
            press_mpa2 = 215 + mc2.vl_mainlineoilpressure * 8;
            press_mpa3 = 215 + mc3.vl_mainlineoilpressure * 8;

        }
        private void angle_Click1(object sender, RoutedEventArgs e)
        {
            float val_ang1;
            bool result = float.TryParse(angleinput1.Text, out val_ang1);
            if (!result)
                return;

            Angle1 = Angle2 = Angle3 = Angle4 = Angle5 = Angle6 = Angle7 = Angle8 = Angle9 = Angle10 = Angle11 = convert100ToDegree((float)convertyRPM(val_ang1));
            xRpm1 = convert1KToDegree((int)convertxRPM(val_ang1));

            angleinput1.Clear();

            dispatcherTimer.Start();
            // Console Output
            //for (int i = 0; i < readCoils.Length; i++)
            //{
            //Console.WriteLine("Value of Coil " + (9 + i + 1) + " " + readCoils[i].ToString());
            //angleinput1.Text = readCoils[i].ToString();
            //}
            //angleinput1.Text = readCoils[9].ToString();

            //for (int i = 0; i < readHoldingRegisters.Length; i++)
            //angleinput1.Text = readHoldingRegisters[9].ToString();
            //Console.WriteLine("Value of HoldingRegister " + (i + 1) + " " + readHoldingRegisters[i].ToString());
            //modbusClient.Disconnect();                                                //Disconnect from Server

        }
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
        public TimeSpan SpinSpeed
        {
            get { return (TimeSpan)GetValue(SpinSpeedProperty); }
            set { SetValue(SpinSpeedProperty, value); }
        }
        public double Angle1
        {
            get { return (double)GetValue(AngleProperty1); }
            set { SetValue(AngleProperty1, value); }

        }
        public double Angle2
        {
            get { return (double)GetValue(AngleProperty2); }
            set { SetValue(AngleProperty2, value); }

        }
        public double Angle3
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty3, value); }

        }
        public double Angle4
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty4, value); }

        }
        public double Angle5
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty5, value); }

        }
        public double Angle6
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty6, value); }

        }
        public double Angle7
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty7, value); }

        }
        public double Angle8
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty8, value); }

        }
        public double Angle9
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty9, value); }

        }
        public double Angle10
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty10, value); }

        }
        public double Angle11
        {
            get { return (double)GetValue(AngleProperty3); }
            set { SetValue(AngleProperty11, value); }

        }
        public double tempmc1
        {
            get { return (double)GetValue(AngleProperty12); }
            set { SetValue(AngleProperty12, value); }

        }
        public double tempmc2
        {
            get { return (double)GetValue(AngleProperty13); }
            set { SetValue(AngleProperty13, value); }

        }
        public double tempmc3
        {
            get { return (double)GetValue(AngleProperty14); }
            set { SetValue(AngleProperty14, value); }

        }
        public double xRpm1
        {
            get { return (double)GetValue(xRpmProperty1); }
            set { SetValue(xRpmProperty1, value); }

        }
        public double xRpm2
        {
            get { return (double)GetValue(xRpmProperty2); }
            set { SetValue(xRpmProperty2, value); }

        }
        public double xRpm3
        {
            get { return (double)GetValue(xRpmProperty3); }
            set { SetValue(xRpmProperty3, value); }

        }
        public double xxRpm1
        {
            get { return (double)GetValue(xxRpmProperty1); }
            set { SetValue(xxRpmProperty1, value); }

        }
        public double xxRpm2
        {
            get { return (double)GetValue(xxRpmProperty2); }
            set { SetValue(xxRpmProperty2, value); }

        }
        public double xxRpm3
        {
            get { return (double)GetValue(xxRpmProperty3); }
            set { SetValue(xxRpmProperty3, value); }

        }
        public double press_mpa1
        {
            get { return (double)GetValue(AngleProperty_press_mpa1); }
            set { SetValue(AngleProperty_press_mpa1, value); }

        }
        public double press_mpa2
        {
            get { return (double)GetValue(AngleProperty_press_mpa2); }
            set { SetValue(AngleProperty_press_mpa2, value); }

        }
        public double press_mpa3
        {
            get { return (double)GetValue(AngleProperty_press_mpa3); }
            set { SetValue(AngleProperty_press_mpa3, value); }

        }
        private void Bug_OnLoaded(object sender, RoutedEventArgs e)
        {
            //DoubleAnimation animation = new DoubleAnimation
            //{
            //    From = 0,
            //    To = 360,
            //    RepeatBehavior = RepeatBehavior.Forever,
            //    Duration = new Duration(TimeSpan.FromSeconds(1))
            //};
            //second.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, animation);

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimer.Start();
        }
        static float convertxRPM(float xrpm)
        {
            float xRpm;
            xRpm = xrpm / 1000;
            return xRpm;
        }
        static float convertyRPM(float yrpm)
        {
            float yRpm;
            yRpm = yrpm % 1000;
            return yRpm;
        }
        static int convert1KToDegree(int k)
        {

            switch (k)
            {
                case 1:
                    return 306;
                case 2:
                    return 342;
                case 3:
                    return 18;
                case 4:
                    return 54;
                case 5:
                    return 90;
                case 6:
                    return 126;
                case 7:
                    return 162;
                case 8:
                    return 198;

                default:
                    return 243;
            }

        }
        static float convert100ToDegree(float j)
        {
            float a;
            if (j < 250)
            {
                a = (float)(270 + ((j * 7.2) / 20));
            }
            else
            {
                a = (float)(((j * 7.2) / 20) - 90);
            }
            return a;
        }
    }
}
