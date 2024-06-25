using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
  
    private bool _canDodge = true;
    private void Update()
    {
        if (_canDodge && Input.GetKeyDown(KeyCode.Space))
        {
            _canDodge = false;
            _playerMovement.enabled = false;
           
            Invoke(nameof(Test), 1f);
        }
    }

    private void Test()
    {
        _canDodge = true;
        _playerMovement.enabled = true;
    }
}