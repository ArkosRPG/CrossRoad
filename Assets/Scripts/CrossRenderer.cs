
using UnityEngine;


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public sealed class CrossRenderer : MonoBehaviour
{
	[SerializeField]
	private Color _color = Color.magenta;
	private SpriteRenderer[] _sprites;


	void Start()
	{
		_sprites = GetComponentsInChildren<SpriteRenderer>();
#if UNITY_EDITOR
	}

	void Update()
	{
#endif
		UpdateColor();
	}


	public void UpdateColor(Color color)
	{
		_color = color;
		UpdateColor();
	}

	private void UpdateColor()
	{
		foreach (var sprite in _sprites)
		{
			sprite.color = _color;
		}
	}
}
