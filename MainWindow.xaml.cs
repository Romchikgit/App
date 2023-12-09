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
            var result = addnewTaskWindow.ShowDialog();
            YalanskyEntities content = new YalanskyEntities();

            try
            {
                if (result == true)
                {
                    content.Task.Add(new Task()
                    {
                        Title = addnewTaskWindow.NewTaskDescription,
                        CategoryID = (Convert.ToInt32(addnewTaskWindow.SelwctCategory)) + 1
                    });
                    content.Task.ToList().Last();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Вы не выбрали категорию");
            }
            content.SaveChanges();
            TaskLV.ItemsSource = content.Task.ToList();
        }


        private void ReViewNewTaks(object sender, RoutedEventArgs e)
        {
            YalanskyEntities content = new YalanskyEntities();

            ReViewWindows reViewWindow = new ReViewWindows();
            var result = reViewWindow.ShowDialog();
            var item = TaskLV.SelectedItem as Task;

            try
            {
                if (result == true)
                {
                    item.CategoryID = Convert.ToInt32(reViewWindow.SelwctCategory) + 1;
                    item.Title = reViewWindow.ReViewTaskDescription;
                    content.Task.AddOrUpdate(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Вы не выбрали категорию");
            }

            content.SaveChanges();
            TaskLV.ItemsSource = content.Task.ToList();
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            var task = (sender as Button).DataContext as YalanskyEntities;

            YalanskyEntities content = new YalanskyEntities();
            content.Task.Remove(content.Task.ToList().Last());

            content.SaveChanges();
            TaskLV.ItemsSource = content.Task.ToList();
        }


        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            YalanskyEntities content = new YalanskyEntities();
            TaskLV.ItemsSource = content.Task.Where(x => x.ID.ToString().Contains(SearshText.Text)).ToList();
        }

    }
}
