using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
enum e_GamePlayFSM
{
    Idle,
    IntroduceComets,
    WaitForIntroduceComets,
    SpawnComets,
    StartGame,
    GameStarted,
    GameClear,
    GameOver,
}
public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public bool isReplay;
    private void Awake()
    {
        // on next stage
        if ((instance != null) &
            (isReplay == false))
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        // on replay
        else if(isReplay == true)
        {
            //??
        }
    }
    e_GamePlayFSM gamePlayFSM;
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject introduceCometUI;
    [SerializeField] GameObject gameClearUI;
    [SerializeField] GameObject gameOverUI;
    public Earth earth;
    private void Start()
    {
        StartGame();
    }
    private void Update()
    {
        Workflow();
        //Debug.Log(CometSpawner.instance.list_Comet.Count);
    }
    private void Workflow()
    {
        switch (gamePlayFSM)
        {
            case e_GamePlayFSM.Idle:
                break;
            case e_GamePlayFSM.IntroduceComets:
                introduceCometUI.GetComponent<IntroduceCometUI>().CreatePreview();
                introduceCometUI.SetActive(true);
                NextState();
                break;
            case e_GamePlayFSM.WaitForIntroduceComets:
                break;
            case e_GamePlayFSM.SpawnComets:
                CometSpawner.instance.Spawn();
                NextState();
                break;
            case e_GamePlayFSM.StartGame:
                playerUI.SetActive(true);
                GameStateManager.instance.SetState(GameState.Play);
                NextState();
                break;
            case e_GamePlayFSM.GameStarted:
                MonitorGameProgress();
                break;
            case e_GamePlayFSM.GameClear:
                gameClearUI.SetActive(true);
                break;
            case e_GamePlayFSM.GameOver:
                gameOverUI.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void StartGame()
    {
        if (isReplay)
        {
            gamePlayFSM = e_GamePlayFSM.SpawnComets;
        }
        else
        {
            gamePlayFSM = e_GamePlayFSM.IntroduceComets;
        }
    }
    public void NextState()
    {
        gamePlayFSM++;
    }
    private void MonitorGameProgress()
    {
        if(earth.HP <= 0)
        {
            gamePlayFSM = e_GamePlayFSM.GameOver;
        }
        else if(CometSpawner.instance.list_Comet.Count == 0)
        {
            gamePlayFSM = e_GamePlayFSM.GameClear;
        }
    }

    public void Replay()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isReplay = true;
    }
    public void BackToRobby()
    {
        SceneManager.LoadScene("StageSelection");
    }
    public void NextStage()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(currentSceneIndex + 1);
        isReplay = false;
    }
}
