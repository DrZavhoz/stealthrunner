using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FunnyBlox
{
    public class Bootstrap : MonoSingleton<Bootstrap>
    {
        private void Start()
        {
            StartCoroutine(PostInitialize());
        }

        private IEnumerator PostInitialize()
        {
#if UNITY_IOS || UNITY_ANDROID
            Application.targetFrameRate = 60;
#endif

            CommonData.LoadDataFromPrefs();
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene("Game");
        }
    }
}