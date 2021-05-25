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
    public partial class FM_ITEM : Form
    {
        private SqlConnection Connect = null;
        public FM_ITEM()
        {
            InitializeComponent();
        }
        private string sqlcon = "DATA Source = 61.105.9.203; Initial Catalog = AppDev; " +
                "User ID=kfqs1; Password=1234;";

        private void FM_ITEM_Load(object sender, EventArgs e)
        {
            try 
            { 
            Connect = new SqlConnection(sqlcon);
            Connect.Open();
            if (Connect.State != ConnectionState.Open)
            {
                MessageBox.Show("데이터베이스 연결에 실패하였습니다.");
                return;
            }


            // 조회할 때는 어댑터가 필요!!
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT DISTINCT ITEMDESC FROM TB_TESTITEM_LHC;", Connect);

            DataTable DtTemp = new DataTable();

            Adapter.Fill(DtTemp);

            cboItemDesc.DataSource = DtTemp; // 콤보박스 Item 목록은 SSMS에서 가져온 데이터로.
            cboItemDesc.DisplayMember = "ITEMDESC"; // 눈으로 보여줄 항목
            cboItemDesc.ValueMember = "ITEMDESC"; // 실제 항목
            cboItemDesc.Text = ""; // 콤보박스 빈칸으로 두기
                
                // 원하는 날짜 픽스
                dtpStart.Text = string.Format("{0:2000-01-01}",DateTime.Now);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"예외 발생 : {ex}");
            }
            finally
            {
                Connect.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Connect = new SqlConnection(sqlcon);
                Connect.Open();
                if(Connect.State != ConnectionState.Open)
                {
                    MessageBox.Show("데이터 베이스 접근에 실패했습니다.");
                    return;
                }

                string sItemCode = txtItemCode.Text; // 품목 코드
                string sItemName = txtItemName.Text; // 품목 명
                string sStartDate = dtpStart.Text; // 출시 시작 일자
                string sEndDate = dtpEnd.Text; // 출시 종료 일자
                string sItemDesc = cboItemDesc.Text; // 품목 상세
                string sEndFlag = "N"; // 단종 여부

                if (rdoEnd.Checked == true) sEndFlag = "Y";
                if (chkNameOnly.Checked == true) sItemCode = "";



                SqlDataAdapter Adapter = new SqlDataAdapter($"SELECT ITEMCODE, "          +
                                                            $"       ITEMNAME,"           +
                                                            $"       ITEMDESC,"           +
                                                            $"       ITEMDESC2,"          +
                                                            $"       CASE WHEN ENDFLAG = 'Y' THEN '단종'"   +
                                                            $"            WHEN ENDFLAG = 'N' THEN '생산'"   +
                                                            $"       END AS ENDFLAG,"                       +
                                                            $"       PRODDATE,"           +
                                                            $"       MAKEDATE,"           +
                                                            $"       MAKER,"              +
                                                            $"       EDITDATE,"           +
                                                            $"       EDITOR"              +
                                                            $"  FROM TB_TESTITEM_LHC WITH(NOLOCK)"          +
                                                            $" WHERE ITEMCODE LIKE '%{sItemCode}%'"         +
                                                            $"   AND ITEMNAME LIKE '%{sItemName}%'"         +
                                                            $"   AND ITEMDESC LIKE '%{sItemDesc}%'"         +
                                                            $"   AND ENDFLAG  = '{sEndFlag}'"               +
                                                            $"   AND PRODDATE BETWEEN '{sStartDate}' AND '{sEndDate}'", Connect);
                DataTable DtTemp = new DataTable();
                Adapter.Fill(DtTemp);
                if (DtTemp.Rows.Count == 0)
                {
                    dgvGrid.DataSource = null;
                    return; // return이 있으면 아래에 else가 없어도 된다. else{}가 들어가면 코드가 한 탭 밀려서 안 하는게 좋음.
                }
                dgvGrid.DataSource = DtTemp; // 데이터 그리드 뷰에 데이터 테이블 등록

                //그리드뷰의 헤더 명칭 선언
                dgvGrid.Columns["ITEMCODE"].HeaderText = "품목 코드";
                dgvGrid.Columns["ITEMNAME"].HeaderText = "품목 명";
                dgvGrid.Columns["ITEMDESC"].HeaderText = "품목 상세";
                dgvGrid.Columns["ITEMDESC2"].HeaderText = "품목 상세2";
                dgvGrid.Columns["PRODDATE"].HeaderText = "출시 일자";
                dgvGrid.Columns["ENDFLAG"].HeaderText = "단종 여부";
                dgvGrid.Columns["MAKEDATE"].HeaderText = "등록 일시";
                dgvGrid.Columns["MAKER"].HeaderText = "등록자";
                dgvGrid.Columns["EDITDATE"].HeaderText = "수정 일시";
                dgvGrid.Columns["EDITOR"].HeaderText = "수정자";

                // 그리드 뷰의 폭 지정. 컬럼 이름을 직접 쓸 수도 있지만 번호로 나타낼 수도 있음.
                dgvGrid.Columns[0].Width = 100;
                dgvGrid.Columns[1].Width = 200;
                dgvGrid.Columns[2].Width = 200;
                dgvGrid.Columns[3].Width = 200;
                dgvGrid.Columns[4].Width = 120;

                // 컬럼의 수정 여부를 지정한다. 
                dgvGrid.Columns["ITEMCODE"].ReadOnly = true;
                dgvGrid.Columns["MAKER"].ReadOnly = true;
                dgvGrid.Columns["MAKEDATE"].ReadOnly = true;
                dgvGrid.Columns["EDITOR"].ReadOnly = true;
                dgvGrid.Columns["EDITDATE"].ReadOnly = true;

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
            DataRow dr = ((DataTable) dgvGrid.DataSource).NewRow(); // dr 정의
            ((DataTable)dgvGrid.DataSource).Rows.Add(dr); // 빈 깡통 dr을 다시 dgv에 추가.
            dgvGrid.Columns["ITEMCODE"].ReadOnly = false; // 아이템 코드는 입력 가능???
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
                string Itemcode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = $"DELETE TB_TESTITEM_LHC WHERE ITEMCODE = '{Itemcode}'";

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
                if (MessageBox.Show("선택된 데이터를 등록 하시겠습니까?", "데이터 등록", MessageBoxButtons.YesNo) == DialogResult.No) return;

                string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                string sItemName = dgvGrid.CurrentRow.Cells["ITEMNAME"].Value.ToString();
                string sItemDesc = dgvGrid.CurrentRow.Cells["ITEMDESC"].Value.ToString();
                string sItemDesc2 = dgvGrid.CurrentRow.Cells["ITEMDESC2"].Value.ToString();

                string sItemEndFlag = dgvGrid.CurrentRow.Cells["ENDFLAG"].Value.ToString();
                //if (sItemEndFlag == "생산") sItemEndFlag = "N";
                //else sItemEndFlag = "Y";
                string sProdDate = dgvGrid.CurrentRow.Cells["PRODDATE"].Value.ToString();

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

                cmd.CommandText = "UPDATE TB_TestItem_LHC                                  " +
                                      "    SET ITEMNAME = '" + sItemName + "',             " +
                                      "        ITEMDESC = '" + sItemDesc + "',             " +
                                      "        ITEMDESC2 = '" + sItemDesc2 + "',            " +
                                      "        ENDFLAG = '" + "N" + "',              " +
                                      "        PRODDATE = '" + sProdDate + "',             " +
                                      "        EDITOR = '',  " +
                                      //"        EDITOR = '"    + Commoncs.LoginUserID + "',  " +
                                      "        EDITDATE = GETDATE()     " +
                                      "  WHERE ITEMCODE = '" + sItemCode + "'" +
                                      " IF (@@ROWCOUNT =0) " +
                                      "INSERT INTO TB_TestItem_LHC (ITEMCODE,           ITEMNAME,            ITEMDESC,           ITEMDESC2,          ENDFLAG,           PRODDATE,      MAKEDATE,     MAKER) " +
                                      "VALUES('" + sItemCode + "','" + sItemName + "','" + sItemDesc + "','" + sItemDesc2 + "','" + "N" + "','" + sProdDate + "',GETDATE(),'')";
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
