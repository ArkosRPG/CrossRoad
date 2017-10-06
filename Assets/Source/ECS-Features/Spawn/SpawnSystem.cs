
using Entitas;
using UnityEngine;

public class SpawnSystem : IInitializeSystem, IExecuteSystem
{
	private MovementContext _context;

	private float SPAWN_INTERVAL = 3f;
	private float _lastSpawn;


	public void Initialize()
	{
		_context = Contexts.sharedInstance.movement;
		_lastSpawn = SPAWN_INTERVAL;
	}


	public void Execute()
	{
		_lastSpawn += Time.deltaTime;

		if (_lastSpawn > SPAWN_INTERVAL)
		{
			_lastSpawn -= SPAWN_INTERVAL;

			var mover = _context.CreateEntity();
			mover.AddPosition(-1.25f * (Random.value > .5f ? -1f : 1f), 7f);
			mover.AddMovementType(Random.value > .5f ? MovementType.Fast : MovementType.Static);
		}
	}
}
