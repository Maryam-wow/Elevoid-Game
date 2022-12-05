using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameManager))]
public class GameSceneManager : MonoBehaviour
{
    [Tooltip("Returns the scene name with the index according to selected character as provided by the Quiz")]
    [SerializeField] private string[] characterGameplaySceneNames;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(characterGameplaySceneNames[index]);
    }
}
