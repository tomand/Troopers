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
        private int _damage;
        private int _maxDistanceSquared;
        public int TimeToShoot { get; set; }

        public Weapon()
        {
            TimeToShoot = 3;
            _damage = 11;
            _maxDistanceSquared = 900;
            _bullets = new List<Bullet>();
        }

        public float ShootDirection { get; set; }

        public bool IsShooting
        {
            get { return GetAliveBullets().Count() > 0; }
            
        }

        public void Fire(float faceDirection, Vector2 centerPosition, Trooper target)
        {
            _bullets.Add(new Bullet(centerPosition, target, CalculateDamage(centerPosition, target.CenterPosition, Dice.Roll())));
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

        private int CalculateDamage(Vector2 centerPosition, Vector2 target, int randomAdjustment)
        {
            int damageBeforeRandomAdjustment = _damage -
                                               (int)
                                               Math.Floor((float)_damage *
                                                          (GetSquaredDistanceFromStartingPoint( centerPosition,  target) /
                                                           (float)_maxDistanceSquared));
            int damageAfterRandomAdjustment = damageBeforeRandomAdjustment - randomAdjustment;
            return Math.Max(0, damageAfterRandomAdjustment);
        }

        public int GetMinDamage(Vector2 centerPosition, Vector2 target)
        {
           return CalculateDamage(centerPosition, target, 6);
        }

        public int GetMaxDamage(Vector2 centerPosition, Vector2 target)
        {
            return CalculateDamage(centerPosition, target, 1);
        }


        private float GetSquaredDistanceFromStartingPoint(Vector2 centerPosition, Vector2 target)
        {
            return Vector2.DistanceSquared(centerPosition, target);
        }
    }

}
