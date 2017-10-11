
using UnityEngine;


public partial class GameController : MonoBehaviour
{
	private struct Wrap
	{
		public CrossMovementController CMC;
		public GameObject GO;
		public Transform Tf;


		public Wrap(GameObject go, Transform tf, CrossMovementController cmc)
		{
			GO = go;
			Tf = tf;
			CMC = cmc;
		}
	}
}
