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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tour
{
    /// <summary>
    /// Логика взаимодействия для HotelPage1.xaml
    /// </summary>
    public partial class HotelPage1 : Page
    {
        //private Hotel _hotel;
        //public static TurismEntities1 _context = new TurismEntities1();
        //private int _currentPage = 1;
        private int _maxpage = 0;
        public HotelPage1()
        {
            InitializeComponent();
            //Pages();
        }


        //public void Pages()
        //{
        //    DGridHotels.ItemsSource = _context.Hotel.OrderBy(h => h.Name).ToList();
        //    _maxpage = Convert.ToInt32(Math.Ceiling(_context.Hotel.ToList().Count * 1.0 / 10));
        //    var listhotel = _context.Hotel.ToList().Skip((_currentPage - 1) * 10).Take(10).ToList();
        //    Total.Content = " of " + _maxpage.ToString();
        //    CurrentPage.Text = _currentPage.ToString();
        //    DGridHotels.ItemsSource = listhotel;

        //}


        //открытие страницы добавления
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditPage1((sender as Button).DataContext as Hotel));
        }


        //удаление записи
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridHotels.SelectedItems.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TurismEntities1.GetContext().Hotel.RemoveRange(hotelsForRemoving);
                    TurismEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridHotels.ItemsSource = TurismEntities1.GetContext().Hotel.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        //событие для обновления страницы
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TurismEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridHotels.ItemsSource = TurismEntities1.GetContext().Hotel.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditPage2((sender as Button).DataContext as Hotel));
        }



        // Пагинация для старницы с отелями
        //private void GoFirstPage_Click(object sender, RoutedEventArgs e)
        //{
        //    _currentPage = 1;
        //    Pages();


        //}

        //private void GoPrevPage_Click(object sender, RoutedEventArgs e)
        //{
        //    if(_currentPage - 1 < 1)
        //    {
        //        return;
        //    }
        //    _currentPage = _currentPage - 1;
        //    Pages();
        //}

        //private void CurrentPage_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if(_currentPage > 0 && _currentPage< _maxpage && CurrentPage.Text !=null)
        //    {
        //        _currentPage = Convert.ToInt32(CurrentPage.Text);
        //        Pages();

        //    }
        //}

        //private void NextPage_Click(object sender, RoutedEventArgs e)
        //{
        //    if(_currentPage +1 > _maxpage)
        //    {
        //        return;
        //    }
        //    _currentPage = _currentPage + 1;
        //    Pages();
        //}

        //private void lastPage_Click(object sender, RoutedEventArgs e)
        //{
        //    _currentPage = _maxpage;
        //    Pages();
        //}
    }
}
