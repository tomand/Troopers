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
            HelpText = string.Format("{1}{0}{0}{2}{0}{0}{3}{0}{0}{4}", 
                Environment.NewLine
                ,"Welcome to Troopers!"
                ,"This is a turnbased game where you control troopers."
                ,"The order of the troopers is randomized and you control the blue troopers."
                ,"To leave this help, click Backspace or the left mouse button."
                ,"To navigate in the menus, use the up and down arrow keys and click Enter to select a menu option.");
        }

    }
}
