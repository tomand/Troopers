using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Troopers.View
{
    public class Camera
    {
        private int _xOffset;
        private int _yOffset;
        private int _yTileSize;
        private int _xTileSize;
        private int _viewPortHeight;
        private int _numberOfXTiles;
        private int _numberOfYTiles;
        private bool _doRotate = false;
        private int _viewPortWidth;
        private double _scale;
        
        public Camera(int viewPortHeight, int viewPortWidth, int xOffset, int yOffset, int xTileSize, int yTileSize, int numberOfXTiles, int numberOfYTiles)
        {
            _scale = Convert.ToDouble(Math.Min(viewPortHeight, viewPortWidth)) / Convert.ToDouble(numberOfXTiles * xTileSize + xOffset * 2);
            this._xOffset = Convert.ToInt32(xOffset * _scale);
            this._yOffset = Convert.ToInt32(yOffset * _scale);
            _xTileSize = Convert.ToInt32(xTileSize * _scale);
            _yTileSize = Convert.ToInt32(yTileSize * _scale);
            _viewPortHeight = viewPortHeight;
            _viewPortWidth = viewPortWidth;
            _numberOfXTiles = numberOfXTiles;
            _numberOfYTiles = numberOfYTiles;
            

        }

        public Vector2 Transform(Vector2 point)
        {
            return new Vector2(TransformX(point.X), TransformY(point.Y)); 
        }

        public Vector2 RotateTransform(Vector2 point)
        {
            return new Vector2(RotateTransformX(point.X), RotateTransformY(point.Y));
        }

        public Vector2 ScaleTransform(Vector2 point)
        {
            return new Vector2(ScaleTransformX(point.X), ScaleTransformY(point.Y));
        }

        public int TransformX(float x)
        {
            //return RotateX(x) * _xTileSize + _xOffset;
            return (int)(x * _xTileSize + _xOffset);
        }

        private float RotateX(float x)
        {
            //return _doRotate ? (_numberOfXTiles - 1) - x : x; 
            return (_numberOfXTiles - 1) - x; 
        }

        public float RotateTransformX(float x)
        {
           return TransformX( RotateX(x) );
        }

        public float RotateTransformY(float y)
        {
          return  TransformY( RotateY(y) );
        }

        public float ScaleTransformX(float x)
        {
            return Scale(TransformX(x));
        }

        public float ScaleTransformY(float y)
        {
            return Scale(TransformY(y));
        }

        public int TransformY(float y)
        {
            //return RotateY(y) * _yTileSize + _yOffset;
            return (int)(y * _yTileSize + _yOffset);
        }

        private float RotateY(float y)
        {
            //return _doRotate ? (_numberOfYTiles - 1) - y : y;
            return (_numberOfYTiles - 1) - y; 
        }



        internal void Rotate()
        {
            _doRotate = !_doRotate;
        }

        internal float Scale(float size)
        {
            return (int)((double)size * _scale);
        }

        internal int TransformSizeX(float x)
        {
            return (int)(x * _xTileSize);
        }

        internal int TransformSizeY(float y)
        {
            return (int)(y * _yTileSize);
        }

        internal Vector2 TransformScreenToLogic(Vector2 screenPosition)
        {
            return new Vector2(TransformScreenPositionX(screenPosition.X), TransformScreenPositionY(screenPosition.Y)); 
        }

        private float TransformScreenPositionY(float p)
        {
            return (p - _yOffset) / _yTileSize;
        }

        private float TransformScreenPositionX(float p)
        {
            return (p - _xOffset) / _xTileSize;
        }
    }
}
