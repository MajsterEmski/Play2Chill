using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentTimeLabel : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] GameObject pauseNotif;
    private void OnEnable()
    {
        ITickService.onTickIncrease += UpdateLabel;
        ITickService.onTickDecrease += UpdateLabel;
        ITickService.onTickReset += UpdateLabel;
        ITickService.onPauseToggle += UpdatePauseNotif;
    }

    private void OnDisable()
    {
        ITickService.onTickIncrease -= UpdateLabel;
        ITickService.onTickDecrease -= UpdateLabel;
        ITickService.onTickReset -= UpdateLabel;
        ITickService.onPauseToggle -= UpdatePauseNotif;
    }

    public void UpdatePauseNotif()
    {
        pauseNotif.SetActive(ITickService.isPaused);
    }

    public void UpdateLabel()
    {
        label.text = "x" + ITickService.currentTickRate.ToString();
    }
}
