namespace 音乐播放器
{
    partial class MusicInfo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicInfo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.MusicName2 = new System.Windows.Forms.Label();
            this.MusicName1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Playbut = new System.Windows.Forms.PictureBox();
            this.PlusGeCiBut = new System.Windows.Forms.PictureBox();
            this.LikeBut = new System.Windows.Forms.PictureBox();
            this.DeleteBut = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Playbut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlusGeCiBut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikeBut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeleteBut)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MusicName2);
            this.panel1.Controls.Add(this.MusicName1);
            this.panel1.Location = new System.Drawing.Point(33, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 44);
            this.panel1.TabIndex = 0;
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // MusicName2
            // 
            this.MusicName2.AutoSize = true;
            this.MusicName2.BackColor = System.Drawing.Color.Transparent;
            this.MusicName2.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MusicName2.ForeColor = System.Drawing.Color.White;
            this.MusicName2.Location = new System.Drawing.Point(325, 13);
            this.MusicName2.Name = "MusicName2";
            this.MusicName2.Size = new System.Drawing.Size(52, 17);
            this.MusicName2.TabIndex = 1;
            this.MusicName2.Text = "label1";
            this.MusicName2.MouseEnter += new System.EventHandler(this.MusicName2_MouseEnter);
            this.MusicName2.MouseLeave += new System.EventHandler(this.MusicName2_MouseLeave);
            // 
            // MusicName1
            // 
            this.MusicName1.AutoSize = true;
            this.MusicName1.BackColor = System.Drawing.Color.Transparent;
            this.MusicName1.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MusicName1.ForeColor = System.Drawing.Color.White;
            this.MusicName1.Location = new System.Drawing.Point(10, 13);
            this.MusicName1.Name = "MusicName1";
            this.MusicName1.Size = new System.Drawing.Size(52, 17);
            this.MusicName1.TabIndex = 0;
            this.MusicName1.Text = "label1";
            this.MusicName1.DoubleClick += new System.EventHandler(this.MusicName1_DoubleClick);
            this.MusicName1.MouseEnter += new System.EventHandler(this.MusicName1_MouseEnter);
            this.MusicName1.MouseLeave += new System.EventHandler(this.MusicName1_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Playbut
            // 
            this.Playbut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Playbut.BackgroundImage")));
            this.Playbut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Playbut.Location = new System.Drawing.Point(26, 9);
            this.Playbut.Name = "Playbut";
            this.Playbut.Size = new System.Drawing.Size(25, 25);
            this.Playbut.TabIndex = 4;
            this.Playbut.TabStop = false;
            this.Playbut.Click += new System.EventHandler(this.Playbut_Click);
            this.Playbut.MouseEnter += new System.EventHandler(this.Playbut_MouseEnter);
            this.Playbut.MouseLeave += new System.EventHandler(this.Playbut_MouseLeave);
            // 
            // PlusGeCiBut
            // 
            this.PlusGeCiBut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PlusGeCiBut.BackgroundImage")));
            this.PlusGeCiBut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlusGeCiBut.Location = new System.Drawing.Point(68, 9);
            this.PlusGeCiBut.Name = "PlusGeCiBut";
            this.PlusGeCiBut.Size = new System.Drawing.Size(25, 25);
            this.PlusGeCiBut.TabIndex = 3;
            this.PlusGeCiBut.TabStop = false;
            this.PlusGeCiBut.Click += new System.EventHandler(this.PlusGeCiBut_Click);
            this.PlusGeCiBut.MouseEnter += new System.EventHandler(this.PlusGeCiBut_MouseEnter);
            this.PlusGeCiBut.MouseLeave += new System.EventHandler(this.PlusGeCiBut_MouseLeave);
            // 
            // LikeBut
            // 
            this.LikeBut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LikeBut.BackgroundImage")));
            this.LikeBut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LikeBut.Location = new System.Drawing.Point(110, 9);
            this.LikeBut.Name = "LikeBut";
            this.LikeBut.Size = new System.Drawing.Size(25, 25);
            this.LikeBut.TabIndex = 2;
            this.LikeBut.TabStop = false;
            this.LikeBut.Click += new System.EventHandler(this.LikeBut_Click);
            this.LikeBut.MouseEnter += new System.EventHandler(this.LikeBut_MouseEnter);
            this.LikeBut.MouseLeave += new System.EventHandler(this.LikeBut_MouseLeave);
            // 
            // DeleteBut
            // 
            this.DeleteBut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteBut.BackgroundImage")));
            this.DeleteBut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeleteBut.Location = new System.Drawing.Point(152, 9);
            this.DeleteBut.Name = "DeleteBut";
            this.DeleteBut.Size = new System.Drawing.Size(25, 25);
            this.DeleteBut.TabIndex = 1;
            this.DeleteBut.TabStop = false;
            this.DeleteBut.Click += new System.EventHandler(this.DeleteBut_Click);
            this.DeleteBut.MouseEnter += new System.EventHandler(this.DeleteBut_MouseEnter);
            this.DeleteBut.MouseLeave += new System.EventHandler(this.DeleteBut_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Playbut);
            this.panel2.Controls.Add(this.PlusGeCiBut);
            this.panel2.Controls.Add(this.DeleteBut);
            this.panel2.Controls.Add(this.LikeBut);
            this.panel2.Location = new System.Drawing.Point(489, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 45);
            this.panel2.TabIndex = 5;
            this.panel2.MouseEnter += new System.EventHandler(this.panel2_MouseEnter);
            this.panel2.MouseLeave += new System.EventHandler(this.panel2_MouseLeave);
            // 
            // MusicInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "MusicInfo";
            this.Size = new System.Drawing.Size(700, 50);
            this.Load += new System.EventHandler(this.MusicInfo_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MusicInfo_Paint);
            this.DoubleClick += new System.EventHandler(this.MusicInfo_DoubleClick);
            this.MouseEnter += new System.EventHandler(this.MusicInfo_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MusicInfo_MouseLeave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Playbut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlusGeCiBut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LikeBut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeleteBut)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label MusicName2;
        public System.Windows.Forms.Label MusicName1;
        public System.Windows.Forms.PictureBox DeleteBut;
        public System.Windows.Forms.PictureBox LikeBut;
        public System.Windows.Forms.PictureBox PlusGeCiBut;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.ToolTip toolTip2;
        public System.Windows.Forms.ToolTip toolTip3;
        public System.Windows.Forms.PictureBox Playbut;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
    }
}
