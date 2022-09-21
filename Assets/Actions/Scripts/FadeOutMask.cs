using UnityEngine;
using UnityEngine.UI;

namespace Actions.Scripts
{
    [CreateAssetMenu(menuName = "Actions/FadeOutMask")]
    public class FadeOutMask : ActionBase
    {
        public GenericReference<string> maskKey;
        public GenericReference<float> fadeSpeed;
        public GenericReference<bool> printingAction;

        [System.NonSerialized] private Image _mask;
        [System.NonSerialized] private bool _fadeOut;
    
        public override void Execute(CachedObjectWrapper cachedObjects)
        {
            if (_mask == null) _mask = cachedObjects.GetGameObjectFromCache(maskKey.GetValue(cachedObjects)).GetComponent<Image>();
            _fadeOut = true;
        }

        public override void UpdateLoop(CachedObjectWrapper cachedObjects)
        {
            if(!_fadeOut) return;
            
            _mask.color = new Color(_mask.color.r, _mask.color.g, _mask.color.b, _mask.color.a - fadeSpeed.GetValue(cachedObjects));
            if (_mask.color.a <= 0)
            {
                _fadeOut = false;
                printingAction.SetValue(false, cachedObjects);
            }
        }
    }
}
