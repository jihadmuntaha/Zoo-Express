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

    // --- TAMBAHAN FITUR PAUSE ---
    [Header("Sistem Pause")]
    [SerializeField] private GameObject panelPause; // Drag object Panel_Pause di Canvas ke sini
    private bool sedangPause = false;

    void Start()
    {
        UpdateUIStatus("Tidak Membawa Apapun");
        AturVisualBak(false, false); // Pastikan awalnya tidak ada visual makanan di bak

        // Memastikan waktu game berjalan normal saat awal play (anti-freeze)
        Time.timeScale = 1f;
    }

    void Update()
    {
        // --- DETEKSI INPUT TOMBOL ESCAPE (PC) UNTUK PAUSE ---
        if (Input.GetKeyDown(KeyCode.Escape) && !gameSelesai)
        {
            if (sedangPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // Jika game selesai atau sedang dipause, hentikan hitung mundur timer
        if (gameSelesai || sedangPause) return;

        // --- SISTEM TIMERS ---
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

    // --- FUNGSI LOGIC PAUSE & RESUME ---

    // Fungsi untuk menjeda game (Bisa dipanggil via ESC atau Tombol UI HUD di Android)
    public void PauseGame()
    {
        sedangPause = true;
        if (panelPause != null) panelPause.SetActive(true); // Munculkan UI Panel Pause
        Time.timeScale = 0f; // Membekukan semua fisika, gerakan mobil, dan timer
    }

    // Fungsi untuk melanjutkan game (Dipakai di Tombol RESUME)
    public void ResumeGame()
    {
        sedangPause = false;
        if (panelPause != null) panelPause.SetActive(false); // Sembunyikan UI Panel Pause
        Time.timeScale = 1f; // Mengembalikan game berjalan normal kembali
    }

    // Fungsi untuk kembali ke Main Menu (Dipakai di Tombol MAIN MENU)
    public void KembaliKeMainMenu()
    {
        Time.timeScale = 1f; // PENTING! Kembalikan ke 1 agar scene main menu tidak ikut membeku
        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama Scene Main Menu kamu yang sebenarnya
    }

    // Fungsi untuk mengatur visual buah di bak mobil
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
        Time.timeScale = 1f; // Pastikan waktu normal kembali sebelum ganti scene

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