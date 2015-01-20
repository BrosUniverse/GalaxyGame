using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform _player;

	private Transform _transform;

	public Vector2 
		Margin, 
		Smoothing;

	public BoxCollider2D bounds;

	private Vector3
		_min,
		_max;

	private bool isFollowing { get; set; }

	void Awake () {
		_transform = transform;
	}

	// Use this for initialization
	void Start () {
		_min = bounds.bounds.min;
		_max = bounds.bounds.max;
		isFollowing = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		var x = _transform.position.x;
		var y = _transform.position.y;

		if (_player != null)
		{
			if (Mathf.Abs (x - _player.position.x) > Margin.x)
				x = Mathf.Lerp (x, _player.position.x, Smoothing.x * Time.deltaTime);

			if (Mathf.Abs (y - _player.position.y) > Margin.y)
				y = Mathf.Lerp (y, _player.position.y, Smoothing.y * Time.deltaTime);
		}

		var cameraHalfWidth = camera.orthographicSize * ((float) Screen.width / Screen.height);

		x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		y = Mathf.Clamp (y, _min.y + camera.orthographicSize, _max.y - camera.orthographicSize);

		_transform.position = new Vector3 (x, y, _transform.position.z);
	}

	void  LateUpdate ()
	{

	}

}