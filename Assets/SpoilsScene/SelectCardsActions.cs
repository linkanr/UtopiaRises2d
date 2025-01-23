using System;

public class SelectCardsActions
{

    public static Action <SoCardBase> OnCardLockedIn;
    public static event Action<SelectionBase> OnCardSelected;

    public static void InvokeCardSelected(SelectionBase selectionBase)
    {
        OnCardSelected?.Invoke(selectionBase);
    }

}