using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class LightController : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    [Header("Random Light Settings")]
    public float minRandomOnTime = 5.0f;
    public float maxRandomOnTime = 15.0f;

    private Renderer myRenderer;
    private Camera mainCamera;
    private bool isCurrentlyOn = false;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        mainCamera = Camera.main;
        TurnLightOff();
        StartCoroutine(RandomLightActivationRoutine());
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
            {
                if (isCurrentlyOn)
                {
                    TurnLightOff();
                }
            }
        }
    }

    void TurnLightOn()
{
    isCurrentlyOn = true;
    myRenderer.material = lightOnMaterial;
    
    // --- LİSTE GÜNCELLEMESİ ---
    // Eğer listede zaten yoksak, kendimizi listeye ekle.
    if (!TurtleBehaviour.activeDistractionTargets.Contains(this.transform))
    {
        TurtleBehaviour.activeDistractionTargets.Add(this.transform);
    }
    // -------------------------
}

    void TurnLightOff()
{
    isCurrentlyOn = false;
    myRenderer.material = lightOffMaterial;

    // --- LİSTE GÜNCELLEMESİ ---
    // Eğer listede varsak, kendimizi listeden çıkar.
    if (TurtleBehaviour.activeDistractionTargets.Contains(this.transform))
    {
        TurtleBehaviour.activeDistractionTargets.Remove(this.transform);
    }
    // -------------------------
}

    IEnumerator RandomLightActivationRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minRandomOnTime, maxRandomOnTime);
            yield return new WaitForSeconds(waitTime);
            
            if (!isCurrentlyOn) {
                TurnLightOn();
            }
        }
    }
}