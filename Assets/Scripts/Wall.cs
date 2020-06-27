using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong{
    public class Wall : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider) {
            if(collider.tag == "Ball"){
                collider.gameObject.GetComponent<Ball>().Direction = new Vector3(
                    collider.gameObject.GetComponent<Ball>().Direction.x,
                    -collider.gameObject.GetComponent<Ball>().Direction.y,
                    0
                );
                Debug.Log(collider.gameObject.GetComponent<Ball>().Direction);
            }
        }
    }
}

