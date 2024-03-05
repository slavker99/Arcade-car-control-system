using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/// <summary>
/// ����� PlayerModel
/// C������� �������� ��������� ������
/// �� ���� ����� ������ ������� HUD
/// </summary>

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private GameSceneUI gameSceneUI;

    [Header("�����")]
    [SerializeField] private int maxLives = 7;
    public int currentLives { get; private set; }

    [Header("��������")]
    [SerializeField] private float currentSpeed = 0;
    [SerializeField] private float crashSpeed = 20;
    [SerializeField] private float defaultSpeed = 30;
    [SerializeField] private float startSpeed = 40;
    [SerializeField] private float maxSpeed = 100;

    [Header("����")]
    [SerializeField] private float allScores = 0;
    [SerializeField] private float currentScores = 0;

    //[Header("���������� � �������")]
    //public static List<int> purchaseMods = new List<int> { 100, 200, 300, 400, 500, 600 };
    //public static List<int> mainMods = new List<int> { 100, 200, 300, 400, 500, 600 };

    public float GetCurrentSpeed() => currentSpeed;
    public float GetCrashSpeed() => crashSpeed;
    public float GetDefaultSpeed() => defaultSpeed;
    public float GetStartSpeed() => startSpeed;
    public float GetMaxSpeed() => maxSpeed;
    public float GetAllScores() => allScores;
    public float GetCurrentScores() => currentScores;

    public void SetStartValues()
    {
        currentLives = maxLives;
        currentScores = 0;
    }

    public void Set�urrentSpeed(float val)
    {
        if (val >= 0)
            currentSpeed = val;
    }

    public void TakeAwayLive()
    {
        if (currentLives > 0)
        {
            currentLives--;
            gameSceneUI.SetLives(currentLives);
        }
    }

    public void SaveValues()
    {
        allScores += currentScores;
    }

    public void AddScores(int value)
    {
        currentScores += value;
    }
}
