using UnityEngine;
using UnityEngine.UI;

namespace Actions.Scripts
{
    [CreateAssetMenu(menuName = "Actions/FadeOutMask")]
    public class FadeOutMask : ActionBase
    {
        public GenericReference<string> maskKey;
        public GenericReference<float> fadeSpeed;
        
        [System.NonSerialized] private Image _mask;
        private bool _fadeOut;
    
        public override void Execute(CachedObjectWrapper cachedObjects)
        {
            if (_mask == null) _mask = cachedObjects.GetGameObjectFromCache(maskKey.GetValue(cachedObjects)).GetComponent<Image>();
            _fadeOut = true;
        }

        public override void UpdateLoop(CachedObjectWrapper cachedObjects)
        {
            if(!_fadeOut) return;

            Debug.Log(_mask.color.a);
            _mask.color = new Color(_mask.color.r, _mask.color.g, _mask.color.b, _mask.color.a - fadeSpeed.GetValue(cachedObjects));
            if (_mask.color.a <= 0) _fadeOut = false;
        }
    }
}
