
using UnityEngine;


public struct CrossWrap
{
	public CrossMovementController CMC;
	public GameObject GO;
	public Transform Tf;


	public CrossWrap(GameObject go, Transform tf, CrossMovementController cmc)
	{
		GO = go;
		Tf = tf;
		CMC = cmc;
	}
}
