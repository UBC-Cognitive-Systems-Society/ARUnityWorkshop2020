using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{

    Animator robbieAnim;

    private int danceID = Animator.StringToHash("Dance");
    private int suprisedID = Animator.StringToHash("Suprised");

    public void InitializeUI(Animator robbieAnim)
    {
        this.robbieAnim = robbieAnim;


        gameObject.SetActive(true);
    }

    public void ClearUI()
    {
        this.robbieAnim = null;
        gameObject.SetActive(false);
    }

    public void Dance()
    {
        if(robbieAnim) robbieAnim.SetTrigger(danceID);
    }

    public void Suprise()
    {
        if(robbieAnim) robbieAnim.SetTrigger(suprisedID);
    }
}
