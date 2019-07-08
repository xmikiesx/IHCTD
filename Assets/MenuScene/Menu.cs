using System;
using UdpKit;
using UnityEngine;

public class Menu : Bolt.GlobalEventListener
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));

        if (GUILayout.Button("Start Server", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            // START SERVER
            BoltLauncher.StartServer();
        }

        if (GUILayout.Button("Start Client", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            // START CLIENT
            BoltLauncher.StartClient();
        }

        GUILayout.EndArea();
    }

    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }
    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = "Test Match";
            BoltNetwork.SetServerInfo(matchName, null);
            BoltNetwork.LoadScene("ARScene");
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            if(photonSession.Source == UdpSessionSource.Photon)
            {
                BoltNetwork.Connect(photonSession);
            }
        }
    }
}
