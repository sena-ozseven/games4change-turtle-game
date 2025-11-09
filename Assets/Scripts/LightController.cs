/* using UnityEngine;

public class LightController : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial; 
    
    private Renderer myRenderer;
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        TurnLightOff();
    }

    void OnMouseDown()
    {
        if (TurtleBehaviour.isLightOn)
        {
            TurnLightOff();
        }
        else
        {
            TurnLightOn();
        }
    }


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
}
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class LightController : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    private Renderer myRenderer;
    private Camera mainCamera;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        mainCamera = Camera.main;
        TurnLightOff();

        // Başlangıçta kamera var mı diye kontrol edelim
        if (mainCamera == null)
        {
            Debug.LogError("HATA: Sahnede 'MainCamera' etiketli bir kamera bulunamadı!");
        }
    }

    void Update()
    {
        // 1. ADIM: Mouse'a tıklandı mı?
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse tıklandı!"); // Bu mesaj Console'da görünmeli

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // 2. ADIM: Işın bir şeye çarptı mı?
            if (Physics.Raycast(ray, out hit))
            {
                // Neye çarptığını yazdır
                Debug.Log("Işın bir objeye çarptı: " + hit.collider.gameObject.name);

                // 3. ADIM: Çarptığı obje bu obje mi?
                if (hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("EVET! Doğru objeye ('Light') tıklandı. Işık durumu değiştiriliyor.");
                    ToggleLight();
                }
            }
            else
            {
                Debug.Log("Işın hiçbir şeye çarpmadı.");
            }
        }
    }

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
    }

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
}