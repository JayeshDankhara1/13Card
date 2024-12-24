using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    public GamePlayUiManager Ref_GamePlayUiManager;
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GameStart()
    {
        Ref_GamePlayUiManager.CounDowan(true);
        for (int i = 3; i >= 0; i--)
        {
            if (i == 0)
            {
                Ref_GamePlayUiManager.CounDownTaxtUpdate("Go!!!");
            }
            else
            {
                Ref_GamePlayUiManager.CounDownTaxtUpdate(i.ToString());
            }
            yield return new WaitForSeconds(1);
        }
        Ref_GamePlayUiManager.CounDowan(false);
    }

   

}
