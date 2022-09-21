using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions.Scripts
{
    [CreateAssetMenu(menuName = "Actions/Action Blocks")]
    public class ActionBlocks : ActionBase
    {
        public List<ActionBase> actions;
        public List<float> actionDelays;
        public GenericReference<bool> printingAction;

        public override void Execute(CachedObjectWrapper cachedObjects) => throw new System.NotImplementedException();

        public IEnumerator ExecuteRoutine(CachedObjectWrapper cachedObjects)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                while (printingAction.GetValue(cachedObjects))
                {
                    yield return null;
                }
                yield return new WaitForSeconds(actionDelays[i]);
                actions[i].Execute(cachedObjects);
                if (actions[i] is not PlayOneShotAudio && actions[i] is not PlayAmbiance)
                {
                    printingAction.SetValue(true, cachedObjects);
                }
            }
        }

        public override void UpdateLoop(CachedObjectWrapper cachedObjects)
        {
            foreach (ActionBase t in actions)
            {
                t.UpdateLoop(cachedObjects);
            }
        }

        public override void FixedUpdateLoop(CachedObjectWrapper cachedObjects)
        {
            foreach (ActionBase t in actions)
            {
                t.FixedUpdateLoop(cachedObjects);
            }
        }
    }
}
