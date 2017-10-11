
using UnityEngine;


[ExecuteInEditMode]
public class ColliderDrawer : MonoBehaviour
{
	private Transform _tf;
	private CircleCollider2D _circle;


	private void OnDrawGizmos()
	{
		if (_tf == null)
			_tf = transform;

		if (_circle == null)
			_circle = GetComponent<CircleCollider2D>();
		if (_circle != null)
			Gizmos.DrawWireSphere(_tf.position, _circle.radius);
	}
}
