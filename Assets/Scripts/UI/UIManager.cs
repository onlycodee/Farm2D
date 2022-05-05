using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void BtnNextLevel()
    {
        Time.timeScale = 1;
        LevelManager.Instance.LoadNextLevel();
    }
    
    public void BtnReset()
    {
        Time.timeScale = 1;
        LevelManager.Instance.Replay();
    }
}
