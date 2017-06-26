using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MumiMusic;
using Mp3Lib;
namespace 音乐播放器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.Opaque, true);//解决背景重绘问题(设置不绘制窗口背景，因为重绘窗口背景会导致性能底下)
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲
            this.UpdateStyles();
            ChargeRoll();
            per = 35;
            isSilence = false;
            SilenceSetVolume();
            GetMusicList();
            timer2.Enabled = false;
            if (musicPal == null) { }
            else
            {
                foreach (var i in this.musicPal)
                {
                    this.MusicListPan.Controls.Add(i);

                }
            }
            toolTip1.SetToolTip(PlusMusicBut, "添加歌曲");
            toolTip2.SetToolTip(PlayModeBut, "列表循环");
            PlayMode = 0;
            GetLrc();
          
        }

        

        #region  减轻闪烁
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;////用双缓冲从下到上绘制窗口的所有子孙
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0014) // 禁掉清除背景消息  

                return;

            base.WndProc(ref m);

        }
        #endregion


        #region 窗体拖动,关闭打开的弹窗（皮肤，音量，头像）,以及更改和保存背景
        /*
         *窗体拖动 
        */
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //主窗体拖动
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        //titile面板拖动
        private void panel6_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (skins)
            {
                SkinPan.Visible = false;
                skins = false;
            }
            if (Volumeopen)
            {
                VolumPan.Visible = false;
                Volumeopen = false;
            }
            if (imageflag)
            {
                this.ImageChoosePan.Visible = false;
                imageflag = false;
            }
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        //左部面板拖动 
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (skins)
            {
                SkinPan.Visible = false;
                skins = false;
            }
            if (Volumeopen)
            {
                VolumPan.Visible = false;
                Volumeopen = false;
            }
            if (imageflag)
            {
                this.ImageChoosePan.Visible = false;
                imageflag = false;
            }
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        //音乐面板
        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            if (skins)
            {
                SkinPan.Visible = false;
                skins = false;
            }
            if (Volumeopen)
            {
                VolumPan.Visible = false;
                Volumeopen = false;
            }
            if (imageflag)
            {
                this.ImageChoosePan.Visible = false;
                imageflag = false;
            }
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            if (skins)
            {
                SkinPan.Visible = false;
                skins = false;
            }
            if (Volumeopen)
            {
                VolumPan.Visible = false;
                Volumeopen = false;
            }
            if (imageflag)
            {
                this.ImageChoosePan.Visible = false;
                imageflag = false;
            }
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);

        }
        private void panel10_MouseDown(object sender, MouseEventArgs e)
        {
            if (skins)
            {
                SkinPan.Visible = false;
                skins = false;
            }
            if (Volumeopen)
            {
                VolumPan.Visible = false;
                Volumeopen = false;
            }
            if (imageflag)
            {
                this.ImageChoosePan.Visible = false;
                imageflag = false;
            }
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        private void panel11_MouseDown(object sender, MouseEventArgs e)
        {
            if (skins)
            {
                SkinPan.Visible = false;
                skins = false;
            }
            if (Volumeopen)
            {
                VolumPan.Visible = false;
                Volumeopen = false;
            }
            if (imageflag)
            {
                this.ImageChoosePan.Visible = false;
                imageflag = false;
            }
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            if (skins)
            {
                SkinPan.Visible = false;
                skins = false;
            }
            if (Volumeopen)
            {
                VolumPan.Visible = false;
                Volumeopen = false;
            }
            if (imageflag)
            {
                this.ImageChoosePan.Visible = false;
                imageflag = false;
            }
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        //更改背景
        private void Skin_1_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.背景;
        }

        private void Skin_2_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources._4;
        }

        private void Skin_3_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources._5;
        }

        private void Skin_4_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources._1;
        }

        private void Skin_5_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources._3;
        }

        private void Skin_6_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources._7;
        }

        private void Skin_7_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources._8;
        }

        private void Skin_8_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources._9;
        }

        //添加背景
        string picfile;//保存copy源
        string picName;
        private void Skin_add_Click(object sender, EventArgs e)
        {

            OpenFileDialog of1 = new OpenFileDialog();  //打开文件窗口
            of1.InitialDirectory = "C:\\Users\\Lenovo\\Pictures\\Camera Roll\\"; //开始文件位置
            of1.Filter = "图片|*.jpg;*.png;*.gif;*.jpeg;*.bmp";   //文件过滤 名称：图片 
            of1.RestoreDirectory = true;        //关闭前还原原目录
            of1.FilterIndex = 1;                //选定文件索引器下标
            if (of1.ShowDialog() == DialogResult.OK)
            {
                picfile = of1.FileName;
                this.BackgroundImage = Image.FromFile(string.Format("{0}", picfile));
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

        private void Skin_2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Skin_2_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Skin_3_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Skin_4_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_4_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Skin_5_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_5_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Skin_6_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_6_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Skin_7_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_7_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Skin_8_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_8_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Skin_add_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Skin_add_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        #endregion


        #region 更改透明度
        //更改透明度
        public void setTrans(int op)
        {
            this.Opacity = (double)op / 220;
        }
        bool Transflag = false;

        private void TransPro_MouseDown(object sender, MouseEventArgs e)
        {
            TransPro.Value = e.Location.X;
            setTrans(e.Location.X);
            int x2 = e.Location.X - 6;
            if (x2 < 0) { x2 = 0; }
            if (x2 > 207) { x2 = 207; }
            TransPoint.Location = new Point(x2, TransPoint.Location.Y);
            Transflag = true;
        }

        private void TransPro_MouseMove(object sender, MouseEventArgs e)
        {
            int x = 0, x2 = 0;

            if (Transflag)
            {
                x = e.Location.X;
                x2 = e.Location.X - 6;
                if (x < 0) { x = 0; }
                if (x > (220)) { x = 220; }
                if (x2 < 0) { x2 = 0; }
                if (x2 > 207) { x2 = 207; }
                TransPro.Value = x;
                setTrans(x);
                TransPoint.Location = new Point(x2, TransPoint.Location.Y);
            }
        }

        private void TransPro_MouseUp(object sender, MouseEventArgs e)
        {
            this.Transflag = false;
        }

        private void TransPan_MouseUp(object sender, MouseEventArgs e)
        {
            this.Transflag = false;
        }

        private void TransPan_MouseMove(object sender, MouseEventArgs e)
        {
            int x = 0, x2 = 0;

            if (Transflag)
            {
                x = e.Location.X;
                x2 = e.Location.X - 6;
                if (x < 0) { x = 0; }
                if (x > (220)) { x = 220; }
                if (x2 < 0) { x2 = 0; }
                if (x2 > 207) { x2 = 207; }
                TransPro.Value = x;
                setTrans(x);
                TransPoint.Location = new Point(x2, TransPoint.Location.Y);
            }
        }

        private void TransPan_MouseDown(object sender, MouseEventArgs e)
        {
            TransPro.Value = e.Location.X;
            setTrans(e.Location.X);
            int x2 = e.Location.X - 6;
            if (x2 < 0) { x2 = 0; }
            if (x2 > 207) { x2 = 207; }
            TransPoint.Location = new Point(x2, TransPoint.Location.Y);
            Transflag = true;

        }

        public Point p3, p4;
        private void TransPoint_MouseMove(object sender, MouseEventArgs e)
        {

            //鼠标相对于屏幕的坐标
            p3 = MousePosition;
            //鼠标相对于窗体的坐标
            p4 = TransPan.PointToClient(p3);
            int x = 0, x2 = 0;
            if (Transflag)
            {
                x = p4.X; x2 = p4.X - 6;
                if (x < 0) { x = 0; }
                if (x > (220)) { x = 220; }
                if (x2 < 0) { x2 = 0; }
                if (x2 > 207) { x2 = 207; }
                TransPro.Value = x;
                setTrans(x);
                TransPoint.Location = new Point(x2, TransPoint.Location.Y);
            }

        }

        private void TransPoint_MouseUp(object sender, MouseEventArgs e)
        {
            this.Transflag = false;
        }

        private void TransPoint_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标相对于屏幕的坐标
            p3 = MousePosition;
            //鼠标相对于窗体的坐标
            p4 = TransPan.PointToClient(p3);
            TransPro.Value = p4.X;
            setTrans(p4.X);
            int x2 = p4.X - 6;
            if (x2 < 0) { x2 = 0; }
            if (x2 > 207) { x2 = 207; }
            TransPoint.Location = new Point(x2, TransPoint.Location.Y);
            Transflag = true;
        }
        #endregion


        #region 关闭按钮，最小化按钮，皮肤按钮处理
        private void CloseBut_Click(object sender, EventArgs e)
        {
            UserDAO ud = new UserDAO();
            if (likeMusicPath != null && username != null)
            {
                ud.UpdateLikeMusic(likeMusicPath, username);
            }
            if (havechangeImage)
            {
                ud.UpdateUser_image(this.username, this.imagenum);
            }
            Application.Exit();
        }
        private void CloseBut_MouseEnter(object sender, EventArgs e)
        {
            CloseBut.BackgroundImage = Properties.Resources.关闭__5_;
            this.Cursor = Cursors.Hand;
            this.panel6.Visible = true;
        }

        private void CloseBut_MouseLeave(object sender, EventArgs e)
        {
            CloseBut.BackgroundImage = Properties.Resources.关闭__4_;
            this.Cursor = Cursors.Default;
        }

        private void MinBut_MouseEnter(object sender, EventArgs e)
        {

            this.Cursor = Cursors.Hand;
            this.panel6.Visible = true;
        }

        private void MinBut_MouseLeave(object sender, EventArgs e)
        {

            this.Cursor = Cursors.Default;
        }

        private void MinBut_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SklnBut_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.panel6.Visible = true;
        }

        private void SklnBut_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        //皮肤更改开关
        Boolean skins = false;
        private void SklnBut_Click(object sender, EventArgs e)
        {
            if (skins)
            {
                SkinPan.Visible = false;
                skins = false;
            }
            else
            {
                SkinPan.Visible = true;
                skins = true;
            }
        }
        #endregion


        #region 登录操作
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            Font f = new Font("等线", 12, FontStyle.Underline | FontStyle.Bold);
            LoginLable.Font = f;
        }
        private void LoginLable_MouseLeave(object sender, EventArgs e)
        {
            Font f = new Font("等线", 12, FontStyle.Bold);
            LoginLable.Font = f;
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
        private void LoginLable_Click(object sender, EventArgs e)
        {
            if (loginflag)
            {

                UserDAO ud = new UserDAO();
                if (likeMusicPath != null && username != null)
                {
                    ud.UpdateLikeMusic(likeMusicPath, username);
                }
                if (havechangeImage)
                {
                    ud.UpdateUser_image(this.username, this.imagenum);
                }
                changelisttobd();
                likeMusicPath.Clear();
                UpdateLike();
                loginflag = false;
                LoginLable.Text = "登录";
                panel2.BackgroundImage = Properties.Resources._00;
            }
            else
            {
                Login l = new Login();
                l.setFather(this);
                Point p = new Point(this.Location.X + 322, this.Location.Y + 95);
                l.Location = p;
                l.StartPosition = FormStartPosition.Manual;
                l.ShowDialog();
            }
        }
        private void panel2_Click(object sender, EventArgs e)
        {
            if (loginflag)
            {
                if (imageflag)
                {
                    this.ImageChoosePan.Visible = false;
                    imageflag = false;
                }
                else
                {
                    this.ImageChoosePan.Visible = true;
                    imageflag = true;
                }
            }
            else
            {
                Login l = new Login();
                l.setFather(this);
                Point p = new Point(this.Location.X + 322, this.Location.Y + 95);
                l.Location = p;
                l.StartPosition = FormStartPosition.Manual;
                l.ShowDialog();
            }

        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            Font f = new Font("等线", 12, FontStyle.Underline | FontStyle.Bold);
            LoginLable.Font = f;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            Font f = new Font("等线", 12, FontStyle.Bold);
            LoginLable.Font = f;
        }

        bool imageflag = false;//头像框显示与否标志
        public void setImage()
        {
            switch (this.imagenum)
            {
                case 11: this.panel2.BackgroundImage = Properties.Resources._11; break;
                case 12: this.panel2.BackgroundImage = Properties.Resources._12; break;
                case 13: this.panel2.BackgroundImage = Properties.Resources._13; break;
                case 14: this.panel2.BackgroundImage = Properties.Resources._14; break;
                case 15: this.panel2.BackgroundImage = Properties.Resources._15; break;
                case 16: this.panel2.BackgroundImage = Properties.Resources._16; break;
                case 17: this.panel2.BackgroundImage = Properties.Resources._17; break;
                case 18: this.panel2.BackgroundImage = Properties.Resources._18; break;
                case 19: this.panel2.BackgroundImage = Properties.Resources._19; break;
                default: break;
            }
            this.ImageChoosePan.Visible = false;
            imageflag = false;
            this.havechangeImage = true;
        }
        bool havechangeImage = false;
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            this.imagenum = 11;
            setImage();

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

            this.imagenum = 12;
            setImage();

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.imagenum = 13;
            setImage();

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.imagenum = 14;
            setImage();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.imagenum = 15;
            setImage();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.imagenum = 16;
            setImage();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.imagenum = 17;
            setImage();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.imagenum = 18;
            setImage();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.imagenum = 19;
            setImage();
        }
        #endregion


        #region 本地列表和我的列表的切换 
        bool leftchooseflag = true; //两个列表谁被选中标记
        private void panel4_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            if (!leftchooseflag)
            {
                panel4.BackColor = Color.Gray;
            }
            this.Cursor = Cursors.Hand;
        }

        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            if (!leftchooseflag)
            {
                panel4.BackColor = Color.Transparent;
            }
            this.Cursor = Cursors.Default;
        }

        private void label1_MouseEnter_1(object sender, EventArgs e)
        {
            if (!leftchooseflag)
            {
                panel4.BackColor = Color.Gray;
            }
            this.Cursor = Cursors.Hand;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            if (!leftchooseflag)
            {
                panel4.BackColor = Color.Transparent;
            }
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!leftchooseflag)
            {
                panel4.BackColor = Color.Gray;
            }
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!leftchooseflag)
            {
                panel4.BackColor = Color.Transparent;
            }
            this.Cursor = Cursors.Default;
        }

        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            if (leftchooseflag)
            {
                panel5.BackColor = Color.Gray;
            }
            this.Cursor = Cursors.Hand;
        }

        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            if (leftchooseflag)
            {
                panel5.BackColor = Color.Transparent;
            }
            this.Cursor = Cursors.Default;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            if (leftchooseflag)
            {
                panel5.BackColor = Color.Gray;
            }
            this.Cursor = Cursors.Hand;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            if (leftchooseflag)
            {
                panel5.BackColor = Color.Transparent;
            }
            this.Cursor = Cursors.Default;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            if (leftchooseflag)
            {
                panel5.BackColor = Color.Gray;
            }
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (leftchooseflag)
            {
                panel5.BackColor = Color.Transparent;
            }
            this.Cursor = Cursors.Default;
        }


        //切换
        bool loginflag = false;
        List<String> likeMusicPath = null;
        string username = null;
        int imagenum = 0;

        //给Login窗体调用的函数，设置用户登录信息
        public void setLogin(string username, int imagenum, List<string> likemusicpath)
        {
            loginflag = true;
            this.username = username;
            this.LoginLable.Text = "退出";
            switch (imagenum)
            {
                case 11: this.panel2.BackgroundImage = Properties.Resources._11; break;
                case 12: this.panel2.BackgroundImage = Properties.Resources._12; break;
                case 13: this.panel2.BackgroundImage = Properties.Resources._13; break;
                case 14: this.panel2.BackgroundImage = Properties.Resources._14; break;
                case 15: this.panel2.BackgroundImage = Properties.Resources._15; break;
                case 16: this.panel2.BackgroundImage = Properties.Resources._16; break;
                case 17: this.panel2.BackgroundImage = Properties.Resources._17; break;
                case 18: this.panel2.BackgroundImage = Properties.Resources._18; break;
                case 19: this.panel2.BackgroundImage = Properties.Resources._19; break;
                default: break;
            }
            this.likeMusicPath = likemusicpath;
            this.imagenum = imagenum;
            UpdateLike();
        }

        //更新喜爱列表
        public void UpdateLike()
        {
            if (loginflag)
            {
                for (int j = 0; j < musicPal.Length; j++)
                {
                    musicPal[j].changeLike(false);
                }
                for (int i = 0; i < likeMusicPath.Count; i++)
                {
                    for (int j = 0; j < musicPal.Length; j++)
                    {
                        if (likeMusicPath[i] == musicPal[j].getPath())
                        {
                            musicPal[j].changeLike(true);   //调用MusicInfo里的函数
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 点击选择本地列表
        /// </summary>
        public void changelisttobd()
        {
            if (!leftchooseflag)
            {
                this.PlusMusicBut.Visible = true;
                this.MusicListPan.Controls.Clear();     //清空
                if (nowPlaying != null)
                {
                    nowPlayingmusic = nowPlaying.getPath();
                }
                this.musicPal = new MusicInfo[this.list.Count];
                int locx = 0;
                int locy = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    musicPal[i] = new MusicInfo();

                    Point p = new Point(locx, locy);
                    locy += 50;
                    musicPal[i].Location = p;
                    musicPal[i].setFatherForm(this);
                    musicPal[i].setMusicNameAndPath(getFileName(list[i]), list[i]);
                    this.MusicListPan.Controls.Add(musicPal[i]);
                }
                if (nowPlayingmusic != null)        //检查是否正在播放音乐  若是更改相应的MusicInfo
                {
                    int num = -1;
                    for (int i = 0; i < musicPal.Length; i++)
                    {
                        if (nowPlayingmusic == musicPal[i].getPath())
                        {
                            num = i;
                        }
                    }
                    if (num != -1)
                    {
                        this.setNowPlaying(musicPal[num]);
                        this.nowPlayIndex = num;
                        GetLrc();
                        this.nowPlaying.changePlayBut(true);
                    }
                    else
                    {
                        this.nowPlayIndex = -1;
                    }
                }
                UpdateLike();
                leftchooseflag = true;
                panel4.BackColor = Color.Gray;
                panel5.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// 点击选择喜爱列表
        /// </summary>
        public void changelisttolike()
        {
            if (!loginflag)
            {
                DialogResult dr = MessageBoxEx.Show(this, "未登录");
            }
            else
            {
                if (leftchooseflag)
                {
                    this.PlusMusicBut.Visible = false;
                    this.MusicListPan.Controls.Clear();
                    if (nowPlaying != null)
                    {
                        nowPlayingmusic = nowPlaying.getPath();
                    }
                    this.musicPal = new MusicInfo[this.likeMusicPath.Count];
                    int locx = 0;
                    int locy = 0;
                    for (int i = 0; i < likeMusicPath.Count; i++)
                    {
                        musicPal[i] = new MusicInfo();

                        Point p = new Point(locx, locy);
                        locy += 50;
                        musicPal[i].Location = p;
                        musicPal[i].setFatherForm(this);
                        musicPal[i].setMusicNameAndPath(getFileName(likeMusicPath[i]), likeMusicPath[i]);
                        musicPal[i].DeleteBut.Visible = false;
                        this.MusicListPan.Controls.Add(musicPal[i]);

                    }
                    if (nowPlayingmusic != null)
                    {
                        int num = -1;
                        for (int i = 0; i < musicPal.Length; i++)
                        {
                            if (nowPlayingmusic == musicPal[i].getPath())
                            {
                                num = i;
                            }
                        }
                        if (num != -1)
                        {
                            this.setNowPlaying(musicPal[num]);
                            this.nowPlayIndex = num;
                            GetLrc();
                            this.nowPlaying.changePlayBut(true);
                        }
                        else
                        {
                            this.nowPlayIndex = -1;
                        }
                    }
                    UpdateLike();
                    leftchooseflag = false;
                    panel5.BackColor = Color.Gray;
                    panel4.BackColor = Color.Transparent;
                }
            }

        }
        String nowPlayingmusic = null;  //保存当前的播放音乐的路径
        private void panel4_Click(object sender, EventArgs e)
        {
            changelisttobd();
        }


        private void panel5_Click(object sender, EventArgs e)
        {
            changelisttolike();
        }



        private void label1_Click(object sender, EventArgs e)
        {
            changelisttobd();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            changelisttobd();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            changelisttolike();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            changelisttolike();
        }
        #endregion


        #region 歌名lable判断是否需要滚动
        int count = 0;
        int lx1 = 0;
        int lx2 = 0;
        int W = 400;
        int H = 18;
        private void ChargeRoll()
        {
            if (MusicName.Size.Width < (W - 10))
            {
                MusicName.Location = new Point((W - MusicName.Size.Width) / 2, H);
                MusicName2.Visible = false;
                GeCIRowTimer.Enabled = false;

            }
            else
            {
                MusicName.Location = new Point(10, H);
                MusicName2.Location = new Point(MusicName.Size.Width + 80, H);
                count = MusicName2.Location.X + MusicName.Size.Width - W;
                lx1 = 10;
                lx2 = MusicName.Size.Width + 80;
                GeCIRowTimer.Enabled = true;

            }
        }
        bool flag = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                if (count >= 0)
                {

                    MusicName.Location = new Point(--lx1, H);
                    MusicName2.Location = new Point(--lx2, H);
                    count--;
                }
                else
                {
                    MusicName.Location = new Point(MusicName.Size.Width + 80, H);
                    count = MusicName.Location.X + MusicName.Size.Width - W;
                    lx1 = MusicName.Size.Width + 80;
                    flag = false;
                }
            }
            else
            {
                if (count >= 0)
                {

                    MusicName.Location = new Point(--lx1, H);
                    MusicName2.Location = new Point(--lx2, H);
                    count--;
                }
                else
                {
                    MusicName2.Location = new Point(MusicName.Size.Width + 80, H);
                    count = MusicName2.Location.X + MusicName.Size.Width - W;
                    lx2 = MusicName.Size.Width + 80;
                    flag = true;
                }
            }
        }

        #endregion


        #region 播放控制图标（播放、暂停、上一首、下一首）

        private void LastSongBut_MouseEnter(object sender, EventArgs e)
        {
            LastSongBut.BackgroundImage = Properties.Resources.上一首__9_;
            this.Cursor = Cursors.Hand;
        }

        private void LastSongBut_MouseLeave(object sender, EventArgs e)
        {
            LastSongBut.BackgroundImage = Properties.Resources.上一首__8_;
            this.Cursor = Cursors.Default;
        }

        private void NextSongBut_MouseEnter(object sender, EventArgs e)
        {
            NextSongBut.BackgroundImage = Properties.Resources.下一首__5_;
            this.Cursor = Cursors.Hand;
        }

        private void NextSongBut_MouseLeave(object sender, EventArgs e)
        {
            NextSongBut.BackgroundImage = Properties.Resources.下一首__4_;
            this.Cursor = Cursors.Default;
        }

        bool isplay = false;
        private void PlayBut_MouseEnter(object sender, EventArgs e)
        {
            if (isplay)
            {
                PlayBut.BackgroundImage = Properties.Resources.播放__9_;
            }
            else
            {
                PlayBut.BackgroundImage = Properties.Resources.播放__7_;
            }
            this.Cursor = Cursors.Hand;
        }

        private void PlayBut_MouseLeave(object sender, EventArgs e)
        {
            if (isplay)
            {
                PlayBut.BackgroundImage = Properties.Resources.播放__8_;
            }
            else
            {
                PlayBut.BackgroundImage = Properties.Resources.播放__6_;
            }
            this.Cursor = Cursors.Default;
        }

        private void PlayModeBut_MouseEnter(object sender, EventArgs e)
        {
            switch (PlayMode)
            {
                case 0: this.PlayModeBut.BackgroundImage = Properties.Resources.列表循环__5_; break;
                case 1: this.PlayModeBut.BackgroundImage = Properties.Resources.单曲循环__5_; break;
                case 2: this.PlayModeBut.BackgroundImage = Properties.Resources.随机播放__2_; break;
                case 3: this.PlayModeBut.BackgroundImage = Properties.Resources.运行一次__1_; break;
                default: break;
            }
            this.Cursor = Cursors.Hand;
        }

        private void PlayModeBut_MouseLeave(object sender, EventArgs e)
        {
            switch (PlayMode)
            {
                case 0: this.PlayModeBut.BackgroundImage = Properties.Resources.列表循环__3_; break;
                case 1: this.PlayModeBut.BackgroundImage = Properties.Resources.单曲循环__4_; break;
                case 2: this.PlayModeBut.BackgroundImage = Properties.Resources.随机播放__1_; break;
                case 3: this.PlayModeBut.BackgroundImage = Properties.Resources.运行一次; break;
                default: break;
            }
            this.Cursor = Cursors.Default;
        }
        bool haveplayed = false;    //已有播放
        private void PlayBut_Click(object sender, EventArgs e)
        {
            if (isplay) //正在播放
            {
                PlayBut.BackgroundImage = Properties.Resources.播放__6_;
                this.axWindowsMediaPlayer1.Ctlcontrols.pause(); //暂停
                timer2.Enabled = false;
                isplay = false;
                nowPlaying.changePlayBut(false);        //改变musiclist对应的图标
            }
            else
            {
                if (haveplayed)               //如果有播放
                {
                    PlayBut.BackgroundImage = Properties.Resources.播放__8_;
                    this.axWindowsMediaPlayer1.Ctlcontrols.play();
                    timer2.Enabled = true;
                    if (stoping)
                    {
                        timer3.Enabled = true;
                        stoping = false;
                    }
                    isplay = true;
                    nowPlaying.changePlayBut(true);
                }
                else
                {       //如果没有播放（刚开始）
                    if (musicPal != null)
                    {
                        this.play(this.musicPal[0].getPath());  //设置播放第一个
                        this.nowPlaying = musicPal[0];
                        nowPlayIndex = 0;
                        musicPal[0].play();
                    }
                }
            }
        }

        public void PlayButCtl(bool listplay)       //MusicInfo调用函数 改变主窗体的播放按钮
        {
            if (listplay)
            {
                PlayBut.BackgroundImage = Properties.Resources.播放__8_;
                this.axWindowsMediaPlayer1.Ctlcontrols.play();
                timer2.Enabled = true;
                isplay = true;
            }
            else
            {
                PlayBut.BackgroundImage = Properties.Resources.播放__6_;
                this.axWindowsMediaPlayer1.Ctlcontrols.pause();
                timer2.Enabled = false;
                isplay = false;
            }
        }

        private void NextSongBut_Click(object sender, EventArgs e)
        {
            if (nowPlaying != null)
            {
                if (nowPlayIndex == musicPal.Length - 1) { }
                else
                {
                    nowPlayIndex++;
                    nowPlaying.pause();
                    nowPlaying = musicPal[nowPlayIndex];
                    this.play(nowPlaying.getPath());
                    nowPlaying.changePlayBut(true);
                }
            }
        }

        private void LastSongBut_Click(object sender, EventArgs e)
        {
            if (nowPlaying != null)
            {
                if (nowPlayIndex <= 0) { }
                else
                {
                    nowPlayIndex--;
                    nowPlaying.pause();     //停止当前播放项
                    nowPlaying = musicPal[nowPlayIndex];      //更改当前播放项
                    this.play(nowPlaying.getPath());           //播放
                    nowPlaying.changePlayBut(true);           //更改当前播放项的图标
                }
            }
        }
        #endregion


        #region 播放音乐，播放模式
        public void play(string namepath)       //播放
        {
            try
            {
                GeCi.Text = "";
                this.axWindowsMediaPlayer1.URL = namepath;  //设置播放路径
                this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0;
                haveplayed = true;                          //将已有播放设为true   
                MusicName.Text = this.axWindowsMediaPlayer1.currentMedia.name;      //设置歌曲名称
                MusicName2.Text = this.axWindowsMediaPlayer1.currentMedia.name;
                ChargeRoll();                       //判断歌名是否需要滚动显示
                isplay = true;                      //设置正在播放为true
                timer2.Enabled = true;              //播放时间timer 开启
                TotalMusicTimeLab.Text = this.axWindowsMediaPlayer1.currentMedia.durationString;    //设置播放总时长           
                PlayBut.BackgroundImage = Properties.Resources.播放__8_;  //改变播放按钮
                for (int i = 0; i < musicPal.Length; i++)
                {
                    if (musicPal[i].getPath() == namepath)
                    {
                        nowPlayIndex = i;
                        break;
                    }
                }
                if (musicPal[nowPlayIndex].getLrc() != null)
                {
                    setGeCi(musicPal[nowPlayIndex].getLrc());
                }
                else
                {
                    stopGeCi();
                }
                ChangePicture(namepath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        int PlayMode = 0;   //播放模式 0列表循环  1单曲循环    2随机播放  3播放一次


        private void PlayModeBut_Click(object sender, EventArgs e)
        {
            if (PlayMode == 0)
            {
                PlayModeBut.BackgroundImage = Properties.Resources.单曲循环__4_;
                PlayMode++;
                toolTip2.SetToolTip(PlayModeBut, "单曲循环");
            }
            else if (PlayMode == 1)
            {
                PlayModeBut.BackgroundImage = Properties.Resources.随机播放__1_;
                PlayMode++;
                toolTip2.SetToolTip(PlayModeBut, "随机播放");
            }
            else if (PlayMode == 2)
            {
                PlayModeBut.BackgroundImage = Properties.Resources.运行一次__1_;
                PlayMode++;
                toolTip2.SetToolTip(PlayModeBut, "播放一次");
            }
            else
            {
                PlayModeBut.BackgroundImage = Properties.Resources.列表循环__3_;
                PlayMode = 0;
                toolTip2.SetToolTip(PlayModeBut, "列表循环");
            }

        }
        bool stoping = false;
        public void stop()                     //停止播放
        {
            this.axWindowsMediaPlayer1.Ctlcontrols.pause();
            isplay = false;
            GeCIRowTimer.Enabled = false;
            NowTimeLab.Text = "00:00";
            timer3.Enabled = false;
            PlayBut.BackgroundImage = Properties.Resources.播放__6_;
            if (nowPlaying != null)
            {
                nowPlaying.changePlayBut(false);
            }
            GeCi.Text = "";
            stoping = true;
        }

        public void LoopPlay()
        {
            if (nowPlaying != null)
            {
                if (nowPlayIndex == musicPal.Length - 1)
                {
                    nowPlayIndex = 0;
                    nowPlaying.changePlayBut(false);
                    nowPlaying = musicPal[nowPlayIndex];
                    this.play(nowPlaying.getPath());
                    nowPlaying.changePlayBut(true);
                }
                else
                {
                    nowPlayIndex++;
                    nowPlaying.changePlayBut(false);
                    nowPlaying = musicPal[nowPlayIndex];
                    this.play(nowPlaying.getPath());
                    nowPlaying.changePlayBut(true);
                }
            }
        }
        public void SinglePlay()
        {
            if (nowPlaying != null)
            {
                this.play(nowPlaying.getPath());
                nowPlaying.changePlayBut(true);
            }
        }
        Random r = new Random(Guid.NewGuid().GetHashCode());
        public void RandomPlay()
        {
            if (nowPlaying != null)
            {
                nowPlayIndex = r.Next(list.Count);
                nowPlaying.changePlayBut(false);
                nowPlaying = musicPal[nowPlayIndex];
                this.play(nowPlaying.getPath());
                nowPlaying.changePlayBut(true);
            }
        }
        double alltime;//全部时间
        double thistime;//当前时间
        double bfb;//百分比
        double thisX;   //X位置
        private void SetPlayPan()   //设置播放滚动条
        {
            thistime = this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            alltime = this.axWindowsMediaPlayer1.currentMedia.duration;
            bfb = thistime / alltime;
            thisX = 600 * bfb;

            Progress.Value = (int)thisX;
            thisX -= 6;
            if (thisX < 0) { thisX = 0; }
            if (thisX > 587)
            {
                thisX = 587;
            }
            Pro.Location = new Point((int)thisX, Pro.Location.Y);
        }

        private void timer2_Tick(object sender, EventArgs e)    //播放滚动timer
        {
            if (isplay)
            {
                SetPlayPan();
                NowTimeLab.Text = this.axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;

                TotalMusicTimeLab.Text = this.axWindowsMediaPlayer1.currentMedia.durationString;
                if (this.axWindowsMediaPlayer1.playState.ToString() == "wmppsStopped")
                {
                    try
                    {
                        switch (PlayMode)
                        {
                            case 0: this.LoopPlay(); break;
                            case 1: this.SinglePlay(); break;
                            case 2: this.RandomPlay(); break;
                            case 3: this.stop(); break;
                            default: this.stop(); break;
                        }


                        //播放下一首
                    }
                    catch (Exception)
                    {

                        timer2.Enabled = false;
                    }
                }
            }
        }
        #endregion


        #region 添加音乐
        private void PlusMusicBut_MouseEnter(object sender, EventArgs e)
        {
            PlusMusicBut.BackgroundImage = Properties.Resources.添加__1_;
            this.Cursor = Cursors.Hand;
        }

        private void PlusMusicBut_MouseLeave(object sender, EventArgs e)
        {
            PlusMusicBut.BackgroundImage = Properties.Resources.添加;
            this.Cursor = Cursors.Default;
        }
        List<String> list = new List<string>();

        MusicInfo[] musicPal = null;
        int nowPlayIndex = 0;
        private void PlusMusicBut_Click(object sender, EventArgs e) //添加音乐
        {


            OpenFileDialog of = new OpenFileDialog();       //打开一个文件窗口
            of.InitialDirectory = "C:\\Users\\Lenovo\\Music";   //默认打开位置
            of.Filter = "音乐|*.mp3;*.wav;*.wma";             //文件过滤器
            of.RestoreDirectory = true;
            of.FilterIndex = 1;
            of.Multiselect = true;                          //多选择
            if (of.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < of.FileNames.Length; i++)
                {
                    bool flag = true;
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (of.FileNames[i] == list[j])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        list.Insert(0, of.FileNames[i]);
                    }
                }


                SaveMusicList();
            }
        }
        public void SaveMusicList()     //保存至.lst文件
        {
            if (File.Exists(".\\Music.lst") == true)
            {
                File.Delete(".\\Music.lst");
            }
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Music.lst";
            sf.RestoreDirectory = true;
            sf.FilterIndex = 1;
            FileStream fs = new FileStream(string.Format("{0}", sf.FileName), FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, list);     //保存
            fs.Close();
            if (this.nowPlaying != null)    //跟新播放列表
            {
                this.nowPlaying.pause();
            }
            this.MusicListPan.Controls.Clear();

            this.musicPal = new MusicInfo[list.Count];

            int locx = 0;
            int locy = 0;
            for (int i = 0; i < list.Count; i++)
            {
                musicPal[i] = new MusicInfo();

                Point p = new Point(locx, locy);
                locy += 50;
                musicPal[i].Location = p;
                musicPal[i].setFatherForm(this);
                musicPal[i].setMusicNameAndPath(getFileName(list[i]), list[i]);
                this.MusicListPan.Controls.Add(musicPal[i]);
            }
            UpdateLike();
            this.setNowPlaying(musicPal[0]);
            this.nowPlayIndex = 0;
            GetLrc();
            this.nowPlaying.play();
            this.play(musicPal[0].getPath());
        }

        public void GetMusicList()
        {
            string[] musicFile;
            if (File.Exists(".\\Music.lst") == false)
            {
            }
            else
            {
                OpenFileDialog of = new OpenFileDialog();
                of.FileName = "Music.lst";
                of.RestoreDirectory = true;
                of.FilterIndex = 1;
                FileStream fs = new FileStream(string.Format("{0}", of.FileName), FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                this.list = ((List<string>)bf.Deserialize(fs));
                fs.Close();
                musicFile = new string[list.Count];
                musicPal = new MusicInfo[list.Count];
                int locx = 0;
                int locy = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    musicFile[i] = list[i];
                    musicPal[i] = new MusicInfo();
                    Point p = new Point(locx, locy);
                    locy += 50;
                    musicPal[i].Location = p;
                    musicPal[i].setFatherForm(this);
                    musicPal[i].setMusicNameAndPath(getFileName(list[i]), list[i]);
                }

            }
        }
        private string getFileName(string path)     //从路径中获取文件名
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }
        #endregion


        #region  音量控制
        int per = 35;
        bool isSilence = false;

        private void setVolume(int Y, int A)
        {
            double p = (double)Y / A;
            per = (int)(p * 100);

            if (isSilence)
            {
                this.axWindowsMediaPlayer1.settings.volume = 0;
            }
            else
            {
                this.axWindowsMediaPlayer1.settings.volume = per;
            }
            this.VolumePersentLab.Text = per.ToString() + "%";
        }
        private void SilenceSetVolume()
        {
            double p = (double)per / 100;

            VolumUpPan.Size = new Size(5, (int)(p * 150));
            VolumUpPan.Location = new Point(0, 150 - (int)(p * 150));
            this.axWindowsMediaPlayer1.settings.volume = per;
            this.VolumePersentLab.Text = per.ToString() + "%";
        }
        private void VolumeBackPan_MouseDown(object sender, MouseEventArgs e)
        {
            VolumUpPan.Size = new Size(5, 150 - e.Location.Y);
            VolumUpPan.Location = new Point(0, e.Location.Y);
            setVolume(150 - e.Location.Y, 150); Volumflag = true;
        }

        private void VolumUpPan_MouseDown(object sender, MouseEventArgs e)
        {
            int rx = VolumUpPan.Size.Height;
            VolumUpPan.Size = new Size(5, rx - e.Location.Y);
            VolumUpPan.Location = new Point(0, 150 - (rx - e.Location.Y));
            setVolume(rx - e.Location.Y, 150); Volumflag = true;
        }

        bool Volumflag = false;
        private void VolumUpPan_MouseMove(object sender, MouseEventArgs e)
        {
            if (Volumflag)
            {
                int rx = VolumUpPan.Size.Height;
                VolumUpPan.Size = new Size(5, rx - e.Location.Y);
                VolumUpPan.Location = new Point(0, 150 - (rx - e.Location.Y));
                setVolume(rx - e.Location.Y, 150);
            }
        }

        private void VolumUpPan_MouseUp(object sender, MouseEventArgs e)
        {
            Volumflag = false;
        }

        private void VolumeBackPan_MouseMove(object sender, MouseEventArgs e)
        {
            if (Volumflag)
            {
                VolumUpPan.Size = new Size(5, 150 - e.Location.Y);
                VolumUpPan.Location = new Point(0, e.Location.Y);
                setVolume(150 - e.Location.Y, 150); Volumflag = true;
            }
        }
        private void VolumeBackPan_MouseUp(object sender, MouseEventArgs e)
        {
            Volumflag = false;
        }
        private void VolumeBackPan_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void VolumeBackPan_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void VolumUpPan_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void VolumUpPan_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void SilenceBut_Click(object sender, EventArgs e)
        {
            if (isSilence)
            {
                SilenceBut.BackgroundImage = Properties.Resources.声音__1_;
                volumeBut.BackgroundImage = Properties.Resources.声音__1_;
                SilenceSetVolume();
                isSilence = false;
            }
            else
            {
                SilenceBut.BackgroundImage = Properties.Resources.静音__1_;
                volumeBut.BackgroundImage = Properties.Resources.静音__1_;
                isSilence = true;
                this.axWindowsMediaPlayer1.settings.volume = 0;
            }
        }

        private void SilenceBut_MouseEnter(object sender, EventArgs e)
        {
            if (isSilence)
            {
                SilenceBut.BackgroundImage = Properties.Resources.静音__1_;
            }
            else
            {
                SilenceBut.BackgroundImage = Properties.Resources.声音__1_;
            }
            this.Cursor = Cursors.Hand;
        }

        private void SilenceBut_MouseLeave(object sender, EventArgs e)
        {
            if (isSilence)
            {
                SilenceBut.BackgroundImage = Properties.Resources.静音;
            }
            else
            {
                SilenceBut.BackgroundImage = Properties.Resources.声音;
            }
            this.Cursor = Cursors.Default;
        }

        bool Volumeopen = false;
        private void volumeBut_Click(object sender, EventArgs e)
        {
            if (Volumeopen)
            {
                VolumPan.Visible = false;
                Volumeopen = false;
            }
            else
            {
                VolumPan.Visible = true;
                Volumeopen = true;
            }
        }

        private void volumeBut_MouseEnter(object sender, EventArgs e)
        {
            if (isSilence)
            {
                volumeBut.BackgroundImage = Properties.Resources.静音__1_;
            }
            else
            {
                volumeBut.BackgroundImage = Properties.Resources.声音__1_;
            }
            this.Cursor = Cursors.Hand;

        }

        private void volumeBut_MouseLeave(object sender, EventArgs e)
        {
            if (isSilence)
            {
                volumeBut.BackgroundImage = Properties.Resources.静音;
            }
            else
            {
                volumeBut.BackgroundImage = Properties.Resources.声音;
            }
            this.Cursor = Cursors.Default;
        }


        #endregion


        #region 歌词，拉动条

        
        ShowLrc s = new ShowLrc();
        string[] lrcTime = new string[200];//保存歌曲时间
        string[] lrcText = new string[200];//保存歌词文字

        public void stopGeCi()
        {
            timer3.Enabled = false;
            GeCi.Visible = false;
        }
        public void setGeCi(String path)
        {
            s.getLrc(path);
            lrcText = s.returnText();
            lrcTime = s.returnTime();
            if (timer3.Enabled != true)
            {
                timer3.Enabled = true;
                GeCi.Visible = true;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < lrcTime.Length; i++)
            {
                if (lrcTime[i] != null)
                {
                    if (getDoubleTime(lrcTime[i]) > this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition)
                    {
                        if (i != 0)
                        {
                            if (GeCi.Text != lrcText[i - 1])
                            {
                                GeCi.Text = lrcText[i - 1];
                                ChargeRoll2();

                            }
                        }
                        break;
                    }
                }
            }

        }
        public void changeGeci(double time)
        {
            for (int i = 0; i < lrcTime.Length; i++)
            {
                if (lrcTime[i] != null)
                {
                    if (getDoubleTime(lrcTime[i]) > time)
                    {
                        if (i != 0)
                        {
                            if (GeCi.Text != lrcText[i - 1])
                            {
                                GeCi.Text = lrcText[i - 1];
                                ChargeRoll2();
                            }
                        }
                        break;
                    }
                }
            }
        }

        List<MusicandLrc> lrclist = new List<MusicandLrc>();

        public void GetLrc()        //从文件中读取歌词
        {
            if (lrclist == null)
            {
                List<MusicandLrc> lrclist = new List<MusicandLrc>();
            }
            if (File.Exists(".\\Musiclrc.lst") == false)
            {
            }
            else
            {
                OpenFileDialog of = new OpenFileDialog();
                of.FileName = "Musiclrc.lst";
                of.RestoreDirectory = true;
                of.FilterIndex = 1;
                FileStream fs = new FileStream(string.Format("{0}", of.FileName), FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                lrclist = ((List<MusicandLrc>)bf.Deserialize(fs));
                fs.Close();
                for (int i = 0; i < lrclist.Count; i++)
                {
                    for (int j = 0; j < musicPal.Length; j++)
                    {
                        if (musicPal[j].getPath() == lrclist[i].Musicname)
                        {
                            musicPal[j].setLrc(lrclist[i].lrcPath);
                        }
                    }
                }
            }
        }
        public void SaveMusicLrcList(String lrcpath, String musicname)     //保存至.lst文件
        {
            MusicandLrc m = new MusicandLrc();
            m.lrcPath = lrcpath;
            m.Musicname = musicname;
            int num = -1;
            for (int i = 0; i < lrclist.Count; i++)
            {
                if (musicname.Equals(lrclist[i].Musicname))
                {
                    num = i;
                }
            }
            if (num != -1)
            {
                lrclist.RemoveAt(num);
            }
            lrclist.Add(m);
            if (File.Exists(".\\Musiclrc.lst") == true)
            {
                File.Delete(".\\Musiclrc.lst");
            }
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Musiclrc.lst";
            sf.RestoreDirectory = true;
            sf.FilterIndex = 1;
            FileStream fs = new FileStream(string.Format("{0}", sf.FileName), FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, lrclist);     //保存
            fs.Close();
        }

        //判断歌词是否需要滚动
        int count2 = 0;
        int lx12 = 0;
        int lx22 = 0;
        int W2 = 600;
        int H2 = 7;

        private void ChargeRoll2()
        {
            if (GeCi.Size.Width < (W2 - 10))
            {
                GeCi.Location = new Point(10, H2);
                GeCi2.Visible = false;
                timer4.Enabled = false;
            }
            else
            {
                GeCi.Location = new Point(10, H2);
                GeCi2.Location = new Point(GeCi.Size.Width + 50, H2);
                GeCi2.Visible = true;
                GeCi2.Text = GeCi.Text;
                count2 = GeCi2.Location.X + GeCi.Size.Width - W2;
                lx12 = 10;
                lx22 = GeCi.Size.Width + 50;
                timer4.Enabled = true;

            }
        }
        bool flag2 = false;
        private void timer4_Tick_1(object sender, EventArgs e)
        {
            if (flag2)
            {
                if (count2 >= 0)
                {
                    GeCi.Location = new Point(--lx12, H2);
                    GeCi2.Location = new Point(--lx22, H2);
                    count2--;
                }
                else
                {
                    GeCi.Location = new Point(GeCi2.Size.Width + 50, H2);
                    count = GeCi.Location.X + GeCi.Size.Width - W2;
                    lx12 = GeCi.Size.Width + 50;
                    flag2 = false;
                }
            }
            else
            {
                if (count2 >= 0)
                {
                    GeCi.Location = new Point(--lx12, H2);
                    GeCi2.Location = new Point(--lx22, H2);
                    count2--;
                }
                else
                {
                    GeCi2.Location = new Point(GeCi.Size.Width + 50, H2);
                    count2 = GeCi2.Location.X + GeCi.Size.Width - W2;
                    lx22 = GeCi.Size.Width + 50;
                    flag2 = true;
                }
            }
        }
        private double getDoubleTime(string time)   //用时间字符串解析为double时间 01:32:34  分：秒：毫秒    
        {
            string[] t = time.Split(':');
            if (t[0].First() == '0')
            {
                t[0].Remove(0, 1);
            }
            int m = int.Parse(t[0]);
            if (t[1].First() == '0')
            {
                t[1].Remove(0, 1);
            }
            int s = int.Parse(t[1]);
            if (t[2].First() == '0')
            {
                t[2].Remove(0, 1);
            }
            int w = int.Parse(t[2]);
            double sum = (double)m * 60 + (double)s + (double)w / 100;
            return sum;
        }
        private string getTime(double t)            //用double时间解析成时间字符串
        {
            t *= 100;
            StringBuilder sb = new StringBuilder();
            int m = (int)(t / 100) / 60;
            int s = (int)(t / 100) % 60;
            int w = (int)t % 100;
            if (m < 10)
            {
                sb.Append("0").Append(m);
            }
            else
            {
                sb.Append(m);
            }
            if (s < 10)
            {
                sb.Append(":").Append("0").Append(s);
            }
            else
            {
                sb.Append(":").Append(s);
            }

            if (w < 10)
            {
                sb.Append(":").Append("0").Append(w);
            }
            else
            {
                sb.Append(":").Append(w);
            }
            return sb.ToString();
        }

        //拉动条

        bool MusicPlayflag = false;     //拉动条按下flag  只有按下才可以拉动

        private void changeTime(double all, double x)    //改变时间
        {
            if (nowPlaying != null && !stoping)
            {
                double b = x / all;
                double alltime = this.axWindowsMediaPlayer1.currentMedia.duration;
                double thisTime = alltime * b;
                this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = thisTime;
            }
        }
        private void panel12_MouseEnter(object sender, EventArgs e)
        {
            Pro.Visible = true;
        }

        private void Progress_MouseEnter(object sender, EventArgs e)
        {
            Pro.Visible = true;
        }

        private void Pro_MouseEnter(object sender, EventArgs e)
        {
            Pro.Visible = true;
        }

        private void panel12_MouseLeave(object sender, EventArgs e)
        {
            Pro.Visible = false;
        }

        private void Progress_MouseDown(object sender, MouseEventArgs e)
        {
            if (nowPlaying != null&&!stoping)
            {
                Progress.Value = e.Location.X;
                changeTime(600, e.Location.X);
                int x2 = e.Location.X - 6;
                if (x2 < 0) { x2 = 0; }
                if (x2 > 587) { x2 = 587; }
                Pro.Location = new Point(x2, Pro.Location.Y);
                MusicPlayflag = true;
            }

        }

        private void Progress_MouseMove(object sender, MouseEventArgs e)
        {
            int x = 0, x2 = 0;
            if (nowPlaying != null&&!stoping)
            {
                if (MusicPlayflag)
                {
                    x = e.Location.X;
                    x2 = e.Location.X - 6;
                    if (x < 0) { x = 0; }
                    if (x > (600)) { x = 600; }
                    if (x2 < 0) { x2 = 0; }
                    if (x2 > 587) { x2 = 587; }
                    Progress.Value = x;
                    //changeTime(600, e.Location.X);
                    Pro.Location = new Point(x2, Pro.Location.Y);
                }
            }
        }

        private void Progress_MouseUp(object sender, MouseEventArgs e)
        {
            changeTime(600, e.Location.X);
            this.MusicPlayflag = false;
        }

        private void panel12_MouseUp(object sender, MouseEventArgs e)
        {
            changeTime(600, e.Location.X);
            this.MusicPlayflag = false;
        }

        private void panel12_MouseMove(object sender, MouseEventArgs e)
        {
            int x = 0, x2 = 0;
            if(nowPlaying != null && !stoping)
            {
                if (MusicPlayflag)
                {
                    x = e.Location.X;
                    x2 = e.Location.X - 6;
                    if (x < 0) { x = 0; }
                    if (x > (600)) { x = 600; }
                    if (x2 < 0) { x2 = 0; }
                    if (x2 > 587) { x2 = 587; }
                    Progress.Value = x;
                    //changeTime(600, e.Location.X);
                    Pro.Location = new Point(x2, Pro.Location.Y);
                }
            }
        }

        private void panel12_MouseDown(object sender, MouseEventArgs e)
        {
            if(nowPlaying != null && !stoping)
            {
                Progress.Value = e.Location.X;
                changeTime(600, e.Location.X);
                int x2 = e.Location.X - 6;
                if (x2 < 0) { x2 = 0; }
                if (x2 > 587) { x2 = 587; }
                Pro.Location = new Point(x2, Pro.Location.Y);
                MusicPlayflag = true;
            }

        }
        public Point p1, p2;

        private void Pro_MouseMove(object sender, MouseEventArgs e)
        {
            //鼠标相对于屏幕的坐标
            p1 = MousePosition;
            //鼠标相对于窗体的坐标
            p2 = panel12.PointToClient(p1);
            int x = 0, x2 = 0;
            if(nowPlaying != null && !stoping)
            {
                if (MusicPlayflag)
                {
                    x = p2.X; x2 = p2.X - 6;
                    if (x < 0) { x = 0; }
                    if (x > (600)) { x = 600; }
                    if (x2 < 0) { x2 = 0; }
                    if (x2 > 587) { x2 = 587; }
                    Progress.Value = x;
                    //changeTime(600, e.Location.X);
                    Pro.Location = new Point(x2, Pro.Location.Y);
                }
            }
        }

        private void Pro_MouseUp(object sender, MouseEventArgs e)
        {
            //鼠标相对于屏幕的坐标
            p1 = MousePosition;
            //鼠标相对于窗体的坐标
            p2 = panel12.PointToClient(p1);
            changeTime(600, p2.X);
            this.MusicPlayflag = false;
        }

        private void Pro_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标相对于屏幕的坐标
            p1 = MousePosition;
            //鼠标相对于窗体的坐标
            p2 = panel12.PointToClient(p1);

            if(nowPlaying != null && !stoping)
            {
                Progress.Value = p2.X;
                changeTime(600, p2.X);
                int x2 = p2.X - 6;
                if (x2 < 0) { x2 = 0; }
                if (x2 > 587) { x2 = 587; }
                Pro.Location = new Point(x2, Pro.Location.Y);
                MusicPlayflag = true;
            }
        }

        #endregion


        #region 更改专辑图片 调用了mp3File项目来解析mp3文件，获得id3标签中的专辑图片
        public void ChangePicture(string filename)
        {
            Mp3File mp3File = null;

            try
            {
                // create mp3 file wrapper; open it and read the tags
                mp3File = new Mp3File(filename);                                        //新建Mp3File
                this.MusicPpicture.BackgroundImage = mp3File.TagHandler.Picture;        //获得专辑图片
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        #endregion


        #region 给MusicInfo调用的函数
        //添加，删除喜爱，设置当前正在播放的音乐，保存音乐列表，保存歌词列表，删除音乐

        //添加喜爱列表
        public void addLike(String path)
        {
            if (!loginflag)
            {
                DialogResult dr = MessageBoxEx.Show(this, "未登录");
            }
            else
            {
                if (likeMusicPath != null)
                {
                    bool flag = true;
                    for (int i = 0; i < likeMusicPath.Count; i++)   //不重复添加
                    {
                        if (path == likeMusicPath[i])
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        this.likeMusicPath.Add(path);
                        UpdateLike();
                    }
                }
                else
                {
                    DialogResult dr = MessageBoxEx.Show(this, "未登录");
                }
            }

        }

        //删除喜爱列表
        public void delLike(String path)
        {
            if (loginflag)
            {
                int index = -1;
                for (int i = 0; i < likeMusicPath.Count; i++)   //先查找是否在列表里
                {
                    if (path == likeMusicPath[i])
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    likeMusicPath.RemoveAt(index);
                }
                if (!leftchooseflag)
                {
                    leftchooseflag = true;
                    changelisttolike();
                }
                UpdateLike();
            }
        }
        MusicInfo nowPlaying = null;        //当前正在播放的musicinfo

        public void setNowPlaying(MusicInfo m)  //设置正在播放MusicInfo 
        {
            this.nowPlaying = m;
            for (int i = 0; i < musicPal.Length; i++)
            {
                if (musicPal[i] == m)
                {
                    nowPlayIndex = i;
                    break;
                }
            }
            if (stoping)
            {
                timer3.Enabled = true;
                stoping = false;
            }
        }
        public MusicInfo getNowPlaying()
        {
            return this.nowPlaying;
        }
        public void SaveMusiList2()
        {
            if (File.Exists(".\\Music.lst") == true)
            {
                File.Delete(".\\Music.lst");
            }
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Music.lst";
            sf.RestoreDirectory = true;
            sf.FilterIndex = 1;
            FileStream fs = new FileStream(string.Format("{0}", sf.FileName), FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, list);     //保存
            fs.Close();
        }
        public void SaveMusicLrcList2()
        {
            if (File.Exists(".\\Musiclrc.lst") == true)
            {
                File.Delete(".\\Musiclrc.lst");
            }
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Musiclrc.lst";
            sf.RestoreDirectory = true;
            sf.FilterIndex = 1;
            FileStream fs = new FileStream(string.Format("{0}", sf.FileName), FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, lrclist);     //保存
            fs.Close();
        }

        private void TransDegBackGround_MouseLeave(object sender, EventArgs e)
        {

        }

        private void TransDegBackGround_MouseEnter(object sender, EventArgs e)
        {

        }

        private void TransDegBackGround_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void TransDeg_MouseEnter(object sender, EventArgs e)
        {

        }

        private void TransDeg_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void TransDeg_MouseLeave(object sender, EventArgs e)
        {

        }

        

        public void DeleteMusic(String musicpath, String lrcpath)        //删除音乐
        {
            int mi = -1;
            for (int i = 0; i < list.Count; i++)     //从音乐list列表中移除
            {
                if (musicpath.Equals(list[i]))
                {
                    mi = i;
                    break;
                }
            }
            if (mi != -1)
            {
                list.RemoveAt(mi);
            }
            if (lrcpath != null)                    //从歌词lrclist列表中移除
            {
                int li = -1;
                for (int i = 0; i < lrclist.Count; i++)
                {
                    if (lrcpath.Equals(lrclist[i].lrcPath) && musicpath.Equals(lrclist[i].Musicname))
                    {
                        li = i;
                        break;
                    }
                }
                if (li != -1)
                    lrclist.RemoveAt(li);
            }
            SaveMusicLrcList2();                //保存
            SaveMusiList2();                    //保存
            bool flag = false;
            String nowplaying = null;
            if (nowPlaying != null)             //保存当前播放的音乐
            {
                nowplaying = nowPlaying.getPath();
                if (nowplaying == musicpath)
                {
                    flag = true;
                }
            }
            this.MusicListPan.Controls.Clear();     //清空再添加
            if (list.Count != 0)
            {
                this.musicPal = new MusicInfo[list.Count];
                int locx = 0;
                int locy = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    musicPal[i] = new MusicInfo();
                    Point p = new Point(locx, locy);
                    locy += 50;
                    musicPal[i].Location = p;
                    musicPal[i].setFatherForm(this);
                    musicPal[i].setMusicNameAndPath(getFileName(list[i]), list[i]);
                    this.MusicListPan.Controls.Add(musicPal[i]);

                }
                UpdateLike();
                if (flag)           //删除的是当前正在播放的音乐
                {

                    this.setNowPlaying(musicPal[0]);
                    this.nowPlayIndex = 0;
                    GetLrc();
                    this.nowPlaying.play();
                    this.play(musicPal[0].getPath());
                }
                else
                {                   //不是当前正在播放的音乐
                    int index = -1;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (nowplaying == list[i])
                        {
                            index = i;
                            break;
                        }
                    }
                    if (index != -1)
                    {
                        this.setNowPlaying(musicPal[index]);
                        this.nowPlayIndex = index;
                        GetLrc();
                        this.nowPlaying.play();
                    }
                }
            }
            else
            {       //被删光
                this.axWindowsMediaPlayer1.Ctlcontrols.pause();
                this.MusicPpicture.BackgroundImage = Properties.Resources.音乐;
                this.MusicName.Text = "Welcome to use MyMusicPlayer";
                this.MusicName2.Text = "Welcome to use MyMusicPlayer";
                haveplayed = false;
            }

        }

        #endregion


        #region title面板鼠标移动tick 移入Visable=true;
        private void timer5_Tick_1(object sender, EventArgs e)
        {


            CheckForIllegalCrossThreadCalls = false;
            //鼠标相对于屏幕的坐标
            Point p1 = MousePosition;
            //鼠标相对于窗体的坐标
            Point p2 = this.PointToClient(p1);
            int x = p2.X;
            int y = p2.Y;
            if (x < panel6.Width && y < panel6.Height)
            {
                panel6.Visible = true;
            }
            else
            {
                panel6.Visible = false;
            }
        }
        #endregion


        #region
        /*  被垃圾回收后出现异常，未解决
        kh = new KeyboardHook();
        kh.SetHook();
        kh.OnKeyDownEvent += kh_OnKeyDownEvent;
         
        #region  全局键盘事件空格暂停/播放
        KeyboardHook kh;
        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Space))
            {
                if (isplay) //正在播放
                {
                    PlayBut.BackgroundImage = Properties.Resources.播放__6_;
                    this.axWindowsMediaPlayer1.Ctlcontrols.pause(); //暂停
                    timer2.Enabled = false;
                    isplay = false;
                    nowPlaying.changePlayBut(false);        //改变musiclist对应的图标
                }
                else
                {
                    if (haveplayed)               //如果有播放
                    {
                        PlayBut.BackgroundImage = Properties.Resources.播放__8_;
                        this.axWindowsMediaPlayer1.Ctlcontrols.play();
                        timer2.Enabled = true;
                        isplay = true;
                        nowPlaying.changePlayBut(true);
                    }
                    else
                    {       //如果没有播放（刚开始）
                        if (musicPal != null)
                        {
                            this.play(this.musicPal[0].getPath());  //设置播放第一个
                            this.nowPlaying = musicPal[0];
                            nowPlayIndex = 0;
                            musicPal[0].play();
                        }
                    }
                }       //改变musiclist对应的图标 }//Ctrl+S显示窗口
            }
        }
        #endregion
        */
        #endregion

    }
}