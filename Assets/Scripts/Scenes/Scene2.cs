using UnityEngine;

public class Scene2 : MonoBehaviour
{
    public static Scene2 instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    [SerializeField] private Dialogue startDialogue;
    
    void Start()
    {
        GameManager.instance.player.GetComponent<Inventory>().AddItem(ItemType.Plank);
        DialogueManager.instance.StartDialogue(startDialogue);
    }
}
