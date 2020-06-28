using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float ball_Xvel = 5.0f;
        [SerializeField] private float ball_Yvel = 3.0f;

        // TODO: Prevent velocity became too low
        /// <summary> Launch ball to target's position. </summary>
        public void Launch(Vector3 targetpos){
            var heading = this.transform.position - targetpos;
            var dir = heading / heading.magnitude;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(ball_Xvel * dir.x, ball_Yvel * dir.y);
        }

    }
}

