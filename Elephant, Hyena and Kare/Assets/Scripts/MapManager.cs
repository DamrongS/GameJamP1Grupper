using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public Transform player;
    public int amountOfLevels;
    public float movementSpeed;

    private float timer = 0f;
    
    private InputAction moveRight;
    private InputAction moveLeft;
    private InputAction enterLevel;
    
    private int _currentPos = 1;
    private int _nextPos = 1;

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
    }

    void Update()
    {
        if (moveRight.WasPressedThisFrame() && _currentPos < amountOfLevels && isCurrentlyMoving == false)
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
}
