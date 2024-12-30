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
        HighLiteCard(transform, 1.2f);
        MousePostionOffset = gameObject.transform.position - GetMousePostion();


    }

    public void OnMouseDrag()
    {
        
        transform.position = GetMousePostion() + MousePostionOffset;

    }
    

    
    
    public void OnMouseUp()
    {
        if (Ref_GamePlayManager.Collide_GameObject != null && Ref_GamePlayManager.Collide_GameObject != transform)
        {
            SwapCard_GameObject(transform.gameObject, Ref_GamePlayManager.Collide_GameObject, TransformPostion, Ref_GamePlayManager.Collide_GameObject_Postion);
           // SwapCard(transform.gameObject.GetComponent<Card>(), Ref_GamePlayManager.Collide_GameObject.GetComponent<Card>());
           // LoadSprit(transform.gameObject);
           // LoadSprit(Ref_GamePlayManager.Collide_GameObject);
            Ref_GamePlayManager.Collide_GameObject = null;
           // transform.position = TransformPostion;
        }
        else
        {
            transform.position = TransformPostion;
        }
        HighLiteCard(transform, 1f);
    }
  

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        Ref_GamePlayManager.Collide_GameObject = collision.gameObject;
        Ref_GamePlayManager.Collide_GameObject_Postion = collision.transform.position;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        HighLiteCard(collision.transform, 1.2f);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        HighLiteCard(collision.transform, 1f);
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

    public void HighLiteCard(Transform transform,float Scale)
    {
        transform.localScale= Vector3.one * Scale;
    }

    public void SwapCard_GameObject(GameObject gameObject1, GameObject gameObject2, Vector3 gameObject1_Postion, Vector3 gameObject2_Postion)
    {

        Transform parent = gameObject1.transform.parent;
        int siblingIndex1 = gameObject1.transform.GetSiblingIndex();
        int siblingIndex2 = gameObject2.transform.GetSiblingIndex();

        
        gameObject1.transform.SetSiblingIndex(-1);
        gameObject2.transform.SetSiblingIndex(-1);

    
        gameObject1.transform.SetSiblingIndex(siblingIndex2);
        gameObject2.transform.SetSiblingIndex(siblingIndex1);

       
        LayoutRebuilder.ForceRebuildLayoutImmediate(parent.GetComponent<RectTransform>());

    }

    

}






