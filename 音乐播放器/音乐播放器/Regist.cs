using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 音乐播放器
{
    public partial class Regist : Form
    {
        
        
        public Regist()
        {
            InitializeComponent();
        }
        int imagenum = 0;

      

        private void LoginBut_Click(object sender, EventArgs e)
        {
            String username = UserNameText.Text;
            String password = PasswordText.Text;
            String repassword = rePasswordText.Text;
           
            if (haveregist)
            {
                DialogResult dr = MessageBoxEx.Show(this, "此账户已被注册！");
            }
            else
            {
                if (username == null || "".Equals(username))
                {
                     DialogResult dr = MessageBoxEx.Show(this, "用户名不能为空");
                }
                else if (password == null || "".Equals(password))
                {
                    DialogResult dr = MessageBoxEx.Show(this, "密码不能为空");
                }
                else if (password != repassword)
                {
                    DialogResult dr = MessageBoxEx.Show(this, "两次填写的密码不同");
                }
                else
                {
                    UserDAO ud = new UserDAO();
                    if (ud.RegistUser(username, password, imagenum))
                    {
                        DialogResult dr = MessageBoxEx.Show(this, "注册成功");
                        this.Close();
                    }
                    else
                    {
                        DialogResult dr = MessageBoxEx.Show(this, "注册失败");
                    }

                }
            }
        }

        private void CloseBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseBut_MouseEnter(object sender, EventArgs e)
        {
            CloseBut.BackColor = Color.Red;
        }

        private void CloseBut_MouseLeave(object sender, EventArgs e)
        {
            CloseBut.BackColor = Color.Transparent;
        }
        bool flag = false;
        private void ImageBut_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void Regist_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            

        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void ImageBut_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void ImageBut_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Skin_1_Click(object sender, EventArgs e)
        {
            this.imagenum = 11;
            this.ImageBut.BackgroundImage = Properties.Resources._11;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.imagenum = 12;
            this.ImageBut.BackgroundImage = Properties.Resources._12;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.imagenum = 13;
            this.ImageBut.BackgroundImage = Properties.Resources._13;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.imagenum = 14;
            this.ImageBut.BackgroundImage = Properties.Resources._14;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            this.imagenum = 15;
            this.ImageBut.BackgroundImage = Properties.Resources._15;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            this.imagenum = 16;
            this.ImageBut.BackgroundImage = Properties.Resources._16;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            this.imagenum = 17;
            this.ImageBut.BackgroundImage = Properties.Resources._17;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

            this.imagenum = 18;
            this.ImageBut.BackgroundImage = Properties.Resources._18;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

            this.imagenum = 19;
            this.ImageBut.BackgroundImage = Properties.Resources._19;
            if (flag)
            {
                this.SkinPan.Visible = false;
                flag = false;
            }
            else
            {
                this.SkinPan.Visible = true;
                flag = true;
            }
        }

        private void Skin_1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {

            this.Cursor = Cursors.Default;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        bool haveregist = false;
        private void UserNameText_Leave(object sender, EventArgs e)
        {
            String username = UserNameText.Text;
            UserDAO ud = new UserDAO();
            if (ud.finduser(username))
            {
                this.haveRegisted.Visible = true;
                this.haveregist = true;
            }
            else
            {
                this.haveRegisted.Visible = false;
                this.haveregist = false;
            }
        }

        private void Regist_Load(object sender, EventArgs e)
        {
            /* kh = new KeyboardHook();
             kh.SetHook();
             kh.OnKeyDownEvent += kh_OnKeyDownEvent;
             */
            this.toolTip1.SetToolTip(UserNameText, "用户名");
            this.toolTip2.SetToolTip(PasswordText, "密码");
            this.toolTip3.SetToolTip(rePasswordText, "重新输入密码");
            this.toolTip4.SetToolTip(LoginBut, "注册");
        }
        #region
        /*KeyboardHook kh;
        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
            {
                String username = UserNameText.Text;
                String password = PasswordText.Text;
                String repassword = rePasswordText.Text;

                if (haveregist)
                {
                    DialogResult dr = MessageBoxEx.Show(this, "此账户已被注册！");
                }
                else
                {
                    if (username == null || "".Equals(username))
                    {
                        DialogResult dr = MessageBoxEx.Show(this, "用户名不能为空");
                    }
                    else if (password == null || "".Equals(password))
                    {
                        DialogResult dr = MessageBoxEx.Show(this, "密码不能为空");
                    }
                    else if (password != repassword)
                    {
                        DialogResult dr = MessageBoxEx.Show(this, "两次填写的密码不同");
                    }
                    else
                    {
                        UserDAO ud = new UserDAO();
                        if (ud.RegistUser(username, password, imagenum))
                        {
                            DialogResult dr = MessageBoxEx.Show(this, "注册成功");
                            this.Close();
                        }
                        else
                        {
                            DialogResult dr = MessageBoxEx.Show(this, "注册失败");
                        }

                    }
                }
            }
        }
        */
        #endregion
    }
}
