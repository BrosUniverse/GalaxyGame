using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	private Rect _rectCrystalInfo;
	private Rect _rectGasolinInfo;
	public GUIStyle _guiStyle = null;

	void Awake ()
	{

	}

	// Use this for initialization
	void Start () {
		_rectCrystalInfo = new Rect (50, 10, 100, 100);
		_rectGasolinInfo = new Rect (100, 10, 100, 100);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI ()
	{
		GUI.Label (_rectCrystalInfo, GameManager.instance.amountCollectedCrystals.ToString (), _guiStyle);
		GUI.Label (_rectGasolinInfo, GameManager.instance.amountCollectedEnergyblocks.ToString (), _guiStyle);
	}
}
