using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    private string sceneName;
    [SerializeField] float delayBeforeLoadingScene;
    public void Initialize(string sceneName)
    {
        this.sceneName = sceneName.Trim(); // Trim arrange les esapces etc.
    }

    public void LaunchVictory()
    {
        StartCoroutine(_VictorySceneLauncher());
    }
    private IEnumerator _VictorySceneLauncher()
    {
        yield return new WaitForSeconds(delayBeforeLoadingScene);
        SceneManager.LoadScene("VictoryScene");
    }
}
