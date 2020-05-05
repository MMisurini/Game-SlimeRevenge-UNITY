using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLoadScene : MonoBehaviour
{
    [SerializeField] private GameObject player_Prefab;

    private GameObject player_insta;
    private Vector3 spawn_insta;

    public static ChooseLoadScene Instance { get; set; }
    private void Awake() {
        Instance = this;
    } 

    public void LoadSceneIndex(int sceneIndex, Vector3 spawn) {
        InstantiatePlayer(spawn);

        StartCoroutine(LoadYourAsyncSceneIndex(sceneIndex, player_insta));
    }

    void InstantiatePlayer(Vector3 spawn) {
        player_insta = Instantiate(player_Prefab, spawn, Quaternion.Euler(0, 180, 0));
        player_insta.name = "Larry";
    }

    public Scene GetSceneAtual() {
        return SceneManager.GetActiveScene();
    }
    public Scene GetSceneIndex(int index) {
        return SceneManager.GetSceneByBuildIndex(index);
    }

    IEnumerator LoadYourAsyncSceneIndex(int value, GameObject player) {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(value);
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByBuildIndex(value));
        //SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), s);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

}
