using System.Linq;
using Code.Data;
using Code.Logic.Spawners;
using Code.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
	[CustomEditor(typeof(LevelStaticData))]
	public class LevelStaticDataEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			LevelStaticData levelStaticData = (LevelStaticData) target;

			if (GUILayout.Button("Collect"))
			{
				levelStaticData.LevelName = SceneManager.GetActiveScene().name;
				levelStaticData.EnemySpawners = FindObjectsOfType<EnemySpawner>()
					.Select(x => new EnemySpawnerData(x.Type, x.transform.position, x.TimeToRespawn))
					.ToList();
				levelStaticData.PlayerPositionOnLevel = FindObjectOfType<PlayerSpawner>().transform.position;
			}
			
			EditorUtility.SetDirty(target);
		}
	}
}