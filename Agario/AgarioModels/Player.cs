using System;
using System.Numerics;

namespace AgarioModels
{
	public class Player : GameObject
	{
        private string Name { get; set; }

        
        public Player(string Name, float X, float Y, int ARGBColor, long ID, float Mass)
            : base (ID, X, Y, Mass, ARGBColor)
        {
            this.Name = Name;            
        }     
        
	}
}

