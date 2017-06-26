using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 音乐播放器
{
    public partial class MusicInfo : UserControl
    {
        bool isPlay = false;        //是否正在播放
        bool isLike = false;        //是否喜爱
        String MusicName = null;    //音乐名称
        String MusicPath = null;    //音乐路径
        Form1 fatherForm = null;    //主窗体指针
        String lrcPath = null;      //歌词路径
        public MusicInfo(MusicInfo m)
        {
            InitializeComponent();
            this.isPlay = m.isPlay;
            this.isLike = m.isLike;
            this.MusicName = m.MusicName;
            this.MusicPath = m.MusicPath;
            this.fatherForm = m.fatherForm;
            this.lrcPath = m.lrcPath;
            
            this.changePlayBut(isPlay);
            this.changeLike(isLike);
        }
        public void setFatherForm(Form1 fatherForm)
        {
            this.fatherForm = fatherForm;
        }
        public String getPath()
        {
            return this.MusicPath;
        }
        public void setMusicNameAndPath(String MusicName, String MusicPath)
        {
            this.MusicName = MusicName;
            this.MusicPath = MusicPath;
            this.MusicName1.Text = MusicName;
            this.MusicName2.Text = MusicName;
            ChargeRoll();
        }
        public void setLrc(String lrcPath)
        {
            this.lrcPath = lrcPath;
        }
        public String getLrc()
        {
            return this.lrcPath;
        }

        #region 减轻闪烁
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0014) // 禁掉清除背景消息  

                return;

            base.WndProc(ref m);

        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;////用双缓冲从下到上绘制窗口的所有子孙
                return cp;
            }
        }
        #endregion

        
        public MusicInfo()
        {
            InitializeComponent();
        }

        private void MusicInfo_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.DeleteBut, "删除");
            toolTip2.SetToolTip(this.LikeBut, "添加喜欢");
            toolTip3.SetToolTip(this.PlusGeCiBut, "添加歌词");
            isPlay = false;
        }

        #region 鼠标移入移除处理函数

        private void MusicInfo_MouseEnter(object sender, EventArgs e)
        {
            
           
            this.BackColor = Color.Gray;
        }

        private void MusicInfo_MouseLeave(object sender, EventArgs e)
        {
            
            
            this.BackColor = Color.Transparent;
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            
            this.BackColor = Color.Gray;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            
            this.BackColor = Color.Transparent;
        }

        private void Playbut_MouseEnter(object sender, EventArgs e)
        {
            if (isPlay)
            {
                this.Playbut.BackgroundImage = Properties.Resources.暂停__1_;
            }
            else
            {
                this.Playbut.BackgroundImage = Properties.Resources.play__1_;      
            }
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.Gray;

        }

        private void Playbut_MouseLeave(object sender, EventArgs e)
        {
            if (isPlay)
            {
                this.Playbut.BackgroundImage = Properties.Resources.暂停;              
            }
            else
            {
                this.Playbut.BackgroundImage = Properties.Resources.play;  
            }

            this.Cursor = Cursors.Default;
            this.BackColor = Color.Transparent;
            
        }

        private void DeleteBut_MouseEnter(object sender, EventArgs e)
        {
            this.DeleteBut.BackgroundImage = Properties.Resources.删除__1_;
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.Gray;
        }

        private void DeleteBut_MouseLeave(object sender, EventArgs e)
        {
            this.DeleteBut.BackgroundImage = Properties.Resources.删除;
            this.Cursor = Cursors.Default;
            this.BackColor = Color.Transparent;
           
        }

        private void LikeBut_MouseEnter(object sender, EventArgs e)
        {
            if (isLike)
            {
                this.LikeBut.BackgroundImage = Properties.Resources.favor_fill__1_;
            }else
            {
                this.LikeBut.BackgroundImage = Properties.Resources.favor__1_;
            }
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.Gray;

        }

        private void LikeBut_MouseLeave(object sender, EventArgs e)
        {
            if (isLike)
            {
                
            }
            else
            {
                this.LikeBut.BackgroundImage = Properties.Resources.favor;
            }
            this.Cursor = Cursors.Default;
            this.BackColor = Color.Transparent;
           

        }

        private void PlusGeCiBut_MouseEnter(object sender, EventArgs e)
        {
            this.PlusGeCiBut.BackgroundImage = Properties.Resources.添加__5_;
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.Gray;

        }

        private void PlusGeCiBut_MouseLeave(object sender, EventArgs e)
        {
            this.PlusGeCiBut.BackgroundImage = Properties.Resources.添加__4_;
            this.Cursor = Cursors.Default;
            this.BackColor = Color.Transparent;
            

        }
        private void MusicName1_MouseEnter(object sender, EventArgs e)
        {


            this.BackColor = Color.Gray;
        }

        private void MusicName2_MouseEnter(object sender, EventArgs e)
        {

            this.BackColor = Color.Gray;
        }

        private void MusicName1_MouseLeave(object sender, EventArgs e)
        {

            this.BackColor = Color.Transparent;
        }

        private void MusicName2_MouseLeave(object sender, EventArgs e)
        {

            this.BackColor = Color.Transparent;
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {

            this.BackColor = Color.Gray;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {

            this.BackColor = Color.Transparent;
        }
        #endregion


        #region 判断歌名是否需要滚动
        int count = 0;
        int lx1 = 0;
        int lx2 = 0;
        int W = 450;
        int H = 12;
        private void ChargeRoll()
        {
            if (MusicName1.Size.Width < (W-10))
            {
                MusicName1.Location = new Point((W - MusicName1.Size.Width) / 2, H);
                MusicName2.Visible = false;
                timer1.Enabled = false;
            }
            else
            {
                MusicName1.Location = new Point(10, H);
                MusicName2.Location = new Point(MusicName1.Size.Width + 80, H);
                count = MusicName2.Location.X + MusicName1.Size.Width - W;
                lx1 = 10;
                lx2 = MusicName1.Size.Width + 80;
                timer1.Enabled = true;

            }
        }
        bool flag = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                if (count >= 0)
                {
                    MusicName1.Location = new Point(--lx1, H);
                    MusicName2.Location = new Point(--lx2, H);
                    count--;
                }
                else
                {
                    MusicName1.Location = new Point(MusicName1.Size.Width + 80, H);
                    count = MusicName1.Location.X + MusicName1.Size.Width - W;
                    lx1 = MusicName1.Size.Width + 80;
                    flag = false;
                }
            }
            else
            {
                if (count >= 0)
                {
                    MusicName1.Location = new Point(--lx1, H);
                    MusicName2.Location = new Point(--lx2, H);
                    count--;
                }
                else
                {
                    MusicName2.Location = new Point(MusicName1.Size.Width + 80, H);
                    count = MusicName2.Location.X + MusicName1.Size.Width - W;
                    lx2 = MusicName1.Size.Width + 80;
                    flag = true;
                }
            }
        }
        #endregion


        private void Playbut_Click(object sender, EventArgs e)
        {
            if (isPlay)
            {
                this.pause();
            }else
            {
                this.play();
            }
        }
        public void pause()
        {
            this.Playbut.BackgroundImage= Properties.Resources.play;
            isPlay = false;
            this.fatherForm.PlayButCtl(false);
        } 
        public void play()
        {
            this.Playbut.BackgroundImage = Properties.Resources.暂停;
            isPlay = true;
            if (fatherForm.getNowPlaying() != null)
            {
                if (fatherForm.getNowPlaying() != this)
                {
                    fatherForm.getNowPlaying().pause();
                    fatherForm.setNowPlaying(this);
                    this.Playbut.BackgroundImage = Properties.Resources.play;
                    this.replay();
                }
                else
                {
                    fatherForm.setNowPlaying(this);
                    this.fatherForm.PlayButCtl(true);
                }
            }
            else
            {
                fatherForm.setNowPlaying(this);
                this.replay();
            }

        }
        public void changePlayBut(bool flag)        //主窗体调用改变播放安妮状态
        {
            if (flag)
            {
                this.Playbut.BackgroundImage = Properties.Resources.暂停;
                isPlay = true;
            }
            else
            {
                this.Playbut.BackgroundImage = Properties.Resources.play;
                isPlay = false;
            }
        }
        public void replay()                    //重新播放 调用主窗体的play函数
        {
            this.Playbut.BackgroundImage = Properties.Resources.暂停;
            isPlay = true;
            this.fatherForm.play(this.MusicPath);
        }
        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            if (isPlay)
            {
                this.pause();
            }
            else
            {
                if (fatherForm.getNowPlaying() != null)
                {
                    if (fatherForm.getNowPlaying() != this)
                    {
                        fatherForm.getNowPlaying().pause();
                        fatherForm.setNowPlaying(this);
                        this.replay();
                    }
                    else
                    {
                        fatherForm.setNowPlaying(this);
                        this.play();
                    }
                }else
                {
                    fatherForm.setNowPlaying(this);
                    this.replay();
                }
                
            }
        }

        private void MusicInfo_DoubleClick(object sender, EventArgs e)
        {
            if (isPlay)
            {
                this.pause();
            }
            else
            {
                if (fatherForm.getNowPlaying() != null)
                {
                    if (fatherForm.getNowPlaying() != this)
                    {
                        fatherForm.getNowPlaying().pause();
                        fatherForm.setNowPlaying(this);
                        this.replay();
                    }
                    else
                    {
                        fatherForm.setNowPlaying(this);
                        this.play();
                    }
                }
                else
                {
                    fatherForm.setNowPlaying(this);
                    this.replay();
                }

            }
        }

       

        private void MusicName1_DoubleClick(object sender, EventArgs e)
        {
            if (isPlay)
            {
                this.pause();
            }
            else
            {
                if (fatherForm.getNowPlaying() != null)
                {
                    if (fatherForm.getNowPlaying() != this)
                    {
                        fatherForm.getNowPlaying().pause();
                        fatherForm.setNowPlaying(this);
                        this.replay();
                    }
                    else
                    {
                        fatherForm.setNowPlaying(this);
                        this.play();
                    }
                }
                else
                {
                    fatherForm.setNowPlaying(this);
                    this.replay();
                }

            }
        }

       

        private void MusicInfo_Paint(object sender, PaintEventArgs e)       //画个边框，上下两条线，左右无线
        {
            Rectangle myRectangle = new Rectangle(0, 0, this.Width, this.Height);
            ControlPaint.DrawBorder(e.Graphics, myRectangle,
            Color.Transparent, 1, ButtonBorderStyle.Solid,  //上
            Color.Gray, 1, ButtonBorderStyle.Solid,         //左
            Color.Transparent, 1, ButtonBorderStyle.Solid,  //下
            Color.Gray, 1, ButtonBorderStyle.Solid          //右
            );
        }

        private void LikeBut_Click(object sender, EventArgs e)
        {
            if (isLike)
            {
                LikeBut.BackgroundImage = Properties.Resources.favor__1_;
                this.fatherForm.delLike(this.MusicPath);
                isLike = false;
                
            }
            else
            {
                LikeBut.BackgroundImage = Properties.Resources.favor_fill__1_;
                this.fatherForm.addLike(this.MusicPath);
                isLike = true;
            }
        }

        
        private void PlusGeCiBut_Click(object sender, EventArgs e)  //添加歌词
        {
            OpenFileDialog of = new OpenFileDialog();       //打开一个文件窗口
            of.InitialDirectory = "C:\\Users\\Lenovo\\Music";   //默认打开位置
            of.Filter = "歌词|*.lrc";             //文件过滤器
            of.RestoreDirectory = true;
            of.FilterIndex = 1;
            if (of.ShowDialog() == DialogResult.OK)
            {
                lrcPath = of.FileName;
            }
            this.fatherForm.SaveMusicLrcList(this.lrcPath, this.MusicPath);
            if (fatherForm.getNowPlaying() == this||fatherForm.getNowPlaying()==null)
            {
                fatherForm.setGeCi(lrcPath);
            }
        }

        private void DeleteBut_Click(object sender, EventArgs e)
        {
            this.fatherForm.DeleteMusic(this.MusicPath, this.lrcPath);
        }
        public void changeLike(bool flag)
        {
            if (flag)
            {
                this.isLike = true;
                LikeBut.BackgroundImage = Properties.Resources.favor_fill__1_;
            }else
            {
                LikeBut.BackgroundImage = Properties.Resources.favor;
                isLike = false;
            }
        }
    }
}
