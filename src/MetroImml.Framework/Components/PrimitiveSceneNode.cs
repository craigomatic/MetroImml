using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml.Scene.Controls;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.IO;
using Imml;

namespace MetroImml.Framework.Components
{
    public class PrimitiveSceneNode : Component, ISceneNode
    {
        private Primitive _Context;

        private Buffer _ConstantBuffer;
        private InputLayout _InputLayout;
        private VertexBufferBinding _VertexBufferBinding;
        private VertexShader _VertexShader;
        private PixelShader _PixelShader;

        public void Initialise(IImmlElement context, DeviceManager deviceManager)
        {
            _Context = context as Primitive;
            
            RemoveAndDispose(ref _ConstantBuffer);

            var path = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;

            var vertexShaderByteCode = NativeFile.ReadAllBytes(System.IO.Path.Combine(path, "MetroImml.Framework\\Resources\\Shaders\\MiniCube_VS.fxo"));
            _VertexShader = new VertexShader(deviceManager.DeviceDirect3D, vertexShaderByteCode);

            _PixelShader = new PixelShader(deviceManager.DeviceDirect3D, NativeFile.ReadAllBytes(System.IO.Path.Combine(path, "MetroImml.Framework\\Resources\\Shaders\\MiniCube_PS.fxo")));

            // Layout from VertexShader input signature
            _InputLayout = new InputLayout(deviceManager.DeviceDirect3D, vertexShaderByteCode, new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
            });

            // Instantiate Vertex buffer from vertex data
            var vertices = SharpDX.Direct3D11.Buffer.Create(deviceManager.DeviceDirect3D, BindFlags.VertexBuffer, new[]
            {
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f), // Front
                new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f), // BACK
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f), // Top
                new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),

                new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f), // Bottom
                new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f), // Left
                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),

                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f), // Right
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
            });

            _VertexBufferBinding = new VertexBufferBinding(vertices, Utilities.SizeOf<Vector4>() * 2, 0);

            //load the resources required by this type of primitive
            _ConstantBuffer = ToDispose(new Buffer(deviceManager.DeviceDirect3D, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0));

            _Stopwatch = new System.Diagnostics.Stopwatch();
            _Stopwatch.Start();
        }

        private System.Diagnostics.Stopwatch _Stopwatch;

        public void Render(TargetBase target, TargetView targetView)
        {
            if (!_Context.IsVisible)
            {
                return;
            }

            var d3dContext = target.DeviceManager.ContextDirect3D;

            var view = targetView.View;
            var proj = Matrix.PerspectiveFovLH(targetView.FieldOfView, (float)target.RenderTargetSize.Width / (float)target.RenderTargetSize.Height, targetView.NearPlane, targetView.FarPlane);
            var viewProj = Matrix.Multiply(view, proj);

            var time = (float)(_Stopwatch.ElapsedMilliseconds / 1000f);

            var scale = _Context.WorldScale.ToSharpDxVector();
            var rotation = _Context.WorldRotation.ToSharpDxVector();
            var translation = _Context.WorldPosition.ToSharpDxVector();

            var worldMatrix = Matrix.Scaling(scale) * Matrix.RotationYawPitchRoll(rotation.Y, rotation.X, rotation.Z) * Matrix.Translation(translation) * viewProj;
            worldMatrix.Transpose();

            // Setup the pipeline
            d3dContext.InputAssembler.SetVertexBuffers(0, _VertexBufferBinding);
            d3dContext.InputAssembler.InputLayout = _InputLayout;
            d3dContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            d3dContext.VertexShader.SetConstantBuffer(0, _ConstantBuffer);
            d3dContext.VertexShader.Set(_VertexShader);
            d3dContext.PixelShader.Set(_PixelShader);

            d3dContext.UpdateSubresource(ref worldMatrix, _ConstantBuffer, 0);
            d3dContext.Draw(36, 0);
        }
    }
}
