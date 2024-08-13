using System;

public interface ILogService
{
    public delegate void OnDestinationReached(Guid id);
    public delegate void OnTickAboveLimit();
    public delegate void OnTickBelowLimit();

    public static OnDestinationReached onDestinationReached;
    public static OnTickAboveLimit onTickAboveLimit;
    public static OnTickBelowLimit onTickBelowLimit;
}
