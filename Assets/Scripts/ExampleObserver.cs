using UnityEngine;
using SagoTouch;
using Touch = SagoTouch.Touch;

public class ExampleObserver : MonoBehaviour, ISingleTouchObserver {

    #region Fields
    [System.NonSerialized]
    private Camera m_Camera;

    [System.NonSerialized]
    private Renderer m_Renderer;

    [System.NonSerialized]
    private Transform m_Transform;
    #endregion

    public Camera Camera {
        get { return m_Camera = m_Camera ?? CameraUtils.FindRootCamera(this.Transform);}
    }

    public Renderer Renderer {
        get { return m_Renderer = m_Renderer ?? GetComponent<Renderer>(); }
    }

    public Transform Transform {
        get { return m_Transform = m_Transform ?? GetComponent<Transform>(); }
    }
        
    public bool OnTouchBegan(Touch touch) {
        // ...
        return false;
    }
    
    public void OnTouchMoved(Touch touch) {
        // ...
    }
    
    public void OnTouchEnded(Touch touch) {
        // ...
    }
    
    public void OnTouchCancelled(Touch touch) {
        // ...
    }

    private void OnEnable() {
        if(TouchDispatcher.Instance){
            TouchDispatcher.Instance.Add(this, 0, true);
        }
    }

    private void OnDisable() {
        if(TouchDispatcher.Instance){
            TouchDispatcher.Instance.Remove(this);
        }
    }

    private bool HitTest(Touch touch) {
        var bounds = this.Renderer.bounds;
        bounds.extents += Vector3.forward;
        return bounds.Contains(CameraUtils.TouchToWorldPoint(touch, this.Transform, this.Camera));
    }
}

