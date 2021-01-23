using System;
using UnityEngine;

namespace Bigdra.PatternLock.Scripts
{
    public class SeHandler : MonoBehaviour
    {
         [SerializeField]private AudioSource _audioSource;
         [SerializeField] private AudioClip _touchAudio;
         [SerializeField] private AudioClip _unlockAudio;

         private void Start()
         {
             _audioSource.playOnAwake = false;
         }

         public void PlayTouchAudio()
         {
             if (!_audioSource) return;
             _audioSource.clip = _touchAudio;
             _audioSource.Play();
         }

         public void PlayUnlockAudio()
         {
             if (!_audioSource) return;
             _audioSource.clip = _unlockAudio;
             _audioSource.Play();
         }
    }
}