using System;
using System.Collections.Generic;
using Core.Input.InputActions;
using UnityEngine;

namespace Core.Input
{
    public class InputComponent : MonoBehaviour
    {
        //TODO: Use string as key might be a good option?
        //TODO: How can I use IInputAction<InputValue> interface instead of BaseInputAction class?
        //TODO: - which cannot use name as key between two mappings
        // private Dictionary<IInputAction<InputValue>, string> actionKeyMapping = new();
        
        /// <summary>
        /// 이걸 baseinputaction을 상속받아서 만들어진 구현체들에 대해 미리 다 가지고 있어서
        /// 이후에 영향이 없게 한다면?...설명 더 잘하셈
        /// </summary>
        private Dictionary<BaseInputAction, string> inputActionKeyMapping = new();
        private Dictionary<string, Action<InputValue>> keyCallbackMapping = new();
        
        // private Dictionary<BaseInputAction, Action<InputValue>> _inputActionMap = new();
        
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
            
            // Debug.Log($"BindAction - {inputActionKeyMapping.Count}, {keyCallbackMapping.Count}");
            // // InputAction과 callback은 1:1로 매칭되어야 하나요?
            // _inputActionMap.TryAdd(action, delegate { });
            // _inputActionMap[action] += callback;
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
                // Debug.Log($"TryRemoveBinding - false - {inputActionKeyMapping.Count}, {keyCallbackMapping.Count}");

                return false;
                //Something wrong?
            }
            
            // Debug.Log($"TryRemoveBinding - {inputActionKeyMapping.Count}, {keyCallbackMapping.Count}");
            return true;
            // if (_inputActionMap.TryGetValue(action, out _) == false) return false;
            // _inputActionMap[action] -= callback;
            // return true;
        }
        
        private void Update()
        {
            // foreach (var action in _inputActionMap.Keys)
            // {
            //     if (action.IsActionInvoked() &&
            //         ((GameInstance.IsGamePaused && action.CanBeInvokedDuringPause == false) == false))
            //     {
            //         _inputActionMap[action]?.Invoke(action.GetInputValue());
            //     }
            // }
            
            // foreach (var (action, callback) in _inputActionMap)
            // {
            //     if (action.IsActionInvoked() &&
            //         ((GameInstance.IsGamePaused && action.CanBeInvokedDuringPause == false) == false))
            //     {
            //         callback?.Invoke(action.GetInputValue());
            //     }
            // }

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