
using Entitas;
using UnityEngine;


[Movement]
public class PositionComponent : IComponent
{
	public float X;
	public float Y;

	public Vector3 GetVector3()
	{
		return new Vector3(X, Y, 1f);
	}
	public Vector3 GetVector2()
	{
		return new Vector3(X, Y);
	}
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
	GameOver = 1,
	Player,
	Jump,
	Static,
	Fast,
}
