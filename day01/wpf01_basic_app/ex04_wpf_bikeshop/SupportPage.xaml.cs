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

namespace ex04_wpf_bikeshop
{
    /// <summary>
    /// SupportPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SupportPage : Page
    {
        public SupportPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var bikeList = new List<Bike>();
            bikeList.Add(new Bike() { speed = 40, color = Colors.Purple});
            bikeList.Add(new Bike() { speed = 20, color = Colors.Pink});
            bikeList.Add(new Bike() { speed = 25, color = Colors.Crimson});
            bikeList.Add(new Bike() { speed = 70, color = Colors.White});
            bikeList.Add(new Bike() { speed = 50, color = Colors.Black});
            bikeList.Add(new Bike() { speed = 80, color = Colors.Bisque });

            // ListBox에 데이터 할당
            LsbBikes.DataContext = bikeList;
        }

        private void LsbBikes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selItem = (LsbBikes.SelectedItem as Bike);
            MessageBox.Show(selItem.speed.ToString() + " / "+ selItem.color.ToString());
        }
    }
}
