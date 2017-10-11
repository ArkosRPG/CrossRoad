
using System.Collections.Generic;
using UnityEngine;


public static class Constants
{
	public static Dictionary<MovementType, Color> COLORS = new Dictionary<MovementType, Color>()
	{
		{ MovementType.Player   , Color.white   },
		{ MovementType.Jump     , Color.blue    },
		{ MovementType.GameOver , Color.red     },
		{ MovementType.Static   , Color.black   },
		{ MovementType.Fast     , Color.black   },
	};

	public static Dictionary<MovementType, float> SPEEDS = new Dictionary<MovementType, float>()
	{
		{ MovementType.Player   , 0.0f },
		{ MovementType.Jump     , 0.0f },
		{ MovementType.GameOver , 1.0f },
		{ MovementType.Static   , 2.5f },
		{ MovementType.Fast     , 5.0f },
	};
	public const float ADDITIONAL_JUMP_TIME = .25f;
	public const float STEERING_SPEED = 3f;

	public const float SPAWN_INTERVAL = 1f;


	public const float MIN_SWIPE_LENGTH = 100f;


	public const float PLAYER_INITIAL_X = 0f;
	public const float PLAYER_INITIAL_Y = -3.5f;
	public const float PLAYER_INITIAL_STEER = -3f;
	public const float BORDER_X = 1.25f;
	public const float BORDER_Y = 6f;


	public const float CROSS_BORDER_1 = 0.67f;
	public const float CROSS_BORDER_2 = 1.33f;
	public const float CROSS_BORDER_3 = 2.00f;

	public const float SPRITE_Z = 1f;
	public const int TARGET_FRAME_RATE = 30;
}
