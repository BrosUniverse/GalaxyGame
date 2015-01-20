using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	private int _amountCollectedCrystals;
	private int _amountCollectedArtefacts;
	private int _amountCollectedEnergyblocks;
	private int _amountKilledMonsters;
	private int _score;

	public int amountCollectedCrystals
	{
		get
		{
			return _amountCollectedCrystals;
		}

		set
		{
			_amountCollectedCrystals = value;
		}
	}

	public int amountCollectedArtefacts
	{
		get
		{
			return _amountCollectedArtefacts;
		}
		
		set
		{
			_amountCollectedArtefacts = value;
		}
	}

	public int amountCollectedEnergyblocks
	{
		get
		{
			return _amountCollectedEnergyblocks;
		}
		
		set
		{
			_amountCollectedEnergyblocks = value;
		}
	}

	public int amountKilledMonsters
	{
		get
		{
			return _amountKilledMonsters;
		}
		
		set
		{
			_amountKilledMonsters = value;
		}
	}

	public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
			}
			return _instance;
		}
	}

	public bool LevelCompleted ()
	{
		if (_amountCollectedCrystals >= 8 && _amountCollectedEnergyblocks == 3)
			return true;
		return false;
	}

	void Awake ()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		_amountCollectedCrystals = 
		_amountCollectedArtefacts =
		_amountCollectedEnergyblocks =
		_amountKilledMonsters = 
		_score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}