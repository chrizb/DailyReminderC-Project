using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dailyreminder.models;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace dailyreminder.controllers {
    class ListController {

        List<Reminder> reminders;
        Grid grid;

        private List<RowDefinition> rows;
        private List<Label> labels;
        private List<Button> buttons;

        public ListController(Grid grid, List<Reminder> reminders) {
            this.reminders = reminders;
            this.grid = grid;

            rows = new List<RowDefinition>();
            labels = new List<Label>();
            buttons = new List<Button>();

        }

        public void ResetGrid() {
            foreach(Label label in labels){
                grid.Children.Remove(label);
            }
            foreach (Button button in buttons) {
                grid.Children.Remove(button);
            }
            foreach (RowDefinition row in rows) {
                grid.RowDefinitions.Remove(row);
            }
            
        }

        public void ListAll(){
            for (int i = 0; i < reminders.Count; i++) {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(30);
                rows.Add(rd);
                grid.RowDefinitions.Add(rows.ElementAt((rows.Count - 1))); // Gets and adds the latest new rowdefinition
                Label title = new Label { Content = reminders.ElementAt(i).Title };
                Grid.SetRow(title, i);
                Grid.SetColumn(title, 0);
                Label startTime = new Label { Content = reminders.ElementAt(i).startTime };
                Grid.SetRow(startTime, i);
                Grid.SetColumn(startTime, 1);
                Label endTime = new Label { Content = reminders.ElementAt(i).endTime };
                Grid.SetRow(endTime, i);
                Grid.SetColumn(endTime, 2);
                Button doneButt = new Button() { Content = "Done!" };
                
                
                // doneButt.Click need a function to call 
                Grid.SetRow(doneButt, i);
                Grid.SetColumn(doneButt, 3);
                // Add all the new elements
                grid.Children.Add(title);
                labels.Add(title);
                grid.Children.Add(startTime);
                labels.Add(startTime);
                grid.Children.Add(endTime);
                labels.Add(endTime);
                grid.Children.Add(doneButt);
                buttons.Add(doneButt);
            }
        }

    }
}
