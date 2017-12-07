using System.Collections.Generic;
using UnityEngine;

public class MyPhysicsEngine : MonoBehaviour
{
	struct MyCollision
	{
		public MySphereCollider colliderA;
		public MySphereCollider colliderB;
		public float distance;

		public MyCollision(MySphereCollider colliderA, MySphereCollider colliderB, float distance)
		{
			this.colliderA = colliderA;
			this.colliderB = colliderB;
			this.distance = distance;
		}
	}

	public static MyPhysicsEngine instance;

	List<MySphereCollider> spheres = new List<MySphereCollider>();
	List<MyCollision> collisions = new List<MyCollision>();

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
	private void FixedUpdate ()
	{
		Simulate();
		CheckCollisions();
		ResolveCollisions();

		collisions.Clear();
	}

	private void Simulate()
	{
		foreach (var sphere in spheres)
		{
			sphere.transform.Translate(sphere.velocity * Time.fixedDeltaTime);
		}
	}

	private void CheckCollisions()
	{
		for (int i = 0; i < spheres.Count; i++)
		{
			for (int j = i + 1; j < spheres.Count; j++)
			{
				var distance = Vector3.Distance(spheres[i].transform.position, spheres[j].transform.position)
				               - (spheres[i].radius + spheres[j].radius);
				if (distance <= 0)
				{
					collisions.Add(new MyCollision(spheres[i], spheres[j], distance));
				}
			}
		}
	}

	private void ResolveCollisions()
	{
		foreach (var collision in collisions)
		{
			var A = collision.colliderA;
			var B = collision.colliderB;

			var velocity = A.mass / (A.mass + B.mass) * A.velocity
			               + B.mass / (A.mass + B.mass) * B.velocity;

			A.velocity = velocity;
			B.velocity = velocity;
		}
	}
}
