using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoList;
using ToDoList.Models;
using Task = ToDoList.Models.Task;

namespace ToDoList
{
    /// <summary>
    /// Логика взаимодействия для ReViewWindows.xaml
    /// </summary>
    public partial class ReViewWindows : Window
    {
        public Task EditTask = null;
        public ReViewWindows(Task editTask)
        {
            InitializeComponent();

            EditTask = editTask;
            this.DataContext = EditTask;

            YalanskyEntities categories = new YalanskyEntities();

            CBCategory.ItemsSource = categories.Categories.ToList();

            CBCategory.SelectedItem = categories.Categories.FirstOrDefault(x => x.CategoryID == EditTask.CategoryID);
        }

        private void SaveReViewTask(object sender, RoutedEventArgs e)
        {
            if (CBCategory.SelectedItem != null)
            {
                EditTask.CategoryID = (CBCategory.SelectedItem as Categories).CategoryID;

                YalanskyEntities content = new YalanskyEntities();

                content.Task.AddOrUpdate(EditTask);

                content.SaveChanges();

                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали категорию");
            }
        }
    }
}
