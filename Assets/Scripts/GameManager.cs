using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pong{
    public class GameManager : MonoBehaviour
    {
        //TODO: FIX ALL... But How? :C
        [SerializeField]
        private GameObject Ball;
        public bool isReady = true;

        private bool isOver = false;

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
        public Vector3 targetPos {get; set;}
        // Start is called before the first frame update
        void Awake()
        {
            //TODO: coin toss? or etc...
            targetPos = Random.Range(0,2) == 0 ? new Vector3(-7.0f,0f,0f) : new Vector3(7.0f,0f,0f);

            leftScore = rightScore = 0;
            ScoreUpdate();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && isReady && !isOver){

                //TODO: Time Delay + AutoMatically
                var launchpos_y =  Random.Range(0,2) == 0? Random.Range(-5.0f,-3.0f) : Random.Range(3.0f, 5.0f);
                Ball.transform.position = new Vector3(0.0f, launchpos_y, 0.0f);
                Ball.SetActive(true);
                Ball.GetComponent<Ball>().Launch(targetPos);
                isReady = false;
            }
        }

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
                isOver = true;
                text.text = "Win!";
            } 
        }
    }
}
