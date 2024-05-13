using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;

namespace ex02_Weather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GrdResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private async void BtnTodayweather_Click(object sender, RoutedEventArgs e)
        {
            string openApiUri = "https://apis.data.go.kr/1360000/MidFcstInfoService/getMidLandFcst?serviceKey=pcZPPoVs1hndQ8SFudvigsMLAkfSdP1omx9Acyi3QzzuXr%2FAksdF3cQZrVKIVc4CD%2FFUPyADPvHZQxUNnyVYGg%3D%3D&dataType=JSON&numOfRows=10&pageNo=1&regId=11H20000&tmFc=202405130600";
            string result = string.Empty;

            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                req = WebRequest.Create(openApiUri);
                res = await req.GetResponseAsync();
                reader = new StreamReader(res.GetResponseStream());
                result = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("오류", $"OpenAPI 조회오류 {ex.Message}");
            }

            var jsonResult = JObject.Parse(result);
            var numOfRows = Convert.ToInt32(jsonResult["numOfRows"]);

            


        }

    }
}