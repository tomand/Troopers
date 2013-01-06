using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    public class Weapon
    {
        private List<Bullet> _bullets;
        public int TimeToShoot { get; set; }

        public Weapon()
        {
            TimeToShoot = 3;
            _bullets = new List<Bullet>();
        }

        public float ShootDirection { get; set; }

        public bool IsShooting
        {
            get { return GetAliveBullets().Count() > 0; }
            
        }

        public void Fire(float faceDirection, Vector2 centerPosition, Trooper target)
        {
            _bullets.Add(new Bullet(centerPosition,target));
        }

        public void Update(GameTime gameTime)
        {
            foreach (Bullet bullet in GetAliveBullets())    
            {
                bullet.Update(gameTime);
            }
        }

        public IEnumerable<Bullet> GetAliveBullets()
        {
            return _bullets.Where(b => b.IsAlive);
        }
    }

}
