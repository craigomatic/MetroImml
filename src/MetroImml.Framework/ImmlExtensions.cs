using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imml;
using Imml.Drawing;
using Imml.Scene.Container;
using Imml.Scene.Controls;

namespace MetroImml.Framework
{
    public static class ImmlExtensions
    {
        public static Color3 GetBackgroundColorOrDefault(this ImmlDocument document, Color3 defaultValue)
        {
            if(string.IsNullOrWhiteSpace(document.Background))
            {
                return defaultValue;
            }

            Background backgroundElement = null;

            if (document.TryGetElementByName<Background>(document.Background, out backgroundElement))
            {
                return backgroundElement.Colour;
            }

            throw new MarkupException("Document does not contain the specified background element");
        }

        public static SharpDX.Matrix GetCameraMatrixOrDefault(this ImmlDocument document, SharpDX.Matrix defaultValue)
        {
            if (string.IsNullOrWhiteSpace(document.Camera))
            {
                return defaultValue;
            }

            Camera cameraElement = null;

            if (document.TryGetElementByName<Camera>(document.Camera, out cameraElement))
            {
                return cameraElement.Matrix.ToSharpDxMatrix();
            }

            throw new MarkupException("Document does not contain the specified camera element");
        }

        public static Camera GetActiveCamera(this ImmlDocument document)
        {
            if (string.IsNullOrWhiteSpace(document.Camera))
            {
                return null;
            }

            Camera cameraElement = null;

            if (document.TryGetElementByName<Camera>(document.Camera, out cameraElement))
            {
                return cameraElement;
            }

            return null;
        }
    }
}
