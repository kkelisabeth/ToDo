/*
* Creator:      Kozlova Elizabeth Paula
*
* Title:        To Do List
*
* Description:
*               This program is a to do list with such functions: 
*                   - add a task 
*                   - export a task list 
*                   - import a task list 
*                   - remove selected tasks
*/

using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW4_Program1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> taskContent = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        //Event Handler for clicking on the Add button 
        //Creates a new checkbox each time the event handler is called 
        //Adds new checkbox to the stack panel with user's tasks 
        private void AddNewtask_Clicked(object sender, RoutedEventArgs e)
        {
            CheckBox newCheckBox = new CheckBox();
            newCheckBox.Content = NewTaskTextBox.Text;

            taskContent.Add(newCheckBox.Content.ToString()); 

            UserTasksStackPanel.Children.Add(newCheckBox);

            NewTaskTextBox.Text = ""; 
        }

        //Event Handler for clicking on the Remove Selected Tasks button 
        //Checks whether the checkbox was checked or not
        //If the checkbox is checked -> removes it from the StackPanel 
        private void RemoveTasksButton_Clicked(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Do you want to delete this tasks from your to do list?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < UserTasksStackPanel.Children.Count; i++)
                {
                    CheckBox userCheckBox = UserTasksStackPanel.Children[i] as CheckBox;

                    //checking if the checkbox is selected 
                    //and if it selected - delete it from the list 
                    if (userCheckBox.IsChecked == true)
                    {
                        UserTasksStackPanel.Children.Remove(userCheckBox);
                        taskContent.Remove(userCheckBox.Content.ToString());
                        --i;
                    }
                }
            }

        }

        //Function for returning the contents of the List 
        private List<string> GetTaskContent()
        {
            return taskContent;
        }

        //Event Handlew for clicking on the Export Tasks button 
        //Saves all the contents of checkboxes to a file 
        //Removes everything from the StackPanel
        private void ExportTasksButton_Click(object sender, RoutedEventArgs e)
        {
            string taskContent = ""; 

            for(int i = 0; i < UserTasksStackPanel.Children.Count; i++)
            {
                CheckBox tasks = UserTasksStackPanel.Children[i] as CheckBox;
                taskContent += tasks.Content + "\n"; 
            }

            UserTasksStackPanel.Children.Clear(); 

            SaveFileDialog saveFileDialog = new SaveFileDialog();   
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;

                System.IO.File.WriteAllText(fileName, taskContent);
            }
        }

        //Event Handler for clicking on the Import Tasks button 
        //Opens a file, chosen by the user, and copies all contents of it to the multiple checkboxes 
        private void ImportTasksButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                string taskText = File.ReadAllText(filename);
                List<Tasks> task = JsonConvert.DeserializeObject<List<Tasks>>(taskText);

                foreach (var entry in task)
                {
                    CheckBox taskCheckBox = new();

                    taskCheckBox.Content = $"{entry.taskDescription}";
                    taskCheckBox.Tag = entry;

                    UserTasksStackPanel.Children.Add(taskCheckBox);
                }

            }
        }
    }
}
