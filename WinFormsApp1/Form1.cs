using WinFormsApp1.Objects;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        GreenCircle greenCircle;

        public Form1()
        {
            InitializeComponent();

            Random rnd = new Random();

            var randomX = rnd.Next(0, pbMain.Width);
            var randomY = rnd.Next(0, pbMain.Height);

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            greenCircle = new GreenCircle(randomX, randomY, 0);

            objects.Add(greenCircle);

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);
            objects.Add(player);

            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 45));
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
    }
}