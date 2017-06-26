namespace 音乐播放器
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.UserNameText = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.UserImage = new System.Windows.Forms.PictureBox();
            this.LoginBut = new System.Windows.Forms.PictureBox();
            this.CloseBut = new System.Windows.Forms.PictureBox();
            this.RegistBut = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginBut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBut)).BeginInit();
            this.SuspendLayout();
            // 
            // UserNameText
            // 
            this.UserNameText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(62)))));
            this.UserNameText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserNameText.Font = new System.Drawing.Font("等线", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserNameText.ForeColor = System.Drawing.Color.White;
            this.UserNameText.Location = new System.Drawing.Point(80, 169);
            this.UserNameText.Name = "UserNameText";
            this.UserNameText.Size = new System.Drawing.Size(225, 33);
            this.UserNameText.TabIndex = 0;
            this.UserNameText.TextChanged += new System.EventHandler(this.UserNameText_TextChanged);
            this.UserNameText.Leave += new System.EventHandler(this.UserNameText_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::音乐播放器.Properties.Resources.用户名__3_;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(47, 169);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 33);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::音乐播放器.Properties.Resources.密码;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(47, 239);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 33);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // PasswordText
            // 
            this.PasswordText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(62)))));
            this.PasswordText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PasswordText.Font = new System.Drawing.Font("等线", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PasswordText.ForeColor = System.Drawing.Color.White;
            this.PasswordText.Location = new System.Drawing.Point(80, 239);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.PasswordChar = '*';
            this.PasswordText.Size = new System.Drawing.Size(225, 33);
            this.PasswordText.TabIndex = 2;
            // 
            // UserImage
            // 
            this.UserImage.BackgroundImage = global::音乐播放器.Properties.Resources._00;
            this.UserImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UserImage.Location = new System.Drawing.Point(135, 40);
            this.UserImage.Name = "UserImage";
            this.UserImage.Size = new System.Drawing.Size(80, 80);
            this.UserImage.TabIndex = 4;
            this.UserImage.TabStop = false;
            // 
            // LoginBut
            // 
            this.LoginBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(204)))), ((int)(((byte)(192)))));
            this.LoginBut.BackgroundImage = global::音乐播放器.Properties.Resources.登录;
            this.LoginBut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LoginBut.Location = new System.Drawing.Point(47, 320);
            this.LoginBut.Name = "LoginBut";
            this.LoginBut.Size = new System.Drawing.Size(258, 33);
            this.LoginBut.TabIndex = 5;
            this.LoginBut.TabStop = false;
            this.LoginBut.Click += new System.EventHandler(this.LoginBut_Click);
            this.LoginBut.MouseEnter += new System.EventHandler(this.LoginBut_MouseEnter);
            this.LoginBut.MouseLeave += new System.EventHandler(this.LoginBut_MouseLeave);
            // 
            // CloseBut
            // 
            this.CloseBut.BackgroundImage = global::音乐播放器.Properties.Resources.关闭__4_;
            this.CloseBut.Location = new System.Drawing.Point(325, 0);
            this.CloseBut.Name = "CloseBut";
            this.CloseBut.Size = new System.Drawing.Size(25, 25);
            this.CloseBut.TabIndex = 6;
            this.CloseBut.TabStop = false;
            this.CloseBut.Click += new System.EventHandler(this.CloseBut_Click);
            this.CloseBut.MouseEnter += new System.EventHandler(this.CloseBut_MouseEnter);
            this.CloseBut.MouseLeave += new System.EventHandler(this.CloseBut_MouseLeave);
            // 
            // RegistBut
            // 
            this.RegistBut.AutoSize = true;
            this.RegistBut.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RegistBut.ForeColor = System.Drawing.Color.Silver;
            this.RegistBut.Location = new System.Drawing.Point(99, 393);
            this.RegistBut.Name = "RegistBut";
            this.RegistBut.Size = new System.Drawing.Size(152, 17);
            this.RegistBut.TabIndex = 7;
            this.RegistBut.Text = "没有账号？立即注册";
            this.RegistBut.Click += new System.EventHandler(this.RegistBut_Click);
            this.RegistBut.MouseEnter += new System.EventHandler(this.RegistBut_MouseEnter);
            this.RegistBut.MouseLeave += new System.EventHandler(this.RegistBut_MouseLeave);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(350, 460);
            this.Controls.Add(this.RegistBut);
            this.Controls.Add(this.CloseBut);
            this.Controls.Add(this.LoginBut);
            this.Controls.Add(this.UserImage);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.UserNameText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginBut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UserNameText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.PictureBox UserImage;
        private System.Windows.Forms.PictureBox LoginBut;
        private System.Windows.Forms.PictureBox CloseBut;
        private System.Windows.Forms.Label RegistBut;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
    }
}