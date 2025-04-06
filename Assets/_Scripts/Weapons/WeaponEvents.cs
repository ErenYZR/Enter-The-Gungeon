using System;

public static class WeaponEvents
{
    public static event Action<float> OnReloadStart; // Reload s�resi g�nderilecek
    public static event Action OnReloadCancel; // Reload iptal edilirse
    public static event Action OnReloadFinish; // Reload tamamland�ysa

    public static void TriggerReloadStart(float reloadTime) => OnReloadStart?.Invoke(reloadTime);
    public static void TriggerReloadCancel() => OnReloadCancel?.Invoke();
    public static void TriggerReloadFinish() => OnReloadFinish?.Invoke();
}
