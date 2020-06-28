using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pong{
    public class Goal : MonoBehaviour
    {
        //TODO: FIX ALL
        [SerializeField] private GameManager gm;
        /// <summary>Which(left or right) side of the screen contains gameobject.</summary>
        [SerializeField] private string pos;

        void OnTriggerEnter2D(Collider2D collider) {
            if(collider.tag == "Ball"){
                collider.gameObject.SetActive(false);
                gm.isReady = true;
                gm.targetPos = - this.transform.position;
                if(pos == "right"){
                    gm.LeftScored();
                }
                else if(pos == "left"){
                    gm.RightScored();
                }
            }
        }
    }
}

