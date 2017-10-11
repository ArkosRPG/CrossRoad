
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public partial class GameController : MonoBehaviour
{
	private HashSet<Wrap> _crosses = new HashSet<Wrap>();


	private void AddToCollisionList(Wrap wrap)
	{
		_crosses.Add(wrap);
	}


	private void Update_Collisions()
	{
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
