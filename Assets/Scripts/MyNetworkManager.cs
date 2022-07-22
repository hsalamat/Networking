using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        Debug.Log("Server Started!");
        MyCustomServerFunction();
    }
    public override void OnStopServer()
    {
        Debug.Log("Server Stopped!");
    }

    public override void OnStartClient()
    {
        Debug.Log("Client Started!");
        MyCustomClientFunction();
    }
    public override void OnStopClient()
    {
        Debug.Log("Client Stopped!");
    }

    public override void OnClientConnect()
    {
        Debug.Log("Connected to Server!");
    }

    public override void OnClientDisconnect()
    {
        Debug.Log("Disconnected from Server!");
    }

    [Client]
    public void MyCustomClientFunction()
    {
        Debug.Log("calling my Custom Client Function!");
    }

    [Server]
    public void MyCustomServerFunction()
    {
        Debug.Log("calling my Custom Server Function!");
    }

}
