using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class MainMenu : BaseMenu
    {
        public MainMenu(Vector2 position, float height)
        {
            _position = position;
            Header = "Main menu";
            _menuItems = new List<MenuItem>();
            _menuItems.Add(new MenuItem("Start", true));
            _menuItems.Add(new MenuItem("Start Level 1", false));
            _menuItems.Add(new MenuItem("Start Level 2", false));
            _menuItems.Add(new MenuItem("Start Level 3", false));
            _menuItems.Add(new MenuItem("Help", false));
            _menuItems.Add(new MenuItem("Exit", false));
            Height = height * (_menuItems.Count + 2);
        }

        
    }

   
}
