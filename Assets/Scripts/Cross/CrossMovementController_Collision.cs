
using UnityEngine;


public partial class CrossMovementController : MonoBehaviour
{
	public bool IsObstacle()
	{
		return _movementType < MovementType.Jump
			&& _movementType > MovementType.OutOfGame;
	}


	public bool IsCrusher()
	{
		return _movementType > MovementType.Jump;
	}


	public void ReportCollision()
	{
		_movementType = MovementType.GameOver;
		UpdateRenderer();
	}
}
