using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITickService
{
    public delegate void OnTickIncrease();
    public delegate void OnTickDecrease();
    public delegate void OnTickReset();
    public delegate void OnPauseToggle();

    public static OnTickIncrease onTickIncrease;
    public static OnTickDecrease onTickDecrease;
    public static OnTickReset onTickReset;
    public static OnPauseToggle onPauseToggle;

    public static int currentTickRate { get; set; }
    public static bool isPaused { get; set; }

    void SetTickRate(bool increase);
    void ResetTickRate();
    void TogglePause();
}
