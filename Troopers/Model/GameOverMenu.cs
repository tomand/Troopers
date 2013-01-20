using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class GameOverMenu : BaseMenu
    {
        private bool _playerWon;
        private float _rowHeight;

        public bool PlayerWon { set
        {
            _menuItems.Find(m => m.Text == "Continue").Visible = value;

            CalculateHeight();
           
            _menuItems.Find(m => m.Text == "Continue").IsSelected = value;
            _menuItems.Find(m => m.Text == "Restart").IsSelected = !value;
            SetHeader(value);
            
            _playerWon = value;
        }  }

        private void SetHeader(bool playerWon)
        {
            if (playerWon)
            {
                Header = "Congratulations, you won!";
            }
            else
            {
                Header = "Game over!";
            }
        }

        private void CalculateHeight()
        {
            Height = _rowHeight * (_menuItems.Count(m => m.Visible) + 1);
        }

        public GameOverMenu(Vector2 position, float height)
        {
            _rowHeight = height;
            _position = position;
          
            _menuItems = new List<MenuItem>();
            _menuItems.Add(new MenuItem("Continue", true));
            _menuItems.Add(new MenuItem("Restart", false));
            _menuItems.Add(new MenuItem("Main menu", false));
            _menuItems.Add(new MenuItem("Help", false));
            _menuItems.Add(new MenuItem("Exit", false));
            CalculateHeight();
       
        }
    }
}
