using UnityEngine;

public class TubeClickHandler : MonoBehaviour
{
    private TubeController tube;

    void Start()
    {
        tube = GetComponent<TubeController>();
    }

    void OnMouseDown()
    {
        FindObjectOfType<GameManager>().OnTubeClicked(tube);
    }
}
