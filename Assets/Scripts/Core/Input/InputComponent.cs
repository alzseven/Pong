using System;
using System.Collections.Generic;
using Core.Input.InputActions;
using UnityEngine;

namespace Core.Input
{
    public class InputComponent : MonoBehaviour
    {
        private readonly Dictionary<IInputAction<InputValue>, Action<InputValue>> _inputActionMapping = new();
        
        public void BindAction(IInputAction<InputValue> action, Action<InputValue> callback)
        {
            if (_inputActionMapping.TryAdd(action, callback) == false) _inputActionMapping[action] = callback;
        }
        
        public bool TryRemoveBinding(IInputAction<InputValue> action, Action<InputValue> callback)
        {
            if (!_inputActionMapping.TryGetValue(action, out _)) return false;
            _inputActionMapping[action] -= callback;
            return true;
        }
        
        private void Update()
        {
            foreach (var (action, callback) in _inputActionMapping)
                if (action.IsActionInvoked())
                    callback?.Invoke(action.GetInputValue());
        }
    }
}