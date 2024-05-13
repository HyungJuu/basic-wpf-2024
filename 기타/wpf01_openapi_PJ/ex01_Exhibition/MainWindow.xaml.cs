using System.Data;
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
using ex01_Exhibition.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace ex01_Exhibition
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitComboDateFromDB();
        }

        private void InitComboDateFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Models.Exhibition.GETDATE_QUERY, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dSet = new DataSet();

                adapter.Fill(dSet);
                List<string> saveDates = new List<string>();

                foreach (DataRow row in dSet.Tables[0].Rows)
                {
                    saveDates.Add(Convert.ToString(row["Save_Date"]));
                }
                CboReqDate.ItemsSource = saveDates;
            }
        }

        private async void BtnExhibitionCheck_Click(object sender, RoutedEventArgs e)
        {
            string openApiUri = "https://apis.data.go.kr/6260000/BusanCultureExhibitDetailService/getBusanCultureExhibitDetail?serviceKey=pcZPPoVs1hndQ8SFudvigsMLAkfSdP1omx9Acyi3QzzuXr%2FAksdF3cQZrVKIVc4CD%2FFUPyADPvHZQxUNnyVYGg%3D%3D&numOfRows=100&resultType=json";
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
            var status = Convert.ToInt32(jsonResult["status"]);

            if (status == 200)
            {
                var data = jsonResult["data"];
                var jsonArray = data as JArray; // json 자체에서 [] 안의 배열데이터만 JArray 변환가능

                var exhibitions = new List<Exhibition>();
                foreach (var item in jsonArray)
                {
                    exhibitions.Add(new Exhibition()
                    {
                        res_no = Convert.ToInt32(item["res_no"]),
                        title = Convert.ToString(item["title"]),
                        op_st_dt = Convert.ToString(item["op_st_dt"]),
                        op_ed_dt = Convert.ToString(item["op_ed_dt"]),
                        op_at = Convert.ToString(item["op_at"]),
                        place_id = Convert.ToString(item["place_id"]),
                        place_nm = Convert.ToString(item["place_nm"]),
                        theme = Convert.ToInt32(item["theme"]),
                        showtime = Convert.ToString(item["showtime"]),
                        rating = Convert.ToString(item["rating"]),
                        price = Convert.ToString(item["price"]),
                        crew = Convert.ToString(item["crew"]),
                        enterprice = Convert.ToString(item["enterprice"]),
                        avg_star = Convert.ToInt32(item["avg_star"]),
                        dabom_url = Convert.ToString(item["dabom_url"]),

                    });
                }

                this.DataContext = exhibitions;
                StsResult.Content = $"OpenAPI {exhibitions.Count}건 조회완료!";
            }
        }

        private void CboReqDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GrrdResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}