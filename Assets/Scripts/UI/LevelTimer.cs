using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelTimer : MonoBehaviour
{
    Text txtTimer;
    float time;

    WaitForSeconds oneSecond = new WaitForSeconds(1);
    private void Awake()
    {
        txtTimer = GetComponent<Text>();
    }

    public void StartCoutingDown()
    {
        StartCoroutine(COCoutingDown());
    }

    private IEnumerator COCoutingDown()
    {
        while (time > 0)
        {
            yield return oneSecond;
            time--;
            txtTimer.text = time.ToString();
        }
        LevelManager.Instance.DisplayFailDialog();
    }

    public void SetTime(float timeSeconds)
    {
        time = timeSeconds;
        txtTimer.text = time.ToString();
    }

    public void StopCO()
    {
        StopAllCoroutines();
    }
}
