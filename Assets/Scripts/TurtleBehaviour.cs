

using UnityEngine;

public class TurtleBehaviour : MonoBehaviour
{
    public GameObject seaTarget;
    public GameObject lightTarget;
    
    public float turtleSpeed = 3f;
    public static bool isLightOn = false;

    private Rigidbody rb;
    private Transform currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        turtleSpeed = Random.Range(1.5f, 4.5f);
    }

    void Update()
    {
        if (isLightOn && lightTarget != null)
        {
            currentTarget = lightTarget.transform;
        }
        else if (!isLightOn && seaTarget != null)
        {
            currentTarget = seaTarget.transform;
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