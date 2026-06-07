using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
    [Header("Status Makanan")]
    public bool punyaSemangka = false;
    public bool punyaDaun = false;

    [Header("Visual Makanan di Bak Mobil")]
    public GameObject buahSemangkaDiBak; // Drag objek semangka di bak ke sini
    public GameObject daunDiBak;         // Drag objek daun di bak ke sini

    [Header("UI Canvas")]
    public TextMeshProUGUI teksStatusMakanan;
    public TextMeshProUGUI teksTimer;

    [Header("Pengaturan Waktu")]
    public float sisaWaktu = 40f;
    private bool gameSelesai = false;

    void Start()
    {
        UpdateUIStatus("Tidak Membawa Apapun");
        AturVisualBak(false, false); // Pastikan awalnya tidak ada visual makanan di bak
    }
    void Update()
    {
        if (gameSelesai) return;

        if (sisaWaktu > 0)
        {
            sisaWaktu -= Time.deltaTime;
            teksTimer.text = "Waktu: " + Mathf.CeilToInt(sisaWaktu).ToString() + "s";
        }
        else
        {
            LolosAtauKalah(false);
        }
    }

    // Fungsi baru untuk mengatur visual buah di bak mobil
    public void AturVisualBak(bool tampilkanSemangka, bool tampilkanDaun)
    {
        if (buahSemangkaDiBak != null) buahSemangkaDiBak.SetActive(tampilkanSemangka);
        if (daunDiBak != null) daunDiBak.SetActive(tampilkanDaun);
    }

    public void UpdateUIStatus(string status)
    {
        teksStatusMakanan.text = "Membawa: " + status;
    }

    public void LolosAtauKalah(bool apakahMenang)
    {
        gameSelesai = true;
        if (apakahMenang)
        {
            // Saat menang, pastikan semua visual di bak langsung hilang bersih
            AturVisualBak(false, false);

            int sceneSekarang = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneSekarang + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}