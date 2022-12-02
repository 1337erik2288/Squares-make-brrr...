using System.Diagnostics.Metrics;
using WinFormsApp1.Objects;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        GreenCircle greenCircle1;
        GreenCircle greenCircle2;

        public Form1()
        {
            InitializeComponent();

            Random rnd = new Random();

            var randomX = rnd.Next(0, pbMain.Width);
            var randomY = rnd.Next(0, pbMain.Height);

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            greenCircle1 = new GreenCircle(randomX, randomY, 0);
            greenCircle2 = new GreenCircle(randomX, randomY, 0);

            objects.Add(greenCircle1);
            objects.Add(greenCircle2);

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.OnGreenCircleOverlap += (m) =>
            {
                var rnd = new Random();
                var randomX = rnd.Next(0, pbMain.Width);
                var randomY = rnd.Next(0, pbMain.Height);

                objects.Remove(m);

                if (m == greenCircle1)
                {
                    greenCircle1 = new GreenCircle(randomX, randomY, 0);
                    objects.Add(greenCircle1);
                }
                else
                {
                    greenCircle2 = new GreenCircle(randomX, randomY, 0);
                    objects.Add(greenCircle2);
                }

                
            };
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);
            objects.Add(player);

            
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            updatePlayer();
            
            foreach(var obj in objects.ToList())
            {
                if (obj != player && player.OverLaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                    
                    
                }

                foreach (var ovj in objects)
                {
                    g.Transform = obj.GetTransform();
                    obj.Render(g);
                }
                
            }
;
        }
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.x - player.x;
                float dy = marker.y - player.y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.x += player.vX;
            player.y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updatePlayer();
 
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }
            marker.x = e.X;
            marker.y = e.Y;
        }

        private void pbMain_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}