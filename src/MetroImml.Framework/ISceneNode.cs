using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml;
using MetroImml.Framework.Components;

namespace MetroImml.Framework
{
    public interface ISceneNode : IDisposable
    {        
        void Initialise(IImmlElement context, DeviceManager deviceManager);

        void Render(TargetBase target, TargetView targetView);
    }
}