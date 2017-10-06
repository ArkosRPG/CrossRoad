﻿
using System.Collections.Generic;
using Entitas;
using System;


public class InputSystem : ReactiveSystem<InputEntity>
{
	private InputContext _context;
	private MovementContext _movementContext;
	private IGroup<MovementEntity> _playersGroup = null;


	public InputSystem(IContext<InputEntity> context = null) : base(context)
	{
		_playersGroup = _movementContext.GetGroup(MovementMatcher.AllOf(
											MovementMatcher.Position,
											MovementMatcher.MovementType,
											MovementMatcher.Steer)
											);
	}


	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
	{
		_context = Contexts.sharedInstance.input;
		_movementContext = Contexts.sharedInstance.movement;
		return new Collector<InputEntity>(_context.GetGroup(InputMatcher.Input), GroupEvent.Added);
	}


	protected override bool Filter(InputEntity entity)
	{
		return true;
	}


	protected override void Execute(List<InputEntity> entities)
	{
		var players = _playersGroup.GetEntities();
		foreach (var inputEntity in entities)
		{
			switch (inputEntity.input.Input)
			{
				case Input.Tap:
					foreach (var player in players)
					{
						player.ReplaceMovementType(MovementType.Jump);
					}
					break;
				case Input.SwipeLeft:
					foreach (var player in players)
					{
						player.ReplaceSteer(-3f);
					}
					break;
				case Input.SwipeRight:
					foreach (var player in players)
					{
						player.ReplaceSteer(3f);
					}
					break;
				default:
					throw new NotImplementedException();
			}
		}
		_context.DestroyAllEntities();
	}
}
