using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using System.Xml;

namespace Counter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DateTime dateconst = DateTime.Now.AddHours(1).AddMinutes(59).AddSeconds(59);
        public DateTime pause = DateTime.Now;
        public int datespace = 0;
        public string key = "";
        public int i = 0;
        public double fontsize { set; get; }
        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (key.Equals("Space"))
                    {
                        if (pause != dateconst)
                        {
                            datespace += ((int)(DateTime.Now - pause).Duration().TotalMilliseconds);
                            pause = dateconst;
                        }
                        int date = Convert.ToInt32(DayCount() / 1000);
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input,
                        new Action(() =>
                        {
                            if (Convert.ToInt32(date / 3600) < 10)
                            {
                                HourCounter.Text = "0" + Convert.ToInt32(date / 3600).ToString();
                            }
                            else
                            {
                                HourCounter.Text = Convert.ToInt32(date / 3600).ToString();
                            }
                            if (Convert.ToInt32(date % 3600 / 60) < 10)
                            {
                                MinuteCounter.Text = "0" + Convert.ToInt32(date % 3600 / 60).ToString();
                            }
                            else
                            {
                                MinuteCounter.Text = Convert.ToInt32(date % 3600 / 60).ToString();
                            }
                            if (Convert.ToInt32(date % 60) < 10)
                            {
                                SecondCounter.Text = "0" + Convert.ToInt32(date % 60).ToString();
                            }
                            else
                            {
                                SecondCounter.Text = Convert.ToInt32(date % 60).ToString();
                            }
                        }));

                        if (date <= 0)
                        {
                            break;
                        }
                    }
                    Thread.Sleep(100);
                }
            });
        }
        public int DayCount()
        {
            int date = 0;
            if (dateconst > DateTime.Now)
            {
                date = ((int)(dateconst - DateTime.Now).Duration().TotalMilliseconds) + datespace;
            }
            else
            {
                date = -((int)(dateconst - DateTime.Now).Duration().TotalMilliseconds) + datespace;
            }
            return date;
        }
        private void Event(object sender, KeyEventArgs e)
        {

            if (key.Equals("Space"))
            {
                key = "";
                pause = DateTime.Now;
            }
            else
            {
                key = e.Key.ToString();
            }
        }
        private void Ev(object sender, SizeChangedEventArgs e)
        {
            double size = ActualWidth;
            int based = 1200;
            this.HourCounter.FontSize = Convert.ToInt32(size / 8);
            this.MinuteCounter.FontSize = Convert.ToInt32(size / 8);
            this.SecondCounter.FontSize = Convert.ToInt32(size / 8);

            this.text1.FontSize = Convert.ToInt32(size / 32);
            this.text2.FontSize = Convert.ToInt32(size / 32);
            this.text3.FontSize = Convert.ToInt32(size / 32);

            this.HourCounter.Margin = new Thickness(-500 * size / based, 0, 0, 0);
            this.MinuteCounter.Margin = new Thickness(0, 0, 0, 0);
            this.SecondCounter.Margin = new Thickness(500 * size / based, 0, 0, 0);

            this.text1.Margin = new Thickness(-510 * size / based, 175 * size / based, 0, 0);
            this.text2.Margin = new Thickness(-7.5 * size / based, 175 * size / based, 0, 0);
            this.text3.Margin = new Thickness(494 * size / based, 175 * size / based, 0, 0);
        }
    }
}
