
using UnityEngine;


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public sealed class CrossRenderer : MonoBehaviour
{
	private Transform _tf;

	[SerializeField] private Color _color = Color.magenta;
	private SpriteRenderer[] _sprites;


	private void Start()
	{
		_tf = transform;
		_sprites = GetComponentsInChildren<SpriteRenderer>();
		UpdateColor();
	}


	private void Update()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying)
		{
			UpdateColor();
			return;
		}
#endif
	}


	public void UpdatePosition(PositionComponent position)
	{
		_tf.position = position.GetVector3();
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
