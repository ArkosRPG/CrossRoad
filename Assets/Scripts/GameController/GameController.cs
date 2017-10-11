
using UnityEngine;


public partial class GameController : MonoBehaviour
{
	[SerializeField] private ScoreController _scoreController;
	[SerializeField] private CollisionsController _collisionsController;


	private float _lastSpawn = Constants.SPAWN_INTERVAL;


	private void Start()
	{
		if (_scoreController == null)
			_scoreController = FindObjectOfType<ScoreController>();

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
		if (_collisionsController.IsGameOver())
			StartCoroutine(RestartCoroutine());
	}
}
