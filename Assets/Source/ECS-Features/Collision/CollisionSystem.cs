﻿
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;


public class CollisionSystem : ReactiveSystem<MovementEntity>, ISystem
{
	private MovementContext _context;
	private IGroup<MovementEntity> _potentialCrushersGroup = null;
	private MovementEntity[] _obstacles = null;


	public CollisionSystem(IContext<MovementEntity> context = null) : base(context)
	{
		_potentialCrushersGroup = _context.GetGroup(MovementMatcher.AllOf(
											MovementMatcher.Position,
											MovementMatcher.MovementType)
											);
	}


	protected override bool Filter(MovementEntity entity)
	{
		return entity.movementType.Value > MovementType.Jump;
	}


	protected override ICollector<MovementEntity> GetTrigger(IContext<MovementEntity> context)
	{
		_context = Contexts.sharedInstance.movement;
		return new Collector<MovementEntity>(_context.GetGroup(MovementMatcher.AllOf(
																MovementMatcher.Position,
																MovementMatcher.MovementType)
																),
											GroupEvent.AddedOrRemoved);
	}


	protected override void Execute(List<MovementEntity> entities)
	{
		_obstacles = _potentialCrushersGroup.GetEntities().Where(e => e.movementType.Value < MovementType.Jump).ToArray();

		foreach (var obstacle in _obstacles)
		{
			foreach (var crusher in entities)
			{
				var delta = obstacle.position.GetVector2() - crusher.position.GetVector2();
				var X = Mathf.Abs(delta.x);
				var Y = Mathf.Abs(delta.y);

				if ((X < 0.67f && Y < 2.00f) ||
					(X < 1.33f && Y < 1.33f) ||
					(X < 2.00f && Y < 0.67f))
				{
					obstacle.ReplaceMovementType(MovementType.GameOver);
					crusher.ReplaceMovementType(MovementType.GameOver);
				}
			}
		}

		_obstacles = null;
	}
}
