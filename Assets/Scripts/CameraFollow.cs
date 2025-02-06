using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referensi ke karakter (Player)
    public Vector3 offset = new Vector3(0, 5, -10); // Posisi offset kamera
    public float smoothSpeed = 5f; // Kecepatan mengikuti

    void LateUpdate()
    {
        if (target != null)
        {
            // Hitung posisi target yang diinginkan
            Vector3 desiredPosition = target.position + offset;
            
            // Gunakan Lerp agar gerakan kamera lebih halus
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            
        }
    }
}
