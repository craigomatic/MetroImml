using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml;
using Imml.ComponentModel;
using MetroImml.Framework.Components;

namespace MetroImml.Framework
{
    public interface IRenderStrategy
    {
        TargetView TargetView { get; set; }

        void Render(TargetBase target);

        void Add(ISceneNode node);
        
        void Remove(ISceneNode node);

        void Clear();
    }
}
