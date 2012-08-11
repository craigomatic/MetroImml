using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml.Scene.Container;
using SharpDX;
using SharpDX.Direct3D11;

namespace MetroImml.Framework
{
    public class DocumentRenderStrategy<T> : IRenderStrategy<T> where T : ImmlDocument
    {
        public T Context { get; set; }

        public void Render(TargetBase target)
        {
            if (this.Context == null)
            {
                return;
            }

            //TODO: render child nodes

            var d3dContext = target.DeviceManager.ContextDirect3D;

            _SetupView(target);

            // Set targets (This is mandatory in the loop)
            d3dContext.OutputMerger.SetTargets(target.DepthStencilView, target.RenderTargetView);

            // Clear the views
            d3dContext.ClearDepthStencilView(target.DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);

            //background colour
            var immlColour = this.Context.GetBackgroundColorOrDefault(Imml.Drawing.Color3.White);
            var bgColour = new Color4(immlColour.R, immlColour.G, immlColour.B, 1);
            d3dContext.ClearRenderTargetView(target.RenderTargetView, bgColour);
        }

        private void _SetupView(TargetBase target)
        {
            //TODO: camera appropriate view code

            //var width = (float)target.RenderTargetSize.Width;
            //var height = (float)target.RenderTargetSize.Height;

            ////setup camera view
            //var view = Matrix.LookAtLH(new Vector3(0, 0, -5), new Vector3(0, 0, 0), Vector3.UnitY);
            //var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, width / (float)height, 0.1f, 100.0f);
            //var viewProj = Matrix.Multiply(view, proj);
        }
    }
}
