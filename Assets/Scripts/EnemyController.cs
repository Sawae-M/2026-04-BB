using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Transform target;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }
}