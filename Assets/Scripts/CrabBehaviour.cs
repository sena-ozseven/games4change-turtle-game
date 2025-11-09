using UnityEngine;
using System.Collections; // Coroutine için gerekli

public class CrabController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 1.0f; // Yengecin hareket hızı
    public float movementRadius = 3.0f; // Başlangıç noktasından ne kadar uzağa gidebileceği
    public float decisionInterval = 2.0f; // Kaç saniyede bir yeni bir yöne gitmeye karar vereceği

    private Rigidbody rb;
    private Vector3 startingPosition; // Yengecin oyuna başladığı ilk pozisyon
    private Vector3 targetPosition; // Yengecin gitmeye çalıştığı hedef pozisyon

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position; // Başlangıç pozisyonunu kaydet

        // Rastgele hareket döngüsünü başlat
        StartCoroutine(UpdateTargetPositionRoutine());
    }

    void FixedUpdate()
    {
        // Hedef pozisyona doğru hareket et
        MoveTowardsTarget();
    }

    // Belirli aralıklarla yeni bir hedef belirleyen Coroutine
    IEnumerator UpdateTargetPositionRoutine()
    {
        while (true)
        {
            // Yeni bir rastgele hedef pozisyonu hesapla
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            float randomDistance = Random.Range(0, movementRadius);
            
            // Yeni hedef, başlangıç noktasının etrafındaki daire içinde olmalı
            targetPosition = startingPosition + randomDirection * randomDistance;

            // Bir sonraki karar anına kadar bekle
            yield return new WaitForSeconds(decisionInterval);
        }
    }

    void MoveTowardsTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.linearVelocity = direction * speed;
            
            // Yengecin yüzünü gittiği yöne döndür (isteğe bağlı ama güzel bir detay)
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Hedefe ulaştıysak dur
            rb.linearVelocity = Vector3.zero;
        }
    }
}
