using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class PauseMenu : BaseMenu
    {
       public PauseMenu(Microsoft.Xna.Framework.Vector2 position, float height)
        {
            _position = position;
            Header = "The game is paused";
            _menuItems = new List<MenuItem>();
            _menuItems.Add(new MenuItem("Resume", true));
            _menuItems.Add(new MenuItem("Restart", false));
            _menuItems.Add(new MenuItem("Main menu", false));
            _menuItems.Add(new MenuItem("Help", false));
            _menuItems.Add(new MenuItem("Exit", false));
            Height = height * (_menuItems.Count + 1);
        }

       
    }
}
