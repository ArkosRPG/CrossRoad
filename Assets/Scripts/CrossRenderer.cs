
using System.Collections.Generic;
using UnityEngine;


public sealed class CrossRenderer : MonoBehaviour
{
	private Dictionary<MovementType, Color> _colors = new Dictionary<MovementType, Color>()
	{
		{ MovementType.Player   , Color.white },
		{ MovementType.Jump     , Color.blue  },
		{ MovementType.GameOver , Color.red   },
		{ MovementType.Static   , Color.black },
		{ MovementType.Fast     , Color.black },
	};


	private Transform _tf;
	private SpriteRenderer[] _sprites;


	private MovementEntity _movementEntity;


	public void Init(MovementEntity movementEntity)
	{
		_tf = transform;
		_sprites = GetComponentsInChildren<SpriteRenderer>();

		_movementEntity = movementEntity;
		_movementEntity.OnComponentReplaced += MovementEntity_OnComponentReplaced;
		_movementEntity.OnDestroyEntity += MovementEntity_OnDestroyEntity;

		UpdateColor();
		UpdatePosition();
	}


	private void UpdateColor()
	{
		var movementType = _movementEntity.movementType.Value;
		gameObject.name = movementType.ToString();
		var color = _colors[movementType];
		foreach (var sprite in _sprites)
		{
			sprite.color = color;
		}
	}


	private void UpdatePosition()
	{
		_tf.position = _movementEntity.position.GetVector3();
	}


	private void MovementEntity_OnComponentReplaced(Entitas.IEntity entity, int index, Entitas.IComponent previousComponent, Entitas.IComponent newComponent)
	{
		if (newComponent is MovementTypeComponent)
			UpdateColor();
		else
		if (newComponent is PositionComponent)
			UpdatePosition();
	}


	private void MovementEntity_OnDestroyEntity(Entitas.IEntity entity)
	{
		Destroy(gameObject);
	}
}
