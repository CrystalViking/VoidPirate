using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum alphaValue
{
    SHRINKING, GROWING
}
public class WarningText : MonoBehaviour
{
    public alphaValue currentAlphaValue;
    public float CommentMinAlpha;
    public float CommentMaxAlpha;
    public float CommentCurrentAlpha;
    public TMP_Text warningText;


    // Start is called before the first frame update
    void Start()
    {
        CommentMinAlpha = 0.2f;
        CommentMaxAlpha = 1f;
        CommentCurrentAlpha = 1f;
        currentAlphaValue = alphaValue.SHRINKING;
    }

    // Update is called once per frame
    void Update()
    {
        AlphaComments();
    }
    public void AlphaComments()
    {
        if (currentAlphaValue == alphaValue.SHRINKING)
        {
            CommentCurrentAlpha = CommentCurrentAlpha - 0.01f;
            warningText.color = new Color((float)0.9607843, (float)0.2313726, (float)0.3411765, CommentCurrentAlpha); 
            if(CommentCurrentAlpha <= CommentMinAlpha)
            {
                currentAlphaValue = alphaValue.GROWING;
            }
        }
        else if (currentAlphaValue == alphaValue.GROWING)
        {
            CommentCurrentAlpha = CommentCurrentAlpha + 0.01f;
            warningText.color = new Color((float)0.9607843, (float)0.2313726, (float)0.3411765, CommentCurrentAlpha);
            if (CommentCurrentAlpha >= CommentMaxAlpha)
            {
                currentAlphaValue = alphaValue.SHRINKING;
            }
        }
    }
}
