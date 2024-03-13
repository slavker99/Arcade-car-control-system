using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс PlayerView
/// отвечает за внешний вид игрока (модель, анимации, звук)
/// </summary>

public class PlayerView : MonoBehaviour
{
    public PlayerTuningView TuningView;
    [Header("Звуки")]
    [SerializeField] private AudioSource soundCrash;
    [SerializeField] private AudioSource soundEngine;
    [Header("Анимации")]
    [SerializeField] private Animator animator;
    [SerializeField] private List<Transform> wheelsTransform;

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

    public void RotateWheelsAnimation()
    {
        foreach (var wh in wheelsTransform)
            wh.Rotate(new Vector3(0,0, 700 * Time.deltaTime));
            //wh.rotation = Quaternion.Euler(wh.rotation.eulerAngles.x, wh.rotation.eulerAngles.y, wh.rotation.eulerAngles.z + 700 * Time.deltaTime);
    }
}
