
using System;


public partial class PlayerController : CrossMovementController
{
	[Obsolete("Use with switch instead. Exists for end of game where player can be spawned as enemy.", true)]
	public override void Init(GameController gameController, MovementType movementType)
	{
		base.Init(gameController, movementType);
	}


	public void Init(GameController gameController, MovementType movementType, Switch steering)
	{
		base.Init(gameController, movementType);
		_steering = steering;
	}
}
