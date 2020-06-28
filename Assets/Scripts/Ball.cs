using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong{
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private float xVelocity = 5.0f;
        [SerializeField]
        private float yVelocity = 3.0f;

        //TODO: Prevent velocity became too low
        public void Launch(Vector3 targetpos){
            var heading = this.transform.position - targetpos;
            var dir = heading / heading.magnitude;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(xVelocity * dir.x, yVelocity * dir.y);
        }

    }
}

