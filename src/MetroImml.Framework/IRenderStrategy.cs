using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDX;
using Imml;
using Imml.ComponentModel;

namespace MetroImml.Framework
{
    public interface IRenderStrategy<T> where T : IImmlElement
    {
        T Context { get; set; }

        void Render(TargetBase target);
    }
}
