using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using YG;

/// <summary>
/// Класс PlayerModel
/// Cодержит основные параметры игрока
/// От сюда берет данные игровой HUD
/// </summary>

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private bool debugMode;

    [Header("Жизни")]
    [SerializeField] private int maxLives = 7;
    public int currentLives { get; private set; }
    public UnityEvent<int> ChangeLivesEvent = new UnityEvent<int>();

    [Header("Скорость")]
    private float currentSpeed = 0;
    [SerializeField] private float crashSpeed = 20;
    [SerializeField] private float defaultSpeed = 30;
    [SerializeField] private float startSpeed = 40;
    [SerializeField] private float maxSpeed = 100;

    [Header("Очки")]
    private int currentScores = 0;

    [Header("Данные для сохранения")]
    private static List<string> purchasedMods = new List<string> ();
    private static List<string> installedMods = new List<string> ();
    private static int allScores = 0;

    public float GetCurrentSpeed() => currentSpeed;
    public float GetCrashSpeed() => crashSpeed;
    public float GetDefaultSpeed() => defaultSpeed;
    public float GetStartSpeed() => startSpeed;
    public float GetMaxSpeed() => maxSpeed;
    public float GetAllScores() => allScores;
    public int GetCurrentScores() => currentScores;

    private void Awake()
    {
        LoadDataFromCloud();
    }

    public List<string> GetMainMods()
    {
        var mods = new List<string> ();
        foreach (var mod in installedMods)
            mods.Add(mod.Clone().ToString());
        return mods;
    }

    public List<string> GetPurchaseMods()
    {
        var mods = new List<string>();
        foreach (var mod in purchasedMods)
            mods.Add(mod.Clone().ToString());
        return mods;
    }

    public void SetStartValues()
    {
        SetLives(maxLives);
        currentScores = 0;
    }

    public void SetСurrentSpeed(float val)
    {
        if (val >= 0)
            currentSpeed = val;
    }

    public void SetLives(int val)
    {
        if (val >= 0)
        {
            ChangeLivesEvent.Invoke(val);
            currentLives = val;
            if (debugMode) Debug.Log("Жизни игроки изменены на: " + val);
        }
    }

    public void TakeAwayLive()
    {
        if (currentLives > 0)
            SetLives(currentLives - 1);
    }


    public void SaveValues()
    {
        allScores += currentScores;
    }

    public void SaveMods(List<string> main, List<string> purchased)
    {
        installedMods = new List<string>();
        purchasedMods = new List<string>();
        foreach (var modName in main)
            installedMods.Add(modName.Clone().ToString()); // чтобы значения не передавались по ссылке
        foreach (var modName in purchased)
            purchasedMods.Add(modName.Clone().ToString());
    }

    public void SaveDataInCloud()
    {
        if (YandexGame.SDKEnabled)
        {
            YandexGame.savesData.scores = allScores;
            YandexGame.savesData.purchaseMods = purchasedMods;
            YandexGame.savesData.mainMods = installedMods;
            YandexGame.SaveProgress();
            Debug.Log("Данные сохранены в облако");
        }
    }
    public void LoadDataFromCloud()
    {
        if (YandexGame.SDKEnabled)
        {
            YandexGame.LoadProgress();
            allScores = YandexGame.savesData.scores;
            purchasedMods = YandexGame.savesData.purchaseMods;
            installedMods = YandexGame.savesData.mainMods; 
        }

        if (purchasedMods.Count == 0)
            purchasedMods = new List<string> {"bm_stock", "hl_stock", "re_stock", "sp_stock", "cl_stock", "tn_stock"};
        if (installedMods.Count == 0)
            installedMods = new List<string> { "bm_stock", "hl_stock", "re_stock", "sp_stock", "cl_stock", "tn_stock" };

        if (debugMode)
        {
            WriteLineLists();
            Debug.Log("Данные с облака загружены");
        }
    }

    private void WriteLineLists()
    {
        var str = "Установленные моды: ";
        foreach (var mod in installedMods)
            str = str + "; " + mod;
        Debug.Log(str);
        str = "Купленные моды: ";
        foreach (var mod in purchasedMods)
            str = str + "; " + mod;
        Debug.Log(str);
    }

    public void ChangeCurScores(int value)
    {
        currentScores += value;
    }
}
