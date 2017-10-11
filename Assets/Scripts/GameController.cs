
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private Transform _root;
	[SerializeField] private Text[] _scoreTexts;

	private float _lastSpawn = Constants.SPAWN_INTERVAL;

	private HashSet<Wrap> _crosses;
	private HashSet<Wrap> _pool;

	private bool _scoring = true;
	private int _score = 0;


	void Start()
	{
		var go = Instantiate(_playerPrefab, new Vector3(Constants.PLAYER_INITIAL_X, Constants.PLAYER_INITIAL_Y, 0f), Quaternion.identity, _root);
		var pc = go.GetComponent<PlayerController>();
		pc.Init(this, MovementType.Player, Constants.PLAYER_INITIAL_STEER);

		_crosses = new HashSet<Wrap>();
		_crosses.Add(new Wrap(go, pc.transform, pc));

		_pool = new HashSet<Wrap>();
	}


	void Update()
	{
		// Spawn
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
				var cmc = go.GetComponent<CrossMovementController>();
				cmc.Init(this, Random.value > .5f ? MovementType.Fast : MovementType.Static);
				_crosses.Add(new Wrap(go, go.transform, cmc));
			}
		}

		//Collisions
		var crushers = _crosses.Where(c => c.CMC.IsCrusher());
		var obstacles = _crosses.Where(c => c.CMC.IsObstacle());

		foreach (var obstacle in obstacles)
		{
			foreach (var crusher in crushers)
			{
				var delta = obstacle.Tf.position - crusher.Tf.position;
				var X = Mathf.Abs(delta.x);
				var Y = Mathf.Abs(delta.y);

				if ((X < Constants.CROSS_BORDER_1 && Y < Constants.CROSS_BORDER_3) ||
					(X < Constants.CROSS_BORDER_2 && Y < Constants.CROSS_BORDER_2) ||
					(X < Constants.CROSS_BORDER_3 && Y < Constants.CROSS_BORDER_1))
				{
					obstacle.CMC.ReportCollision();
					crusher.CMC.ReportCollision();
				}
			}
		}
	}


	public void ReportFree(GameObject go, Transform tf, CrossMovementController cmc)
	{
		if (_scoring)
		{
			_score += cmc.GetScore();
			UpdateScore();
		}

		go.SetActive(false);
		_pool.Add(new Wrap(go, tf, cmc));
	}


	private void UpdateScore()
	{
		foreach (var text in _scoreTexts)
		{
			text.text = "Score: " + _score.ToString();
		}
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
