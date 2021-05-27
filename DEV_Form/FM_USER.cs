using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEV_Form
{
    public partial class FM_USER : BaseMDIChildForm
    {
        public FM_USER()
        {
            InitializeComponent();
        }

        public override void Inquire()
        {
            base.Inquire();

            DBHelper helper = new DBHelper(false);
            try
            {
                string sUserID = txtUserID.Text;
                string sUserName = txtUserName.Text;
                DataTable DtTemp = new DataTable();
                DtTemp = helper.FillTable("SP_USER_LHC_S1", CommandType.StoredProcedure
                    , helper.CreateParameter("USERID",sUserID)
                    , helper.CreateParameter("USERNAME", sUserName));

                if (DtTemp.Rows.Count == 0)
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("조회할 데이터가 없습니다.");
                }
                else
                {
                    dataGridView1.DataSource = DtTemp;
                }
                    //dataGridView1.CurrentRow.Cells["USERID"].Value.ToString();

                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("예외 발생" + ex);
            }
            finally
            {
                helper.Close();
            }
        }

        public override void DoNew()
        {
            base.DoNew();
            if (this.dataGridView1.Rows.Count == 0) return;
            DataRow dr = ((DataTable)dataGridView1.DataSource).NewRow();
            ((DataTable)dataGridView1.DataSource).Rows.Add(dr);


            // DataRow dr = ((DataTable)dgvGrid.DataSource).NewRow(); // dr 정의
            // ((DataTable)dgvGrid.DataSource).Rows.Add(dr); // 빈 깡통 dr을 다시 dgv에 추가.
        }
        public override void Delete()
        {
            base.Delete();
            /*if (dataGridView1.Rows.Count == 0) return;
            int iSelectIndex = dataGridView1.CurrentCell.RowIndex;
            DataTable DtTemp = (DataTable)dataGridView1.DataSource;
            DtTemp.Rows[iSelectIndex].Delete();*/ // 지금 데이터 그리드 뷰의 선택된 셀의 로우인덱스에 해당하는 열을 지움
            
            // 아니 DataTable의 객체 DtTemp의 행을 하나 지웠는데 왜 dataGridView의 행이 지워지냐고 ㅅㅂ
            // dgv의 데이터 소스를 DataTable의 객체로 선언하면 실시간 연동이 되는거냐?
            // 표 상에서만 지워
            
            if (dataGridView1.Rows.Count == 0) return;

            string sUserID = dataGridView1.CurrentRow.Cells["USERID"].Value.ToString(); //선택된 셀
            DataTable DtTemp = (DataTable)dataGridView1.DataSource;
            for (int i = 0; i < DtTemp.Rows.Count; i++)
            {
                if (DtTemp.Rows[i].RowState == DataRowState.Deleted) continue; //지워진 상태이면 다음 행으로
                if (DtTemp.Rows[i][0].ToString() == sUserID) //선택된 셀의 UserID와 같으면
                {
                    DtTemp.Rows[i].Delete();
                    break;
                }
            }

        }
        public override void Save()
        {
            base.Save();
            string UserID = string.Empty;
            string sUserName = string.Empty;
            string sPassWord = string.Empty;
            DataTable dtTemp = ((DataTable)dataGridView1.DataSource).GetChanges();
            if (dtTemp == null) return;

            if (MessageBox.Show("데이터를 등록하시겠습니까?", "데이터 저장", MessageBoxButtons.YesNo)
                == DialogResult.No) return;



            DBHelper helper = new DBHelper(true);

            try
            {
                // FOREACH : 내가 추가, 수정, 삭제한 모든 행동을 GETCHANGE로 담아서 모두 처리한다.

                foreach (DataRow drRow in dtTemp.Rows) 
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            drRow.RejectChanges();
                            UserID = drRow["USERID"].ToString();
                            helper.ExecuteNoneQuery("SP_USER_LHC_D1",
                                CommandType.StoredProcedure, helper.CreateParameter("USERID", UserID));
                            break;
                        case DataRowState.Added:
                            #region 추가
                            UserID = drRow["USERID"].ToString();
                            sUserName = drRow["USERNAME"].ToString();
                            sPassWord = drRow["PW"].ToString();
                            helper.ExecuteNoneQuery("SP_USER_LHC_I1", CommandType.StoredProcedure
                                , helper.CreateParameter("USERID", UserID)
                                , helper.CreateParameter("USERNAME", sUserName)
                                , helper.CreateParameter("PASSWORD", sPassWord)
                                , helper.CreateParameter("MAKER", Common.LogInID)
                                );
                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정
                            UserID = drRow["USERID"].ToString();
                            sUserName = drRow["USERNAME"].ToString();
                            sPassWord = drRow["PW"].ToString();
                            helper.ExecuteNoneQuery("SP_USER_LHC_U1", CommandType.StoredProcedure
                                , helper.CreateParameter("USERID", UserID)
                                , helper.CreateParameter("USERNAME", sUserName)
                                , helper.CreateParameter("PASSWORD", sPassWord)
                                , helper.CreateParameter("EDITOR", Common.LogInID)
                                );
                            #endregion
                            break;
                    }
                }
                // 성공 시 DB Commit
                helper.Commit();
                // 메세지
                MessageBox.Show("정상적으로 등록하였습니다.");
                // 재조회
                Inquire();

            }
            catch (Exception ex)
            {
                helper.Rollback();

                MessageBox.Show("데이터 등록에 실패하였습니다." + ex);
            }
            finally
            {
                helper.Close();
            }
        }
    }
}
