
using UnityEngine;
using UnityEngine.UI;


public partial class GameController : MonoBehaviour
{
	[SerializeField] private Text[] _scoreTexts;
	[SerializeField] private Transform _scoreRoot;

	private bool _scoring = true;
	private int _score = 0;
	private int _hiScore = 0;


	public void GatherScore(CrossMovementController cmc)
	{
		if (_scoring)
		{
			_score += cmc.GetScore();
			UpdateScore();
		}
	}


	private void UpdateScore()
	{
		var txt = string.Format("Score: {0}\nBest: {1}", _score, _hiScore);
		foreach (var text in _scoreTexts)
		{
			text.text = txt;
		}
	}
}
