
using System.Collections.Generic;
using UnityEngine;


public abstract class SoSeekSystemBase : ScriptableObject
{ 
    public abstract SceneObject Seek(Vector3 position, List<SceneObjectTypeEnum> sceneObjectTypeEnums, TargeterBaseClass attackerComponent, IMoverComponent moverComponent = null);

}