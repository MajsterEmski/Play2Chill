using UnityEngine;

public class TimeButtons : MonoBehaviour, ITickService
{
    public void SetTickRate(bool increase)
    {
        if (increase)
            ITickService.onTickIncrease?.Invoke();
        else
            ITickService.onTickDecrease?.Invoke();
    }

    public void ResetTickRate()
    {
        ITickService.onTickReset?.Invoke();
    }

    public void TogglePause()
    {
        ITickService.onPauseToggle?.Invoke();
    }
}
