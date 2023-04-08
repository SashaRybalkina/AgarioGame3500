using System;
using System.Numerics;

namespace TowradsAgarioStepOne
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
            Direction = new Vector2(50, 25);
		}

        public void AdvanceGameOneStep()
        {
            lock(this){ X++; Y++; }
        }
	}
}