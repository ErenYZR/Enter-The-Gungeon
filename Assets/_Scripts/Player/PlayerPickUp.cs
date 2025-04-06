using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private IObtainable currentPickUp = null;

    // Update is called once per frame
    void Update()
    {
		// E tu�una bas�ld���nda toplama i�lemini ger�ekle�tir
		if (currentPickUp != null && Input.GetKeyDown(KeyCode.E))
		{
			currentPickUp.Obtain(gameObject); // Nesneyi elde et
			currentPickUp = null; // Nesne al�nd�ktan sonra null yap
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PickUp"))
		{
			IObtainable pickup = collision.gameObject.GetComponent<IObtainable>();
			if (pickup != null)
			{
				currentPickUp = pickup; // Oyuncu nesneye yakla�t�, toplama i�lemi yap�labilir
										// Burada oyuncuya bir bildirim veya etkile�im mesaj� g�sterebilirsin.
			}
		}
	}

	// Toplanabilir nesneden uzakla��ld���nda
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PickUp"))
		{
			currentPickUp = null; // Nesneden uzakla��ld���nda etkile�imi iptal et
		}
	}
}
