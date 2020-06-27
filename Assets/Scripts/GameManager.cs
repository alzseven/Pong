using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject Ball;
        public bool isReady = true;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && isReady){
                var ball = Instantiate(Ball, Vector3.zero, Quaternion.Euler(Vector3.zero) );
                ball.GetComponent<Ball>().Direction = Vector3.right;
                isReady = false;
            }
        }
    }

}
