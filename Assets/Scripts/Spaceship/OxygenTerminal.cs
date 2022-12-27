using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTerminal : MonoBehaviour
{
    public List<GameObject> objectsToGreyOut;
    private Dictionary<GameObject, Color> originalColors;
    bool isInRange;
    bool isActive = true;
    bool isInGoodCondition;

    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;
    void Start()
    {
        StoreOriginalColors();
        UngreyOutObjects();

        int number = Random.Range(0, 2);
        isInGoodCondition = number == 0;
    }

    void Update()
    {
        if (Timer.Instance.isOxygenShortage && Timer.Instance.timerIsRunning)
        {
            InteractOnAction();
            if (isActive)
            {
                GreyOutObjects();
            }
            else
            {
                UngreyOutObjects();
            }
        }
        else if (Timer.Instance.isEnergyShortage && Timer.Instance.timerIsRunning)
        {
            UngreyOutObjects();
        }

    }

    public void InteractOnAction()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(itemInteractionCode) && isActive)
            {
                isActive = false;
                SetInfoAboutFinishedEvent();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void SetInfoAboutFinishedEvent()
    {
        Timer.Instance.timerIsRunning = false;
        Timer.Instance.DisplayEventMessage("Event succeeded");
    }

    public void GreyOutObjects()
    {
        // Iterate through the list of objects
        foreach (GameObject obj in objectsToGreyOut)
        {
            // Get the object's renderer component
            Renderer renderer = obj.GetComponent<Renderer>();

            // Check if the object has a renderer component
            if (renderer != null)
            {
                // Get the material of the renderer
                Material material = renderer.material;

                // Get the current color of the material
                Color color = material.color;

                // Set the color to a grey color
                color.r = 0.5f;
                color.g = 0.5f;
                color.b = 0.5f;

                // Apply the updated color to the material
                material.color = color;
            }
        }
    }

    public void UngreyOutObjects()
    {
        // Iterate through the list of objects
        foreach (GameObject obj in objectsToGreyOut)
        {
            // Get the object's renderer component
            Renderer renderer = obj.GetComponent<Renderer>();

            // Check if the object has a renderer component
            if (renderer != null)
            {
                // Get the material of the renderer
                Material material = renderer.material;

                // Set the color of the material back to the original color
                material.color = originalColors[obj];
            }
        }
    }

    private void StoreOriginalColors()
    {
        // Initialize the dictionary to store the original colors of the objects
        originalColors = new Dictionary<GameObject, Color>();

        // Iterate through the list of objects
        foreach (GameObject obj in objectsToGreyOut)
        {
            // Get the object's renderer component
            Renderer renderer = obj.GetComponent<Renderer>();

            // Check if the object has a renderer component
            if (renderer != null)
            {
                // Save the original color of the material in the dictionary
                originalColors[obj] = renderer.material.color;
            }
        }
    }
}
