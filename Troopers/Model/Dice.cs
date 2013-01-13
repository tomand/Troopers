using System;

namespace Troopers.Model
{
    internal static class Dice
    {
        private static Random _random;
        public static int Roll()
        {
            if (_random == null)
            {
                _random = new Random();
            }
            return _random.Next(1, 6);
        }
    }
}