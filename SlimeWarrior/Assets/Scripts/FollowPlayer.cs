using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //Offset from the player
    [SerializeField] private float playerOffset;
    //Player to follow
    [SerializeField] private Transform player;
    //Rotate Speed
    [SerializeField] private float rotateSpeed;
    //Follow Speed
    [SerializeField] private float followSpeed;

    //Camera Floats
    private float mouseX;
    private float mouseY;
    //Camera RotationBounds
    [SerializeField] private float minPitch = 0.0f;
    [SerializeField] private float maxPitch = 90.0f;

    //Activation
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        //Register to start with the Game Manager

        GameManager.instance.GameStartedEvent += Activate;

        //register to stop with the Game Manager

        GameManager.instance.GameOverEvent += Deactivate;

        //Register to Pause with the Game Manager

        GameManager.instance.PauseGameEvent += OnPause;
    }

    private void OnPause(bool pause)

    {

        if (!pause)

        {

            Activate();

        }

        else

        {

            Deactivate();

        }

    }

    //Set the camera to be active

    private void Activate()

    {

        isActive = true;

    }

    //Set the camera to be inactive

    private void Deactivate()

    {

        isActive = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        //Get Mouse Input
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        }
    private void LateUpdate()
    {
        if (!isActive) return;
        //Rotate the Camera around the player
        RotateCamera();
    
        //calculate the speed and destination to catch up to the player
        Vector3 destination = player.position + (transform.forward * playerOffset);
        float distance = followSpeed * Time.deltaTime;
    
        transform.position = Vector3.Lerp(transform.position, destination, distance);
        }
    //Rotate Camera
    private void RotateCamera()
    {
        //Rotate the Camera around the player
        //X Rotation
        float rotationAmountX = mouseX * rotateSpeed * Time.deltaTime;
        transform.RotateAround(player.position, Vector3.up, rotationAmountX);
        //YRotation
        float rotationAmountY = -mouseY * rotateSpeed * Time.deltaTime;
        transform.RotateAround(player.position, transform.right, rotationAmountY);
   
    //clamp the Camera rotationa
    //set the pitch
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = Mathf.Clamp(eulerRotation.x, minPitch, maxPitch);
        //fix the z
        eulerRotation.z = 0;
        transform.eulerAngles = eulerRotation;
    }
}
