
using System;
using UnityEngine;


public class PlayerController : CrossMovementController
{
	private CrossRenderer _playerRenderer;

	private Switch _steering = 0;
	private float _jumpingLeft = 0f;

	private bool _touch = false;
	private Vector3 _touchPosition = Vector3.zero;


	protected override void Update()
	{
		// Falling
		base.Update();

		// Steering
		{
			var pos = _tf.position;
			var X = pos.x;
			X += _steering * Constants.STEERING_SPEED * Time.deltaTime;

			if (X < -Constants.BORDER_X)
			{
				X = -Constants.BORDER_X;
				_steering = 0;
			}
			else
			if (X > Constants.BORDER_X)
			{
				X = Constants.BORDER_X;
				_steering = 0;
			}

			if (X != pos.x)
				_tf.position = new Vector3(X, pos.y, pos.z);
		}

		// Jumping
		{
			if (_jumpingLeft > 0f)
				_jumpingLeft -= Time.deltaTime;
		}

		// Controls
		if (!_touch)
		{
			if (_touch = Input.GetMouseButton(0))
				_touchPosition = Input.mousePosition;
		}
		else
		{
			if (!(_touch = Input.GetMouseButton(0)))
			{
				var newPos = Input.mousePosition;
				var delta = newPos - _touchPosition;

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

				if (delta.y > Constants.MIN_SWIPE_LENGTH)
				{
					if (_jumpingLeft <= 0f)
						_jumpingLeft = Constants.SPEEDS[MovementType.Static] + Constants.ADDITIONAL_JUMP_TIME;
				}
			}
		}

		var newState = _jumpingLeft > 0f ? MovementType.Jump : _steering ? MovementType.Steering : MovementType.Player;
		if (_movementType != newState)
		{
			_movementType = newState;
			UpdateRenderer();
		}
	}


	[Obsolete("Use with switch instead", true)]
	public override void Init(GameController gameController, MovementType movementType)
	{
		throw new NotSupportedException();
	}


	public void Init(GameController gameController, MovementType movementType, Switch steering)
	{
		base.Init(gameController, movementType);
		_steering = steering;
	}
}
