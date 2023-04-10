using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GJ
{
    public class LoadingScene : MonoBehaviour
    {
        static string nextScene;
        [SerializeField]
        Image progressBar;

        /// <summary>
        /// 로딩씬에서 다음 씬으로 넘어가는 메소드 
        /// </summary>
        /// <param name="_sceneName">다음 씬의 이름을 string으로 입력하세요.</param>
        public static void LoadScene(/*string _sceneName*/)
        {
            // nextScene = _sceneName;
            // 로딩 이후 플레이씬 실행
            SceneManager.LoadScene(1);
        }
        void Start()
        {
            StartCoroutine(LoadSceneProcess());
        }
        IEnumerator LoadSceneProcess()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);         // 비동기 방식으로 불러온다.
            op.allowSceneActivation = false;                                    // 씬을 90% 정도까지만 불러오고 대기 (씬 로딩 속도가 너무 빠를 때를 대비, 에셋 번들로 나눠서 빌드할 때 에셋 번들로부터 리소스를 읽어와야 하기에 씬로딩보다 리소스 로딩이 늦을 경우 로딩 되지 않은 오브젝트들이 깨져서 보일 경우를 대비 )

            float timer = 0f;
            while (!op.isDone)                                                  // 씬 로딩이 끝나지 않은 경우 반복
            {
                yield return null;                                              // 반복문이 한 번 반복될 때마다 유니티 엔진에 제어권 반환, 제어권을 주지 않을 시 화면이 갱신되지 않아 로딩바가 차지 않는다.

                if (op.progress < 0.9f)                                         // 씬의 로딩 진행도가 90%보다 작으면
                {
                    progressBar.fillAmount = op.progress;                       // 진행도 만큼 프로그레스바를 채운다.
                }
                else                                                            // 씬의 로딩 진행도가 90%를 넘었다면
                {
                    timer += Time.unscaledDeltaTime;
                    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);       // 프로그레스바를 0.9에서 1로 1초에 걸쳐서 채운다.
                    if (progressBar.fillAmount >= 1.0f)                         // 프로그레스바가 다 찼다면
                    {
                        op.allowSceneActivation = true;                         // 씬을 불러온다.
                        yield break;
                    }
                }
            }
        }
    }
}
