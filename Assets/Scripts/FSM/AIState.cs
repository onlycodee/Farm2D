using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState 
{
    protected List<Collider2D> result = new List<Collider2D>();
    protected ContactFilter2D filter = new ContactFilter2D();
    protected Player player;
    public AIState(Player player)
    {
        this.player = player;
    }

    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void Execute();

    public abstract string GetName();
}
