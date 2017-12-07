using UnityEngine;

public class MySphereCollider : MonoBehaviour
{
	public float radius;

	private void Start()
	{
		MyPhysicsEngine.instance.AddSphere(this);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position , radius);
	}
}
