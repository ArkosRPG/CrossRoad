
using UnityEngine;


public class PlayerController : CrossMovementController
{
	[HideInInspector] private GameObject _playerObj;
	private CrossRenderer _playerRenderer;

	private float _jumpingLeft = 0f;

	private bool _touch = false;
	private Vector3 _touchPosition = Vector3.zero;


	protected override void Update()
	{
		// Falling and Steering
		base.Update();

		// Jumping
		{
			if (_jumpingLeft > 0f)
			{
				_jumpingLeft -= Time.deltaTime;
				if (_jumpingLeft <= 0f)
				{
					_movementType = MovementType.Player;
					UpdateRenderer();
				}
			}
		}

		// Controls
		if (!_touch)
		{
			if (_touch = Input.GetMouseButton(0))
			{
				_touchPosition = Input.mousePosition;
			}
		}
		else
		{
			if (!(_touch = Input.GetMouseButton(0)))
			{
				var newPos = Input.mousePosition;
				var delta = newPos - _touchPosition;

				if (delta.y > Constants.MIN_SWIPE_LENGTH)
				{
					if (_jumpingLeft <= 0f)
					{
						_jumpingLeft = Constants.SPEEDS[MovementType.Static] + Constants.ADDITIONAL_JUMP_TIME;
						_movementType = MovementType.Jump;
						UpdateRenderer();
					}
				}

				if (delta.x < -Constants.MIN_SWIPE_LENGTH)
				{
					if (_steering == 0)
						_steering = -1;
				}
				else
				if (delta.x > Constants.MIN_SWIPE_LENGTH)
				{
					if (_steering == 0)
						_steering = 1;
				}
			}
		}
	}
}
