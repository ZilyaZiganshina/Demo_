using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ToursPage1());

            Manager.MainFrame = MainFrame;
          
            
            //ImportTours();
        }

        // отктыие страницы с отелямми
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HotelPage1());

            Manager.MainFrame = MainFrame;

        }

        // импорт данных о турах
        private void ImportTours()
        {
            var filedata = File.ReadAllLines(@"C:\Users\zilzi\Desktop\ДЭ\ДЭ\import\Туры.txt");
            var images = Directory.GetFiles(@"C:\Users\zilzi\Desktop\ДЭ\ДЭ\import\Туры фото");
            foreach (var line in filedata)
            {
                var data = line.Split('\t');
                var tempTour = new Tours
                {
                    Name = data[0].Replace("\"", ""),
                    TicketCount = int.Parse(data[2]),
                    Price = decimal.Parse(data[3]),
                    IsActual = (data[4] == "0") ? false : true
                };
                foreach (var tourType in data[5].Replace("\"", "").Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var currentType = TurismEntities1.GetContext().Type.ToList().FirstOrDefault(p => p.Name == tourType);
                    if (currentType != null)
                        tempTour.Type.Add(currentType);
                }
                try
                {
                    tempTour.ImagePreview = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempTour.Name)));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                TurismEntities1.GetContext().Tours.Add(tempTour);
                TurismEntities1.GetContext().SaveChanges();
            }
        }

      // кнопка "Назад"
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        //
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                Back.Visibility = Visibility.Visible;
            }
            else
            {
                Back.Visibility = Visibility.Hidden;
            }
        }
    }
}
