using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIOptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //public Image prompt;
    //public Image none;

    public AudioClip hover;
    public AudioClip click;
    
    
    Image image;
    AudioSource audioSource;

    private void OnDisable()
    {
        image.gameObject.SetActive(false);
    }

    void Start()
    {
        image = GetComponentInChildren<Image>();
        audioSource = GetComponent<AudioSource>();
        image.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.gameObject.SetActive(true);
        audioSource.clip = hover;
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.gameObject.SetActive(false);
        audioSource.Stop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.clip = click;
        audioSource.Play();
    }

    
}
