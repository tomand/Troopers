using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class Help
    {
        public Vector2 Position { get; set; }
        public string HelpText { get; set; }

        public Help(Vector2 position)
        {
            Position = position;
            HelpText = string.Format("{1}{0}{0}{2}{0}{0}{3}{0}{0}{4}{0}{0}{5}{0}{0}{6}{0}{0}{8}{0}{0}{7}", 
                Environment.NewLine
                ,"Welcome to Troopers!"
                ,"This is a turnbased game where you control troopers."
                ,"The order of the troopers is randomized and you control the blue troopers."
                ,string.Format("In your turn, you can either shoot or move.{0}To shoot, you move the cursor to a red enemy trooper and click with the mouse.", Environment.NewLine)
                ,string.Format("To move, you click where the cursor is a green or yellow square.{0}When the square is yellow, you won't have time to shoot.",Environment.NewLine)
                ,"To pause the game, click Space."
                ,"To leave this help, click Backspace."
                ,"To navigate in the menus, use the up and down arrow keys and click Enter to select a menu option.");
        }

    }
}
