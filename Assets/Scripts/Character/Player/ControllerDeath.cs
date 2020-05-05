using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDeath : MonoBehaviour
{
    [Header("Player Material")]
    [SerializeField] private Material playerRenderer = null;
    [SerializeField] private Color cPlayer = Color.black;
    [SerializeField] private Color cPlayerEmission = Color.black;
    [SerializeField] private float faded = 1f;

    private ControllerMatriz cMatriz;
    [Header("Effect Death")]
    [SerializeField] private GameObject effectDeath_prefab = null;
    private GameObject effectDeath_insta = null;

    private float effectGravityStarted = 0.2f;
    private float effectSpeedStarted = 1f;

    void Start() {
        cMatriz = GetComponent<ControllerMatriz>();

        playerRenderer = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Renderer>().material;
    }

    public void Death() {
        Color color = Color.Lerp(playerRenderer.GetColor("Color_5590CD4E"), cPlayer, Time.deltaTime * faded);
        playerRenderer.SetColor("Color_5590CD4E", color);
        playerRenderer.SetColor("Color_F6DE7F4C", color);

        cMatriz.Animation.SetDeath(1);

        if (effectDeath_insta == null)
            effectDeath_insta = Instantiate(effectDeath_prefab, transform);

        Revive();
    }

    public void Revive() {
        StartCoroutine(Reborn());
    }

    IEnumerator Reborn() {
        yield return new WaitForSeconds(.1f);
        cMatriz.Health.Life = 100;
        cMatriz.cMoviment.CharController.enabled = false;

        yield return new WaitForSeconds(3);
        Destroy(effectDeath_insta);

        cMatriz.Animation.SetDeath(0);
        
        transform.position = cMatriz.spawn;

        var main = effectDeath_prefab.GetComponent<ParticleSystem>().main;
        main.gravityModifier = effectGravityStarted;
        main.startSpeed = effectSpeedStarted;

        var main2 = effectDeath_prefab.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        main2.gravityModifier = effectGravityStarted;
        main2.startSpeed = effectSpeedStarted;

        playerRenderer.SetColor("Color_5590CD4E", cMatriz.ColorMainStarted);
        playerRenderer.SetColor("Color_F6DE7F4C", cMatriz.ColorEmissionStarted);

        cMatriz.cMoviment.CharController.enabled = true;
        cMatriz.Moviment = true;
    }

    public GameObject EffectPrefab {
        get { return effectDeath_prefab; }
    }
}
