
using UnityEngine;


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
[RequireComponent(typeof(CrossRenderer))]
public class CrossMovementController : MonoBehaviour
{
	protected Transform _tf;
	private CrossRenderer _renderer;
	[SerializeField] protected MovementType _movementType;


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

		if (Y != pos.y)
			_tf.position = new Vector3(pos.x, Y, pos.z);
	}


	protected void UpdateRenderer()
	{
		_renderer.UpdateColor(_movementType);
	}
}
