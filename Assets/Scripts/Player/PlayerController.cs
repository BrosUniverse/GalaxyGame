using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float _maxSpeed = 20f;
	private bool _isFacingRight = true;
	private Animator _anim;
	public bool _keyboard = false;
	
	public GUITexture _buttonJump;
	public GUITexture _buttonLeft;
	public GUITexture _buttonRight;

	private bool _jump = false;

	public float _forceJump = 800f;
	private float _maxSpeedJump = 2f;

	private float _distToGround;

	private float __move_h;

	private float _velosityX = 0f;

	void JumpStart ()
	{
		_anim.SetBool ("JumpStart", false);
		_anim.SetBool ("Grounded", false);
		rigidbody2D.AddForce (Vector2.up * _forceJump);
		Debug.Log ("JumpStart");
	}

	// Use this for initialization
	void Start () 
	{
		_anim = GetComponent<Animator> ();
		_distToGround = gameObject.collider2D.bounds.extents.y;
	}

	void Update () 
	{
		#if UNITY_IOS
		
		foreach (Touch __touch in Input.touches)
		{
			if (_buttonLeft.HitTest (__touch.position) && __touch.phase == TouchPhase.Stationary)
			{
				_velosityX = Mathf.Lerp (0f, -1f, 10 * Time.deltaTime);
				_velosityX = Mathf.Clamp (_velosityX, -1, 0);
				rigidbody2D.velocity = new Vector2 (_velosityX * _maxSpeed, rigidbody2D.velocity.y);
			}
			else if (_buttonLeft.HitTest (__touch.position) && __touch.phase == TouchPhase.Ended)
			{
				_velosityX = 0f;
				rigidbody2D.velocity = new Vector2 (_velosityX, rigidbody2D.velocity.y);
			}
			
			if (_buttonRight.HitTest (__touch.position) && __touch.phase == TouchPhase.Stationary)
			{
				_velosityX = Mathf.Lerp (0f, 1f, 10 * Time.deltaTime);
				_velosityX = Mathf.Clamp (_velosityX, 0, 1);
				rigidbody2D.velocity = new Vector2 (_velosityX * _maxSpeed, rigidbody2D.velocity.y);
			}
			else if (_buttonRight.HitTest (__touch.position) && __touch.phase == TouchPhase.Ended)
			{
				_velosityX = 0f;
				rigidbody2D.velocity = new Vector2 (_velosityX, rigidbody2D.velocity.y);
			}
			
			if (_buttonJump.HitTest (__touch.position) && __touch.phase == TouchPhase.Began)
			{
				_anim.SetBool ("JumpStart", true);
			}
		}
		
		_anim.SetFloat ("Speed", Mathf.Abs (_velosityX));
		
		#endif
		
		#if UNITY_EDITOR_OSX
		
		_velosityX = Input.GetAxis ("Horizontal");
		_anim.SetFloat ("Speed", Mathf.Abs (_velosityX));
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_anim.SetBool ("JumpStart", true);
		}
		
		rigidbody2D.velocity = new Vector2 (_velosityX *_maxSpeed, rigidbody2D.velocity.y);
		#endif
		
		_anim.SetFloat ("SpeedV", rigidbody2D.velocity.y);
		
		if (_velosityX > 0 && !_isFacingRight) 
		{
			Flip();
		} 
		else if (_velosityX < 0 && _isFacingRight) 
		{
			Flip();
		}
	}

	void FixedUpdate () 
	{

	}

	private void Flip () 
	{
		_isFacingRight = !_isFacingRight;
		Vector2 __theScale = transform.localScale;
		__theScale.x *= - 1;
		transform.localScale = __theScale;
	}

	void OnCollisionEnter2D (Collision2D __col)
	{
		if (__col.gameObject.tag == "ground")
		{
			_anim.SetBool ("Grounded", true);
			_anim.SetBool ("JumpStart", false);
		}
	}
}
