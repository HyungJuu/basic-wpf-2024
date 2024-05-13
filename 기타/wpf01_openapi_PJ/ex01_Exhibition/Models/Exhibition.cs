using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Exhibition.Models
{
    public class Exhibition
    {
        public int res_no { get; set; }
        public string title { get; set; }
        public string op_st_dt { get; set; }
        public string op_ed_dt { get; set; }
        public string op_at { get; set; }
        public string place_id { get; set; }
        public string place_nm { get; set; }
        public int theme { get; set; }
        public string showtime { get; set; }
        public string rating { get; set; }
        public string price { get; set; }
        public string crew { get; set; }
        public string enterprice { get; set; }
        public int avg_star { get; set; }
        public string dabom_url { get; set; }


        public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[BusanExhibition]
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
                                                          FROM [dbo].[BusanExhibition]
                                                         WHERE CONVERT(CHAR(10), GETDATE(), 23) = @op_st_dt";

        public static readonly string GETDATE_QUERY = @"SELECT CONVERT(CHAR(10), op_st_dt, 23) AS Save_Date
                                                          FROM [dbo].[BusanExhibition]
                                                         GROUP BY CONVERT(CHAR(10), op_st_dt, 23)";
    }
}
