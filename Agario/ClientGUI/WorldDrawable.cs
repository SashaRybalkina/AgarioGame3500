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
            for (int i = 0; i < worldModel.foods.Count(); i++)
            {
                canvas.FillColor = Color.FromArgb(worldModel.foods[i].ARGBcolor + "");
                canvas.FillCircle(worldModel.foods[i].X, worldModel.foods[i].Y, worldModel.foods[i].Radius);
            }                        
        }
    }
}

