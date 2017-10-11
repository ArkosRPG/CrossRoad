
using System;
using UnityEngine;


[Serializable]
public class Switch
{
	[SerializeField] private int _value = 0;

	public int Value
	{
		get { return _value; }
		set { _value = value > 0 ? 1 : value < 0 ? -1 : 0; }
	}


	public Switch(int value = 0)
	{
		_value = value;
	}


	public static implicit operator Switch(int value)
	{
		return new Switch(value);
	}


	public static implicit operator int(Switch @switch)
	{
		return @switch.Value;
	}


	public static implicit operator bool(Switch @switch)
	{
		return @switch.Value != 0;
	}
}
