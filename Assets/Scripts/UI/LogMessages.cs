using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogMessages : MonoBehaviour
{
    public RectTransform logMessagesContainer;
    public GameObject logMessagesTemplate;

    private void OnEnable()
    {
        ILogService.onDestinationReached += DisplayLogMessage;
    }

    private void OnDisable()
    {
        ILogService.onDestinationReached -= DisplayLogMessage;
    }

    private void DisplayLogMessage(Guid id)
    {
        GameObject log = Instantiate(logMessagesTemplate, logMessagesContainer);
        log.GetComponent<TMP_Text>().text = $"Agent {id} reached its destination!";
        log.SetActive(true);
        StartCoroutine(DestroyLogAfterDelay(1.5f, log));
    }

    private IEnumerator DestroyLogAfterDelay(float t, GameObject obj)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }
}
