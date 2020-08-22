namespace Blurts
{
  partial class CommandDlg
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandDlg));
      this.sendBtn = new System.Windows.Forms.Button();
      this.menuBtn = new System.Windows.Forms.Button();
      this.clickBtn = new System.Windows.Forms.Button();
      this.escBtn = new System.Windows.Forms.Button();
      this.endBtn = new System.Windows.Forms.Button();
      this.rightBtn = new System.Windows.Forms.Button();
      this.leftBtn = new System.Windows.Forms.Button();
      this.downBtn = new System.Windows.Forms.Button();
      this.upBtn = new System.Windows.Forms.Button();
      this.SettingBtn = new System.Windows.Forms.Button();
      this.displayKey = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.OkBtn = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // sendBtn
      // 
      this.sendBtn.Location = new System.Drawing.Point(20, 151);
      this.sendBtn.Name = "sendBtn";
      this.sendBtn.Size = new System.Drawing.Size(50, 23);
      this.sendBtn.TabIndex = 0;
      this.sendBtn.Text = "Send";
      this.sendBtn.UseVisualStyleBackColor = true;
      this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
      // 
      // menuBtn
      // 
      this.menuBtn.Location = new System.Drawing.Point(76, 151);
      this.menuBtn.Name = "menuBtn";
      this.menuBtn.Size = new System.Drawing.Size(50, 23);
      this.menuBtn.TabIndex = 1;
      this.menuBtn.Text = "Menu";
      this.menuBtn.UseVisualStyleBackColor = true;
      this.menuBtn.Click += new System.EventHandler(this.menuBtn_Click);
      // 
      // clickBtn
      // 
      this.clickBtn.Location = new System.Drawing.Point(188, 151);
      this.clickBtn.Name = "clickBtn";
      this.clickBtn.Size = new System.Drawing.Size(50, 23);
      this.clickBtn.TabIndex = 2;
      this.clickBtn.Text = "Click";
      this.clickBtn.UseVisualStyleBackColor = true;
      this.clickBtn.Click += new System.EventHandler(this.clickBtn_Click);
      // 
      // escBtn
      // 
      this.escBtn.Location = new System.Drawing.Point(300, 151);
      this.escBtn.Name = "escBtn";
      this.escBtn.Size = new System.Drawing.Size(50, 23);
      this.escBtn.TabIndex = 3;
      this.escBtn.Text = "Esc";
      this.escBtn.UseVisualStyleBackColor = true;
      this.escBtn.Click += new System.EventHandler(this.escBtn_Click);
      // 
      // endBtn
      // 
      this.endBtn.Location = new System.Drawing.Point(356, 151);
      this.endBtn.Name = "endBtn";
      this.endBtn.Size = new System.Drawing.Size(50, 23);
      this.endBtn.TabIndex = 4;
      this.endBtn.Text = "End";
      this.endBtn.UseVisualStyleBackColor = true;
      this.endBtn.Click += new System.EventHandler(this.endBtn_Click);
      // 
      // rightBtn
      // 
      this.rightBtn.Location = new System.Drawing.Point(244, 151);
      this.rightBtn.Name = "rightBtn";
      this.rightBtn.Size = new System.Drawing.Size(50, 23);
      this.rightBtn.TabIndex = 5;
      this.rightBtn.Text = "Right";
      this.rightBtn.UseVisualStyleBackColor = true;
      this.rightBtn.Click += new System.EventHandler(this.rightBtn_Click);
      // 
      // leftBtn
      // 
      this.leftBtn.Location = new System.Drawing.Point(132, 151);
      this.leftBtn.Name = "leftBtn";
      this.leftBtn.Size = new System.Drawing.Size(50, 23);
      this.leftBtn.TabIndex = 6;
      this.leftBtn.Text = "Left";
      this.leftBtn.UseVisualStyleBackColor = true;
      this.leftBtn.Click += new System.EventHandler(this.leftBtn_Click);
      // 
      // downBtn
      // 
      this.downBtn.Location = new System.Drawing.Point(188, 179);
      this.downBtn.Name = "downBtn";
      this.downBtn.Size = new System.Drawing.Size(50, 23);
      this.downBtn.TabIndex = 7;
      this.downBtn.Text = "Down";
      this.downBtn.UseVisualStyleBackColor = true;
      this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
      // 
      // upBtn
      // 
      this.upBtn.Location = new System.Drawing.Point(188, 124);
      this.upBtn.Name = "upBtn";
      this.upBtn.Size = new System.Drawing.Size(50, 23);
      this.upBtn.TabIndex = 8;
      this.upBtn.Text = "Up";
      this.upBtn.UseVisualStyleBackColor = true;
      this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
      // 
      // SettingBtn
      // 
      this.SettingBtn.Location = new System.Drawing.Point(403, 191);
      this.SettingBtn.Name = "SettingBtn";
      this.SettingBtn.Size = new System.Drawing.Size(51, 23);
      this.SettingBtn.TabIndex = 10;
      this.SettingBtn.Text = "Settings";
      this.SettingBtn.UseVisualStyleBackColor = true;
      this.SettingBtn.Visible = false;
      this.SettingBtn.Click += new System.EventHandler(this.settingsBtn_Click);
      // 
      // displayKey
      // 
      this.displayKey.AutoSize = true;
      this.displayKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.displayKey.Location = new System.Drawing.Point(74, 71);
      this.displayKey.Name = "displayKey";
      this.displayKey.Size = new System.Drawing.Size(303, 46);
      this.displayKey.TabIndex = 12;
      this.displayKey.Text = "Type something";
      // 
      // OkBtn
      // 
      this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkBtn.Location = new System.Drawing.Point(368, 9);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(75, 23);
      this.OkBtn.TabIndex = 14;
      this.OkBtn.Text = "Done";
      this.OkBtn.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(13, 9);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(337, 46);
      this.textBox1.TabIndex = 15;
      this.textBox1.Text = "BETA: This dialog is not complete, but is a demo of what is coming. What you type" +
          " will be sent to your BlackBerry (Not all keys work yet). Use arrow keys to simu" +
          "late trackball, or use buttons below. ";
      // 
      // CommandDlg
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(455, 210);
      this.ControlBox = false;
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.OkBtn);
      this.Controls.Add(this.displayKey);
      this.Controls.Add(this.SettingBtn);
      this.Controls.Add(this.upBtn);
      this.Controls.Add(this.downBtn);
      this.Controls.Add(this.leftBtn);
      this.Controls.Add(this.rightBtn);
      this.Controls.Add(this.endBtn);
      this.Controls.Add(this.escBtn);
      this.Controls.Add(this.clickBtn);
      this.Controls.Add(this.menuBtn);
      this.Controls.Add(this.sendBtn);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "CommandDlg";
      this.Text = "BlackBerry Control";
      this.Deactivate += new System.EventHandler(this.CommandDlg_Deactivate);
      this.Load += new System.EventHandler(this.CommandDlg_Load);
      this.Activated += new System.EventHandler(this.CommandDlg_Activated);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommandDlg_KeyPress);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommandDlg_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button sendBtn;
    private System.Windows.Forms.Button menuBtn;
    private System.Windows.Forms.Button clickBtn;
    private System.Windows.Forms.Button escBtn;
    private System.Windows.Forms.Button endBtn;
    private System.Windows.Forms.Button rightBtn;
    private System.Windows.Forms.Button leftBtn;
    private System.Windows.Forms.Button downBtn;
    private System.Windows.Forms.Button upBtn;
    private System.Windows.Forms.Button SettingBtn;
    private System.Windows.Forms.Label displayKey;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.TextBox textBox1;
  }
}