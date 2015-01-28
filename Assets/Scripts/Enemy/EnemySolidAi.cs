using UnityEngine;
using System.Collections;

public class EnemySolidAi : MonoBehaviour {

	private enum _kEnemyActions 
	{
		kEnemyActionGoToTheLeft = 0,
		kEnemyActionGoToTheRigth,
		kEnemyActionStand
	}

	public float _speed = 0.5f;
	public float _move = 0.5f;
	private bool _isFacingRight = true;

	private bool _actionIsComplited = true;

	private float _distance;

	private Animator _anim;

	private Transform _transform;
	private Vector2 _localScale;

	private EnemyStateMashine _esm;

	int ChooseAnAction () 
	{
		return Random.Range ((int)_kEnemyActions.kEnemyActionGoToTheLeft,
		                     (int)_kEnemyActions.kEnemyActionStand + 1);
	}

	void PerformAnAction (int __action) 
	{

		_actionIsComplited = false;

		switch (__action) 
		{

		case (int)_kEnemyActions.kEnemyActionGoToTheLeft :

			_esm.GoToTheLeft ();

			break;

		case (int)_kEnemyActions.kEnemyActionGoToTheRigth:

			_esm.GoToTheRight ();

			break;

		case (int)_kEnemyActions.kEnemyActionStand :

			_esm.Idle ();

			break;

		default :
			Debug.Log ("Default");
			break;

		}
	}

	void Awake () 
	{
		_transform = transform;
		_localScale = transform.localScale;

		_esm = GetComponent<EnemyStateMashine> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (_esm.state.GetType().Equals (typeof (WaitingState)))
		{
			int __action = ChooseAnAction ();
			PerformAnAction (__action);
		}
	}

	void OnTriggerEnter2D (Collider2D __col)
	{
		if (__col.gameObject.tag == "EnemyTriggerLeft")
		{
			Debug.Log ("Enemy Trigger Left");
			_esm.GoToTheRight ();
		}
		else if (__col.gameObject.tag == "EnemyTriggerRight")
		{
			Debug.Log ("Enemy Trigger Right");
			_esm.GoToTheLeft ();
		}
	}
}
