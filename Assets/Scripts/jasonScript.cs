using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
using System.Text;
using System;
using UnityEngine.UIElements;

public class jasonScript : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    float timeBetweenJumps;
    public float minTimeBetweenJumps;
    public float maxTimeBetweenJumps;

    public float minJumpForce;
    public float maxJumpForce;

    bool canJump = true;
    bool isGrounded = true;
    bool hopping = true;

    [SerializeField]
    private string[] dislikes;

    bool stopSign = false;

    AnimationClip jumpAnim;

    public GameObject menu;
    bool menuOpen = false;

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Y-Velocity", rb.velocity.y);


        if (GetActiveWindowTitle().Contains("ATEEZ") && GetActiveWindowTitle().Contains("BOUNCY"))
        {
            transform.Rotate(0, 0, 50 * Time.deltaTime);
            Debug.Log("BOOGIE");
        }

        foreach (var dislike in dislikes)
        {
            if (GetActiveWindowTitle().Contains(dislike))
            {
                stopSign = true;

                break;
            }
            else
            {
                stopSign = false;
            }
        }

        if (stopSign == true)
        {
            hopping = false;

            animator.SetBool("StopSign", true);
        }
        else if (stopSign == false)
        {
            hopping = true;

            animator.SetBool("StopSign", false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider.gameObject.name == gameObject.name)
            {
                if (!menuOpen)
                {   
                    menu.SetActive(true);

                    menuOpen = true;
                }
                else
                {
                    menu.SetActive(false);

                    menuOpen = false;
                }

                
            }

            
        }
    }

    private void FixedUpdate()
    {
        if (canJump)
        {
            if (isGrounded && hopping)
            {
                StartCoroutine(WaitToJump());
            }
        }

        Debug.Log(GetActiveWindowTitle());
    }

    IEnumerator WaitToJump()
    {
        timeBetweenJumps = UnityEngine.Random.Range(minTimeBetweenJumps, maxTimeBetweenJumps);
        canJump = false;

        yield return new WaitForSeconds(timeBetweenJumps);

        animator.SetTrigger("Jump");        

        yield return new WaitForSeconds(0.6f);

        rb.AddForce(new Vector2(UnityEngine.Random.Range(-180, 180), UnityEngine.Random.Range(4, 10) * UnityEngine.Random.Range(minJumpForce, maxJumpForce)));

        Debug.Log("Jump");

        canJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            isGrounded = false;
        }
    }






    private string GetActiveWindowTitle()
    {
        const int nChars = 256;
        StringBuilder Buff = new StringBuilder(nChars);
        IntPtr handle = GetForegroundWindow();

        if (GetWindowText(handle, Buff, nChars) > 0)
        {
            return Buff.ToString();
        }
        return null;
    }


    

}


