using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int _offsetX = 2;

	public bool _hasARightBuddy = false;
	public bool _hasALeftBuddy  = false;

	private float _spriteWidth = 0f;

	private Camera _cam;

	private Transform _transform;

	private bool __reverseScale;

	public GameObject _parallaxing;

	void Awake ()
	{
		_cam = Camera.main;
		_transform = transform;
	}

	// Use this for initialization
	void Start () 
	{
		SpriteRenderer __spriteRenderer = GetComponent<SpriteRenderer>();
		_spriteWidth = __spriteRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_hasARightBuddy == false || _hasALeftBuddy == false)
		{
			float __camHorizontalExtend = _cam.orthographicSize * Screen.width / Screen.height;

			float __edgeVisiblePositionRight = (_transform.position.x + _spriteWidth/2) - __camHorizontalExtend;
			float __edgeVisiblePositionLeft = (_transform.position.x + _spriteWidth/2) + __camHorizontalExtend;

			if (_cam.transform.position.x >= __edgeVisiblePositionRight - _offsetX && _hasARightBuddy == false)
			{
				MakeNewBuddy (1);
				_hasARightBuddy = true;
			}
			else if (_cam.transform.position.x <= __edgeVisiblePositionLeft + _offsetX && _hasALeftBuddy == false)
			{
				MakeNewBuddy (-1);
				_hasALeftBuddy = true;
			}
		}
	}

	void MakeNewBuddy (int __rightOrLeft)
	{
		Vector3 __newPosition = new Vector3 (_transform.position.x + _spriteWidth * __rightOrLeft, _transform.position.y, _transform.position.z);
		Transform __newBuddy = Instantiate (_transform, __newPosition, transform.rotation) as Transform;

		if (__reverseScale == true)
		{
			__newBuddy.transform.localScale = new Vector3 (__newBuddy.localScale.x * -1, __newBuddy.localScale.y, __newBuddy.localScale.z);
		}

		__newBuddy.parent = _transform.parent;

		if (__rightOrLeft > 0)
		{
			__newBuddy.GetComponent<Tiling>()._hasALeftBuddy = true;
		}
		else
		{
			__newBuddy.GetComponent<Tiling>()._hasARightBuddy = true;
		}

		if (_parallaxing != null)
		{
			_parallaxing.GetComponent<Parallaxing>().AddNewBackground (__newBuddy);
		}
	}
}
