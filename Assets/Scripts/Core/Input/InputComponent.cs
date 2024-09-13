using System;
using System.Collections.Generic;
using Core.Input.InputActions;
using UnityEngine;

namespace Core.Input
{
    public class InputComponent : MonoBehaviour
    {
        private Dictionary<BaseInputAction, string> inputActionKeyMapping = new();
        private Dictionary<string, Action<InputValue>> keyCallbackMapping = new();
        
        public void BindAction(BaseInputAction action, Action<InputValue> callback)
        {
            var actionName = action.name;
            if (inputActionKeyMapping.TryAdd(action, actionName))
            {
                if (keyCallbackMapping.TryAdd(actionName, callback))
                {
                    // keyCallbackMapping[actionName] += callback;
                }
                else
                {
                    //Something wrong?
                }
            }
            else
            {
                // there's already same action
                if (keyCallbackMapping.TryAdd(actionName, callback))
                {
                    // keyCallbackMapping[actionName] += ;
                }
                else
                {
                    //Something wrong?
                }
            }
        }
        
        public bool TryRemoveBinding(BaseInputAction action, Action<InputValue> callback)
        {
            var actionName = action.name;
            if (keyCallbackMapping.TryGetValue(actionName, out _))
            {
                keyCallbackMapping.Remove(actionName);
            }
            else
            {
                return false;
                //Something wrong?
            }
            return true;
        }
        
        private void Update()
        {
            foreach (var (action, key) in inputActionKeyMapping)
            {
                if (action.IsActionInvoked())
                {
                    if(keyCallbackMapping.TryGetValue(key, out var callback))
                        callback?.Invoke(action.GetInputValue());
                }
            }
        }
    }
}