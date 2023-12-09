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

namespace ToDoList
{
    /// <summary>
    /// Логика взаимодействия для ReViewWindows.xaml
    /// </summary>
    public partial class ReViewWindows : Window
    {
        //List<Categories> categories = new List<Categories>();
        public ReViewWindows()
        {
            InitializeComponent();

            YalanskyEntities categories = new YalanskyEntities();
            CBCategory.ItemsSource = categories.Categories.ToList();

        }

        private void SaveReViewTask(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string ReViewTaskDescription
        {
            get { return new TextRange(TaskDescription.Document.ContentStart, TaskDescription.Document.ContentEnd).Text; }
        }
        public string SelwctCategory
        {
            get { return Convert.ToString(CBCategory.SelectedIndex); }
        }
    }
}
