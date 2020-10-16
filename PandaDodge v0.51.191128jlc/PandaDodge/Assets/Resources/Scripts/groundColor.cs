using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer m_SpriteRenderer;

        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();


        Analytics analytics = new Analytics();
        int lvl = analytics.ReturnLastLevel();
        switch (lvl)
        {
            case 1:
                m_SpriteRenderer.color = new Color(0f/255f, 132f / 255f, 70f/255f);
                break;
            case 2:
                m_SpriteRenderer.color = new Color(157f / 255f, 142f / 255f, 0f / 255f);
                break;
            case 3:
                m_SpriteRenderer.color = new Color(247f / 203f, 213f / 255f, 40f / 255f);
                break;
            case 4:
                m_SpriteRenderer.color = new Color(227f / 255f, 227f / 255f, 227f / 255f);
                break;
            case 5:
                //246,233,219
                m_SpriteRenderer.color = new Color(246f / 255f, 233f / 255f, 219f / 255f);
                break;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}