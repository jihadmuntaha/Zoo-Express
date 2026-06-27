using UnityEngine;

public class KandangHewan : MonoBehaviour
{
    [Header("Pengaturan Kandang")]
    public bool butuhSemangka = true; // Centang = Gajah, Kosong = Jerapah

    [Header("Komponen Panah")]
    [SerializeField] private GameObject panahPenunjuk;

    private ManagerGame gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<ManagerGame>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager == null) return;

            // Pilihan 1: Jika ini Kandang Gajah
            if (butuhSemangka)
            {
                Debug.Log("--- PLAYER MEMASUKI KANDANG GAJAH ---");

                if (gameManager.punyaSemangka)
                {
                    Debug.Log("Hasil: BENAR! Gajah dapat Semangka.");
                    if (panahPenunjuk != null) panahPenunjuk.SetActive(false);

                    gameManager.punyaSemangka = false;
                    gameManager.UpdateUIStatus("Kosong (Makanan Berhasil Diantar!)");
                    gameManager.AturVisualBak(false, false);

                    gameManager.LolosAtauKalah(true); // Panggil WIN
                }
                else
                {
                    Debug.Log("Hasil: SALAH! Gajah tidak mau makanan ini.");
                    gameManager.UpdateUIStatus("Game Over! Gajah hanya mau Semangka!");
                    gameManager.LolosAtauKalah(false); // Panggil GAMEOVER
                }
            }
            // Pilihan 2: Jika ini Kandang Jerapah
            else
            {
                Debug.Log("--- PLAYER MEMASUKI KANDANG JERAPAH ---");

                if (gameManager.punyaDaun)
                {
                    Debug.Log("Hasil: BENAR! Jerapah dapat Daun.");
                    if (panahPenunjuk != null) panahPenunjuk.SetActive(false);

                    gameManager.punyaDaun = false;
                    gameManager.UpdateUIStatus("Kosong (Makanan Berhasil Diantar!)");
                    gameManager.AturVisualBak(false, false);

                    gameManager.LolosAtauKalah(true); // Panggil WIN
                }
                else
                {
                    Debug.Log("Hasil: SALAH! Jerapah tidak mau makanan ini.");
                    gameManager.UpdateUIStatus("Game Over! Jerapah hanya mau Daun!");
                    gameManager.LolosAtauKalah(false); // Panggil GAMEOVER
                }
            }
        }
    }
}