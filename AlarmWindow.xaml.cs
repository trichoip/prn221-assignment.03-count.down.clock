using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Prn221_Count_Down_Clock
{
    /// <summary>
    /// Interaction logic for AlarmWindow.xaml
    /// </summary>
    public partial class AlarmWindow : Window
    {
        private DispatcherTimer timer;
        private DateTime alarmTime;

        public AlarmWindow()
        {
            InitializeComponent();
            StopButton.IsEnabled = false;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int hour = int.Parse(((ComboBoxItem)HourComboBox.SelectedItem).Content.ToString());
            int minute = int.Parse(((ComboBoxItem)MinuteComboBox.SelectedItem).Content.ToString());
            int second = int.Parse(((ComboBoxItem)SecondComboBox.SelectedItem).Content.ToString());
            alarmTime = DateTime.Today + new TimeSpan(hour, minute, second);

            TimeSpan timeRemaining = alarmTime - DateTime.Now;
            if (timeRemaining.TotalMilliseconds > 0)
            {
                if (timer == null)
                {
                    timer = new DispatcherTimer();
                    timer.Interval = timeRemaining;
                    timer.Tick += Timer_Tick;
                }

                timer.Start();
                MessageBox.Show("Alarm set for " + alarmTime.ToString("t"));
                StopButton.IsEnabled = true;
                StartButton.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("you cann't set time before now");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
            StopButton.IsEnabled = false;
            StartButton.IsEnabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            MessageBox.Show("Alarm!");
            StopButton.IsEnabled = false;
            StartButton.IsEnabled = true;
        }
    }
}
