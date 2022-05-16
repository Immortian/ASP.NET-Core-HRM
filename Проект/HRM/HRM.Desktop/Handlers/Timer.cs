using System;
using System.Windows.Threading;

namespace HRM.Desktop.Handlers
{
    public class Timer
    {
        readonly MainWindow window;
        public Timer(MainWindow mainWindow)
        {
            window = mainWindow;
        }
        public DispatcherTimer timer = new DispatcherTimer(); // Таймер афк
        public void StartupTimer()
        {
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Start();
        }
        public void ResetTimer()
        {
            timer.Stop();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e)
        {
            window.error.TimerExeptions();
        }
    }
}
