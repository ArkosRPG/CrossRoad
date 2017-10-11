
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CollisionsController : MonoBehaviour
{
	[SerializeField] ScoreController _scoreController;


	private HashSet<CrossWrap> _crosses = new HashSet<CrossWrap>();


	public void Add(CrossWrap wrap)
	{
		_crosses.Add(wrap);
	}


	public bool IsGameOver()
	{
		var isRestartNeeded = false;

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

					isRestartNeeded |= _scoreController.FinalScoreSaving();
				}
			}
		}

		return isRestartNeeded;
	}
}
