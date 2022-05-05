using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject winDialog;
    [SerializeField] GameObject failDialog;
    [SerializeField] List<LevelAsset> levels;
    [SerializeField] LevelTextDisplay levelText;
    private LevelManager _instance;
    int currentLevel = 0;
    public static LevelManager Instance = null;
    const string SaveKey = "LevelIndex";
    private void Awake()
    {
        if (FindObjectsOfType<LevelManager>().Length > 1)
        {
            Destroy(this);
        }
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Mission mission = FindObjectOfType<Mission>();
        currentLevel = LoadCurrentLevelIndex();
        mission.SetLevel(levels[currentLevel]);
        mission.ResetData();
        levelText.SetLevel(currentLevel + 1);
        Money.Instance.currentMoney = levels[currentLevel].startingMoney;
        Money.Instance.UpdateTxtMoney();
    }

    public LevelAsset GetLevelAssets(int levelNum)
    {
        if (levelNum >= 0 && levelNum < levels.Count)
        {
            return levels[levelNum];
        }
        print("Level num is out of range: " + levelNum);
        return null;
    }

    public void DisplayFailDialog()
    {
        Time.timeScale = 0;
        failDialog.SetActive(true);
    }

    public void DisplayWinDialog()
    {
        Time.timeScale = 0;
        winDialog.SetActive(true);
    }

    public void LoadNextLevel()
    {
        print("Load next level");
        currentLevel++;
        Mission mission = FindObjectOfType<Mission>();
        mission.SetLevel(levels[currentLevel]);
        mission.ResetData();
        ResetLevelData();
        levelText.SetLevel(currentLevel + 1);
        winDialog.SetActive(false);
    }

    public void ResetLevelData()
    {
        foreach (var entity in FindObjectsOfType<ResetableEntity>())
        {
            foreach (var resetable in entity.GetComponents<IReset>())
            {
                resetable.Reset();
            }
        }
    }

    public void Replay()
    {
        Mission misson = FindObjectOfType<Mission>();
        misson.ResetData();
        ResetLevelData();
    }

    public int LoadCurrentLevelIndex()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            return PlayerPrefs.GetInt(SaveKey);
        }
        return 0;
    }

    public void SaveCurrentLevelIndex()
    {
        PlayerPrefs.SetInt(SaveKey, currentLevel);
        PlayerPrefs.Save();
    }
}
