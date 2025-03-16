using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    [SerializeField] private GameOverMenu dedMenu;
    [SerializeField] private GameOverMenu finMenu;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        if (player.GetComponent<PlayerMovement>().inWheelchair)
        {
            var chairs = FindObjectsByType<Wheelchair>(FindObjectsSortMode.None);
            foreach (var chair in chairs)
            {
                if (!chair.GetComponentInParent<PlayerMovement>())
                {
                    Destroy(chair.gameObject);
                }
            }
        }
        var startLoc = GameObject.FindWithTag("Start")?.transform.position;
        if (startLoc != null)
        {
            player.transform.position = (Vector3)startLoc;
        }
    }

    public void LoadNext()
    {
        var pm = player.GetComponent<PlayerMovement>();
        if (!pm.inWheelchair)
        {
            DialogueManager.instance.StartDialogue(new Dialogue
            {
                sentences = new DialogueData[]
                {
                    new ()
                    {
                        name = "System",
                        sentence = "Please come back with your wheelchair"
                    }
                }
            });
            return;
        }
        var newIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (newIndex >= SceneManager.sceneCountInBuildSettings)
        {
            return;
        }
        SceneManager.LoadScene(newIndex);
    }

    public void GameOver()
    {
        player.SetActive(false);
        dedMenu.GameOver();
    }

    public void Fin()
    {
        finMenu.GameOver();
    }

    public void Reload()
    {
        player.GetComponent<Inventory>().SetInventory(new());
        var pm = player.GetComponent<PlayerMovement>();
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (!pm.inWheelchair)
            {
                var wheelChair = GameObject.Find("Wheelchair");
                wheelChair.GetComponent<Wheelchair>().Interact(player);
            }
        }
        else
        {
            if (pm.inWheelchair)
            {
                pm.GetOutOfChairImplementation();
                Destroy(GameObject.Find("Wheelchair"));
            }
        }
        
        player.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(1);
    }
}