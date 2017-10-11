
using UnityEngine;


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public sealed class CrossRenderer : MonoBehaviour
{
	private SpriteRenderer[] _sprites;


	void Start()
	{
		_sprites = GetComponentsInChildren<SpriteRenderer>();
	}


	public void UpdateColor(MovementType movementType)
	{
		if (!Constants.COLORS.ContainsKey(movementType))
			return;

		var color = Constants.COLORS[movementType];
		foreach (var sprite in _sprites)
		{
			sprite.color = color;
		}
	}
}
