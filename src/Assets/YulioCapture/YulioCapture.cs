using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace PTLabs
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class YulioCapture : MonoBehaviour
    {
        bool isRendering = false;

        Camera captureCamera;

        public Texture2D RenderCubemapTexture(int _faceResolution, float _ipd)
        {
            isRendering = true;
            captureCamera = GetComponent<Camera>();
            int x = 0;
            int y = 0;

            // order of the cubemap faces for Yulio intake. 
            CubemapFace[] facesToRender = new CubemapFace[] {
                CubemapFace.NegativeX, CubemapFace.PositiveZ,
                CubemapFace.PositiveX, CubemapFace.NegativeZ,
                CubemapFace.NegativeY, CubemapFace.PositiveY,
            };

            Cubemap leftEye = new Cubemap(_faceResolution, TextureFormat.RGB24, false);
            Cubemap rightEye = new Cubemap(_faceResolution, TextureFormat.RGB24, false);

            // create a texture for 6x2 cubemap faces
            var textureOutput = new Texture2D(leftEye.width * 6, leftEye.height * 2, TextureFormat.RGB24, false);

            // renders each eye to a cubemap. 
            Vector3 startingPos = captureCamera.transform.position;
            captureCamera.RenderToCubemap(leftEye);
            captureCamera.transform.Translate((new Vector3(-_ipd, 0, 0)));
            captureCamera.RenderToCubemap(rightEye);

            // draw the right eye to the texture
            for (int i = 0; i < 6; i++)
            {
                Color[] pixels = rightEye.GetPixels(facesToRender[i]);
                Color[] sourcePixels = new Color[pixels.Length];
                for (int ypos = 0; ypos < _faceResolution; ypos++)
                {
                    for (int xpos = 0; xpos < _faceResolution; xpos++)
                    {
                        sourcePixels[ypos * _faceResolution + xpos] = pixels[((_faceResolution - 1 - ypos) * _faceResolution) + xpos];
                    }
                }
                // Copy them to the dest texture
                textureOutput.SetPixels(x, y, _faceResolution, _faceResolution, sourcePixels);

                // move to the next face
                x += _faceResolution;
            }

            // reset x and move to the next row
            x = 0;
            y += _faceResolution;

            // draw the left eye
            for (int i = 6; i < 12; i++)
            {
                Color[] pixels = leftEye.GetPixels(facesToRender[i - 6]);
                Color[] sourcePixels = new Color[pixels.Length];
                for (int ypos = 0; ypos < _faceResolution; ypos++)
                {
                    for (int xpos = 0; xpos < _faceResolution; xpos++)
                    {
                        sourcePixels[ypos * _faceResolution + xpos] = pixels[((_faceResolution - 1 - ypos) * _faceResolution) + xpos];
                    }
                }
                // Copy them to the dest texture
                textureOutput.SetPixels(x, y, _faceResolution, _faceResolution, sourcePixels);

                // move to the next face
                x += _faceResolution;
            }

            DestroyImmediate((leftEye));
            DestroyImmediate(rightEye);
            isRendering = false;

            return textureOutput;
        }
    }
}

