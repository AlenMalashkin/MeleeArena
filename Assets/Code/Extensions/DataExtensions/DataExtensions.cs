using UnityEngine;

namespace Code.Extensions.DataExtensions
{
	public static class DataExtensions
	{
		public static float SqrMagnitudeToTarget(this Vector3 from, Vector3 to)
			=> Vector3.SqrMagnitude(to - from);
		
		public static string ToJson(this object json)
			=> JsonUtility.ToJson(json);

		public static T FromJson<T>(this string json)
			=> JsonUtility.FromJson<T>(json);
	}
}