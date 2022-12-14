using UnityEngine;
using UnityEngine.UI;

namespace Actions.Scripts
{
    [CreateAssetMenu(menuName = "Actions/PrintRegularText")]
    public class PrintStringToOutput : ActionBase
    {
        public GenericReference<string> stringToPrint;
        [System.NonSerialized] private IOHandler _ioHandler;

        public override void Execute(CachedObjectWrapper cachedObjects)
        {
            if(_ioHandler == null) _ioHandler = cachedObjects.GetGameObjectFromCache("Parent").GetComponent<IOHandler>();
            _ioHandler.PrintTextToOutput("< " + stringToPrint.GetValue(cachedObjects), "white");
        }
    }
}
