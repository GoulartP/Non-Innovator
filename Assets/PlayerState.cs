
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    bool vivo = true;
    public bool Vivo
    {
        get => vivo;
    }

    void OnCollisionEnter(Collision collisionInfo) 
    {
        if(collisionInfo.collider.tag == "Morte")
        {
            Matar();
        }   
    }

    void Matar ()
    {
        vivo = false;
    }

}
