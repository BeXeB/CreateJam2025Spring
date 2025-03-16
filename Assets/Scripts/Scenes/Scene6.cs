using UnityEngine;

public class Scene6 : MonoBehaviour
{
    public static Scene6 instance;

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
    
    private void Start()
    {
        DialogueManager.instance.StartDialogue(startDialogue);
    }
}
