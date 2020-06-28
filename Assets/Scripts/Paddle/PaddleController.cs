using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] private float velocity = 5.0f;
        [SerializeField] private string axisName = "Vertical";
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private bool isAIInput = false;
        private IPaddelInput paddleInput;

        void Awake() => SetInput();
        // Update is called once per frame
        void Update()
        {
            paddleInput.ReadInput();
            rb.velocity = new Vector2(rb.velocity.x, paddleInput.VertDir * velocity );
        }

        public void SetInput() => paddleInput = isAIInput? new AIInput() as IPaddelInput : new PlayerInput(axisName);
    }

}

