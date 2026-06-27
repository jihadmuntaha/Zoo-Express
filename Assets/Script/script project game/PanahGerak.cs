using UnityEngine;

public class PanahGerak : MonoBehaviour
{
    [Header("Pengaturan Gerakan Naik-Turun")]
    [SerializeField] private float kecepatanGerak = 3f;  // Seberapa cepat naik turunnya
    [SerializeField] private float tinggiGerak = 0.2f;    // Seberapa tinggi jarak naik turunnya

    [Header("Pengaturan Berputar")]
    [SerializeField] private bool apakahBerputar = true;
    [SerializeField] private float kecepatanPutar = 50f;

    private Vector3 posisiAwal;

    void Start()
    {
        posisiAwal = transform.localPosition;
    }

    void Update()
    {
        // Rumus matematika Sinus biar gerakannya smooth melayang
        float posisiYBaru = Mathf.Sin(Time.time * kecepatanGerak) * tinggiGerak;
        transform.localPosition = new Vector3(posisiAwal.x, posisiAwal.y + posisiYBaru, posisiAwal.z);

        if (apakahBerputar)
        {
            transform.Rotate(Vector3.up * kecepatanPutar * Time.deltaTime);
        }
    }
}