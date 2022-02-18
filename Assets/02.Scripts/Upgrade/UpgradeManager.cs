using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour
{
    static public UpgradeManager instance;
    private UpgradeElement[] upgradeElements;
    private List<UpgradeElement> list_CurrentUpgrade = new List<UpgradeElement>();
    [HideInInspector] public List<GameObject> deffenders = new List<GameObject>();
    [HideInInspector] public List<GameObject> interceptors = new List<GameObject>();

    public float upgradeSpeed = 1f;
    public float upgradeTimeRequired;
    private float _elapsedUpgradeTime;
    private float elapsedUpgradeTime {
        set
        {
            _elapsedUpgradeTime = value;
            upgradeTimeSlider.value = _elapsedUpgradeTime / upgradeTimeRequired;
        }
        get { return _elapsedUpgradeTime; }
    }
    
    [SerializeField] GameObject upgradeSelectorUI;
    [SerializeField] GameObject upgradeSelectOptionUI;
    [SerializeField] Slider upgradeTimeSlider;
    private void Awake()
    {
        instance = this;
        GameStateManager.instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void Start()
    {
        upgradeElements = transform.GetChild(0).GetComponents<UpgradeElement>();
    }
    private void OnDestroy()
    {
        GameStateManager.instance.OnGameStateChanged -= OnGameStateChanged;
    }
    private void FixedUpdate()
    {
        if ((elapsedUpgradeTime > upgradeTimeRequired) &
            (upgradeSelectorUI.activeSelf == false))
        {
            ShowUpgradeSelector();
            elapsedUpgradeTime = 0f;
        }
            

        if(GameStateManager.instance.CurrentGameState == GameState.Play)
            elapsedUpgradeTime += upgradeSpeed * Time.fixedDeltaTime;
    }
    public void Upgrade(UpgradeElement upgradeElement)
    {
        upgradeElement.OnUpgrade();
    }
    public void Downgrade(UpgradeElement upgradeElement)
    {
        upgradeElement.OnDowngrade();
    }
    public void ShowUpgradeSelector()
    {
        GameStateManager.instance.SetState(GameState.Paused);
        DrawSelectorOptionUI();
        upgradeSelectorUI.SetActive(true);
    }
    public void DrawSelectorOptionUI()
    {   
        List<UpgradeElement> tmpList = upgradeElements.ToList();
        int index;
        GameObject[] options = new GameObject[3];
        for (int i = 0; i < options.Length; i++)
        {
            options[i] = Instantiate(upgradeSelectOptionUI, upgradeSelectorUI.transform);
            index = Random.Range(0, tmpList.Count);
            UpgradeElement tmpUpgradeElement = tmpList[index];
            tmpList.Remove(tmpUpgradeElement);
            options[i].transform.GetChild(1).GetComponent<RawImage>().texture = tmpUpgradeElement.previewTexture;
            options[i].transform.GetChild(2).GetComponent<Text>().text = tmpUpgradeElement.discription;
            options[i].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => Upgrade(tmpUpgradeElement));
            options[i].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => {
                for (int i = 0; i < options.Length; i++)
                {
                    Destroy(options[i]);
                }
                upgradeSelectorUI.SetActive(false);
                GameStateManager.instance.SetState(GameState.Play);
            });
        }
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Play;
    }
}