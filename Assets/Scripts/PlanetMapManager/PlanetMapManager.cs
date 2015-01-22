using UnityEngine;
using System.Collections;

public class PlanetMapManager : MonoBehaviour {

	public void LoadLevel ()
	{
		Debug.Log ("LoadLevel_1");
		Application.LoadLevel ("testing");
	}
}