using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringState : AIState
{
    public WateringState(Player player) : base(player)
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
                player.Watering(target);
            }
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {

        }
    }

    public override string GetName()
    {
        return "Watering";
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
