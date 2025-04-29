using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Animation : MonoBehaviour
{
    #region Varibal
    public static Animation instance;
    #endregion

    #region Unity Function
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Animtion Function
    public void ScoreUpdate(int targetScore, int currentScore, TextMeshProUGUI text, string ExtraText = "")
    {
        DOTween.To(() => currentScore, x =>
        {
            currentScore = x;
            text.text = (currentScore.ToString() + ExtraText);
        }, targetScore, 1f)
        .SetEase(Ease.InOutQuad);
    }

    public void Movecard(RectTransform transform, Vector3 targetPosition, float duration, UnityAction onCompleteAction = null, UnityAction OnStart = null)
    {
        transform.DOAnchorPos(targetPosition, duration)
                        .From(new Vector3(-775, -1070, 0))
                       .SetEase(Ease.Flash) 
                       .OnStart(() =>
                       {
                           OnStart?.Invoke();
                       })

                       .OnComplete(() =>
                       {
                        
                           onCompleteAction?.Invoke();
                       });
    }
    #endregion
}
