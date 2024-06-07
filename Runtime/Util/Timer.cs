using UnityEngine;

namespace CamLib
{
	public class Timer
	{
		private float _startTime = -1;
		private float _endTime = -1;

		public float Duration { get; private set; }
		
		public bool IsRunning => Time.time < _endTime;
		public float Elapsed => Time.time - _startTime;

		public float Ratio => Elapsed / Duration;
		public float RatioReverse => 1 - Ratio;

		public Timer Set(float time)
		{
			_startTime = Time.time;
			_endTime = _startTime + time;

			Duration = _endTime - _startTime;
			return this;
		}
		
		public static Timer StartNew(float time)
		{
			return new Timer().Set(time);
		}
	}
}
