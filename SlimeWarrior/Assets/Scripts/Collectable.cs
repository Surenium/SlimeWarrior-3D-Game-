using UnityEngine;

public enum CollectableType
{
    Coin,
    Potion,
    Item
}
public class Collectable : MonoBehaviour
{
    //Type
    [SerializeField] private CollectableType collectableType;
    //value
    [SerializeField] private int collectableValue;
    //Collider
    private Collider collectableCollider;
    //Renderer
    private Renderer collectableRenderer;
private void Start()
{
    //Get the collider on the object
    collectableCollider = GetComponent<Collider>();
    //make sure that it is set to isTrigger
    collectableCollider.isTrigger = true;
    //Collectable Renderer
    collectableRenderer = GetComponent<Renderer>();
}
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
        {
        //Get the PlayerController
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
        if (controller == null) return;
        //Call the collect method
        Collect(controller);
        
        }
    }
//Collection Actions
private void Collect(PlayerController controller)
{
    //Send the colelctable type and value to ther player controller
    controller.Collect(collectableType, collectableValue);
    //Disable the renderer
    collectableRenderer.enabled = false;
    //Disable the collider
    collectableCollider.enabled = false;
    //Destroy this object
    Destroy(gameObject);
    }
}
