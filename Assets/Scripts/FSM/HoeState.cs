using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeState : AIState
{
    public HoeState(Player player) : base(player: player)
    {
        filter.useTriggers = true;
        filter.SetLayerMask(LayerMask.GetMask(LayerMarkName.DIRT));
    }

    public override void Execute()
    {
        BoxCollider2D leftHandCollider = player.GetLeftHandCollider();
        if (Input.GetKeyDown(KeyCode.N))
        {
            result.Clear();
            leftHandCollider.OverlapCollider(filter, result);
            Dirt target = null;
            if (result.Count > 0)
            {
                target = result[0].GetComponent<Dirt>();
                player.Hoe(target);
            }
        }
    }

    public override string GetName()
    {
        return "Hoe";
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
       
    }

    
}
