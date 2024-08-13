using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILogService
{
    public delegate void OnDestinationReached(Guid id);
    public static OnDestinationReached onDestinationReached;
}
