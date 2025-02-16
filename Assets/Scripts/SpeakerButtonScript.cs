using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpeakerButtonScript : MonoBehaviour
{
    public GameObject mediaControls;

    private bool speakerOpen;

    private void Start()
    {
        mediaControls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider.gameObject.name == gameObject.name)
            {
                if (!speakerOpen)
                {
                    mediaControls.SetActive(true);

                    speakerOpen = true;
                }
                else
                {
                    mediaControls.SetActive(false);

                    speakerOpen = false;
                }
            }
        }
    }
}
