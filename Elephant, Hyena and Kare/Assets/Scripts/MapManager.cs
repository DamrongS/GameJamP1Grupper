using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
    
    void Start()
    {
        moveRight = InputSystem.actions.FindAction("MoveRight");
        moveLeft = InputSystem.actions.FindAction("MoveLeft");
        enterLevel = InputSystem.actions.FindAction("EnterLevel");
        
        moveRight.Enable();
        moveLeft.Enable();
        enterLevel.Enable();
        
        Debug.Log(GameObject.Find("Level_1").transform.position);
        player.position = GameObject.Find("Level_1").transform.position;

        _unlockedLevels = PlayerPrefs.GetInt("LevelsUnlocked");
        Debug.Log(_unlockedLevels);

        mapSpriteRenderer.sprite = mapsTextures[_unlockedLevels - 1];
        player.transform.position = GameObject.Find($"Level_{pPrefsCurrentCompleted}").transform.position;
        _nextPos = pPrefsCurrentCompleted;
    }

    void Update()
    {
        if (moveRight.WasPressedThisFrame() && _currentPos < amountOfLevels && isCurrentlyMoving == false && _currentPos <= _unlockedLevels - 1)
        {
            _nextPos++;
            Debug.Log(_nextPos);
        } else if (moveLeft.WasPressedThisFrame() && _currentPos > 1 && isCurrentlyMoving == false)
        {
            _nextPos--;
            Debug.Log(_nextPos);
        }   

        if (_nextPos != _currentPos)
        {
            Vector3 desPos = GameObject.Find($"Level_{_nextPos}").transform.position;

            player.position = Vector3.MoveTowards(player.position, desPos, movementSpeed * Time.deltaTime);
            isCurrentlyMoving = true;

            if (Vector3.Distance(player.position, desPos) < 0.01f)
            {
                player.position = desPos;
                _currentPos = _nextPos;
                isCurrentlyMoving = false;
            }
        }

        if (enterLevel.WasPressedThisFrame())
        {
            SceneManager.LoadScene($"Level_{_currentPos}");
        }
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("LevelsUnlocked", pPrefsUnlockedLevels);
        PlayerPrefs.SetInt("LastLevelCompleted", pPrefsCurrentCompleted);
        _unlockedLevels = PlayerPrefs.GetInt("LevelsUnlocked");
        Debug.Log(PlayerPrefs.GetInt("LevelsUnlocked"));
        PlayerPrefs.Save();
    }
}