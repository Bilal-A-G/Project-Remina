using UnityEngine;

namespace Actions.Scripts
{
    [CreateAssetMenu(menuName = "Actions/PlayOneShot")]
    public class PlayOneShotAudio : ActionBase
    {
        public GenericReference<string> audioSourceKey;
        public AudioClip clipToPlay;
        public float volume;

        [System.NonSerialized] private AudioSource _source;
        [System.NonSerialized] private bool _playAudio;
        
        public override void Execute(CachedObjectWrapper cachedObjects)
        {
            _source = cachedObjects.GetGameObjectFromCache(audioSourceKey.GetValue(cachedObjects))
                .GetComponent<AudioSource>();
            _source.volume = volume;
            _source.PlayOneShot(clipToPlay);
        }

    }
}
