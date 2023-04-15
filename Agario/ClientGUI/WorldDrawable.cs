using AgarioModels;
using Font = Microsoft.Maui.Graphics.Font;

namespace ClientGUI
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
    ///		This class functions as a helper class to draw the foods and players on the
    ///		game world. 
    /// </summary>
    public class WorldDrawable : IDrawable
	{
        private World worldModel;

        /// <summary>
        /// Default constructor, initiates the world object.
        /// </summary>
        /// <param name="worldModel"></param>
        public WorldDrawable(ref World worldModel)
        {
            this.worldModel = worldModel;
        }

        /// <summary>
        /// Draws the game objects. 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="dirtyRect"></param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            foreach (Food food in worldModel.foods)
            {
                // calculates the food's position on the screen
                float screenX = food.X - worldModel.player.X + 800 / 2;
                float screenY = food.Y - worldModel.player.Y + 800 / 2;
                // draws within the boundaries
                if (screenX > 0 && screenX < 800 && screenY > 0 && screenY < 800)
                {
                    canvas.FillColor = Color.FromInt(food.ARGBColor);
                    canvas.FillCircle(screenX, screenY, food.Radius);
                }

            }
            foreach (Player player in worldModel.players)
            {
                // calculates the player's position on the screen
                float screenX = player.X - worldModel.player.X + 800 / 2;
                float screenY = player.Y - worldModel.player.Y + 800 / 2;
                // draws within the boundaries
                if (screenX > 0 && screenX < 800 && screenY > 0 && screenY < 800)
                {
                    canvas.FillColor = Color.FromInt(player.ARGBColor);
                    canvas.FillCircle(screenX, screenY, player.Radius);
                }
            }

            // draws the "Player" label on the screen
            canvas.StrokeColor = Colors.Red;
            canvas.Font = Font.Default;
            canvas.FontColor = Colors.Black;
            canvas.FontSize = 20;
            canvas.DrawString("Player", 50, 50, HorizontalAlignment.Left);
        }
    }
}