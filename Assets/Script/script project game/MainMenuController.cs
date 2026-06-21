using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject panelCredit;

    [Header("Audio Sliders & Toggles")]
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Toggle toggleMuteBGM;
    [SerializeField] private Toggle toggleMuteSFX;

    [Header("Graphics Toggles")]
    [SerializeField] private Toggle toggleShadows;

    [Header("Audio Sources Target")]
    [SerializeField] private AudioSource audioSourceBGM; // Drag objek Musik_Latar ke sini
    [SerializeField] private AudioSource audioSourceSFX; // Drag objek SFX_Klik_Tombol ke sini

    void Start()
    {
        if (panelSettings != null) panelSettings.SetActive(false);
        if (panelCredit != null) panelCredit.SetActive(false);

        // 1. Load Data Volume Audio
        if (sliderBGM != null) sliderBGM.value = PlayerPrefs.GetFloat("VolumeBGM", 0.75f);
        if (sliderSFX != null) sliderSFX.value = PlayerPrefs.GetFloat("VolumeSFX", 0.75f);

        // 2. Load Status Mute BGM
        if (toggleMuteBGM != null)
        {
            int statusMuteBGM = PlayerPrefs.GetInt("MuteBGM", 0);
            toggleMuteBGM.isOn = (statusMuteBGM == 1);
            AplikasiMuteBGM(toggleMuteBGM.isOn);
        }

        // 3. Load Status Mute SFX
        if (toggleMuteSFX != null)
        {
            int statusMuteSFX = PlayerPrefs.GetInt("MuteSFX", 0);
            toggleMuteSFX.isOn = (statusMuteSFX == 1);
            AplikasiMuteSFX(toggleMuteSFX.isOn);
        }

        // 4. Load Status Grafik Bayangan
        if (toggleShadows != null)
        {
            int statusShadow = PlayerPrefs.GetInt("ShadowSetting", 1);
            toggleShadows.isOn = (statusShadow == 1);
            AplikasiGrafikBayangan(toggleShadows.isOn);
        }
    }

    // --- FUNGSI NAVIGASI MENU ---
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void BukaSettings()
    {
        if (panelSettings != null) panelSettings.SetActive(true);
    }

    public void TutupSettings()
    {
        if (panelSettings != null) panelSettings.SetActive(false);
    }

    public void BukaCredit()
    {
        if (panelCredit != null) panelCredit.SetActive(true);
    }

    public void TutupCredit()
    {
        if (panelCredit != null) panelCredit.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Game Ditutup!");
        Application.Quit();
    }

    // --- LOGIC KONTROL AUDIO ---

    public void UbahVolumeBGM()
    {
        if (sliderBGM != null && audioSourceBGM != null)
        {
            PlayerPrefs.SetFloat("VolumeBGM", sliderBGM.value);
            // Hanya ganti volume jika tidak sedang dicentang mute
            if (toggleMuteBGM != null && !toggleMuteBGM.isOn)
            {
                audioSourceBGM.volume = sliderBGM.value;
            }
        }
    }

    public void UbahVolumeSFX()
    {
        if (sliderSFX != null && audioSourceSFX != null)
        {
            PlayerPrefs.SetFloat("VolumeSFX", sliderSFX.value);
            if (toggleMuteSFX != null && !toggleMuteSFX.isOn)
            {
                audioSourceSFX.volume = sliderSFX.value;
            }
        }
    }

    public void KlikToggleMuteBGM(bool apakahMute)
    {
        PlayerPrefs.SetInt("MuteBGM", apakahMute ? 1 : 0);
        AplikasiMuteBGM(apakahMute);
    }

    private void AplikasiMuteBGM(bool apakahMute)
    {
        if (audioSourceBGM == null) return;

        audioSourceBGM.mute = apakahMute; // Langsung true/false sesuai centang toggle
        if (!apakahMute && sliderBGM != null)
        {
            audioSourceBGM.volume = sliderBGM.value; // Kembalikan volume sesuai slider
        }
    }

    public void KlikToggleMuteSFX(bool apakahMute)
    {
        PlayerPrefs.SetInt("MuteSFX", apakahMute ? 1 : 0);
        AplikasiMuteSFX(apakahMute);
    }

    private void AplikasiMuteSFX(bool apakahMute)
    {
        if (audioSourceSFX == null) return;

        audioSourceSFX.mute = apakahMute; // Langsung true/false sesuai centang toggle
        if (!apakahMute && sliderSFX != null)
        {
            audioSourceSFX.volume = sliderSFX.value; // Kembalikan volume sesuai slider
        }
    }

    public void PutarSFXKlik()
    {
        if (audioSourceSFX != null && !audioSourceSFX.mute)
        {
            audioSourceSFX.Play();
        }
    }

    // --- LOGIC GRAFIK ---
    public void KlikToggleShadows(bool bayanganAktif)
    {
        PlayerPrefs.SetInt("ShadowSetting", bayanganAktif ? 1 : 0);
        AplikasiGrafikBayangan(bayanganAktif);
    }

    private void AplikasiGrafikBayangan(bool bayanganAktif)
    {
        if (bayanganAktif)
        {
            QualitySettings.SetQualityLevel(2, true);
        }
        else
        {
            QualitySettings.SetQualityLevel(0, true);
        }
    }
}