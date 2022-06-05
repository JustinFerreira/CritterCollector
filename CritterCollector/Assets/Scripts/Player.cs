using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public Animator animator;

    //Alternate Sprites
    public Sprite SideView;
    public Sprite BackView;
    public Sprite FrontView;

    //Movement Tracker
    private Vector3 moveDelta;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Reset MoveDelta
        //Allows player to only move in one direction at a time (No diagonals)
        //If Movement is greater in x director only move in x direction
        if(Mathf.Abs(x) > Mathf.Abs(y))
        {
            moveDelta = new Vector3(x, 0, 0);
        }
        //If Movement is greater in y director only move in y direction
        else if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            moveDelta = new Vector3(0, y, 0);
        }
        //Sets player to Idle if no buttons are being pushed
        else if(Mathf.Abs(y) < 0.001f && Mathf.Abs(x) < 0.001f)
        {
            moveDelta = new Vector3(0, 0, 0);
        }

        //Animator Floats to zero
        animator.SetFloat("Speed", 0);


        //Swap spirte direction, wether you're going right or left
        if(moveDelta.x > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = SideView;
            animator.SetFloat("Speed", Mathf.Abs(moveDelta.x));
            animator.SetFloat("LR", 1);
            animator.SetFloat("FB", 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(moveDelta.x < 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = SideView;
            animator.SetFloat("Speed", Mathf.Abs(moveDelta.x));
            animator.SetFloat("LR", 1);
            animator.SetFloat("FB", 0);
            transform.localScale = Vector3.one;
        }
        else if(moveDelta.y > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = BackView;
            animator.SetFloat("Speed", Mathf.Abs(moveDelta.y));
            animator.SetFloat("FB", 1);
            animator.SetFloat("LR", 0);
            transform.localScale = Vector3.one;
        }
        else if(moveDelta.y < 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = FrontView;
            animator.SetFloat("Speed", Mathf.Abs(moveDelta.y));
            animator.SetFloat("FB", -1);
            animator.SetFloat("LR", 0);
            transform.localScale = Vector3.one;
        }
        else if(moveDelta.x < 0.001 && moveDelta.y < 0.001)
        {
            animator.SetFloat("Speed", 0);
        }

        //Make This thing move
        transform.Translate(moveDelta * Time.deltaTime);

    }
}
