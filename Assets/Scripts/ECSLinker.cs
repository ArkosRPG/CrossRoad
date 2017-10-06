
using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class ECSLinker : MonoBehaviour
{
	private Transform _tf;

	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject _enemyPrefab;

	private Dictionary<MovementEntity, Transform> _renderers;

	private Contexts _contexts;
	private MovementContext _movementContext;


	void Start()
	{
		_tf = transform;

		_renderers = new Dictionary<MovementEntity, Transform>();

		_contexts = Contexts.sharedInstance;
		_movementContext = _contexts.movement;

		_movementContext.OnEntityCreated += MovementContext_OnEntityCreated;
		_movementContext.OnEntityDestroyed += MovementContext_OnEntityDestroyed;
	}


	private void MovementContext_OnEntityCreated(IContext context, IEntity entity)
	{
		var movementEntity = entity as MovementEntity;
		if (movementEntity == null)
			throw new System.NotImplementedException("OnCreate: wrong entity type: " + entity.GetType());

		StartCoroutine(MovementContext_OnEntityCreated_Coroutine(movementEntity));
	}


	private IEnumerator MovementContext_OnEntityCreated_Coroutine(MovementEntity movementEntity)
	{
		yield return new WaitForEndOfFrame();
		var pos = movementEntity.position;
		var prefab = movementEntity.movementType.Value == MovementType.Player ? _playerPrefab : _enemyPrefab;
		var go = Instantiate(prefab, GetPositionFromComponent(pos), Quaternion.identity, _tf);
		_renderers.Add(movementEntity, go.transform);
	}


	private void MovementContext_OnEntityDestroyed(IContext context, IEntity entity)
	{
		var movementEntity = entity as MovementEntity;
		if (movementEntity == null)
			throw new System.NotImplementedException("OnDestroy: wrong entity type: " + entity.GetType());

		if (!_renderers.ContainsKey(movementEntity))
			throw new System.NotImplementedException("OnDestroy: entity doesn't have linked renderer");

		_renderers.Remove(movementEntity);
	}


	private void Update()
	{
		PositionComponent pos;
		foreach (var pair in _renderers)
		{
			pos = pair.Key.position;
			pair.Value.position = GetPositionFromComponent(pos);
		}
	}


	private Vector3 GetPositionFromComponent(PositionComponent pos)
	{
		return new Vector3(pos.X, pos.Y, 1f);
	}
}
