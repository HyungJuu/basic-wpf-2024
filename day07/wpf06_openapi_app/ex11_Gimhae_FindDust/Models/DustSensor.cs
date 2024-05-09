using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex11_Gimhae_FindDust.Models
{
    public class DustSensor
    {
        public int Id { get; set; } // 추가생성. DB에 넣을때 사용할 값
        public string Dev_id { get; set; } // 디바이스 아이디
        public string Name { get; set; } // 디바이스 이름
        public string Loc { get; set; } // 디바이스 위치주소
        public double Coordx { get; set; } // 경도
        public double Coordy { get; set; } // 위도
        public bool Ison { get; set; }  // 디바이스 온 여부
        public int Pm10_after { get; set; } // PM 10mm 미세먼지 측정값 
        public int Pm25_after { get; set; } // PM 2.5mm 초미세먼지 측정값
        public int State { get; set; } // 상태
        public DateTime Timestamp { get; set; } // 측정일시
        public string Company_id { get; set; } // 회사아이디
        public string Company_name { get; set; } // 회사명
        
        public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[Dustsensor]
                                                                 ( [Dev_id]
                                                                 , [Name]
                                                                 , [Loc]
                                                                 , [Coordx]
                                                                 , [Coordy]
                                                                 , [Ison]
                                                                 , [Pm10_after]
                                                                 , [Pm25_after]
                                                                 , [State]
                                                                 , [Timestamp]
                                                                 , [Company_id]
                                                                 , [Company_name])
                                                            VALUES
                                                                 ( @Dev_id
                                                                 , @Name
                                                                 , @Loc
                                                                 , @Coordx
                                                                 , @Coordy
                                                                 , @Ison
                                                                 , @Pm10_after
                                                                 , @Pm25_after
                                                                 , @State
                                                                 , @Timestamp
                                                                 , @Company_id
                                                                 , @Company_name)";

        public static readonly string SELECT_QUERY = @"SELECT [Id]
                                                            , [Dev_id]
                                                            , [Name]
                                                            , [Loc]
                                                            , [Coordx]
                                                            , [Coordy]
                                                            , [Ison]
                                                            , [Pm10_after]
                                                            , [Pm25_after]
                                                            , [State]
                                                            , [Timestamp]
                                                            , [Company_id]
                                                            , [Company_name]
                                                         FROM [dbo].[Dustsensor]
                                                        WHERE CONVERT(CHAR(10), GETDATE(), 23) = @Timestamp";

        public static readonly string GETDATE_QUERY = @"SELECT CONVERT(CHAR(10), Timestamp, 23) AS Save_Date
                                                          FROM [dbo].[DustSensor]
                                                         GROUP BY CONVERT(CHAR(10), Timestamp, 23)";
    }
}
