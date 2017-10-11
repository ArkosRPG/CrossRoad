
using System.Collections.Generic;
using UnityEngine;


public partial class GameController : MonoBehaviour
{
	private bool _started = false;

	private float _lastSpawn = Constants.SPAWN_INTERVAL;

	private HashSet<Wrap> _crosses;
	private HashSet<Wrap> _pool;

	private bool _scoring = true;
	private int _score = 0;
	private int _hiScore = 0;


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
		var txt = string.Format("Score: {0}\nBest: {1}", _score, _hiScore);
		foreach (var text in _scoreTexts)
		{
			text.text = txt;
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
