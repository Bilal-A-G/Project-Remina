using UnityEngine;
using UnityEngine.UI;

namespace Actions.Scripts
{
    [CreateAssetMenu(menuName = "Actions/PrintStringToOutput")]
    public class PrintStringToOutput : ActionBase
    {
        public GenericReference<string> stringToPrint;
        public GenericReference<string> outputFieldKey;
        [System.NonSerialized] private Text _outputField;

        public override void Execute(CachedObjectWrapper cachedObjects)
        {
            if(_outputField == null) _outputField = cachedObjects.GetGameObjectFromCache(outputFieldKey.GetValue(cachedObjects)).GetComponent<Text>();
        
            _outputField.text = _outputField.text + "> " + stringToPrint.GetValue(cachedObjects);
            _outputField.text += "\n";
        }
    }
}
