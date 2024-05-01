using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetAnimator();
    }

    private void Start()
    {
        gameObject.GetComponent<StateController>().StateChanged.AddListener(OnStateChanged);
    }

    private void OnStateChanged(IState newState)
    {
    }

    private Animator GetAnimator()
    {
        Animator animator = gameObject.GetComponentInChildren<Animator>();
        return animator != null ? animator : null;
    }
}