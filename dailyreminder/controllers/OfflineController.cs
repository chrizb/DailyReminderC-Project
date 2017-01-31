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
            } catch(FileNotFoundException e) {
                // Do somemthing, if you pallar
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
