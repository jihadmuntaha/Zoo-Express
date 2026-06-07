using UnityEngine;

public class Coin : MonoBehaviour
{
    int coinValue = 1; // Variable untuk menyimpan nilai koin
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Koin berhasil diambil!"); 
            coinValue++;
            Destroy(gameObject); // Destroy the coin after collection
        }
    }
}
