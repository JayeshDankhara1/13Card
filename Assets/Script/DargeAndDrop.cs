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
    Vector3 TransformPostion;

    public GamePlayManager Ref_GamePlayManager;


    public void Start()
    {
        Ref_GamePlayManager = GamePlayManager.instance;
    }

    public Vector3 GetMousePostion()
    { 
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDown()
    {
        TransformPostion = transform.position;
        MousePostionOffset= gameObject.transform.position - GetMousePostion();
    }

    public void OnMouseDrag()
    {
        
        transform.position = GetMousePostion() + MousePostionOffset;
    }
    

    
    
    public void OnMouseUp()
    {
        if (Ref_GamePlayManager.Collide_GameObject != null && Ref_GamePlayManager.Collide_GameObject != transform)
        {
            SwapCard(transform.gameObject.GetComponent<Card>(), Ref_GamePlayManager.Collide_GameObject.GetComponent<Card>());
            LoadSprit(transform.gameObject);
            LoadSprit(Ref_GamePlayManager.Collide_GameObject);
            Ref_GamePlayManager.Collide_GameObject = null;
            transform.position = TransformPostion;
        }
        else
        transform.position = TransformPostion;


    }
  

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        Ref_GamePlayManager.Collide_GameObject = collision.gameObject;
    }

    public void SwapCard(Card card1, Card card2)
    {
        Color tempColor = card1.Color;
        Name tempName = card1.Name;

        card1.Color = card2.Color;
        card1.Name = card2.Name;

        card2.Color = tempColor;
        card2.Name = tempName;

    }

    public void LoadSprit(GameObject gameObject)
    {
        gameObject.GetComponent<Image>().sprite = Ref_GamePlayManager.Ref_GamePlayUiManager.Card_Sprite[Ref_GamePlayManager.Ref_GamePlayUiManager.GetCardIndex(gameObject.GetComponent<Card>())];

    }




}






