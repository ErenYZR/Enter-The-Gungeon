using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public virtual void Fire()
    {
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * weaponData.bulletSpeed;
            bullet.GetComponent<Bullet>().damage = weaponData.damage;
    }
}
