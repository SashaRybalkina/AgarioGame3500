using System;
using System.Numerics;
using System.Text.Json.Serialization;

namespace AgarioModels
{
	public class Food : GameObject
	{
		[JsonConstructor]
		public Food(long ID, float X, float Y, float Mass, int ARGBColor)
			: base(ID, X, Y, Mass, ARGBColor)
		{
			
		}
	}
}

