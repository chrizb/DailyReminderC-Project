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
            public string endTime;
            public string startTime;
            public long Id;

            public Alarm(string starttime, string endtime, long id) {
                endTime = endtime;
                startTime = starttime;
                Id = id;
            }
        }

        List<Alarm> alarms;

        MainController mainController;

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer startTimeTimer = new DispatcherTimer();

        public AlarmController(List<Reminder> reminders, MainController mc) {
            mainController = mc;
            alarms = new List<Alarm>();
            foreach (Reminder reminder in reminders) {
                alarms.Add(new Alarm(reminder.getStartTimeString(), reminder.getEndTimeString(), reminder.Id));
            }
            startclock();
        }

        private void startclock() {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickEvent;
            timer.Start();

            startTimeTimer.Interval = TimeSpan.FromMinutes(1);
            startTimeTimer.Tick += slowTickEvent;
            startTimeTimer.Start();
        }

        private void slowTickEvent(object sender, EventArgs e) {
            string nowTime = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            foreach (Alarm alarm in alarms) {
                Reminder currentReminder = new Reminder { Id = -1 };
                mainController.getTodaysReminders().Find(x => x.Id == alarm.Id);
                if (alarm.startTime == nowTime && !currentReminder.Done) {
                    // Visa en pop-up
                }

            }
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

                if (alarm.endTime == nowTime && !currentReminder.Done) {
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
            alarms.Add(new Alarm(reminder.getStartTimeString(), reminder.getEndTimeString(), reminder.Id));
        }
    }
}
