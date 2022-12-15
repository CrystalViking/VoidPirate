using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static Wire hoverItem;
    public GameObject linePrefab;
    public string itemName;
    private GameObject line;
    public GameObject lightOn;
     public void OnPointerDown(PointerEventData eventData)
    {
        line = Instantiate(linePrefab, transform.position, Quaternion.identity,transform.parent);
        UpdateLine(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverItem = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!this.Equals(hoverItem) && itemName.Equals(hoverItem.itemName))
        {
            UpdateLine(hoverItem.transform.position);
            lightOn.SetActive(true);
            WireLogic.instance.AddPoint();
            Destroy(hoverItem);
            Destroy(this);

        }
        else
        {
            Destroy(line);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    void UpdateLine(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        line.transform.right = direction;
        line.transform.localScale = new Vector3(direction.magnitude/GetComponentInParent<Canvas>().scaleFactor/256, 1, 1);
    }
    // Start is called before the first frame update


    // Update is called once per frame

}


