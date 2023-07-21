using System.Linq;
using Code.Data;
using Code.Logic;
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
				levelStaticData.EnemySpawners = FindObjectsOfType<EnemySpawnMarker>()
					.Select(x => new EnemySpawnerData(x.transform.position))
					.ToList();
				levelStaticData.PlayerPositionOnLevel = FindObjectOfType<PlayerSpawner>().transform.position;
			}
			
			EditorUtility.SetDirty(target);
		}
	}
}