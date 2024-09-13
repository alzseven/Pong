using System;
using UnityEngine;

namespace Core.Input.InputActions
{
    [CreateAssetMenu(fileName = "NormalizedKeyboardSingleAxisInputAction",
        menuName = "InputActions/NormalizedKeyboardSingleAxisInputAction", order = 2)]
    public class NormalizedKeyboardSingleAxisInputAction : BaseInputAction
    {
        [SerializeField] private KeyCode positiveKeyCode;
        [SerializeField] private KeyCode negativeKeyCode;

        public override bool IsActionInvoked() => true;

        public override InputValue GetInputValue() =>
            InputCondition switch
            {
                EInputCondition.Up => new InputValue
                {
                    FloatValue = UnityEngine.Input.GetKeyUp(positiveKeyCode) ? 1f :
                        UnityEngine.Input.GetKeyUp(negativeKeyCode) ? -1f : 0f
                },
                EInputCondition.Down => new InputValue
                {
                    FloatValue = UnityEngine.Input.GetKeyDown(positiveKeyCode) ? 1f :
                        UnityEngine.Input.GetKeyDown(negativeKeyCode) ? -1f : 0f
                },
                EInputCondition.Pressing => new InputValue
                {
                    FloatValue = UnityEngine.Input.GetKey(positiveKeyCode) ? 1f :
                        UnityEngine.Input.GetKey(negativeKeyCode) ? -1f : 0f
                },
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}