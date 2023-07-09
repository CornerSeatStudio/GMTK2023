using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOpacity : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    public Image image;
    private float opacity;
    private bool isActive;
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && !isActive)
        {
            opacity = 1;
            text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            isActive = true;
            StartCoroutine("Fade");
        }
    }

    IEnumerator Fade()
    {
        if (opacity == 1)
        {
            yield return new WaitForSeconds(3f);
        }

        yield return new WaitForSeconds(0.05f);
        opacity = opacity - 0.05f;
        if (opacity >= 0){
            text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            StartCoroutine("Fade");
        }
        else
        {
            isActive = false;
            gameObject.SetActive(false);
        }
        
    }
}
