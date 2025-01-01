
using UnityEngine;

public class DargeAndDrop : MonoBehaviour
{


    Vector3 MousePostionOffset;
    Vector3 TransformPostion;
    Vector3 CollidGameObjectPostion;

    public GamePlayManager Ref_GamePlayManager;

    public GameObject ThisGameObject=null;
    public GameObject CollidGameObject=null;

    public bool IsRaning = false;
    public bool IsCollide = false;

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
        IsRaning = true;
        ThisGameObject = transform.gameObject;
        TransformPostion = transform.position;
        HighLiteCard(ThisGameObject.transform, 1.2f);
        MousePostionOffset = gameObject.transform.position - GetMousePostion();
        transform.gameObject.layer = LayerMask.NameToLayer("DrageCard");
    }

    public void OnMouseDrag()
    {

        transform.position = GetMousePostion() + MousePostionOffset;

    }

    public void OnMouseUp()
    {
        if (ThisGameObject !=null && CollidGameObject!=null)
        {
            IsCollide = false;
            SwapCard_GameObject(ThisGameObject, CollidGameObject,TransformPostion, CollidGameObjectPostion);
            Ref_GamePlayManager.Ref_GamePlayUiManager.AllCardListUpdate();
            CollidGameObject = null;
            ThisGameObject = null;
        }
        IsRaning = false;
        transform.position = TransformPostion;
        transform.gameObject.layer = LayerMask.NameToLayer("UI");
      
        HighLiteCard(transform, 1f);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (IsRaning && !IsCollide)
        {
            IsCollide = true;
            CollidGameObject = collision.gameObject;
            CollidGameObjectPostion = collision.transform.position;
            HighLiteCard(collision.gameObject.transform, 1.2f);
        }
       
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (IsRaning)
        {
            IsCollide = false;
            CollidGameObject = null;
            HighLiteCard(collision.gameObject.transform, 1f);
        }
      
    }

    public void HighLiteCard(Transform transform, float Scale)
    {
        transform.localScale = Vector3.one * Scale;
    }

    public void SwapCard_GameObject(GameObject gameObject1, GameObject gameObject2, Vector3 gameObject1_Postion, Vector3 gameObject2_Postion)
    {

        Transform parent1 = gameObject1.transform.parent;
        Transform parent2 = gameObject2.transform.parent;

        int siblingIndex1 = gameObject1.transform.GetSiblingIndex();
        int siblingIndex2 = gameObject2.transform.GetSiblingIndex();

        gameObject1.transform.SetParent(parent2);
        gameObject2.transform.SetParent(parent1);

        gameObject1.transform.SetSiblingIndex(siblingIndex2);
        gameObject2.transform.SetSiblingIndex(siblingIndex1);

        gameObject1.transform.localPosition = gameObject2_Postion;
        gameObject2.transform.localPosition = gameObject2_Postion;

        HighLiteCard(gameObject1.transform,1f);
        HighLiteCard(gameObject2.transform, 1f);
        
    }

}






