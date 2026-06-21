using UnityEngine;
using UnityEngine.EventSystems; // Wajib untuk mendeteksi sentuhan ditahan/lepas

public class TombolInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public bool sedangDitekan = false;

    // Fungsi otomatis berjalan saat layar Android disentuh/ditahan
    public void OnPointerDown(PointerEventData eventData)
    {
        sedangDitekan = true;
    }

    // Fungsi otomatis berjalan saat jari diangkat dari layar
    public void OnPointerUp(PointerEventData eventData)
    {
        sedangDitekan = false;
    }
}