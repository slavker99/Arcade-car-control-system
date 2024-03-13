using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс DynamicPointMovement
/// Описывает движение игрока по радиальной траектории с перестроениями
/// </summary>

public class DynamicPointMovement : PlayerMovement
{
    [SerializeField] private Transform lanesTransform;
    [SerializeField] private List<Transform> lanes;
    private Transform currentLaneTransform;
    [SerializeField] private CameraController cameraController;

    private void FixedUpdate()
    {
        if (currentSpeed < controller.PlayerModel.GetMaxSpeed())
            currentSpeed = currentSpeed + acceleration;
        if (controller.isMoving)
        {
            MoveForward();
        }
        if (cameraController) 
            cameraController.CustomUpdate();
    }

    private void MoveForward()
    {
        lanesTransform.Rotate(new Vector3(0, -currentSpeed * speedCoef, 0));
        if (Math.Abs(playerTransform.localPosition.x - currentLaneTransform.localPosition.x) < 0.1f)
            controller.isChangingLane = Direction.None;
        else if (controller.isChangingLane != Direction.None)
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, currentLaneTransform.position, changeLaneSpeed);
    }

    private void ChangeLane(int lane)
    {
        currentLaneTransform = lanes[lane].transform;
        lastLane = currentLane;
        currentLane = lane;
        cameraController.ChangePos(lane);
    }

    public override void MoveToPoint(Transform point)
    {
        playerTransform.position = point.transform.position;
        playerTransform.rotation = point.transform.rotation;
    }

    public override void ToStartState(Transform point)
    {
        currentSpeed = controller.PlayerModel.GetStartSpeed();
        MoveToPoint(point);
    }

    public override void CrashState()
    {
        //currentSpeed = model.crashSpeed;
        if (controller.isChangingLane != Direction.None)
        {
            ChangeLane(lastLane);
        }
    }

    public override void GameOverState()
    {
        currentSpeed = 0;
    }

    public override void MoveLeft()
    {
        var nextLane = currentLane - 1;
        if (0 <= nextLane && nextLane < lanes.Count)
        {
            ChangeLane(nextLane);
            controller.isChangingLane = Direction.Left;
            controller.PlayerView.ChangingLaneState(Direction.Left);
        }
    }

    public override void MoveRight()
    {
        var nextLane = currentLane + 1;
        if (0 <= nextLane && nextLane < lanes.Count)
        {
            ChangeLane(nextLane);
            controller.isChangingLane = Direction.Right;
            controller.PlayerView.ChangingLaneState(Direction.Right);
        }
    }

    public override void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
