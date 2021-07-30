using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;

namespace irtifa
{
    public class Consts
    {
        //sabit değerler
        public const GMarkerGoogleType DEFAULT_PIN_TYPE = GMarkerGoogleType.blue;
        public const GMarkerGoogleType STARTING_PIN_TYPE = GMarkerGoogleType.green;
        public const GMarkerGoogleType ENDING_PIN_TYPE = GMarkerGoogleType.red;
        public const GMarkerGoogleType SPECIAL_PIN_TYPE = GMarkerGoogleType.yellow;
        public const double NULL_ALT = -41234;
        public const string DEFAULT_OVERLAY_ID = "default_overlay";
    }

    public class PlanPoint
    {
        public int index; //listedeki indeks, bunun yerine doğrudan liste indeksi kullanılıyor
        public PointLatLng pos; //konum
        public double speed; //hız
        public double alt; //yükseklik
        public string role = "NORMAL"; //rol, şimdilik kullanılmıyor
        public GMapMarker marker; //marker

        //constructor'lar
        public PlanPoint() { }

        public PlanPoint(PointLatLng location, int setindex)
        {
            index = setindex;
            marker = new GMarkerGoogle(pos, Consts.DEFAULT_PIN_TYPE);
        }

        public PlanPoint(PointLatLng location)
        {
            pos = location;
            marker = new GMarkerGoogle(pos, Consts.DEFAULT_PIN_TYPE);
        }

        //role göre marker belirleme
        public void UpdateMarker(int ListSize)
        {
            if (index == 0)
            {
                marker = new GMarkerGoogle(pos, Consts.STARTING_PIN_TYPE);
            }
            else if (index == ListSize - 1)
            {
                marker = new GMarkerGoogle(pos, Consts.ENDING_PIN_TYPE);
            }
            else
            {
                marker = new GMarkerGoogle(pos, Consts.DEFAULT_PIN_TYPE);
            }

            if (!role.Equals("NORMAL"))
            {
                if (index > 0 && index < ListSize - 1)
                {
                    marker = new GMarkerGoogle(pos, Consts.SPECIAL_PIN_TYPE);
                }
            }
        }
    }

    public class PlanOverlay
    {
        Color PLAN_COLOR = Color.Blue;

        //ilgili harita ve overlay
        public GMapControl gmap;
        private GMapOverlay overlay;

        //planın kendisi
        public List<PlanPoint> points;

        public PlanOverlay(GMapControl setgmap, string name)
        {
            gmap = setgmap;
            overlay = new GMapOverlay(name);
            overlay.Id = name;
            points = new List<PlanPoint>();
        }

        public GMapOverlay GetOverlay() { return overlay; }

        public void AppendPoint(PlanPoint pt)
        {
            points.Add(pt);
            Synchronize();
        }

        public void AddPointBeforeIndex(PlanPoint pt, int index)
        {
            points.Insert(index, pt);
            Synchronize();
        }

        public void AddPointAfterIndex(PlanPoint pt, int index)
        {
            points.Insert(index + 1, pt);
            Synchronize();
        }

        //kendi plan formatımızı GMap'in görüntüleyebileceği şekilde formatlamak için
        public void Synchronize()
        {
            foreach (GMapOverlay gmo in gmap.Overlays)
            {
                //önceki rota overlayini haritadan kaldır
                string id = gmo.Id;
                if (id != null)
                {
                    Console.WriteLine("Overlay with id: " + id);
                }
                else
                {
                    Console.WriteLine("Unidentifiable overlay");
                }

                if (id != null && id.Equals(Consts.DEFAULT_OVERLAY_ID))
                {
                    gmap.Overlays.Remove(gmo);
                    break;
                }
            }

            overlay.Polygons.Clear();

            //overlay.Routes.Clear();

            overlay.Markers.Clear();

            List<PointLatLng> OverlayPoints = new List<PointLatLng>();
            List<GMapMarker> OverlayMarkers = new List<GMapMarker>();

            //indeksleri güncelle
            int indexing = 0;
            foreach (PlanPoint pt in points)
            {
                pt.index = indexing; indexing++;
                pt.UpdateMarker(points.Count);
                OverlayPoints.Add(pt.pos);
                OverlayMarkers.Add(pt.marker);
            }

            overlay = new GMapOverlay();
            overlay.Polygons.Add(new GMapPolygon(OverlayPoints, "default_pol"));
            
            foreach (GMapMarker marker in OverlayMarkers)
            {
                overlay.Markers.Add(marker);
            }
            SynchronizePinPositions();
            overlay.Id = Consts.DEFAULT_OVERLAY_ID;
            gmap.Overlays.Add(overlay);
        }

        //marker konumlarını noktalarla eşle
        public void SynchronizePinPositions()
        {
            for (int i = 0; i < points.Count; i++)
            {
                //overlay.Markers[i].Position = overlay.Polygons[0].Points[i];
                points[i].marker.Position = points[i].pos;
            }
        }

        public void UpdateMap()
        {
            overlay.IsVisibile = false;
            overlay.IsVisibile = true;
        }
    }
}
