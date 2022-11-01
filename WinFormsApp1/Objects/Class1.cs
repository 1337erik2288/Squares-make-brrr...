using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual void Render(Graphics g)
        {

        }
    }
}
