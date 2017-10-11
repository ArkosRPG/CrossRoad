
using System;


public partial class PlayerController : CrossMovementController
{
	[Obsolete("Use with switch instead. Exists for end of game where player can be spawned as enemy.", true)]
	public override void Init(PoolController poolController, MovementType movementType)
	{
		base.Init(poolController, movementType);
	}


	public void Init(PoolController poolController, MovementType movementType, Switch steering)
	{
		base.Init(poolController, movementType);
		_steering = steering;
	}
}
