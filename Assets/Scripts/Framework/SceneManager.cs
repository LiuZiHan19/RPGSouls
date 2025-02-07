using System.Collections;
using UnityEngine.Events;

public class SceneManager : Singleton<SceneManager>
{
    public IEnumerator LoadSceneAsync(string sceneName, UnityAction callback = null, float delayCallback = 0f)
    {
        var ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        yield return ao;
        yield return delayCallback;
        callback?.Invoke();
    }
}