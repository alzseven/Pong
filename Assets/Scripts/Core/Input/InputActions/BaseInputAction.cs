using UnityEngine;

namespace Core.Input.InputActions
{
    public abstract class BaseInputAction : ScriptableObject, IInputAction<InputValue>
    {
        public EInputCondition InputCondition;
        
        public virtual bool IsActionInvoked() => default;

        public virtual InputValue GetInputValue() => new();
    }
}