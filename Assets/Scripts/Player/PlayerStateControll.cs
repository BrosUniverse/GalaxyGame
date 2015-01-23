using UnityEngine;
using System.Collections;

public class PlayerStateControll : MonoBehaviour {

	public GUITexture _buttonToSolid;
	public GUITexture _buttonToLiquid;
	public GUITexture _buttonToGas;
	public GUITexture _buttonJump;

	public GameObject _PlayerDead;

	private enum kPlayerStates {
		kPlayerPhysicalStateSolid = 0,
		kPlayerPhisicalStateLiquid,
		kPlyerPhysicalStateGas
	};

	public int _currentPlayerPhysState;
	public float _forceJump = 800f;

	private Animator _anim;

	private GameObject _attackedEnemy;

	void Clear ()
	{
		GameObject __instance = (GameObject)Instantiate (_PlayerDead, transform.position, transform.rotation);
		Destroy (gameObject);
		Debug.Log ("Clear");
	}

	void Attack ()
	{
		_attackedEnemy.GetComponent<EnemyStateMashine>().Death ();
		_attackedEnemy.collider2D.enabled = false;
		_anim.SetBool ("Attack", false);
		Debug.Log ("Attack");
	}

	void Start () {
		_anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () 
	{

#if UNITY_IOS
		foreach (Touch __touch in Input.touches)
		{
			if (_buttonToSolid.HitTest(__touch.position)) 
			{
				Debug.Log("Go to Solid");
				_currentPlayerPhysState = (int)kPlayerStates.kPlayerPhysicalStateSolid;
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				_anim.SetInteger("State", 0);
			}
			
			if (_buttonToLiquid.HitTest(__touch.position)) 
			{
				Debug.Log("Go to Liquid");
				_currentPlayerPhysState = (int)kPlayerStates.kPlayerPhisicalStateLiquid;
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				_anim.SetInteger("State", 1);
			}
			
			if (_buttonToGas.HitTest(__touch.position)) 
			{
				Debug.Log("Go to Gas");
				_currentPlayerPhysState = (int)kPlayerStates.kPlyerPhysicalStateGas;
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				_anim.SetInteger("State", 2);
			}
		}
#endif

#if UNITY_EDITOR_OSX
		if (Input.GetMouseButtonDown (0)) 
		{
			if (_buttonToSolid.HitTest(Input.mousePosition)) 
			{
				Debug.Log("Go to Solid");
				_currentPlayerPhysState = (int)kPlayerStates.kPlayerPhysicalStateSolid;
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				_anim.SetInteger("State", 0);
			}
				
			if (_buttonToLiquid.HitTest(Input.mousePosition)) 
			{
				Debug.Log("Go to Solid");
				_currentPlayerPhysState = (int)kPlayerStates.kPlayerPhisicalStateLiquid;
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				_anim.SetInteger("State", 1);
			}
				
			if (_buttonToGas.HitTest(Input.mousePosition)) 
			{
				Debug.Log("Go to Solid");
				_currentPlayerPhysState = (int)kPlayerStates.kPlyerPhysicalStateGas;
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				_anim.SetInteger("State", 2);
			}
				
		}
#endif

}

	void OnCollisionEnter2D (Collision2D __col)
	{
		if (__col.gameObject.tag == "EnemySolid" && _currentPlayerPhysState == (int)kPlayerStates.kPlayerPhysicalStateSolid)
		{
			_attackedEnemy = __col.gameObject;
			_attackedEnemy.GetComponent<EnemyStateMashine>().Death ();
			_anim.SetBool ("Attack", true);
		}
		else if (__col.gameObject.tag == "EnemySolid" && _currentPlayerPhysState != (int)kPlayerStates.kPlayerPhysicalStateSolid)
		{
			Debug.Log ("Game Over");
			_anim.SetBool ("Dead", true);
		}

		if (__col.gameObject.tag == "EnemyLiquid" && _currentPlayerPhysState == (int)kPlayerStates.kPlayerPhisicalStateLiquid)
		{
			_attackedEnemy = __col.gameObject;
			_attackedEnemy.GetComponent<EnemyStateMashine>().Death ();
			_anim.SetBool ("Attack", true);
		}
		else if (__col.gameObject.tag == "EnemyLiquid" && _currentPlayerPhysState != (int)kPlayerStates.kPlayerPhisicalStateLiquid)
		{
			Debug.Log ("Game Over");
			_anim.SetBool ("Dead", true);
		}

		if (__col.gameObject.tag == "EnemyGas" && _currentPlayerPhysState == (int)kPlayerStates.kPlyerPhysicalStateGas)
		{
			_attackedEnemy = __col.gameObject;
			_attackedEnemy.GetComponent<EnemyStateMashine>().Death ();
			_anim.SetBool ("Attack", true);
		}
		else if (__col.gameObject.tag == "EnemyGas" && _currentPlayerPhysState != (int)kPlayerStates.kPlyerPhysicalStateGas)
		{
			Debug.Log ("Game Over");
			_anim.SetBool ("Dead", true);
		}
	}

	void OnTriggerEnter2D (Collider2D __col)
	{
		if (__col.gameObject.tag == "Crystal")
		{
			GameManager.instance.amountCollectedCrystals++;
			Destroy (__col.gameObject);
		}

		if (__col.gameObject.tag == "Gasolin")
		{
			GameManager.instance.amountCollectedEnergyblocks++;
			Destroy (__col.gameObject);
		}

		if (__col.gameObject.tag == "SpaceShip")
		{
			if (GameManager.instance.LevelCompleted ())
			{
				Application.LoadLevel ("Level_1");
			}
		}

		if (__col.gameObject.tag == "FlowDown" && _currentPlayerPhysState == (int)kPlayerStates.kPlayerPhisicalStateLiquid)
		{
			Debug.Log ("FlowDown");
			gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;
		}
	}

	void OnTriggerExit2D (Collider2D __col)
	{
		if (__col.gameObject.tag == "FlowDown" && _currentPlayerPhysState == (int)kPlayerStates.kPlayerPhisicalStateLiquid)
		{
			Debug.Log ("FlowDown");
			gameObject.GetComponent<CircleCollider2D>().radius = 0.395f;
		}
	}

	void OnTriggerStay2D (Collider2D __col)
	{
		if (__col.gameObject.tag == "Windup" && _currentPlayerPhysState == (int)kPlayerStates.kPlyerPhysicalStateGas)
		{
			Debug.Log ("Windup");
			rigidbody2D.AddRelativeForce (Vector2.up * 15.5f);
		}
	}
}
