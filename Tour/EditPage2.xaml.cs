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
    /// Логика взаимодействия для EditPage2.xaml
    /// </summary>
    public partial class EditPage2 : Page
    {
        private Hotel _currentHotel = new Hotel();
        public EditPage2(Hotel selectedHotel)
        {
            InitializeComponent();
            if (selectedHotel != null)
                _currentHotel = selectedHotel;

            DataContext = _currentHotel;
            ComboCountries.ItemsSource = TurismEntities1.GetContext().Country.ToList();
        }
         // редактирование данных об отеле--------------------------------------------------------------------------------------
        private void BtnSave_Click_2(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
                errors.AppendLine("Количество звезд - число от 1 до 5");
            if (_currentHotel.Country == null)
                errors.AppendLine("Выберите страну");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentHotel.Id == 0)
            {
                TurismEntities1.GetContext().Hotel.Add(_currentHotel);
            }

            try
            {
            
                TurismEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена успешно");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 

