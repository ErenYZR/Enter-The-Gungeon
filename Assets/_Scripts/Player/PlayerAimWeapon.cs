using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
	public event EventHandler<OnShootEventArgs> OnShoot;

	public class OnShootEventArgs : EventArgs
	{
		public Vector3 gunEndPosition;
		public Vector3 shootPosition;
	}

	private Transform aimTransform;
	public Camera cam;
	private Vector3 mousePos;
	public SpriteRenderer aimSprite;
	public Transform aimGunEndPositionTransform;
	private PlayerWeaponManager weaponManager;

	//bullet
	[SerializeField] private GameObject bulletPrefab;
	public float fireTimer;

	private Animator aimAnimator;

	private void Awake()
	{
		aimTransform = transform.Find("Aim");
		aimAnimator = aimTransform.GetComponent<Animator>();
		aimGunEndPositionTransform = aimTransform.Find("GunEndPointPosition");
		weaponManager = GetComponent<PlayerWeaponManager>();
	}

	private void Update()
	{
		HandleAiming();
		if (Input.GetMouseButton(0))
		{
			weaponManager.Shoot();
			print("Ateþ");
		}
		HandleShooting();
	}

	private void HandleAiming()
	{
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0f;

		Vector3 aimDirection = (mousePos - aimGunEndPositionTransform.position).normalized;
		float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
		aimTransform.eulerAngles = new Vector3(0, 0, angle);
	}

	private void HandleShooting()
	{
		if (Input.GetMouseButton(0) && fireTimer <=0f)
		{
			aimAnimator.SetTrigger("Shoot");

			OnShoot?.Invoke(this, new OnShootEventArgs {
				gunEndPosition = aimGunEndPositionTransform.position,
				shootPosition = mousePos,
			});
		}
		else
		{
			fireTimer -= Time.deltaTime;
		}
	}
	

}
