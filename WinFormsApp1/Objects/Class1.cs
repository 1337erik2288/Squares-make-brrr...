using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinFormsApp1.Objects
{
    class BaseObject
    {
        public float x;
        public float y;
        public float Angle;
        public BaseObject(float x, float y, float angle)
        {
            this.x = x;
            this.y = y;
            Angle = angle;
        }
        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(x, y);
            matrix.Rotate(Angle);
            
            return matrix;
        }
        public virtual void Render(Graphics g)
        {

        }
        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }
        public virtual bool OverLaps (BaseObject obj, Graphics g)
        {
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            var region = new Region(path2);
            region.Intersect(path2);
            return !region.IsEmpty(g);
        }
    }
}
