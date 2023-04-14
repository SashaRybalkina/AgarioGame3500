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
                float screenX = food.X - worldModel.player.X + 800/2;
                float screenY = food.Y - worldModel.player.Y + 800/2;
                if (screenX > 0 && screenX < 800 && screenY > 0 && screenY < 800)
                {
                    if (!worldModel.eaten.Contains(food))
                    {
                        canvas.FillColor = Color.FromInt(food.ARGBColor);
                        canvas.FillCircle(screenX, screenY, food.Radius);
                    }
                }
            }

            foreach (Player player in worldModel.players)
            {
                float screenX = player.X - worldModel.player.X + 800 / 2;
                float screenY = player.Y - worldModel.player.Y + 800 / 2;
                if (screenX > 0 && screenX < 800 && screenY > 0 && screenY < 800)
                {
                    canvas.FillColor = Color.FromInt(player.ARGBColor);
                    canvas.FillCircle(screenX, screenY, player.Radius);
                }
            }
        }
    }
}