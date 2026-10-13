using UnityEngine;
using UnityEngine.InputSystem;

public class MapManager : MonoBehaviour
{
    public Transform player;
    public int amountOfLevels;
    public float movementSpeed;
    
    private InputAction moveRight;
    private InputAction moveLeft;
    private int _currentPos = 1;
    private int _nextPos = 2;
    
    void Start()
    {
        moveRight = InputSystem.actions.FindAction("MoveRight");
        moveLeft = InputSystem.actions.FindAction("MoveLeft");
        
        moveRight.Enable();
        moveLeft.Enable();
        
        Debug.Log(GameObject.Find("Level_1").transform.position);
        player.position = GameObject.Find("Level_1").transform.position;
    }

    void Update()
    {
        if (moveRight.WasPressedThisFrame() && _currentPos != amountOfLevels)
        {
            _nextPos++;
            Debug.Log(_nextPos);
        } else if (moveLeft.WasPressedThisFrame() && _currentPos != 1)
        {
            _nextPos--;
            Debug.Log(_nextPos);
        }

        if (_nextPos != _currentPos)
        {
            Vector3 startPos = player.position;
            Vector3 desPos = GameObject.Find($"Level_{_nextPos}").transform.position;

            player.position = Vector3.Lerp(startPos, desPos, movementSpeed * Time.deltaTime);
            _currentPos = _nextPos;
        }
    }
}
