using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string[] npcDialog; // Dialog untuk NPC ini
    private CharacterController characterController; 
    [SerializeField] private GameObject interactiveUI; // UI "Press F"
    private DialogueManager dialogueManager; // Referensi ke DialogueManager
    private bool playerInRange = false;
    private bool isInteracting = false;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        characterController = FindObjectOfType<CharacterController>();
        interactiveUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactiveUI.SetActive(true); // Munculkan UI "Press F"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactiveUI.SetActive(false); // Sembunyikan UI
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !isInteracting)
        {   
            isInteracting = true; // Set status interaksi
            characterController.enabled = false; // Matikan kontrol pemain
            interactiveUI.SetActive(false); // Sembunyikan UI "Press F"

            dialogueManager.ShowMessage(npcDialog, EndInteraction); // Panggil dialog dengan callback
        }
    }

    void EndInteraction()
    {
        isInteracting = false;
        characterController.enabled = true; // Aktifkan kontrol pemain kembali
    }
}
