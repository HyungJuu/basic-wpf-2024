using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03_BusanExhibition.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        public int res_no { get; set; } // 공연번호
        public string title { get; set; } // 제목
        public string op_st_dt { get; set; } // 전시시작일
        public string op_ed_dt { get; set; } // 전시종료일
        public string op_at { get; set; } // 오픈런
        public string place_id { get; set; } // 시설ID
        public string place_nm { get; set; } // 시설명
        public string theme { get; set; } // 테마코드
        public string showtime { get; set; } // 관람회차
        public string rating { get; set; } // 관람연령
        public string price { get; set; } // 가격
        public string crew { get; set; } // 제작진
        public string enterprice { get; set; } // 기획사
        public int avg_star { get; set; } // 평균별점
        public string dabom_url { get; set; } // 다봄 url

        public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[Exhibition]
                                                                 ( [res_no]
                                                                 , [title]
                                                                 , [op_st_dt]
                                                                 , [op_ed_dt]
                                                                 , [op_at]
                                                                 , [place_id]
                                                                 , [place_nm]
                                                                 , [theme]
                                                                 , [showtime]
                                                                 , [rating]
                                                                 , [price]
                                                                 , [crew]
                                                                 , [enterprice]
                                                                 , [avg_star]
                                                                 , [dabom_url])
                                                            VALUES
                                                                 ( @res_no
                                                                 , @title
                                                                 , @op_st_dt
                                                                 , @op_ed_dt
                                                                 , @op_at
                                                                 , @place_id
                                                                 , @place_nm
                                                                 , @theme
                                                                 , @showtime
                                                                 , @rating
                                                                 , @price
                                                                 , @crew
                                                                 , @enterprice
                                                                 , @avg_star
                                                                 , @dabom_url)";

        public static readonly string SELECT_QUERY = @"SELECT [res_no]
                                                              ,[title]
                                                              ,[op_st_dt]
                                                              ,[op_ed_dt]
                                                              ,[op_at]
                                                              ,[place_id]
                                                              ,[place_nm]
                                                              ,[theme]
                                                              ,[showtime]
                                                              ,[rating]
                                                              ,[price]
                                                              ,[crew]
                                                              ,[enterprice]
                                                              ,[avg_star]
                                                              ,[dabom_url]
                                                          FROM [dbo].[Exhibition]
                                                         WHERE CONVERT(CHAR(10), GETDATE(), 23) = @op_st_dt";

        public static readonly string GETDATE_QUERY = @"SELECT CONVERT(CHAR(10), op_st_dt, 23) AS Save_Date
                                                          FROM [dbo].[Exhibition]
                                                         GROUP BY CONVERT(CHAR(10), op_st_dt, 23)";
    }
}
