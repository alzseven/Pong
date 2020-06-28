using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pong{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private GameManager gm;
        [SerializeField] private string pos;

        void OnTriggerEnter2D(Collider2D collider) {
            if(collider.tag == "Ball"){
                if(pos == "right"){
                    gm.LeftScored();
                }
                else if(pos == "left"){
                    gm.RightScored();
                }
                collider.gameObject.SetActive(false);
                gm.isReady = true;
                gm.targetPos = - this.transform.position;
            }
        }
    }
}

