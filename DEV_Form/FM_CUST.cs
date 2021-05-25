using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DEV_Form
{
    public partial class FM_CUST : Form
    {
        private SqlConnection Connect = null;

        public FM_CUST()
        {
            InitializeComponent();
        }

        private string sqlcon = "DATA Source = 61.105.9.203; Initial Catalog = AppDev; " +
                "User ID=kfqs1; Password=1234;";

        private void FM_CUST_Load(object sender, EventArgs e)
        {
                // 원하는 날짜 픽스
                dtpStartDate.Text = string.Format("{0:yyyy-01-01}", DateTime.Now);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Connect = new SqlConnection(sqlcon);
                Connect.Open();
                if (Connect.State != ConnectionState.Open)
                {
                    MessageBox.Show("데이터 베이스 접근에 실패했습니다.");
                    return;
                }

                string sCUSTCode = txtCUSTCode.Text; // 품목 코드
                string sCUSTName = txtCUSTName.Text; // 품목 명
                string sStartDate = dtpStartDate.Text; // 출시 시작 일자
                string sEndDate = dtpEndDate.Text; // 출시 종료 일자
                string sBizType = "상용차 부품";
                string sCUSTType = "";

                if (rdoCar.Checked == true) sBizType = "자동차부품";
                else if (rdoCut.Checked == true) sBizType = "절삭가공";
                else if (rdoPumpCom.Checked == true) sBizType = "펌프압축";




                if (chkCUSTOnly.Checked == true)
                {
                    sCUSTCode = "";
                    sCUSTType = "C";
                }

                

                SqlDataAdapter Adapter = new SqlDataAdapter($"SELECT CUSTCODE, " +
                                                            $"       CUSTTYPE," +
                                                            $"       CUSTNAME," +
                                                            $"       BIZCLASS," +
                                                            $"       BIZTYPE," +
                                                            $"       USEFLAG," +
                                                            $"       FIRSTDATE," +
                                                            $"       MAKEDATE," +
                                                            $"       MAKER," +
                                                            $"       EDITDATE," +
                                                            $"       EDITOR" +
                                                            $"  FROM TB_CUST_LHC WITH(NOLOCK)" +
                                                            $" WHERE CUSTCODE LIKE '%{sCUSTCode}%'" +
                                                            $"   AND CUSTNAME LIKE '%{sCUSTName}%'" +
                                                            $"   AND BIZTYPE  = '{sBizType}'" +
                                                            $"   AND CUSTTYPE LIKE '%{sCUSTType}%'" +
                                                            $"   AND FIRSTDATE BETWEEN '{sStartDate}' AND '{sEndDate}'", Connect);
                DataTable DtTemp = new DataTable();
                Adapter.Fill(DtTemp);
                if (DtTemp.Rows.Count == 0)
                {
                    dgvGrid.DataSource = null;
                    return; // return이 있으면 아래에 else가 없어도 된다. else{}가 들어가면 코드가 한 탭 밀려서 안 하는게 좋음.
                }
                for ( int i = 0; i< DtTemp.Rows.Count;i++)
                {

                    
                    if(DtTemp.Rows[i]["USEFLAG"].ToString()== "Y") DtTemp.Rows[i]["USEFLAG"] = "사용";
                    else DtTemp.Rows[i]["USEFLAG"] = "미사용";
                }

                for (int i = 0; i < DtTemp.Rows.Count; i++)
                {


                    if (DtTemp.Rows[i]["CUSTTYPE"].ToString() == "C") DtTemp.Rows[i]["CUSTTYPE"] = "고객사";
                    else DtTemp.Rows[i]["CUSTTYPE"] = "협력사";
                }
                dgvGrid.DataSource = DtTemp; // 데이터 그리드 뷰에 데이터 테이블 등록

                //그리드뷰의 헤더 명칭 선언
                dgvGrid.Columns["CUSTCODE"].HeaderText = "거래처 코드";
                dgvGrid.Columns["CUSTTYPE"].HeaderText = "거래처 타입";
                dgvGrid.Columns["CUSTNAME"].HeaderText = "거래처 명";
                dgvGrid.Columns["BIZCLASS"].HeaderText = "업태";
                dgvGrid.Columns["BIZTYPE"].HeaderText = "종목";
                dgvGrid.Columns["USEFLAG"].HeaderText = "사용여부";
                dgvGrid.Columns["FIRSTDATE"].HeaderText = "거래일자";
                dgvGrid.Columns["MAKEDATE"].HeaderText = "등록일시";
                dgvGrid.Columns["MAKER"].HeaderText = "등록자";
                dgvGrid.Columns["EDITDATE"].HeaderText = "수정일시";
                dgvGrid.Columns["EDITOR"].HeaderText = "수정자";

                // 그리드 뷰의 폭 지정. 컬럼 이름을 직접 쓸 수도 있지만 번호로 나타낼 수도 있음.
                dgvGrid.Columns[0].Width = 120;
                dgvGrid.Columns[1].Width = 120;
                dgvGrid.Columns[2].Width = 120;
                dgvGrid.Columns[3].Width = 120;
                dgvGrid.Columns[5].Width = 120;
                dgvGrid.Columns[6].Width = 120;
                dgvGrid.Columns[7].Width = 200;
                dgvGrid.Columns[8].Width = 120;
                dgvGrid.Columns[9].Width = 200;
                /*
                // 컬럼의 수정 여부를 지정한다. 
                dgvGrid.Columns["CUSTCODE"].ReadOnly = true;
                dgvGrid.Columns["CUSTTYPE"].ReadOnly = true;
                dgvGrid.Columns["MAKEDATE"].ReadOnly = true;
                dgvGrid.Columns["EDITOR"].ReadOnly = true;
                dgvGrid.Columns["EDITDATE"].ReadOnly = true;
                */
                dgvGrid.Columns["CUSTCODE"].ReadOnly = true;
                dgvGrid.Columns["CUSTTYPE"].ReadOnly = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"예외 발생 {ex}");
            }
            finally { Connect.Close(); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 데이터 그리드 뷰에 신규 레코드를 추가
            DataRow dr = ((DataTable)dgvGrid.DataSource).NewRow(); // dr 정의
            ((DataTable)dgvGrid.DataSource).Rows.Add(dr); // 빈 깡통 dr을 다시 dgv에 추가.
            dgvGrid.Columns["CUSTCODE"].ReadOnly = false; // 아이템 코드는 입력 가능???
            dgvGrid.Columns["CUSTTYPE"].ReadOnly = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 데이터 그리드 뷰에서 선택된 행을 삭제

            if (this.dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택된 데이터를 삭제하시겠습니까?", "데이터 삭제", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            SqlCommand cmd = new SqlCommand();
            SqlTransaction Tran;

            Connect = new SqlConnection(sqlcon);
            Connect.Open();

            Tran = Connect.BeginTransaction("TranStart");
            cmd.Transaction = Tran;
            cmd.Connection = Connect;
            try
            {
                string Custcode = dgvGrid.CurrentRow.Cells["CUSTCODE"].Value.ToString();
                cmd.CommandText = $"DELETE TB_CUST_LHC WHERE CUSTCODE = '{Custcode}'";

                cmd.ExecuteNonQuery();

                // 성공시 DB Commit
                Tran.Commit();
                MessageBox.Show("정상적으로 삭제하였습니다.");
                btnSearch_Click(null, null);      // 정상적으로 삭제된 것을 보여주기 위해 조회버튼 한 번 눌러줌.          
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                MessageBox.Show($"데이터 삭제에 실패하였습니다. : {ex}");
            }
            finally
            {
                Connect.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 선택된 행 데이터 저장
                if (dgvGrid.Rows.Count == 0) return;
                // 거래처코드 및 거래처 타입, 거래 일자 미입력 후 저장 시 메시지 호출)
                if (dgvGrid.CurrentRow.Cells["CUSTCODE"].Value.ToString() == "" ||
                    dgvGrid.CurrentRow.Cells["CUSTTYPE"].Value.ToString() == "" ||
                    dgvGrid.CurrentRow.Cells["FIRSTDATE"].Value.ToString() == "")
                {
                    MessageBox.Show("거래처코드 및 거래처 타입, 거래 일자는 필수로 입력해 주세요.");
                    return;
                }    

                if (MessageBox.Show("선택된 데이터를 등록 하시겠습니까?", "데이터 등록", MessageBoxButtons.YesNo) == DialogResult.No) return;

                string sCustCode = dgvGrid.CurrentRow.Cells["CUSTCODE"].Value.ToString();
                string sCustName = dgvGrid.CurrentRow.Cells["CUSTNAME"].Value.ToString();

                string sCustType = dgvGrid.CurrentRow.Cells["CUSTNAME"].Value.ToString();

                if (sCustType == "협력사") sCustType = "V";
                else sCustType = "C";
                string sBizClass = dgvGrid.CurrentRow.Cells["BIZCLASS"].Value.ToString();
                string sBizType = dgvGrid.CurrentRow.Cells["BIZTYPE"].Value.ToString();

                string sUseFlag = dgvGrid.CurrentRow.Cells["USEFLAG"].Value.ToString();
                if (sUseFlag == "미사용") sUseFlag = "N";
                else sUseFlag = "Y";
                string sFirstDate = dgvGrid.CurrentRow.Cells["FIRSTDATE"].Value.ToString();
                
                SqlCommand cmd = new SqlCommand();
                SqlTransaction Transaction;

                Connect = new SqlConnection(sqlcon);
                Connect.Open();

                // 데이터 조회 후 해당 데이터가 있는지 확인 후 UPDATE, INSERT 구문 분기
                //string sSql = $"SELECT ITEMCODE FROM TB_TESTITEM_LHC WHERE ITEMCODE = '{sItemCode}'";
                //SqlDataAdapter Adapter = new SqlDataAdapter(sSql, Connect);
                //DataTable DtTemp = new DataTable();
                //Adapter.Fill(DtTemp);

                // 트랜잭션 설정
                Transaction = Connect.BeginTransaction("TestTran");
                cmd.Transaction = Transaction;
                cmd.Connection = Connect;


                // 데이터가 있는 경우 UPDATE, 없는 경우 INSERT
                //if (DtTemp.Rows.Count == 0)
                //{
                //    // 데이터가 없으니 INTSERT 해라.
                //    cmd.CommandText = "INSERT INTO TB_TESTITEM_LHC (ITEMCODE, ITEMNAME, ITEMDESC, ITEMDESC2, ENDFLAG, PRODDATE, MAKEDATE, MAKER) " +
                //                      $"VALUES ('{sItemCode}', '{sItemName}', '{sItemDesc}', '{sItemDesc2}', 'N', '{sProdDate}', GETDATE(), '')";

                //}
                //else
                //{
                //    //데이터가 없으니 UPDATE 해라
                //    cmd.CommandText = "UPDATE TB_TestItem_LHC                                  " +
                //                      "    SET ITEMNAME = '" + sItemName + "',             " +
                //                      "        ITEMDESC = '" + sItemDesc + "',             " +
                //                      "        ITEMDESC2 = '" + sItemDesc2 + "',            " +
                //                      "        ENDFLAG = '" + "N" + "',              " +
                //                      "        PRODDATE = '" + sProdDate + "',             " +
                //                      "        EDITOR = '',  " +
                //                      //"        EDITOR = '"    + Commoncs.LoginUserID + "',  " +
                //                      "        EDITDATE = GETDATE()     " +
                //                      "  WHERE ITEMCODE = '" + sItemCode + "'";
                //}
                //cmd.ExecuteNonQuery(); // 위에서 작성한 CommandText 구문을 실행

                // SSMS 내에서 바로 SELECT문으로 INSERT, UPDATE 알아서 처리하는 것.

                cmd.CommandText = "UPDATE TB_CUST_LHC                                  " +
                                      "    SET CUSTCODE = '" + sCustCode + "',             " +
                                      "        CUSTTYPE = '" + sCustType + "',             " +
                                      "        CUSTNAME = '" + sCustName + "',             " +
                                      "        BIZCLASS = '" + sBizClass + "',            " +
                                      "        BIZTYPE = '" + sBizType + "',            " +
                                      "        USEFLAG = '" + sUseFlag + "',              " +
                                      "        FIRSTDATE = '" + sFirstDate + "',             " +
                                      "        EDITOR = '" + Common.LogInID + "'," +
                                      "        EDITDATE = GETDATE()     " +
                                      "  WHERE CUSTCODE = '" + sCustCode + "'" +
                                      " IF (@@ROWCOUNT =0) " +
                                      "INSERT INTO TB_CUST_LHC (CUSTCODE, CUSTTYPE,          CUSTNAME,            BIZCLASS,           BIZTYPE,          USEFLAG,           FIRSTDATE,      MAKEDATE,     MAKER) " +
                                      "VALUES('" + sCustCode + "','" + sCustType + "','" + sCustName + "','" + sBizClass + "','" + sBizType + "','" + sUseFlag + "','" + sFirstDate + $"',GETDATE(),'{Common.LogInID}')";
                cmd.ExecuteNonQuery();
                // 성공시 DB COMMIT
                Transaction.Commit();
                MessageBox.Show("정상적으로 등록하였습니다.");
                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"예외 {ex}");
            }
            finally
            {
                Connect.Close();
            }
        }
    }
}
