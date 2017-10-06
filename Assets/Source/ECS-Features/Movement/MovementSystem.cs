
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
		foreach (var obj in _movers.GetEntities())
		{
			var type = obj.movementType.Value;
			var delta = 0f;
			if (ECSCarrier.Speeds.ContainsKey(type))
				delta = ECSCarrier.Speeds[type] * Time.deltaTime;

			if (delta != 0f)
				obj.ReplacePosition(obj.position.X, obj.position.Y - delta);
		}
	}
}
