// 0.0.1
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Input.InputActions
{
    public abstract class BaseInputAction : ScriptableObject, IInputAction<InputValue>
    {
        public EInputCondition inputCondition;
        
        public virtual bool IsActionInvoked() => default;

        public virtual InputValue GetInputValue() => new();
    }
}