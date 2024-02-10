using System;
using UnityEngine;

namespace Upily.Games.MonkeyWantsBanana.DragAndDrop
{
    [RequireComponent(typeof(Collider2D))]
    public class Dragger : MonoBehaviour
    {
        // Events
        /// <summary>
        /// Invoked once when the object is first touched.
        /// </summary>
        public event Action OnGrabbed;

        /// <summary>
        /// Invoked every FixedUpdate cycle when the object is being dragged.
        /// </summary>
        public event Action OnDragging;

        /// <summary>
        /// Invoked once when the object is released.
        /// </summary>
        public event Action OnReleased;

        // Settings
        [SerializeField, Tooltip("Enables/disables dragging on this gameobject")] private bool _draggingEnabled = true;

        /// <summary>
        /// Activates or deactivates dragging based on the given true/false value.
        /// Can be switched any time during gameplay.
        /// </summary>
        public bool DraggingEnabled
        {
            get { return _draggingEnabled; }
            set { _draggingEnabled = value; }
        }

        [Header("Freeze Position")]
        [SerializeField, Tooltip("Constrain position in X axis")] private bool _x;
        [SerializeField, Tooltip("Constrain position in Y axis")] private bool _y;

        /// <summary>
        /// If set to true, constrains movement in X axis.
        /// Can be switched any time during gameplay.
        /// </summary>
        public bool FreezeX
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// If set to true, constrains movement in Y axis.
        /// Can be switched any time during gameplay.
        /// </summary>
        public bool FreezeY
        {
            get { return _y; }
            set { _y = value; }
        }

        // Private settings
        private Camera _camera;
        private Vector3 _offset;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnMouseDown()
        {
            if (_draggingEnabled)
            {
                StartDrag();
                OnGrabbed?.Invoke();
            }
        }

        private void OnMouseDrag()
        {
            if (_draggingEnabled)
            {
                Drag();
                OnDragging?.Invoke();
            }
        }

        private void OnMouseUp()
        {
            if (_draggingEnabled)
            {
                EndDrag();
                OnReleased?.Invoke();
            }
        }

        /// <summary>
        /// Called once when the object is first touched.
        /// </summary>
        protected virtual void StartDrag()
        {
            Vector2 initialCoordinates = new(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            _offset = transform.position - _camera.ScreenToWorldPoint(initialCoordinates);
        }

        /// <summary>
        /// Called every FixedUpdate cycle when the object is being dragged.
        /// </summary>
        protected virtual void Drag()
        {
            Vector2 coordinates = new(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            Vector3 newPosition = _camera.ScreenToWorldPoint(coordinates) + _offset;
            if (_x) newPosition.x = transform.position.x;
            if (_y) newPosition.y = transform.position.y;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }

        /// <summary>
        /// Called once when the object is released.
        /// </summary>
        protected virtual void EndDrag()
        {
            _offset = Vector3.zero;
        }
    }
}
