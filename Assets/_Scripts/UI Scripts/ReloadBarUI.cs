using UnityEngine;
using UnityEngine.UI;

public class ReloadBarUI : MonoBehaviour
{
	[SerializeField] private Image reloadBar;
	private float reloadTime;
	private float elapsedTime;
	private bool isReloading;

	private void OnEnable()
	{
		WeaponEvents.OnReloadStart += StartReload;
		WeaponEvents.OnReloadCancel += CancelReload;
		WeaponEvents.OnReloadFinish += FinishReload;
		HideBar();
	}

	private void OnDisable()
	{
		WeaponEvents.OnReloadStart -= StartReload;
		WeaponEvents.OnReloadCancel -= CancelReload;
		WeaponEvents.OnReloadFinish -= FinishReload;
	}

	private void Update()
	{
		if (isReloading)
		{
			elapsedTime += Time.deltaTime;
			reloadBar.fillAmount = elapsedTime / reloadTime;
		}
	}

	private void StartReload(float time)
	{
		reloadTime = time;
		elapsedTime = 0f;
		isReloading = true;
		reloadBar.fillAmount = 0f;
		reloadBar.gameObject.SetActive(true);
	}

	private void CancelReload()
	{
		isReloading = false;
		reloadBar.gameObject.SetActive(false);
	}

	private void FinishReload()
	{
		isReloading = false;
		reloadBar.fillAmount = 1f;
		Invoke(nameof(HideBar), 0.2f); // Biraz bekleyip kaybolsun
	}

	private void HideBar()
	{
		reloadBar.gameObject.SetActive(false);
	}
}
