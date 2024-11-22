using System;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
    public class SetStateTransformSpringComponent : MonoBehaviour
    {
        [Header("When to set the state")]
        [SerializeField] private TransformSpringComponent transformSpringComponent;
        [SerializeField] private bool onStart, onEnable, onUpdate;

        [Space, Header("Position")]
        [SerializeField] private bool setPositionEnabled;
        [SerializeField] private bool setPositionTarget, setPositionAtEquilibrium;
        [SerializeField] private Vector3 newPositionTarget;
        [SerializeField] private bool setPositionValue;
        [SerializeField] private Vector3 newPositionValue;
    
        [Space, Header("Rotation")]
        [SerializeField] private bool setRotationEnabled;
        [SerializeField] private bool setRotationTarget, setRotationAtEquilibrium;
        [SerializeField] private Quaternion newRotationTarget;
        [SerializeField] private bool setRotationValue;
        [SerializeField] private Quaternion newRotationValue;
    
        [Space, Header("Scale")]
        [SerializeField] private bool setScaleEnabled;
        [SerializeField] private bool setScaleTarget, setScaleAtEquilibrium;
        [SerializeField] private Vector3 newScaleTarget;
        [SerializeField] private bool setScaleValue;
        [SerializeField] private Vector3 newScaleValue;
        
        private void SetState()
        {
            if (setPositionEnabled)
            {
                if (setPositionTarget)
                {
                    transformSpringComponent.SetTargetPosition(newPositionTarget);
                }
                
                if (setPositionAtEquilibrium)
                {
                    transformSpringComponent.ReachEquilibriumPosition();
                }
                
                if (setPositionValue)
                {
                    transformSpringComponent.SetCurrentValuePosition(newPositionValue);
                }
            }
            
            if (setRotationEnabled)
            {
                if (setRotationTarget)
                {
                    transformSpringComponent.SetTargetRotation(newRotationTarget);
                }
                
                if (setRotationAtEquilibrium)
                {
                    transformSpringComponent.ReachEquilibriumRotation();
                }
                
                if (setRotationValue)
                {
					transformSpringComponent.SetCurrentValueRotation(newRotationValue);
				}
			}
            
            if (setScaleEnabled)
            {
                if (setScaleTarget)
                {
                    transformSpringComponent.SetTargetScale(newScaleTarget);
                }
                
                if (setScaleAtEquilibrium)
                {
                    transformSpringComponent.ReachEquilibriumScale();
                }
                
                if (setScaleValue)
                {
                    transformSpringComponent.SetCurrentValueScale(newScaleValue);
                }
            }
        }
        
        private void Start()
        {
            if (onStart)
            {
                SetState();
            }
        }
        
        private void OnEnable()
        {
            if (onEnable)
            {
                SetState();
            }
        }

        private void Update()
        {
            if (onUpdate)
            {
                SetState();
            }
        }

        private void Reset()
        {
            if (transformSpringComponent == null)
            {
                transformSpringComponent = GetComponent<TransformSpringComponent>();
            }
        }
    }
}