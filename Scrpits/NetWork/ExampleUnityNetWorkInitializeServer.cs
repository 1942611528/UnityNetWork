using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleUnityNetWorkInitializeServer : MonoBehaviour {

    void OnGUI() {
        if (GUILayout.Button("Link Server")) {
            LinkServer();
        }
    }

    void LinkServer() {
        Network.InitializeServer(8,25005,true);
    }
    void OnServerInitialize() {
        Debug.Log("LinkServer!");
    }

}
