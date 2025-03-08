using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour, IObtainable
{
	public void Obtain(GameObject player)
	{
        PlayerWeaponManager weaponManager = player.GetComponent<PlayerWeaponManager>();
        if(!weaponManager.GunIsFull())
        if(weaponManager != null)
        {
            weaponManager.RefillCurrentWeaponAmmo();
            Destroy(gameObject);
        }
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
