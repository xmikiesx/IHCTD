using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
 //   public GameObject modelPlayer;
    public override void SceneLoadLocalDone(string scene)
    {
        if (!BoltNetwork.IsClient)
        {
            SceneManager.LoadScene("vrscene");
            StartCoroutine(ActivatorVR("cardboard"));
            var spawnPosition = new Vector3(0.4f, 1, 0.4f);
         //   GameObject Modelo = BoltNetwork.Instantiate(BoltPrefabs.Cube, spawnPosition, Quaternion.identity);
//            Modelo.transform.SetParent(modelPlayer.transform);
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
