using System.Collections;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    
    public Transform respawnPoint;

    

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        
        {

            Debug.Log("Morreu");

            var player = other.gameObject.GetComponent<ThirdPersonMovement>();
            player.Restart();

            Debug.Log("Morreeeeeeeeeeeeeeeeu");
        }
    }


}
