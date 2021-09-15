using UnityEngine;

public class LeaderboardsManager : MonoBehaviour
{
	public void GhostDestroyedHandler()
	{
		System.Diagnostics.Debug.WriteLine("GhostDestroyedHandler");



	}

	public void PlayerSurvivalHandler(float time)
	{
		System.Diagnostics.Debug.WriteLine("PlayerSurvivalHandler - " + time.ToString());



	}

	public void AccuracyHandler(BulletEventType type)
	{
		System.Diagnostics.Debug.WriteLine("AccuracyHandler" + type.ToString());



	}

	public void GhostSurvivalHandler(float time)
	{
		System.Diagnostics.Debug.WriteLine("GhostSurvivalHandler" + time.ToString());



	}
}

