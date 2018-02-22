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
        public static BitmapImage GetDiceImage(int _number)
        {
            BitmapImage _numberImage = new BitmapImage();

            switch (_number)
            {
                case 1:
                    _numberImage.UriSource = new Uri(Path.Combine(Environment.CurrentDirectory, "Images", "dice-one.png"));
                    break;
                case 2:
                    _numberImage.UriSource = new Uri(Path.Combine(Environment.CurrentDirectory, "Images", "dice-two.png"));
                    break;
                case 3:
                    _numberImage.UriSource = new Uri(Path.Combine(Environment.CurrentDirectory, "Images", "dice-three.png"));
                    break;
                case 4:
                    _numberImage.UriSource = new Uri(Path.Combine(Environment.CurrentDirectory, "Images", "dice-four.png"));
                    break;
                case 5:
                    _numberImage.UriSource = new Uri(Path.Combine(Environment.CurrentDirectory, "Images", "dice-five.png"));
                    break;
                case 6:
                    _numberImage.UriSource = new Uri(Path.Combine(Environment.CurrentDirectory, "Images", "dice-six.png"));
                    break;
            }

            return _numberImage;
        }

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
