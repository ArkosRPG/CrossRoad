
using UnityEngine;


public class ECSCarrier : MonoBehaviour
{
	private Contexts _contexts;
	private MovementContext _movementContext;
	private Feature _movementSystems;
	private Feature _collisionSystems;
	private Feature _inputSystems;
	private Feature _spawnSystems;


	private void Awake()
	{
		Application.targetFrameRate = Constants.TARGET_FRAME_RATE;
	}


	private void Start()
	{
		_contexts = Contexts.sharedInstance;
		_movementContext = _contexts.movement;

		_movementSystems = new Feature("Movement");
		_movementSystems.Add(new MovementSystem());
		_movementSystems.Initialize();

		_collisionSystems = new Feature("Collision");
		_collisionSystems.Add(new CollisionSystem());
		_collisionSystems.Initialize();

		_inputSystems = new Feature("Input");
		_inputSystems.Add(new InputSystem());
		_inputSystems.Initialize();

		_spawnSystems = new Feature("Spawn");
		_spawnSystems.Add(new SpawnSystem());
		_spawnSystems.Initialize();


		var input = Contexts.sharedInstance.input.CreateEntity();
		input.AddInput(InputType.Lock);

		var playerEntity = _movementContext.CreateEntity();
		playerEntity.AddPosition(Constants.PLAYER_INITIAL_X, Constants.PLAYER_INITIAL_Y);
		playerEntity.AddMovementType(MovementType.Player);
		playerEntity.AddSteer(Constants.PLAYER_INITIAL_STEER);
	}


	private void Update()
	{
		_inputSystems.Execute();
		_inputSystems.Cleanup();

		_movementSystems.Execute();
		_movementSystems.Cleanup();

		_collisionSystems.Execute();
		_collisionSystems.Cleanup();

		_spawnSystems.Execute();
		_spawnSystems.Cleanup();
	}


	private void OnDestroy()
	{
		_movementSystems.TearDown();
		_collisionSystems.TearDown();
		_spawnSystems.TearDown();
	}
}
