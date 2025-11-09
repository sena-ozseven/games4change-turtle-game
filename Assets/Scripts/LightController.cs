using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class LightController : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    [Header("Random Light Settings")]
    public float minRandomOnTime;
    public float maxRandomOnTime; 

    private Renderer myRenderer;
    private Camera mainCamera;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        mainCamera = Camera.main;
        TurnLightOff();

        if (mainCamera == null)
        {
            Debug.LogError("error: could not find a camera labeled as 'MainCamera'!");
        }

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
                if (TurtleBehaviour.isLightOn)
                {
                    TurnLightOff();
                }
            }
        }
    }

    /* NOT IN USE ANYMORE
    void ToggleLight()
    {
        if (TurtleBehaviour.isLightOn)
        {
            TurnLightOff();
        }
        else
        {
            TurnLightOn();
        }
    } */

    void TurnLightOn()
    {
        TurtleBehaviour.isLightOn = true;
        myRenderer.material = lightOnMaterial;
    }

    void TurnLightOff()
    {
        TurtleBehaviour.isLightOn = false;
        myRenderer.material = lightOffMaterial;
    }

    IEnumerator RandomLightActivationRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minRandomOnTime, maxRandomOnTime);
            yield return new WaitForSeconds(waitTime);
            if (!TurtleBehaviour.isLightOn)
            {
                Debug.Log(waitTime + " saniye geçti, ışık otomatik olarak açılıyor!");
                TurnLightOn();
            }
        }
    }
}