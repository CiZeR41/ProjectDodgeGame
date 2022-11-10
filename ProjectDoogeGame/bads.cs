using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ProjectDoogeGame
{
    class bads : Monsters
    {
        public bads(int sizex,int sizey,string src, Canvas playground, int startx, int starty) : base(sizex, sizey,src, playground, startx, starty)
        {
        }

        public override void Chase(double x ,double y, double speed)
        {
            if (x > Left)
            {
                Canvas.SetLeft(_img, Canvas.GetLeft(_img) + speed);
            }

            else if (x < Left)
            {
                Canvas.SetLeft(_img, Canvas.GetLeft(_img) - speed);
            }
            if (y > Top)
            {
                Canvas.SetTop(_img, Canvas.GetTop(_img) + speed);
            }
            else if (y < Top)
            {
                Canvas.SetTop(_img, Canvas.GetTop(_img) - speed);
            }

        }
    }
}
