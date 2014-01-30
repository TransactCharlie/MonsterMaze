using System;
using System.Linq;
using MonsterMaze.ConsoleController.Input;
using MonsterMaze.ConsoleView;
using MonsterMaze.GameModel;

namespace MonsterMaze.ConsoleController
{
	public class Controller
	{
		private readonly IModel _model;
		private readonly IView _view;

		public Controller(IModel model)
		{
			_model = model;
			_view = new View(_model);
			_model.TurnEnd += _view.Render;
		}

		public void Init()
		{
			_view.Render();

			while (true)
			{
				var message = KeyReader.Read(Console.ReadKey(true));
				
				if (message.Body == "quit") break;
				if (message.Header == "error") continue;

				_model.InputBuffer.Send(message);
				
				if (_model.CurrentTurnMessages != null)
					_model.CurrentTurnMessages.ToList().ForEach(msg=> 
						Console.WriteLine(String.Format("The {0} {1} from {2}, to {3}", msg.Name, msg.Type, msg.From, msg.To)));
			}
		}
	}
}