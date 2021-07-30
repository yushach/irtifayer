namespace irtifa
{
    partial class Form1
    {
        //string SEND_PORT_NAME = "/dev/pts/2";

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
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.propListView = new System.Windows.Forms.ListView();
            this.names = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.values = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pointPropView = new System.Windows.Forms.ListView();
            this.pointOrderColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.latitudeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.longitudeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.altitudeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.speedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roleColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.editPointButton = new System.Windows.Forms.Button();
            this.addPointAfterButton = new System.Windows.Forms.Button();
            this.addPointBeforeButton = new System.Windows.Forms.Button();
            this.removePointButton = new System.Windows.Forms.Button();
            this.stateIndicator = new System.Windows.Forms.Label();
            this.armLabel = new System.Windows.Forms.Label();
            this.actualArmLabel = new System.Windows.Forms.Label();
            this.modeLabel = new System.Windows.Forms.Label();
            this.lasttimeLabel = new System.Windows.Forms.Label();
            this.actualLasttimeLabel = new System.Windows.Forms.Label();
            this.actualModeLabel = new System.Windows.Forms.Label();
            //this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bQuickLoad = new System.Windows.Forms.Button();
            //this.startMissionButton = new System.Windows.Forms.Button();
            //this.stateLabel = new System.Windows.Forms.Label();
            this.bQuickSave = new System.Windows.Forms.Button();
            //this.lastReceivedLabel = new System.Windows.Forms.Label();
            //this.portStatusLabel = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.bDisarm = new System.Windows.Forms.Button();
            //this.sendDataBtn = new System.Windows.Forms.Button();
            //this.closePortBtn = new System.Windows.Forms.Button();
            //this.portOpenBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            /*this.baudRateBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sendPortNameBox = new System.Windows.Forms.TextBox();*/



            this.stateLabel = new System.Windows.Forms.Label();
            this.actualStateLabel = new System.Windows.Forms.Label();
            this.modeCombobox = new System.Windows.Forms.ComboBox();
            this.setModeButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.takeoffAltBox = new System.Windows.Forms.TextBox();


            this.telLogBox = new System.Windows.Forms.RichTextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.loadRouteButton = new System.Windows.Forms.Button();
            this.trackBar6 = new System.Windows.Forms.TrackBar();
            this.propellerIndicator1 = new irtifa.PropellerIndicator();
            this.pfd1 = new irtifa.PFD();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statusBox = new System.Windows.Forms.GroupBox();
            //this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusBox.SuspendLayout();
            
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            //this.gmap.LevelsKeepInMemory = 5;
            this.gmap.Location = new System.Drawing.Point(3, 9);
            this.gmap.Margin = new System.Windows.Forms.Padding(0);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 20;
            this.gmap.MinZoom = 1;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Fractional;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(616, 633);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 16;

            this.gmap.ShowCenter = false;


            //this.gmap.OnMapClick += new GMap.NET.WindowsForms.MapClick(this.gmap_OnMapClick);

            this.gmap.Click += new System.EventHandler(this.gmap_ClickHandler);


            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick);
            this.gmap.OnMapDrag += new GMap.NET.MapDrag(this.gmap_OnMapDrag);
            this.gmap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseDown);
            this.gmap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseUp);
            // 
            // propListView
            // 
            this.propListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.names,
            this.values});
            this.propListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.propListView.GridLines = true;
            this.propListView.HideSelection = false;
            this.propListView.Location = new System.Drawing.Point(1236, 12);
            this.propListView.Name = "propListView";
            this.propListView.Size = new System.Drawing.Size(228, 800);
            this.propListView.TabIndex = 3;
            this.propListView.UseCompatibleStateImageBehavior = false;
            this.propListView.View = System.Windows.Forms.View.Details;

            gmap.MapProvider = GMap.NET.MapProviders.YandexMapProvider.Instance;
            // 
            // names
            // 
            this.names.Text = "Veri Adı";
            this.names.Width = 105;
            // 
            // values
            // 
            this.values.Text = "Değer";
            this.values.Width = 115;
            // 
            // pointPropView
            // 
            this.pointPropView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointPropView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pointOrderColumn,
            this.latitudeColumn,
            this.longitudeColumn,
            this.altitudeColumn,
            this.speedColumn,
            this.roleColumn    
            });
            this.pointPropView.FullRowSelect = true;
            this.pointPropView.GridLines = true;
            this.pointPropView.HideSelection = false;
            this.pointPropView.Location = new System.Drawing.Point(3, 648);
            this.pointPropView.Name = "pointPropView";
            this.pointPropView.Size = new System.Drawing.Size(1045, 171);
            this.pointPropView.TabIndex = 4;
            this.pointPropView.UseCompatibleStateImageBehavior = false;
            this.pointPropView.View = System.Windows.Forms.View.Details;
            // 
            // pointOrderColumn
            // 
            this.pointOrderColumn.Text = "Sıra";
            this.pointOrderColumn.Width = 71;
            // 
            // latitudeColumn
            // 
            this.latitudeColumn.Text = "Enlem";
            this.latitudeColumn.Width = 156;
            // 
            // longitudeColumn
            // 
            this.longitudeColumn.Text = "Boylam";
            this.longitudeColumn.Width = 155;
            // 
            // altitudeColumn
            // 
            this.altitudeColumn.Text = "Yükseklik";
            this.altitudeColumn.Width = 152;
            // 
            // speedColumn
            // 
            this.speedColumn.Text = "Hız";
            this.speedColumn.Width = 150;
            //
            // roleColumn
            //
            this.roleColumn.Text = "Rol";
            this.roleColumn.Width = 100;
            // 
            // editPointButton
            // 
            this.editPointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editPointButton.Location = new System.Drawing.Point(1054, 648);
            this.editPointButton.Name = "editPointButton";
            this.editPointButton.Size = new System.Drawing.Size(179, 23);
            this.editPointButton.TabIndex = 5;
            this.editPointButton.Text = "Noktayı Düzenle";
            this.editPointButton.UseVisualStyleBackColor = true;
            this.editPointButton.Click += new System.EventHandler(this.editPointButton_Click);
            // 
            // addPointAfterButton
            // 
            this.addPointAfterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addPointAfterButton.Location = new System.Drawing.Point(1054, 677);
            this.addPointAfterButton.Name = "addPointAfterButton";
            this.addPointAfterButton.Size = new System.Drawing.Size(179, 23);
            this.addPointAfterButton.TabIndex = 6;
            this.addPointAfterButton.Text = "Seçimden Sonra Nokta Ekle";
            this.addPointAfterButton.UseVisualStyleBackColor = true;
            this.addPointAfterButton.Click += new System.EventHandler(this.addPointAfterButton_Click);
            // 
            // addPointBeforeButton
            // 
            this.addPointBeforeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addPointBeforeButton.Location = new System.Drawing.Point(1054, 706);
            this.addPointBeforeButton.Name = "addPointBeforeButton";
            this.addPointBeforeButton.Size = new System.Drawing.Size(179, 23);
            this.addPointBeforeButton.TabIndex = 7;
            this.addPointBeforeButton.Text = "Seçimden Önce Nokta Ekle";
            this.addPointBeforeButton.UseVisualStyleBackColor = true;
            this.addPointBeforeButton.Click += new System.EventHandler(this.addPointBeforeButton_Click);
            // 
            // removePointButton
            // 
            this.removePointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removePointButton.Location = new System.Drawing.Point(1054, 735);
            this.removePointButton.Name = "removePointButton";
            this.removePointButton.Size = new System.Drawing.Size(179, 23);
            this.removePointButton.TabIndex = 8;
            this.removePointButton.Text = "Noktayı Kaldır";
            this.removePointButton.UseVisualStyleBackColor = true;
            this.removePointButton.Click += new System.EventHandler(this.removePointButton_Click);
            // 
            // stateIndicator
            // 
            this.stateIndicator.AutoSize = true;
            this.stateIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.stateIndicator.Location = new System.Drawing.Point(12, 12);
            this.stateIndicator.Name = "stateIndicator";
            this.stateIndicator.Size = new System.Drawing.Size(193, 25);
            this.stateIndicator.TabIndex = 9;
            this.stateIndicator.Text = "STATE INDICATOR";
            this.stateIndicator.Visible = false;
            // 
            // groupBox1
            // 
            //this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.groupBox1.Controls.Add(this.startMissionButton);
            //this.groupBox1.Controls.Add(this.stateLabel);
            //this.groupBox1.Controls.Add(this.lastReceivedLabel);
            //this.groupBox1.Controls.Add(this.portStatusLabel);
            //this.groupBox1.Controls.Add(this.sendDataBtn);
            //this.groupBox1.Controls.Add(this.closePortBtn);
            //this.groupBox1.Controls.Add(this.portOpenBtn);
            /*this.groupBox1.Controls.Add(this.baudRateBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.sendPortNameBox);*/
            /*this.groupBox1.Location = new System.Drawing.Point(1048, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 232);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Bağlantısı";*/
            // 
            // bQuickLoad
            // 
            this.bQuickLoad.Location = new System.Drawing.Point(6, 49);
            this.bQuickLoad.Name = "bQuickLoad";
            this.bQuickLoad.Size = new System.Drawing.Size(165, 23);
            this.bQuickLoad.TabIndex = 26;
            this.bQuickLoad.Text = "Çabuk Yedekten Yükle";
            this.bQuickLoad.UseVisualStyleBackColor = true;
            this.bQuickLoad.Click += new System.EventHandler(this.bQuickLoad_Click);
            // 
            // clearLogButton
            // 
            this.clearLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearLogButton.Location = new System.Drawing.Point(1055, 600);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(165, 23);
            this.clearLogButton.TabIndex = 26;
            this.clearLogButton.Text = "Logu Temizle";
            this.clearLogButton.UseVisualStyleBackColor = true;
            this.clearLogButton.Click += new System.EventHandler(this.clearLogButton_Click);
            // 
            // startMissionButton
            // 
            /*this.startMissionButton.Location = new System.Drawing.Point(101, 145);
            this.startMissionButton.Name = "startMissionButton";
            this.startMissionButton.Size = new System.Drawing.Size(75, 23);
            this.startMissionButton.TabIndex = 11;
            this.startMissionButton.Text = "Başla";
            this.startMissionButton.UseVisualStyleBackColor = true;
            this.startMissionButton.Click += new System.EventHandler(this.startMissionButton_Click);*/
            // 
            // stateLabel
            // 
            /*this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(7, 204);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(39, 13);
            this.stateLabel.TabIndex = 9;
            this.stateLabel.Text = "Belirsiz";*/
            // 
            // bQuickSave
            // 
            this.bQuickSave.Location = new System.Drawing.Point(6, 20);
            this.bQuickSave.Name = "bQuickSave";
            this.bQuickSave.Size = new System.Drawing.Size(165, 23);
            this.bQuickSave.TabIndex = 25;
            this.bQuickSave.Text = "Çabuk Yedekle";
            this.bQuickSave.UseVisualStyleBackColor = true;
            this.bQuickSave.Click += new System.EventHandler(this.bQuickSave_Click);
            // 
            // lastReceivedLabel
            // 
            /*this.lastReceivedLabel.AutoSize = true;
            this.lastReceivedLabel.Location = new System.Drawing.Point(7, 191);
            this.lastReceivedLabel.Name = "lastReceivedLabel";
            this.lastReceivedLabel.Size = new System.Drawing.Size(35, 13);
            this.lastReceivedLabel.TabIndex = 8;
            this.lastReceivedLabel.Text = "State:";*/
            // 
            // portStatusLabel
            // 
            /*this.portStatusLabel.AutoSize = true;
            this.portStatusLabel.Location = new System.Drawing.Point(7, 71);
            this.portStatusLabel.Name = "portStatusLabel";
            this.portStatusLabel.Size = new System.Drawing.Size(115, 13);
            this.portStatusLabel.TabIndex = 7;
            this.portStatusLabel.Text = "Port Durumu: Bilinmiyor";*/
            // 
            // armLabel
            // 
            this.armLabel.AutoSize = true;
            this.armLabel.Location = new System.Drawing.Point(7, 15);
            this.armLabel.Name = "armLabel";
            this.armLabel.Size = new System.Drawing.Size(35, 13);
            this.armLabel.TabIndex = 8;
            this.armLabel.Text = "Arm:";
            // 
            // actualArmLabel
            // 
            this.actualArmLabel.AutoSize = true;
            this.actualArmLabel.Location = new System.Drawing.Point(36, 15);
            this.actualArmLabel.Name = "actualArmLabel";
            this.actualArmLabel.Size = new System.Drawing.Size(35, 13);
            this.actualArmLabel.TabIndex = 8;
            this.actualArmLabel.Text = "Bilinmiyor";
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Location = new System.Drawing.Point(7, 30);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(35, 13);
            this.modeLabel.TabIndex = 8;
            this.modeLabel.Text = "Mod:";
            // 
            // actualModeLabel
            // 
            this.actualModeLabel.AutoSize = true;
            this.actualModeLabel.Location = new System.Drawing.Point(36, 30);
            this.actualModeLabel.Name = "actualModeLabel";
            this.actualModeLabel.Size = new System.Drawing.Size(35, 13);
            this.actualModeLabel.TabIndex = 8;
            this.actualModeLabel.Text = "Bilinmiyor";
            // 
            // lasttimeLabel
            // 
            this.lasttimeLabel.AutoSize = true;
            this.lasttimeLabel.Location = new System.Drawing.Point(7, 45);
            this.lasttimeLabel.Name = "lasttimeLabel";
            this.lasttimeLabel.Size = new System.Drawing.Size(35, 13);
            this.lasttimeLabel.TabIndex = 8;
            this.lasttimeLabel.Text = "Son Telemetri: ";
            // 
            // actualLasttimeLabel
            // 
            this.actualLasttimeLabel.AutoSize = true;
            this.actualLasttimeLabel.Location = new System.Drawing.Point(85, 45);
            this.actualLasttimeLabel.Name = "actualLasttimeLabel";
            this.actualLasttimeLabel.Size = new System.Drawing.Size(35, 13);
            this.actualLasttimeLabel.TabIndex = 8;
            this.actualLasttimeLabel.Text = "Bilinmiyor";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(7, 60);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(35, 13);
            this.stateLabel.TabIndex = 8;
            this.stateLabel.Text = "State: ";
            // 
            // actualStateLabel
            // 
            this.actualStateLabel.AutoSize = true;
            this.actualStateLabel.Location = new System.Drawing.Point(60, 60);
            this.actualStateLabel.Name = "actualStateLabel";
            this.actualStateLabel.Size = new System.Drawing.Size(35, 13);
            this.actualStateLabel.TabIndex = 8;
            this.actualStateLabel.Text = "Bilinmiyor";
            // 
            // takeoffAltBox
            // 
            this.takeoffAltBox.AutoSize = true;
            this.takeoffAltBox.Location = new System.Drawing.Point(7, 108);
            this.takeoffAltBox.Name = "takeoffAltBox";
            this.takeoffAltBox.Size = new System.Drawing.Size(30, 13);
            this.takeoffAltBox.TabIndex = 8;
            this.takeoffAltBox.Text = "5";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(41, 108);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Kalkış Yap";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(6, 78);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(165, 23);
            this.button4.TabIndex = 22;
            this.button4.Text = "Göreve Başla";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(6, 108);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(165, 23);
            this.button5.TabIndex = 23;
            this.button5.Text = "Uçuş İzlerini Temizle";

            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);

            // 
            // sendDataBtn
            // 
            /*this.sendDataBtn.Location = new System.Drawing.Point(10, 145);
            this.sendDataBtn.Name = "sendDataBtn";
            this.sendDataBtn.Size = new System.Drawing.Size(85, 23);
            this.sendDataBtn.TabIndex = 6;
            this.sendDataBtn.Text = "Handshake";
            this.sendDataBtn.UseVisualStyleBackColor = true;
            this.sendDataBtn.Click += new System.EventHandler(this.sendDataBtn_Click);*/
            // 
            // closePortBtn
            // 
            /*this.closePortBtn.Location = new System.Drawing.Point(9, 116);
            this.closePortBtn.Name = "closePortBtn";
            this.closePortBtn.Size = new System.Drawing.Size(167, 23);
            this.closePortBtn.TabIndex = 5;
            this.closePortBtn.Text = "Portu Kapat";
            this.closePortBtn.UseVisualStyleBackColor = true;
            this.closePortBtn.Click += new System.EventHandler(this.closePortBtn_Click);*/
            // 
            // portOpenBtn
            // 
            /*this.portOpenBtn.Location = new System.Drawing.Point(9, 87);
            this.portOpenBtn.Name = "portOpenBtn";
            this.portOpenBtn.Size = new System.Drawing.Size(167, 23);
            this.portOpenBtn.TabIndex = 4;
            this.portOpenBtn.Text = "Portu Aç";
            this.portOpenBtn.UseVisualStyleBackColor = true;
            this.portOpenBtn.Click += new System.EventHandler(this.portOpenBtn_Click);*/
            // 
            // modeCombobox
            // 
            this.modeCombobox.Anchor = this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
            this.modeCombobox.Location = new System.Drawing.Point(1055, 510);
            this.modeCombobox.Name = "modeCombobox";
            this.modeCombobox.Size = new System.Drawing.Size(175, 20);
            this.modeCombobox.TabIndex = 9;
            this.modeCombobox.DataSource = new string[] { "AUTO", "GUIDED", "ALT_HOLD", "STABILIZE", "LAND" };
            // 
            // setModeButton
            // 
            this.setModeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
            this.setModeButton.Location = new System.Drawing.Point(1055, 540);
            this.setModeButton.Name = "setModeButton";
            this.setModeButton.Size = new System.Drawing.Size(175, 23);
            this.setModeButton.TabIndex = 18;
            this.setModeButton.Text = "Mode Ayarla";
            this.setModeButton.UseVisualStyleBackColor = true;
            this.setModeButton.Click += new System.EventHandler(this.setModeButton_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(6, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Arm'la";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // bDisarm
            // 
            this.bDisarm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bDisarm.Location = new System.Drawing.Point(6, 48);
            this.bDisarm.Name = "bDisarm";
            this.bDisarm.Size = new System.Drawing.Size(165, 23);
            this.bDisarm.TabIndex = 20;
            this.bDisarm.Text = "Disarm'la";
            this.bDisarm.UseVisualStyleBackColor = true;
            this.bDisarm.Click += new System.EventHandler(this.bDisarm_Click);
            // 
            // baudRateBox
            // 
            /*this.baudRateBox.Location = new System.Drawing.Point(72, 42);
            this.baudRateBox.Name = "baudRateBox";
            this.baudRateBox.Size = new System.Drawing.Size(104, 20);
            this.baudRateBox.TabIndex = 3;
            this.baudRateBox.Text = "57600";*/
            // 
            // label2
            // 
            /*this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baud Rate";*/
            // 
            // label1
            // 
            /*this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port Adı";*/
            // 
            // sendPortNameBox
            // 
            /*this.sendPortNameBox.Location = new System.Drawing.Point(72, 19);
            this.sendPortNameBox.Name = "sendPortNameBox";
            this.sendPortNameBox.Size = new System.Drawing.Size(104, 20);
            this.sendPortNameBox.TabIndex = 0;
            this.sendPortNameBox.Text = SEND_PORT_NAME;*/
            // 
            // telLogBox
            // 
            this.telLogBox.Location = new System.Drawing.Point(628, 435);
            this.telLogBox.Name = "telLogBox";
            this.telLogBox.ReadOnly = true;
            this.telLogBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.telLogBox.Size = new System.Drawing.Size(420, 207);
            this.telLogBox.TabIndex = 10;
            this.telLogBox.Text = "";
            this.telLogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top)));
            // 
            // serialPort1
            // 
            //this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // serialPort2
            // 
            //this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(414, 662);
            this.trackBar1.Maximum = 180;
            this.trackBar1.Minimum = -180;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(227, 45);
            this.trackBar1.TabIndex = 12;
            this.trackBar1.Visible = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.Location = new System.Drawing.Point(705, 677);
            this.trackBar2.Maximum = 40;
            this.trackBar2.Minimum = -40;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(218, 45);
            this.trackBar2.TabIndex = 14;
            this.trackBar2.Visible = false;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // trackBar3
            // 
            this.trackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar3.Location = new System.Drawing.Point(401, 713);
            this.trackBar3.Maximum = 50;
            this.trackBar3.Minimum = -50;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(230, 45);
            this.trackBar3.TabIndex = 15;
            this.trackBar3.Visible = false;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // trackBar4
            // 
            this.trackBar4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar4.Location = new System.Drawing.Point(715, 713);
            this.trackBar4.Maximum = 150;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(218, 45);
            this.trackBar4.TabIndex = 16;
            this.trackBar4.Visible = false;
            this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // trackBar5
            // 
            this.trackBar5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar5.Location = new System.Drawing.Point(414, 764);
            this.trackBar5.Maximum = 180;
            this.trackBar5.Minimum = -180;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(227, 45);
            this.trackBar5.TabIndex = 17;
            this.trackBar5.Visible = false;
            this.trackBar5.Scroll += new System.EventHandler(this.trackBar5_Scroll);
            // 
            // loadRouteButton
            // 
            this.loadRouteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadRouteButton.Location = new System.Drawing.Point(6, 78);
            this.loadRouteButton.Name = "loadRouteButton";
            this.loadRouteButton.Size = new System.Drawing.Size(165, 23);
            this.loadRouteButton.TabIndex = 18;
            this.loadRouteButton.Text = "Rotayı Drone\'a Yükle";
            this.loadRouteButton.UseVisualStyleBackColor = true;
            this.loadRouteButton.Click += new System.EventHandler(this.loadRouteButton_Click);
            // 
            // trackBar6
            // 
            this.trackBar6.Location = new System.Drawing.Point(664, 764);
            this.trackBar6.Maximum = 30;
            this.trackBar6.Minimum = -30;
            this.trackBar6.Name = "trackBar6";
            this.trackBar6.Size = new System.Drawing.Size(218, 45);
            this.trackBar6.TabIndex = 19;
            this.trackBar6.Visible = false;
            this.trackBar6.Scroll += new System.EventHandler(this.trackBar6_Scroll);
            // 
            // propellerIndicator1
            // 
            this.propellerIndicator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.propellerIndicator1.Location = new System.Drawing.Point(1236, 476);
            this.propellerIndicator1.Name = "propellerIndicator1";
            this.propellerIndicator1.Size = new System.Drawing.Size(228, 355);
            this.propellerIndicator1.TabIndex = 24;
            this.propellerIndicator1.Text = "propellerIndicator1";
            // 
            // pfd1
            // 
            this.pfd1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pfd1.Location = new System.Drawing.Point(628, 9);
            this.pfd1.Name = "pfd1";
            this.pfd1.Size = new System.Drawing.Size(420, 420);
            this.pfd1.TabIndex = 13;
            this.pfd1.Text = "pfd1";
            // 
            // groupBox2
            // 
            //this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.bDisarm);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.takeoffAltBox);
            this.groupBox2.Location = new System.Drawing.Point(1055, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 170);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Görev";
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.bQuickSave);
            this.groupBox3.Controls.Add(this.loadRouteButton);
            this.groupBox3.Controls.Add(this.bQuickLoad);
            this.groupBox3.Location = new System.Drawing.Point(1055, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 150);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rota";
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // statusBox
            // 
            this.statusBox.Controls.Add(this.modeLabel);
            this.statusBox.Controls.Add(this.actualModeLabel);
            this.statusBox.Controls.Add(this.armLabel);
            this.statusBox.Controls.Add(this.actualArmLabel);
            this.statusBox.Controls.Add(this.lasttimeLabel);
            this.statusBox.Controls.Add(this.actualLasttimeLabel);
            //this.statusBox.Controls.Add(this.stateLabel);
            //this.statusBox.Controls.Add(this.actualStateLabel);
            this.statusBox.Location = new System.Drawing.Point(1055, 362);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(175, 140);
            this.statusBox.TabIndex = 27;
            this.statusBox.TabStop = false;
            this.statusBox.Text = "Durum Bilgisi";
            this.statusBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 831);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.telLogBox);
            //this.Controls.Add(this.propellerIndicator1);
            this.Controls.Add(this.trackBar6);
            this.Controls.Add(this.trackBar5);
            this.Controls.Add(this.trackBar4);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.pfd1);
            this.Controls.Add(this.trackBar1);
            //this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stateIndicator);
            this.Controls.Add(this.removePointButton);
            this.Controls.Add(this.addPointBeforeButton);
            this.Controls.Add(this.addPointAfterButton);
            this.Controls.Add(this.editPointButton);
            this.Controls.Add(this.pointPropView);
            this.Controls.Add(this.propListView);
            this.Controls.Add(this.gmap);

            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.modeCombobox);
            this.Controls.Add(this.setModeButton);
            this.Controls.Add(this.clearLogButton);

            this.Name = "Form1";
            this.Text = "İrtifa Yer İstasyonu";
            this.Load += new System.EventHandler(this.Form1_Load);
            //this.groupBox1.ResumeLayout(false);
            //this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.statusBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.ListView propListView;
        private System.Windows.Forms.ColumnHeader names;
        private System.Windows.Forms.ColumnHeader values;
        private System.Windows.Forms.ListView pointPropView;
        private System.Windows.Forms.ColumnHeader pointOrderColumn;
        private System.Windows.Forms.ColumnHeader latitudeColumn;
        private System.Windows.Forms.ColumnHeader longitudeColumn;
        private System.Windows.Forms.ColumnHeader altitudeColumn;
        private System.Windows.Forms.Button editPointButton;
        private System.Windows.Forms.Button addPointAfterButton;
        private System.Windows.Forms.Button addPointBeforeButton;
        private System.Windows.Forms.Button removePointButton;
        private System.Windows.Forms.Label stateIndicator;
        //private System.Windows.Forms.GroupBox groupBox1;
        //private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.TextBox sendPortNameBox;
        //private System.Windows.Forms.TextBox baudRateBox;
        //private System.Windows.Forms.Label label2;
        //private System.Windows.Forms.Button closePortBtn;
        //private System.Windows.Forms.Button portOpenBtn;
        //private System.Windows.Forms.Button sendDataBtn;
        //private System.Windows.Forms.Label portStatusLabel;
        private System.IO.Ports.SerialPort serialPort1;
        //private System.Windows.Forms.Label lastReceivedLabel;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.TrackBar trackBar1;
        private PFD pfd1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.Button loadRouteButton;
        private System.Windows.Forms.ColumnHeader speedColumn;
        private System.Windows.Forms.TrackBar trackBar6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        //private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.RichTextBox telLogBox;
        //private System.Windows.Forms.Button startMissionButton;
        private PropellerIndicator propellerIndicator1;
        private System.Windows.Forms.Button bQuickSave;
        private System.Windows.Forms.Button bQuickLoad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox statusBox;
        private System.Windows.Forms.ColumnHeader roleColumn;

        private System.Windows.Forms.Label armLabel; //only the text that says arm:
        private System.Windows.Forms.Label actualArmLabel; //the text that is actually informative
        private System.Windows.Forms.Label modeLabel; //again, only the text
        private System.Windows.Forms.Label actualModeLabel;
        private System.Windows.Forms.Button bDisarm;

        private System.Windows.Forms.Label lasttimeLabel; //same conventions
        private System.Windows.Forms.Label actualLasttimeLabel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Label actualStateLabel;

        private System.Windows.Forms.ComboBox modeCombobox;
        private System.Windows.Forms.Button setModeButton;
        private System.Windows.Forms.Button clearLogButton;

        private System.Windows.Forms.TextBox takeoffAltBox;
    }
}

