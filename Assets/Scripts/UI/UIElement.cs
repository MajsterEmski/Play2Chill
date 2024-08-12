using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour, IAgentService
{
    public void RequestAgentSpawn()
    {
        IAgentService.onAgentSpawn?.Invoke();
    }
    public void RequestAgentDestroy()
    {
        IAgentService.onAgentDestroy?.Invoke();
    }
    public void RequestAgentClear()
    {
        IAgentService.onAgentClear?.Invoke();
    }
}
