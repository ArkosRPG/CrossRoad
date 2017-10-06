
using Entitas;
using UnityEngine;


public class MovementSystem : IInitializeSystem, IExecuteSystem
{
	private MovementContext _context;
	IGroup<MovementEntity> _movers;


	public void Initialize()
	{
		_context = Contexts.sharedInstance.movement;
		_movers = _context.GetGroup(MovementMatcher.AllOf(MovementMatcher.Position, MovementMatcher.MovementType));
	}


	public void Execute()
	{
		var deltaTime = Time.deltaTime;
		foreach (var obj in _movers.GetEntities())
		{
			var type = obj.movementType.Value;
			var delta = 0f;
			if (ECSCarrier.Speeds.ContainsKey(type))
				delta = ECSCarrier.Speeds[type] * deltaTime;

			var X = obj.position.X;
			var Y = obj.position.Y - delta;

			if (obj.hasSteer)
				X += obj.steer.Value * deltaTime;

			if (X < -1.25f)
			{
				X = -1.25f;
				obj.ReplaceSteer(0f);
			}
			else
			if (X > 1.25f)
			{
				X = 1.25f;
				obj.ReplaceSteer(0f);
			}

			if (X != obj.position.X || Y != obj.position.Y)
				obj.ReplacePosition(X, Y);
		}
	}
}
