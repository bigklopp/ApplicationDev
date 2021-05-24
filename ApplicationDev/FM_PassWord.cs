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
    public partial class FM_PassWord : Form
    {
        private SqlConnection Connect = null; // 데이터 베이스 접속 정보 관리
        private SqlTransaction Tran; // DB 관리 권한
        private SqlCommand cmd = new SqlCommand(); // DB 명령 전달
        public FM_PassWord()
        {
            InitializeComponent();
        }


        private void btnChgPW_Click(object sender, EventArgs e)
        {
            // 비밀 번호를 변경한다.


            // 데이터 베이스에 접속 한다.  

            string strCon = "DATA Source = 61.105.9.203; Initial Catalog = AppDev; " +
                "User ID=kfqs; Password=1234;";

            Connect = new SqlConnection(strCon);

            Connect.Open();

            // DB 접속이 되지 않았을 경우 메세지 출력.
            if (Connect.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("데이터 베이스 연결에 실패하였습니다.");
                return;
            }

            // 1. ID 존재 여부 확인
            string sLoginid = string.Empty; // 로그인 ID
            string sPerPw = string.Empty; // 이전 비밀번호
            string sNewPw = string.Empty; // 신규 비밀번호

            sLoginid = txtID.Text;
            sPerPw = TxtPrevPW.Text;
            sNewPw = TxtChgPW.Text;

            // SQL 조회문을 실행시키기 위한 어댑터 PW는 그냥 아무거나 쓴 거고 데이터 가져오기 위함.
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT PW FROM TB_USER_LHC WHERE USERID = '" + sLoginid + "'", Connect);
            // 데이터를 담을 그릇
            DataTable DtTemp = new DataTable(); // DataTable 은 2차원적 데이터 테이블
            // 어댑터 실행 후 그릇에 데이터 담기
            Adapter.Fill(DtTemp);
            // 데이터가 없는 경우 사용자가 없다고 메세지 및 리턴
            if (DtTemp.Rows.Count == 0)
            {
                MessageBox.Show("등록되지 않은 사용자 입니다.");
                return;
            }
            // 2. 이전 비밀번호가 일치하는지 확인
            else if (DtTemp.Rows[0]["PW"].ToString() != sPerPw)
            {
                MessageBox.Show("이전 비밀번호가 일치하지 않습니다.");
                return;
            }
            else
            {
                if (MessageBox.Show("해당 비밀번호로 변경 하시겠습니까?", "비밀번호 변경",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            Tran = Connect.BeginTransaction("TEST_Tran"); //트랜잭션 선언
            cmd.Transaction = Tran; // 커맨드에 트랜잭션 사용여부 등록
            cmd.Connection = Connect; // 커맨드에 접속 정보 입력
            cmd.CommandText = "UPDATE TB_USER_LHC SET PW = '" + sNewPw + "' " +
                "WHERE UserID ='" + sLoginid + "'";
            cmd.ExecuteNonQuery(); // C,U,D (Create, Update, Delete) 실행 함수 실행

            Tran.Commit(); // 변경 내용 승인
            MessageBox.Show("정상적으로 변경하였습니다.");
            this.Close();
            // 3. 바뀔 비밀번호로 등록
            // 4. 변경여부 메세지 처리

            //연습
            /*
             string strCon1 = "DATA Source = 61.105.9.203; Initial Catalog = AppDev;" +
                 "User ID =kfqs; Password =1234";
             SqlConnection Connect1 = new SqlConnection(strCon1);
             Connect1.Open();

             string LoginID = this.txtID.Text;
             string PrevPW = this.TxtPrevPW.Text;
             string NewPW = this.TxtChgPW.Text;

             if (Connect1.State != ConnectionState.Open)
             {
                 MessageBox.Show("연결 안 됨");
             }

             SqlDataAdapter Adapter1 = new SqlDataAdapter("SELECT * FROM TB_USER_LHC WHERE userID = '" + LoginID + "'",Connect1);

             DataTable DtTemp1 = new DataTable();
             Adapter1.Fill(DtTemp1);
             if (DtTemp1.Rows.Count == 0)
             {
                 MessageBox.Show("아이디가 없습니다.");
             }

             else if (DtTemp1.Rows[0]["PW"].ToString() != PrevPW)
             {
                 MessageBox.Show("패스워드가 다릅니다.");
             }

             else
             {
                 if (MessageBox.Show("패스워드를 변경하시겠습니까?","변경",
                     MessageBoxButtons.YesNo) == DialogResult.No)
                     {
                     return;
                 }
             }

             Tran = Connect1.BeginTransaction("TEST_Tran");
             cmd.Transaction = Tran;
             cmd.Connection = Connect1;
             cmd.CommandText = "UPDATE TB_USER_LHC SET PW ='" + NewPW + "'";
             cmd.ExecuteNonQuery();

             Tran.Commit();
             MessageBox.Show("정상적으로 변경되었습니다.");
             Connect1.Close();
*/
        }
    }
}
