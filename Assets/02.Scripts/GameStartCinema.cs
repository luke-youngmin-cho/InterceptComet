using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameStartCinema : MonoBehaviour
{
    
    [SerializeField] List<GameObject> fadingLeftObjects;
    [SerializeField] List<GameObject> fadingRightObjects;
    [SerializeField] List<GameObject> fadingTopObjects;
    [SerializeField] List<GameObject> fadingBottomObjects;
    [SerializeField] float moveSpeed;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float canvasFadeSpeed;
    [SerializeField] CanvasGroup fadeBlackOutCanvasGroup;
    [SerializeField] Camera cam;
    [SerializeField] float camSpeed;
    private float fadeProgress;
    [SerializeField] float fadeMin;

    public void OnStartButtonClick()
    {
        canvasGroup.interactable = false;
        StartCoroutine(GameStartingCinema());
    }

    IEnumerator GameStartingCinema()
    {
        while (fadeProgress < fadeMin)
        {
            foreach (var item in fadingLeftObjects)
            {
                item.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            foreach (var item in fadingRightObjects)
            {
                item.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            foreach (var item in fadingTopObjects)
            {
                item.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            foreach (var item in fadingBottomObjects)
            {
                item.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            }
            cam.transform.Translate(Vector3.back * camSpeed * Time.deltaTime);
            fadeProgress += canvasFadeSpeed * Time.deltaTime;
            canvasGroup.alpha -= fadeProgress;
            fadeBlackOutCanvasGroup.alpha += fadeProgress;
            yield return null;
        }
        SceneManager.LoadScene("GamePlay");
    }
}
