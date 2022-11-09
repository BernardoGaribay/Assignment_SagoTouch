namespace  AssignmentExample
{
	using UnityEngine;
	using SagoTouch;
	using Touch = SagoTouch.Touch;

	public class ExampleButton : MonoBehaviour {
		
		#region Fields

		[System.NonSerialized]
		private TouchArea m_TouchArea;

		[System.NonSerialized]
		private TouchAreaObserver m_TouchAreaObserver;

		#endregion

		#region Properties

		public TouchArea TouchArea {
			get { return m_TouchArea = m_TouchArea ?? GetComponent<TouchArea>(); }
		}

		public TouchAreaObserver TouchAreaObserver {
			get { return m_TouchAreaObserver = m_TouchAreaObserver ?? GetComponent<TouchAreaObserver>(); }
		}

		#endregion


		#region Methods

		private void OnEnable() {
			this.TouchAreaObserver.TouchUpDelegate = OnTouchUp;
		}

		private void OnDisable() {
			this.TouchAreaObserver.TouchUpDelegate = null;
		}

		public void OnTouchUp(TouchArea touchArea, Touch touch) {
			Debug.Log("Touch Up!", this);
		}

		private void DisableTouchInput() {
			this.TouchArea.enabled = false;
		}

		private void EnableTouchInput() {
			this.TouchArea.enabled = true;
		}

		// it is suggested to either delay the re-enabling of the TouchArea 
		//or to skip disabling it if it is known it will be re-enabled in the same frame
		//it is known to cause issues without this workaround
		private void DisableAllTouchInput() {
			if (TouchDispatcher.Instance) {
				TouchDispatcher.Instance.enabled = false;
			}
		}

		private void EnableAllTouchInput() {
			if (TouchDispatcher.Instance) {
				TouchDispatcher.Instance.enabled = true;
			}
		}

		#endregion

	}
}


