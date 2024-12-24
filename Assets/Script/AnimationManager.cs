using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationManager : MonoBehaviour
{
    //public static AnimationManager Instance;



    //private void Awake()
    //{
    //    if(Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(this);
    //    }else
    //    {
    //        Destroy(this);
    //    }
    //}





    public static void Scale(Transform transform,float DurtionTime,Vector3 StartPostion, Vector3 EndPostion,float DelayTime=0 , UnityAction EndAction= null,UnityAction StartAction = null, Ease ease=Ease.Linear)
    {
        


        transform.DOScale(EndPostion, DurtionTime)
            .OnStart(()=>
            {
                StartAction?.Invoke();
            })
           .From(StartPostion)
           .SetDelay(DelayTime)
           .SetEase(ease)
           .OnComplete(()=>
            {
                EndAction?.Invoke();
                //Debug.Log("Complete");   
            }
           );

    }

    public void Move(Transform transform, float DurtionTime, Vector3 StartPostion, Vector3 EndPostion, UnityAction EndAction = null, UnityAction StartAction = null)
    {



        transform.DOScale(EndPostion, DurtionTime)
            .OnStart(() =>
            {
                StartAction?.Invoke();
            })
           .From(StartPostion)
           .SetDelay(2f)
           .SetEase(Ease.OutBack)
           .OnComplete(() =>
           {
               EndAction?.Invoke();
               Debug.Log("Complete");
           }
           );

    }

    public static void PunchScale(Transform transform,  Vector3 Punch, float DurtionTime = 0, float DelayTime = 0, UnityAction EndAction = null, UnityAction StartAction = null, Ease ease = Ease.Linear)
    {



        transform.DOPunchScale(Punch, DurtionTime)
            .OnStart(() =>
            {
                StartAction?.Invoke();
            })
           .SetDelay(DelayTime)
           .SetEase(ease)
           .OnComplete(() =>
           {
               EndAction?.Invoke();
               //Debug.Log("Complete");   
           }
           );

    }
}
