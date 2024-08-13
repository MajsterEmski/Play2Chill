using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LogMessages : MonoBehaviour
{
    public RectTransform logMessagesContainer;
    public GameObject logMessagesTemplate;

    private void OnEnable()
    {
        ILogService.onDestinationReached += LogDestinationReached;
        ILogService.onTickAboveLimit += LogTickAboveLimit;
        ILogService.onTickBelowLimit += LogTickBelowLimit;
    }

    private void OnDisable()
    {
        ILogService.onDestinationReached -= LogDestinationReached;
        ILogService.onTickAboveLimit -= LogTickAboveLimit;
        ILogService.onTickBelowLimit -= LogTickBelowLimit;
    }

    private void LogDestinationReached(Guid id)
    {
        DisplayLog($"Agent {id} reached its destination!");
    }

    private void LogTickAboveLimit()
    {
        DisplayLog("Attempting to increase the tick rate beyond the upper limit!");
    }

    private void LogTickBelowLimit()
    {
        DisplayLog("Attempting to decrease the tick rate below the lower limit!");
    }

    private void DisplayLog(string message)
    {
        GameObject log = Instantiate(logMessagesTemplate, logMessagesContainer);
        log.GetComponent<TMP_Text>().text = message;
        log.SetActive(true);
        StartCoroutine(DestroyLogAfterDelay(1.5f, log));
    }

    private IEnumerator DestroyLogAfterDelay(float t, GameObject obj)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }
}
