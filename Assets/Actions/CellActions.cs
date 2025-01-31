using System;
using System.Collections.Generic;

public static class CellActions
{
    public static Action<List<Cell>> UpdateCells;
    public static Action<CellEffectUpdateArgs> UpdateCellEffect;
}

public class CellEffectUpdateArgs
{
    public Cell cell;
    public CellEffectEnum cellEffect;

}