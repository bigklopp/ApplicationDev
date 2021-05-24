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



                SqlDataAdapter Adapter = new SqlDataAdapter($"SELECT ITEMCODE, " +
                                                            $"       ITEMNAME," +
                                                            $"       ITEMDESC," +
                                                            $"       ITEMDESC2," +
                                                            $"       ENDFLAG," +
                                                            $"       PRODDATE," +
                                                            $"       MAKEDATE," +
                                                            $"       MAKER," +
                                                            $"       EDITDATE," +
                                                            $"       EDITOR" +
                                                            $"  FROM TB_TESTITEM_LHC WITH(NOLOCK)" +
                                                            $" WHERE ITEMCODE LIKE '%{sItemCode}%'" +
                                                            $"   AND ITEMNAME LIKE '%{sItemName}%'" +
                                                            $"   AND ITEMDESC LIKE '%{sItemDesc}%'" +
                                                            $"   AND ENDFLAG  = '{sEndFlag}'", Connect);
                DataTable DtTemp = new DataTable();
                Adapter.Fill(DtTemp);
                if (DtTemp.Rows.Count == 0) return;
                dgvGrid.DataSource = DtTemp; // 데이터 그리드 뷰에 데이터 테이블 등록

                //그리드뷰의 헤더 명칭 선언
                dgvGrid.Columns["ITEMCODE"].HeaderText = "품목 코드";
                dgvGrid.Columns["ITEMNAME"].HeaderText = "품목 명";
                dgvGrid.Columns["ITEMDESC"].HeaderText = "품목 상세";
                dgvGrid.Columns["ITEMDESC2"].HeaderText = "품목 상세2";
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
                dgvGrid.Columns[4].Width = 100;

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
    }
}
