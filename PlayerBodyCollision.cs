using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� PlayerBodyCollision
/// ��������� �� ������������/������ ����� � ��������� PlayerController
/// </summary>

public class PlayerBodyCollision : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private GameManagement gameManagement;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "WayPoint") && (controller.PlayerMovement != null))
        {
            controller.PlayerMovement.SetNextPoint(other.transform);
        }

        if ((other.tag == "BotBody") || (other.tag == "Border"))
        {
            controller.CrashState();
        }

        if (other.tag == "ScoreBooster")
        {
            gameManagement.AddScores(10);
        }
    }
}
