using System;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text text; // Tempat untuk menampilkan teks dialog
    public GameObject DialogSystem; // Panel dialog yang akan muncul
    private string[] npcDialog; // Array yang menyimpan dialog untuk NPC
    private int currentLine = 0; // Menyimpan baris dialog yang sedang ditampilkan
    private Action onDialogueEnd; // Callback saat dialog selesai

    void Start()
    {
        DialogSystem.SetActive(false); // Sembunyikan dialog saat awal
    }

    // ✅ Tambahkan parameter kedua untuk callback saat dialog selesai
    public void ShowMessage(string[] message, Action callback)
    {
        if (message == null || message.Length == 0) return; // Cegah error jika dialog kosong

        npcDialog = message; // Simpan dialog NPC
        currentLine = 0; // Reset ke awal
        onDialogueEnd = callback; // Simpan callback
        DialogSystem.SetActive(true); // Munculkan panel dialog
        ShowNextLine(); // Tampilkan baris pertama
    }

    // Menampilkan baris berikutnya
    public void ShowNextLine()
    {
        if (currentLine < npcDialog.Length)
        {
            text.text = npcDialog[currentLine]; // Tampilkan teks dialog
            currentLine++; // Pindah ke baris berikutnya
        }
        else
        {
            DialogSystem.SetActive(false); // Tutup dialog
            currentLine = 0; // Reset untuk dialog berikutnya
            onDialogueEnd?.Invoke(); // 🔥 Panggil callback setelah dialog selesai
        }
    }

    void Update()
    {
        if (DialogSystem.activeSelf && Input.GetKeyDown(KeyCode.Space)) // Tekan Spasi untuk lanjut
        {
            ShowNextLine();
        }
    }
}
