using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventTerminalInput : MonoBehaviour
{
    public TMP_Text successText;
    public TMP_Text errorText;
    public TMP_InputField inputField;
    public GameObject anyObject;

    // Start is called before the first frame update
    void Start()
    {
        TextConvert();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(inputField.GetComponent<TMP_InputField>().text == "INITIALIZE SYSTEM RESTART")
            {
                OnSuccess();
            }
            else
            {

                OnFail();
            }
        }
    }
   
    public void TextConvert()
    {
        inputField.onValidateInput +=
         delegate (string s, int i, char c) { return char.ToUpper(c); };
    }
    public void OnSuccess()
    {
        errorText.gameObject.SetActive(false);
        successText.gameObject.SetActive(true);
        //anyObject.SetActive(true);
        EventManager.instance.EventComplete();
    }
    public void OnFail()
    {
        successText.gameObject.SetActive(false);
        errorText.gameObject.SetActive(true);
    }
}
