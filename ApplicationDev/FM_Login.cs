using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationDev
{
    public partial class FM_Login : Form
    {
        private SqlConnection Connect = null; // 데이터 베이스 접속 정보 관리
        private SqlTransaction Tran; // DB 관리 권한
        private SqlCommand cmd = new SqlCommand(); // DB 명령 전달
        private int WrongPWCount = 0;
        public FM_Login()
        {
            InitializeComponent();
            this.Tag = "FAIL";
        }

        private void btnPwdChg_Click(object sender, EventArgs e)
        {
            //비밀번호 변경 확인 팝업을 호출한다.
            this.Visible = false; //팝업 호출 시 로그인 화면을 보이지 않게 한다.
            FM_PassWord FmPassWord = new FM_PassWord();
            FmPassWord.ShowDialog();
            this.Visible = true; // 팝업을 닫으면 다시 로그인 화면을 보이게 한다.
            this.Text = "디버깅 모드 소스 수정";
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            // 실제로 데이터 베이스에 있는 것과 맞으면 반갑습니다. 메세지
            // 비밀번호 3회 이상 틀리면 어플리케이션 종료
            try
            {

            
            string strCon = "DATA Source = 61.105.9.203; Initial Catalog = AppDev; " +
                "User ID=kfqs1; Password=1234;";
            Connect = new SqlConnection(strCon);
            Connect.Open();

            string userID = txtID.Text;
            string PW = txtPassword.Text;

            if (Connect.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("데이터 베이스 연결에 실패하였습니다.");
                return;
            }

            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT PW, UserName FROM TB_USER_LHC WHERE UserID = '" + userID + "'", Connect);
            DataTable DtTemp = new DataTable();
            Adapter.Fill(DtTemp);




            if (DtTemp.Rows.Count == 0)
            {
                MessageBox.Show("등록되지 않은 사용자 입니다.");
                txtID.Text = "";
                txtID.Focus();
                return;
            }
            // 2. 이전 비밀번호가 일치하는지 확인
            else if (DtTemp.Rows[0]["PW"].ToString() != PW)
            {
                WrongPWCount++;
                if (WrongPWCount == 3)
                {
                    MessageBox.Show("비밀번호가 3회 이상 틀렸습니다. 종료합니다.");
                    this.Close();
                    return;
                }
                MessageBox.Show($"비밀번호가 일치하지 않습니다. 3회 이상 틀리면 종료합니다. 누적 {WrongPWCount} 회");
                return;
            }
            else
            {
                MessageBox.Show("환영합니다.");

                    // 유저 명을 Common에 등록함.                                                                                                                                                                                                                        
                    Common.LogInID = txtID.Text;
                    Common.LogInName = DtTemp.Rows[0]["USERNAME"].ToString();


                    this.Tag = DtTemp.Rows[0]["UserName"].ToString(); // 유저 명을 메인화면으로 보냄??
                    //MessageBox.Show($"{this.Tag}");

                    this.Close();
            }

            }// try
            catch (Exception ex)
            {
                MessageBox.Show("로그인 작업 중 오류가 발생하였습니다." + ex.ToString());
                
            }
            finally
            {
                Connect.Close();
            }
        }

        

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}
