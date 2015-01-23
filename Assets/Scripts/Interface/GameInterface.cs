using UnityEngine;
using System.Collections;

public class GameInterface : MonoBehaviour {

	public GameObject _panel;

	public void StopContinueGame ()
	{
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			_panel.SetActive (false);
		}
		else
		{
			Time.timeScale = 0;
			_panel.SetActive (true);
		}
	}

	public void Quit ()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("Galaxy");
	}

	public void Refresh ()
	{
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevelName);
	}
}
