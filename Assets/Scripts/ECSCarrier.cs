﻿
using System.Collections.Generic;
using UnityEngine;


public class ECSCarrier : MonoBehaviour
{
	public static Dictionary<MovementType, float> Speeds = new Dictionary<MovementType, float>()
	{
		{ MovementType.Player,  0f },
		{ MovementType.Static,  5f },
		{ MovementType.Fast  , 10f },
	};


	private Contexts _contexts;
	private MovementContext _movementContext;
	private Feature _movementSystems;


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


		//initial test spawn
		var playerEntity = _movementContext.CreateEntity();
		playerEntity.AddPosition(0f, 0f);
		playerEntity.AddMovementType(MovementType.Player);
		playerEntity.AddSteer(-1f);

		var staticEnemy = _movementContext.CreateEntity();
		staticEnemy.AddPosition(1f, 5f);
		staticEnemy.AddMovementType(MovementType.Static);

		var fastEnemy = _movementContext.CreateEntity();
		fastEnemy.AddPosition(-1f, 10f);
		fastEnemy.AddMovementType(MovementType.Fast);
	}


	private void Update()
	{
		_movementSystems.Execute();
		_movementSystems.Cleanup();
	}


	private void OnDestroy()
	{
		_movementSystems.TearDown();
	}
}
