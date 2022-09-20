using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Action Blocks")]
public class ActionBlocks : ActionBase
{
    public List<ActionBase> actions;
    
    public override void Execute(CachedObjectWrapper cachedObjects)
    {
        foreach (ActionBase t in actions)
        {
            t.Execute(cachedObjects);
        }
    }
}
