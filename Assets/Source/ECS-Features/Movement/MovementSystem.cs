
using Entitas;
using UnityEngine;


public class MovementSystem : IInitializeSystem, IExecuteSystem
{
	private MovementContext _context;
	IGroup<MovementEntity> _movers;
	private InputContext _inputContext;


	public void Initialize()
	{
		var contexts = Contexts.sharedInstance;
		_context = contexts.movement;
		_movers = _context.GetGroup(MovementMatcher.AllOf(MovementMatcher.Position, MovementMatcher.MovementType));
		_inputContext = contexts.input;
	}


	public void Execute()
	{
		var deltaTime = Time.deltaTime;
		foreach (var obj in _movers.GetEntities())
		{
			if (obj.hasJumpTimer)
			{
				var newTimer = obj.jumpTimer.Left - deltaTime;
				if (newTimer > 0f)
				{
					obj.ReplaceJumpTimer(newTimer);
				}
				else
				{
					obj.RemoveJumpTimer();
					obj.ReplaceMovementType(MovementType.Player);
				}
			}


			var type = obj.movementType.Value;
			var delta = 0f;
			if (ECSCarrier.Speeds.ContainsKey(type))
				delta = ECSCarrier.Speeds[type] * deltaTime;

			var X = obj.position.X;
			var Y = obj.position.Y - delta;

			if (obj.hasSteer)
				X += obj.steer.Value * deltaTime;

			if (X < -1.26f)
			{
				X = -1.25f;
				obj.ReplaceSteer(0f);
				var input = _inputContext.CreateEntity();
				input.AddInput(InputType.Unlock);
			}
			else
			if (X > 1.26f)
			{
				X = 1.25f;
				obj.ReplaceSteer(0f);
				var input = _inputContext.CreateEntity();
				input.AddInput(InputType.Unlock);
			}

			if (X != obj.position.X || Y != obj.position.Y)
				obj.ReplacePosition(X, Y);
		}
	}
}
