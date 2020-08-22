namespace Blurts
{
  partial class SetupForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
      this.OkBtn = new System.Windows.Forms.Button();
      this.cancelBtn = new System.Windows.Forms.Button();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.alertPage = new System.Windows.Forms.TabPage();
      this.tbAlertOpacity = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.screenCB = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.locationList = new System.Windows.Forms.ComboBox();
      this.showPhotosChkBox = new System.Windows.Forms.CheckBox();
      this.soundFileTxtBox = new System.Windows.Forms.TextBox();
      this.disTimetxtBox = new System.Windows.Forms.TextBox();
      this.clearSndBtn = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.testSndBtn = new System.Windows.Forms.Button();
      this.soundFileBtn = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.smsPage = new System.Windows.Forms.TabPage();
      this.bgColorBtn = new System.Windows.Forms.Button();
      this.textColorBtn = new System.Windows.Forms.Button();
      this.label9 = new System.Windows.Forms.Label();
      this.smsBgColor = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.smsTextColor = new System.Windows.Forms.TextBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.selectImgBtn = new System.Windows.Forms.Button();
      this.smsImage = new System.Windows.Forms.CheckBox();
      this.smsMultiMsg = new System.Windows.Forms.CheckBox();
      this.label4 = new System.Windows.Forms.Label();
      this.smsTxtBox = new System.Windows.Forms.TextBox();
      this.GeneralPage = new System.Windows.Forms.TabPage();
      this.tbToolbarOpacity = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.dbClickShowMsg = new System.Windows.Forms.CheckBox();
      this.toolBarTopMostChkBox = new System.Windows.Forms.CheckBox();
      this.startUpChk = new System.Windows.Forms.CheckBox();
      this.lockChkBox = new System.Windows.Forms.CheckBox();
      this.BlueToothPage = new System.Windows.Forms.TabPage();
      this.autoConnectChk = new System.Windows.Forms.CheckBox();
      this.searchBtn = new System.Windows.Forms.Button();
      this.bbLabel = new System.Windows.Forms.Label();
      this.adrLabel = new System.Windows.Forms.Label();
      this.addressTxt = new System.Windows.Forms.TextBox();
      this.helpTxtBox = new System.Windows.Forms.TextBox();
      this.bbNameTxt = new System.Windows.Forms.TextBox();
      this.pairBtn = new System.Windows.Forms.Button();
      this.portCmbBox = new System.Windows.Forms.ComboBox();
      this.comLabel = new System.Windows.Forms.Label();
      this.tabControl = new System.Windows.Forms.TabControl();
      this.ScriptPage = new System.Windows.Forms.TabPage();
      this.dataFolderBtn = new System.Windows.Forms.Button();
      this.scriptBtn = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.scriptFileTextbox = new System.Windows.Forms.TextBox();
      this.enableScriptErrMsgChkbx = new System.Windows.Forms.CheckBox();
      this.enableScriptChkBx = new System.Windows.Forms.CheckBox();
      this.commadPage = new System.Windows.Forms.TabPage();
      this.label7 = new System.Windows.Forms.Label();
      this.colorDialog = new System.Windows.Forms.ColorDialog();
      this.showToolbarOnConnectchkBox = new System.Windows.Forms.CheckBox();
      this.alertPage.SuspendLayout();
      this.smsPage.SuspendLayout();
      this.GeneralPage.SuspendLayout();
      this.BlueToothPage.SuspendLayout();
      this.tabControl.SuspendLayout();
      this.ScriptPage.SuspendLayout();
      this.commadPage.SuspendLayout();
      this.SuspendLayout();
      // 
      // OkBtn
      // 
      this.OkBtn.Location = new System.Drawing.Point(280, 445);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(75, 23);
      this.OkBtn.TabIndex = 5;
      this.OkBtn.Text = "Ok";
      this.OkBtn.UseVisualStyleBackColor = true;
      this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
      // 
      // cancelBtn
      // 
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.Location = new System.Drawing.Point(361, 445);
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size(75, 23);
      this.cancelBtn.TabIndex = 6;
      this.cancelBtn.Text = "Cancel";
      this.cancelBtn.UseVisualStyleBackColor = true;
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
      this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel1.Location = new System.Drawing.Point(485, 340);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(74, 31);
      this.linkLabel1.TabIndex = 14;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Help";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // alertPage
      // 
      this.alertPage.BackColor = System.Drawing.SystemColors.Window;
      this.alertPage.Controls.Add(this.tbAlertOpacity);
      this.alertPage.Controls.Add(this.label10);
      this.alertPage.Controls.Add(this.label5);
      this.alertPage.Controls.Add(this.screenCB);
      this.alertPage.Controls.Add(this.label2);
      this.alertPage.Controls.Add(this.locationList);
      this.alertPage.Controls.Add(this.showPhotosChkBox);
      this.alertPage.Controls.Add(this.soundFileTxtBox);
      this.alertPage.Controls.Add(this.disTimetxtBox);
      this.alertPage.Controls.Add(this.clearSndBtn);
      this.alertPage.Controls.Add(this.label3);
      this.alertPage.Controls.Add(this.testSndBtn);
      this.alertPage.Controls.Add(this.soundFileBtn);
      this.alertPage.Controls.Add(this.label1);
      this.alertPage.Location = new System.Drawing.Point(4, 22);
      this.alertPage.Name = "alertPage";
      this.alertPage.Padding = new System.Windows.Forms.Padding(3);
      this.alertPage.Size = new System.Drawing.Size(430, 408);
      this.alertPage.TabIndex = 4;
      this.alertPage.Text = "Alerts";
      // 
      // tbAlertOpacity
      // 
      this.tbAlertOpacity.Location = new System.Drawing.Point(120, 78);
      this.tbAlertOpacity.MaxLength = 3;
      this.tbAlertOpacity.Name = "tbAlertOpacity";
      this.tbAlertOpacity.Size = new System.Drawing.Size(45, 20);
      this.tbAlertOpacity.TabIndex = 37;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(21, 78);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(78, 13);
      this.label10.TabIndex = 36;
      this.label10.Text = "Alert Opacity %";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(21, 109);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(41, 13);
      this.label5.TabIndex = 35;
      this.label5.Text = "Screen";
      // 
      // screenCB
      // 
      this.screenCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.screenCB.FormattingEnabled = true;
      this.screenCB.Location = new System.Drawing.Point(91, 109);
      this.screenCB.Name = "screenCB";
      this.screenCB.Size = new System.Drawing.Size(121, 21);
      this.screenCB.TabIndex = 34;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(21, 143);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 13);
      this.label2.TabIndex = 33;
      this.label2.Text = "Location";
      // 
      // locationList
      // 
      this.locationList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.locationList.FormattingEnabled = true;
      this.locationList.Location = new System.Drawing.Point(91, 143);
      this.locationList.Name = "locationList";
      this.locationList.Size = new System.Drawing.Size(121, 21);
      this.locationList.TabIndex = 32;
      // 
      // showPhotosChkBox
      // 
      this.showPhotosChkBox.AutoSize = true;
      this.showPhotosChkBox.Location = new System.Drawing.Point(21, 9);
      this.showPhotosChkBox.Name = "showPhotosChkBox";
      this.showPhotosChkBox.Size = new System.Drawing.Size(131, 17);
      this.showPhotosChkBox.TabIndex = 31;
      this.showPhotosChkBox.Text = "Show photos on alerts";
      this.showPhotosChkBox.UseVisualStyleBackColor = true;
      // 
      // soundFileTxtBox
      // 
      this.soundFileTxtBox.Location = new System.Drawing.Point(91, 177);
      this.soundFileTxtBox.Name = "soundFileTxtBox";
      this.soundFileTxtBox.Size = new System.Drawing.Size(218, 20);
      this.soundFileTxtBox.TabIndex = 27;
      // 
      // disTimetxtBox
      // 
      this.disTimetxtBox.Location = new System.Drawing.Point(120, 47);
      this.disTimetxtBox.Name = "disTimetxtBox";
      this.disTimetxtBox.Size = new System.Drawing.Size(45, 20);
      this.disTimetxtBox.TabIndex = 25;
      // 
      // clearSndBtn
      // 
      this.clearSndBtn.Location = new System.Drawing.Point(209, 204);
      this.clearSndBtn.Name = "clearSndBtn";
      this.clearSndBtn.Size = new System.Drawing.Size(75, 23);
      this.clearSndBtn.TabIndex = 30;
      this.clearSndBtn.Text = "Clear Sound";
      this.clearSndBtn.UseVisualStyleBackColor = true;
      this.clearSndBtn.Click += new System.EventHandler(this.clearSndBtn_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(21, 47);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(91, 13);
      this.label3.TabIndex = 24;
      this.label3.Text = "Alert Display Time";
      // 
      // testSndBtn
      // 
      this.testSndBtn.Location = new System.Drawing.Point(120, 204);
      this.testSndBtn.Name = "testSndBtn";
      this.testSndBtn.Size = new System.Drawing.Size(75, 23);
      this.testSndBtn.TabIndex = 29;
      this.testSndBtn.Text = "Test Sound";
      this.testSndBtn.UseVisualStyleBackColor = true;
      this.testSndBtn.Click += new System.EventHandler(this.testSndBtn_Click);
      // 
      // soundFileBtn
      // 
      this.soundFileBtn.Location = new System.Drawing.Point(315, 177);
      this.soundFileBtn.Name = "soundFileBtn";
      this.soundFileBtn.Size = new System.Drawing.Size(32, 23);
      this.soundFileBtn.TabIndex = 28;
      this.soundFileBtn.Text = "...";
      this.soundFileBtn.UseVisualStyleBackColor = true;
      this.soundFileBtn.Click += new System.EventHandler(this.soundFileBtn_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(21, 177);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(61, 13);
      this.label1.TabIndex = 26;
      this.label1.Text = "Play Sound";
      // 
      // smsPage
      // 
      this.smsPage.BackColor = System.Drawing.SystemColors.Window;
      this.smsPage.Controls.Add(this.bgColorBtn);
      this.smsPage.Controls.Add(this.textColorBtn);
      this.smsPage.Controls.Add(this.label9);
      this.smsPage.Controls.Add(this.smsBgColor);
      this.smsPage.Controls.Add(this.label8);
      this.smsPage.Controls.Add(this.smsTextColor);
      this.smsPage.Controls.Add(this.textBox1);
      this.smsPage.Controls.Add(this.selectImgBtn);
      this.smsPage.Controls.Add(this.smsImage);
      this.smsPage.Controls.Add(this.smsMultiMsg);
      this.smsPage.Controls.Add(this.label4);
      this.smsPage.Controls.Add(this.smsTxtBox);
      this.smsPage.Location = new System.Drawing.Point(4, 22);
      this.smsPage.Name = "smsPage";
      this.smsPage.Padding = new System.Windows.Forms.Padding(3);
      this.smsPage.Size = new System.Drawing.Size(430, 408);
      this.smsPage.TabIndex = 3;
      this.smsPage.Text = "SMS";
      this.smsPage.Click += new System.EventHandler(this.smsPage_Click);
      // 
      // bgColorBtn
      // 
      this.bgColorBtn.Location = new System.Drawing.Point(225, 268);
      this.bgColorBtn.Name = "bgColorBtn";
      this.bgColorBtn.Size = new System.Drawing.Size(31, 23);
      this.bgColorBtn.TabIndex = 28;
      this.bgColorBtn.Text = "...";
      this.bgColorBtn.UseVisualStyleBackColor = true;
      this.bgColorBtn.Click += new System.EventHandler(this.bgColorBtn_Click);
      // 
      // textColorBtn
      // 
      this.textColorBtn.Location = new System.Drawing.Point(225, 238);
      this.textColorBtn.Name = "textColorBtn";
      this.textColorBtn.Size = new System.Drawing.Size(31, 23);
      this.textColorBtn.TabIndex = 27;
      this.textColorBtn.Text = "...";
      this.textColorBtn.UseVisualStyleBackColor = true;
      this.textColorBtn.Click += new System.EventHandler(this.textColorBtn_Click);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(17, 273);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(92, 13);
      this.label9.TabIndex = 25;
      this.label9.Text = "Background Color";
      // 
      // smsBgColor
      // 
      this.smsBgColor.Location = new System.Drawing.Point(127, 269);
      this.smsBgColor.Name = "smsBgColor";
      this.smsBgColor.Size = new System.Drawing.Size(92, 20);
      this.smsBgColor.TabIndex = 26;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(17, 243);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(55, 13);
      this.label8.TabIndex = 23;
      this.label8.Text = "Text Color";
      // 
      // smsTextColor
      // 
      this.smsTextColor.Location = new System.Drawing.Point(127, 239);
      this.smsTextColor.Name = "smsTextColor";
      this.smsTextColor.Size = new System.Drawing.Size(92, 20);
      this.smsTextColor.TabIndex = 24;
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.SystemColors.Window;
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.Location = new System.Drawing.Point(20, 19);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(390, 49);
      this.textBox1.TabIndex = 22;
      this.textBox1.Text = "Max SMS should be set to the max SMS message size allowed by your wireless carrie" +
          "r. 140 or 160 are most common any other value could cause your messages to not b" +
          "e sent.";
      // 
      // selectImgBtn
      // 
      this.selectImgBtn.Location = new System.Drawing.Point(20, 194);
      this.selectImgBtn.Name = "selectImgBtn";
      this.selectImgBtn.Size = new System.Drawing.Size(141, 23);
      this.selectImgBtn.TabIndex = 21;
      this.selectImgBtn.Text = "Select my image";
      this.selectImgBtn.UseVisualStyleBackColor = true;
      this.selectImgBtn.Click += new System.EventHandler(this.selectImgBtn_Click);
      // 
      // smsImage
      // 
      this.smsImage.AutoSize = true;
      this.smsImage.Location = new System.Drawing.Point(20, 162);
      this.smsImage.Name = "smsImage";
      this.smsImage.Size = new System.Drawing.Size(171, 17);
      this.smsImage.TabIndex = 20;
      this.smsImage.Text = "Display images in SMS threads";
      this.smsImage.UseVisualStyleBackColor = true;
      // 
      // smsMultiMsg
      // 
      this.smsMultiMsg.Location = new System.Drawing.Point(20, 107);
      this.smsMultiMsg.Name = "smsMultiMsg";
      this.smsMultiMsg.Size = new System.Drawing.Size(404, 49);
      this.smsMultiMsg.TabIndex = 19;
      this.smsMultiMsg.Text = "Allow text to span more then one message. Messages large then Max SMS setting wil" +
          "l be sent as multiple SMS message. (Be aware of your wireless carrier charges.) " +
          "";
      this.smsMultiMsg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.smsMultiMsg.UseVisualStyleBackColor = true;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(17, 77);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(53, 13);
      this.label4.TabIndex = 17;
      this.label4.Text = "Max SMS";
      // 
      // smsTxtBox
      // 
      this.smsTxtBox.Location = new System.Drawing.Point(86, 74);
      this.smsTxtBox.Name = "smsTxtBox";
      this.smsTxtBox.Size = new System.Drawing.Size(75, 20);
      this.smsTxtBox.TabIndex = 18;
      // 
      // GeneralPage
      // 
      this.GeneralPage.BackColor = System.Drawing.SystemColors.Window;
      this.GeneralPage.Controls.Add(this.showToolbarOnConnectchkBox);
      this.GeneralPage.Controls.Add(this.tbToolbarOpacity);
      this.GeneralPage.Controls.Add(this.label11);
      this.GeneralPage.Controls.Add(this.dbClickShowMsg);
      this.GeneralPage.Controls.Add(this.toolBarTopMostChkBox);
      this.GeneralPage.Controls.Add(this.startUpChk);
      this.GeneralPage.Controls.Add(this.lockChkBox);
      this.GeneralPage.Location = new System.Drawing.Point(4, 22);
      this.GeneralPage.Name = "GeneralPage";
      this.GeneralPage.Padding = new System.Windows.Forms.Padding(3);
      this.GeneralPage.Size = new System.Drawing.Size(430, 408);
      this.GeneralPage.TabIndex = 1;
      this.GeneralPage.Text = "General";
      // 
      // tbToolbarOpacity
      // 
      this.tbToolbarOpacity.Location = new System.Drawing.Point(119, 134);
      this.tbToolbarOpacity.MaxLength = 3;
      this.tbToolbarOpacity.Name = "tbToolbarOpacity";
      this.tbToolbarOpacity.Size = new System.Drawing.Size(45, 20);
      this.tbToolbarOpacity.TabIndex = 39;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(20, 138);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(93, 13);
      this.label11.TabIndex = 38;
      this.label11.Text = "Toolbar Opacity %";
      // 
      // dbClickShowMsg
      // 
      this.dbClickShowMsg.AutoSize = true;
      this.dbClickShowMsg.Location = new System.Drawing.Point(20, 111);
      this.dbClickShowMsg.Name = "dbClickShowMsg";
      this.dbClickShowMsg.Size = new System.Drawing.Size(219, 17);
      this.dbClickShowMsg.TabIndex = 19;
      this.dbClickShowMsg.Text = "Double clicking icon shows last message";
      this.dbClickShowMsg.UseVisualStyleBackColor = true;
      // 
      // toolBarTopMostChkBox
      // 
      this.toolBarTopMostChkBox.AutoSize = true;
      this.toolBarTopMostChkBox.Location = new System.Drawing.Point(20, 65);
      this.toolBarTopMostChkBox.Name = "toolBarTopMostChkBox";
      this.toolBarTopMostChkBox.Size = new System.Drawing.Size(220, 17);
      this.toolBarTopMostChkBox.TabIndex = 18;
      this.toolBarTopMostChkBox.Text = "Keep the toolbar on top of other windows";
      this.toolBarTopMostChkBox.UseVisualStyleBackColor = true;
      // 
      // startUpChk
      // 
      this.startUpChk.AutoSize = true;
      this.startUpChk.Location = new System.Drawing.Point(20, 19);
      this.startUpChk.Name = "startUpChk";
      this.startUpChk.Size = new System.Drawing.Size(162, 17);
      this.startUpChk.TabIndex = 11;
      this.startUpChk.Text = "Add Blurts to desktop startup";
      this.startUpChk.UseVisualStyleBackColor = true;
      // 
      // lockChkBox
      // 
      this.lockChkBox.AutoSize = true;
      this.lockChkBox.Location = new System.Drawing.Point(20, 42);
      this.lockChkBox.Name = "lockChkBox";
      this.lockChkBox.Size = new System.Drawing.Size(177, 17);
      this.lockChkBox.TabIndex = 17;
      this.lockChkBox.Text = "Lock workstation on disconnect";
      this.lockChkBox.UseVisualStyleBackColor = true;
      // 
      // BlueToothPage
      // 
      this.BlueToothPage.BackColor = System.Drawing.SystemColors.Window;
      this.BlueToothPage.Controls.Add(this.autoConnectChk);
      this.BlueToothPage.Controls.Add(this.searchBtn);
      this.BlueToothPage.Controls.Add(this.bbLabel);
      this.BlueToothPage.Controls.Add(this.adrLabel);
      this.BlueToothPage.Controls.Add(this.addressTxt);
      this.BlueToothPage.Controls.Add(this.helpTxtBox);
      this.BlueToothPage.Controls.Add(this.bbNameTxt);
      this.BlueToothPage.Controls.Add(this.pairBtn);
      this.BlueToothPage.Controls.Add(this.portCmbBox);
      this.BlueToothPage.Controls.Add(this.comLabel);
      this.BlueToothPage.Location = new System.Drawing.Point(4, 22);
      this.BlueToothPage.Name = "BlueToothPage";
      this.BlueToothPage.Padding = new System.Windows.Forms.Padding(3);
      this.BlueToothPage.Size = new System.Drawing.Size(430, 408);
      this.BlueToothPage.TabIndex = 0;
      this.BlueToothPage.Text = "Bluetooth";
      // 
      // autoConnectChk
      // 
      this.autoConnectChk.AutoSize = true;
      this.autoConnectChk.Location = new System.Drawing.Point(16, 165);
      this.autoConnectChk.Name = "autoConnectChk";
      this.autoConnectChk.Size = new System.Drawing.Size(139, 17);
      this.autoConnectChk.TabIndex = 4;
      this.autoConnectChk.Text = "Auto Connect Bluetooth";
      this.autoConnectChk.UseVisualStyleBackColor = true;
      // 
      // searchBtn
      // 
      this.searchBtn.Location = new System.Drawing.Point(258, 88);
      this.searchBtn.Name = "searchBtn";
      this.searchBtn.Size = new System.Drawing.Size(75, 23);
      this.searchBtn.TabIndex = 2;
      this.searchBtn.Text = "Search";
      this.searchBtn.UseVisualStyleBackColor = true;
      this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
      // 
      // bbLabel
      // 
      this.bbLabel.AutoSize = true;
      this.bbLabel.Location = new System.Drawing.Point(13, 91);
      this.bbLabel.Name = "bbLabel";
      this.bbLabel.Size = new System.Drawing.Size(58, 13);
      this.bbLabel.TabIndex = 7;
      this.bbLabel.Text = "BlackBerry";
      // 
      // adrLabel
      // 
      this.adrLabel.AutoSize = true;
      this.adrLabel.Location = new System.Drawing.Point(13, 119);
      this.adrLabel.Name = "adrLabel";
      this.adrLabel.Size = new System.Drawing.Size(45, 13);
      this.adrLabel.TabIndex = 0;
      this.adrLabel.Text = "Address";
      // 
      // addressTxt
      // 
      this.addressTxt.Location = new System.Drawing.Point(87, 116);
      this.addressTxt.Name = "addressTxt";
      this.addressTxt.Size = new System.Drawing.Size(161, 20);
      this.addressTxt.TabIndex = 1;
      this.addressTxt.TextChanged += new System.EventHandler(this.addressTxt_TextChanged);
      // 
      // helpTxtBox
      // 
      this.helpTxtBox.BackColor = System.Drawing.SystemColors.Window;
      this.helpTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.helpTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.helpTxtBox.Location = new System.Drawing.Point(13, 15);
      this.helpTxtBox.Multiline = true;
      this.helpTxtBox.Name = "helpTxtBox";
      this.helpTxtBox.ReadOnly = true;
      this.helpTxtBox.Size = new System.Drawing.Size(320, 49);
      this.helpTxtBox.TabIndex = 18;
      this.helpTxtBox.Text = "Create a virtual COM Port for the Blurts BlackBerry service and then select the C" +
          "OM port below. Click help (? button in titlebar)  for more help.";
      this.helpTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // bbNameTxt
      // 
      this.bbNameTxt.BackColor = System.Drawing.SystemColors.Window;
      this.bbNameTxt.Location = new System.Drawing.Point(88, 88);
      this.bbNameTxt.Name = "bbNameTxt";
      this.bbNameTxt.ReadOnly = true;
      this.bbNameTxt.Size = new System.Drawing.Size(161, 20);
      this.bbNameTxt.TabIndex = 8;
      // 
      // pairBtn
      // 
      this.pairBtn.Location = new System.Drawing.Point(258, 116);
      this.pairBtn.Name = "pairBtn";
      this.pairBtn.Size = new System.Drawing.Size(75, 23);
      this.pairBtn.TabIndex = 3;
      this.pairBtn.Text = "Pair";
      this.pairBtn.UseVisualStyleBackColor = true;
      this.pairBtn.Click += new System.EventHandler(this.pairBtn_Click);
      // 
      // portCmbBox
      // 
      this.portCmbBox.FormattingEnabled = true;
      this.portCmbBox.Location = new System.Drawing.Point(87, 88);
      this.portCmbBox.Name = "portCmbBox";
      this.portCmbBox.Size = new System.Drawing.Size(161, 21);
      this.portCmbBox.TabIndex = 12;
      // 
      // comLabel
      // 
      this.comLabel.AutoSize = true;
      this.comLabel.Location = new System.Drawing.Point(10, 91);
      this.comLabel.Name = "comLabel";
      this.comLabel.Size = new System.Drawing.Size(53, 13);
      this.comLabel.TabIndex = 13;
      this.comLabel.Text = "COM Port";
      // 
      // tabControl
      // 
      this.tabControl.Controls.Add(this.BlueToothPage);
      this.tabControl.Controls.Add(this.GeneralPage);
      this.tabControl.Controls.Add(this.alertPage);
      this.tabControl.Controls.Add(this.smsPage);
      this.tabControl.Controls.Add(this.ScriptPage);
      this.tabControl.Controls.Add(this.commadPage);
      this.tabControl.Location = new System.Drawing.Point(5, 5);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new System.Drawing.Size(438, 434);
      this.tabControl.TabIndex = 24;
      this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
      // 
      // ScriptPage
      // 
      this.ScriptPage.BackColor = System.Drawing.SystemColors.Window;
      this.ScriptPage.Controls.Add(this.dataFolderBtn);
      this.ScriptPage.Controls.Add(this.scriptBtn);
      this.ScriptPage.Controls.Add(this.label6);
      this.ScriptPage.Controls.Add(this.scriptFileTextbox);
      this.ScriptPage.Controls.Add(this.enableScriptErrMsgChkbx);
      this.ScriptPage.Controls.Add(this.enableScriptChkBx);
      this.ScriptPage.Location = new System.Drawing.Point(4, 22);
      this.ScriptPage.Name = "ScriptPage";
      this.ScriptPage.Padding = new System.Windows.Forms.Padding(3);
      this.ScriptPage.Size = new System.Drawing.Size(430, 408);
      this.ScriptPage.TabIndex = 5;
      this.ScriptPage.Text = "Scripting";
      // 
      // dataFolderBtn
      // 
      this.dataFolderBtn.Location = new System.Drawing.Point(271, 379);
      this.dataFolderBtn.Name = "dataFolderBtn";
      this.dataFolderBtn.Size = new System.Drawing.Size(136, 23);
      this.dataFolderBtn.TabIndex = 5;
      this.dataFolderBtn.Text = "Open Data Folder";
      this.dataFolderBtn.UseVisualStyleBackColor = true;
      this.dataFolderBtn.Click += new System.EventHandler(this.dataFolderBtn_Click);
      // 
      // scriptBtn
      // 
      this.scriptBtn.Location = new System.Drawing.Point(495, 93);
      this.scriptBtn.Name = "scriptBtn";
      this.scriptBtn.Size = new System.Drawing.Size(30, 23);
      this.scriptBtn.TabIndex = 4;
      this.scriptBtn.Text = "...";
      this.scriptBtn.UseVisualStyleBackColor = true;
      this.scriptBtn.Click += new System.EventHandler(this.scriptBtn_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(19, 74);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(53, 13);
      this.label6.TabIndex = 3;
      this.label6.Text = "Script File";
      // 
      // scriptFileTextbox
      // 
      this.scriptFileTextbox.Location = new System.Drawing.Point(19, 93);
      this.scriptFileTextbox.Name = "scriptFileTextbox";
      this.scriptFileTextbox.Size = new System.Drawing.Size(388, 20);
      this.scriptFileTextbox.TabIndex = 2;
      // 
      // enableScriptErrMsgChkbx
      // 
      this.enableScriptErrMsgChkbx.AutoSize = true;
      this.enableScriptErrMsgChkbx.Location = new System.Drawing.Point(19, 49);
      this.enableScriptErrMsgChkbx.Name = "enableScriptErrMsgChkbx";
      this.enableScriptErrMsgChkbx.Size = new System.Drawing.Size(166, 17);
      this.enableScriptErrMsgChkbx.TabIndex = 1;
      this.enableScriptErrMsgChkbx.Text = "Display Script Error Messages";
      this.enableScriptErrMsgChkbx.UseVisualStyleBackColor = true;
      // 
      // enableScriptChkBx
      // 
      this.enableScriptChkBx.AutoSize = true;
      this.enableScriptChkBx.Location = new System.Drawing.Point(19, 16);
      this.enableScriptChkBx.Name = "enableScriptChkBx";
      this.enableScriptChkBx.Size = new System.Drawing.Size(103, 17);
      this.enableScriptChkBx.TabIndex = 0;
      this.enableScriptChkBx.Text = "Enable Scripting";
      this.enableScriptChkBx.UseVisualStyleBackColor = true;
      // 
      // commadPage
      // 
      this.commadPage.Controls.Add(this.label7);
      this.commadPage.Location = new System.Drawing.Point(4, 22);
      this.commadPage.Name = "commadPage";
      this.commadPage.Padding = new System.Windows.Forms.Padding(3);
      this.commadPage.Size = new System.Drawing.Size(430, 408);
      this.commadPage.TabIndex = 6;
      this.commadPage.Text = "Hot Keys";
      this.commadPage.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(15, 20);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(50, 13);
      this.label7.TabIndex = 0;
      this.label7.Text = "Hot Keys";
      // 
      // showToolbarOnConnectchkBox
      // 
      this.showToolbarOnConnectchkBox.AutoSize = true;
      this.showToolbarOnConnectchkBox.Location = new System.Drawing.Point(20, 88);
      this.showToolbarOnConnectchkBox.Name = "showToolbarOnConnectchkBox";
      this.showToolbarOnConnectchkBox.Size = new System.Drawing.Size(227, 17);
      this.showToolbarOnConnectchkBox.TabIndex = 40;
      this.showToolbarOnConnectchkBox.Text = "Show/hide toolbar on connect/disconnect";
      this.showToolbarOnConnectchkBox.UseVisualStyleBackColor = true;
      // 
      // SetupForm
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.CancelButton = this.cancelBtn;
      this.ClientSize = new System.Drawing.Size(448, 480);
      this.Controls.Add(this.tabControl);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.cancelBtn);
      this.Controls.Add(this.OkBtn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.HelpButton = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SetupForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Options";
      this.Load += new System.EventHandler(this.SetupForm_Load);
      this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.SetupForm_HelpButtonClicked);
      this.alertPage.ResumeLayout(false);
      this.alertPage.PerformLayout();
      this.smsPage.ResumeLayout(false);
      this.smsPage.PerformLayout();
      this.GeneralPage.ResumeLayout(false);
      this.GeneralPage.PerformLayout();
      this.BlueToothPage.ResumeLayout(false);
      this.BlueToothPage.PerformLayout();
      this.tabControl.ResumeLayout(false);
      this.ScriptPage.ResumeLayout(false);
      this.ScriptPage.PerformLayout();
      this.commadPage.ResumeLayout(false);
      this.commadPage.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.TabPage alertPage;
    private System.Windows.Forms.CheckBox showPhotosChkBox;
    private System.Windows.Forms.TextBox soundFileTxtBox;
    private System.Windows.Forms.TextBox disTimetxtBox;
    private System.Windows.Forms.Button clearSndBtn;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button testSndBtn;
    private System.Windows.Forms.Button soundFileBtn;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabPage smsPage;
    private System.Windows.Forms.Button selectImgBtn;
    private System.Windows.Forms.CheckBox smsImage;
    private System.Windows.Forms.CheckBox smsMultiMsg;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox smsTxtBox;
    private System.Windows.Forms.TabPage GeneralPage;
    private System.Windows.Forms.CheckBox startUpChk;
    private System.Windows.Forms.CheckBox lockChkBox;
    private System.Windows.Forms.TabPage BlueToothPage;
    private System.Windows.Forms.CheckBox autoConnectChk;
    private System.Windows.Forms.Button searchBtn;
    private System.Windows.Forms.Label bbLabel;
    private System.Windows.Forms.Label adrLabel;
    private System.Windows.Forms.TextBox addressTxt;
    private System.Windows.Forms.TextBox helpTxtBox;
    private System.Windows.Forms.TextBox bbNameTxt;
    private System.Windows.Forms.Button pairBtn;
    private System.Windows.Forms.ComboBox portCmbBox;
    private System.Windows.Forms.Label comLabel;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox locationList;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox screenCB;
    private System.Windows.Forms.TabPage ScriptPage;
    private System.Windows.Forms.Button scriptBtn;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox scriptFileTextbox;
    private System.Windows.Forms.CheckBox enableScriptErrMsgChkbx;
    private System.Windows.Forms.CheckBox enableScriptChkBx;
    private System.Windows.Forms.CheckBox toolBarTopMostChkBox;
    private System.Windows.Forms.TabPage commadPage;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.CheckBox dbClickShowMsg;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox smsBgColor;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox smsTextColor;
    private System.Windows.Forms.ColorDialog colorDialog;
    private System.Windows.Forms.Button bgColorBtn;
    private System.Windows.Forms.Button textColorBtn;
    private System.Windows.Forms.Button dataFolderBtn;
    private System.Windows.Forms.TextBox tbAlertOpacity;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox tbToolbarOpacity;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.CheckBox showToolbarOnConnectchkBox;

  }
}