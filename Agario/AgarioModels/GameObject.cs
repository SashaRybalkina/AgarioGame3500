using System;
using System.Numerics;
using System.Text.Json.Serialization;

namespace AgarioModels
{
	public class GameObject
	{
        public float X { get { return location.X; } set { location.X = value; } }
        public float Y { get { return location.Y; } set { location.Y = value; } }
        public Vector2 location;
        public int ARGBColor { get; set; }
        public float Mass { get; set; }
        public long ID { get; set; }
        public float Radius => MathF.Sqrt(Mass);

        [JsonConstructor]
        public GameObject(long ID, float X, float Y, float Mass, int ARGBColor)
		{
            this.ID = ID;
            this.Mass = Mass;
            this.ARGBColor = ARGBColor;
            this.X = X;
            this.Y = Y;
		}
	}
}

