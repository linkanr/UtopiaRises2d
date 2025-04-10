using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebuggPanelUi : MonoBehaviour

{
    
    public static DebuggPanelUi instance;
    public TMP_Dropdown instanceDropbown;
    public TMP_Dropdown cellInfoDropdown;
    public TMP_Dropdown effectType;
    public Toggle cellInfoToggle;
    public Toggle instanceToggle;
    public Toggle drawCellOutlines;
    public Toggle instanceEffects;
    public Toggle sceneObjectInfo;
    public Toggle disableEdgeScrolling;
   
    public TextMeshProUGUI SceneObjectsText;



    private void OnEnable()
    {
        BattleSceneActions.OnSpawningInterwallEnding += Showinfluence;
        BattleSceneActions.OnSpawningStarting += HideInfluence;

        instanceDropbown.onValueChanged.AddListener(delegate
        {
            SceneObjectInstanciator.instance.SetString(instanceDropbown.options[instanceDropbown.value].text);
        });
        cellInfoToggle.onValueChanged.AddListener(delegate
        {
            DebuggerGlobal.instance.ForcedUpdate();
            DebuggerGlobal.instance.drawCellInfluence = cellInfoToggle.isOn;
        });
        disableEdgeScrolling.onValueChanged.AddListener(delegate
        {
            DebuggerGlobal.instance.disableEdgeScrolling = disableEdgeScrolling.isOn;
        });
        cellInfoDropdown.onValueChanged.AddListener(delegate
        {
            DebuggerGlobal.instance.ForcedUpdate();
        });
        drawCellOutlines.onValueChanged.AddListener(delegate
        {
            DebuggerGlobal.instance.drawCellOutlines = drawCellOutlines.isOn;
        });
        instanceEffects.onValueChanged.AddListener(delegate
        {
            DebuggerGlobal.instance.effectInstancing = instanceEffects.isOn;
        });
    }

    private void HideInfluence()
    {
    
        cellInfoToggle.isOn = false;
        DebuggerGlobal.instance.ForcedUpdate();
        DebuggerGlobal.instance.drawCellInfluence = cellInfoToggle.isOn;
    }

    private void Showinfluence()
    {
        cellInfoDropdown.value = 1;
        cellInfoToggle.isOn = true;
        DebuggerGlobal.instance.ForcedUpdate();
        DebuggerGlobal.instance.drawCellInfluence = cellInfoToggle.isOn;

    }

    private void EnablePanel()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (sceneObjectInfo.isOn)
        {
            string text;
            Cell cell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();
            if (cell == null)
            {
                return;
            }
            SceneObject[] sceneObjects = cell.containingSceneObjects.ToArray();
            text = "Scene Objects in cell: \n";
            foreach (SceneObject s in sceneObjects)
            {
                text += s.GetStats().name + "\n";
            }
            SceneObjectsText.text = text;
        }

    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Double DebugPanelUi");
    }
    void Start()
    {
        foreach (var sceneObject in SceneObjectInstanciator.instance.allSceneObjects)
        {
          
            instanceDropbown.options.Add(new TMP_Dropdown.OptionData(sceneObject.sceneObjectName));
            instanceDropbown.value = 0;
            instanceDropbown.RefreshShownValue();
            SceneObjectInstanciator.instance.SetString(instanceDropbown.options[instanceDropbown.value].text);
        }
        DebuggerGlobal.instance.EnabblePanel += EnablePanel;
        gameObject.SetActive(false);
        disableEdgeScrolling.isOn = true;
        disableEdgeScrolling.isOn = false;
    }
    private void OnDestroy()
    {
        
        instanceDropbown.onValueChanged.RemoveAllListeners();
        cellInfoToggle.onValueChanged.RemoveAllListeners();
        cellInfoDropdown.onValueChanged.RemoveAllListeners();
        drawCellOutlines.onValueChanged.RemoveAllListeners();
        instanceEffects.onValueChanged.RemoveAllListeners();
        DebuggerGlobal.instance.EnabblePanel -= EnablePanel;
    }


}
