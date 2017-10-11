
using UnityEngine;


public partial class CrossMovementController : MonoBehaviour
{
	[SerializeField] protected MovementType _movementType;


	public virtual void Init(GameController gameController, MovementType movementType)
	{
		_gameController = gameController;
		_movementType = movementType;
	}


	protected void UpdateRenderer()
	{
		_renderer.UpdateColor(_movementType);
	}


	public int GetScore()
	{
		if (!Constants.SCORE.ContainsKey(_movementType))
			throw new System.Exception(_movementType + " don't have score!");

		return Constants.SCORE[_movementType];
	}
}
