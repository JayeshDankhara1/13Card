
using UnityEngine;

using UnityEngine.UI;



public class DargeAndDrop : MonoBehaviour
{

    #region Varibal And Refrance Script varibal
    public Canvas Canvas;
    [HideInInspector]
    public GamePlayManager Ref_GamePlayManager;
    Vector3 MousePostionOffset;
    Vector3 TransformPostion;
    Vector3 CollidGameObjectPostion;
    GameObject ThisGameObject=null;
    GameObject CollidGameObject=null;
    bool IsRaning = false;
    bool IsCollide = false;

    #endregion

    #region Unity Function
    public void Start()
    {
        Ref_GamePlayManager = GamePlayManager.instance;
    }

    #endregion

    #region Darg Function
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

    #endregion

    #region Collide Dection Reletad Function
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
    #endregion

    #region other Function
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
      
        HighLiteCard(gameObject1.transform, 1f);
        HighLiteCard(gameObject2.transform, 1f);

    }
    public Vector3 GetMousePostion()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    #endregion
}






