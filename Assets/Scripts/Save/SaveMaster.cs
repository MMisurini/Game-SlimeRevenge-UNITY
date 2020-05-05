using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMaster : MonoBehaviour
{
    public static SaveMaster Instance { get; set; }
    public SaveState state;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
    }

    IEnumerator Checkpoint() {
        yield return new WaitForSeconds(1.5f);
        ChooseLoadScene.Instance.LoadSceneIndex(state.sceneSave, new Vector3(state.x,state.y,state.z));
    }

    public void Save() {
        state.x = -0.25f;
        state.y = 0.004f;
        state.z = 0.14f;

        state.sceneSave = 1;

        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    public void Load() {
        if (PlayerPrefs.HasKey("save")) {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        } else {
            state = new SaveState();
            Save();
        }

        //StartCoroutine(Checkpoint());
    }

    #region Block or Desblock

    public bool IsItensOwned(int index) {

        return (state.itensOwned & (1 << index)) != 0;
    }

    public void UnlockItens(int index) {
        state.itensOwned |= 1 << index;
    }

    #endregion

    #region Position

    public void Position(float x,float y,float z) {
        state.x = x;
        state.y = y;
        state.z = z;

        Save();
    }

    #endregion

    public void ResetSave() {
        PlayerPrefs.DeleteKey("save");
    }

}
