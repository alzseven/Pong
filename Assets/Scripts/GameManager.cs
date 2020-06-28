using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pong{
    public class GameManager : MonoBehaviour
    {
        // TODO: FIX ALL... But How? :C
        [SerializeField] private GameObject Ball;
        public bool isReady = true;
        private bool gameIsOver = false;
        public Vector3 targetPos {get; set;}

        [Header("Player_Left")]
        [SerializeField]
        private int leftScore;
        [SerializeField]
        private Text leftText;

        [Header("Player_Right")]
        [SerializeField]
        private int rightScore;
        [SerializeField]
        private Text rightText;


        void Awake()
        {
            // TODO: coin toss? or etc...
            targetPos = Random.Range(0,2) == 0 ? new Vector3(-7.0f,0f,0f) : new Vector3(7.0f,0f,0f);
            leftScore = rightScore = 0;
            ScoreUpdate();
        }

        // TODO: Take out
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && isReady && !gameIsOver){

                // TODO: Time Delay + AutoMatically
                var launchpos_y =  Random.Range(0,2) == 0? Random.Range(-5.0f,-3.0f) : Random.Range(3.0f, 5.0f);
                Ball.transform.position = new Vector3(0.0f, launchpos_y, 0.0f);
                Ball.SetActive(true);
                Ball.GetComponent<Ball>().Launch(targetPos);
                isReady = false;
            }
        }

        /// <summary>
        /// Update score text UI.
        /// </summary>
        void ScoreUpdate(){
            leftText.text = leftScore.ToString();
            rightText.text = rightScore.ToString();
        }

        public void LeftScored(){
            leftScore++;
            ScoreUpdate();
            isEleven(leftScore,leftText);
        }

        public void RightScored(){
            rightScore++;
            ScoreUpdate();
            isEleven(rightScore,rightText);
        }

        void isEleven(int curScore, Text text){
            if(curScore == 11){
                gameIsOver = true;
                text.text = "Win!";
            } 
        }
    }
}
