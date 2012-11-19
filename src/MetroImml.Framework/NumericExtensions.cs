using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroImml.Framework
{
    public static class NumericExtensions
    {
        public static SharpDX.Matrix ToSharpDxMatrix(this Imml.Numerics.Matrix4 immlMatrix)
        {
            return new SharpDX.Matrix(immlMatrix.ToArray());
        }

        public static SharpDX.Vector3 ToSharpDxVector(this Imml.Numerics.Vector3 immlVector)
        {
            return new SharpDX.Vector3(immlVector.X, immlVector.Y, immlVector.Z);
        }
    }
}
