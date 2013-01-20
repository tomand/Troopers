using Microsoft.Xna.Framework;

namespace Troopers.Model
{
    internal class Line
    {
        Vector2 Point1 { get; set; }
        Vector2 Point2 { get; set; }

        public Line(Vector2 positionA, Vector2 positionB)
        {
            Point1 = positionA;
            Point2 = positionB;
        }


        public bool Intersects(Line line)
        {
            float firstLineSlopeX, firstLineSlopeY, secondLineSlopeX, secondLineSlopeY;

            firstLineSlopeX = this.Point2.X - this.Point1.X;
            firstLineSlopeY = this.Point2.Y - this.Point1.Y;

            secondLineSlopeX = line.Point2.X - line.Point1.X;
            secondLineSlopeY = line.Point2.Y - line.Point1.Y;

            float s, t;
            s = (-firstLineSlopeY * (this.Point1.X - line.Point1.X) + firstLineSlopeX * (this.Point1.Y - line.Point1.Y)) / (-secondLineSlopeX * firstLineSlopeY + firstLineSlopeX * secondLineSlopeY);
            t = (secondLineSlopeX * (this.Point1.Y - line.Point1.Y) - secondLineSlopeY * (this.Point1.X - line.Point1.X)) / (-secondLineSlopeX * firstLineSlopeY + firstLineSlopeX * secondLineSlopeY);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
              //  float intersectionPointX = this.Point1.X + (t * firstLineSlopeX);
                //float intersectionPointY = this.Point1.Y + (t * firstLineSlopeY);

                // Collision detected
              //  intersectionPoint = new Vector2(intersectionPointX, intersectionPointY, 0);

                return true;
            }

         //   intersectionPoint = Vector2.Zero;
            return false; // No collision
        }
    }
}