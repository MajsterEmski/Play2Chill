using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgentAmountLabel : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    private void OnEnable()
    {
        IAgentService.onAgentSpawn += UpdateCounter;
        IAgentService.onAgentDestroy += UpdateCounter;
        IAgentService.onAgentClear += UpdateCounter;
    }

    private void OnDisable()
    {
        IAgentService.onAgentSpawn -= UpdateCounter;
        IAgentService.onAgentDestroy -= UpdateCounter;
        IAgentService.onAgentClear -= UpdateCounter;
    }

    public void UpdateCounter()
    {
        label.text = IAgentService.agents.Count.ToString();
    }
}
