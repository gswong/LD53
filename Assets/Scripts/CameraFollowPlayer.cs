using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Vector3 CameraPositionToPlayer = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = CameraPositionToPlayer;
        }
    }
}
