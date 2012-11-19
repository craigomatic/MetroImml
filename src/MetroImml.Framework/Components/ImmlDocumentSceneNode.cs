using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml;
using Imml.ComponentModel;
using SharpDX;
using SharpDX.Direct3D11;
using Imml.Scene.Container;

namespace MetroImml.Framework.Components
{
    public class ImmlDocumentSceneNode : Component, ISceneNode
    {
        private ImmlDocument _Context;

        public void Initialise(IImmlElement context, DeviceManager deviceManager)
        {
            _Context = context as ImmlDocument;
        }

        public void Render(TargetBase target, TargetView targetView)
        {
            //background colour
            var immlColour = _Context.GetBackgroundColorOrDefault(Imml.Drawing.Color3.White);
            var bgColour = new Color4(immlColour.R, immlColour.G, immlColour.B, 1);
            target.DeviceManager.ContextDirect3D.ClearRenderTargetView(target.RenderTargetView, bgColour);


            var camera = _Context.GetActiveCamera();

            if (camera != null)
            {
                //TODO: incorporate the requested rotation of the camera into this matrix
                targetView.View = Matrix.LookAtLH(camera.WorldPosition.ToSharpDxVector(), Vector3.UnitZ, Vector3.UnitY);
            }
        }
    }
}
