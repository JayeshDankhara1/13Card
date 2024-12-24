using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUiManager : MonoBehaviour
{



    public TextMeshProUGUI CounDowan_Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CounDownTaxtUpdate(string Text)
    {
        CounDowan_Text.text = Text;
    }
    public void CounDowan(bool IsActive)
    {
        CounDowan_Text.gameObject.SetActive(IsActive);
    }

}
