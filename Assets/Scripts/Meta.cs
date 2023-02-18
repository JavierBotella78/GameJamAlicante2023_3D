using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    public Action OnPlayerTouch;
    public Action OnMetaPlaySound;

    //private Animator anim;

    //public static readonly int MetaAnim = Animator.StringToHash("metaAnim");
    private void Awake()
    {
        //TryGetComponent(out anim);

    }
    private void OnCollisionEnter(Collision collision)
    {
        //anim.Play(MetaAnim);
        //OnMetaPlaySound?.Invoke();
        Invoke(nameof(nextLevel), 1.25f);
    }

    private void nextLevel()
    {
        OnPlayerTouch?.Invoke();
    }
}
