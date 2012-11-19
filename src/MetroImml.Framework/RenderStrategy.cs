using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml.Scene.Container;
using SharpDX;
using SharpDX.Direct3D11;
using System.Collections.Concurrent;
using Imml;
using MetroImml.Framework.Components;

namespace MetroImml.Framework
{
    public class RenderStrategy : IRenderStrategy
    {
        public TargetView TargetView { get; set; }

        private List<ISceneNode> _Nodes;

        public RenderStrategy()
        {
            this.TargetView = new TargetView();

            _Nodes = new List<ISceneNode>();
        }

        public void Render(TargetBase target)
        {
            if (_Nodes.Count == 0)
            {
                return;
            }

            var d3dContext = target.DeviceManager.ContextDirect3D;

            d3dContext.OutputMerger.SetTargets(target.DepthStencilView, target.RenderTargetView);
            d3dContext.ClearDepthStencilView(target.DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
            
            foreach (var node in _Nodes)
            {
                node.Render(target, this.TargetView);
            }                       
        }      

        public void Add(ISceneNode node)
        {
            _Nodes.Add(node);
        }

        public void Remove(ISceneNode node)
        {
            _Nodes.Remove(node);
        }

        public void Clear()
        {
            _Nodes.Clear();
        }
    }
}
