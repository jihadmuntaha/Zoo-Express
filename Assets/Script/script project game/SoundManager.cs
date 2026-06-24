using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Source Anak (Sesuai Hierarchy)")]
    public AudioSource audioSourceMesin;
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceFruits;

    // --- TAMBAHAN SLOT UNTUK MENGONTROL BUAH DI BAK TRUK ---
    [Header("Visual Buah di Bak Truk")]
    [SerializeField] private GameObject buahDiBakTruk; // Seret objek buah yang nempel di bak mobil ke sini

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ApplySavedVolumeSettings();

        // AMANKAN DI AWAL GAME: Paksa buah di bak truk mati/sembunyi saat baru mulai
        if (buahDiBakTruk != null)
        {
            buahDiBakTruk.SetActive(false);
        }
    }

    private void ApplySavedVolumeSettings()
    {
        float volBGM = PlayerPrefs.GetFloat("VolumeBGM", 0.75f);
        int muteBGM = PlayerPrefs.GetInt("MuteBGM", 0);

        float volSFX = PlayerPrefs.GetFloat("VolumeSFX", 0.75f);
        int muteSFX = PlayerPrefs.GetInt("MuteSFX", 0);

        if (audioSourceBGM != null)
        {
            audioSourceBGM.volume = volBGM;
            audioSourceBGM.mute = (muteBGM == 1);
        }

        if (audioSourceMesin != null)
        {
            audioSourceMesin.volume = volSFX;
            audioSourceMesin.mute = (muteSFX == 1);
        }

        if (audioSourceFruits != null)
        {
            audioSourceFruits.volume = volSFX;
            audioSourceFruits.mute = (muteSFX == 1);
        }
    }

    // Fungsi pemicu saat buah di jalan diambil
    public void PutarSFXBuah()
    {
        if (audioSourceFruits != null && !audioSourceFruits.mute)
        {
            audioSourceFruits.PlayOneShot(audioSourceFruits.clip);
        }

        // --- SEKALIGUS NYALAKAN VISUAL BUAH DI BAK TRUK PAS DITABRAK ---
        if (buahDiBakTruk != null)
        {
            buahDiBakTruk.SetActive(true);
        }
    }
}