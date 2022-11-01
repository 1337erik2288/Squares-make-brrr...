using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Objects
{
    class MyRectangle : BaseObject
    {
        public MyRectangle(float x, float y, float angle)
        {

        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Green), 200, 100, 50, 30);
            g.DrawRectangle(new Pen(Color.Red), 200, 100, 50, 30);
        }
    }
}
