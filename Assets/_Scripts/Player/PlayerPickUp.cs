using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private IObtainable currentPickUp = null;

    // Update is called once per frame
    void Update()
    {
		// E tuþuna basýldýðýnda toplama iþlemini gerçekleþtir
		if (currentPickUp != null && Input.GetKeyDown(KeyCode.E))
		{
			currentPickUp.Obtain(gameObject); // Nesneyi elde et
			currentPickUp = null; // Nesne alýndýktan sonra null yap
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PickUp"))
		{
			IObtainable pickup = collision.gameObject.GetComponent<IObtainable>();
			if (pickup != null)
			{
				currentPickUp = pickup; // Oyuncu nesneye yaklaþtý, toplama iþlemi yapýlabilir
										// Burada oyuncuya bir bildirim veya etkileþim mesajý gösterebilirsin.
			}
		}
	}

	// Toplanabilir nesneden uzaklaþýldýðýnda
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PickUp"))
		{
			currentPickUp = null; // Nesneden uzaklaþýldýðýnda etkileþimi iptal et
		}
	}
}
