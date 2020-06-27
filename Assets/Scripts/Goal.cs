using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pong{
    public class Goal : MonoBehaviour
    {
        [SerializeField]
        private int curScore = 0;
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private GameManager gm;
        // Start is called before the first frame update
        void Awake()
        {
            ScoreUpdate();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        void ScoreUpdate(){
            scoreText.text = curScore.ToString();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if(collider.tag == "Ball"){
                curScore++;
                ScoreUpdate();
                Destroy(collider.gameObject);
                gm.isReady = true;
            }
        }
    }
}

