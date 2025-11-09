using UnityEngine;

public class TurtleBehaviour : MonoBehaviour
{
    public static Transform activeDistractionTarget;

    public Transform seaTarget;
    
    public float turtleSpeed = 3f;
    
    private Rigidbody rb;
    private Transform currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        turtleSpeed = Random.Range(1.5f, 4.5f);

        if (seaTarget != null)
        {
            currentTarget = seaTarget;
        }
    }

    void Update()
    {
        if (activeDistractionTarget != null)
        {
            currentTarget = activeDistractionTarget;
        }
        else
        {
            currentTarget = seaTarget;
        }
    }

    void FixedUpdate()
    {
        if (currentTarget == null) return;

        Vector3 direction = (currentTarget.position - transform.position).normalized;
        direction.y = 0;

        rb.MovePosition(transform.position + direction * turtleSpeed * Time.fixedDeltaTime);

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}