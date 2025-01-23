
using UnityEngine;

using UnityEngine.UI;



public class DargeAndDrop : MonoBehaviour
{


    Vector3 MousePostionOffset;
    Vector3 TransformPostion;
    Vector3 CollidGameObjectPostion;

    public Canvas Canvas;
    
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
        TransformPostion = transform.localPosition;
        HighLiteCard(ThisGameObject.transform, 1.2f);
        MousePostionOffset = gameObject.transform.position - GetMousePostion();
        Canvas.sortingOrder = 10;
    }

    public void OnMouseDrag()
    {

        transform.position = GetMousePostion() + MousePostionOffset;

    }




    public void OnMouseUp()
    {
        if (ThisGameObject != null && CollidGameObject != null)
        {
            SwapCard_GameObject(ThisGameObject, CollidGameObject);
            Ref_GamePlayManager.Ref_GamePlayUiManager.DeHighliteAllCard();

            IsCollide = false;
            ThisGameObject = null;
            CollidGameObject = null;
            Ref_GamePlayManager.Ref_GamePlayUiManager.AllCardListUpdate();


        }
       

            transform.localPosition = TransformPostion;
            IsCollide = false;
            ThisGameObject = null;
            CollidGameObject = null;
        
        IsRaning = false;
        Canvas.sortingOrder = 1;
        HighLiteCard(transform.transform, 1f);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (IsRaning && !IsCollide)
        {

            IsCollide = true;
            CollidGameObject = collision.gameObject;
            CollidGameObjectPostion = collision.transform.localPosition;
            HighLiteCard(collision.gameObject.transform, 1.2f);
        }
       
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (IsRaning || IsCollide)
        {
            IsCollide = false;
            CollidGameObject = null;
            HighLiteCard(collision.gameObject.transform, 1f);
        }
      
    }




    public void LoadSprit(GameObject gameObject)
    {
        gameObject.GetComponent<Image>().sprite = Ref_GamePlayManager.Ref_GamePlayUiManager.Card_Sprite[Ref_GamePlayManager.Ref_GamePlayUiManager.GetCardIndex(gameObject.GetComponent<Card>())];

    }

    public void HighLiteCard(Transform transform, float Scale)
    {
        transform.localScale = Vector3.one * Scale;
    }

    public void SwapCard_GameObject(GameObject gameObject1, GameObject gameObject2)
    {
       Ref_GamePlayManager.SwapCard(gameObject1.GetComponent<Card>(), gameObject2.GetComponent<Card>());
        LoadSprit(gameObject1);
        LoadSprit(gameObject2);
        //  Ref_GamePlayManager.Ref_GamePlayUiManager.AllCardListUpdate();
        //   Transform parent1 = gameObject1.transform.parent;
        //   Transform parent2 = gameObject2.transform.parent;
        //   int siblingIndex1 = gameObject1.transform.GetSiblingIndex();
        //   int siblingIndex2 = gameObject2.transform.GetSiblingIndex();


        //   //   gameObject1.transform.SetSiblingIndex(-1);
        //   //  gameObject2.transform.SetSiblingIndex(-1);




        ////   gameObject1.transform.localPosition = gameObject1_Postion;
        //  // gameObject2.transform.localPosition = gameObject2_Postion;

        //   gameObject1.transform.SetParent(parent2);
        //   gameObject2.transform.SetParent(parent1);
        //   gameObject1.transform.SetSiblingIndex(siblingIndex2);
        //   gameObject2.transform.SetSiblingIndex(siblingIndex1);

        HighLiteCard(gameObject1.transform, 1f);
        HighLiteCard(gameObject2.transform, 1f);

        //   LayoutRebuilder.ForceRebuildLayoutImmediate(parent.GetComponent<RectTransform>());

    }


}






