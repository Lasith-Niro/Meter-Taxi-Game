using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region Fields
    private static GameController _instance;
    public static float interpolator = 1.0f;
    public bool slerpAnimate = false;
    public float slerpSpeed = 1.0f;

    public Text countdownText;
    public Text scoreText;
    public float countdown = 5f;

    [Header("Cameras")]
    public Camera cam1;
    public Camera cam2;

    [Header("Score")]
    public int score = 0;

    #endregion

    #region Properties	
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region Methods
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else
        {
            Destroy(gameObject);
            Debug.LogError("Multiple GameController instances in Scene. Destroying clone!");
        };
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        if (slerpAnimate)
        {
            interpolator = 0.5f * Mathf.Sin(Time.time * slerpSpeed) + 0.5f;
        }
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            Destroy(countdownText);
            countdown = 0;
        }
        else
        {
            countdownText.text = Mathf.Floor(countdown).ToString();
        }

    }

    #endregion
    #endregion
}
