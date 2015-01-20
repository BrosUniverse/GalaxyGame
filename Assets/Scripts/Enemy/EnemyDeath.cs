using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {

	public GameObject _EnemySolidDead;
	public GameObject _EnemyLiquidDead;
	public GameObject _EnemyGasDead;

	void DeleteEnemy ()
	{
		if (tag == "EnemySolid")
		{
			GameObject __instance = (GameObject)Instantiate (_EnemySolidDead, transform.position, transform.rotation);
		}
		else if (tag == "EnemyLiquid")
		{
			GameObject __instance = (GameObject)Instantiate (_EnemyLiquidDead, transform.position, transform.rotation);
		}
		else if (tag == "EnemyGas")
		{
			GameObject __instance = (GameObject)Instantiate (_EnemyGasDead, transform.position, transform.rotation);
		}

		Destroy (gameObject);
		Debug.Log ("Clear");
	}
}
