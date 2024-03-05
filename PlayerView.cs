using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����� PlayerView
/// �������� �� ������� ��� ������ (������, ��������, ����)
/// </summary>

public class PlayerView : MonoBehaviour
{
    [Header("�����")]
    [SerializeField] private AudioSource soundCrash;
    [SerializeField] private AudioSource soundEngine;
    [SerializeField] private Animator animator;

    public void CrashState(Direction dir)
    {
        soundCrash.Play();
        if (dir == Direction.Left)
            animator.Play("LeftCrash", 0);
        else if (dir == Direction.Right)
            animator.Play("RightCrash", 0);
    }

    public void ChangingLaneState(Direction dir)
    {
        if (dir == Direction.Left)
            animator.Play("ChangeLaneLeft", 0);
        else if (dir == Direction.Right)
            animator.Play("ChangeLaneRight", 0);
    }

    public void GameOverState()
    {

    }

    public void OnMainHeadLights()
    {

    }

    public void OnStopLights()
    {

    }

    public void OnGabaritLights()
    {

    }
}
