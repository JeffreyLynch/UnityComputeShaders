using System;
using UnityEngine;

public class AssignTexture : MonoBehaviour
{
  public ComputeShader shader;
  public int texResolution = 256;

  private Renderer rend;
  private RenderTexture outputTexture;
  private int kernelHandle;
  private static readonly int MainTex = Shader.PropertyToID("_MainTex");

  private void Start()
  {
    outputTexture = new RenderTexture(texResolution, texResolution, 0);
    outputTexture.enableRandomWrite = true;
    outputTexture.Create();

    rend = GetComponent<Renderer>();
    rend.enabled = true;
    InitShader();
  }

  private void DispatchShader(int x, int y)
  {
    shader.Dispatch(kernelHandle, x, y,1);
  }

  private void InitShader()
  {
    kernelHandle = shader.FindKernel("CSMain");
    shader.SetTexture(kernelHandle, "Result", outputTexture);
    rend.material.SetTexture(MainTex, outputTexture);
    
    DispatchShader(texResolution/4, texResolution/4);
  }

  private void Update()
  {
    if (Input.GetKeyUp(KeyCode.U))
    {
      DispatchShader(texResolution/8, texResolution/8);
    }
  }
}
