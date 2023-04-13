using System;
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
            canvas.FillColor = Colors.Red;
            canvas.FillCircle(worldModel.player.location.X, worldModel.player.Y, worldModel.player.Radius);
            foreach (Food food in worldModel.foods)
            {
                object var = Color.FromArgb(food.ARGBColor);
                canvas.FillColor = Color.FromArgb(food.ARGBColor.ToString());
                canvas.FillCircle(food.X, food.Y, food.Radius);
            }
            foreach (Player player in worldModel.players)
            {
                canvas.FillColor = Color.FromArgb(player.ARGBColor.ToString());
                canvas.FillCircle(player.X, player.Y, player.Radius);
            }
        }
    }
}