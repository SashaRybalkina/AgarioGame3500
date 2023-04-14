using System;
using System.Numerics;
using System.Text.Json.Serialization;

namespace AgarioModels
{
	public class Food : GameObject
	{
		[JsonConstructor]
		public Food(float X, float Y, int ARGBColor, long ID, float Mass)
			: base(ID, X, Y, Mass, ARGBColor)
		{
			
		}
	}
}

