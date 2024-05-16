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
using Microsoft.Data.SqlClient;
using System.Data;
using ex03_BusanExhibition.Models;
using System.Diagnostics;

namespace ex03_BusanExhibition
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

        private async void BtnReqRealtime_Click(object sender, RoutedEventArgs e)
        {
            string openApiUri = "https://apis.data.go.kr/6260000/BusanCultureExhibitDetailService/getBusanCultureExhibitDetail?serviceKey=pcZPPoVs1hndQ8SFudvigsMLAkfSdP1omx9Acyi3QzzuXr%2FAksdF3cQZrVKIVc4CD%2FFUPyADPvHZQxUNnyVYGg%3D%3D&numOfRows=5000&resultType=json";
            string result = string.Empty;

            // WebRequest, WebResponse 객체
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
            var status = Convert.ToString(jsonResult["getBusanCultureExhibitDetail"]["header"]["code"]); // getBusanCultureExhibitDetail.header 아래 item에 정보가 저장되어 있음

            if (status == "00") // header의 code가 00 일때 item의 데이터를 가져온다
            {
                var data = jsonResult["getBusanCultureExhibitDetail"]["item"];
                var jsonArray = data as JArray;

                var exhibitions = new List<Exhibition>();
                foreach (var item in jsonArray)
                {
                    exhibitions.Add(new Exhibition()
                    {
                        Id = 0,
                        res_no = Convert.ToInt32(item["res_no"]),
                        title = Convert.ToString(item["title"]),
                        op_st_dt = Convert.ToString(item["op_st_dt"]),
                        op_ed_dt = Convert.ToString(item["op_ed_dt"]),
                        op_at = Convert.ToString(item["op_at"]),
                        place_id = Convert.ToString(item["place_id"]),
                        place_nm = Convert.ToString(item["place_nm"]),
                        theme = Convert.ToString(item["theme"]),
                        showtime = Convert.ToString(item["showtime"]),
                        rating = Convert.ToString(item["rating"]),
                        price = Convert.ToString(item["price"]),
                        crew = Convert.ToString(item["crew"]),
                        enterprice = Convert.ToString(item["enterprice"]),
                        avg_star = Convert.ToInt32(item["avg_star"]),
                        dabom_url = Convert.ToString(item["dabom_url"])

                    });
                }

                this.DataContext = exhibitions;
                StsResult.Content = $"OpenAPI {exhibitions.Count}건 조회완료!";
            }
        }

        private async void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            if (GrdResult.Items.Count == 0)
            {
                await this.ShowMessageAsync("경고", "조회 후 저장하세요.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    var insRes = 0;
                    foreach (Exhibition item in GrdResult.Items)
                    {
                        SqlCommand cmd = new SqlCommand(Models.Exhibition.INSERT_QUERY, conn);
                        cmd.Parameters.AddWithValue("@res_no", item.res_no);
                        cmd.Parameters.AddWithValue("@title", item.title);
                        cmd.Parameters.AddWithValue("@op_st_dt", item.op_st_dt);
                        cmd.Parameters.AddWithValue("@op_ed_dt", item.op_ed_dt);
                        cmd.Parameters.AddWithValue("@op_at", item.op_at);
                        cmd.Parameters.AddWithValue("@place_id", item.place_id);
                        cmd.Parameters.AddWithValue("@place_nm", item.place_nm);
                        cmd.Parameters.AddWithValue("@theme", item.theme);
                        cmd.Parameters.AddWithValue("@showtime", item.showtime);
                        cmd.Parameters.AddWithValue("@rating", item.rating);
                        cmd.Parameters.AddWithValue("@price", item.price);
                        cmd.Parameters.AddWithValue("@crew", item.crew);
                        cmd.Parameters.AddWithValue("@enterprice", item.enterprice);
                        cmd.Parameters.AddWithValue("@avg_star", item.avg_star);
                        cmd.Parameters.AddWithValue("@dabom_url", item.dabom_url);

                        insRes += cmd.ExecuteNonQuery();
                    }

                    if (insRes > 0)
                    {
                        await this.ShowMessageAsync("저장", "DB저장성공!");
                        StsResult.Content = $"DB저장 {insRes}건 성공!";
                    }
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("저장오류", $"저장오류 {ex.Message}");
            }

            InitComboDateFromDB();
        }


        // 전시회 더블클릭으로 url 열기
        private void GrrdResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var curItem = GrdResult.SelectedItem as Exhibition;

            // Exhibition 객체에서 dabom_url 가져오기
            string dabomUrl = curItem.dabom_url;

            // dabom_url이 존재하는 경우에만 MapWindow 열기
            if (!string.IsNullOrEmpty(dabomUrl))
            {
                // MapWindow 열기
                var mapWindow = new MapWindow(dabomUrl);
                mapWindow.Owner = this;
                mapWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                mapWindow.ShowDialog();
            }
        }


        private void CboReqDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            string SearchYear = TxtYear.Text.Trim('-');
            if (!string.IsNullOrEmpty(SearchYear) && SearchYear.Length == 4)
            {

            }
        }
    }
}