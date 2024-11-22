using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AllIn1SpringsToolkit.Utils
{
    //This script is a simple button that uses springs to animate its scale and rotation
    //Additionally it handles the button states and animations just like the default Unity Button
    public class AllIn1SpringsToolkitSimpleButton : Selectable, IPointerClickHandler, ISubmitHandler
    {
        [SerializeField] private UnityEvent onClickEvent;
        [SerializeField] private TransformSpringComponent transformSpringComponent;
        
        [Header("Scale Spring")]
        [SerializeField] private bool doScaleSpring = true;
        [SerializeField] private Vector3 highlightedScale, pressedScaleVelocityAdd;
        private Vector3 originalScale;     
        
        [Space, Header("Rotation Spring")]
        [SerializeField] private bool doRotationSpring = true;
        [SerializeField] private Vector3 highlightedRotation, pressedRotationVelocityAdd;
        private Quaternion originalRotation;
        private Coroutine resetButtonBackToNormalCr;

        protected override void Awake()
        {
            base.Awake();

            originalScale = Vector3.one;
            originalRotation = Quaternion.identity;
        }

        private void HandleSelectionState(SelectionState state)
        {
            switch(state)
            {
                case SelectionState.Disabled:
                case SelectionState.Normal:
					if (doScaleSpring)
					{
						transformSpringComponent.SetTargetScale(originalScale);
					}
					if (doRotationSpring)
					{
						transformSpringComponent.SetTargetRotation(originalRotation);
					}
					break;

                case SelectionState.Highlighted:
                case SelectionState.Selected:
                    if (doScaleSpring)
					{
						transformSpringComponent.SetTargetScale(highlightedScale);
					}
					if (doRotationSpring)
					{
						transformSpringComponent.SetTargetRotation(Quaternion.Euler(highlightedRotation));
					}
                    break;

                case SelectionState.Pressed:
					if (doScaleSpring)
					{
						transformSpringComponent.AddVelocityScale(pressedScaleVelocityAdd);
					}
					if (doRotationSpring)
					{
                        transformSpringComponent.AddVelocityRotation(pressedRotationVelocityAdd);
					}
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            HandleSelectionState(SelectionState.Highlighted);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            HandleSelectionState(SelectionState.Normal);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClickEvent?.Invoke();
            HandleSelectionState(SelectionState.Pressed);
        }
        
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            HandleSelectionState(SelectionState.Pressed);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            HandleSelectionState(SelectionState.Normal);
        }
        
        public void SimulateClick()
        {
            onClickEvent?.Invoke();
            
            base.DoStateTransition(SelectionState.Pressed, false);
            HandleSelectionState(SelectionState.Pressed);
            HandleSelectionState(SelectionState.Selected);
            
            if(resetButtonBackToNormalCr != null) StopCoroutine(resetButtonBackToNormalCr);
            resetButtonBackToNormalCr = StartCoroutine(ResetButtonBackToNormalCr());
        }
        
        public void OnSubmit(BaseEventData eventData)
        {
            SimulateClick();
        }
        
        private IEnumerator ResetButtonBackToNormalCr()
        {
            yield return new WaitForSeconds(0.07f);
            base.DoStateTransition(SelectionState.Normal, false);
            HandleSelectionState(SelectionState.Normal);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if(resetButtonBackToNormalCr != null) StopCoroutine(resetButtonBackToNormalCr);
        }
    }
}