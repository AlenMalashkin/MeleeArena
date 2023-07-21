using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Code.Infrastructure
{
	public class SceneLoader
	{
		private ICoroutineRunner _coroutineRunner;
		
		public SceneLoader(ICoroutineRunner coroutineRunner)
		{
			_coroutineRunner = coroutineRunner;
		}

		public void Load(string sceneName, Action onLoaded = null)
		{
			_coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));
		}

		private IEnumerator LoadScene(string nextScene, Action onLoaded)
		{
			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

			while (!waitNextScene.isDone)
				yield return null;

			onLoaded?.Invoke();
			YandexGame.FullscreenShow();
		}
	}
}