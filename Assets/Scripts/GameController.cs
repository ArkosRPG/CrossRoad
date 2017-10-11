
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private Transform _root;

	private float _lastSpawn = Constants.SPAWN_INTERVAL;

	private HashSet<Wrap> _pool;


	void Start()
	{
		var go = Instantiate(_playerPrefab, new Vector3(Constants.PLAYER_INITIAL_X, Constants.PLAYER_INITIAL_Y, 0f), Quaternion.identity, _root);
		var pc = go.GetComponent<PlayerController>();
		pc.Init(this, MovementType.Player, Constants.PLAYER_INITIAL_STEER);

		_pool = new HashSet<Wrap>();
	}


	void Update()
	{
		_lastSpawn += Time.deltaTime;

		if (_lastSpawn > Constants.SPAWN_INTERVAL)
		{
			_lastSpawn -= Constants.SPAWN_INTERVAL;

			if (_pool.Count > 0)
			{
				var item = _pool.First();
				item.Tf.position = new Vector3(-Constants.BORDER_X * (Random.value > .5f ? -1f : 1f), Constants.BORDER_Y, 0f);
				item.CMC.Init(this, Random.value > .5f ? MovementType.Fast : MovementType.Static);
				item.GO.SetActive(true);
				_pool.Remove(item);
			}
			else
			{
				var go = Instantiate(_enemyPrefab, new Vector3(-Constants.BORDER_X * (Random.value > .5f ? -1f : 1f), Constants.BORDER_Y, 0f), Quaternion.identity, _root);
				var enemy = go.GetComponent<CrossMovementController>();
				enemy.Init(this, Random.value > .5f ? MovementType.Fast : MovementType.Static);
			}
		}
	}


	public void ReportFree(GameObject go, Transform tf, CrossMovementController cmc)
	{
		go.SetActive(false);
		_pool.Add(new Wrap(go, tf, cmc));
	}




	private struct Wrap
	{
		public CrossMovementController CMC;
		public GameObject GO;
		public Transform Tf;


		public Wrap(GameObject go, Transform tf, CrossMovementController cmc)
		{
			GO = go;
			Tf = tf;
			CMC = cmc;
		}
	}
}
