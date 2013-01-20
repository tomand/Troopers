using System;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    internal class TrooperInfo
    {
        public Vector2 Position { get; private set; }
        public Trooper Trooper { get; set; }

        public TrooperInfo(Vector2 position)
        {
            Position = position;
           
        }

        public string GetTrooperInfo()
        {
            if (!Trooper.IsControlledByComputer)
            {
                return string.Format("Health: {1}{0}Time:{2}{0}Cost to shoot:{3}{0}{0}{4}", Environment.NewLine, Trooper.Health,
                                 Trooper.Time, Trooper.Weapon.TimeToShoot, GetTargetEnemyInfo());    
            }
            else
            {
                return string.Format("Health: {1}", Environment.NewLine, Trooper.Health,
                                 Trooper.Time, Trooper.Weapon.TimeToShoot); 
            }
        }

        private string GetTargetEnemyInfo()
        {
            if (Trooper.ShootingTarget != null)
                return string.Format("Target enemy:{0}Health:{1}{0}Min - Max damage:{0}{2} - {3}", Environment.NewLine, Trooper.ShootingTarget.Health, GetMinDamage(),GetMaxDamage());

            return "";
        }

        private int GetMaxDamage()
        {
            return Trooper.Weapon.GetMaxDamage(Trooper.CenterPosition, Trooper.ShootingTarget.CenterPosition);
        }

        private int GetMinDamage()
        {
            return Trooper.Weapon.GetMinDamage(Trooper.CenterPosition, Trooper.ShootingTarget.CenterPosition);
        }
    }
}