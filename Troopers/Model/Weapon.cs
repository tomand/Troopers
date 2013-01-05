using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class Weapon
    {
        public int TimeToShoot { get; set; }

        public Weapon()
        {
            TimeToShoot = 3;
        }

        public float ShootDirection { get; set; }

        public void Fire(float faceDirection, Vector2 centerPosition)
        {
            
        }
    }
}
