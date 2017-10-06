﻿
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
	Jump,
	Static,
	Fast,
}
