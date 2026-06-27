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
        if (other.CompareTag("Player"))
        {
            // LOG BARU: Untuk memastikan objek mana yang mendeteksi tabrakan
            Debug.Log("WADAH DITABRAK: " + gameObject.name + " | Status iniSemangka di Inspector: " + iniSemangka);

            if (iniSemangka)
            {
                gameManager.punyaSemangka = true;
                gameManager.punyaDaun = false;
                gameManager.UpdateUIStatus("Semangka (Untuk Gajah)");
                gameManager.AturVisualBak(true, false);
            }
            else
            {
                gameManager.punyaDaun = true;
                gameManager.punyaSemangka = false;
                gameManager.UpdateUIStatus("Daun (Untuk Jerapah)");
                gameManager.AturVisualBak(false, true);
            }
        }
    }
}