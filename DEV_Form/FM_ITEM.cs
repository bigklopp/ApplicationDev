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
using System.IO;

namespace DEV_Form
{
    public partial class FM_ITEM : Form , ChildInterFace
    {
        private SqlConnection Connect = null;
        public FM_ITEM()
        {
            InitializeComponent();
        }
        private string sqlcon = "DATA Source = 222.235.141.8; Initial Catalog = AppDev; " +
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
            if (this.dgvGrid.Rows.Count == 0) return;
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
                                      "        EDITOR = '" + Common.LogInID + "'," +
                                      "        EDITDATE = GETDATE()     " +
                                      "  WHERE ITEMCODE = '" + sItemCode + "'" +
                                      " IF (@@ROWCOUNT =0) " +
                                      "INSERT INTO TB_TestItem_LHC (ITEMCODE,           ITEMNAME,            ITEMDESC,           ITEMDESC2,          ENDFLAG,           PRODDATE,      MAKEDATE,     MAKER) " +
                                      "VALUES('" + sItemCode + "','" + sItemName + "','" + sItemDesc + "','" + sItemDesc2 + "','" + "N" + "','" + sProdDate + $"',GETDATE(),'{Common.LogInID}')";
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

        private void btnPicLoad_Click(object sender, EventArgs e)
        {
            // 이미지 불러오기 및 저장, 파일 탐색기 호출
            string sImageFile = string.Empty;

            OpenFileDialog Dialog = new OpenFileDialog();
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                sImageFile = Dialog.FileName;
                picItemImage.Tag = Dialog.FileName;

                // 지정된 파일에서 이미지를 만들어 픽쳐박스에 넣는다.
                picItemImage.Image = Bitmap.FromFile(sImageFile);
            }


        }

        private void picItemImage_Click(object sender, EventArgs e)
        {
            // 픽쳐박스 크기 최대화 및 이전 사이즈로
            if(this.picItemImage.Dock == System.Windows.Forms.DockStyle.Fill)
            {
                //이미지가 가득 채워져 있는 상태이면 원래 상태로 바꾸기.
                this.picItemImage.Dock = System.Windows.Forms.DockStyle.None;
            }
            else
            {
                // 이미지가 가득 채워져 있지 않으면 가득 채우기
                picItemImage.Dock = System.Windows.Forms.DockStyle.Fill;
                // 이미지를 가장 앞으로 가지고 온다.
                picItemImage.BringToFront();
            }    
        }

        private void btnPicSave_Click(object sender, EventArgs e)
        {
            // 픽쳐박스 이미지 저장.
            if (dgvGrid.Rows.Count == 0) return;
            if (picItemImage.Image == null) return;
            if (picItemImage.Tag.ToString() == "") return;

            if (MessageBox.Show("선택된 이미지로 등록하시겠습니까?", "이미지 등록", MessageBoxButtons.YesNo) == DialogResult.No) return;
            Byte[] bImage = null;
            Connect = new SqlConnection(Common.db);

            try
            {
                // database에다가 그림을 넣자.
                // 바이너리 코드는 컴퓨터가 인식할 수 있는 0과 1로 구성된 이진코드
                // 바이트 코드는 CPU가 아닌 가상 머신에서 이해할 수 있는 코드

                // 파일을 불러오기 위한 파일 경로 방법 지정
                FileStream stream = new FileStream(picItemImage.Tag.ToString(), FileMode.Open, FileAccess.Read);
                // 읽어들인 파일을 바이너리 코드로 변환
                BinaryReader reader = new BinaryReader(stream);
                // 만들어진 바이너리 코드 이미지를 Byte화 하여 저장. 
                bImage = reader.ReadBytes(Convert.ToInt32(stream.Length));
                reader.Close();
                stream.Close();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connect;
                Connect.Open();

                string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = "UPDATE TB_TESTITEM_LHC"      +
                                  "   SET ITEMIMG = @IMAGE"     +
                                  " WHERE ITEMCODE = @ITEMCODE ";
                cmd.Parameters.AddWithValue("@IMAGE", bImage);  // @IMAGE에 bImage를 넣어서 사용
                cmd.Parameters.AddWithValue("@ITEMCODE", sItemCode); // @ITEMCODE에 sItemCode를 넣어서 사용
                cmd.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show($"이미지가 등록되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"예외 발생 : {ex}");
            }
        }

        private void dgvGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 선택시 해당 품목 이미지 가져오기.
            string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();

            Connect = new SqlConnection(Common.db);
            Connect.Open();
            try
            {
                //이미지 초기화
                picItemImage.Image = null;
                string ssql = $"SELECT ITEMIMG FROM TB_TESTITEM_LHC WHERE ITEMCODE = '{sItemCode}'" +
                              $"   AND ITEMIMG IS NOT NULL"; 
                // 이 부분 NULL이 아닌 것만 가져온다! null인 것 가져오면 아래 카운트가 1로 잡혀
                // 예외가 발생한다. 
                SqlDataAdapter Adapter = new SqlDataAdapter(ssql, Connect);
                DataTable DtTemp =new DataTable();
                Adapter.Fill(DtTemp);

                if (DtTemp.Rows.Count == 0) return;
                byte[] bImage = null;
                bImage = (byte[])DtTemp.Rows[0]["ITEMIMG"];  // 이미지를 byte화 한다.

                if(bImage != null)
                {
                    picItemImage.Image = new Bitmap(new MemoryStream(bImage)); // 메모리 스트림을 이용하여 파일을 그려본다.!
                    picItemImage.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"예외 발생 {ex}");
            }
            finally
            {
                Connect.Close();
            }
        }

        private void btnPicDelete_Click(object sender, EventArgs e)
        {
            // 품목에 저장된 이미지 삭제
            if (this.dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택된 이미지를 삭제하시겠습니까?", "이미지 삭제", MessageBoxButtons.YesNo)
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
                string sItemcode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = $"UPDATE TB_TESTITEM_LHC " +
                                  $"   SET ITEMIMG = NULL" +
                                  $" WHERE ITEMCODE = '{sItemcode}'";

                cmd.ExecuteNonQuery();
                Tran.Commit();

                picItemImage.Image = null; // 성공하고 나서 표시되는 화면을 빈 칸으로
                // 다시 바꿔줘서 이미지가 삭제된 것처럼 나타내어 준다.


                MessageBox.Show("정상적으로 삭제하였습니다.");
                //btnSearch_Click(null, null);(긁어온 흔적) 정상적으로 삭제된 것을 보여주기 위해 조회버튼 한 번 눌러줌.          
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

        public void Inquire()
        {
            btnSearch_Click(null, null);
        }

        public void DoNew()
        {
            btnAdd_Click(null, null);
        }

        public void Delete()
        {
            btnDelete_Click(null, null);
        }

        public void Save()
        {
            btnSave_Click(null, null);
        }
    }
}
