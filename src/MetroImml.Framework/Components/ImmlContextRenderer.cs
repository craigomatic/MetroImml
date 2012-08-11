using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml.ComponentModel;
using SharpDX;
using SharpDX.Direct3D11;

namespace MetroImml.Framework.Components
{
    public class ImmlContextRenderer<T> : ISceneNode<T> where T : IImmlContext
    {
        public T Context { get; private set; }

        private IRenderStrategy<T> _RenderStrategy;

        private object _RenderLock;

        public ImmlContextRenderer(IRenderStrategy<T> renderStrategy)
        {
            _RenderStrategy = renderStrategy;

            _RenderLock = new object();
        }

        public void Initialise(T context)
        {
            lock (_RenderLock)
            {
                //TODO: Load the media associated with the elements
                this.Context = context;
                _RenderStrategy.Context = context;
            }
        }

        public void Render(TargetBase target)
        {
            lock (_RenderLock)
            {
                _RenderStrategy.Render(target);
            }
        }

        public void Dispose()
        {
            //TODO: clean up resources
        }
    }
}
