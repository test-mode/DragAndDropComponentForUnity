# DragAndDropComponentForUnity
A component for Unity GameObject that allows for "drag and drop" feature in 2D touchscreen games

This is a component that enables a "drag and drop" feature in Unity 2D projects that take advantage of touchscreen controls.
It should be attached to a gameobject with a Collider2D component that will use the drag and drop mechanic.
The script acts as a component and should not be modified, but rather extended. It is designed to be able to acquire more feature by extending it with inheritance.
The activation or deactivation of this script should be done using the "DraggingEnabled" property.
It features three Action events that get fired when the GameObject that this script is first touched ("OnGrabbed"), is being dragged ("OnDragging"), and when released ("OnReleased").
The script also features freezing the dragging direction with "FreezeX" and "FreezeY" properties.
