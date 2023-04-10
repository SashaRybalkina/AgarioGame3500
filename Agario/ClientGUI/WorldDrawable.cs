using System;
using AgarioModels;

namespace ClientGUI
{
	public class WorldDrawable : IDrawable
	{
        private World worldModel;

        public WorldDrawable(World worldModel)
        {
            this.worldModel = worldModel;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.Red;
            canvas.FillCircle(worldModel.X, worldModel.Y, worldModel.Radius);
        }
    }
}

