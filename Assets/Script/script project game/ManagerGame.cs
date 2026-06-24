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

    // --- SOUND SYSTEM GABUNGAN ---
    [Header("Sistem Audio Gameplay")]
    [SerializeField] private AudioSource audioSourceBGM;    // Tarik BGM_Gameplay ke sini
    [SerializeField] private AudioSource audioSourceMesin;  // Tarik SFX_Mesin ke sini
    [SerializeField] private AudioSource audioSourceGameOver; // SLOT BARU: Tarik AudioSource khusus Game Over ke sini

    void Start()
    {
        UpdateUIStatus("Tidak Membawa Apapun");
        AturVisualBak(false, false);

        if (panelPause != null) panelPause.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);

        // Pastikan audio khusus Game Over mati saat game baru mulai
        if (audioSourceGameOver != null) audioSourceGameOver.Stop();

        Time.timeScale = 1f;
    }

    void Update()
    {
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

    public void PauseGame()
    {
        if (gameSelesai) return;
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
            Time.timeScale = 1f;
            AturVisualBak(false, false);
            int sceneSekarang = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneSekarang + 1);
        }
        else
        {
            // --- LOGIC SAAT GAME OVER (WAKTU HABIS) ---
            Time.timeScale = 0f;

            // 1. Matikan BGM Utama & Suara Mesin biar gak tabrakan
            if (audioSourceBGM != null) audioSourceBGM.Stop();
            if (audioSourceMesin != null) audioSourceMesin.Stop();

            // 2. Bunyikan Musik/SFX Game Over khusus
            if (audioSourceGameOver != null)
            {
                audioSourceGameOver.Play();
            }

            if (panelGameOver != null)
            {
                panelGameOver.SetActive(true);
            }
        }
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}