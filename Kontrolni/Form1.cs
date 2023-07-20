using System.Drawing.Drawing2D;

namespace Kontrolni
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int dimenzija = (int)(vratiManjuDimenziju() * 0.9);
            Point centar = new Point(ClientRectangle.Width / 2, ClientRectangle.Height / 2);
            nacrtajTeren(g, centar, dimenzija);

            Bitmap bitmapa = new Bitmap(1000, 600);
            Graphics g1 = Graphics.FromImage(bitmapa);

            g1.SmoothingMode = SmoothingMode.AntiAlias;
            centar = new Point(500, 300);
            dimenzija = (int)(0.9 * 300);
            nacrtajTeren(g1, centar, dimenzija);
            bitmapa.Save("bitmapa.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void nacrtajTeren(Graphics g, Point centarTerena, int duzinaTerena)
        {
            int sirinaTerena = (int)(0.6 * duzinaTerena);
            Rectangle terenRectangle = new Rectangle(centarTerena.X - duzinaTerena / 2, centarTerena.Y - sirinaTerena / 2, duzinaTerena, sirinaTerena);
            g.DrawRectangle(new Pen(Color.Black, 2.5f), terenRectangle);
            g.FillRectangle(new SolidBrush(Color.Orange), terenRectangle);

            Point pocetakTerena = new Point(centarTerena.X - duzinaTerena / 2, centarTerena.Y - sirinaTerena / 2);

            nacrtajLinijeTerena(g, pocetakTerena, duzinaTerena, sirinaTerena, new Pen(Color.White, 2.5f));
            nacrtajIgrace(g, pocetakTerena, duzinaTerena, sirinaTerena, sirinaTerena / 10);
        }

        private void nacrtajLinijeTerena(Graphics g, Point pocetakTerena, int duzinaTerena, int sirinaTerena, Pen olovka)
        {
            int autLinija = (int)(0.07 * sirinaTerena);
            int napadackeLinije = (int)(duzinaTerena / 6);

            Rectangle autLinijeRectangle = new Rectangle(pocetakTerena.X + autLinija, pocetakTerena.Y + autLinija, duzinaTerena - 2 * autLinija, sirinaTerena - 2 * autLinija);
            g.DrawRectangle(olovka, autLinijeRectangle);

            g.DrawLine(olovka, new Point(pocetakTerena.X + duzinaTerena / 2, pocetakTerena.Y + autLinija), new Point(pocetakTerena.X + duzinaTerena / 2, pocetakTerena.Y + sirinaTerena - autLinija));
            g.DrawLine(olovka, new Point(pocetakTerena.X + duzinaTerena / 2 - napadackeLinije, pocetakTerena.Y + autLinija), new Point(pocetakTerena.X + duzinaTerena / 2 - napadackeLinije, pocetakTerena.Y + sirinaTerena - autLinija));
            g.DrawLine(olovka, new Point(pocetakTerena.X + duzinaTerena / 2 + napadackeLinije, pocetakTerena.Y + autLinija), new Point(pocetakTerena.X + duzinaTerena / 2 + napadackeLinije, pocetakTerena.Y + sirinaTerena - autLinija));

            olovka.DashStyle = DashStyle.Dash;
            g.DrawLine(olovka, new Point(pocetakTerena.X + duzinaTerena / 2 - napadackeLinije, pocetakTerena.Y), new Point(pocetakTerena.X + duzinaTerena / 2 - napadackeLinije, pocetakTerena.Y + sirinaTerena));
            g.DrawLine(olovka, new Point(pocetakTerena.X + duzinaTerena / 2 + napadackeLinije, pocetakTerena.Y), new Point(pocetakTerena.X + duzinaTerena / 2 + napadackeLinije, pocetakTerena.Y + sirinaTerena));

        }

        private void nacrtajIgrace(Graphics g, Point pocetakTerena, int duzinaTerena, int sirinaTerena, int precnikIgraca)
        {
            Color ljubicasti = Color.MediumVioletRed;
            Color zeleni = Color.ForestGreen;
            Pen olovka = new Pen(Color.Black, 2.5f);
            Color bojaBroja = Color.White;

            Point pozicijaIgraca = new Point((int)(pocetakTerena.X + duzinaTerena / 5), (int)(pocetakTerena.Y + sirinaTerena / 3.7));
            nacrtajIgraca(g, pozicijaIgraca, precnikIgraca, olovka, ljubicasti, 12, bojaBroja);
            pozicijaIgraca = new Point((int)(pocetakTerena.X + duzinaTerena / 2.7), (int)(pocetakTerena.Y + sirinaTerena / 1.7));
            nacrtajIgraca(g, pozicijaIgraca, precnikIgraca, olovka, ljubicasti, 10, bojaBroja);

            pozicijaIgraca = new Point((int)(pocetakTerena.X + duzinaTerena / 1.5), (int)(pocetakTerena.Y + sirinaTerena / 2.7));
            nacrtajIgraca(g, pozicijaIgraca, precnikIgraca, olovka, zeleni, 3, bojaBroja);
            pozicijaIgraca = new Point((int)(pocetakTerena.X + duzinaTerena / 1), (int)(pocetakTerena.Y + sirinaTerena / 2.1));
            nacrtajIgraca(g, pozicijaIgraca, precnikIgraca, olovka, zeleni, 7, bojaBroja);
        }

        private void nacrtajIgraca(Graphics g, Point centar, int precnik, Pen olovka, Color bojaIgraca, int broj, Color bojaBroja)
        {
            Rectangle igrac = new Rectangle(centar.X - precnik / 2, centar.Y - precnik / 2, precnik, precnik);
            g.DrawEllipse(olovka, igrac);
            g.FillEllipse(new SolidBrush(bojaIgraca), igrac);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            g.DrawString(broj + "", new Font("Comic Sans MS", precnik / 2), new SolidBrush(bojaBroja), centar, format);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        public int vratiManjuDimenziju()
        {
            if (ClientRectangle.Width < ClientRectangle.Height)
                return ClientRectangle.Width;
            else
                return ClientRectangle.Height;
        }
    }
}