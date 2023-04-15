using System.Text.Json.Serialization;

namespace AgarioModels
{
    /// <summary>
    /// Author:    Aurora Zuo 
    /// Partner:   Sasha Rybalkina
    /// Date:      14-Apr-2023
    /// Course:    CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500, Aurora Zuo, and Zhuofei Lyu - This work not 
    ///            be copied for use in Academic Coursework.
    ///            
    /// Aurora Zuo and Sasha Rybalkina certify that we wrote this code from scratch and
    /// did not copy it in part or whole from another source. All 
    /// references used in the completion of the assignments are cited 
    /// in our README file.
    /// 
    /// File Content
    ///		This class is a subclass of the GameObject, stores the Food object. 
    /// </summary>
    public class Food : GameObject
	{
        /// <summary>
        /// Takes in X and Y positions, ARGB color, ID, and Mass as parameters.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="ARGBColor"></param>
        /// <param name="ID"></param>
        /// <param name="Mass"></param>
		[JsonConstructor]      
		public Food(float X, float Y, int ARGBColor, long ID, float Mass)
			: base(ID, X, Y, Mass, ARGBColor)
		{
			
		}
	}
}

