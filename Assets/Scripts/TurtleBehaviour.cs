using UnityEngine;
using System.Collections.Generic; // List kullanmak için bu kütüphaneyi ekliyoruz!
using System.Linq; // En yakın hedefi kolayca bulmak için (LINQ)

public class TurtleBehaviour : MonoBehaviour
{
    // --- TEMEL DEĞİŞİKLİK: Tek bir Transform yerine, Transform'lardan oluşan bir LİSTE tutuyoruz ---
    public static List<Transform> activeDistractionTargets = new List<Transform>();
    // -----------------------------------------------------------------------------------------

    public Transform seaTarget;
    
    public float turtleSpeed = 3f;
    
    private Rigidbody rb;
    private Transform currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        turtleSpeed = Random.Range(1.5f, 4.5f);
        currentTarget = seaTarget;
    }

    void Update()
    {
        // Aktif dikkat dağıtıcı hedefler listesi boş mu?
        if (activeDistractionTargets.Count > 0)
        {
            // Liste boş değilse, en yakın hedefi bul ve onu ata.
            currentTarget = GetClosestTarget(activeDistractionTargets);
        }
        else
        {
            // Liste boşsa, hedefimiz deniz olsun.
            currentTarget = seaTarget;
        }
    }

    // Bu fonksiyon, verilen listedeki Transform'lardan bu kaplumbağaya en yakın olanı bulur.
    Transform GetClosestTarget(List<Transform> targets)
    {
        // LINQ kullanarak tek satırda en yakın hedefi buluyoruz.
        // Her bir hedefi (t) alıp, o hedefin pozisyonu ile bizim pozisyonumuz arasındaki mesafeye göre sırala,
        // ve bu sıralanmış listenin ilk elemanını (en yakını) geri döndür.
        return targets.OrderBy(t => Vector3.Distance(transform.position, t.position)).FirstOrDefault();
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