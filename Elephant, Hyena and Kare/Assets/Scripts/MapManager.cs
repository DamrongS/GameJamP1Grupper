
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [Header("Map Settings")]
    public Transform player;
    public int amountOfLevels;
    public float movementSpeed;
    public Sprite[] mapsTextures;
    public SpriteRenderer mapSpriteRenderer;

    [Header("Debug Settings - !DNT!")]
    public int pPrefsUnlockedLevels;
    public int pPrefsCurrentCompleted;

    private InputAction moveRight;
    private InputAction moveLeft;
    private InputAction enterLevel;

    private int _currentPos = 1;
    private int _nextPos = 1;
    private int _unlockedLevels = 1;

    private bool isCurrentlyMoving = false;
    private bool waitingForInputRelease = true;
    
    void Start()
    {
        // Set up input actions
        moveRight = InputSystem.actions.FindAction("MoveRight");
        moveLeft = InputSystem.actions.FindAction("MoveLeft");
        enterLevel = InputSystem.actions.FindAction("EnterLevel");

        moveRight.Enable();
        moveLeft.Enable();
        enterLevel.Enable();

        // Load saved progress
        _unlockedLevels = Mathf.Clamp(
            PlayerPrefs.GetInt("LevelsUnlocked", 1), 1, amountOfLevels
        );

        pPrefsUnlockedLevels = _unlockedLevels;
        pPrefsCurrentCompleted = PlayerPrefs.GetInt("LastLevelCompleted", 0);

        // Set map texture based on unlocked levels
        mapSpriteRenderer.sprite = mapsTextures[_unlockedLevels - 1];

        // Spawn at the level just completed
        _currentPos = Mathf.Clamp(pPrefsCurrentCompleted, 1, _unlockedLevels);

        // Synchronize positions to prevent automatic movement
        _nextPos = _currentPos;

        // Set player position instantly
        player.position = GameObject.Find($"Level_{_currentPos}").transform.position;

        // Reset movement state
        isCurrentlyMoving = false;

        // Debug information
        Debug.Log($"Current Position: {_currentPos}");
        Debug.Log($"Next Position: {_nextPos}");
        Debug.Log($"Levels Unlocked: {_unlockedLevels}");
        Debug.Log($"Last Level Completed: {pPrefsCurrentCompleted}");
    }

    
    void Update()
    {
        // Ignore movement until both keys have been released
        if (waitingForInputRelease)
        {
            if (moveRight.IsPressed() || moveLeft.IsPressed())
            {
                return;
            }

            // Both keys released, movement is now allowed
            waitingForInputRelease = false;
            return;
        }

        // Handle movement input
        if (moveRight.WasPressedThisFrame() &&
            _currentPos < amountOfLevels &&
            !isCurrentlyMoving &&
            _currentPos < _unlockedLevels)
        {
            _nextPos++;
        }
        else if (moveLeft.WasPressedThisFrame() &&
                 _currentPos > 1 &&
                 !isCurrentlyMoving)
        {
            _nextPos--;
        }

        // Move player between map positions
        if (_nextPos != _currentPos)
        {
            Vector3 desPos = GameObject.Find($"Level_{_nextPos}").transform.position;

            player.position = Vector3.MoveTowards(
                player.position,
                desPos,
                movementSpeed * Time.deltaTime
            );

            isCurrentlyMoving = true;

            if (Vector3.Distance(player.position, desPos) < 0.01f)
            {
                player.position = desPos;
                _currentPos = _nextPos;
                isCurrentlyMoving = false;
            }
        }

        // Enter selected level
        if (enterLevel.WasPressedThisFrame() && !isCurrentlyMoving)
        {
            SceneManager.LoadScene($"Level_{_currentPos - 1}");
        }
    }


    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("LevelsUnlocked", pPrefsUnlockedLevels);
        PlayerPrefs.SetInt("LastLevelCompleted", pPrefsCurrentCompleted);
        PlayerPrefs.Save();

        // Refresh map
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
