using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Vector2 mouse_delta;   

    [Header("Camera view")]
    public Transform camera;
    public float minXview;
    public float maxXview;
    public float mouse_sensitivity;

    float cameraRotationY;
    float cameraRotationX;

    [Header("Player Movement")]
    public float _speed;
    private Vector2 _moveInput;
    private Rigidbody _rbody;

    public float _jumpForce;

    private void Awake() {
        _rbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Movement();
    }

    void LateUpdate()
    {
        CameraView();
    }

    public void CameraView(){

        cameraRotationY += mouse_delta.y * mouse_sensitivity;
        cameraRotationX = mouse_delta.x * mouse_sensitivity;

        cameraRotationY = Mathf.Clamp(cameraRotationY, minXview, maxXview);
        camera.localEulerAngles = new Vector3(-cameraRotationY,0,0);

        transform.eulerAngles += Vector3.up *  cameraRotationX;
        
    }

    public void Movement(){
        
        Vector3 direction = transform.right * _moveInput.x * _speed + transform.forward * _moveInput.y * _speed;

        direction.y = _rbody.velocity.y;

        _rbody.velocity = direction ;

    }

    public void OnViewInput(InputAction.CallbackContext context){
        mouse_delta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context){

        if(context.phase == InputActionPhase.Started){
            Ray hit = new Ray(transform.position, Vector3.down);

            if(Physics.Raycast(hit, 1.1f)){
                _rbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
            
        }
    }

    public void OnRun(InputAction.CallbackContext context){

        if(context.phase == InputActionPhase.Performed){
            _speed = _speed * 2;
        }

        if(context.phase == InputActionPhase.Canceled){
            _speed = _speed / 2;
        }
    }

}
