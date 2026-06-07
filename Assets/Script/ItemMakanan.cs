using UnityEngine;

public class ItemMakanan : MonoBehaviour
{
    [Header("Jenis Makanan")]
    public bool iniSemangka = true; // Jika dicentang = Semangka, jika dikosongkan = Daun

    private ManagerGame gameManager;

    void Start()
    {
        // Mencari script ManagerGame otomatis yang ada di Scene
        gameManager = FindObjectOfType<ManagerGame>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Memastikan yang menabrak wadah ini adalah mobil (Player)
        if (other.CompareTag("Player"))
        {
            if (iniSemangka)
            {
                gameManager.punyaSemangka = true;
                gameManager.punyaDaun = false;
                gameManager.UpdateUIStatus("Semangka (Untuk Gajah)");
                gameManager.AturVisualBak(true, false); // Munculin semangka, sembunyiin daun
            }
            else
            {
                gameManager.punyaDaun = true;
                gameManager.punyaSemangka = false;
                gameManager.UpdateUIStatus("Daun (Untuk Jerapah)");
                gameManager.AturVisualBak(false, true); // Sembunyiin semangka, munculin daun
            }
        }
    }
}