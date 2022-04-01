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

    private void Awake() {
        _rbody = GetComponent<Rigidbody>();
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
        Vector3 direction = new Vector3(  _moveInput.x * _speed , _rbody.velocity.y, _moveInput.y * _speed ); 

        _rbody.velocity = direction ;
    }

    public void OnViewInput(InputAction.CallbackContext context){
        mouse_delta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

}
