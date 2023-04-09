using System;
using System.Numerics;

namespace TowardAgarioStepOne
{
    public class WorldModel
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float Radius;
        public Vector2 Direction;

        public WorldModel()
        {
            X = 100;
            Y = 100;
            Radius = 15;
            Direction = new Vector2(10, 5);
        }

        public void AdvanceGameOneStep()
        {
            lock (this)
            {
                X += Direction.X;
                Y += Direction.Y;
                if (X == 800 | Y == 800)
                {
                    if (X == 800)
                        Direction = new Vector2(-Direction.X, Direction.Y);
                    if (Y == 800)
                        Direction = new Vector2(Direction.X, -Direction.Y);
                }
            }
        }
    }
}