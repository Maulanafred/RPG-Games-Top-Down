using UnityEngine;

public class LookAtCameraUI : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main; // Ambil kamera utama
    }

    void Update()
    {
        transform.LookAt(transform.position + cam.transform.forward); // Selalu menghadap kamera
    }
}
