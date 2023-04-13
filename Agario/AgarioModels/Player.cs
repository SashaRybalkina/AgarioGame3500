using System;
using System.Numerics;

namespace AgarioModels
{
	public class Player : GameObject
	{
        private string name { get; }

        
        public Player(long ID, string name, float X, float Y, float Mass, int ARGBColor)
            : base (ID, X, Y, Mass, ARGBColor)
        {
            this.name = name;            
        }     
        
	}
}

