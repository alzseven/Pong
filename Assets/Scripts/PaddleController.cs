using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField]
        private float velocity = 5.0f;
        [SerializeField]
        private string axisName = "Vertical";
        [SerializeField]
        private Rigidbody2D rb;

        // Update is called once per frame
        void Update()
        {
            var dir = Input.GetAxisRaw(axisName);
            rb.velocity = new Vector2(rb.velocity.x, dir * velocity );
        }
    }
}

