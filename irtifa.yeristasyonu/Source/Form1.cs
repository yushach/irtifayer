using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Drawing;

namespace irtifa
{
    public partial class Form1 : Form
    {

        const int ADDING_POLYGON = 0;
        const int BROWSING = 1;
        const int MODDING_POINT = 2;
        int state = BROWSING;

        Color ROUTE_COLOR = Color.Orange;

        string ihacomm = "/tmp/ihacomm";
        string ihatelem = "/tmp/ihatelem";
        string wpbildirim = "/tmp/wpbildirim";

        PlanOverlay newoverlay; //yeni rota eklemek için
        ContextMenu cm = new ContextMenu();
        GMapOverlay etcoverlay; //iha konumunu göstermek için

        PlanOverlay plan;

        int EditPointIndex; //düzenlenmekte olan noktanın idenksi

        private System.Windows.Forms.Timer timer1;

        int time = 0;

        //string fullDataReceived = "";

        double lastLat = 0.0; //son gelen enlem verisi
        double lastLng = 0.0; //son gelen boylam verisi

        System.Windows.Forms.Timer telemtimer;

        /*List<string> TelemetryKeys = new List<string> { "Lylp", "GYlp", "Lyns" };

        List<string> UsedTelemetryKeys = new List<string> { "AIRS", "VRTS", "HDNG", "ATTR", "ATTP", "ALT" };*/

        List<double> telemetry = new List<double>();

        List<string> KeysOnTheList = new List<string>();
        List<string> SpecialKeys = new List<string> { "AIRS", "VRTS", "HDNG", "ATTR", "ATTP", "ALT", "ARMED", "MODE", "LAT", "LNG", "LAST" };

        System.Diagnostics.Process coreprocess = new System.Diagnostics.Process();
        //bool CoreStarted = false;

        List<PointLatLng> TraversedRoute = new List<PointLatLng>();

        GMapOverlay routesOverlay = new GMapOverlay("routes");

        public Form1()
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            InitializeComponent();
            this.DoubleBuffered = true;


            newoverlay = new PlanOverlay(gmap, "new_temp_ovr");

            plan = new PlanOverlay(gmap, Consts.DEFAULT_OVERLAY_ID);

            GMapRoute TRoute = new GMapRoute(TraversedRoute, "traversedroute");
            TRoute.Stroke.Color = ROUTE_COLOR;
            routesOverlay.Routes.Add(TRoute);
            gmap.Overlays.Add(routesOverlay);

            etcoverlay = new GMapOverlay("misc");
            gmap.Overlays.Add(etcoverlay);

            //linux için düzenlemeler
            GMap.NET.MapProviders.GMapProvider.WebProxy = System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            if (File.Exists("Mono.Security.dll")){
                File.Delete("Mono.Security.dll");
            }
            //this.gmap.OnMapClicked += new GMap.NET.WindowsForms.MapClick(this.gmap_OnMapClick);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.Name = "MAIN";
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.Position = new GMap.NET.PointLatLng(41.0903, 29.0828);
            gmap.ShowCenter = false;
            gmap.DragButton = MouseButtons.Left;
            gmap.DisableFocusOnMouseEnter = true;

            InitTimer(); //sanırım artık kullanmıyoruz

            cm.MenuItems.Add("Sıfırdan Rota Çiz", new EventHandler(MenuClick));
            gmap.ContextMenu = cm;


            telemtimer = new System.Windows.Forms.Timer();
            telemtimer.Tick += new EventHandler(telemtimer_Tick);
            telemtimer.Interval = 900; // milisaniyede telemetri kontrolü
            telemtimer.Start();

        }

        //telemetri kontrollerini düzenleyen fonksiyon
        private void telemtimer_Tick(object sender, EventArgs e)
        {
            CheckTelemetry();
            CheckForWaypointUpdates();
            CheckMessages();
        }

        public void InitTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += timer1.Interval;
        }

        //rota noktalarını nokta göstericiye yüklemek için
        private void DisplayPointsInListView(PlanOverlay po)
        {
            pointPropView.Items.Clear();
            for (int i=0; i<po.points.Count; i++)
            {
                ListViewItem itemToAdd = new ListViewItem();
                itemToAdd.Text = "" + i;
                itemToAdd.SubItems.Add("" + po.points[i].pos.Lat);
                itemToAdd.SubItems.Add("" + po.points[i].pos.Lng);
                string AltitudeString = "Ayarlanmamış";
                string SpeedString = "Ayarlanmamış";
                if (po.points[i].alt != Consts.NULL_ALT)
                {
                    AltitudeString = po.points[i].alt.ToString();
                }
                if (po.points[i].speed != Consts.NULL_ALT)
                {
                    SpeedString = po.points[i].speed.ToString();
                }
                itemToAdd.SubItems.Add(AltitudeString);
                itemToAdd.SubItems.Add(SpeedString);
                itemToAdd.SubItems.Add(po.points[i].role);
                pointPropView.Items.Add(itemToAdd);
            }
        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            //Console.WriteLine("**********\nMARKER TIKLANDI\n**********");
        }

        private void gmap_OnMapClick(PointLatLng pointClick, MouseEventArgs e)
        {

            PointLatLng location = gmap.FromLocalToLatLng(e.X, e.Y);

            if (state == ADDING_POLYGON)
            {
                if (newoverlay.points.Count == 0)
                {
                    gmap.Overlays.Add(newoverlay.GetOverlay());
                }
                newoverlay.points.Add(new PlanPoint(location));
                newoverlay.Synchronize();
                newoverlay.UpdateMap();
            }

        }

        private void gmap_ClickHandler(Object sender, EventArgs e)
        {
            MouseEventArgs mea = (MouseEventArgs)e;
            Point mousepos = mea.Location;
            PointLatLng mappos = gmap.FromLocalToLatLng(mousepos.X, mousepos.Y);
            gmap_OnMapClick(mappos, mea);
        }

        //haritada son konumu göstermek için
        private void ShowCurrentLocationOnMap()
        {
            etcoverlay.Markers.Clear();
            GMapMarker marker = new GMapMarkerImage(new PointLatLng(lastLat, lastLng), new Bitmap("red.png"));
            etcoverlay.Markers.Add(marker);
        }

        //haritada düzenleme yaparken düzenleme yapıldığını belirtmek için
        private void EnableIndicator(string msg)
        {
            stateIndicator.Visible = true;
            stateIndicator.Text = msg;
        }

        //haritada düzenleme bitince belirteci kapatmak için
        private void DisableIndicator()
        {
            stateIndicator.Visible = false;
        }

        //rota eklemek için sağ tıklandığında açılacak menüyü düzenler
        private void MenuClick(object sender, EventArgs e)
        {
            if (state == BROWSING)
            {
                state = ADDING_POLYGON;

                EnableIndicator("ROTA EKLENİYOR");

                cm.MenuItems[0].Text = "Rotayı Tamamla";
                newoverlay.points.Clear();
            }
            else
            {
                Console.WriteLine("CAME HERE \n\n\n");
                cm.MenuItems[0].Text = "Sıfırdan Rota Çiz";
                state = BROWSING;
                DisableIndicator();
                plan.points = newoverlay.points;
                plan.Synchronize();
                gmap.Overlays.Remove(newoverlay.GetOverlay());
                UpdateMap();
                DisplayPointsInListView(plan);
            }
        }

        private void gmap_OnMapDrag()
        {
            //Console.WriteLine("sürükleniyor");

        }

        //harita üzerinde değişiklik yapılınca değişiklikleri güncellemek için
        private void UpdateMap()
        {
            foreach (GMapOverlay ovr in gmap.Overlays)
            {
                ovr.IsVisibile = false;
                ovr.IsVisibile = true;
            }
        }


        //fare tıklaması ile noktaları düzenlemek için
        private void gmap_MouseDown(object sender, MouseEventArgs e)
        {
            //Switch to editing mode if conditions are met, handle editing in another method
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < plan.points.Count; i++)
                {
                    PointLatLng p = plan.points[i].pos;
                    double px = gmap.FromLatLngToLocal(p).X;
                    double py = gmap.FromLatLngToLocal(p).Y;
                    double dist = Math.Sqrt(Math.Pow(px - e.X, 2) + Math.Pow(py - e.Y, 2));
                    if (dist < 14)
                    {
                        gmap.CanDragMap = false;
                        state = MODDING_POINT;
                        EditPointIndex = i;
                        EnableIndicator("NOKTA KONUMLANDIRILIYOR");
                        UpdateMap();
                        break;
                    }
                }
            }
        }

        //fare tıklaması kaldırılınca nokta konumlandırmayı bitirmek için
        private void gmap_MouseUp(object sender, MouseEventArgs e)
        {
            //nokta konumlandırmayı sonlandır
            if (e.Button == MouseButtons.Left)
            {
                if (state == MODDING_POINT)
                {
                    state = BROWSING;
                    DisableIndicator();
                    gmap.CanDragMap = true;

                    plan.points[EditPointIndex].pos = gmap.FromLocalToLatLng(e.X, e.Y);
                    plan.points[EditPointIndex].UpdateMarker(plan.points.Count);
                    plan.Synchronize();
                    UpdateMap();
                    DisplayPointsInListView(plan);
                    Console.WriteLine(gmap.Overlays.Count);
                }
            }
        }

        //nokta kaldırma düğmesine
        private void removePointButton_Click(object sender, EventArgs e)
        {

            try
            {
                int pointIndex = pointPropView.Items.IndexOf(pointPropView.SelectedItems[0]);
                RemovePointFromPolygon(pointIndex);
            }
            catch (Exception) { }
        }

        //listeden nokta kaldırmak için
        private void RemovePointFromPolygon(int pointIndex)
        {
            plan.points.RemoveAt(pointIndex);
            plan.Synchronize();
            DisplayPointsInListView(plan);
            UpdateMap();
        }


        //noktadan sonra yeni düğme eklemek için
        private void addPointAfterButton_Click(object sender, EventArgs e)
        {
            int MainIndex = 0;
            int NextIndex;
            try
            {
                MainIndex = pointPropView.Items.IndexOf(pointPropView.SelectedItems[0]);
            }
            catch (Exception)
            {
                Console.WriteLine("CATCH NO 2");
                WriteLog("Hata 2");
            }
            if (MainIndex + 1 >= pointPropView.Items.Count)
            {
                NextIndex = 0;
            }
            else
            {
                NextIndex = MainIndex + 1;
            }

            try
            {
                double NewAlt = CustomOverlay.NULL_ALT;
                double NewSpd = CustomOverlay.NULL_ALT;
                string GetMainAlt = pointPropView.Items[MainIndex].SubItems[3].Text;
                string GetNextAlt = pointPropView.Items[NextIndex].SubItems[3].Text;
                string GetMainSpd = pointPropView.Items[MainIndex].SubItems[4].Text;
                string GetNextSpd = pointPropView.Items[NextIndex].SubItems[4].Text;
                if (GetMainAlt != "Ayarlanmamış" & GetNextAlt != "Ayarlanmamış")
                {
                    NewAlt = (Convert.ToDouble(GetMainAlt) + Convert.ToDouble(GetNextAlt)) / 2;
                }
                if (GetMainSpd != "Ayarlanmamış" & GetNextSpd != "Ayarlanmamış")
                {
                    NewSpd = (Convert.ToDouble(GetMainSpd) + Convert.ToDouble(GetNextSpd)) / 2;
                }
                double selectedLat = Convert.ToDouble(pointPropView.Items[MainIndex].SubItems[1].Text);
                double nextLat = Convert.ToDouble(pointPropView.Items[NextIndex].SubItems[1].Text);
                double selectedLng = Convert.ToDouble(pointPropView.Items[MainIndex].SubItems[2].Text);
                double nextLng = Convert.ToDouble(pointPropView.Items[NextIndex].SubItems[2].Text);
                double newLat = (selectedLat + nextLat) / 2;
                double newLng = (selectedLng + nextLng) / 2;
                //lastSelectedOverlay.AddPointAfterIndex(new PointLatLng(newLat, newLng), MainIndex, NewAlt, NewSpd);

                PlanPoint newpt = new PlanPoint(new PointLatLng(newLat, newLng));
                newpt.alt = NewAlt;
                newpt.speed = NewSpd;

                plan.AddPointAfterIndex(newpt, MainIndex);
                UpdateMap();
                DisplayPointsInListView(plan);
            }
            catch (Exception)
            {
                Console.WriteLine("CATCH NO 13");
                WriteLog("Nokta eklenemedi :13");
            }

        }

        //noktadan önce yeni nokta eklemek için
        private void addPointBeforeButton_Click(object sender, EventArgs e)
        {
            int MainIndex = 1;
            int NextIndex;
            try
            {
                MainIndex = pointPropView.Items.IndexOf(pointPropView.SelectedItems[0]);
            }
            catch (Exception)
            {
                Console.WriteLine("CATCH NO 3");
                WriteLog("Hata 3");
            }
            if (MainIndex == 0)
            {
                NextIndex = plan.points.Count - 1;
            }
            else
            {
                NextIndex = MainIndex - 1;
            }

            try
            {
                double NewAlt = CustomOverlay.NULL_ALT;
                double NewSpd = CustomOverlay.NULL_ALT;
                string GetMainAlt = pointPropView.Items[MainIndex].SubItems[3].Text;
                string GetNextAlt = pointPropView.Items[NextIndex].SubItems[3].Text;
                string GetMainSpd = pointPropView.Items[MainIndex].SubItems[4].Text;
                string GetNextSpd = pointPropView.Items[NextIndex].SubItems[4].Text;
                if (GetMainAlt != "Ayarlanmamış" & GetNextAlt != "Ayarlanmamış")
                {
                    NewAlt = (Convert.ToDouble(GetMainAlt) + Convert.ToDouble(GetNextAlt)) / 2;
                }
                if (GetMainSpd != "Ayarlanmamış" & GetNextSpd != "Ayarlanmamış")
                {
                    NewSpd = (Convert.ToDouble(GetMainSpd) + Convert.ToDouble(GetNextSpd)) / 2;
                }
                double selectedLat = Convert.ToDouble(pointPropView.Items[MainIndex].SubItems[1].Text);
                double nextLat = Convert.ToDouble(pointPropView.Items[NextIndex].SubItems[1].Text);
                double selectedLng = Convert.ToDouble(pointPropView.Items[MainIndex].SubItems[2].Text);
                double nextLng = Convert.ToDouble(pointPropView.Items[NextIndex].SubItems[2].Text);
                double newLat = (selectedLat + nextLat) / 2;
                double newLng = (selectedLng + nextLng) / 2;
                //lastSelectedOverlay.AddPointBeforeIndex(new PointLatLng(newLat, newLng), MainIndex, NewAlt, NewSpd);

                PlanPoint newpt = new PlanPoint(new PointLatLng(newLat, newLng));
                newpt.alt = NewAlt;
                newpt.speed = NewSpd;

                plan.AddPointBeforeIndex(newpt, MainIndex);

                UpdateMap();
                DisplayPointsInListView(plan);
            }
            catch (Exception)
            {
                Console.WriteLine("CATCH NO 12");
                WriteLog("Nokta eklenemedi :12");
            }

        }

        //nokta düzenlemek için
        private void editPointButton_Click(object sender, EventArgs e)
        {
            try
            {
                int PointIndex = pointPropView.Items.IndexOf(pointPropView.SelectedItems[0]);
                if (PointIndex >= 0)
                {
                    Form EditForm0 = new EditForm(PointIndex, plan);
                    EditForm0.FormClosed += EditForm0_FormClosed;
                    EditForm0.Show();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("CATCH NO 11");
                WriteLog("Düzenleme başarısız :11");
            }
        }

        //düzenleme penceresi kapatıldığında nokta özelliklerini günceller
        public void EditForm0_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisplayPointsInListView(plan);
            plan.Synchronize();
            UpdateMap();
        }

        //son telemetri zamanını günceller
        void SetLastTimeLabel(string newtime)
        {
            actualLasttimeLabel.Text = newtime;
        }

        //arm komutunu gönderir
        void SendArmCommand()
        {
            string msg = "CMD_ARM\nEND";
            File.WriteAllText(ihacomm, msg);
            WriteLog("Arm komutu gönderildi");
        }

        //disarm komutunu gönderir
        void SendDisarmCommand()
        {
            string msg = "CMD_DISARM\nEND";
            File.WriteAllText(ihacomm, msg);
            WriteLog("Disarm komutu gönderildi");
        }

        //rotayı gönderir
        private void SendRoute()
        {
            try
            {
                List<string> lines = new List<string>();
                lines.Add("WPS");
                for (int i = 0; i < plan.points.Count; i++)
                {
                    double sendLat = plan.points[i].pos.Lat;
                    double sendLng = plan.points[i].pos.Lng;
                    double sendAlt = plan.points[i].alt;
                    double sendSpd = plan.points[i].speed;
                    string sendRole = plan.points[i].role;
                    string towrite = sendLat + "," + sendLng + "," + sendAlt + "," + sendSpd + "," + sendRole;
                    lines.Add(towrite);
                }
                lines.Add("END");
                
                File.WriteAllLines(ihacomm, lines);
                WriteLog("Rota İHA'ya gönderildi");
            }
            catch (Exception expt)
            {
                WriteLog("Rota gönderilemedi");
                Console.WriteLine("FAILED TO SEND ROUTE");
            }
        }

        //telemetri gösteren bölümdeki keyleri günceller
        private void RefreshKeysOnTheListViewList()
        {
            KeysOnTheList.Clear();
            foreach (ListViewItem lvi in propListView.Items)
            {
                KeysOnTheList.Add(lvi.Text);
            }
        }

        //arm durumunu günceller
        void SetArmLabels(string armstatus)
        {
            if (armstatus.Equals("true")) {
                actualArmLabel.Text = "EVET";
                actualArmLabel.ForeColor = Color.Green;
            }
            else
            {
                actualArmLabel.Text = "HAYIR";
                actualArmLabel.ForeColor = Color.Red;
            }
        }

        //modu günceller
        void SetModeLabel(string mode)
        {
            actualModeLabel.Text = mode;
        }

        //telemetri listesini alıp işler
        private void TakeCareOfTelemetry(List<string> received)
        {
            double gotlat = -9999;
            double gotlng = -9999;
            //PrintTelForDebugPurposes(received);
            for (int index = 0; index < received.Count; index++)
            {
                try
                {
                    if (received[index].Contains(":"))
                    { //başlık koyarsak onlar işlenmeye çalışılmasın diye : içeren elemanları al
                        string[] pair = received[index].Split((char)58);
                        //Console.WriteLine(pair[0]+" "+pair[1]);

                        float floatVal;
                        //özel işlevi olan değerleri hallet
                        if (SpecialKeys.Contains(pair[0]))
                        {
                            //WriteLog("pair " + pair[0] + " " + pair[1]);
                            switch (pair[0])
                            {
                                case ("AIRS"):
                                    floatVal = float.Parse("" + pair[1]);
                                    pfd1.UpdateAirSpeed(floatVal, false); break;
                                case ("VRTS"):
                                    floatVal = float.Parse("" + pair[1]);
                                    pfd1.UpdateVerticalS(floatVal, false); break;
                                case ("HDNG"):
                                    floatVal = float.Parse("" + pair[1]);
                                    pfd1.UpdateHeading(floatVal, false); break;
                                case ("ATTP"):
                                    floatVal = float.Parse("" + pair[1]);
                                    pfd1.UpdateAttPit(floatVal, false); break;
                                case ("ATTR"):
                                    floatVal = float.Parse("" + pair[1]);
                                    pfd1.UpdateAttRoll(floatVal, false); break;
                                case ("ALT"):
                                    floatVal = float.Parse("" + pair[1]);
                                    pfd1.UpdateAltitude(floatVal, false); break;
                                case ("LAT"):
                                    floatVal = float.Parse("" + pair[1]);
                                    lastLat = floatVal;
                                    gotlat = floatVal;
                                    ShowCurrentLocationOnMap();
                                    break;
                                case ("LNG"):
                                    floatVal = float.Parse("" + pair[1]);
                                    lastLng = floatVal;
                                    gotlng = floatVal;
                                    ShowCurrentLocationOnMap();
                                    break;
                                case ("ARMED"):
                                    SetArmLabels(pair[1]);
                                    break;
                                case ("MODE"):
                                    SetModeLabel(pair[1]);
                                    break;
                                case ("LAST"):
                                    SetLastTimeLabel(pair[1]);
                                    break;
                            }
                        }

                        if (KeysOnTheList.Contains(pair[0]))
                        {
                            propListView.Items[KeysOnTheList.IndexOf(pair[0])].SubItems[1].Text = pair[1];
                        }
                        else
                        {
                            if (!pair[0].Equals("ARMED") & !pair[0].Equals("MODE") & !pair[0].Equals("LAST"))
                            {
                                ListViewItem newTel = new ListViewItem();
                                newTel.Text = pair[0];
                                newTel.SubItems.Add(pair[1]);
                                propListView.Items.Add(newTel);
                                //WriteLog("not on list");
                            }
                        }
                    }
                }
                catch (Exception) { Console.WriteLine("CATCH 20"); WriteLog("CATCH 20"); }

            }
            RefreshKeysOnTheListViewList();
            pfd1.Invalidate();

            if (!gotlat.Equals(-9999) && !gotlng.Equals(-9999))
            {
                TraversedRoute.Add(new PointLatLng(gotlat, gotlng));
                routesOverlay.Routes.RemoveAt(0);
                GMapRoute newroute = new GMapRoute(TraversedRoute, "traversedroute");
                newroute.Stroke.Color = ROUTE_COLOR;
                routesOverlay.Routes.Add(newroute);
                UpdateMap();
            }
        }

        //loga ekleme yap
        protected void WriteLog(string message)
        {
            telLogBox.AppendText(message + "\n");
            telLogBox.SelectionStart = telLogBox.Text.Length;
            telLogBox.ScrollToCaret();
        }

        //rotayı kaydet
        //keyler: Order,Lat,Long,Alt,Spd
        string quickfilename = "SavedPath.txt";
        private void bQuickSave_Click(object sender, EventArgs e)
        {
            List<string> linestosave = new List<string>();
            for (int i = 0; i < plan.points.Count; i++)
            {
                PointLatLng p = plan.points[i].pos;
                linestosave.Add(i + "," + p.Lat + "," + p.Lng + "," + plan.points[i].alt + "," + plan.points[i].speed + "," + plan.points[i].role);
            }
            File.WriteAllLines(quickfilename, linestosave.ToArray());
            WriteLog("Plan kaydedildi");
        }

        //kaydedilmiş rotayı yükle
        private void bQuickLoad_Click(object sender, EventArgs e)
        {
            //yeni overlay oluştur
            PlanOverlay newplan = new PlanOverlay(gmap, Consts.DEFAULT_OVERLAY_ID);

            string[] lines = File.ReadAllLines(quickfilename);
            foreach (string line in lines)
            {
                string[] prms = line.Split((char)44);
                PlanPoint newpt = new PlanPoint(new PointLatLng(Convert.ToDouble(prms[1]), Convert.ToDouble(prms[2])));
                newpt.index = Convert.ToInt32(prms[0]);
                newpt.alt = Convert.ToDouble(prms[3]);
                newpt.speed = Convert.ToDouble(prms[4]);
                newpt.role = prms[5];
                newplan.points.Add(newpt);
            }

            plan = newplan;
            plan.Synchronize();

            foreach (GMapOverlay o in gmap.Overlays)
            {
                if (o.Id != null && o.Id.Equals(Consts.DEFAULT_OVERLAY_ID))
                {
                    gmap.Overlays.Remove(o);
                    break;
                }
            }
            gmap.Overlays.Add(plan.GetOverlay());
            UpdateMap();
            DisplayPointsInListView(plan);
        }

        //waypointlerde değişiklik varsa haritayı güncelle
        void CheckForWaypointUpdates()
        {
            try {
                string[] lines = File.ReadAllLines(wpbildirim);

                if (lines[lines.Length - 1].Equals("END"))
                {
                    List<PlanPoint> newplan = new List<PlanPoint>();
                    for(int i=0; i<lines.Length-1; i++)
                    {
                        string[] prms = lines[i].Split(',');
                        PlanPoint newpt = new PlanPoint();
                        newpt.pos.Lat = Convert.ToDouble(prms[0]);
                        newpt.pos.Lng = Convert.ToDouble(prms[1]);
                        newpt.alt = Convert.ToDouble(prms[2]);
                        newpt.speed = Convert.ToDouble(prms[3]);
                        newpt.role = prms[4];
                        newplan.Add(newpt);
                    }
                    plan.points = newplan;
                    plan.Synchronize();
                    UpdateMap();
                    DisplayPointsInListView(plan);
                    WriteLog("Rota noktaları güncellendi");
                    File.WriteAllText(wpbildirim, "");
                }
            } catch (Exception e)

            {
               
            }

        }

        //telemetri verilerini kontrol et
        void CheckTelemetry()
        {

            try
            {
                string[] lines = File.ReadAllLines(ihatelem);
                if (lines[lines.Length-1].Equals("END"))
                {
                    //WriteLog("RECEIVED TELEMETRY");
                    List<string> strlist = lines.ToList<string>();
                    strlist.RemoveAt(strlist.Count - 1);
                    TakeCareOfTelemetry(strlist);
                    //WriteLog("got telem");
                    File.WriteAllText(ihatelem, "");
                }
                else
                {
                    //bişey yok
                }
            }
            catch (Exception ignore) {
                //WriteLog("Telemetri okunamadı");
            }
        }

        //mesajları kontrol et, şimdilik kullanılmıyor
        void CheckMessages()
        {
            try
            {
                string[] lines = File.ReadAllLines("/tmp/ihamsg");
                if (lines[lines.Length - 1].Equals("END"))
                {
                    //WriteLog("telemetri geldi");
                    List<string> strlist = lines.ToList<string>();
                    //WriteLog(strlist[0]);
                    File.WriteAllText("/tmp/ihamsg", "");
                }
            }catch(Exception ignore) {}
        }

        //görev başlama komutu
        void SendStartMission()
        {
            string msg = "CMD_START\nEND";
            File.WriteAllText(ihacomm, msg);
            WriteLog("Başlama komutu gönderildi");
        }

        //mode ayarlama komutu
        void SendSetMode()
        {
            string msg = "CMD_MODE\n"+modeCombobox.Text+"\nEND\n";
            File.WriteAllText(ihacomm, msg);
            WriteLog("Mod komutu gönderildi: "+modeCombobox.Text);
        }

        //kalkış komutu
        void SendTakeoffCommand()
        {
            string msg = "CMD_TAKEOFF\n" + takeoffAltBox.Text + "\nEND\n";
            File.WriteAllText(ihacomm, msg);
            WriteLog("Kalkış komutu gönderildi");
        }

        //trackbarlar primary flight displayi test etmek için koyulup testlerden sonra etkisiz hale getirilmiştir
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pfd1.UpdateAttRoll((float)(trackBar1.Value));
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pfd1.UpdateAttPit((float)(trackBar2.Value));
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            pfd1.UpdateAirSpeed((float)trackBar3.Value);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            pfd1.UpdateAltitude((float)trackBar4.Value);
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            pfd1.UpdateHeading((float)trackBar5.Value);
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            pfd1.UpdateVerticalS((float)(trackBar6.Value / 10.0f));
        }

        //rotayı ihaya yükleme düğmesi
        private void loadRouteButton_Click(object sender, EventArgs e)
        {
            SendRoute();
        }

        //kalkış komutu düğmesi
        private void button3_Click(object sender, EventArgs e)
        {
            SendTakeoffCommand();
        }

        //arm komutu düğmesi
        private void button2_Click(object sender, EventArgs e)
        {
            SendArmCommand();
        }

        //disarm komutu düğmesi
        private void bDisarm_Click(object sender, EventArgs e)
        {
            SendDisarmCommand();
        }

        //görev başlama komutu düğmesi
        private void button4_Click(object sender, EventArgs e)
        {
            SendStartMission();
        }

        //mode ayarlama düğmesi
        private void setModeButton_Click(object sender, EventArgs e)
        {
            SendSetMode();
        }

        //log kutusunu temizleme düğmesi
        private void clearLogButton_Click(object sender, EventArgs e)
        {
            telLogBox.Text = "";
        }

        //alınan yol izlerini temizleme düğmesi
        private void button5_Click(object sender, EventArgs e)
        {
            routesOverlay.Routes.RemoveAt(0);
            TraversedRoute.Clear();
            GMapRoute r = new GMapRoute(TraversedRoute, "traversedroute");
            r.Stroke.Color = ROUTE_COLOR;
            routesOverlay.Routes.Add(r);
        }

    }
}
