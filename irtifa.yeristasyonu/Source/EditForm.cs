using System;
using GMap.NET;

using System.Windows.Forms;

namespace irtifa
{
    public partial class EditForm : Form
    {
        int index; //düzenlenmekte olan noktanın listedeki indeksi
        PlanOverlay po; //noktanın bulunduğu overlay
        PlanPoint pt; //noktanın verileri
        public EditForm(int SetIndex, PlanOverlay plan)
        {
            po = plan;
            index = SetIndex;
            InitializeComponent();
            pt = plan.points[SetIndex];
            //nokta özelliklerini kutulara yaz
            latitudeTextBox.Text = pt.pos.Lat.ToString();
            longitudeTextBox.Text = pt.pos.Lng.ToString();
            string AltValueString = pt.alt.ToString();
            string SpdValueString = pt.speed.ToString();
            //hız ve yükseklik belirlenmemişse 0 yaz
            if (Convert.ToDouble(AltValueString) == CustomOverlay.NULL_ALT)
            {
                altitudeTextBox.Text = "0";
            }
            else
            {
                altitudeTextBox.Text = AltValueString;
            }
            if (Convert.ToDouble(SpdValueString) == CustomOverlay.NULL_ALT)
            {
                speedBox.Text = "0";
            }
            else
            {
                speedBox.Text = SpdValueString;
            }

        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            //rol şimdilik kullanılmıyor
            if (!pt.role.Equals("NORMAL"))
            {
                roleCombobox.SelectedIndex = 1;
            }
        }

        //bir şey yapma
        private void cancelEditButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //kaydet ve çık
        private void applyEditButton_Click(object sender, EventArgs e)
        {
            double NewLat = Convert.ToDouble(latitudeTextBox.Text);
            double NewLng = Convert.ToDouble(longitudeTextBox.Text);
            po.points[index] = new PlanPoint(new PointLatLng(NewLat, NewLng));
            po.points[index].alt = Convert.ToDouble(altitudeTextBox.Text);
            po.points[index].speed = Convert.ToDouble(speedBox.Text);
            po.points[index].role = roleCombobox.Text;
            this.Close();
        }
    }
}
