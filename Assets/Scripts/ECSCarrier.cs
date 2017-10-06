
using System.Collections.Generic;
using UnityEngine;


public class ECSCarrier : MonoBehaviour
{
	public static Dictionary<MovementType, float> Speeds = new Dictionary<MovementType, float>()
	{
		{ MovementType.Player   , 0.0f },
		{ MovementType.Jump     , 0.0f },
		{ MovementType.GameOver , 0.5f },
		{ MovementType.Static   , 1.5f },
		{ MovementType.Fast     , 3.0f },
	};


	private Contexts _contexts;
	private MovementContext _movementContext;
	private Feature _movementSystems;
	private Feature _collisionSystems;
	private Feature _inputSystems;


	private void Awake()
	{
		Application.targetFrameRate = 30;
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


		//initial test spawn
		var input = Contexts.sharedInstance.input.CreateEntity();
		input.AddInput(InputType.Lock);

		var playerEntity = _movementContext.CreateEntity();
		playerEntity.AddPosition(0f, -3.5f);
		playerEntity.AddMovementType(MovementType.Player);
		playerEntity.AddSteer(-1f);

		var staticEnemy = _movementContext.CreateEntity();
		staticEnemy.AddPosition(1.25f, 3.5f);
		staticEnemy.AddMovementType(MovementType.Static);

		var fastEnemy = _movementContext.CreateEntity();
		fastEnemy.AddPosition(-1.25f, 7f);
		fastEnemy.AddMovementType(MovementType.Fast);
	}


	private void Update()
	{
		_inputSystems.Execute();
		_inputSystems.Cleanup();

		_movementSystems.Execute();
		_movementSystems.Cleanup();

		_collisionSystems.Execute();
		_collisionSystems.Cleanup();
	}


	private void OnDestroy()
	{
		_movementSystems.TearDown();
		_collisionSystems.TearDown();
	}
}
