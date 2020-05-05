using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPressed : MonoBehaviour
{
    private GameObject pagsGameobject = null;
    private Animation pagsAnim = null;

    private Animation animCanvas = null;

    [SerializeField] private Sprite spriteMenu = null;
    [SerializeField] private Sprite spriteMenuDrain = null;

    private void OnEnable() {
        //pagsAnim = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).GetComponent<Animation>();
        //pagsGameobject = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject;
        
        animCanvas = Master.main.Canvas.GetComponent<Animation>();
    }

    public void Menu(string value) {
        pagsGameobject.SetActive(true);

        pagsAnim.Play("DrainIn");

        ActiveTransform(true);

        StartCoroutine(PagsIn(value));
    }

    public void Voltar() {
        pagsAnim.GetComponent<Image>().sprite = spriteMenuDrain;

        ActiveTransform(false);

        pagsAnim.Play("DrainOut");
    }

    public void Jogar() {
        Menu mn = GetComponent<Menu>();
        mn.MenuToGame(true);

        StartCoroutine(FadeInGame(animCanvas.GetClip("Menu").length));
    }

    IEnumerator FadeInGame(float value) {
        yield return new WaitForSeconds(value);
        animCanvas.Play("GameIn");
    }

    public void Sair() {
        Application.Quit();
    }

    void ActiveTransform(bool value) {
        for (int i = 0; i < pagsGameobject.transform.childCount; i++) {
            pagsGameobject.transform.GetChild(i).gameObject.SetActive(value);
            
            if(value == false)
                pagsGameobject.transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 0;
        }

        if (value == false)
            StartCoroutine(PagsOut());
    }

    IEnumerator PagsIn(string value) {
        yield return new WaitForSeconds(1.2f);
        pagsAnim.GetComponent<Image>().sprite = spriteMenu;
        yield return new WaitForSeconds(1f);
        switch (value) {
            case "Options":
                pagsAnim.Play(value);
                break;
            case "Credits":
                pagsAnim.Play(value);
                break;
        }
    }

    IEnumerator PagsOut() {
        yield return new WaitForSeconds(2.9f);
        pagsGameobject.SetActive(false);
    }
}
