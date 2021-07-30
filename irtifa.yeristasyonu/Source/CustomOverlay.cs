/*
 * 
 * eski versyon, artık kullanılmıyor
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace irtifa
{
    public class CustomOverlay
    {
        public GMapControl gmap;
        public GMapOverlay overlay;
        public string name;

        public const GMarkerGoogleType PIN_TYPE = GMarkerGoogleType.blue;
        public const GMarkerGoogleType STARTING_PIN_TYPE = GMarkerGoogleType.green;
        public const GMarkerGoogleType ENDING_PIN_TYPE = GMarkerGoogleType.red;
        public const double NULL_ALT = -41234;

        public List<double> altitudes;
        public List<double> speeds;
        public CustomOverlay(GMapControl gmapname, String name0)
        {
            gmap = gmapname;
            overlay = new GMapOverlay(name0);
            overlay.Polygons.Add(new GMapPolygon(new List<PointLatLng>(), name0 + "_POL0"));
            name = name0;
            altitudes = new List<double>();
            speeds = new List<double>();
        }

        public void AddPoint(PointLatLng pnt)
        {
            overlay.Polygons[0].Points.Add(pnt);
            GMarkerGoogleType PinType = ENDING_PIN_TYPE;
            if (overlay.Markers.Count == 0)
            {
                PinType = STARTING_PIN_TYPE;
            }
            overlay.Markers.Add(new GMarkerGoogle(pnt, PinType));
            if (overlay.Markers.Count > 2)
            {
                PointLatLng getPos = overlay.Markers[overlay.Markers.Count - 2].Position;
                overlay.Markers[overlay.Markers.Count - 2] = new GMarkerGoogle(getPos, PIN_TYPE);
            }
            altitudes.Add(NULL_ALT);
            speeds.Add(NULL_ALT);
        }

        public void AddPointAfterIndex(PointLatLng pnt, int index, double altit = NULL_ALT, double speed = NULL_ALT)
        {
            overlay.Polygons[0].Points.Add(new PointLatLng(0, 0));
            altitudes.Add(NULL_ALT);
            speeds.Add(NULL_ALT);
            for (int i = overlay.Polygons[0].Points.Count - 1; i > index; i -= 1)
            {
                overlay.Polygons[0].Points[i] = overlay.Polygons[0].Points[i - 1];
                altitudes[i] = altitudes[i - 1];
                speeds[i] = speeds[i - 1];

            }
            overlay.Polygons[0].Points[index + 1] = pnt;
            altitudes[index + 1] = altit;
            speeds[index + 1] = speed;
            UpdateMarkers();
        }

        public void UpdateMarkers()
        {
            overlay.Markers.Clear();
            foreach (PointLatLng pnt in overlay.Polygons[0].Points)
            {
                GMarkerGoogleType PinType = PIN_TYPE;
                if (overlay.Polygons[0].Points.IndexOf(pnt) == 0)
                {
                    PinType = STARTING_PIN_TYPE;
                }
                else if (overlay.Polygons[0].Points.IndexOf(pnt) == overlay.Polygons[0].Points.Count - 1)
                {
                    PinType = ENDING_PIN_TYPE;
                }
                overlay.Markers.Add(new GMarkerGoogle(pnt, PinType));
            }
        }

        public void AddPointBeforeIndex(PointLatLng pnt, int index, double altit = NULL_ALT, double speed = NULL_ALT)
        {
            overlay.Polygons[0].Points.Add(new PointLatLng(0, 0));
            altitudes.Add(NULL_ALT);
            speeds.Add(NULL_ALT);
            for (int i = overlay.Polygons[0].Points.Count - 1; i > index - 1; i -= 1)
            {
                if (i - 1 >= 0)
                {
                    overlay.Polygons[0].Points[i] = overlay.Polygons[0].Points[i - 1];
                    altitudes[i] = altitudes[i - 1];
                    speeds[i] = speeds[i - 1];
                }
            }
            overlay.Polygons[0].Points[index] = pnt;
            altitudes[index] = altit;
            speeds[index] = speed;
            UpdateMarkers();
        }

        public void UpdatePins()
        {
            for (int i = 0; i < overlay.Polygons[0].Points.Count; i++)
            {
                overlay.Markers[i].Position = overlay.Polygons[0].Points[i];
            }
        }

        public void UpdateMap()
        {
            foreach (GMapOverlay ovr in gmap.Overlays)
            {
                ovr.IsVisibile = false;
                ovr.IsVisibile = true;
            }
        }

    }
}
