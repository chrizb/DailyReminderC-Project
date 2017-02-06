using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dailyreminder.models;
using System.Windows.Threading;
using System.Media;

namespace dailyreminder.controllers {
    class AlarmController {

        struct Alarm {
            public string Time;
            public long Id;

            public Alarm(string time, long id) {
                Time = time;
                Id = id;
            }
        }

        List<Alarm> alarms;

        MainController mainController;

        DispatcherTimer timer = new DispatcherTimer();

        public AlarmController(List<Reminder> reminders, MainController mc) {
            mainController = mc;
            alarms = new List<Alarm>();
            foreach (Reminder reminder in reminders) {
                alarms.Add(new Alarm(reminder.getEndTimeString(), reminder.Id));
            }
            startclock();
        }

        private void startclock() {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickEvent;
            timer.Start();
        }

        private void tickEvent(object sender, EventArgs e) {
            string nowTime = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            foreach (Alarm alarm in alarms) {
                Reminder currentReminder = new Reminder { Id = -1 };
                foreach (Reminder reminder in mainController.getTodaysReminders()) { // Get the specific reminder
                    if (reminder.Id == alarm.Id) {
                        currentReminder = reminder;
                    }
                }

                if (alarm.Time == nowTime && !currentReminder.Done) {
                    SoundPlayer sp = new SoundPlayer(@"c:\data\foghorn.wav");
                    sp.Play();
                }
            }
        }

        public void removeAlarm(long id) {
            for (int i = 0; i < alarms.Count; i++) {
                if (alarms.ElementAt(i).Id == id) {
                    alarms.RemoveAt(i);
                }
            }
        }

        public void addAlarm(Reminder reminder) {
            alarms.Add(new Alarm(reminder.getEndTimeString(), reminder.Id));
        }
    }
}
