﻿using System;
using System.Numerics;

namespace AgarioModels
{
	public class Food : GameObject
	{
		public Food(long id, Vector2 location, float mass, int argbColor)
			: base(id, location, mass, argbColor)
		{
			Random random = new Random();			
			argbColor = random.Next();
			mass = random.Next(10);
			id++;
		}
	}
}

