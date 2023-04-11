using System;
using System.Numerics;

namespace AgarioModels
{
	public class Player : GameObject
	{
        private string name { get; }
        public float x { get; set; }
        public float y { get; set; }
        public Player(long id, string name, Vector2 location, float mass, int argbColor)
            : base (id, location, mass, argbColor)
        {
            this.name = name;            
        }     
        
	}
}

