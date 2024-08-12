using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AgentManager : MonoBehaviour
{
    //Setting up a pool to avoid the performance hit of endlessly instantiating and destroying agents :)
    public ObjectPool<Agent> poolAgents;

    private void OnEnable()
    {
        IAgentService.onAgentSpawn += SpawnAgent;
        IAgentService.onAgentClear += ClearAgents;
        IAgentService.onAgentDestroy += DestroyAgent;
        ITickService.onTickIncrease += IncreaseTick;
        ITickService.onTickDecrease += DecreaseTick;
        ITickService.onTickReset += ResetTick;
        ITickService.onPauseToggle += TogglePause;
    }

    private void OnDisable()
    {
        IAgentService.onAgentSpawn -= SpawnAgent;
        IAgentService.onAgentClear -= ClearAgents;
        IAgentService.onAgentDestroy -= DestroyAgent;
        ITickService.onTickIncrease -= IncreaseTick;
        ITickService.onTickDecrease -= DecreaseTick;
        ITickService.onTickReset -= ResetTick;
        ITickService.onPauseToggle -= TogglePause;
    }

    private void Start()
    {
        //Setting initial values
        IAgentService.onAgentClear?.Invoke();
        ITickService.onTickReset?.Invoke();
        ITickService.isPaused = false;
    }

    public void SpawnAgent()
    {
        Debug.Log("Agent Manager attempting to spawn agents!");
        IAgentService.agentsNumber++;
    }

    public void DestroyAgent()
    {
        Debug.Log("Agent Manager attempting to destroy a random agent!");
        if (IAgentService.agentsNumber <= 0) return;
        IAgentService.agentsNumber--;
    }

    public void ClearAgents()
    {
        Debug.Log("Agent Manager attempting to clear agents!");
        IAgentService.agentsNumber = 0;
    }

    public void IncreaseTick()
    {
        Debug.Log("Agent Manager attempting to increase the tick rate!");
        if (ITickService.currentTickRate >= 5) return;
        ITickService.currentTickRate++;
    }

    public void DecreaseTick() 
    {
        Debug.Log("Agent Manager attempting to decrease the tick rate!");
        if (ITickService.currentTickRate <= 1) return;
        ITickService.currentTickRate--;
    }

    public void ResetTick()
    {
        Debug.Log("Agent Manager attempting to reset the tick rate!");
        ITickService.currentTickRate = 1;
    }

    public void TogglePause()
    {
        Debug.Log("Agent Manager attempting to toggle pause!");
        ITickService.isPaused = !ITickService.isPaused;
    }
}
