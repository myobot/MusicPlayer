using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 音乐播放器
{
    public partial class Login : Form
    {
        Form1 father;
        public void setFather(Form1 fa)
        {
            this.father = fa;
        }
        public Login()
        {
            InitializeComponent();
        }
     
        private void CloseBut_MouseEnter(object sender, EventArgs e)
        {
            CloseBut.BackColor = Color.Red;
        }

        private void CloseBut_MouseLeave(object sender, EventArgs e)
        {
            CloseBut.BackColor = Color.Transparent;
        }

        private void LoginBut_MouseEnter(object sender, EventArgs e)
        {
            LoginBut.BackColor =Color.FromArgb(45,181,171);
        }

        private void LoginBut_MouseLeave(object sender, EventArgs e)
        {
            LoginBut.BackColor = Color.FromArgb(38, 204, 192);
        }

        private void RegistBut_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.RegistBut.ForeColor = Color.White;
        }

        private void RegistBut_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.RegistBut.ForeColor = Color.Silver;
        }

        private void UserNameText_TextChanged(object sender, EventArgs e)
        {
            String username = UserNameText.Text;
        }

        private void CloseBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginBut_Click(object sender, EventArgs e)
        {
            UserDAO ud = new UserDAO();
            String username = UserNameText.Text;
            String password = PasswordText.Text;
            if (ud.finduser(username, password))
            {
                //father
                List<String> likemusicpath = ud.getUserLikeList(username);
                int num = ud.getUserImg(username);
                father.setLogin(username, num, likemusicpath);
                this.Close();
            }
            else
            {
                DialogResult dr = MessageBoxEx.Show(this, "账号密码错误");
            }
        }

        private void UserNameText_Leave(object sender, EventArgs e)
        {
            UserDAO ud = new UserDAO();
            String username = UserNameText.Text;
            int num = ud.getUserImg(username);
            if (num!=0)
            {
                switch (num)
                {
                    case 11: this.UserImage.BackgroundImage = Properties.Resources._11; break;
                    case 12: this.UserImage.BackgroundImage = Properties.Resources._12; break;
                    case 13: this.UserImage.BackgroundImage = Properties.Resources._13; break;
                    case 14: this.UserImage.BackgroundImage = Properties.Resources._14; break;
                    case 15: this.UserImage.BackgroundImage = Properties.Resources._15; break;
                    case 16: this.UserImage.BackgroundImage = Properties.Resources._16; break;
                    case 17: this.UserImage.BackgroundImage = Properties.Resources._17; break;
                    case 18: this.UserImage.BackgroundImage = Properties.Resources._18; break;
                    case 19: this.UserImage.BackgroundImage = Properties.Resources._19; break;
                    default: break;
                }
            }
        }

        private void RegistBut_Click(object sender, EventArgs e)
        {
            Regist r = new Regist();
            r.Location = this.Location;
            r.StartPosition = FormStartPosition.Manual;
            r.ShowDialog();

        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(UserNameText, "用户名");
            this.toolTip2.SetToolTip(PasswordText, "密码");
            this.toolTip3.SetToolTip(LoginBut, "登录");
        }
        #region
        /*
        kh = new KeyboardHook();
        kh.SetHook();
        kh.OnKeyDownEvent += kh_OnKeyDownEvent;
        KeyboardHook kh;
        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
            {
                UserDAO ud = new UserDAO();
                String username = UserNameText.Text;
                String password = PasswordText.Text;
                if (ud.finduser(username, password))
                {
                    //father
                    List<String> likemusicpath = ud.getUserLikeList(username);
                    int num = ud.getUserImg(username);
                    father.setLogin(username, num, likemusicpath);
                    this.Close();
                }
                else
                {
                    DialogResult dr = MessageBoxEx.Show(this, "账号密码错误");
                }
            }
        }
        */
        #endregion
    }
}
