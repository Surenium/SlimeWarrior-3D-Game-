using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Demo for common Monobehaviour Methods Exectution Order
public class ExectionOrderDemo : MonoBehaviour
{
    public int maxUpdates = 10;
    private int updates = 0;
    public int maxFixedUpdates = 10;
    private int fixedUpdates = 0;
    #region "Editor"    //Will only be available in the editor
    //Reset is called when the user hits the Reset button in the Inspector
    void Reset()
    {
        Debug.Log("Reset");
    }
    #endregion

    #region "Initialization"    //Will only be called on the first frame
    //Awake is called before Start
    void Awake()
    {
        Debug.Log("Awake");
    }
    //OnEnable is called when the object is enabled
    void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        //Start the coroutine
        StartCoroutine(ExampleCoroutine());
    }
    #endregion

    #region "Physics"   //Will only be called on the physics update (FixedTimeStep which is set to 0.02 in the project settings)
    //FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        if(fixedUpdates < maxFixedUpdates)
        {
            float time = Time.fixedDeltaTime;
            Debug.Log("FixedUpdate : " + time);
            fixedUpdates++;
        }
    }
    //OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
    }
    //OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }
    #endregion

    #region "Input"  //Will only be called on the input update
    //OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider
    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    }
    //OnMouseOver is called every frame while the mouse is over the GUIElement or Collider
    void OnMouseOver()
    {
        Debug.Log("OnMouseOver");
    }
    //OnMouseUp is called when the user has released the mouse button
    void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
    }
    #endregion

    #region "Game Logic" //Called when the game logic is updated
    // Update is called once per frame
    void Update()
    {
        if(updates < maxUpdates)
        {
            float timeSinceLastFrame = Time.deltaTime;
            Debug.Log("Update: " + timeSinceLastFrame);
            updates++;
        }
    }
    //LateUpdate is called every frame after all other update functions have been called
    void LateUpdate()
    {
        if(updates < maxUpdates)
        {
            //Time since last frame
            float timeSinceLastFrame = Time.deltaTime;
            Debug.Log("LateUpdate: " + timeSinceLastFrame);
        }
    }
    #endregion

    #region "Decommissioning" //Called when the object being removed from the game
    //OnDisable is called when the behaviour becomes disabled
    void OnDisable()
    {
        Debug.Log("OnDisable");
    }
    //OnDestroy is called when the behaviour is destroyed
    void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
    #endregion

    #region "Coroutine Times"
    //Coroutines are used to run code in a background thread
    //Couroutine is a function that can be paused and resumed
    //yeild return is used to pause the coroutine
    //Pause time (what comes after the yield) is the time that the coroutine will wait before resuming
    IEnumerator ExampleCoroutine()
    {
        //Start time
        float startTime = Time.time;
        Debug.LogFormat("CoroutineTimes: {0}",Time.time - startTime);
        //Wait for 1 second
        yield return new WaitForSeconds(1);
        Debug.LogFormat("Wait for Seconds(1) ended after {0} ", Time.time - startTime);
        startTime = Time.time;
        //Wait for 1 frame
        yield return null;
        Debug.LogFormat("Wait until null(Next Frame) ended after {0}", Time.time - startTime);
        startTime = Time.time;
        //Wait for the end of the frame
        yield return new WaitForEndOfFrame();
        Debug.LogFormat("Wait for EndOfFrame() ended after {0}", Time.time - startTime);
        startTime = Time.time;
        //Wait for the FixedUpdate
        yield return new WaitForFixedUpdate();
        Debug.LogFormat("Wait for FixedUpdate() ended after {0}", Time.time - startTime);
        startTime = Time.time;
        //Wait for 1 second in realtime
        yield return new WaitForSecondsRealtime(1);
        Debug.LogFormat("Wait for SecondsRealtime(1) ended after {0}", Time.time - startTime);
        startTime = Time.time;
        //Wait unitil I release the space key
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        Debug.LogFormat("Wait until Input.GetKeyUp(KeyCode.Space) ended after {0}", Time.time - startTime);
        startTime = Time.time;
        //Wait until I press the space key
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        Debug.LogFormat("Wait until Input.GetKeyDown(KeyCode.Space) ended after {0}", Time.time - startTime);
        startTime = Time.time;
        //Wait while I press the space key
        yield return new WaitWhile(() => Input.GetKey(KeyCode.Space));
        Debug.LogFormat("Wait while Input.GetKey(KeyCode.Space) ended after {0}", Time.time - startTime);
        startTime = Time.time;
    }
    #endregion
}
