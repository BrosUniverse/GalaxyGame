using UnityEngine;
using System.Collections;

public interface ICharacterState
{
	string Idle ();
	string GoToTheLeft ();
	string GoToTheRight ();
	string Jump ();
	string IsCollisingRight ();
}

public interface IEnemyAutomat
{
	void Idle ();
	void GoToTheLeft ();
	void GoToTheRight ();
	void Jump ();
	void IsCollisingRight ();
	
	void SetState (ICharacterState __state);
	ICharacterState GetIdleState ();
	ICharacterState GetGoToTheLeftState ();
	ICharacterState GetGoToTheRightState ();
	ICharacterState GetJumpState ();
	ICharacterState GetIsCollisingRightState ();
	
	//int count { get; set; }
}

public class ChatecterStateMachine : MonoBehaviour {

	public class EnemyAutomat : IEnemyAutomat
	{
		private ICharacterState _idle;
		private ICharacterState _goToTheLeft;
		private ICharacterState _goToTheRight;
		private ICharacterState _jump;
		private ICharacterState _isCollisingRight;
		private ICharacterState _state;
		private int _count;

		public EnemyAutomat ()
		{
			_idle = new IdleState (this);
			_goToTheLeft = new GoToTheLeftState (this);
			_goToTheRight = new GoToTheRightState (this);
			_jump = new JumpState (this);
			_isCollisingRight = new IsCollisingRightState (this);
			_state = _idle;
		}

		public void Idle ()
		{
			Debug.Log (_state.Idle ());
		}

		public void GoToTheLeft ()
		{
			Debug.Log (_state.GoToTheLeft ());
		}

		public void GoToTheRight ()
		{
			Debug.Log (_state.GoToTheRight ());
		}

		public void Jump ()
		{
			Debug.Log (_state.Jump ());
		}

		public void IsCollisingRight ()
		{
			Debug.Log (_state.IsCollisingRight ());
		}

		public void SetState (ICharacterState __state)
		{
			_state = __state;
		}

		public ICharacterState GetIdleState ()
		{
			return _idle;
		}

		public ICharacterState GetGoToTheLeftState ()
		{
			return _goToTheLeft;
		}

		public ICharacterState GetGoToTheRightState ()
		{
			return _goToTheRight;
		}

		public ICharacterState GetJumpState ()
		{
			return _jump;
		}

		public ICharacterState GetIsCollisingRightState ()
		{
			return _isCollisingRight;
		}
	}

	public class IdleState : ICharacterState
	{
		private EnemyAutomat _enemyAutomat;

		public IdleState (EnemyAutomat __enemyAutomat)
		{
			_enemyAutomat = __enemyAutomat;
		}

		public string Idle ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIdleState ());
			return "Idle Idle";
		}

		public string GoToTheLeft ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheLeftState ());
			return "GoToTheLeft Idle";
		}

		public string GoToTheRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheRightState ());
			return "GoToTheRight Idle";
		}

		public string Jump ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetJumpState ());
			return "GoToTheLeft Idle";
		}

		public string IsCollisingRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIsCollisingRightState ());
			return "IsCollisingRight Idle";
		}
	}

	public class GoToTheLeftState : ICharacterState
	{
		private EnemyAutomat _enemyAutomat;

		public GoToTheLeftState (EnemyAutomat __enemyAutomat)
		{
			_enemyAutomat = __enemyAutomat;
		}

		public string Idle ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIdleState ());
			return "Idle GTTL";
		}
		
		public string GoToTheLeft ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheLeftState ());
			return "GoToTheLeft GTTL";
		}
		
		public string GoToTheRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheRightState ());
			return "GoToTheRight GTTL";
		}
		
		public string Jump ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetJumpState ());
			return "GoToTheLeft GTTL";
		}

		public string IsCollisingRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIsCollisingRightState ());
			return "IsCollisingRight GTTL";
		}
	}

	public class GoToTheRightState : ICharacterState
	{
		private EnemyAutomat _enemyAutomat;
		
		public GoToTheRightState (EnemyAutomat __enemyAutomat)
		{
			_enemyAutomat = __enemyAutomat;
		}

		public string Idle ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIdleState ());
			return "Idle GTTR";
		}
		
		public string GoToTheLeft ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheLeftState ());
			return "GoToTheLeft GTTR";
		}
		
		public string GoToTheRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheRightState ());
			return "GoToTheRight GTTR";
		}
		
		public string Jump ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetJumpState ());
			return "GoToTheLeft GTTR";
		}

		public string IsCollisingRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIsCollisingRightState ());
			return "IsCollisingRight GTTR";
		}
	}

	public class JumpState : ICharacterState
	{
		private EnemyAutomat _enemyAutomat;
		
		public JumpState (EnemyAutomat __enemyAutomat)
		{
			_enemyAutomat = __enemyAutomat;
		}

		public string Idle ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIdleState ());
			return "Idle J";
		}
		
		public string GoToTheLeft ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheLeftState ());
			return "GoToTheLeft J";
		}
		
		public string GoToTheRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheRightState ());
			return "GoToTheRight J";
		}
		
		public string Jump ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetJumpState ());
			return "GoToTheLeft J";
		}

		public string IsCollisingRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIsCollisingRightState ());
			return "IsCollisingRight J";
		}
	}

	public class IsCollisingRightState : ICharacterState
	{
		private EnemyAutomat _enemyAutomat;
		
		public IsCollisingRightState (EnemyAutomat __enemyAutomat)
		{
			_enemyAutomat = __enemyAutomat;
		}
		
		public string Idle ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIdleState ());
			return "Idle CRS";
		}
		
		public string GoToTheLeft ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetGoToTheLeftState ());
			return "GoToTheLeft CRS";
		}
		
		public string GoToTheRight ()
		{
			//_enemyAutomat.SetState (_enemyAutomat.GetGoToTheRightState ());
			return "I cant to to the right";
		}
		
		public string Jump ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetJumpState ());
			return "GoToTheLeft CRS";
		}
		
		public string IsCollisingRight ()
		{
			_enemyAutomat.SetState (_enemyAutomat.GetIsCollisingRightState ());
			return "IsCollisingRight CRS";
		}
	}

	public EnemyAutomat _enemy;

	void Start ()
	{
		_enemy = new EnemyAutomat ();

		_enemy.Idle ();
		_enemy.GoToTheLeft ();
		_enemy.GoToTheRight ();
		_enemy.IsCollisingRight ();
		_enemy.GoToTheRight ();
	}

	void Update ()
	{

	}
}
