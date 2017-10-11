
using UnityEngine;


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
[RequireComponent(typeof(CrossRenderer))]
public class CrossMovementController : MonoBehaviour
{
	private GameController _gameController;

	private GameObject _go;
	protected Transform _tf;
	private CrossRenderer _renderer;
	[SerializeField] protected MovementType _movementType;


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
				_movementType = MovementType.OutOfGame;
				_gameController.ReportFree(_go, _tf, this);
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


	public virtual void Init(GameController gameController, MovementType movementType)
	{
		_gameController = gameController;
		_movementType = movementType;
	}


	public void ReportCollision()
	{
		_movementType = MovementType.GameOver;
		UpdateRenderer();
	}


	public bool IsCrusher()
	{
		return _movementType > MovementType.Jump;
	}


	public bool IsObstacle()
	{
		return _movementType < MovementType.Jump
			&& _movementType > MovementType.OutOfGame;
	}
}
