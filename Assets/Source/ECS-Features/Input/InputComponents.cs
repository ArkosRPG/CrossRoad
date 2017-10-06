
using Entitas;


[Input]
public class InputComponent : IComponent
{
	public Input Input;
}


public enum Input
{
	Tap = 1,
	SwipeLeft,
	SwipeRight,
}
