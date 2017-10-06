
using System.Collections.Generic;


public static class Constants
{
	public static Dictionary<MovementType, float> SPEEDS = new Dictionary<MovementType, float>()
	{
		{ MovementType.Player   , 0.0f },
		{ MovementType.Jump     , 0.0f },
		{ MovementType.GameOver , 0.5f },
		{ MovementType.Static   , 1.5f },
		{ MovementType.Fast     , 3.0f },
	};
	public const float ADDITIONAL_JUMP_TIME = .25f;
	public const float STEERING_SPEED = 3f;

	public const float SPAWN_INTERVAL = 3f;


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
