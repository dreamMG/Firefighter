using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region Singleton
    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);
    }
    #endregion

    public static string namePlayer;

    [SerializeField] private InputField field;
    [SerializeField] private Text error;
    [SerializeField] private Text score;
    private AudioSource sourceAudio;

    private void Start()
    {
        sourceAudio = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Lead"))
            score.text = PlayerPrefs.GetString("Lead") + " " + PlayerPrefs.GetFloat("Time") + "s";
        else
            score.text = "None 60s";
    }

    public void ButtonStart()
    {
        if (field.text.Length > 2)
        {
            namePlayer = field.text;
            sourceAudio.Play();
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
        else
        {
            error.text = "Name must have more than 2 letters";
        }
    }
}
