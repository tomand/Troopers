using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    internal class LevelManager
    {
        private List<Level> _levels;

        public LevelManager(int numberOfXTiles, int numberOfYTiles)
        {
            _levels = new List<Model.Level>
                {
                    new Model.Level(numberOfXTiles, numberOfYTiles, new Vector2(0, 0), "level1"),
                    new Model.Level(numberOfXTiles, numberOfYTiles, new Vector2(0, 0), "level2"),
                    new Model.Level(numberOfXTiles, numberOfYTiles, new Vector2(0, 0), "level3")
                };
        }



        public Level CurrentLevel { get { return _levels.Find(l => l.Current); } }

        internal void StartLevel(int levelNumber)
        {
            _levels[levelNumber - 1].Current = true;
            StartLevel();
        }

        internal void StartLevel()
        {
            CurrentLevel.Start();
        }

        public void GotoNextLevel()
        {
            for (int i = 0; i < _levels.Count; i++)
            {
                if (_levels[i].Current && i < _levels.Count - 1)
                {
                    _levels[i].Current = false;
                    _levels[i + 1].Current = true;
                    return;
                }
                else if (_levels[i].Current)
                {
                    _levels[i].Current = false;
                    _levels[0].Current = true;
                    return;
                }
            }
        }
    }
}