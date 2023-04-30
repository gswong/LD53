using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsOpen", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert the mouse position to world point
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Cast a ray from the camera to the mouse position
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            // Check if the raycast hit this box GameObject
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                //animator.SetTrigger("Open");
                animator.SetBool("IsOpen", true);
                Debug.Log("Mouse clicked the box");
            }
        }
    }
}
