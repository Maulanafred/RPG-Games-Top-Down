using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController charController;
    private Quaternion targetRotation;
    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 8;

    public float gravity = 9.8f;
    private Vector3 velocity; // Untuk menyimpan kecepatan jatuh

    public KeyCode runKey = KeyCode.LeftShift; // Hotkey untuk lari (bisa diubah di Inspector)
    public Transform gunTransform; // Transform dari posisi tembakan (misalnya dari senjata)
    public GameObject bulletPrefab; // Prefab peluru
    public float bulletSpeed = 20f; // Kecepatan peluru

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (!charController.enabled) return;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 motion = input.normalized;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1f;
        motion *= Input.GetKey(runKey) ? runSpeed : walkSpeed;

        // Terapkan gravitasi
        if (!charController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime; // Tambahkan efek jatuh
        }
        else
        {
            velocity.y = -2f; // Pastikan karakter tetap di tanah (bukan 0 agar tidak terdeteksi "melayang")
        }

        // Gabungkan gerakan dengan gravitasi
        charController.Move((motion + velocity) * Time.deltaTime);

        RotateTowardsMouse();

        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    // Fungsi untuk merotasi karakter menghadap cursor mouse
    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Membuat ray dari posisi mouse
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point - transform.position; // Menghitung arah ke posisi mouse
            direction.y = 0; // Mengabaikan rotasi vertikal
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Membuat rotasi menuju posisi mouse
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Fungsi untuk menembakkan peluru
    void ShootBullet()
    {
        if (bulletPrefab != null && gunTransform != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = gunTransform.forward * bulletSpeed;
            }
        }
    }
}
