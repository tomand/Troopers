using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class MainMenu
    {
        private Vector2 _position;
        private List<MenuItem> _menuItems;
        public float Height { get; set; }

        public MainMenu(Vector2 position, float height)
        {
            _position = position;
            Height = height;
            _menuItems = new List<MenuItem>();
            _menuItems.Add(new MenuItem("Start", true));
            _menuItems.Add(new MenuItem("Start Level 1", false));
            _menuItems.Add(new MenuItem("Start Level 2", false));
            _menuItems.Add(new MenuItem("Start Level 3", false));
            _menuItems.Add(new MenuItem("Help", false));
            _menuItems.Add(new MenuItem("Exit", false));
        }

        public Vector2 Position
        {
            get { return _position; }
            
        }

        public MenuItem SelectedItem
        {
            get { return _menuItems.Find(i => i.IsSelected); }  
        }


        public IList<MenuItem> GetMenuItems()
        {
            return _menuItems;
        }

        public void Update(GameTime gameTime, MenuNavigation navigation)
        {
            if (navigation == MenuNavigation.Next)
            {
                GotoNextMenuItem();
            }

            if (navigation == MenuNavigation.Previous)
            {
                GotoPreviousMenuItem();
            }
        }

        private void GotoPreviousMenuItem()
        {
            for (int i = 0; i < _menuItems.Count; i++)
            {
                if (i > 0 && _menuItems[i].IsSelected)
                {
                    _menuItems[i].IsSelected = false;
                    _menuItems[i-1].IsSelected = true;
                    return;
                }
            }
        }

        private void GotoNextMenuItem()
        {
            for (int i = 0; i < _menuItems.Count; i++)
            {
                if (i < _menuItems.Count - 1 && _menuItems[i].IsSelected)
                {
                    _menuItems[i].IsSelected = false;
                    _menuItems[i + 1].IsSelected = true;
                    return;
                }
            }
        }
    }

    public enum MenuNavigation
    {
        None,
        Next,
        Previous
    }
}
