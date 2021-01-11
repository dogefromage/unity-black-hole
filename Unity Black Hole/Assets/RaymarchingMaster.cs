using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class RaymarchingMaster : MonoBehaviour
{
    // SHADER
    public ComputeShader RaymarchingShader;
    private RenderTexture _target;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        SetShaderParameters();
        Render(destination);
    }
    private void Render(RenderTexture destination)
    {
        // Make sure we have a current render target
        InitRenderTexture();
        // Set the target and dispatch the compute shader
        RaymarchingShader.SetTexture(0, "Result", _target);
        int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
        RaymarchingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);
        // Blit the result texture to the screen
        Graphics.Blit(_target, destination);
    }
    private void InitRenderTexture()
    {
        if (_target == null || _target.width != Screen.width || _target.height != Screen.height)
        {
            // Release render texture if we already have one
            if (_target != null)
                _target.Release();
            // Get a render target for Ray Tracing
            _target = new RenderTexture(Screen.width, Screen.height, 0,
                RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            _target.enableRandomWrite = true;
            _target.Create();
        }
    }

    // CAMERA
    private Camera _camera;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    private void SetShaderParameters()
    {
        RaymarchingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
        RaymarchingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
    }
}