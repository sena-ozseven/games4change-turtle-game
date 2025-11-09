using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TurtleBehaviour : MonoBehaviour
{
    // Aktif dikkat dağıtıcı hedefler
    public static List<Transform> activeDistractionTargets = new List<Transform>();

    [Header("Targets")]
    public GameObject seaTarget; // Deniz hedefi (sahnede boş bir GameObject olabilir)

    [Header("Movement Settings")]
    public float turtleSpeed = 3f;

    private Rigidbody rb;
    private Transform currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("Rigidbody component bulunamadı!");

        turtleSpeed = Random.Range(1.5f, 4.5f);

        // Sahnede "Sea" tag'ine sahip tek bir obje bul
        seaTarget = GameObject.FindGameObjectWithTag("Sea");
        if (seaTarget != null)
            currentTarget = seaTarget.transform;
        else
            Debug.LogError("Sea target bulunamadı!");
    }

    void Update()
    {
        // Null objeleri listeden temizle
        activeDistractionTargets = activeDistractionTargets.Where(t => t != null).ToList();

        if (activeDistractionTargets.Count > 0)
        {
            Transform closest = GetClosestTarget(activeDistractionTargets);
            currentTarget = closest != null ? closest : (seaTarget != null ? seaTarget.transform : null);
        }
        else
        {
            if(seaTarget != null)
                currentTarget = seaTarget.transform;
        }
    }

    // Verilen listeden en yakın Transform'u döndür
    Transform GetClosestTarget(List<Transform> targets)
    {
        if (targets == null || targets.Count == 0) return null;

        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (Transform t in targets)
        {
            if (t == null) continue;
            float dist = Vector3.Distance(currentPos, t.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = t;
            }
        }

        return closest;
    }

    void FixedUpdate()
    {
        if (currentTarget == null) return;

        Vector3 direction = (currentTarget.position - transform.position).normalized;
        direction.y = 0;

        rb.MovePosition(transform.position + direction * turtleSpeed * Time.fixedDeltaTime);

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}
