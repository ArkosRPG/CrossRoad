
using UnityEngine;


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
			Pull();
		}

		// Collision
		Update_Collisions();
	}
}
