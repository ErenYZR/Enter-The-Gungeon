using UnityEngine;

public class ChargeWeapons : Weapon
{
	public ChargeWeaponData chargeWeaponData;

	private float chargeTimer = 0f;
	private bool isCharging = false;
	private bool canShoot = false;
	[SerializeField] private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();

		if(chargeWeaponData.animatiorController != null)
		{
			animator.runtimeAnimatorController = chargeWeaponData.animatiorController;
		}
	}

	private void Update()
	{
		animator.SetBool("Charging", isCharging);
		animator.SetBool("Charged", canShoot);

		if (Input.GetMouseButtonDown(0)) // Sol týk basýldý
		{
			isCharging = true;
			chargeTimer = 0f;
			canShoot = false;
		}

		if (Input.GetMouseButton(0)) // Sol týk basýlý tutuluyor
		{
			if (isCharging)
			{
				chargeTimer += Time.deltaTime;
				if (chargeTimer >= chargeWeaponData.chargeTime)
				{
					canShoot = true; // Þarj tamamlandý, ateþ edebilir
					print("Þarj oldu");
				}
			}
		}

		if (Input.GetMouseButtonUp(0)) // Sol týk býrakýldý
		{
			if (isCharging && canShoot/* && timeToFire <= 0f*/) // Eðer yeterince uzun basýldýysa
			{
				Fire();
				//timeToFire = weaponData.fireRate;
			}

			isCharging = false;
			canShoot = false;
		}

		/*if (timeToFire > 0)
		{
			timeToFire -= Time.deltaTime;
		}*/
	}

	public override void Fire()
	{
		if (Input.GetMouseButtonUp(0)) // Sol týk býrakýldý
		{
			if (isCharging && canShoot/* && timeToFire <= 0f*/) // Eðer yeterince uzun basýldýysa
			{
				float spread = Random.Range(-weaponData.spreadAngle / 2, weaponData.spreadAngle / 2);
				Quaternion bulletRotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + spread);

				GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
				//bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * weaponData.bulletSpeed;
				bullet.GetComponent<Bullet>().speed = weaponData.bulletSpeed;
				bullet.GetComponent<Bullet>().damage = weaponData.damage;
				//timeToFire = weaponData.fireRate;
				animator.SetTrigger("Relased");
				currentClipAmmo--;
			}

			isCharging = false;
			canShoot = false;
		}
	}
}
