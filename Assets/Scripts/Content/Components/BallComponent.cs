using Core;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Content.Components
{
    public class BallComponent : MonoBehaviour
    {
        [HideInInspector] public float deltaX;
        [HideInInspector] public float deltaY;
        [Header("Ball Data")]
        [SerializeField] private float ballYMax;
        [SerializeField] private float ballYMin;
        [SerializeField] private float movementSpeed;
        //0.3240741 ~ 0.462963
        [Header("Sound")]
        [SerializeField] private SoundComponent sfxComponent;
        [SerializeField] private AudioClip wallHitSound;
        [SerializeField] private AudioClip paddleHitSound;
        
        private void Awake()
        {
            // Data
            Assert.IsFalse(ballYMax < ballYMin);
            Assert.IsFalse(movementSpeed <= 0f);
            // Sound
            Assert.IsNotNull(sfxComponent);
            Assert.IsNotNull(wallHitSound);
            Assert.IsNotNull(paddleHitSound);
            // Trigger Available
            Assert.IsTrue(TryGetComponent(out Collider2D component) && component.isTrigger);
        }

        private void Update()
        {
            transform.position += new Vector3(deltaX, deltaY, 0) * (Time.deltaTime * movementSpeed);
            
            if (transform.position.y >= ballYMax || transform.position.y <= ballYMin) {
                deltaY *= -1;
                sfxComponent.PlayOneShot(wallHitSound);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                deltaX *= -1;
                //TODO:
                deltaY = deltaY < 0f ? Random.Range(-1f, 0f) : Random.Range(0f, 1f);
                sfxComponent.PlayOneShot(paddleHitSound);
            }
        }
    }
}