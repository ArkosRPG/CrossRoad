
using UnityEngine;


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public sealed class CrossRenderer : MonoBehaviour
{
	private SpriteRenderer[] _sprites;


	public void UpdateColor(MovementType movementType)
	{
		if (!Constants.COLORS.ContainsKey(movementType))
			return;

		var color = Constants.COLORS[movementType];

		if (_sprites == null)
			_sprites = GetComponentsInChildren<SpriteRenderer>();

		foreach (var sprite in _sprites)
		{
			sprite.color = color;
		}
	}
}
