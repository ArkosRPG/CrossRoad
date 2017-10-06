
using Entitas;


[Input]
public class InputComponent : IComponent
{
	public InputType Input;
}


public enum InputType
{
	Lock = 1,
	Unlock,
	SwipeUp,
	SwipeLeft,
	SwipeRight,
}
