
using UnityEngine;


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
[RequireComponent(typeof(CrossRenderer))]
public class CrossMovementController : MonoBehaviour
{
	private Transform _tf;
	private CrossRenderer _renderer;
	[SerializeField] protected MovementType _movementType;
	protected Switch _steering = 0;


	private void Start()
	{
		_tf = transform;
		_renderer = GetComponent<CrossRenderer>();
		UpdateRenderer();
	}


	protected virtual void Update()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying)
			return;
#endif
		var pos = _tf.position;
		var Y = pos.y;
		var X = pos.x;

		// Falling
		{
			var delta = 0f;
			if (Constants.SPEEDS.ContainsKey(_movementType))
				delta = Constants.SPEEDS[_movementType] * Time.deltaTime;
			Y -= delta;

			// Out of screen
			if (Y < -Constants.BORDER_Y)
			{
				Destroy(gameObject);
				return;
			}
		}

		// Steering
		{
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
		}

		if (X != pos.x || Y != pos.y)
			_tf.position = new Vector3(X, Y, pos.z);
	}


	protected void UpdateRenderer()
	{
		_renderer.UpdateColor(_movementType);
	}
}
