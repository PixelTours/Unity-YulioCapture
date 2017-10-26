using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace PTLabs
{
	public enum ImageFormat
	{
		PNG,
		JPG
	}

    public class YulioCaptureWizard : ScriptableWizard
    {

        public Camera targetCamera;
        public float ipd = 0.063f;
        public int faceResolution = 1536;
        public ImageFormat imageFormat = ImageFormat.PNG;

        static string yulioURL = "http://www.yulio.com";

		[MenuItem("Tools/Yulio/Export Yulio Cubemaps")]
		static void CreateWizard()
		{
            ScriptableWizard.DisplayWizard<YulioCaptureWizard>("Export Cubemaps", "Export", "Open Yulio");
		}

		[MenuItem("Tools/Yulio/Go to Yulio.com")]
		static void OpenYulio()
		{
            Application.OpenURL(yulioURL);
		}

        // Validates that there is a camera to render from. 
		void OnWizardUpdate()
		{
			helpString = "Select a camera to generate cubemaps from.";
            if (targetCamera == null)
			{
				errorString = "Please assign an camera";
				isValid = false;
			}
			else
			{
				errorString = "";
				isValid = true;
			}
		}

        // Add YulioCapture to the camera and generate the cubemap. 
		void OnWizardCreate()
		{
            GameObject activeGameObject = targetCamera.gameObject;
            YulioCapture captureComponent = activeGameObject.AddComponent<YulioCapture>();

            // render the texture
            Texture2D textureOutput = captureComponent.RenderCubemapTexture(faceResolution, ipd);

            string fullPath = EditorUtility.SaveFilePanel(string.Format("Save Yulio Cubemap as {0}", imageFormat.ToString()), "", "Yulio Cubemap", imageFormat.ToString().ToLower());

            // write to a nice Yulio-ready file. Yum.
            switch(imageFormat)
            {
                case ImageFormat.PNG:
                    File.WriteAllBytes(fullPath,textureOutput.EncodeToPNG());
                    break;
                case ImageFormat.JPG:
                    File.WriteAllBytes(fullPath, textureOutput.EncodeToJPG());
                    break;
                default:
                    File.WriteAllBytes(fullPath, textureOutput.EncodeToJPG());
                    break;
            }

            // remove the capture component from the camera, leaving it as it was.
            DestroyImmediate(captureComponent);

            // refresh asset database in case cubemap was saved to Unity Project. 
            AssetDatabase.Refresh();
		}

        void OnWizardOtherButton()
        {
            Application.OpenURL(yulioURL);
        }



    }
}
