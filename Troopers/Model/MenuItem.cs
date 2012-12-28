using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace Troopers.Model
{
    class MenuItem
    {
        public string Text { get; set; }
        public bool IsSelected { get; set; }

        public MenuItem(string text, bool isSelected)
        {
            Text = text;
            IsSelected = isSelected;
        }

        
    }
}
