using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {

	public enum FollowType
	{
		MoveTowards,
		Lerp
	}

	public FollowType _followType = FollowType.MoveTowards;

	public float _speed = 1;

	public PathDefinition _Path;

	public IEnumerator<Transform> _currentPath;

	private Transform _transform;

	void Start ()
	{
		if (_Path == null)
		{
			Debug.Log ("Path not be null");
		}

		_currentPath = _Path.GetPathDefinition ();
		_currentPath.MoveNext ();

		if (_currentPath.Current == null)
			return;

		_transform = transform;

		_transform.position = _currentPath.Current.position;
	}

	void Update ()
	{
		if (_currentPath == null || _currentPath.Current == null)
			return;

		if (_followType == FollowType.Lerp)
			_transform.position = Vector3.Lerp (_transform.position, _currentPath.Current.position, _speed * Time.deltaTime);
		else if (_followType == FollowType.MoveTowards)
			_transform.position = Vector3.MoveTowards (_transform.position, _currentPath.Current.position, _speed * Time.deltaTime);

		var __distanceSquared = (_transform.position - _currentPath.Current.position).sqrMagnitude;

		if (__distanceSquared <= 0.1f * 0.1f)
			_currentPath.MoveNext ();
	}


}
