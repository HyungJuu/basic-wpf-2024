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

namespace ex03_BusanExhibition
{
    /// <summary>
    /// MapWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindow(string dabomUrl)
        {
            InitializeComponent();

            BrsLoc.Address = dabomUrl;
        }

        public string DabomUrl { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DabomUrl))
            {
                try
                {
                    // 웹 브라우저 열기
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(DabomUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"웹 브라우저를 열 수 없습니다: {ex.Message}");
                }
            }
        }


    }
}
