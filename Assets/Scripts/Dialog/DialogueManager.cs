using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text text; // Tempat untuk menampilkan teks dialog
    public GameObject DialogSystem; // Panel dialog yang akan muncul
    public string[] npcDialog; // Array yang menyimpan dialog untuk setiap NPC
    private int currentLine = 0; // Menyimpan baris dialog yang sedang ditampilkan

    // Start is called before the first frame update
    void Start()
    {
        DialogSystem.SetActive(false); // Menyembunyikan dialog saat game dimulai
    }

    // Memulai dialog untuk NPC tertentu
    public void ShowMessage(string[] message){
        npcDialog = message; // Mengganti dialog sesuai dengan NPC yang dipilih
        currentLine = 0; // Mengatur ulang ke baris pertama
        DialogSystem.SetActive(true); // Menampilkan panel dialog
        ShowNextLine(); // Menampilkan baris pertama
    }

    // Menampilkan baris berikutnya
    public void ShowNextLine(){
        if (currentLine < npcDialog.Length){
            text.text = npcDialog[currentLine];
            currentLine++;
        }
        else{
            // Jika sudah selesai, sembunyikan dialog
            DialogSystem.SetActive(false);
            currentLine = 0; // Mengatur ulang ke awal untuk dialog berikutnya
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Menunggu input untuk melewati dialog
        if (Input.GetKeyDown(KeyCode.Space)) // Misal tekan spasi untuk melanjutkan
        {
            ShowNextLine();
        }
    }
}
