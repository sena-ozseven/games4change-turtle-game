using UnityEngine;

public class TurtleBehaviour : MonoBehaviour
{
    public Transform seaTarget;
    public Transform lightTarget;
    public float turtleSpeed = 3f;
    public static bool isLightOn = false;
    private Rigidbody rb;
    private Transform currentTarget; //target to walk to: sea or light
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         rb = GetComponent<Rigidbody>();
        turtleSpeed = Random.Range(1.5f, 4.5f);
    }

    // Update is called once per fr
    void Update()
    {
        if (isLightOn)
        {
            currentTarget = lightTarget;
        }
        else
        {
            currentTarget = seaTarget;
        }
    }

    void FixedUpdate()
    {
        if (currentTarget == null) return; // Hedef yoksa hareket etme

        // Hedefe doğru bir yön vektörü hesapla
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        
        // Y eksenindeki hareketi engelle ki kaplumbağa havalanmasın
        direction.y = 0;

        // Hesaplanan yönde kuvvet uygula
        rb.MovePosition(transform.position + direction * turtleSpeed * Time.fixedDeltaTime);

        // Kaplumbağanın yüzünü hedefe döndür
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
