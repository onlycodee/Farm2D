using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIState {

    void Update();
    void OnEnter();
    void OnExit();
}
