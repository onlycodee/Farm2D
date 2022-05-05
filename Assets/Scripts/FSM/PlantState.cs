using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantState : AIState
{
    
    public PlantState(Player player) : base(player)
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
            Dirt target = null;
            leftHandCollider.OverlapCollider(filter, result);
            if (result.Count > 0)
            {
                foreach (var obj in result)
                {
                    target = obj.GetComponent<Dirt>();
                    if (target.IsPlantable())
                    {
                        player.PlantSeed(target);
                        break;
                    }
                }
                
            }
        }

    }

    public override string GetName()
    {
        return "Plant";
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
