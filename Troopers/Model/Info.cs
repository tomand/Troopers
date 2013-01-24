using System;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    internal class Info
    {
        public Vector2 Position { get; private set; }
        public Trooper Trooper { get; set; }

        public Info(Vector2 position)
        {
            Position = position;
           
        }

        public string GetTrooperInfo()
        {
            if (!Trooper.IsControlledByComputer)
            {
                return string.Format("Health: {1}{0}Time: {2}{0}Bullets: {6}{0}Cost to shoot: {3}{0}{0}{4}{0}{0}{0}{0}{5}", Environment.NewLine, Trooper.Health,
                                 Trooper.Time, Trooper.Weapon.TimeToShoot, GetTargetEnemyInfo(), GetHelpText(), Trooper.NumberOfBullets);    
            }
            else
            {
                return string.Format("Health: {1}{0}{2}", Environment.NewLine, Trooper.Health,
                                 GetSimpleHelp()); 
            }
        }

        private string GetSimpleHelp()
        {
            return string.Format("{0}{0}Click \"Esc\" to pause.", Environment.NewLine);

        }

        private string GetHelpText()
        {
            StringBuilder help = new StringBuilder();
            help.Append( string.Format("Help:{0}To shoot, move the cursor{0}to a red trooper and{0}click with the mouse.", Environment.NewLine));
            help.Append(string.Format("{0}{0}To move, click where the{0}cursor is a green or{0}yellow square.", Environment.NewLine));
            help.Append(string.Format("{0}{0}When the square is yellow,{0}you won't have time to{0}shoot.", Environment.NewLine));
            help.Append(GetSimpleHelp());
            return help.ToString();
        }

        private string GetTargetEnemyInfo()
        {
            if (Trooper.ShootingTarget != null)
                return string.Format("Target enemy:{0}Health: {1}{0}Min - Max damage:{0}{2} - {3}", Environment.NewLine, Trooper.ShootingTarget.Health, GetMinDamage(),GetMaxDamage());

            return string.Format("{0}{0}{0}", Environment.NewLine);
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