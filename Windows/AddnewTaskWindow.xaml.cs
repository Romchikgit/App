using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddnewTaskWindow.xaml
    /// </summary>
    public partial class AddnewTaskWindow : Window
    {
        public AddnewTaskWindow()
        {
            InitializeComponent();

            YalanskyEntities categories = new YalanskyEntities();
            CBCategory.ItemsSource = categories.Categories.ToList();


            //CBCategory.ItemsSource = categories;
        }

        private void SaveTask(object sender, RoutedEventArgs e)
        {
            YalanskyEntities content = new YalanskyEntities();

            if(CBCategory.SelectedItem != null)
            {
                content.Task.Add(new Task()
                {
                    Title = TaskDescription.Text,
                    CategoryID = (CBCategory.SelectedItem as Categories).CategoryID
                });

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
