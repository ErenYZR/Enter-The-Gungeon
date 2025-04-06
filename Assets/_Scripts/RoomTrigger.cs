using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
	[SerializeField] private RoomController roomController;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			roomController.TryActivateRoom(); // Odayý aktive et
			gameObject.SetActive(false); // Tek seferlik çalýþsýn
		}
	}
}
