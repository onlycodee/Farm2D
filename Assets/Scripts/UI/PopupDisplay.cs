using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDisplay : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject recipe;

    public void TurnOnTutorial()
    {
        Time.timeScale = 0;
        tutorial.SetActive(true);
    }

    public void TurnOffTutorial()
    {
        Time.timeScale = 1;
        tutorial.SetActive(false);
    }

    public void TurnOnRecipe()
    {
        Time.timeScale = 0;
        recipe.SetActive(true);
    }

    public void TurnOffRecipe()
    {
        Time.timeScale = 1;
        recipe.SetActive(false);
    }
}
