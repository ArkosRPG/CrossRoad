
using System.Collections.Generic;
using UnityEngine;


public partial class GameController : MonoBehaviour
{
	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private Transform _root;

	private HashSet<Wrap> _crosses;
	private HashSet<Wrap> _pool;


	private void InstantiatePlayer()
	{
		var go = Instantiate(_playerPrefab, new Vector3(Constants.PLAYER_INITIAL_X, Constants.PLAYER_INITIAL_Y, 0f), Quaternion.identity, _root);
		var pc = go.GetComponent<PlayerController>();
		pc.Init(this, MovementType.Player, Constants.PLAYER_INITIAL_STEER);

		_crosses.Add(new Wrap(go, pc.transform, pc));
	}


	private void InstantiateEnemy()
	{
		var go = Instantiate(_playerPrefab, new Vector3(Constants.PLAYER_INITIAL_X, Constants.PLAYER_INITIAL_Y, 0f), Quaternion.identity, _root);
		var pc = go.GetComponent<PlayerController>();
		pc.Init(this, MovementType.Player, Constants.PLAYER_INITIAL_STEER);

		_crosses.Add(new Wrap(go, pc.transform, pc));
	}


	public void ReportFree(GameObject go, Transform tf, CrossMovementController cmc)
	{
		PutInPool(new Wrap(go, tf, cmc));
		GatherScore(cmc);
	}


	private void PutInPool(Wrap wrap)
	{
		wrap.GO.SetActive(false);
		_pool.Add(wrap);
	}
}
