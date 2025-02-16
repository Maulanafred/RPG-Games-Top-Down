using UnityEngine;
using System.Collections.Generic;

public class ObjectTransparancy : MonoBehaviour
{
    public Transform player;
    public LayerMask transparentLayer;
    private Dictionary<Renderer, Color[]> originalColors = new Dictionary<Renderer, Color[]>();

    void Update()
    {
        HandleTransparency();
    }

    void HandleTransparency()
    {
        // Reset transparansi objek yang sebelumnya terkena raycast
        foreach (var entry in originalColors)
        {
            Renderer rend = entry.Key;
            if (rend != null)
            {
                Material[] materials = rend.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    Color originalColor = entry.Value[i];
                    materials[i].color = originalColor;
                }
            }
        }
        originalColors.Clear();

        // Kirim ray dari kamera ke pemain
        RaycastHit[] hits;
        Vector3 direction = player.position - transform.position;
        hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, transparentLayer);

        // Ubah objek yang terkena ray menjadi transparan
        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null && !originalColors.ContainsKey(rend))
            {
                Material[] materials = rend.materials;
                Color[] colors = new Color[materials.Length];

                for (int i = 0; i < materials.Length; i++)
                {
                    colors[i] = materials[i].color; // Simpan warna asli
                    Color newColor = materials[i].color;
                    newColor.a = 0.3f; // Transparan 30%
                    materials[i].color = newColor;
                }

                originalColors.Add(rend, colors);
            }
        }
    }
}
