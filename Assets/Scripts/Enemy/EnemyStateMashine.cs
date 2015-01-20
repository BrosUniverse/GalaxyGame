using UnityEngine;
using System.Collections;

public interface IEnemyState
{
	string Idle ();
	string GoToTheLeft ();
	string GoToTheRight ();
	string Jump ();
	string Death ();
	string IsCollisingLeft ();
	string IsCollisingRight ();
	string Waiting ();
}

public interface IEnemyStateMashine
{
	void Idle ();
	void GoToTheLeft ();
	void GoToTheRight ();
	void Jump ();
	void Death ();
	void IsCollisingLeft ();
	void IsCollisingRight ();
	void Waiting ();
	
	void SetState (IEnemyState __state);
	IEnemyState GetIdleState ();
	IEnemyState GetGoToTheLeftState ();
	IEnemyState GetGoToTheRightState ();
	IEnemyState GetJumpState ();
	IEnemyState GetDeathState ();
	IEnemyState GetIsCollisingLeftState ();
	IEnemyState GetIsCollisingRightState ();
	IEnemyState GetWaitingState ();
}

public class EnemyStateMashine : MonoBehaviour {

	private IEnemyState _idle;
	private IEnemyState _goToTheLeft;
	private IEnemyState _goToTheRight;
	private IEnemyState _jump;
	private IEnemyState _death;
	private IEnemyState _isCollisingLeft;
	private IEnemyState _isCollisingRight;
	private IEnemyState _waiting;
	private IEnemyState _state;
	private IEnemyState _prevState;
	private Transform   _transform;
	private Animator    _anim;
	private bool 		_isFacingRight;

	public IEnemyState state
	{
		get { return _state; }
	}

	public IEnemyState prevState
	{
		get { return _prevState; }
		set { _prevState = value; }
	}

	public Animator anim
	{
		get { return _anim; }
	}

	void Awake ()
	{
		_idle = new IdleState (this);
		_goToTheLeft = new GoToTheLeftState (this);
		_goToTheRight = new GoToTheRightState (this);
		_jump = new JumpState (this);
		_death = new DeathState (this);
		_isCollisingLeft = new IsCollisingLeftState (this);
		_isCollisingRight = new IsCollisingRightState (this);
		_waiting = new WaitingState (this);
		_state = _waiting;
		_prevState = null;
		_isFacingRight = false;

		_transform = transform;
		_anim = GetComponent<Animator> ();
	}

	public void Idle ()
	{
		Debug.Log (_state.Idle ());
		StopAllCoroutines ();
		StartCoroutine (this.cIdle ());
	}
	
	public void GoToTheLeft ()
	{
		Debug.Log (_state.GoToTheLeft ());
		StopAllCoroutines ();
		StartCoroutine (this.cGoToTheLeft ());
	}
	
	public void GoToTheRight ()
	{
		Debug.Log (_state.GoToTheRight ());
		StopAllCoroutines ();
		StartCoroutine (this.cGoToTheRight ());
	}
	
	public void Jump ()
	{
		Debug.Log (_state.Jump ());
	}

	public void Death ()
	{
		Debug.Log (_state.Death ());
	}

	public void IsCollisingLeft ()
	{
		Debug.Log (_state.IsCollisingLeft ());
	}
	
	public void IsCollisingRight ()
	{
		Debug.Log (_state.IsCollisingRight ());
	}

	public void Waiting ()
	{
		Debug.Log (_state.Waiting ());
		StartCoroutine (this.cIdle ());
	}
	
	public void SetState (IEnemyState __state)
	{
		_state = __state;
	}
	
	public IEnemyState GetIdleState ()
	{
		return _idle;
	}
	
	public IEnemyState GetGoToTheLeftState ()
	{
		return _goToTheLeft;
	}
	
	public IEnemyState GetGoToTheRightState ()
	{
		return _goToTheRight;
	}
	
	public IEnemyState GetJumpState ()
	{
		return _jump;
	}

	public IEnemyState GetDeathState ()
	{
		return _death;
	}

	public IEnemyState GetIsCollisingLeftState ()
	{
		return _isCollisingLeft;
	}
	
	public IEnemyState GetIsCollisingRightState ()
	{
		return _isCollisingRight;
	}

	public IEnemyState GetWaitingState ()
	{
		return _waiting;
	}

	IEnumerator cIdle ()
	{
		yield return new WaitForSeconds (Random.Range (1f, 4f));
		this.Waiting ();
	}

	IEnumerator cGoToTheLeft ()
	{
		float __randomDistance = Random.Range (0.5f, 1f);
		float __distance = _transform.position.x - __randomDistance;

		if (_isFacingRight) 
		{ 
			_transform.localScale = new Vector2 (_transform.localScale.x * -1, _transform.localScale.y); 
			_isFacingRight = !_isFacingRight; 
		}

		_anim.SetFloat ("Speed", 0.02f);
		while (_transform.position.x > __distance)
		{
			_transform.Translate (-Vector2.right * Time.deltaTime);
			yield return null;
		}

		this.Waiting ();
	}

	IEnumerator cGoToTheRight ()
	{
		float __randomDistance = Random.Range (0.5f, 1f);
		float __distance = _transform.position.x + __randomDistance;

		if (!_isFacingRight) 
		{ 
			_transform.localScale = new Vector2 (_transform.localScale.x * -1, _transform.localScale.y); 
			_isFacingRight = !_isFacingRight; 
		}

		_anim.SetFloat ("Speed", 0.02f);
		while (_transform.position.x < __distance)
		{
			_transform.Translate (Vector2.right * Time.deltaTime);
			yield return null;
		}

		this.Waiting ();
	}
}

public class IdleState : IEnemyState
{
	private EnemyStateMashine _esm;
	
	public IdleState (EnemyStateMashine __esm)
	{
		_esm = __esm;
	}
	
	public string Idle ()
	{
		_esm.SetState (_esm.GetIdleState ());
		return "Idle Idle";
	}
	
	public string GoToTheLeft ()
	{
		_esm.SetState (_esm.GetGoToTheLeftState ());
		return "GoToTheLeft Idle";
	}
	
	public string GoToTheRight ()
	{
		_esm.SetState (_esm.GetGoToTheRightState ());
		return "GoToTheRight Idle";
	}
	
	public string Jump ()
	{
		_esm.SetState (_esm.GetJumpState ());
		return "GoToTheLeft Idle";
	}

	public string Death ()
	{
		_esm.SetState (_esm.GetDeathState ());
		_esm.StopAllCoroutines ();
		_esm.anim.SetBool ("Death", true);
		return "Death Idle";
	}

	public string IsCollisingLeft ()
	{
		//_esm.SetState (_esm.GetIsCollisingLeftState ());
		return "IsCollisingLeft Idle";
	}
	
	public string IsCollisingRight ()
	{
		//_esm.SetState (_esm.GetIsCollisingRightState ());
		return "IsCollisingRight Idle";
	}

	public string Waiting ()
	{
		_esm.SetState (_esm.GetWaitingState ());
		_esm.StopAllCoroutines ();
		return "Waiting Idle";
	}
}

public class GoToTheLeftState : IEnemyState
{
	private EnemyStateMashine _esm;
	
	public GoToTheLeftState (EnemyStateMashine __esm)
	{
		_esm = __esm;
	}
	
	public string Idle ()
	{
		_esm.SetState (_esm.GetIdleState ());
		return "Idle GTTL";
	}
	
	public string GoToTheLeft ()
	{
		_esm.SetState (_esm.GetGoToTheLeftState ());
		return "GoToTheLeft GTTL";
	}
	
	public string GoToTheRight ()
	{
		_esm.SetState (_esm.GetGoToTheRightState ());
		return "GoToTheRight GTTL";
	}
	
	public string Jump ()
	{
		_esm.SetState (_esm.GetJumpState ());
		return "GoToTheLeft GTTL";
	}

	public string Death ()
	{
		_esm.SetState (_esm.GetDeathState ());
		_esm.StopAllCoroutines ();
		_esm.anim.SetBool ("Death", true);
		return "Death GTTL";
	}

	public string IsCollisingLeft ()
	{
		_esm.StopAllCoroutines ();
		_esm.prevState = _esm.GetIsCollisingLeftState ();
		_esm.SetState (_esm.GetWaitingState ());
		_esm.transform.localScale = new Vector2 (_esm.transform.localScale.x * -1, _esm.transform.localScale.y);
		_esm.anim.SetFloat ("Speed", 0f);
		return "IsCollisingLeft GTTL";
	}
	
	public string IsCollisingRight ()
	{
		_esm.SetState (_esm.GetIsCollisingRightState ());
		return "IsCollisingRight GTTL";
	}

	public string Waiting ()
	{
		_esm.SetState (_esm.GetWaitingState ());
		_esm.StopAllCoroutines ();
		_esm.anim.SetFloat ("Speed", 0f);
		return "Waiting GTTL";
	}
}

public class GoToTheRightState : IEnemyState
{
	private EnemyStateMashine _esm;
	
	public GoToTheRightState (EnemyStateMashine __esm)
	{
		_esm = __esm;
	}
	
	public string Idle ()
	{
		_esm.SetState (_esm.GetIdleState ());
		return "Idle GTTR";
	}
	
	public string GoToTheLeft ()
	{
		_esm.SetState (_esm.GetGoToTheLeftState ());
		return "GoToTheLeft GTTR";
	}
	
	public string GoToTheRight ()
	{
		_esm.SetState (_esm.GetGoToTheRightState ());
		return "GoToTheRight GTTR";
	}
	
	public string Jump ()
	{
		_esm.SetState (_esm.GetJumpState ());
		return "GoToTheLeft GTTR";
	}

	public string Death ()
	{
		_esm.SetState (_esm.GetDeathState ());
		_esm.StopAllCoroutines ();
		_esm.anim.SetBool ("Death", true);
		return "Death GTTR";
	}

	public string IsCollisingLeft ()
	{
		_esm.SetState (_esm.GetIsCollisingLeftState ());
		return "IsCollisingLeft GTTR";
	}
	
	public string IsCollisingRight ()
	{
		_esm.StopAllCoroutines ();
		_esm.prevState = _esm.GetIsCollisingRightState ();
		_esm.SetState (_esm.GetWaitingState ());
		_esm.transform.localScale = new Vector2 (_esm.transform.localScale.x * -1, _esm.transform.localScale.y);
		_esm.anim.SetFloat ("Speed", 0f);
		return "IsCollisingRight GTTR";
	}

	public string Waiting ()
	{
		_esm.SetState (_esm.GetWaitingState ());
		_esm.StopAllCoroutines ();
		_esm.anim.SetFloat ("Speed", 0f);
		return "Waiting GTTR";
	}
}

public class JumpState : IEnemyState
{
	private EnemyStateMashine _esm;
	
	public JumpState (EnemyStateMashine __esm)
	{
		_esm = __esm;
	}
	
	public string Idle ()
	{
		_esm.SetState (_esm.GetIdleState ());
		return "Idle J";
	}
	
	public string GoToTheLeft ()
	{
		_esm.SetState (_esm.GetGoToTheLeftState ());
		return "GoToTheLeft J";
	}
	
	public string GoToTheRight ()
	{
		_esm.SetState (_esm.GetGoToTheRightState ());
		return "GoToTheRight J";
	}
	
	public string Jump ()
	{
		_esm.SetState (_esm.GetJumpState ());
		return "GoToTheLeft J";
	}

	public string Death ()
	{
		_esm.SetState (_esm.GetDeathState ());
		return "Death J";
	}

	public string IsCollisingLeft ()
	{
		_esm.SetState (_esm.GetIsCollisingLeftState ());
		return "IsCollisingLeft J";
	}
	
	public string IsCollisingRight ()
	{
		_esm.SetState (_esm.GetIsCollisingRightState ());
		return "IsCollisingRight J";
	}

	public string Waiting ()
	{
		_esm.SetState (_esm.GetWaitingState ());
		return "Waiting J";
	}
}

public class DeathState : IEnemyState
{
	private EnemyStateMashine _esm;
	
	public DeathState (EnemyStateMashine __esm)
	{
		_esm = __esm;
	}
	
	public string Idle ()
	{
		_esm.SetState (_esm.GetIdleState ());
		return "Idle D";
	}
	
	public string GoToTheLeft ()
	{
		_esm.SetState (_esm.GetGoToTheLeftState ());
		return "GoToTheLeft D";
	}
	
	public string GoToTheRight ()
	{
		_esm.SetState (_esm.GetGoToTheRightState ());
		return "GoToTheRight D";
	}
	
	public string Jump ()
	{
		_esm.SetState (_esm.GetJumpState ());
		return "GoToTheLeft D";
	}

	public string Death ()
	{
		_esm.SetState (_esm.GetDeathState ());
		return "Death D";
	}
	
	public string IsCollisingLeft ()
	{
		_esm.SetState (_esm.GetIsCollisingLeftState ());
		return "IsCollisingLeft D";
	}
	
	public string IsCollisingRight ()
	{
		_esm.SetState (_esm.GetIsCollisingRightState ());
		return "IsCollisingRight D";
	}
	
	public string Waiting ()
	{
		_esm.SetState (_esm.GetWaitingState ());
		return "Waiting D";
	}
}

public class IsCollisingLeftState : IEnemyState
{
	private EnemyStateMashine _esm;
	
	public IsCollisingLeftState (EnemyStateMashine __esm)
	{
		_esm = __esm;
	}
	
	public string Idle ()
	{
		_esm.SetState (_esm.GetIdleState ());
		return "Idle CLS";
	}
	
	public string GoToTheLeft ()
	{
		_esm.SetState (_esm.GetGoToTheLeftState ());
		return "GoToTheLeft CLS";
	}
	
	public string GoToTheRight ()
	{
		_esm.SetState (_esm.GetGoToTheRightState ());
		return "I cant to to the left";
	}
	
	public string Jump ()
	{
		_esm.SetState (_esm.GetJumpState ());
		return "GoToTheLeft CLS";
	}

	public string Death ()
	{
		_esm.SetState (_esm.GetDeathState ());
		return "Death CLS";
	}

	public string IsCollisingLeft ()
	{
		_esm.SetState (_esm.GetIsCollisingLeftState ());
		return "IsCollisingLeft CLS";
	}
	
	public string IsCollisingRight ()
	{
		_esm.SetState (_esm.GetIsCollisingRightState ());
		return "IsCollisingRight CLS";
	}

	public string Waiting ()
	{
		_esm.SetState (_esm.GetWaitingState ());
		return "Waiting CLS";
	}
}

public class IsCollisingRightState : IEnemyState
{
	private EnemyStateMashine _esm;
	
	public IsCollisingRightState (EnemyStateMashine __esm)
	{
		_esm = __esm;
	}
	
	public string Idle ()
	{
		_esm.SetState (_esm.GetIdleState ());
		return "Idle CRS";
	}
	
	public string GoToTheLeft ()
	{
		_esm.SetState (_esm.GetGoToTheLeftState ());
		return "GoToTheLeft CRS";
	}
	
	public string GoToTheRight ()
	{
		//_enemyAutomat.SetState (_enemyAutomat.GetGoToTheRightState ());
		// я перейду в состояние Idle
		// то есть мне нужно запустить корутину Idle
		_esm.SetState (_esm.GetWaitingState ());
		return "I cant to to the right";
	}
	
	public string Jump ()
	{
		_esm.SetState (_esm.GetJumpState ());
		return "GoToTheLeft CRS";
	}

	public string Death ()
	{
		_esm.SetState (_esm.GetDeathState ());
		return "Death CRS";
	}

	public string IsCollisingLeft ()
	{
		_esm.SetState (_esm.GetIsCollisingLeftState ());
		return "IsCollisingLeft CRS";
	}
	
	public string IsCollisingRight ()
	{
		_esm.SetState (_esm.GetIsCollisingRightState ());
		return "IsCollisingRight CRS";
	}

	public string Waiting ()
	{
		_esm.SetState (_esm.GetWaitingState ());
		return "Waiting CRS";
	}
}

public class WaitingState : IEnemyState
{
	private EnemyStateMashine _esm;
	
	public WaitingState (EnemyStateMashine __esm)
	{
		_esm = __esm;
	}
	
	public string Idle ()
	{
		_esm.SetState (_esm.GetIdleState ());
		return "Idle Waiting";
	}
	
	public string GoToTheLeft ()
	{
		if (_esm.prevState != _esm.GetIsCollisingLeftState ())
		{
			_esm.SetState (_esm.GetGoToTheLeftState ());
			_esm.prevState = null;
			return "GoToTheLeft Waiting";
		}
		return "Waiting Waiting";
	}
	
	public string GoToTheRight ()
	{
		if (_esm.prevState != _esm.GetIsCollisingRightState ())
		{
			_esm.SetState (_esm.GetGoToTheRightState ());
			_esm.prevState = null;
			return "GoToTheRight Waiting";
		}
		return "Waiting Waiting";
	}
	
	public string Jump ()
	{
		_esm.SetState (_esm.GetJumpState ());
		return "Jump Waiting";
	}

	public string Death ()
	{
		_esm.SetState (_esm.GetDeathState ());
		return "Death Waiting";
	}

	public string IsCollisingLeft ()
	{
		//_esm.SetState (_esm.GetIsCollisingLeftState ());
		return "IsCollisingLeft Waiting";
	}
	
	public string IsCollisingRight ()
	{
		//_esm.SetState (_esm.GetWaitingState ());
		//_esm.anim.SetFloat ("Speed", 0f);
		return "IsCollisingRight Waiting";
	}
	
	public string Waiting ()
	{
		_esm.SetState (_esm.GetWaitingState ());
		return "Waiting Waiting";
	}
}


