using Content.Components;
using Core;
using Core.Input;
using Core.Input.InputActions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Content
{
    public class PongGameMode : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private BallComponent ballComponent;
        [SerializeField] private PaddleComponent leftPlayerPaddleComponent;
        [SerializeField] private PaddleComponent rightPlayerPaddleComponent;
        [Header("UI")]
        [SerializeField] private Text uiLeftPlayerScoreText;
        [SerializeField] private Text uiRightPlayerScoreText;
        [SerializeField] private Text uiGameStateMessageText;
        [SerializeField] private Text uiCommandMessageText;
        [Header("UI Info")]
        [SerializeField] private UIMessageTextInfo uiGameStateMessageSmallTextInfo;
        [SerializeField] private UIMessageTextInfo uiGameStateMessageLargeTextInfo;
        [SerializeField] private UIMessageTextInfo uiCommandMessageTextInfo;
        [SerializeField] private UIMessageTextInfo uiCommandMessageLowerTextInfo;
        [Header("Input")]
        [SerializeField] private InputComponent _inputComponent;
        [SerializeField] private KeyboardSingleKeyInputAction shootBallAction;
        [Header("Sound")]
        [SerializeField] private SoundComponent _sfxComponent;
        [SerializeField] private AudioClip scoreSound;
        [Header("GameMode Data")]
        [SerializeField] private int winScore;
        //
        private int _normalizedServeDirectionX;
        private int _leftPlayerScore;
        private int _rightPlayerScore;
        private bool _isPlaying;
        private bool _isGameStarted;
        private bool _isGameDone;
        
        private void Awake()
        {
            // Contents
            Assert.IsNotNull(ballComponent);
            Assert.IsNotNull(leftPlayerPaddleComponent);
            Assert.IsNotNull(rightPlayerPaddleComponent);
            // UI
            Assert.IsNotNull(uiLeftPlayerScoreText);
            Assert.IsNotNull(uiRightPlayerScoreText);
            Assert.IsNotNull(uiGameStateMessageText);
            Assert.IsNotNull(uiCommandMessageText);
            // UI Info
            Assert.IsFalse(Equals(uiGameStateMessageSmallTextInfo, default(UIMessageTextInfo)));
            Assert.IsFalse(Equals(uiGameStateMessageLargeTextInfo, default(UIMessageTextInfo)));
            Assert.IsFalse(Equals(uiCommandMessageTextInfo, default(UIMessageTextInfo)));
            Assert.IsFalse(Equals(uiCommandMessageLowerTextInfo, default(UIMessageTextInfo)));
            // Input
            Assert.IsNotNull(_inputComponent);
            Assert.IsNotNull(shootBallAction);
            // Sfx
            Assert.IsNotNull(_sfxComponent);
            Assert.IsNotNull(scoreSound);
            // Data
            Assert.IsFalse(winScore == 0);
            
            ClearVariables();
        }

        private void ClearVariables()
        {
            _normalizedServeDirectionX = 0;
            _leftPlayerScore = 0;
            _rightPlayerScore = 0;
            _isPlaying = false;
            _isGameStarted = false;
            _isGameDone = false;
        }

        private void Initialize()
        {
            SetGameStartMessageTexts();
            
            uiLeftPlayerScoreText.text = _leftPlayerScore.ToString();
            uiRightPlayerScoreText.text = _rightPlayerScore.ToString();
            
            _normalizedServeDirectionX = Random.Range(0, 2) == 1 ? 1 : -1;
        }
        
        private void Start() => Initialize();

        private void OnEnable() => _inputComponent.BindAction(shootBallAction, OnPressedShootBall);

        private void OnDisable() => _inputComponent.TryRemoveBinding(shootBallAction, OnPressedShootBall);

        private void OnPressedShootBall(InputValue value)
        {
            if (_isGameDone)
            {
                // Restart Game
                ClearVariables();
                Initialize();
            }
            
            if (_isGameStarted)
            {
                if (_isPlaying == false)
                {
                    ballComponent.deltaX = _normalizedServeDirectionX;
                    ballComponent.deltaY = Random.Range(-1, 1);
                    HideMessageTexts();
                    _isPlaying = true;
                }
            }
            else
            {
                SetServeMessageTexts();
                _isGameStarted = true;
            }
        }
        
        private void Update()
        {
            if (_isPlaying == false) return;
            
            if (ballComponent.transform.position.x <= leftPlayerPaddleComponent.transform.position.x -
                leftPlayerPaddleComponent.transform.localScale.x / 2)
                OnBallReachedSide(true);
            else if (ballComponent.transform.position.x >= rightPlayerPaddleComponent.transform.position.x +
                     leftPlayerPaddleComponent.transform.localScale.x / 2)
                OnBallReachedSide(false);              
        }
        
        private void OnBallReachedSide(bool isLeftSide)
        {
            _isPlaying = false;
            
            // Reset Ball
            ballComponent.transform.position = Vector3.zero;
            ballComponent.deltaX = 0f;
            ballComponent.deltaY = 0f;

            // Update Score
            if (isLeftSide)
            {
                _rightPlayerScore++;
                _normalizedServeDirectionX = 1;
                uiRightPlayerScoreText.text = _rightPlayerScore.ToString();
            }
            else
            {
                _leftPlayerScore++;
                _normalizedServeDirectionX = -1;
                uiLeftPlayerScoreText.text = _leftPlayerScore.ToString();
            }
            _sfxComponent.PlayOneShot(scoreSound);

            //
            if (IsGameEnded())
            {
                SetResultMessageTexts();
                _isGameDone = true;
            }
            else SetServeMessageTexts();
        }

        private bool IsGameEnded() => _rightPlayerScore == winScore || _leftPlayerScore == winScore;
        
        private void SetMessageText(Text target, UIMessageTextInfo info, string message)
        {
            target.gameObject.SetActive(message is not null);
            if (message is not null)
            {
                var rectTransform = target.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = info.AnchoredPosition;
                target.fontSize = info.FontSize;
                target.text = message;
            }
        }
        
        private void SetGameStartMessageTexts()
        {
            SetMessageText(uiGameStateMessageText, uiGameStateMessageSmallTextInfo, "Welcome to Pong!");
            SetMessageText(uiCommandMessageText, uiCommandMessageTextInfo, "Press Enter to begin!");
        }
        
        private void SetResultMessageTexts()
        {
            SetMessageText(uiGameStateMessageText, uiGameStateMessageLargeTextInfo,
                $"{(_rightPlayerScore == winScore ? "Right" : "Left")} Player wins!");
            SetMessageText(uiCommandMessageText, uiCommandMessageLowerTextInfo, "Press Enter to restart!");
            
        }

        private void SetServeMessageTexts()
        {
            SetMessageText(uiGameStateMessageText, uiGameStateMessageSmallTextInfo,
                $"{(_normalizedServeDirectionX == -1 ? "Left" : "Right")} Player's serve!");
            SetMessageText(uiCommandMessageText, uiCommandMessageTextInfo, "Press Enter to serve!");
        }

        private void HideMessageTexts()
        {
            SetMessageText(uiGameStateMessageText, default, null);
            SetMessageText(uiCommandMessageText, default, null);
        }
    }
}