using Core;
using Core.Input;
using Core.Input.InputActions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace Content.Components
{
    public class PaddleComponent : MonoBehaviour
    {
        [Header("Paddle Data")]
        [SerializeField] private float paddleYMax;
        [SerializeField] private float paddleYMin;
        [SerializeField] private float movementSpeed;
        [FormerlySerializedAs("upDownKeyDownAction")]
        [Header("Input")]
        [SerializeField] private NormalizedKeyboardSingleAxisInputAction upDownKeyPressedAction;
        [SerializeField] private InputComponent _inputComponent;
        private float _deltaY;
        
        private void Awake()
        {
            // Data
            Assert.IsFalse(paddleYMax < paddleYMin);
            Assert.IsFalse(movementSpeed <= 0f);
            // Input
            Assert.IsNotNull(upDownKeyPressedAction);
            Assert.IsNotNull(_inputComponent);
            //
            Assert.IsTrue(gameObject.CompareTag("Player"));
        }

        private void OnEnable() => _inputComponent.BindAction(upDownKeyPressedAction, AddUpDownMovement);

        private void OnDisable() => _inputComponent.TryRemoveBinding(upDownKeyPressedAction, AddUpDownMovement);
        
        private void AddUpDownMovement(InputValue value) => _deltaY = value.FloatValue;

        private void Update()
        {
            var currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x,
                Mathf.Max(paddleYMin,
                    Mathf.Min(paddleYMax, currentPosition.y + _deltaY * movementSpeed * Time.deltaTime)),
                currentPosition.z);
        }
    }
}