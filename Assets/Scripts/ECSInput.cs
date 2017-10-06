
using UnityEngine;


public class ECSInput : MonoBehaviour
{
	private bool _touch = false;
	private Vector3 _touchPosition = Vector3.zero;
	private InputContext _context;


	private void Start()
	{
		_context = Contexts.sharedInstance.input;
	}


	private void Update()
	{
		if (_touch)
		{
			if (!(_touch = Input.GetMouseButton(0)))
			{
				var newPos = Input.mousePosition;
				var delta = newPos - _touchPosition;

				if (delta.y > Constants.MIN_SWIPE_LENGTH)
				{
					var input = _context.CreateEntity();
					input.AddInput(InputType.SwipeUp);
				}

				if (delta.x < -Constants.MIN_SWIPE_LENGTH)
				{
					var input = _context.CreateEntity();
					input.AddInput(InputType.SwipeLeft);
				}
				else
				if (delta.x > Constants.MIN_SWIPE_LENGTH)
				{
					var input = _context.CreateEntity();
					input.AddInput(InputType.SwipeRight);
				}
			}
		}
		else
		{
			if (_touch = Input.GetMouseButton(0))
			{
				_touchPosition = Input.mousePosition;
			}
		}
	}
}
