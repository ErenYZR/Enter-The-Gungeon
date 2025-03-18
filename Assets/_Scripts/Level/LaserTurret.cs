using System.Collections;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
	public enum LaserDirection { Up, Down, Left, Right }
	public LaserDirection laserDirection;

	[SerializeField] private float detectionRange = 5f;
	[SerializeField] private float chargeTime = 1.5f;
	[SerializeField] private LayerMask targetLayer; // Oyuncu ve düþmanlarý içeren layer
	[SerializeField] private GameObject laserBeamPrefab;

	private bool isFiring = false;
	private Vector2 directionVector;

	private void Start()
	{
		SetDirection();
	}

	private void Update()
	{
		DetectTargets();
	}

	private void SetDirection()
	{
		switch (laserDirection)
		{
			case LaserDirection.Up: 
				directionVector = Vector2.up;
				transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
			case LaserDirection.Down:
				directionVector = Vector2.down;
				transform.rotation = Quaternion.Euler(0, 0, 270);
				break;
			case LaserDirection.Left: 
				directionVector = Vector2.left;
				transform.rotation = Quaternion.Euler(0, 0, 180);
				break;
			case LaserDirection.Right:
				directionVector = Vector2.right;
				transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
		}
	}

	private void DetectTargets()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVector, detectionRange, targetLayer);

		if (hit.collider != null && !isFiring) // Hedef algýlandý ve zaten ateþ etmiyorsa
		{
			StartCoroutine(FireLaser());
		}
	}

	private IEnumerator FireLaser()
	{
		isFiring = true;
		yield return new WaitForSeconds(chargeTime); // Þarj süresi bekleniyor

		// Lazer ýþýný oluþtur
		GameObject laser = Instantiate(laserBeamPrefab, transform.position, Quaternion.identity);
		LaserBeam beam = laser.GetComponent<LaserBeam>();
		beam.SetLaserDirection(directionVector, detectionRange);

		isFiring = false;
	}
}
