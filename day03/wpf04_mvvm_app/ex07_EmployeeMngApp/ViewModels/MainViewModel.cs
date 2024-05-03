using Caliburn.Micro;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ex07_EmployeeMngApp.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        // 멤버변수
        private int id;
        private string empName;
        private decimal salary;
        private string deptName;
        private string addr;
        
        // 속성
        public int Id { get => id; set => id = value; }
        public string EmpName { get => empName; set => empName = value; }
        public decimal Salary { get => salary; set => salary = value; }
        public string DeptName { get => deptName; set => deptName = value; }
        public string Addr { get => addr; set => addr = value; }

        public MainViewModel()
        {
            DisplayName = "직원관리 시스템";
        }

        /// <summary>
        /// Caliburn.Micro가 Xaml의 버튼 x:Name과 동일한 이름의 메서드로 매핑
        /// </summary>
        public void SaveEmployee()
        {
            //MessageBox.Show("저장버튼 동작!");
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Models.Employees.INSERT_QUERY;

                SqlParameter prmEmpName = new SqlParameter("@EmpName", EmpName);
                cmd.Parameters.Add(prmEmpName);
                SqlParameter prmSalary = new SqlParameter("@Salary", Salary);
                cmd.Parameters.Add(prmSalary);
                SqlParameter prmDeptName = new SqlParameter("@DeptName", DeptName);
                cmd.Parameters.Add(prmDeptName);
                SqlParameter prmAddr = new SqlParameter("@Addr", Addr);
                cmd.Parameters.Add(prmAddr);

                var result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("저장 성공!");
                }
                else
                {
                    MessageBox.Show("저장 실패!");
                }
            }
        }
    }
}
