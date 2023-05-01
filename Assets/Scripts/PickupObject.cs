using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool isInRange = false;
    private bool isCarrying = false;
    private GameObject player;
    private Rigidbody2D playerRigidbody;
    public float throwForceMultiplier = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isCarrying && Input.GetKeyDown(KeyCode.E))
        {
            Release();
        }
        else if (!isCarrying && isInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void PickUp()
    {
        Debug.Log("Pickup");
        isCarrying = true;
        transform.SetParent(player.transform);
        Vector3 carryingPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
        transform.position = carryingPosition;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }

    private void Release()
    {
        Debug.Log("Release");
        isCarrying = false;
        transform.SetParent(null);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;

        // Use the player's momentum as the throw force
        Rigidbody2D objectRigidbody = GetComponent<Rigidbody2D>();
        Vector2 throwForce = new Vector2(player.transform.localScale.x * 1f, 0f) + playerRigidbody.velocity * throwForceMultiplier;
        objectRigidbody.velocity = throwForce;
    }
}