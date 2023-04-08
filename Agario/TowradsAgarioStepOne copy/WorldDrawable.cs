using System;
namespace TowradsAgarioStepOne
{
	public class WorldDrawable : IDrawable
	{

        private WorldModel worldModel;

		public WorldDrawable(WorldModel worldModel)
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

