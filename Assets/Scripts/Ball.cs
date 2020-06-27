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
        private Vector3 _Direction;
        public Vector3 Direction{ get; set;}

        // Start is called before the first frame update
        void Awake()
        {
            Direction = Vector3.zero;
            Launch();
        }

        // Update is called once per frame
        void Update()
        {
            // this.transform.position += new Vector3( 
            //     Direction.x * xVelocity,
            //     Direction.y * yVelocity,
            //     Direction.z
            // ) * Time.deltaTime;
        }

        private void Launch(){
            float x = Random.Range(0,2) == 0 ? -1: 1;
            float y = Random.Range(0,2) == 0 ? -1: 1;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(xVelocity * x, yVelocity * y);
        }

    }
}

