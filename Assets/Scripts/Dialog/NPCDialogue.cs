using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string[] npcDialog; // Dialog untuk NPC ini
    private DialogueManager dialogueManager; // Referensi ke DialogueManager
    private bool playerInRange = false; // Untuk mendeteksi apakah pemain berada dalam jangkauan

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(); // Menemukan DialogueManager yang ada
    }

    // Fungsi untuk berinteraksi dengan NPC saat pemain memasuki area trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Pastikan yang masuk adalah pemain
        {
            playerInRange = true; // Pemain berada dalam jangkauan
        }
    }

    // Fungsi untuk berhenti berinteraksi saat pemain keluar dari area trigger
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Pastikan yang keluar adalah pemain
        {
            playerInRange = false; // Pemain keluar dari jangkauan
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Jika pemain dalam jangkauan dan menekan tombol interaksi (misal 'E')
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            dialogueManager.ShowMessage(npcDialog); // Menampilkan dialog
        }
    }
}
