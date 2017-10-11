
using UnityEngine;


public partial class CrossMovementController : MonoBehaviour
{
	[SerializeField] protected MovementType _movementType;


	public virtual void Init(PoolController poolController, MovementType movementType)
	{
		_poolController = poolController;
		_movementType = movementType;
		UpdateRenderer();
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
