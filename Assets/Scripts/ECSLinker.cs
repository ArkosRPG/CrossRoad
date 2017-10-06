
using System;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class ECSLinker : MonoBehaviour
{
	private Transform _tf;

	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject _enemyPrefab;

	private Dictionary<MovementEntity, CrossRenderer> _renderers;

	private Contexts _contexts;
	private MovementContext _movementContext;


	void Start()
	{
		_tf = transform;

		_renderers = new Dictionary<MovementEntity, CrossRenderer>();

		_contexts = Contexts.sharedInstance;
		_movementContext = _contexts.movement;

		_movementContext.OnEntityCreated += MovementContext_OnEntityCreated;
		_movementContext.OnEntityDestroyed += MovementContext_OnEntityDestroyed;
	}


	private void MovementContext_OnEntityCreated(IContext context, IEntity entity)
	{
		var movementEntity = entity as MovementEntity;
		if (movementEntity == null)
			throw new NotImplementedException("OnCreate: wrong entity type: " + entity.GetType());

		StartCoroutine(MovementContext_OnEntityCreated_Coroutine(movementEntity));
	}


	private IEnumerator MovementContext_OnEntityCreated_Coroutine(MovementEntity movementEntity)
	{
		yield return new WaitForEndOfFrame();

		var positionComponent = movementEntity.position;
		var prefab = movementEntity.movementType.Value == MovementType.Player ? _playerPrefab : _enemyPrefab;
		var go = Instantiate(prefab, positionComponent.GetVector3(), Quaternion.identity, _tf);

		var renderer = go.GetComponent<CrossRenderer>();
		if (renderer == null)
			throw new MissingComponentException(typeof(CrossRenderer) + " doesn't present");

		_renderers.Add(movementEntity, renderer);

		yield return new WaitForEndOfFrame();

		renderer.UpdateColor(movementEntity.movementType.Value == MovementType.Player ? Color.white : Color.black);
	}


	private void MovementContext_OnEntityDestroyed(IContext context, IEntity entity)
	{
		var movementEntity = entity as MovementEntity;
		if (movementEntity == null)
			throw new NotImplementedException("OnDestroy: wrong entity type: " + entity.GetType());

		if (!_renderers.ContainsKey(movementEntity))
			throw new NullReferenceException("OnDestroy: entity doesn't have linked renderer");

		_renderers.Remove(movementEntity);
	}


	private void Update()
	{
		PositionComponent pos;
		foreach (var pair in _renderers)
		{
			pos = pair.Key.position;
			pair.Value.UpdatePosition(pos);
		}
	}
}
