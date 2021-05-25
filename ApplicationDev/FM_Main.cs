using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            this.M_SYSTEM.DropDownItemClicked +=
                new System.Windows.Forms.ToolStripItemClickedEventHandler(this.M_SYSTEM_DropDownItemClicked);

            this.stbClose.Click += new System.EventHandler(this.stbClose_Click);
            
            //string UserName = Login.Tag.ToString();
        }

        private void stbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stbClose_Click(object sender, EventArgs e)
        {
            // 열려 있는 화면이 있는지 확인
            if (myTabControl1.TabPages.Count == 0) return;
            // 선택된 탭페이지를 닫는다.
            myTabControl1.SelectedTab.Dispose();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            tssNowDate.Text = DateTime.Now.ToString();
        }
        private void M_SYSTEM_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // 1. 단순히 폼을 호출하는 경우.
            //DEV_Form.MDI_TEST Form = new DEV_Form.MDI_TEST();
            //Form.MdiParent = this;
            //Form.Show();

            // 2. 프로그램을 호출
            Assembly assy = Assembly.LoadFrom(Application.StartupPath + @"\" + "DEV_FORM.DLL");
            Type typeForm = assy.GetType("DEV_Form." + e.ClickedItem.Name.ToString(), true);
            Form ShowForm = (Form)Activator.CreateInstance(typeForm);

            // 해당되는 폼(탭)이 이미 오픈되어 있는지 확인 후 활성화 또는 신규 오픈한다.
            for ( int i = 0; i < myTabControl1.TabPages.Count; i++)
            {
                if (myTabControl1.TabPages[i].Name == e.ClickedItem.Name.ToString())
                {
                    myTabControl1.SelectedTab = myTabControl1.TabPages[i];
                    return;
                }
            }
            //ShowForm.MdiParent = this;
            //ShowForm.Show();
            myTabControl1.AddForm(ShowForm); // 탭 페이지에 폼을 추가하여 오픈한다.
        }
    }

    public partial class MDIForm : TabPage // 내용이 없으니 MDIForm TabPage라는 클래스와 다를게 없다.
    {
        
    }

    public partial class MyTabControl : TabControl
    {
        public void AddForm(Form NewForm) // 인자 받아서 정의하는거 : 오버로딩??
        {
            if (NewForm == null) return; // 인자로 받은 폼이 없는 경우 실행 중지. 베리데이션?
            NewForm.TopLevel = false; // 인자로 받은 폼을 최상위 창으로 유지하지 않도록(false) 한다.
            MDIForm page = new MDIForm(); // 위에 정의한 MDIForm이라는 TabPage의 파생 클래스의 객체 page를 생성.
            page.Controls.Clear(); // page의 Controls 속성을 clear 한다. : 페이지 초기화
            page.Controls.Add(NewForm); // page의 Controls 속성에 NewForm을 Add한다. : 페이지에 폼 추가
            page.Text = NewForm.Text; // page의 Text 속성에 NewForm의 Text 속성을 입력한다. : 폼에서 지정한 명칭으로 탭 페이지 설정
            page.Name = NewForm.Name; // page의 Name 속성에 NewForm의 Name 속성을 입력한다. : 폼에서 설정한 이름으로 탭 페이지 설정
            base.TabPages.Add(page); // 부모(TabControl) 클래스의 TabPages 속성에 page를 Add 한다. : 탭 컨트롤에 페이지를 추가한다.
            NewForm.Show(); // NewForm을 Show 한다. : 인자로 받은 폼을 보여준다.
            base.SelectedTab = page; // 부모(TabControl) 클래스의 SelectedTab 속성에 page를 입력한다. : 현재 선택된 페이지를 호출한 폼의 페이지로 실행한다.

        }
    }
}
