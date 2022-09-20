using UnityEngine;

namespace Actions.Scripts
{
    [CreateAssetMenu(menuName = "Actions/PrintDecisionText")]
    public class PrintDecisionTextToOutput : ActionBase
    {
        public GenericReference<string> stringToPrint;
        [System.NonSerialized] private IOHandler _ioHandler;

        public override void Execute(CachedObjectWrapper cachedObjects)
        {
            if(_ioHandler == null) _ioHandler = cachedObjects.GetGameObjectFromCache("Parent").GetComponent<IOHandler>();
            _ioHandler.PrintTextToOutput("\n  > " + stringToPrint.GetValue(cachedObjects) + "\n", "red");
        }
    }
}
