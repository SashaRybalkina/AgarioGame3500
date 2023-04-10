using System;
using System.Numerics;
namespace AgarioModels
{
	public class GameObject
	{
        public float X => location.X;
        public float Y => location.Y;
        public Vector2 location { get; set; }
        public int ARGBcolor { get; set; }
        public float Mass { get; set; }
        public long ID { get; set; }
        public float Radius => MathF.Sqrt(Mass);

        public GameObject(long id, Vector2 location, float mass, int argbColor)
		{
            this.ID = id;
            this.location = location;
            this.Mass = mass;
            this.ARGBcolor = argbColor;
		}
	}
}

