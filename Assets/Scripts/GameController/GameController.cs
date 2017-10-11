
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public partial class GameController : MonoBehaviour
{
	private float _lastSpawn = Constants.SPAWN_INTERVAL;


	private void Start()
	{
		if (PlayerPrefs.HasKey(Constants.PP_SCORE))
		{
			_hiScore = PlayerPrefs.GetInt(Constants.PP_SCORE, _score);
			UpdateScore();
		}

		_crosses = new HashSet<Wrap>();
		_pool = new HashSet<Wrap>();

		InstantiatePlayer();


		StartCoroutine(StartCoroutine());
	}


	private void Update()
	{
		if (!_started)
			return;

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

					if (_scoring)
					{
						_scoring = false;
						_hiScore = Mathf.Max(_hiScore, _score);
						PlayerPrefs.SetInt(Constants.PP_SCORE, _hiScore);
						PlayerPrefs.Save();

						StartCoroutine(RestartCoroutine());
					}
				}
			}
		}
	}
}
