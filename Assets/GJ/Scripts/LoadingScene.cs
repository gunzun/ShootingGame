using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GJ
{
    public class LoadingScene : MonoBehaviour
    {
        static string nextScene_str;
        static int nextScene_num;
        static bool isValuePassedStr = false;

        [SerializeField]
        Image progressBar;

        /// <summary>
        /// �ε����� �ҷ��� ���� �ε��� ���� ���� �ٷ� ���� ������ �Ѿ�� �Ѵ�.
        /// </summary>
        /// <param name="_sceneName">�ε� �� ������ ���� �̸�</param>
        public static void LoadScene(string _sceneName)
        {
            // nextScene = _sceneName;
            // �ε� ���� �÷��̾� ����
            SceneManager.LoadScene(GameManager.Instance.GetLoadingSceneNum);
            nextScene_str = _sceneName;
            isValuePassedStr = true;
        }
        /// <summary>
        /// �ε����� �ҷ��� ���� �ε��� ���� ���� �ٷ� ���� ������ �Ѿ�� �Ѵ�.
        /// </summary>
        /// <param name="_sceneName">�ε� �� ������ ���� �̸�</param>
        public static void LoadScene(int _sceneNum)
        {
            // nextScene = _sceneNum;
            // �ε� ���� �÷��̾� ����
            SceneManager.LoadScene(GameManager.Instance.GetLoadingSceneNum);
            nextScene_num = _sceneNum;
            isValuePassedStr = false;
        }
        void Start()
        {
            StartCoroutine(LoadSceneProcess());
        }
        IEnumerator LoadSceneProcess()
        {
            AsyncOperation op;
            if (isValuePassedStr == true)
            {
                op = SceneManager.LoadSceneAsync(nextScene_str);                // �񵿱� ������� �ҷ��´�.
            }
            else
            {
                op = SceneManager.LoadSceneAsync(nextScene_num);                // �񵿱� ������� �ҷ��´�.
            }

            op.allowSceneActivation = false;                                    // ���� 90% ���������� �ҷ����� ��� (�� �ε� �ӵ��� �ʹ� ���� ���� ���, ���� ����� ������ ������ �� ���� ����κ��� ���ҽ��� �о�;� �ϱ⿡ ���ε����� ���ҽ� �ε��� ���� ��� �ε� ���� ���� ������Ʈ���� ������ ���� ��츦 ��� )

            float timer = 0f;
            while (!op.isDone)                                                  // �� �ε��� ������ ���� ��� �ݺ�
            {
                yield return null;                                              // �ݺ����� �� �� �ݺ��� ������ ����Ƽ ������ ����� ��ȯ, ������� ���� ���� �� ȭ���� ���ŵ��� �ʾ� �ε��ٰ� ���� �ʴ´�.

                if (op.progress < 0.9f)                                         // ���� �ε� ���൵�� 90%���� ������
                {
                    progressBar.fillAmount = op.progress;                       // ���൵ ��ŭ ���α׷����ٸ� ä���.
                }
                else                                                            // ���� �ε� ���൵�� 90%�� �Ѿ��ٸ�
                {
                    timer += Time.unscaledDeltaTime;
                    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);       // ���α׷����ٸ� 0.9���� 1�� 1�ʿ� ���ļ� ä���.
                    if (progressBar.fillAmount >= 1.0f)                         // ���α׷����ٰ� �� á�ٸ�
                    {
                        op.allowSceneActivation = true;                         // ���� �ҷ��´�.
                        yield break;
                    }
                }
            }
        }
    }
}
