
using UnityEngine;


public class GameController : MonoBehaviour
{
	[SerializeField] private FadeController _fadeController;
	[SerializeField] private PoolController _poolController;
	[SerializeField] private ScoreController _scoreController;
	[SerializeField] private CollisionsController _collisionsController;


	private float _lastSpawn = Constants.SPAWN_INTERVAL;


	private void Start()
	{
		if (_scoreController == null)
			_scoreController = FindObjectOfType<ScoreController>();

		_poolController.InstantiatePlayer();
	}


	private void Update()
	{
		if (!_fadeController.Started)
			return;

		// Spawn
		_lastSpawn += Time.deltaTime;

		if (_lastSpawn > Constants.SPAWN_INTERVAL)
		{
			_lastSpawn -= Constants.SPAWN_INTERVAL;
			_poolController.Pull();
		}

		// Collision
		if (_collisionsController.IsGameOver())
			_fadeController.Restart();
	}
}
