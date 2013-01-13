using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    class MediKit
    {
        public Vector2 Position { get; set; }
        public bool IsTaken { get; set; }

        public MediKit(Vector2 position)
        {
            Position = position;
            IsTaken = false;
        }

        
    }
}
