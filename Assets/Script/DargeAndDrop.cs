using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DargeAndDrop : MonoBehaviour
{

    Vector3 MousePostionOffset;

    public Vector3 GetMousePostion()
    { 
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDown()
    {
        MousePostionOffset= gameObject.transform.position - GetMousePostion();
    }
    public void OnMouseDrag()
    {
        transform.position = GetMousePostion() + MousePostionOffset;
    }

}






