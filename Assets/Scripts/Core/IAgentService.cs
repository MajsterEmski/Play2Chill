using System.Collections.Generic;
using UnityEngine;

public interface IAgentService
{
    public delegate void OnAgentSpawn();
    public delegate void OnAgentDestroy();
    public delegate void OnAgentClear();

    public static OnAgentSpawn onAgentSpawn;
    public static OnAgentDestroy onAgentDestroy;
    public static OnAgentClear onAgentClear;

    public static List<GameObject> agents = new List<GameObject>();

    void RequestAgentSpawn();
    void RequestAgentDestroy();
    void RequestAgentClear();
}
