using System;
using System.Text.Json;
using AgarioModels;

namespace ClientGUI
{
	public class WorldDrawable : IDrawable
	{
        private World worldModel;

        public WorldDrawable(ref World worldModel)
        {
            this.worldModel = worldModel;
         
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            //canvas.FillColor = Colors.Red;
            //canvas.FillCircle(worldModel.player.location.X, worldModel.player.Y, worldModel.player.Radius);
            foreach (Food food in worldModel.foods)
            {
                canvas.FillColor = Color.FromInt(food.ARGBColor);
                canvas.FillCircle(food.X, food.Y, food.Radius);
            }
            foreach (Player player in worldModel.players)
            {
                canvas.FillColor = Color.FromInt(player.ARGBColor);
                canvas.FillCircle(player.X, player.Y, player.Radius);
            }
        }
    }
}