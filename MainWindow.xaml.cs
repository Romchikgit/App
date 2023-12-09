using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoList.Models;
using ToDoList;
using Task = ToDoList.Models.Task;
using System.Data.Entity.Migrations;

namespace ToDoList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            YalanskyEntities content = new YalanskyEntities();

            TaskLV.ItemsSource = content.Task.ToList();
        }

        private void AddNewTaks(object sender, RoutedEventArgs e)
        {
            AddnewTaskWindow addnewTaskWindow = new AddnewTaskWindow();
            YalanskyEntities content = new YalanskyEntities();

            var result = addnewTaskWindow.ShowDialog();

            GetSearch();
        }


        private void ReViewNewTaks(object sender, RoutedEventArgs e)
        {
            if (TaskLV.SelectedItem != null)
            {
                YalanskyEntities content = new YalanskyEntities();
                ReViewWindows reViewWindow = new ReViewWindows(TaskLV.SelectedItem as Task);

                reViewWindow.ShowDialog();

                GetSearch();
            }
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            YalanskyEntities content = new YalanskyEntities();

            var task = (sender as Button).DataContext as Task;
            var deleteTask = content.Task.FirstOrDefault(x => x.ID == task.ID);

            if (deleteTask != null)
            {
                content.Task.Remove(deleteTask);

                content.SaveChanges();

                GetSearch();
            }
        }


        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetSearch();
        }

        public void GetSearch()
        {
            YalanskyEntities content = new YalanskyEntities();
            TaskLV.ItemsSource = content.Task.Where(x => x.ID.ToString().Contains(SearshText.Text)).ToList();
        }
    }
}
