using WinFormsApp1.Objects;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        //MyRectangle myRect;
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);
            objects.Add(player);

            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 45));
            //myRect = new MyRectangle(100, 100, 45);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            
            foreach(var obj in objects.ToList())
            {
                if (obj != player && player.OverLaps(obj, g))
                {
                    txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n"+ txtLog.Text;  
                    
                    if (obj == marker)
                    {
                        objects.Remove(marker);
                        marker = null;
                    }
                }

                g.Transform = obj.GetTransform();
                obj.Render(g);
            }

            //g.Transform = myRect.GetTransform();

            //g.FillRectangle(new SolidBrush(Color.White),0, 0, pbMain.Width, pbMain.Height);

            //myRect.Render(g);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (marker != null)
            {
                float dx = marker.x - player.x;
                float dy = marker.y - player.y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.x += dx * 2;
                player.y += dy * 2;
            }
            

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