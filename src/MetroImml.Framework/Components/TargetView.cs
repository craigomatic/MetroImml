using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroImml.Framework.Components
{
    public class TargetView : BindableBase
    {
        /// <summary>
        /// Gets or sets the field of view.
        /// </summary>
        /// <value>
        /// The field of view.
        /// </value>
        public float FieldOfView { get; set; }

        /// <summary>
        /// Gets or sets the near plane.
        /// </summary>
        /// <value>
        /// The near plane.
        /// </value>
        public float NearPlane { get; set; }

        /// <summary>
        /// Gets or sets the far plane.
        /// </summary>
        /// <value>
        /// The far plane.
        /// </value>
        public float FarPlane { get; set; }

        /// <summary>
        /// Gets or sets the aspect.
        /// </summary>
        /// <value>
        /// The aspect.
        /// </value>
        public float Aspect { get; set; }


        private Matrix _View;

        /// <summary>
        /// Gets or sets the view matrix.
        /// </summary>
        /// <value>
        /// The view matrix.
        /// </value>
        public Matrix View
        {
            get { return _View; }
            set { base.SetProperty(ref _View, value); }
        }

        public TargetView()
        {
            this.FieldOfView = 45;
            this.NearPlane = 0.1f;
            this.FarPlane = 1000f;
            this.View = Matrix.Identity;
        }

    }
}
