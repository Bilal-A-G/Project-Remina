using System.Collections;
using UnityEngine;

namespace Actions.Scripts
{
    [CreateAssetMenu(menuName = "Actions/PlayAmbiance")]
    public class PlayAmbiance : ActionBase
    {
        public AudioClip clipToPlay;
        public float fadeSpeed;
        public float targetVolume;
        public GenericReference<string> ambianceManagerKey;

        [System.NonSerialized] private AudioSource _audioManager;
        [System.NonSerialized] private bool _fadeOut;

        public override void Execute(CachedObjectWrapper cachedObjects)
        {
            if (_audioManager == null)
                _audioManager = cachedObjects.GetGameObjectFromCache(ambianceManagerKey.GetValue(cachedObjects))
                    .GetComponent<AudioSource>();
            _fadeOut = true; 
        }

        public override void UpdateLoop(CachedObjectWrapper cachedObjects)
        {
            if(_audioManager == null) return;
            
            if (!_fadeOut)
            {
                if (_audioManager.volume < targetVolume)
                {
                    _audioManager.volume += Time.deltaTime * fadeSpeed;
                }
                else
                {
                    _audioManager = null;
                }
            }
            else
            {
                _audioManager.volume -= Time.deltaTime * fadeSpeed;
                if (_audioManager.volume > 0) return;
                
                _fadeOut = false;
                _audioManager.clip = clipToPlay;
                _audioManager.Play();
            }
        }
    }
}
