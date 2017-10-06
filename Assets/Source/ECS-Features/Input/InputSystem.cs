
using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;


public class InputSystem : ReactiveSystem<InputEntity>
{
	private InputContext _context;
	private MovementContext _movementContext;
	private IGroup<MovementEntity> _playersGroup = null;


	public InputSystem(IContext<InputEntity> context = null) : base(context)
	{
		_movementContext = Contexts.sharedInstance.movement;
		_playersGroup = _movementContext.GetGroup(MovementMatcher.AllOf(
													MovementMatcher.Position,
													MovementMatcher.MovementType,
													MovementMatcher.Steer)
													);
	}


	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
	{
		_context = Contexts.sharedInstance.input;
		return new Collector<InputEntity>(_context.GetGroup(InputMatcher.Input), GroupEvent.Added);
	}


	protected override bool Filter(InputEntity entity)
	{
		return entity.hasInput;
	}


	protected override void Execute(List<InputEntity> entities)
	{
		var all = _context.GetEntities(InputMatcher.Input);
		if (all.Any(e => e.input.Input == InputType.Unlock))
		{
			foreach (var inputEntity in all)
			{
				inputEntity.Destroy();
			}
			return;
		}
		else
		if (all.Any(e => e.input.Input == InputType.Lock))
		{
			foreach (var inputEntity in all.Where(e => e.input.Input != InputType.Lock))
			{
				inputEntity.Destroy();
			}
			return;
		}


		var players = _playersGroup.GetEntities();
		foreach (var inputEntity in entities)
		{
			if (!inputEntity.hasInput)
				continue;

			switch (inputEntity.input.Input)
			{
				case InputType.SwipeUp:
					foreach (var player in players)
					{
						player.ReplaceMovementType(MovementType.Jump);
					}
					inputEntity.ReplaceInput(InputType.Lock);
					break;
				case InputType.SwipeLeft:
					foreach (var player in players)
					{
						player.ReplaceSteer(-3f);
					}
					inputEntity.ReplaceInput(InputType.Lock);
					break;
				case InputType.SwipeRight:
					foreach (var player in players)
					{
						player.ReplaceSteer(3f);
					}
					inputEntity.ReplaceInput(InputType.Lock);
					break;
				default:
					throw new NotImplementedException();
			}
		}
	}
}
