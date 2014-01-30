using System;
using MonsterMaze.GameModel;
using MonsterMaze.GameModel.Tiles;

namespace MonsterMaze.ConsoleView
{
	public sealed class View : IView
	{
		private readonly IModel _model;
		private readonly int _yDimension;
		private readonly int _xDimension;
	
		public View(IModel model)
		{
			_model = model;
			_yDimension = model.Height;
			_xDimension = model.Width;
		}

		public void Render()
		{
			Console.Clear();

			for (var y = 0; y <= _yDimension; y++)
			{
				for (var x = 0; x <= _xDimension; x++)
				{
					var mobile = _model.Mobiles[x,y];
					if (mobile != null)
						Draw(mobile);
					else
						Draw(_model.Statics[x,y]);
				}
				Console.WriteLine();
			}

			Console.ForegroundColor = ConsoleColor.White;
		}

		private static void Draw(Tile tile)
		{
			var sprite = SpriteSheet.Sprites[tile.Token];
			Console.ForegroundColor = sprite.Item2;
			Console.Write(sprite.Item1);
		}
	}
}