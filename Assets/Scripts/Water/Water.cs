using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	//Our renderer that'll make the top of the water visible
	LineRenderer Body;
	//Our physics arrays
	float[] xpositions;
	float[] ypositions;
	float[] velocities;
	float[] accelerations;
	//Our meshes and colliders
	GameObject[] meshobjects;
	GameObject[] colliders;
	Mesh[] meshes;
	//Our particle system
	public GameObject splash;
	//The material we're using for the top of the water
	public Material mat;
	//The GameObject we're using for a mesh
	public GameObject watermesh;
	//All our constants
	const float springconstant = 0.02f;
	const float damping = 0.04f;
	const float spread = 0.05f;
	const float z = -1f;
	//The properties of our water
	float baseheight;
	float left;
	float bottom;

	void Start()
	{
		//Spawning our water
		SpawnWater(43,20, 0,-3);
		StartCoroutine ("Waves");
	}

	public void SpawnWater(float Left, float Width, float Top, float Bottom)
	{
		//Calculating the number of edges and nodes we have
		int edgecount = Mathf.RoundToInt(Width) * 5;
		int nodecount = edgecount + 1;

		//Declare our physics arrays
		xpositions = new float[nodecount];
		ypositions = new float[nodecount];
		velocities = new float[nodecount];
		accelerations = new float[nodecount];
		//Declare our mesh arrays
		meshobjects = new GameObject[edgecount];
		meshes = new Mesh[edgecount];
		colliders = new GameObject[edgecount];
		//Set our variables
		baseheight = Top;
		bottom = Bottom;
		left = Left;
		//For each node, set the line renderer and our physics arrays
		for (int i = 0; i < nodecount; i++)
		{
			ypositions[i] = Top;
			xpositions[i] = Left + Width * i / edgecount;
			accelerations[i] = 0;
			velocities[i] = 0;
		}
		//Setting the meshes now:
		for (int i = 0; i < edgecount; i++)
		{
			//Make the mesh
			meshes[i] = new Mesh();
			//Create the corners of the mesh
			Vector3[] Vertices = new Vector3[4];
			Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
			Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z);
			Vertices[2] = new Vector3(xpositions[i], bottom, z);
			Vertices[3] = new Vector3(xpositions[i+1], bottom, z);
			//Set the UVs of the texture
			Vector2[] UVs = new Vector2[4];
			UVs[0] = new Vector2(0, 1);
			UVs[1] = new Vector2(1, 1);
			UVs[2] = new Vector2(0, 0);
			UVs[3] = new Vector2(1, 0);
			//Set where the triangles should be.
			int[] tris = new int[6] { 0, 1, 3, 3, 2, 0};
			//Add all this data to the mesh.
			meshes[i].vertices = Vertices;
			meshes[i].uv = UVs;
			meshes[i].triangles = tris;
			//Create a holder for the mesh, set it to be the manager's child
			meshobjects[i] = Instantiate(watermesh,Vector3.zero,Quaternion.identity) as GameObject;
			meshobjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
			meshobjects[i].transform.parent = transform;

		}
	}
	//Same as the code from in the meshes before, set the new mesh positions
	void UpdateMeshes()
	{
		for (int i = 0; i < meshes.Length; i++)
		{
			Vector3[] Vertices = new Vector3[4];
			Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
			Vertices[1] = new Vector3(xpositions[i+1], ypositions[i+1], z);
			Vertices[2] = new Vector3(xpositions[i], bottom, z);
			Vertices[3] = new Vector3(xpositions[i+1], bottom, z);
			meshes[i].vertices = Vertices;
		}
	}

	IEnumerator Waves ()
	{
		yield return new WaitForSeconds (2f);

		while (true)
		{
			yield return new WaitForSeconds (2f); 

			ypositions [0] = Random.Range (0f, 5f);
			yield return new WaitForSeconds (2f);
			ypositions [50] = Random.Range (0f, 5f);
			yield return new WaitForSeconds (2f);
			ypositions [100] = Random.Range (0, 5f);

			yield return new WaitForSeconds (2f);
		}
	}
	//Called regularly by Unity
	void FixedUpdate()
	{
		//Here we use the Euler method to handle all the physics of our springs:

		for (int i = 0; i < xpositions.Length ; i++)
		{
			float force = springconstant * (ypositions[i] - baseheight) + velocities[i]*damping ;
			accelerations[i] = -force;
			ypositions[i] += velocities[i];
			velocities[i] += accelerations[i];
		}
		//Now we store the difference in heights:
		float[] leftDeltas = new float[xpositions.Length];
		float[] rightDeltas = new float[xpositions.Length];
		//We make 8 small passes for fluidity:
		for (int j = 0; j < 8; j++)
		{
			for (int i = 0; i < xpositions.Length; i++)
			{
				//We check the heights of the nearby nodes, adjust velocities accordingly, record the height differences
				if (i > 0)
				{
					leftDeltas[i] = spread * (ypositions[i] - ypositions[i-1]);
					velocities[i - 1] += leftDeltas[i];
				}
				if (i < xpositions.Length - 1)
				{
					rightDeltas[i] = spread * (ypositions[i] - ypositions[i + 1]);
					velocities[i + 1] += rightDeltas[i];
				}
			}
			//Now we apply a difference in position
			for (int i = 0; i < xpositions.Length; i++)
			{
				if (i > 0)
					ypositions[i-1] += leftDeltas[i];
				if (i < xpositions.Length - 1)
					ypositions[i + 1] += rightDeltas[i];
			}
		}
		//Finally we update the meshes to reflect this
		UpdateMeshes();
	}
}