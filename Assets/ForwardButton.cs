using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider.gameObject.name == gameObject.name)
            {
                gameObject.GetComponentInParent<MediaControls>().ForwardButton();
            }
        }
    }
}
