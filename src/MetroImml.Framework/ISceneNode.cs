using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml;

namespace MetroImml.Framework
{
    public interface ISceneNode<T> : IDisposable where T : IImmlElement
    {        
        void Initialise(T context);

        void Render(TargetBase target);
    }
}