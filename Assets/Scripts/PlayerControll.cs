using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        ApplyMovement();

        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime);
    }
    void ApplyMovement()
    {
        Vector3 movement = transform.forward * moveInput *moveSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        //y�͏d�͂Ȃǂ��ێ����邽�߂�rb.velocity�ŕێ�
    }
}
