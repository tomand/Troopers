using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class BaseMenu
    {
        protected Vector2 _position;
        protected List<MenuItem> _menuItems;
        public string Help { get; set; }
        public float Height { get; set; }
        public string Header { get; set; }

        public BaseMenu()
        {
            Help = string.Format("To navigate in the menus, use the up and down arrow keys or the mouse wheel.{0}Left click with the mouse or hit Enter to select a menu option.", Environment.NewLine);
        }

        public Vector2 Position
        {
            get { return _position; }
            
        }

        public MenuItem SelectedItem
        {
            get { return _menuItems.Find(i => i.IsSelected); }  
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            return _menuItems.Where(m => m.Visible);
        }

        protected void GotoPreviousMenuItem()
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

        protected void GotoNextMenuItem()
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
    }

    public enum MenuNavigation
    {
        None,
        Next,
        Previous
    }
}
