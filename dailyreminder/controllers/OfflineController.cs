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
        public void saveAll(List<Reminder> reminderList, string token){
            string path = @"c:\tempFile.dr";
            string json = new JavaScriptSerializer().Serialize(reminderList);
            File.WriteAllText(path, json);
        }
        public List<Reminder> loadAll(string filepath = @"c:\tempFile.dr") {
            string fileContent = File.ReadAllText(filepath);
            List<Reminder> reminderList = new JavaScriptSerializer().Deserialize<List<Reminder>>(fileContent);
            return reminderList;
        }
        public void saveReminder(Reminder reminder) { }
        public bool login(string email, string token) { return true; }
        public void addNewReminder(Reminder reminder) { }
        public void deleteReminder(int reminderId) { }
        public void updateReminder(Reminder reminder) { }
    }
}
