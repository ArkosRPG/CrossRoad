
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public partial class GameController : MonoBehaviour
{
	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private Transform _root;

	private HashSet<Wrap> _pool = new HashSet<Wrap>();


	private void InstantiatePlayer()
	{
		var go = Instantiate(_playerPrefab, new Vector3(Constants.PLAYER_INITIAL_X, Constants.PLAYER_INITIAL_Y, 0f), Quaternion.identity, _root);
		var pc = go.GetComponent<PlayerController>();
		pc.Init(this, MovementType.Player, Constants.PLAYER_INITIAL_STEER);

		AddToCollisionList(new Wrap(go, pc.transform, pc));
	}


	private Wrap InstantiateEnemy()
	{
		var go = Instantiate(_enemyPrefab, _root);
		var cmc = go.GetComponent<CrossMovementController>();

		var wrap = new Wrap(go, cmc.transform, cmc);
		AddToCollisionList(wrap);

		return wrap;
	}


	private void InitEnemy(Wrap wrap)
	{
		wrap.Tf.position = new Vector3(-Constants.BORDER_X * (Random.value > .5f ? -1f : 1f), Constants.BORDER_Y, 0f);
		wrap.CMC.Init(this, Random.value > .5f ? MovementType.Fast : MovementType.Static);
	}


	private void Pull()
	{
		Wrap wrap;
		if (_pool.Count > 0)
		{
			wrap = _pool.First();
			wrap.GO.SetActive(true);
			_pool.Remove(wrap);
		}
		else
		{
			wrap = InstantiateEnemy();
		}
		InitEnemy(wrap);
	}


	public void ReportFree(GameObject go, Transform tf, CrossMovementController cmc)
	{
		Push(new Wrap(go, tf, cmc));
		_scoreController.GatherScore(cmc);
	}


	private void Push(Wrap wrap)
	{
		wrap.GO.SetActive(false);
		_pool.Add(wrap);
	}
}
