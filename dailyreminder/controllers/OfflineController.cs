using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dailyreminder.models;
using System.IO;
using System.Web.Script.Serialization;

namespace dailyreminder.controllers {
    class OfflineController : ReminderDataController{
        public override void saveAll(List<Reminder> reminderList, string token) {
            string path = @"c:\data\tempFile.dr";
            string json = new JavaScriptSerializer().Serialize(reminderList);
            File.WriteAllText(path, json);

        }
        public override List<Reminder> loadAll(string filepath = @"c:\data\tempFile.dr") {
            List<Reminder> reminderList = new List<Reminder>();
            try {
                string fileContent = File.ReadAllText(filepath);
                reminderList = new JavaScriptSerializer().Deserialize<List<Reminder>>(fileContent);
                if (reminderList == null)
                    reminderList = new List<Reminder>();
            } catch(FileNotFoundException e) {
                // Do somemthing, if you pallar
            }
            
            // Check which reminders is old and which is not
            foreach (Reminder reminder in reminderList) {
                if (!reminder.dateSetToDone.Date.Equals(DateTime.Now.Date)) { // Checks is the "Done"-attribute were set a different day
                    reminder.Done = false;                                    // if so, set the "done"-attribute back to false
                }
            }
            
            return reminderList;
        }
        public override void saveReminder(Reminder reminder) { }
        public override bool login(string email, string token) { return true; }
        public override void addNewReminder(Reminder reminder) { }
        public override void deleteReminder(int reminderId) { }
        public override void updateReminder(Reminder reminder) { }
    }
}
