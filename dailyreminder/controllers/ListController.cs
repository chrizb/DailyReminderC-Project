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
using dailyreminder;

namespace dailyreminder.controllers {
    class ListController {

        List<Reminder> reminders;
        Grid grid;
        MainController mainController;
        MainWindow mainWindow;

        private List<RowDefinition> rows;
        private List<Label> labels;
        private List<Button> buttons;

        public ListController(Grid grid, List<Reminder> reminders, MainController mc) {
            this.reminders = reminders;
            this.grid = grid;
            mainController = mc;

            rows = new List<RowDefinition>();
            labels = new List<Label>();
            buttons = new List<Button>();

        }

        public void setNewReminderList(List<Reminder> reminders) {
            this.reminders = reminders;
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

        public void ListAllFrontPage(){
            for (int i = 0; i < reminders.Count; i++) {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(30);
                rows.Add(rd);
                grid.RowDefinitions.Add(rows.ElementAt((rows.Count - 1))); // Gets and adds the latest new rowdefinition
                Label title = new Label { Content = reminders.ElementAt(i).Title};
                Grid.SetRow(title, i);
                Grid.SetColumn(title, 0);
                Label startTime = new Label { Content = reminders.ElementAt(i).getStartTimeString() };
                Grid.SetRow(startTime, i);
                Grid.SetColumn(startTime, 1);
                Label endTime = new Label { Content = reminders.ElementAt(i).getEndTimeString() };
                Grid.SetRow(endTime, i);
                Grid.SetColumn(endTime, 2);
                    
                // doneButt.Click need a function to call 

                Button doneButt = new Button { Content = "Done!", Tag = reminders.ElementAt(i).Id }; // Sets the name of the button
                doneButt.Click += doneButton_Clicked;

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

        public void ListAllOverview() {
            for (int i = 0; i < reminders.Count; i++) {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(30);
                rows.Add(rd);
                grid.RowDefinitions.Add(rows.ElementAt((rows.Count - 1))); // Gets and adds the latest new rowdefinition
                Label title = new Label { Content = reminders.ElementAt(i).Title };
                Grid.SetRow(title, i);
                Grid.SetColumn(title, 0);
                Label startTime = new Label { Content = reminders.ElementAt(i).getStartTimeString() };
                Grid.SetRow(startTime, i);
                Grid.SetColumn(startTime, 1);
                Label endTime = new Label { Content = reminders.ElementAt(i).getEndTimeString() };
                Grid.SetRow(endTime, i);
                Grid.SetColumn(endTime, 2);
                Button doneButt = new Button { Content = "Edit", Tag = reminders.ElementAt(i).Id }; // Sets the .Tag of the button
                doneButt.Click += editButton_Clicked;

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

        private void editButton_Clicked(object sender, RoutedEventArgs e)
        {
            Button edit = (Button) sender;
            mainController.editFunction(long.Parse(edit.Tag.ToString()));
            ListAllOverview();
        }


        private void doneButton_Clicked(object sender, EventArgs e) {
            Button butt = (Button)sender;
            long id = (long)butt.Tag;
            mainController.setReminderToDone(id);
        }
    }
}
