using System.Collections.Generic;
using UnityEngine;

public class MyPhysicsEngine : MonoBehaviour
{
	public static MyPhysicsEngine instance;

	List<MySphereCollider> spheres = new List<MySphereCollider>();

	public void AddSphere(MySphereCollider sphere)
	{
		spheres.Add(sphere);
	}

	// Use this for initialization
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogWarning("Multipe MyPhysicsEngine detected!");
			Destroy(this);
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		Debug.Log(spheres.Count);
	}
}
