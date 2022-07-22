using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnMyCountChanged))]
    int myCount = 0;
   void HandleMovement()
    {
        if(isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
            transform.position += movement*0.01f;
        }
    }

    private void Update()
    {        
       HandleMovement();

        if(isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Sending Hi to Server!");
            SayHiToServer();
        }

        if(isServer && transform.position.y> 10)
        {
            TooHigh();
        }
    }

    public override void OnStartServer()
    {
        Debug.Log("Player has been spawned on the server");
    }

    [Command]
    //Command attribute to call a function from a client to run on the server machine
    void SayHiToServer()
    {
        Debug.Log("Received Hi from Client!");
        myCount++;
        
        //Say hi back to the associated client who called the server!
        SayHiBackToClient();
    }

    [TargetRpc]
    //TargetRpc attribute to call a function from a server to run on the specific client machine
    void SayHiBackToClient()
    {
        Debug.Log("Received Hi from Server!");
    }

    //ClientRpc attribute to call a function from a server to run on the all client machines
    [ClientRpc]
    void TooHigh()
    {
        Debug.Log("Too high!");
        Debug.Log("heigth = " + transform.position.y);
    }

    void OnMyCountChanged(int oldCount, int newCount)
    {
        Debug.Log($"We had {oldCount} counts, but now we have {newCount} counts!");
    }
}
