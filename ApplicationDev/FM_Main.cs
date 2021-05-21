using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DEV_Form;

namespace ApplicationDev
{
    public partial class FM_Main : Form
    {
        public FM_Main()
        {
            InitializeComponent();
            FM_Login Login = new FM_Login();
            Login.ShowDialog(); // FM_Main이 실행되기 전에 먼저 로그인 화면 실행
                                // ShowDialog()로 로그인 하기 전에 Main 창이 뜨지 않도록 한다.?
                                // Show()로 하면 동시에 Main, 로그인 창 둘 다 뜨네.

            //Tag를 가져와서 StatusStrip에 입력한다.
            tssUserName.Text = Login.Tag.ToString();
            
            if (Login.Tag.ToString() == "FAIL")
            {
                //Application.ExitThread();
                //Application.Exit();
                //this.Close(); 지금 환경에서는 종료가 안 된다. 수업에서 this.Close()는 안 씀. 
                System.Environment.Exit(0);
                //이렇게 하니 종료된다.
            }
            // 버튼에 이벤트 추가.
            this.stbExit.Click += new System.EventHandler(this.stbExit_Click);
            
            //string UserName = Login.Tag.ToString();
        }

        private void stbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            tssNowDate.Text = DateTime.Now.ToString();
        }
    }
}
