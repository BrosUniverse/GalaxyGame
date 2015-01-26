using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;
	private Transform cam;
	private Vector3 prevCamPosition;

	private float[] parallaxScales;
	public float smoothing = 1f;

	void Awake ()
	{
		cam = Camera.main.transform;
	}
	// Use this for initialization
	void Start () {

		prevCamPosition = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++)
		{
			parallaxScales[i] = backgrounds[i].position.z * -1;
		}

	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < backgrounds.Length; i++)
		{
			float parallax = (prevCamPosition.x - cam.position.x) * parallaxScales[i];

			float backgroundPosX = backgrounds[i].position.x + parallax;

			Vector3 newBackgroundPos = new Vector3 (backgroundPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, newBackgroundPos, smoothing * Time.deltaTime);
		}

		prevCamPosition = cam.position;
	}

}
