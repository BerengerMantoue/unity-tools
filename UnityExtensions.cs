/********************************************************************************************
 * Author : Mantoue Bérenger
 * Version : 1.0
 * Date : February 2014
 * Description : Extension of Unity's components.
 * ******************************************************************************************/

using UnityEngine;
using System.Collections;

/// <summary>
/// Extension of Unity's GameObject class.
/// </summary>
public static class GameObjectExtension
{
    #region iTween
    /// <summary>
    /// Smoothly moves toward a position.
    /// </summary>
    /// <param name="obj">Object to move.</param>
    /// <param name="pos">Position to reach.</param>
    /// <param name="duration">Duration of the tween.</param>
    /// <param name="onCompleteFunction">Name of the function called on complete. Must be implemented by a composent attached to obj.</param>
    public static void TweenPosition(this GameObject obj, Vector3 pos, float duration, string onCompleteFunction = "")
    {
        iTween.MoveTo(obj, iTween.Hash(iT.MoveTo.position, pos, iT.MoveTo.time, duration, iT.MoveTo.easetype, iTween.EaseType.easeInOutCubic,
                                       iT.MoveTo.oncomplete, onCompleteFunction));
    }

    /// <summary>
    /// Smoothly rotate toward a quat.
    /// </summary>
    /// <param name="obj">Object to rotate.</param>
    /// <param name="to">Quaternion to rotate to.</param>
    /// <param name="duration">Duration of the tween.</param>
    public static void TweenRotation(this GameObject obj, Quaternion to, float duration)
    {
        iTween.RotateTo(obj, iTween.Hash(iT.RotateTo.rotation, to.eulerAngles, iT.RotateTo.time, duration, iT.RotateTo.easetype, iTween.EaseType.easeInOutCubic));
    }

    /// <summary>
    /// Smootly lerps a float between a from and to values.
    /// </summary>
    /// <param name="obj">Object used for the tween.</param>
    /// <param name="from">Start value of tween.</param>
    /// <param name="to">End value of the tween.</param>
    /// <param name="duration">Duration of the tween.</param>
    /// <param name="delay">Delay before starting the tween.</param>
    /// <param name="onUpdateFunction">Name of the function called on update. Must be implemented by a composent attached to obj.</param>
    /// <param name="onCompleteFunction">Name of the function called on complete. Must be implemented by a composent attached to obj.</param>
    /// <param name="name">Name of the tween (to stop it by name).</param>
    public static void TweenValue(this GameObject obj, float from, float to, float duration, float delay, string onUpdateFunction, string onCompleteFunction = "", string name = "")
    {
        iTween.ValueTo(obj, iTween.Hash(iT.ValueTo.from, from, iT.ValueTo.to, to, iT.ValueTo.time, duration, iT.ValueTo.delay, delay, iT.ValueTo.looptype, iT.LoopType.none,
                                                iT.ValueTo.easetype, iTween.EaseType.easeInOutCubic, iT.ValueTo.onupdate, onUpdateFunction, iT.ValueTo.oncomplete, onCompleteFunction, "name", name));
    }

    /// <summary>
    /// Stop every tweens on an object.
    /// </summary>
    /// <param name="obj">Objects to stop the tweens on.</param>
    public static void StopTweens(this GameObject obj)
    {
        iTween.Stop(obj);
    }
    #endregion

    /// <summary>
    /// Set the layer on an object
    /// </summary>
    /// <param name="obj">Object to set the layer on.</param>
    /// <param name="layerName">Name of the layer.</param>
    public static void SetLayer(this GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);
    }

    /// <summary>
    /// Set the layer on an object and its children recursively.
    /// </summary>
    /// <param name="obj">Object to set the layer on.</param>
    /// <param name="layerName">Name of the layer.</param>
    public static void SetLayerRecursively(this GameObject obj, string layerName)
    {
        foreach (Transform child in obj.transform)
            child.gameObject.SetLayerRecursively(layerName);

        obj.SetLayer(layerName);
    }

    /// <summary>
    /// Check if an object has the right layer.
    /// </summary>
    /// <param name="obj">Object to check the layer on.</param>
    /// <param name="layerName">Name of the layer to check.</param>
    /// <returns>True if obj has the layer "layerName".</returns>
    public static bool IsLayer(this GameObject obj, string layerName)
    {
        return obj.layer == LayerMask.NameToLayer(layerName);
    }
}

/// <summary>
/// Extension of Unity's Transform class.
/// </summary>
public static class TransformExtension
{
    /// <summary>
    /// Transform a world point into a point in t's local space
    /// </summary>
    public static Vector3 WorldToLocalPosition(this Transform t, Vector3 worldPos) { return t.InverseTransformPoint(worldPos); }

    /// <summary>
    /// Transform a world direction into a direction in t's local space
    /// </summary>
    public static Vector3 WorldToLocalDirection(this Transform t, Vector3 worldPos) { return t.InverseTransformDirection(worldPos); }

    /// <summary>
    /// Transform a point from t's local space into a point in world space
    /// </summary>
    public static Vector3 LocalToWorldPosition(this Transform t, Vector3 worldPos) { return t.TransformPoint(worldPos); }

    /// <summary>
    /// Transform a direction from t's local space into a direction in world space
    /// </summary>
    public static Vector3 LocalToWorldDirection(this Transform t, Vector3 worldPos) { return t.TransformDirection(worldPos); }


    public static Vector3 Center(this Transform t)
    {
        Renderer r = t.renderer;
        if (r != null)
            return r.Center();
        else
            return t.position;
    }
}

/// <summary>
/// Extension of Unity's Renderer class.
/// </summary>
public static class RendererExtension
{
    /// <summary>
    /// Get the center of an object's bounds.
    /// </summary>
    /// <param name="r">Renderer to get the center of.</param>
    /// <returns>The center of an object's bounds.</returns>
    public static Vector3 Center(this Renderer r)
    {
        return r.bounds.center;
    }
}

public static class CameraExtension
{
    public static Ray MouseRay(this Camera cam)
    {
        return cam.ScreenPointToRay(Input.mousePosition);
    }

    public static Vector3 MouseWorldPos(this Camera cam)
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}