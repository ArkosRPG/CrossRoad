﻿
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
			if (Constants.SPEEDS.ContainsKey(type))
				delta = Constants.SPEEDS[type] * deltaTime;


			var X = obj.position.X;
			var Y = obj.position.Y - delta;
			if (Y < -Constants.BORDER_Y)
			{
				obj.Destroy();
				continue;
			}


			if (obj.hasSteer)
				X += obj.steer.Value * deltaTime;

			if (X < -Constants.BORDER_X - .1f)
			{
				X = -Constants.BORDER_X;
				obj.ReplaceSteer(0f);
				var input = _inputContext.CreateEntity();
				input.AddInput(InputType.Unlock);
			}
			else
			if (X > Constants.BORDER_X + .1f)
			{
				X = Constants.BORDER_X;
				obj.ReplaceSteer(0f);
				var input = _inputContext.CreateEntity();
				input.AddInput(InputType.Unlock);
			}

			if (X != obj.position.X || Y != obj.position.Y)
				obj.ReplacePosition(X, Y);
		}
	}
}
