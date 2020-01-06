using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
   

    public class FadeAudioSource: MonoBehaviour
    {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _duration;
        [SerializeField] private float _targetVolume;

        public IEnumerator StartFade()
        {
            float currentTime = 0;
            float start = _audioSource.volume;

            while (currentTime < _duration)
            {
                currentTime += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(start, _targetVolume, currentTime / _duration);
                yield return null;
            }
            yield break;
        }
    }

}