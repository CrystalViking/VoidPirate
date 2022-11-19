using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    public SpriteRenderer playerRenderer;

    Color originalColor;

    Color darkGreen;

    private int secondsLeft;


    private void Start()
    {
        originalColor = playerRenderer.color;
        darkGreen = new Color(0.43f, 0.55f, 0.46f, 1f);
    }

    

   
    public IEnumerator FlashRed(int secondsLeft)
    {
        this.secondsLeft = secondsLeft;

        while(secondsLeft > 0)
        {
            if (secondsLeft > 0)
            {
                secondsLeft--;
            }
            yield return new WaitForSeconds(1f);

            playerRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            playerRenderer.color = originalColor;
        }
    }


    public IEnumerator FlashDarkGreen(int secondsLeft)
    {
        this.secondsLeft = secondsLeft;

        while (secondsLeft > 0)
        {
            if (secondsLeft > 0)
            {
                secondsLeft--;
            }
            yield return new WaitForSeconds(1f);

            playerRenderer.color = darkGreen;
            yield return new WaitForSeconds(0.1f);
            playerRenderer.color = originalColor;
        }
    }



}
