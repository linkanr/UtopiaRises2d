using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSelectionHandler
{
    private readonly Card card;
    private Color naColor = Color.red;
    private Color AvilibleColor = Color.green;

    public CardSelectionHandler(Card card)
    {
        this.card = card;
    }

    /// <summary>
    /// Determines if the card is currently clickable.
    /// </summary>
    public bool IsCardClickable()
    {
        bool isClickable = (card.cardState == CardStateEnum.availible || card.cardState == CardStateEnum.mousedOver) &&
                           card.mode == Card.CardMode.playable;

        return isClickable;
    }

    /// <summary>
    /// Locks the card for selection and updates its state.
    /// </summary>
    public void LockCardForSelection()
    {
        DisplayActions.OnRemoveMouseSprite?.Invoke();
        // Update mouse display for cards with a click effect
        if (card.cardBase is IHasClickEffect effect)
        {
            DisplayActions.OnRemoveMouseSprite?.Invoke();
            DisplayActions.OnSetMouseSprite?.Invoke(new OnDisplaySpriteArgs(effect.GetSprite(), effect.GetSpriteSize()));
        }

        // Highlight scene objects if the card can be played on them
        if (card.cardBase.cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.damagable)
        {
            DisplayActions.OnHighligtSceneObject(true);
        }

        // Lock the card for selection
        card.isSelected = true;
        card.cardState = CardStateEnum.lockedForSelection;

        // Animate card to visually indicate selection
        card.cardAnimations.AnimateScale(card.cardAnimations.clickScale, card);

        
    }

    /// <summary>
    /// Highlights the playable area based on the card's playability.
    /// </summary>
    private void HighlightPlayableArea()
    {
        int sizeX = 1;
        int sizeY = 1;
        bool activateDisplay = true;
        // Adjust the playable area for cards with instantiation size
        if (card.cardBase is SoCardInstanciate soCardInstanciate)
        {
            sizeX = soCardInstanciate.sizeX;
            sizeY = soCardInstanciate.sizeY;
        }
        if (card.cardBase.cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.instantClick)
        {

            activateDisplay = false;
        }
         
        Cell activeCell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();
        if (activeCell == null)
        {
            
            return;
        }
        SceneObject[] containingObject = activeCell.containingSceneObjects.ToArray();
        Color displayColor;

        if (!activeCell.hasSceneObejct)
        {
            //Debug.Log("Empty cell");
            displayColor = HandleEmptyCell(activeCell);
        }
        else
        {
            //Debug.Log("Non empty cell");
            displayColor = HandleNonEmptyCell(containingObject, activeCell);
        }

        SetDisplayHandle(displayColor, activateDisplay, sizeX, sizeY);
    }

    private Color HandleNonEmptyCell(SceneObject[] containingObject, Cell activeCell)
    {
       
        switch (card.cardBase.cardCanBePlayedOnEnum)
        {
            case CardCanBePlayedOnEnum.damagable:
                Debug.Log("damagable");
                foreach (SceneObject sceneObject in containingObject)
                {
                    if (sceneObject.healthSystem != null)
                    {
                        
                        return AvilibleColor;
                    }
                }
                
                return naColor;
                
            case CardCanBePlayedOnEnum.influencedTerritory:
                foreach (SceneObject sceneObject in containingObject)
                {
                    if (sceneObject.GetStats().sceneObjectType == SceneObjectTypeEnum.playerConstructionBase)
                    {
                    
                        return AvilibleColor;
                    }
                }
          
                return naColor;

            case CardCanBePlayedOnEnum.enemyGround:
                if (activeCell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil || activeCell.cellTerrain.cellTerrainEnum==CellTerrainEnum.grass)
                {
                  
                    return AvilibleColor;
                }
                else
                {
                   
                    return naColor;
                }
            case CardCanBePlayedOnEnum.playerGround:
                if (activeCell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain)
                {
                
                    return AvilibleColor;
                }
                else
                {
                    return naColor;
                }
            case CardCanBePlayedOnEnum.playerOrEnemyGround:
                {
                    if (activeCell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil || activeCell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain || activeCell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass)
                    {
                  
                        return AvilibleColor;
                    }
                    else
                    {
                        return naColor;
                    }

                }
             default:
             return naColor;
        }
    }

    private void SetDisplayHandle(Color displayColor, bool active, int sizeX, int sizeY)
    {
        //Debug.Log("SetDisplayHandle to active: " + active);
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(
        active,displayColor,
        sizeX, sizeY));
    }

    private Color HandleEmptyCell(Cell activeCell)
    {
        //Debug.Log("HandleEmptyCell");
        CellTerrainEnum cellTerrainEnum = activeCell.cellTerrain.cellTerrainEnum;
        List<CardCanBePlayedOnEnum> avilibleEnums = GetAvibleEnums(cellTerrainEnum);
        //Debug.Log("avilibleEnums count: " + avilibleEnums.Count);
        foreach (CardCanBePlayedOnEnum cardCanBePlayedOnEnum in avilibleEnums)
        {

            if (cardCanBePlayedOnEnum == card.cardBase.cardCanBePlayedOnEnum)
            {
                if (cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.influencedTerritory)
                {
                    if (activeCell.isPlayerInfluenced)
                    {
                        return AvilibleColor;
                    }
                }
                else
                {
                    return AvilibleColor;
                }
                

                
            }
           
          

        }
        //Debug.Log("HandleEmptyCell No cardcanbeplayedEnum found naColor");
        return naColor;
    }

    private List<CardCanBePlayedOnEnum> GetAvibleEnums(CellTerrainEnum cellTerrainEnum)
    {
        List<CardCanBePlayedOnEnum> returnList = new List<CardCanBePlayedOnEnum>();
        switch (cellTerrainEnum)
        {
            case CellTerrainEnum.playerTerrain:
                returnList.Add(CardCanBePlayedOnEnum.playerGround);
                returnList.Add(CardCanBePlayedOnEnum.playerOrEnemyGround);
                returnList.Add(CardCanBePlayedOnEnum.influencedTerritory);

                break;
            case CellTerrainEnum.soil:
                returnList.Add(CardCanBePlayedOnEnum.enemyGround);
                returnList.Add(CardCanBePlayedOnEnum.playerOrEnemyGround);
                break;
            case CellTerrainEnum.grass:
                returnList.Add(CardCanBePlayedOnEnum.enemyGround);
                returnList.Add(CardCanBePlayedOnEnum.playerOrEnemyGround);
                break;
            case CellTerrainEnum.water:
                returnList.Add(CardCanBePlayedOnEnum.enemyGround);
                returnList.Add(CardCanBePlayedOnEnum.playerOrEnemyGround);
                break;

        }
        return returnList;
    }


    /// <summary>
    /// Handles mouse click updates for card selection.
    /// </summary>
    public void HandleSelectionUpdate()
    {
        if (card.mode == Card.CardMode.playable)
            HighlightPlayableArea(); 
        if (Input.GetMouseButtonDown(0) && !MouseDisplayManager.instance.mouseOverCard)
        {
            ProcessSelectionClick();
        }
    }

    /// <summary>
    /// Processes the selection click logic.
    /// </summary>
    private void ProcessSelectionClick()
    {
        if (card.mode != Card.CardMode.playable)
        {
            return;
        }
        ClickableType clickableType = WorldSpaceUtils.CheckClickableType();

        switch (card.cardBase.cardCanBePlayedOnEnum)
        {
            case CardCanBePlayedOnEnum.instantClick:
                if (clickableType != ClickableType.card)
                {
                    PlayCard();
                }
                else
                {
                    ResetCardSelection();
                }
                break;

            case CardCanBePlayedOnEnum.damagable:
                if (clickableType == ClickableType.SceneObject)
                {
                    PlayCard();
                }
                else
                {
                    ResetCardSelection();
                }
                break;

            case CardCanBePlayedOnEnum.playerGround:
                if (clickableType != ClickableType.card)
                {
                    Cell cell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();
                    if (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain)
                    {
                        PlayCard();
                    }
                    else
                    {
                        ResetCardSelection();
                    }
                }
                else
                {
                    ResetCardSelection();
                }
                break;
            case CardCanBePlayedOnEnum.enemyGround:
                if (clickableType != ClickableType.card)
                {
                    Cell cell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();
                    if (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil || cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass || cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.water)
                    {
                        PlayCard();
                    }
                    else
                    {
                        ResetCardSelection();
                    }
                }
                else
                {
                    ResetCardSelection();
                }
                break;
            case CardCanBePlayedOnEnum.playerOrEnemyGround:
                if (clickableType != ClickableType.card)
                {
                    Cell cell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();
                    if (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil|| cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass || cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain)
                    {
                        PlayCard();
                    }
                    else
                    {
                        ResetCardSelection();
                    }
                }
                else
                {
                    ResetCardSelection();
                }
                break;
                case CardCanBePlayedOnEnum.influencedTerritory:
                if (clickableType != ClickableType.card)
                {
                    Cell cell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();
                    if (cell.isPlayerInfluenced && cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain)
                    {
                        PlayCard();
                        break;
                    }
                    else
                    {
                        ResetCardSelection();
                        break;
                    }
       

                }
                else
                {
                    ResetCardSelection();
                }
                break;
            default:
                Debug.LogError($"Unhandled CardCanBePlayedOnEnum value: {card.cardBase.cardCanBePlayedOnEnum}");
                ResetCardSelection();
                break;
        }
    }

    /// <summary>
    /// Resets the card selection and updates the state.
    /// </summary>
    public void ResetCardSelection()
    {
        // Remove mouse display
        DisplayActions.OnRemoveMouseSprite?.Invoke();

        // Reset card state
        card.cardState = CardStateEnum.availible;
        card.isSelected = false;

        // Reset scene and UI highlights
        DisplayActions.OnHighligtSceneObject(false);
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false, naColor));

        // Reset card animation
        card.cardAnimations.ScaleResetAndRelease(card);
    }

    /// <summary>
    /// Handles the play logic for the selected card.
    /// </summary>
    private void PlayCard()
    {
        if (card.cardBase.Effect(WorldSpaceUtils.GetMouseWorldPosition(), out string result, card.cardCostModifier))
        {
            FinalizeSuccessfulPlay();
        }
        else
        {
            GlobalActions.ShowTooltip(new ToolTipArgs { time = 3f, Tooltip = result });
        }
    }

    /// <summary>
    /// Finalizes the play process for the card.
    /// </summary>
    private void FinalizeSuccessfulPlay()
    {
        // Clear cost modifier and reset card state

        card.cardState = CardStateEnum.inDiscardPile;
        card.isSelected = false;

        // Reset display actions
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false,naColor));
        DisplayActions.OnHighligtSceneObject(false);
        DisplayActions.OnRemoveMouseSprite?.Invoke();

        // Move card to the appropriate pile
        if (card.cardBase.cardPlayTypeEnum == CardPlayTypeEnum.exhuast)
        {
            CardsInPlayManager.instance.ExhausedCardInHand(card);
        }
        else
        {
            CardsInPlayManager.instance.DiscardCardInHand(card);
        }
    }
}
