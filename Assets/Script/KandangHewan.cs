using UnityEngine;

public class KandangHewan : MonoBehaviour
{
    [Header("Pengaturan Kandang")]
    public bool butuhSemangka = true; // Jika dicentang = Kandang Gajah, jika kosong = Kandang Jerapah

    private ManagerGame gameManager;

    void Start()
    {
        // Hubungkan ke Game Manager di Scene
        gameManager = FindObjectOfType<ManagerGame>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Memastikan yang menabrak sensor ini adalah mobil pemain
        if (other.CompareTag("Player"))
        {
            // Pilihan 1: Jika ini Kandang Gajah
            if (butuhSemangka)
            {
                if (gameManager.punyaSemangka)
                {
                    gameManager.punyaSemangka = false; // Reset makanan setelah dikasih
                    gameManager.UpdateUIStatus("Kosong (Makanan Berhasil Diantar!)");
                    gameManager.AturVisualBak(false, false); // Pastikan semua visual di bak langsung hilang bersih

                    // Trigger menang jika berada di Level 1
                    gameManager.LolosAtauKalah(true);
                }
                else
                {
                    gameManager.UpdateUIStatus("Salah! Gajah hanya mau Semangka!");
                    Debug.Log("Gajah menolak makanan.");
                }
            }
            // Pilihan 2: Jika ini Kandang Jerapah
            else
            {
                if (gameManager.punyaDaun)
                {
                    gameManager.punyaDaun = false; // Reset makanan
                    gameManager.UpdateUIStatus("Kosong (Makanan Berhasil Diantar!)");
                    gameManager.AturVisualBak(false, false); // Pastikan semua visual di bak langsung hilang bersih

                    // Nanti di Level 2 bisa dipasang logika check komplit di sini
                    Debug.Log("Jerapah kenyang!");
                }
                else
                {
                    gameManager.UpdateUIStatus("Salah! Jerapah hanya mau Daun!");
                    Debug.Log("Jerapah menolak makanan.");
                }
            }
        }
    }
}