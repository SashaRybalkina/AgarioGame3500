using System;
using System.Numerics;

namespace AgarioModels
{
	public class Player : GameObject
	{
        private string name { get; }
        public Player(long id, string name, Vector2 location, float mass, int argbColor)
            : base (id, location, mass, argbColor)
        {
            this.name = name;
            location = new Vector2(400, 400);
            mass = 12;
            argbColor = 0;
            id++;
        }     
        
	}
}

