namespace AssignmentExample {

	using UnityEngine;
	using System.Collections.Generic;
	using SagoTouch;
	using Touch = SagoTouch.Touch;
	using SagoNewton;

	public class ExampleObserver : MonoBehaviour, ISingleTouchObserver {

		#region Fields

		[System.NonSerialized]
		private Camera m_Camera;

		[System.NonSerialized]
		private Renderer m_Renderer;

		[System.NonSerialized]
		private List<Touch> m_Touches;

		[System.NonSerialized]
		private Transform m_Transform;

		[System.NonSerialized]
		private Moveable m_Moveable;

		#endregion


		#region Properties

		public Camera Camera {
			get { return m_Camera = m_Camera ?? CameraUtils.FindRootCamera(this.Transform);}
		}

		public Renderer Renderer {
			get { return m_Renderer = m_Renderer ?? GetComponent<Renderer>(); }
		}

		public Transform Transform {
			get { return m_Transform = m_Transform ?? GetComponent<Transform>(); }
		}

		public List<Touch> Touches {
			get { return m_Touches = m_Touches ?? new List<Touch>();}
		}

		public Moveable Moveable {
			get { return m_Moveable = m_Moveable ?? GetComponent<Moveable>(); }
		}

		#endregion


		#region Methods

		private void OnEnable() {
			if (TouchDispatcher.Instance){
				TouchDispatcher.Instance.Add(this, 0, true);
			}
		}

		private void OnDisable() {
			if (TouchDispatcher.Instance){
				TouchDispatcher.Instance.Remove(this);
			}
		}

		private bool HitTest(Touch touch) {
			var bounds = this.Renderer.bounds;
			bounds.extents += Vector3.forward;
			return bounds.Contains(CameraUtils.TouchToWorldPoint(touch, this.Transform, this.Camera));
		}

		#endregion


		#region ISingleTouchObserver

		public bool OnTouchBegan(Touch touch) {
			if (HitTest(touch)) {
				this.Touches.Add(touch);
				return true;
			}
			return false;
		}

		public void OnTouchMoved(Touch touch) {
			this.Moveable.Position = CameraUtils.TouchToWorldPoint(touch, this.Transform, this.Camera);
		}

		public void OnTouchEnded(Touch touch) {
			this.Touches.Remove(touch);
		}

		//Important to always implement OnTouchCancelled, otherwise observer may look like it stopped responding to touches (not clearing m_Touch)
		public void OnTouchCancelled(Touch touch) {
			OnTouchEnded(touch);
		}

		#endregion
		
	}
}

