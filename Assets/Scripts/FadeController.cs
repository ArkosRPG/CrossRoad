
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadeController : MonoBehaviour
{
	[SerializeField] private Image _curtain;
	[SerializeField] private Transform _scoreRoot;


	public bool Started { get; private set; } = false;


	private void Start()
	{
		StartCoroutine(StartCoroutine());
	}
	private IEnumerator StartCoroutine()
	{
		var start = DateTime.UtcNow;
		var progress = 0f;
		while (progress < 1f)
		{
			progress = (float)(DateTime.UtcNow - start).TotalSeconds / Constants.START_PERIOD;
			_curtain.color = new Color(0f, 0f, 0f, 1f - progress);
			yield return new WaitForEndOfFrame();
		}
		_curtain.color = new Color(0f, 0f, 0f, 0f);
		Started = true;
	}


	public void Restart()
	{
		StartCoroutine(RestartCoroutine());
	}
	private IEnumerator RestartCoroutine()
	{
		var start = DateTime.UtcNow;
		var delta = Constants.BORDER_Y / 2 / Constants.RESTART_PERIOD;
		Vector3 pos;
		var progress = 0f;
		while (progress < 1f)
		{
			progress = (float)(DateTime.UtcNow - start).TotalSeconds / Constants.RESTART_PERIOD;
			_curtain.color = new Color(0f, 0f, 0f, progress);
			pos = _scoreRoot.position;
			_scoreRoot.position = new Vector3(pos.x, pos.y - delta * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		SceneManager.LoadScene(0);
	}
}
