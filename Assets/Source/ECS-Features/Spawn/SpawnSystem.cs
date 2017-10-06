
using Entitas;
using UnityEngine;

public class SpawnSystem : IInitializeSystem, IExecuteSystem
{
	private MovementContext _context;

	private float _lastSpawn;


	public void Initialize()
	{
		_context = Contexts.sharedInstance.movement;
		_lastSpawn = Constants.SPAWN_INTERVAL;
	}


	public void Execute()
	{
		_lastSpawn += Time.deltaTime;

		if (_lastSpawn > Constants.SPAWN_INTERVAL)
		{
			_lastSpawn -= Constants.SPAWN_INTERVAL;

			var mover = _context.CreateEntity();
			mover.AddPosition(-Constants.BORDER_X * (Random.value > .5f ? -1f : 1f), Constants.BORDER_Y);
			mover.AddMovementType(Random.value > .5f ? MovementType.Fast : MovementType.Static);
		}
	}
}
