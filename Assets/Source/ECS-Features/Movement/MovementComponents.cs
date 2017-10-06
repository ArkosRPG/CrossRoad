
using Entitas;


[Movement]
public class PositionComponent : IComponent
{
	public float X;
	public float Y;
}


[Movement]
public class SteerComponent : IComponent
{
	public float Value;
}


[Movement]
public class MovementTypeComponent : IComponent
{
	public MovementType Value;
}


public enum MovementType
{
	Player = 1,
	Static,
	Fast,
}
