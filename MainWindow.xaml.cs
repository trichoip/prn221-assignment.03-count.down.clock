using System;
using System.Windows;
using System.Windows.Threading;

namespace Prn221_Count_Down_Clock
{

    public partial class MainWindow : Window
    {

        private int totalSeconds;
        private DispatcherTimer timer;

        public MainWindow()
        {

            InitializeComponent();
            ResetTimer();

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputSecondsTextBox.Text, out int seconds))
            {
                totalSeconds = seconds;
                if (timer == null)
                {
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += Timer_Tick;
                }

                timer.Start();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of seconds.");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            totalSeconds--;
            UpdateTimeDisplay();

            if (totalSeconds <= 0)
            {
                timer.Stop();
                MessageBox.Show("Countdown finished!");
            }
        }

        private void UpdateTimeDisplay()
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            TimerTextBlock.Text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        private void ResetTimer()
        {
            totalSeconds = 0;
            UpdateTimeDisplay();
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        private void Alarm_Click(object sender, RoutedEventArgs e)
        {
            new AlarmWindow().Show();
            this.Close();

        }
    }
}
