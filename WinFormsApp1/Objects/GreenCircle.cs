using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WinFormsApp1.Objects
{
    class GreenCircle : BaseObject
    {
        public float r = 50;
        public Action<GreenCircle> deathGreenCircle;
        public Action<GreenCircle> Action;

        public GreenCircle(float x, float y, float angle) : base(x, y, angle)
        {

        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -(r/2), -(r/2), r, r);
            this.r -= (float)0.1;
            if (r <= 0)
            {
                r = 50;
                deathGreenCircle(this);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-(r / 2), -(r / 2), r, r);
            return path;
        }
    }
}
