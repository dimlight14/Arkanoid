using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private uint currentLevel = 1;
        [Space(20)]
        [SerializeField]
        private GameObject ballPrefab;

        private static GameManager instance;
        private bool gameStarted = false;

        private List<BrickMainComponent> allBricks = new List<BrickMainComponent>();
        private List<BallMainComponent> allBalls = new List<BallMainComponent>();

        private void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            else {
                instance = this;
            }
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
        private void Initialize() {
            EventBus.Subscribe<BrickRegisteredEvent>(RegisterBrick);
            EventBus.Subscribe<BrickDestroyedEvent>(OnBrickDestroyed);

            EventBus.Subscribe<BallRegisteredEvent>(RegisterBall);
            EventBus.Subscribe<BallDestroyedEvent>(OnBallDestroyed);

            EventBus.Subscribe<SpawnExtraBallEvent>(OnSpawnExtraBallEvent);
            allBricks.Clear();
            allBalls.Clear();
            CustomArkanoidPhysics.SetUp();
        }

        #region  Register Objects
        private void OnBrickDestroyed(BrickDestroyedEvent customeEvent) {
            allBricks.Remove(customeEvent.Brick);
            CheckIfLevelCompleted();
        }
        private void RegisterBrick(BrickRegisteredEvent customeEvent) {
            allBricks.Add(customeEvent.Brick);
        }
        private void RegisterBall(BallRegisteredEvent customeEvent) {
            allBalls.Add(customeEvent.Ball);
        }
        private void OnBallDestroyed(BallDestroyedEvent customeEvent) {
            allBalls.Remove(customeEvent.Ball);
            CheckIfLevelLost();
        }
        #endregion

        private void Update() {
            if (!gameStarted && Input.GetMouseButtonUp(0)) {
                EventBus.FireEvent<GameStartedEvent>();
            }
        }
        private void OnSpawnExtraBallEvent(SpawnExtraBallEvent customeEvent) {
            if (allBalls.Count == 0) return;

            SpawnNewBallAt(allBalls[0].transform.position - new Vector3(0, 0.3f, 0));
        }
        private void SpawnNewBallAt(Vector3 position) {
            GameObject newBall = Instantiate(ballPrefab, position, Quaternion.identity);
            newBall.GetComponent<BallMainComponent>().SetUpAsExtraBall();
        }

        #region Ending Level/Loading Level
        private void CheckIfLevelCompleted() {
            if (allBricks.Count == 0) WinLevel();
        }
        private void CheckIfLevelLost() {
            if (allBalls.Count == 0) LoseLevel();
        }
        private void LoseLevel() {
            Debug.Log("level lost");
            LoadLevel(currentLevel);
        }
        private void WinLevel() {
            Debug.Log("level won");
            currentLevel++;
            LoadLevel(currentLevel);
        }
        private void LoadLevel(uint levelNumber) {
            EventBus.ClearAll();
            CustomArkanoidPhysics.ClearAll();
            Initialize();
            switch (levelNumber) {
                case 1:
                    currentLevel = 1;
                    SceneManager.LoadScene("Level1");
                    break;
                case 2:
                    currentLevel = 2;
                    SceneManager.LoadScene("Level2");
                    break;
                case 3:
                    currentLevel = 3;
                    SceneManager.LoadScene("Level3");
                    break;
                default:
                    currentLevel = 3;
                    SceneManager.LoadScene("Level3");
                    break;
            }
        }
        #endregion
    }
}