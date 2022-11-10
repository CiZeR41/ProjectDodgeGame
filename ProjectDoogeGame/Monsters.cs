using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace ProjectDoogeGame
{
    class Monsters
    {
        protected Image _img;
        Canvas _playground;
        string _src;
        Random rnd = new Random();
        public Monsters(int sizex, int sizey, string src, Canvas playground, int x, int y)
        {
            _src = src;
            _playground = playground;
            _img = new Image();
            _img.Source = new BitmapImage(new Uri(src));
            _img.Height = sizex;
            _img.Width = sizey;
            Canvas.SetLeft(_img, x);
            Canvas.SetTop(_img, y);
            _playground.Children.Add(_img);
        }
        public Monsters()
        {

        }
   
        public virtual void Chase(double x, double y, double speed)
        {

        }

        public void Kill()
        {
            _playground.Children.Remove(_img);
        }
        public void DoJump()
        {
            Canvas.SetLeft(_img, rnd.Next(0,800));
            Canvas.SetTop(_img, rnd.Next(200,600));
        }
        public double Left
        {

            get { return Canvas.GetLeft(_img); }
        }
        public double Top
        {
            get { return Canvas.GetTop(_img); }
        }
        public string SavePosition()
        {
            return $"{Top},{Left}";
        }
        public void GoUp()
        {
            Canvas.SetTop(_img, Canvas.GetTop(_img) - 20);
        }
        public void GoDown()
        {
            Canvas.SetTop(_img, Canvas.GetTop(_img) + 20);

        }
        public void GoRight()
        {
            Canvas.SetLeft(_img, Canvas.GetLeft(_img) + 20);
        }
        public void GoLeft()
        {
            Canvas.SetLeft(_img, Canvas.GetLeft(_img) - 20);
        }
    }
}
