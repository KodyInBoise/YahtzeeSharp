using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Yahtzee.Utilities
{
    class ImageHelper
    {
        public static BitmapImage DiceOne()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images", "dice-one.png");
            var uri = new Uri(path);

            return new BitmapImage(uri);
        }

        public static BitmapImage DiceTwo()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images", "dice-two.png");
            var uri = new Uri(path);

            return new BitmapImage(uri);
        }

        public static BitmapImage DiceThree()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images", "dice-three.png");
            var uri = new Uri(path);

            return new BitmapImage(uri);
        }

        public static BitmapImage DiceFour()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images", "dice-four.png");
            var uri = new Uri(path);

            return new BitmapImage(uri);
        }

        public static BitmapImage DiceFive()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images", "dice-five.png");
            var uri = new Uri(path);

            return new BitmapImage(uri);
        }

        public static BitmapImage DiceSix()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images", "dice-six.png");
            var uri = new Uri(path);

            return new BitmapImage(uri);
        }
    }
}
