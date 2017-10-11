
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : MonoBehaviour
{
	[SerializeField] private Text[] _scoreTexts;

	private bool _scoring = true;
	private int _score = 0;
	private int _hiScore = 0;


	private void Start()
	{
		if (PlayerPrefs.HasKey(Constants.PP_SCORE))
		{
			_hiScore = PlayerPrefs.GetInt(Constants.PP_SCORE, _score);
			UpdateScore();
		}
	}


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


	public bool FinalScoreSaving()
	{
		if (!_scoring)
			return false;

		_scoring = false;
		_hiScore = Mathf.Max(_hiScore, _score);
		PlayerPrefs.SetInt(Constants.PP_SCORE, _hiScore);
		PlayerPrefs.Save();
		return true;
	}
}
