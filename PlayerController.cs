using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// Класс PlayerController
/// Основной класс игрока, из него происходит контроль всех действий и состояний
/// Управление игроком осуществляется с помощью изменения трёх основных классов:
/// - PlayerView - отвечает за внешний вид игрока (модель, анимации, звук)
/// - PlayerModel - содержит основные параметры игрока
/// - PlayerMovement - отвечает за способ перемещения, перестроения, разгон/торможение и т.д.
/// Также содержит вспомогательный класс ChangingLaneQueue
/// Он помогает обрабатывать несколько последовательных перестроение подряд
/// </summary>

public class PlayerController : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PlayerModel PlayerModel;
    public PlayerView PlayerView;
    [SerializeField] private Transform startPoint;
    [SerializeField] private ChangingLaneQueue changingLaneQueue;

    public bool isControlled { get; protected set; }
    public bool isMoving { get; protected set; }
    public bool isCrash { get; protected set; }
    public bool isGameOver { get; protected set; }
    public Direction isChangingLane = Direction.None;

    private void Awake()
    {
        changingLaneQueue.Initialization(PlayerMovement, this);
    }

    public void ToStartState()
    {
        isMoving = true;
        PlayerModel.SetStartValues();
        PlayerMovement.ToStartState(startPoint);
    }

    public void StartControlling()
    {
        isControlled = true;
    }

    public void Crash()
    {
        PlayerModel.TakeAwayLive();
        PlayerMovement.CrashState();
        changingLaneQueue.ClearQueue();
        if (PlayerModel.currentLives == 0)
        {
            //GameOver();
            //return;
        }
        StartCoroutine(CrashStateCoruntine());
        PlayerView.CrashState(isChangingLane);
    }

    public IEnumerator CrashStateCoruntine()
    {
        isControlled = false;
        isCrash = true;
        yield return new WaitForSeconds(2);
        isControlled = true;
        isCrash = false;
        yield return null;
    }

    public void GameOver()
    {
        PlayerView.GameOverState();
        PlayerMovement.GameOverState();
        isControlled = false;
    }

    public void SetSpeed(float speed)
    {
        PlayerMovement.SetSpeed(speed);
    }

    private void Update()
    {
        if (isControlled)
            InputControlling();
        PlayerModel.SetСurrentSpeed(PlayerMovement.GetCurrentSpeed());
    }

    private void InputControlling()
    {
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
            changingLaneQueue.AddAction(Direction.Left);
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
            changingLaneQueue.AddAction(Direction.Right);
    }
}
