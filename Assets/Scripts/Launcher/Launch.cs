﻿using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LaunchGame ()
	{
		Debug.Log ("Launch");
		Application.LoadLevel ("Level_1");
	}
}
