using System;
using System.Collections.Generic;
using UnityEngine;

public static class CellActions
{
    public static Action<List<Cell>> UpdateCells;
    public static Action<CellEffectUpdateArgs> UpdateCellEffect;
    public static Action<Texture2D> updatedTexture;
}

public class CellEffectUpdateArgs
{
    public Cell cell;
    public CellEffectEnum cellEffect;

}