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
        public float r = 36;
        public Action<GreenCircle> deathGreenCircle;
        public Action<GreenCircle> Action;

        public GreenCircle(float x, float y, float angle) : base(x, y, angle)
        {

        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -18, -18, r, r);
            this.r -= (float)0.1;
            if (r <= 0)
            {
                r = 36;
                deathGreenCircle(this);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-18, -18, 36, 36);
            return path;
        }
    }
}
