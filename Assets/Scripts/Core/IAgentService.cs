using System;
using System.Collections;
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

    public static int agentsNumber { get; set; }
    public static List<Guid> agentsGuids = new List<Guid>();

    void RequestAgentSpawn();
    void RequestAgentDestroy();
    void RequestAgentClear();
}
