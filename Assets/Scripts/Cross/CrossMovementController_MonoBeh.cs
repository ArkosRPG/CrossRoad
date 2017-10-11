
using UnityEngine;


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
[RequireComponent(typeof(CrossRenderer))]
public partial class CrossMovementController : MonoBehaviour
{
	private GameController _gameController;

	private GameObject _go;
	protected Transform _tf;
	private CrossRenderer _renderer;


	private void OnEnable()
	{
		if (_go == null)
			_go = gameObject;
		if (_tf == null)
			_tf = transform;
		if (_renderer == null)
			_renderer = GetComponent<CrossRenderer>();

		UpdateRenderer();
	}


	private void Start()
	{
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
				_gameController.ReportFree(_go, _tf, this);
				_movementType = MovementType.OutOfGame;
				return;
			}
		}

		if (Y != pos.y)
			_tf.position = new Vector3(pos.x, Y, pos.z);
	}
}
