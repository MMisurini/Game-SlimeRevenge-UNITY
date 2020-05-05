using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnemiesDrop : MonoBehaviour
{
    [SerializeField] private GameObject prefab_powerUp = null;

    public GameObject PowerUpPrefab {
        get { return prefab_powerUp; }
    }
}
