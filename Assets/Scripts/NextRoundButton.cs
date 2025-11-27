using UnityEngine;
using UnityEngine.UI;

public class NextRoundButton : MonoBehaviour
{
    [SerializeField]
    WaveSpawner waveSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button> ().onClick.AddListener(waveSpawner.StartNewRound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
