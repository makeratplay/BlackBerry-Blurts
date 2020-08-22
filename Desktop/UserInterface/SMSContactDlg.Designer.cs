namespace Blurts
{
  partial class SMSContactDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMSContactDlg));
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.searchTxtBx = new System.Windows.Forms.TextBox();
      this.closeBtn = new System.Windows.Forms.Button();
      this.Ok = new System.Windows.Forms.Button();
      this.maximizeBtn = new System.Windows.Forms.Button();
      this.minimizeBtn = new System.Windows.Forms.Button();
      this.HistoryBtn = new System.Windows.Forms.Button();
      this.AllBtn = new System.Windows.Forms.Button();
      this.keypadBtn = new System.Windows.Forms.Button();
      this.keyPound = new System.Windows.Forms.Button();
      this.key9 = new System.Windows.Forms.Button();
      this.key6 = new System.Windows.Forms.Button();
      this.key3 = new System.Windows.Forms.Button();
      this.key0 = new System.Windows.Forms.Button();
      this.key8 = new System.Windows.Forms.Button();
      this.key5 = new System.Windows.Forms.Button();
      this.key2 = new System.Windows.Forms.Button();
      this.keyStar = new System.Windows.Forms.Button();
      this.key7 = new System.Windows.Forms.Button();
      this.key4 = new System.Windows.Forms.Button();
      this.key1 = new System.Windows.Forms.Button();
      this.favoriteBtn = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // webBrowser1
      // 
      this.webBrowser1.Location = new System.Drawing.Point(10, 83);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.ScrollBarsEnabled = false;
      this.webBrowser1.Size = new System.Drawing.Size(298, 252);
      this.webBrowser1.TabIndex = 1;
      // 
      // searchTxtBx
      // 
      this.searchTxtBx.BackColor = System.Drawing.Color.White;
      this.searchTxtBx.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.searchTxtBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.searchTxtBx.Location = new System.Drawing.Point(10, 39);
      this.searchTxtBx.Name = "searchTxtBx";
      this.searchTxtBx.Size = new System.Drawing.Size(265, 37);
      this.searchTxtBx.TabIndex = 0;
      this.searchTxtBx.TextChanged += new System.EventHandler(this.searchTxtBx_TextChanged);
      // 
      // closeBtn
      // 
      this.closeBtn.BackColor = System.Drawing.Color.Transparent;
      this.closeBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.closeBtn.FlatAppearance.BorderSize = 0;
      this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn;
      this.closeBtn.Location = new System.Drawing.Point(293, 6);
      this.closeBtn.Margin = new System.Windows.Forms.Padding(0);
      this.closeBtn.Name = "closeBtn";
      this.closeBtn.Size = new System.Drawing.Size(18, 20);
      this.closeBtn.TabIndex = 4;
      this.closeBtn.UseVisualStyleBackColor = false;
      this.closeBtn.MouseLeave += new System.EventHandler(this.closeBtn_MouseLeave);
      this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
      this.closeBtn.MouseEnter += new System.EventHandler(this.closeBtn_MouseEnter);
      // 
      // Ok
      // 
      this.Ok.BackColor = System.Drawing.Color.Black;
      this.Ok.FlatAppearance.BorderSize = 0;
      this.Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.Ok.Image = global::Blurts.Properties.Resources.goBtn;
      this.Ok.Location = new System.Drawing.Point(274, 39);
      this.Ok.Name = "Ok";
      this.Ok.Size = new System.Drawing.Size(37, 37);
      this.Ok.TabIndex = 5;
      this.Ok.UseVisualStyleBackColor = false;
      this.Ok.Click += new System.EventHandler(this.Ok_Click);
      // 
      // maximizeBtn
      // 
      this.maximizeBtn.BackColor = System.Drawing.Color.Black;
      this.maximizeBtn.FlatAppearance.BorderSize = 0;
      this.maximizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.maximizeBtn.Image = global::Blurts.Properties.Resources.maximize_Btn;
      this.maximizeBtn.Location = new System.Drawing.Point(272, 6);
      this.maximizeBtn.Margin = new System.Windows.Forms.Padding(0);
      this.maximizeBtn.Name = "maximizeBtn";
      this.maximizeBtn.Size = new System.Drawing.Size(18, 20);
      this.maximizeBtn.TabIndex = 6;
      this.maximizeBtn.UseVisualStyleBackColor = false;
      this.maximizeBtn.Visible = false;
      this.maximizeBtn.MouseLeave += new System.EventHandler(this.maximizeBtn_MouseLeave);
      this.maximizeBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.maximizeBtn_MouseClick);
      this.maximizeBtn.MouseEnter += new System.EventHandler(this.maximizeBtn_MouseEnter);
      // 
      // minimizeBtn
      // 
      this.minimizeBtn.BackColor = System.Drawing.Color.Transparent;
      this.minimizeBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.minimizeBtn.FlatAppearance.BorderSize = 0;
      this.minimizeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.minimizeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.minimizeBtn.Image = global::Blurts.Properties.Resources.minimizeBtn;
      this.minimizeBtn.Location = new System.Drawing.Point(272, 6);
      this.minimizeBtn.Margin = new System.Windows.Forms.Padding(0);
      this.minimizeBtn.Name = "minimizeBtn";
      this.minimizeBtn.Size = new System.Drawing.Size(18, 20);
      this.minimizeBtn.TabIndex = 7;
      this.minimizeBtn.UseVisualStyleBackColor = false;
      this.minimizeBtn.MouseLeave += new System.EventHandler(this.minimizeBtn_MouseLeave);
      this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
      this.minimizeBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.minimizeBtn_MouseClick);
      this.minimizeBtn.MouseEnter += new System.EventHandler(this.minimizeBtn_MouseEnter);
      // 
      // HistoryBtn
      // 
      this.HistoryBtn.BackColor = System.Drawing.Color.Black;
      this.HistoryBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.HistoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.HistoryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.HistoryBtn.ForeColor = System.Drawing.Color.White;
      this.HistoryBtn.Location = new System.Drawing.Point(11, 334);
      this.HistoryBtn.Name = "HistoryBtn";
      this.HistoryBtn.Size = new System.Drawing.Size(75, 23);
      this.HistoryBtn.TabIndex = 8;
      this.HistoryBtn.Text = "History";
      this.HistoryBtn.UseVisualStyleBackColor = false;
      this.HistoryBtn.Click += new System.EventHandler(this.HistoryBtn_Click);
      // 
      // AllBtn
      // 
      this.AllBtn.BackColor = System.Drawing.Color.Black;
      this.AllBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.AllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.AllBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.AllBtn.ForeColor = System.Drawing.Color.White;
      this.AllBtn.Location = new System.Drawing.Point(159, 334);
      this.AllBtn.Name = "AllBtn";
      this.AllBtn.Size = new System.Drawing.Size(75, 23);
      this.AllBtn.TabIndex = 9;
      this.AllBtn.Text = "All";
      this.AllBtn.UseVisualStyleBackColor = false;
      this.AllBtn.Click += new System.EventHandler(this.AllBtn_Click);
      // 
      // keypadBtn
      // 
      this.keypadBtn.BackColor = System.Drawing.Color.Black;
      this.keypadBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.keypadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.keypadBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.keypadBtn.ForeColor = System.Drawing.Color.White;
      this.keypadBtn.Location = new System.Drawing.Point(226, 334);
      this.keypadBtn.Name = "keypadBtn";
      this.keypadBtn.Size = new System.Drawing.Size(75, 23);
      this.keypadBtn.TabIndex = 10;
      this.keypadBtn.Text = "Keypad";
      this.keypadBtn.UseVisualStyleBackColor = false;
      this.keypadBtn.Click += new System.EventHandler(this.keypadBtn_Click);
      // 
      // keyPound
      // 
      this.keyPound.BackColor = System.Drawing.Color.Black;
      this.keyPound.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.keyPound.FlatAppearance.BorderSize = 0;
      this.keyPound.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.keyPound.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.keyPound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.keyPound.Image = global::Blurts.Properties.Resources.poundkey;
      this.keyPound.Location = new System.Drawing.Point(208, 272);
      this.keyPound.Margin = new System.Windows.Forms.Padding(0);
      this.keyPound.Name = "keyPound";
      this.keyPound.Size = new System.Drawing.Size(100, 63);
      this.keyPound.TabIndex = 29;
      this.keyPound.UseVisualStyleBackColor = false;
      this.keyPound.Click += new System.EventHandler(this.keyPound_Click);
      this.keyPound.MouseDown += new System.Windows.Forms.MouseEventHandler(this.keyPound_MouseDown);
      this.keyPound.MouseUp += new System.Windows.Forms.MouseEventHandler(this.keyPound_MouseUp);
      // 
      // key9
      // 
      this.key9.BackColor = System.Drawing.Color.Black;
      this.key9.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key9.FlatAppearance.BorderSize = 0;
      this.key9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key9.Image = global::Blurts.Properties.Resources._9key;
      this.key9.Location = new System.Drawing.Point(208, 209);
      this.key9.Margin = new System.Windows.Forms.Padding(0);
      this.key9.Name = "key9";
      this.key9.Size = new System.Drawing.Size(100, 63);
      this.key9.TabIndex = 28;
      this.key9.UseVisualStyleBackColor = false;
      this.key9.Click += new System.EventHandler(this.key9_Click);
      this.key9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key9_MouseDown);
      this.key9.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key9_MouseUp);
      // 
      // key6
      // 
      this.key6.BackColor = System.Drawing.Color.Black;
      this.key6.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key6.FlatAppearance.BorderSize = 0;
      this.key6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key6.Image = global::Blurts.Properties.Resources._6key;
      this.key6.Location = new System.Drawing.Point(208, 146);
      this.key6.Margin = new System.Windows.Forms.Padding(0);
      this.key6.Name = "key6";
      this.key6.Size = new System.Drawing.Size(100, 63);
      this.key6.TabIndex = 27;
      this.key6.UseVisualStyleBackColor = false;
      this.key6.Click += new System.EventHandler(this.key6_Click);
      this.key6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key6_MouseDown);
      this.key6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key6_MouseUp);
      // 
      // key3
      // 
      this.key3.BackColor = System.Drawing.Color.Black;
      this.key3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key3.FlatAppearance.BorderSize = 0;
      this.key3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key3.Image = global::Blurts.Properties.Resources._3key;
      this.key3.Location = new System.Drawing.Point(208, 83);
      this.key3.Margin = new System.Windows.Forms.Padding(0);
      this.key3.Name = "key3";
      this.key3.Size = new System.Drawing.Size(100, 63);
      this.key3.TabIndex = 26;
      this.key3.UseVisualStyleBackColor = false;
      this.key3.Click += new System.EventHandler(this.key3_Click);
      this.key3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key3_MouseDown);
      this.key3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key3_MouseUp);
      // 
      // key0
      // 
      this.key0.BackColor = System.Drawing.Color.Black;
      this.key0.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key0.FlatAppearance.BorderSize = 0;
      this.key0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key0.Image = global::Blurts.Properties.Resources._0key;
      this.key0.Location = new System.Drawing.Point(108, 272);
      this.key0.Margin = new System.Windows.Forms.Padding(0);
      this.key0.Name = "key0";
      this.key0.Size = new System.Drawing.Size(100, 63);
      this.key0.TabIndex = 25;
      this.key0.UseVisualStyleBackColor = false;
      this.key0.Click += new System.EventHandler(this.key0_Click);
      this.key0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key0_MouseDown);
      this.key0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key0_MouseUp);
      // 
      // key8
      // 
      this.key8.BackColor = System.Drawing.Color.Black;
      this.key8.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key8.FlatAppearance.BorderSize = 0;
      this.key8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key8.Image = global::Blurts.Properties.Resources._8key;
      this.key8.Location = new System.Drawing.Point(108, 209);
      this.key8.Margin = new System.Windows.Forms.Padding(0);
      this.key8.Name = "key8";
      this.key8.Size = new System.Drawing.Size(100, 63);
      this.key8.TabIndex = 24;
      this.key8.UseVisualStyleBackColor = false;
      this.key8.Click += new System.EventHandler(this.key8_Click);
      this.key8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key8_MouseDown);
      this.key8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key8_MouseUp);
      // 
      // key5
      // 
      this.key5.BackColor = System.Drawing.Color.Black;
      this.key5.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key5.FlatAppearance.BorderSize = 0;
      this.key5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key5.Image = global::Blurts.Properties.Resources._5key;
      this.key5.Location = new System.Drawing.Point(108, 146);
      this.key5.Margin = new System.Windows.Forms.Padding(0);
      this.key5.Name = "key5";
      this.key5.Size = new System.Drawing.Size(100, 63);
      this.key5.TabIndex = 23;
      this.key5.UseVisualStyleBackColor = false;
      this.key5.Click += new System.EventHandler(this.key5_Click);
      this.key5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key5_MouseDown);
      this.key5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key5_MouseUp);
      // 
      // key2
      // 
      this.key2.BackColor = System.Drawing.Color.Black;
      this.key2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key2.FlatAppearance.BorderSize = 0;
      this.key2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key2.Image = global::Blurts.Properties.Resources._2key;
      this.key2.Location = new System.Drawing.Point(108, 83);
      this.key2.Margin = new System.Windows.Forms.Padding(0);
      this.key2.Name = "key2";
      this.key2.Size = new System.Drawing.Size(100, 63);
      this.key2.TabIndex = 22;
      this.key2.UseVisualStyleBackColor = false;
      this.key2.Click += new System.EventHandler(this.key2_Click);
      this.key2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key2_MouseDown);
      this.key2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key2_MouseUp);
      // 
      // keyStar
      // 
      this.keyStar.BackColor = System.Drawing.Color.Black;
      this.keyStar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.keyStar.FlatAppearance.BorderSize = 0;
      this.keyStar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.keyStar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.keyStar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.keyStar.Image = global::Blurts.Properties.Resources.Starkey;
      this.keyStar.Location = new System.Drawing.Point(10, 272);
      this.keyStar.Margin = new System.Windows.Forms.Padding(0);
      this.keyStar.Name = "keyStar";
      this.keyStar.Size = new System.Drawing.Size(98, 63);
      this.keyStar.TabIndex = 21;
      this.keyStar.UseVisualStyleBackColor = false;
      this.keyStar.Click += new System.EventHandler(this.keyStar_Click);
      this.keyStar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.keyStar_MouseDown);
      this.keyStar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.keyStar_MouseUp);
      // 
      // key7
      // 
      this.key7.BackColor = System.Drawing.Color.Black;
      this.key7.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key7.FlatAppearance.BorderSize = 0;
      this.key7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key7.Image = global::Blurts.Properties.Resources._7key;
      this.key7.Location = new System.Drawing.Point(10, 209);
      this.key7.Margin = new System.Windows.Forms.Padding(0);
      this.key7.Name = "key7";
      this.key7.Size = new System.Drawing.Size(98, 63);
      this.key7.TabIndex = 20;
      this.key7.UseVisualStyleBackColor = false;
      this.key7.Click += new System.EventHandler(this.key7_Click);
      this.key7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key7_MouseDown);
      this.key7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key7_MouseUp);
      // 
      // key4
      // 
      this.key4.BackColor = System.Drawing.Color.Black;
      this.key4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key4.FlatAppearance.BorderSize = 0;
      this.key4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key4.Image = global::Blurts.Properties.Resources._4key;
      this.key4.Location = new System.Drawing.Point(10, 146);
      this.key4.Margin = new System.Windows.Forms.Padding(0);
      this.key4.Name = "key4";
      this.key4.Size = new System.Drawing.Size(98, 63);
      this.key4.TabIndex = 19;
      this.key4.UseVisualStyleBackColor = false;
      this.key4.Click += new System.EventHandler(this.key4_Click);
      this.key4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key4_MouseDown);
      this.key4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key4_MouseUp);
      // 
      // key1
      // 
      this.key1.BackColor = System.Drawing.Color.Black;
      this.key1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.key1.FlatAppearance.BorderSize = 0;
      this.key1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
      this.key1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
      this.key1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.key1.Image = global::Blurts.Properties.Resources._1key;
      this.key1.Location = new System.Drawing.Point(10, 83);
      this.key1.Margin = new System.Windows.Forms.Padding(0);
      this.key1.Name = "key1";
      this.key1.Size = new System.Drawing.Size(98, 63);
      this.key1.TabIndex = 18;
      this.key1.UseVisualStyleBackColor = false;
      this.key1.Click += new System.EventHandler(this.key1_Click);
      this.key1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key1_MouseDown);
      this.key1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key1_MouseUp);
      // 
      // favoriteBtn
      // 
      this.favoriteBtn.BackColor = System.Drawing.Color.Black;
      this.favoriteBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.favoriteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.favoriteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.favoriteBtn.ForeColor = System.Drawing.Color.White;
      this.favoriteBtn.Location = new System.Drawing.Point(85, 334);
      this.favoriteBtn.Name = "favoriteBtn";
      this.favoriteBtn.Size = new System.Drawing.Size(75, 23);
      this.favoriteBtn.TabIndex = 30;
      this.favoriteBtn.Text = "Favorite";
      this.favoriteBtn.UseVisualStyleBackColor = false;
      this.favoriteBtn.Click += new System.EventHandler(this.favoriteBtn_Click);
      // 
      // SMSContactDlg
      // 
      this.AcceptButton = this.Ok;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Fuchsia;
      this.BackgroundImage = global::Blurts.Properties.Resources.smsBackground;
      this.ClientSize = new System.Drawing.Size(320, 372);
      this.ControlBox = false;
      this.Controls.Add(this.keyPound);
      this.Controls.Add(this.key9);
      this.Controls.Add(this.key6);
      this.Controls.Add(this.key3);
      this.Controls.Add(this.key0);
      this.Controls.Add(this.key8);
      this.Controls.Add(this.key5);
      this.Controls.Add(this.key2);
      this.Controls.Add(this.keyStar);
      this.Controls.Add(this.key7);
      this.Controls.Add(this.key4);
      this.Controls.Add(this.key1);
      this.Controls.Add(this.minimizeBtn);
      this.Controls.Add(this.maximizeBtn);
      this.Controls.Add(this.Ok);
      this.Controls.Add(this.closeBtn);
      this.Controls.Add(this.searchTxtBx);
      this.Controls.Add(this.webBrowser1);
      this.Controls.Add(this.HistoryBtn);
      this.Controls.Add(this.AllBtn);
      this.Controls.Add(this.keypadBtn);
      this.Controls.Add(this.favoriteBtn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(320, 372);
      this.Name = "SMSContactDlg";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Blurts";
      this.TransparencyKey = System.Drawing.Color.Fuchsia;
      this.Load += new System.EventHandler(this.SMSContactDlg_Load);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SMSContactDlg_MouseUp);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SMSContactDlg_MouseDown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SMSContactDlg_FormClosing);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SMSContactDlg_MouseMove);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.TextBox searchTxtBx;
    private System.Windows.Forms.Button closeBtn;
    private System.Windows.Forms.Button Ok;
    private System.Windows.Forms.Button maximizeBtn;
    private System.Windows.Forms.Button minimizeBtn;
    private System.Windows.Forms.Button HistoryBtn;
    private System.Windows.Forms.Button AllBtn;
    private System.Windows.Forms.Button keypadBtn;
    private System.Windows.Forms.Button keyPound;
    private System.Windows.Forms.Button key9;
    private System.Windows.Forms.Button key6;
    private System.Windows.Forms.Button key3;
    private System.Windows.Forms.Button key0;
    private System.Windows.Forms.Button key8;
    private System.Windows.Forms.Button key5;
    private System.Windows.Forms.Button key2;
    private System.Windows.Forms.Button keyStar;
    private System.Windows.Forms.Button key7;
    private System.Windows.Forms.Button key4;
    private System.Windows.Forms.Button key1;
    private System.Windows.Forms.Button favoriteBtn;
  }
}