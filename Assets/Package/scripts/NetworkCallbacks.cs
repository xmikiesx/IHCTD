using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        if (!BoltNetwork.IsClient)
        {
            SceneManager.LoadScene("vrscene");
            StartCoroutine(ActivatorVR("cardboard"));
            var spawnPosition = new Vector3(0.4f, 2, 1.0f);
            var spawnPosition2 = new Vector3(1.0f, 0.8f, 1.0f);
            BoltNetwork.Instantiate(BoltPrefabs.Coin, spawnPosition2, Quaternion.identity);
            BoltNetwork.Instantiate(BoltPrefabs.RobotVR, spawnPosition, Quaternion.identity);
            //            Modelo.transform.SetParent(modelPlayer.transform);
        }
        else
        {
            var spawnTowerPosition = new Vector3(0.56f, 0.689f, 0.393f);
            BoltNetwork.Instantiate(BoltPrefabs.PlayerHolderBolt);
            BoltNetwork.Instantiate(BoltPrefabs.TowerBolt, spawnTowerPosition, Quaternion.identity);
            BoltNetwork.Instantiate(BoltPrefabs.CanvasBolt);
        }

        //var spawnPosition = new Vector3(Random.Range(-2, 2), 2, Random.Range(-2, 2));
        //BoltNetwork.Instantiate(BoltPrefabs.Cub                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               nnuue, spawnPosition, Quaternion.identity);
    }
    public IEnumerator ActivatorVR(string YESVR)
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName(YESVR);
        yield return null;
        UnityEngine.XR.XRSettings.enabled = true;
        //        VRSettings.LoadDeviceByName(YESVR);
        //       VRSettings.enabled = true;
    }

}
