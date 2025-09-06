using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class BaseState
{
    public abstract void EnterState(Enemy enemy);
    public abstract void UpdateState(Enemy enemy);
    public abstract void ExitState(Enemy enemy);
}

