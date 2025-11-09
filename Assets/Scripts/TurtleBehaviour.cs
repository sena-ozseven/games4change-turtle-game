using UnityEngine;

public class TurtleBehaviour : MonoBehaviour
{
    public Transform seaTarget;
    public Transform lightTarget;
    public float turtleSpeed;
    public static bool isLightOn = false;
    private Rigidbody rb;
    private Transform currentTarget; //target to walk to: sea or light
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per fr
    void Update()
    {
        
    }
}
