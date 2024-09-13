using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundComponent : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {   
            if(TryGetComponent(out _audioSource) == false) Debug.LogWarning($"No AudioSource on {name}");
        }
        
        public void Play(AudioClip clip, float pitch = 1.0f, bool loop = false)
        {
            Assert.IsNotNull(_audioSource);
            Assert.IsNotNull(clip);
                
            _audioSource.pitch = pitch;
            _audioSource.clip = clip;
            _audioSource.loop = loop;
            _audioSource.Play();
        }
        
        public void PlayOneShot(AudioClip clip, float pitch = 1.0f)
        {
            Assert.IsNotNull(_audioSource);
            Assert.IsNotNull(clip);
                
            _audioSource.pitch = pitch;
            _audioSource.clip = clip;
            _audioSource.PlayOneShot(clip);
        }
    }
}