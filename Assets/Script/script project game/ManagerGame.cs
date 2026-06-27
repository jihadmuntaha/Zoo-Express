using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
    [Header("Status Makanan")]
    public bool punyaSemangka = false;
    public bool punyaDaun = false;

    [Header("Visual Makanan di Bak Mobil")]
    public GameObject buahSemangkaDiBak;
    public GameObject daunDiBak;

    [Header("UI Canvas HUD")]
    public TextMeshProUGUI teksStatusMakanan;
    public TextMeshProUGUI teksTimer;

    [Header("Pengaturan Waktu")]
    public float sisaWaktu = 40f;
    private bool gameSelesai = false;

    [Header("Sistem Pause")]
    [SerializeField] private GameObject panelPause;
    private bool sedangPause = false;

    [Header("Sistem Game Over")]
    [SerializeField] private GameObject panelGameOver;

    // --- FITUR BARU: SISTEM MENANG (WIN) ---
    [Header("Sistem Menang (Win)")]
    [SerializeField] private GameObject panelWin; // Drag objek Panel_Win ke sini

    // --- TAMBAHAN FITUR TUTORIAL ---
    [Header("Sistem Tutorial Awal")]
    [SerializeField] private GameObject panelTutorial; // Drag objek Panel_Tutorial ke sini
    private bool tutorialSelesai = false; // Penanda apakah game sudah boleh dimulai

    [Header("Sistem Audio Gameplay")]
    [SerializeField] private AudioSource audioSourceBGM;
    [SerializeField] private AudioSource audioSourceMesin;
    [SerializeField] private AudioSource audioSourceGameOver;

    void Start()
    {
        UpdateUIStatus("Tidak Membawa Apapun");
        AturVisualBak(false, false);

        if (panelPause != null) panelPause.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelWin != null) panelWin.SetActive(false); // Pastikan panel win mati di awal

        // LOGIC TUTORIAL: Jika panel tutorial ada, freeze dunia game di awal!
        if (panelTutorial != null)
        {
            panelTutorial.SetActive(true);
            tutorialSelesai = false;
            Time.timeScale = 0f; // Bekukan game sementara pemain membaca tutorial
        }
        else
        {
            tutorialSelesai = true;
            Time.timeScale = 1f;
        }

        if (audioSourceGameOver != null) audioSourceGameOver.Stop();
    }

    void Update()
    {
        // Jika tutorial belum selesai dibaca, jangan jalankan input pause game dulu
        if (!tutorialSelesai) return;

        if (Input.GetKeyDown(KeyCode.Escape) && !gameSelesai)
        {
            if (sedangPause) ResumeGame();
            else PauseGame();
        }

        if (gameSelesai || sedangPause) return;

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

    // --- FUNGSI BARU: DIPANGGIL SAAT TOMBOL 'MULAI' DI PANEL TUTORIAL DIPENCET ---
    public void TutupTutorialDanMulai()
    {
        tutorialSelesai = true;
        if (panelTutorial != null) panelTutorial.SetActive(false); // Sembunyikan panel tutorial
        Time.timeScale = 1f; // Jalankan kembali waktu dan fisik game!
    }

    public void PauseGame()
    {
        if (gameSelesai || !tutorialSelesai) return;
        sedangPause = true;
        if (panelPause != null) panelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        sedangPause = false;
        if (panelPause != null) panelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void KembaliKeMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void AturVisualBak(bool tampilkanSemangka, bool tampilkanDaun)
    {
        if (buahSemangkaDiBak != null) buahSemangkaDiBak.SetActive(tampilkanSemangka);
        if (daunDiBak != null) daunDiBak.SetActive(tampilkanDaun);
    }

    public void UpdateUIStatus(string status)
    {
        if (teksStatusMakanan != null)
        {
            teksStatusMakanan.text = "Membawa: " + status;
        }
    }

    public void LolosAtauKalah(bool apakahMenang)
    {
        gameSelesai = true;

        if (apakahMenang)
        {
            // --- KINI MEMBUKA PANEL WIN SAAT MENANG ---
            Time.timeScale = 0f; // Freeze game agar mobil langsung berhenti saat menang
            AturVisualBak(false, false);

            if (panelWin != null)
            {
                panelWin.SetActive(true); // Memunculkan panel berisi Next Game, Retry, Main Menu
            }

            if (audioSourceMesin != null) audioSourceMesin.Stop(); // Matikan suara mesin biar rapi
        }
        else
        {
            Time.timeScale = 0f;
            if (audioSourceBGM != null) audioSourceBGM.Stop();
            if (audioSourceMesin != null) audioSourceMesin.Stop();
            if (audioSourceGameOver != null) audioSourceGameOver.Play();

            if (panelGameOver != null) panelGameOver.SetActive(true);
        }
    }

    // --- FUNGSI BARU UNTUK TOMBOL NEXT GAME / NEXT LEVEL ---
    public void NextLevel()
    {
        Time.timeScale = 1f; // Kembalikan waktu normal sebelum pindah scene
        int sceneSekarang = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneSekarang + 1); // Pindah otomatis ke scene index berikutnya (Level 2)
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}