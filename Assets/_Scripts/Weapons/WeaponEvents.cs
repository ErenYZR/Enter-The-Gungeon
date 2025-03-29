using System;

public static class WeaponEvents
{
    public static event Action<float> OnReloadStart; // Reload süresi gönderilecek
    public static event Action OnReloadCancel; // Reload iptal edilirse
    public static event Action OnReloadFinish; // Reload tamamlandýysa

    public static void TriggerReloadStart(float reloadTime) => OnReloadStart?.Invoke(reloadTime);
    public static void TriggerReloadCancel() => OnReloadCancel?.Invoke();
    public static void TriggerReloadFinish() => OnReloadFinish?.Invoke();
}
