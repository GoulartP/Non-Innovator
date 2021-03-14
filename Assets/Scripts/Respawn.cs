using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))

        {
            var player = other.gameObject.GetComponent<ThirdPersonMovement>();
            player.Death(respawnPoint);
        }
    }
}