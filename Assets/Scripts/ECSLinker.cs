
using System;
using System.Collections;
using Entitas;
using UnityEngine;


public class ECSLinker : MonoBehaviour
{
	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private Transform _root;


	private Contexts _contexts;
	private MovementContext _movementContext;


	private void Start()
	{
		//_root = transform;

		_contexts = Contexts.sharedInstance;
		_movementContext = _contexts.movement;

		_movementContext.OnEntityCreated += MovementContext_OnEntityCreated;
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
		var go = Instantiate(prefab, positionComponent.GetVector3(), Quaternion.identity, _root);

		var renderer = go.GetComponent<CrossRenderer>();
		if (renderer == null)
		{
			Destroy(go);
			Debug.LogErrorFormat("{0} doesn't present", typeof(CrossRenderer));
		}

		renderer.Init(movementEntity);
	}
}
