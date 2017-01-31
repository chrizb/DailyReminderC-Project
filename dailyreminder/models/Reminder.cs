using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dailyreminder.models {
    public class Reminder {

        long id;
        public long Id { get; set; }
      
        // Time is measured in minutes from 00:00
        public int startTime{ get; set; }
        public int endTime{ get; set; }
        String days;
        public String Days {
            get { return days; }
            set { this.days = value; }
        }
        String title;
        public String Title {
            get { return title; }
            set { this.title = value; }
        }
        String description;
        public String Description {
            get { return description; }
            set { this.description = value; }
        }
        bool done;
        public bool Done {
            get { return done; }
        }

        public void setToDone() {
            done = true;
        }
        public String getStartTimeString() {
            return (startTime/60) + ":" + (startTime % 60);
        }
        public String getEndTimeString() {
            return (endTime / 60) + ":" + (endTime % 60);
        }


    }
}
