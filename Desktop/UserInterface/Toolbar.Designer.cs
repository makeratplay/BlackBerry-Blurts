namespace Blurts
{
  partial class Toolbar
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbar));
      this.buzzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.phoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.placeCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.endCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.speakerphoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.volumeUpMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.volumeDownMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.sendSMSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.screenShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.keyboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sendToBlackBerryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sendToPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.runMacroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeBtn = new System.Windows.Forms.Button();
      this.phoneBtn = new System.Windows.Forms.Button();
      this.screenCapture = new System.Windows.Forms.Button();
      this.smsBtn = new System.Windows.Forms.Button();
      this.clipboardBtn = new System.Windows.Forms.Button();
      this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.keepOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clipboardGetBtn = new System.Windows.Forms.Button();
      this.contextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // buzzToolStripMenuItem
      // 
      this.buzzToolStripMenuItem.Name = "buzzToolStripMenuItem";
      this.buzzToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.buzzToolStripMenuItem.Text = "Buzz";
      this.buzzToolStripMenuItem.Click += new System.EventHandler(this.buzzToolStripMenuItem_Click);
      // 
      // phoneToolStripMenuItem
      // 
      this.phoneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.placeCallToolStripMenuItem,
            this.endCallToolStripMenuItem,
            this.speakerphoneToolStripMenuItem,
            this.volumeUpMenu,
            this.volumeDownMenu});
      this.phoneToolStripMenuItem.Name = "phoneToolStripMenuItem";
      this.phoneToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.phoneToolStripMenuItem.Text = "Phone";
      // 
      // placeCallToolStripMenuItem
      // 
      this.placeCallToolStripMenuItem.Name = "placeCallToolStripMenuItem";
      this.placeCallToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
      this.placeCallToolStripMenuItem.Text = "Place Call";
      this.placeCallToolStripMenuItem.Click += new System.EventHandler(this.placeCallToolStripMenuItem_Click);
      // 
      // endCallToolStripMenuItem
      // 
      this.endCallToolStripMenuItem.Name = "endCallToolStripMenuItem";
      this.endCallToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
      this.endCallToolStripMenuItem.Text = "End Call";
      this.endCallToolStripMenuItem.Click += new System.EventHandler(this.endCallToolStripMenuItem_Click);
      // 
      // speakerphoneToolStripMenuItem
      // 
      this.speakerphoneToolStripMenuItem.Name = "speakerphoneToolStripMenuItem";
      this.speakerphoneToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
      this.speakerphoneToolStripMenuItem.Text = "Speakerphone";
      this.speakerphoneToolStripMenuItem.Click += new System.EventHandler(this.speakerphoneToolStripMenuItem_Click);
      // 
      // volumeUpMenu
      // 
      this.volumeUpMenu.Name = "volumeUpMenu";
      this.volumeUpMenu.Size = new System.Drawing.Size(149, 22);
      this.volumeUpMenu.Text = "Volume +";
      this.volumeUpMenu.Click += new System.EventHandler(this.volumeToolStripMenuItem_Click);
      // 
      // volumeDownMenu
      // 
      this.volumeDownMenu.Name = "volumeDownMenu";
      this.volumeDownMenu.Size = new System.Drawing.Size(149, 22);
      this.volumeDownMenu.Text = "Volume -";
      this.volumeDownMenu.Click += new System.EventHandler(this.volumeToolStripMenuItem1_Click);
      // 
      // sendSMSToolStripMenuItem
      // 
      this.sendSMSToolStripMenuItem.Name = "sendSMSToolStripMenuItem";
      this.sendSMSToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.sendSMSToolStripMenuItem.Text = "Send SMS";
      this.sendSMSToolStripMenuItem.Click += new System.EventHandler(this.sendSMSToolStripMenuItem_Click);
      // 
      // screenShotToolStripMenuItem
      // 
      this.screenShotToolStripMenuItem.Name = "screenShotToolStripMenuItem";
      this.screenShotToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.screenShotToolStripMenuItem.Text = "Screen Capture";
      this.screenShotToolStripMenuItem.Click += new System.EventHandler(this.screenShotToolStripMenuItem_Click);
      // 
      // keyboardToolStripMenuItem
      // 
      this.keyboardToolStripMenuItem.Name = "keyboardToolStripMenuItem";
      this.keyboardToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.keyboardToolStripMenuItem.Text = "Keyboard";
      this.keyboardToolStripMenuItem.Visible = false;
      this.keyboardToolStripMenuItem.Click += new System.EventHandler(this.keyboardToolStripMenuItem_Click);
      // 
      // clipboardToolStripMenuItem
      // 
      this.clipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendToBlackBerryToolStripMenuItem,
            this.sendToPCToolStripMenuItem});
      this.clipboardToolStripMenuItem.Name = "clipboardToolStripMenuItem";
      this.clipboardToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.clipboardToolStripMenuItem.Text = "Clipboard";
      // 
      // sendToBlackBerryToolStripMenuItem
      // 
      this.sendToBlackBerryToolStripMenuItem.Name = "sendToBlackBerryToolStripMenuItem";
      this.sendToBlackBerryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
      this.sendToBlackBerryToolStripMenuItem.Text = "Read BlackBerry";
      this.sendToBlackBerryToolStripMenuItem.Click += new System.EventHandler(this.sendToBlackBerryToolStripMenuItem_Click);
      // 
      // sendToPCToolStripMenuItem
      // 
      this.sendToPCToolStripMenuItem.Name = "sendToPCToolStripMenuItem";
      this.sendToPCToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
      this.sendToPCToolStripMenuItem.Text = "Send Windows";
      this.sendToPCToolStripMenuItem.Click += new System.EventHandler(this.sendToPCToolStripMenuItem_Click);
      // 
      // runMacroToolStripMenuItem
      // 
      this.runMacroToolStripMenuItem.Name = "runMacroToolStripMenuItem";
      this.runMacroToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.runMacroToolStripMenuItem.Text = "Run Macro";
      this.runMacroToolStripMenuItem.Click += new System.EventHandler(this.runMacroToolStripMenuItem_Click);
      // 
      // closeBtn
      // 
      this.closeBtn.BackColor = System.Drawing.Color.Black;
      this.closeBtn.FlatAppearance.BorderSize = 0;
      this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn;
      this.closeBtn.Location = new System.Drawing.Point(63, 5);
      this.closeBtn.Margin = new System.Windows.Forms.Padding(0);
      this.closeBtn.Name = "closeBtn";
      this.closeBtn.Size = new System.Drawing.Size(14, 14);
      this.closeBtn.TabIndex = 0;
      this.closeBtn.UseVisualStyleBackColor = false;
      this.closeBtn.Visible = false;
      this.closeBtn.MouseLeave += new System.EventHandler(this.closeBtn_MouseLeave);
      this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
      this.closeBtn.MouseEnter += new System.EventHandler(this.closeBtn_MouseEnter);
      // 
      // phoneBtn
      // 
      this.phoneBtn.BackColor = System.Drawing.Color.Transparent;
      this.phoneBtn.FlatAppearance.BorderSize = 0;
      this.phoneBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.phoneBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.phoneBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.phoneBtn.Image = global::Blurts.Properties.Resources.phoneBtn;
      this.phoneBtn.Location = new System.Drawing.Point(4, 55);
      this.phoneBtn.Name = "phoneBtn";
      this.phoneBtn.Size = new System.Drawing.Size(75, 60);
      this.phoneBtn.TabIndex = 2;
      this.phoneBtn.UseVisualStyleBackColor = false;
      this.phoneBtn.Click += new System.EventHandler(this.phoneBtn_Click);
      // 
      // screenCapture
      // 
      this.screenCapture.BackColor = System.Drawing.Color.Transparent;
      this.screenCapture.FlatAppearance.BorderSize = 0;
      this.screenCapture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.screenCapture.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.screenCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.screenCapture.Image = global::Blurts.Properties.Resources.screenCaptureBtn;
      this.screenCapture.Location = new System.Drawing.Point(4, 187);
      this.screenCapture.Name = "screenCapture";
      this.screenCapture.Size = new System.Drawing.Size(75, 60);
      this.screenCapture.TabIndex = 3;
      this.screenCapture.UseVisualStyleBackColor = false;
      this.screenCapture.Click += new System.EventHandler(this.screenCapture_Click);
      // 
      // smsBtn
      // 
      this.smsBtn.BackColor = System.Drawing.Color.Transparent;
      this.smsBtn.FlatAppearance.BorderSize = 0;
      this.smsBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.smsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.smsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.smsBtn.Image = global::Blurts.Properties.Resources.smsBtn;
      this.smsBtn.Location = new System.Drawing.Point(4, 121);
      this.smsBtn.Name = "smsBtn";
      this.smsBtn.Size = new System.Drawing.Size(75, 60);
      this.smsBtn.TabIndex = 4;
      this.smsBtn.UseVisualStyleBackColor = false;
      this.smsBtn.Click += new System.EventHandler(this.smsBtn_Click);
      // 
      // clipboardBtn
      // 
      this.clipboardBtn.BackColor = System.Drawing.Color.Transparent;
      this.clipboardBtn.FlatAppearance.BorderSize = 0;
      this.clipboardBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.clipboardBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.clipboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.clipboardBtn.Image = global::Blurts.Properties.Resources.clipboardSendBtn;
      this.clipboardBtn.Location = new System.Drawing.Point(4, 253);
      this.clipboardBtn.Name = "clipboardBtn";
      this.clipboardBtn.Size = new System.Drawing.Size(75, 60);
      this.clipboardBtn.TabIndex = 5;
      this.clipboardBtn.UseVisualStyleBackColor = false;
      this.clipboardBtn.Click += new System.EventHandler(this.clipboardBtn_Click);
      // 
      // contextMenu
      // 
      this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phoneToolStripMenuItem,
            this.sendSMSToolStripMenuItem,
            this.keyboardToolStripMenuItem,
            this.screenShotToolStripMenuItem,
            this.clipboardToolStripMenuItem,
            this.runMacroToolStripMenuItem,
            this.buzzToolStripMenuItem,
            this.keepOnTopToolStripMenuItem,
            this.minimizeToolStripMenuItem,
            this.hideToolStripMenuItem});
      this.contextMenu.Name = "contextMenu";
      this.contextMenu.Size = new System.Drawing.Size(155, 224);
      this.contextMenu.Text = "BlackBerry";
      this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
      // 
      // keepOnTopToolStripMenuItem
      // 
      this.keepOnTopToolStripMenuItem.Name = "keepOnTopToolStripMenuItem";
      this.keepOnTopToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.keepOnTopToolStripMenuItem.Text = "Keep on Top";
      this.keepOnTopToolStripMenuItem.Click += new System.EventHandler(this.keepOnTopToolStripMenuItem_Click);
      // 
      // minimizeToolStripMenuItem
      // 
      this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
      this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.minimizeToolStripMenuItem.Text = "Minimize";
      this.minimizeToolStripMenuItem.Click += new System.EventHandler(this.minimizeToolStripMenuItem_Click);
      // 
      // hideToolStripMenuItem
      // 
      this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
      this.hideToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.hideToolStripMenuItem.Text = "Hide";
      this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
      // 
      // clipboardGetBtn
      // 
      this.clipboardGetBtn.BackColor = System.Drawing.Color.Transparent;
      this.clipboardGetBtn.FlatAppearance.BorderSize = 0;
      this.clipboardGetBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
      this.clipboardGetBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
      this.clipboardGetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.clipboardGetBtn.Image = global::Blurts.Properties.Resources.clipboardGetBtn;
      this.clipboardGetBtn.Location = new System.Drawing.Point(4, 322);
      this.clipboardGetBtn.Name = "clipboardGetBtn";
      this.clipboardGetBtn.Size = new System.Drawing.Size(75, 60);
      this.clipboardGetBtn.TabIndex = 8;
      this.clipboardGetBtn.UseVisualStyleBackColor = false;
      this.clipboardGetBtn.Click += new System.EventHandler(this.clipboardGetBtn_Click);
      // 
      // Toolbar
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Fuchsia;
      this.BackgroundImage = global::Blurts.Properties.Resources.toolbarBackground;
      this.ClientSize = new System.Drawing.Size(82, 432);
      this.ContextMenuStrip = this.contextMenu;
      this.ControlBox = false;
      this.Controls.Add(this.clipboardGetBtn);
      this.Controls.Add(this.clipboardBtn);
      this.Controls.Add(this.smsBtn);
      this.Controls.Add(this.screenCapture);
      this.Controls.Add(this.phoneBtn);
      this.Controls.Add(this.closeBtn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Toolbar";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Blurts";
      this.TransparencyKey = System.Drawing.Color.Fuchsia;
      this.Load += new System.EventHandler(this.Toolbar_Load);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseUp);
      this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDoubleClick);
      this.SizeChanged += new System.EventHandler(this.Toolbar_SizeChanged);
      this.MouseEnter += new System.EventHandler(this.Toolbar_MouseEnter);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDown);
      this.MouseLeave += new System.EventHandler(this.Toolbar_MouseLeave);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseMove);
      this.contextMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button closeBtn;
    private System.Windows.Forms.Button phoneBtn;
    private System.Windows.Forms.Button screenCapture;
    private System.Windows.Forms.Button smsBtn;
    private System.Windows.Forms.Button clipboardBtn;
    private System.Windows.Forms.ContextMenuStrip contextMenu;


    private System.Windows.Forms.ToolStripMenuItem buzzToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem phoneToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem placeCallToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem endCallToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem speakerphoneToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem volumeUpMenu;
    private System.Windows.Forms.ToolStripMenuItem volumeDownMenu;
    private System.Windows.Forms.ToolStripMenuItem sendSMSToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem screenShotToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem keyboardToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clipboardToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sendToBlackBerryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sendToPCToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem runMacroToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem keepOnTopToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
    private System.Windows.Forms.Button clipboardGetBtn;
    private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;

  }
}