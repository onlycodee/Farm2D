using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
    public AttackState(Player player) : base(player)
    {
        filter.useTriggers = true;
        filter.SetLayerMask(LayerMask.GetMask(LayerMarkName.ANIMAL));
    }

    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            player.Attack();
        }
    }

    public override string GetName()
    {
        return "Attack";
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
