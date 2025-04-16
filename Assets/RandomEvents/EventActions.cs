using System;

public static class EventActions
{
    public static Action<RandomEventBase> OnDisplayEvent;
    public static Action<RandomEventOutcome> OnDisplayEventOutcome;

}