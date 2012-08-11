using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imml;
using Imml.Drawing;
using Imml.Scene.Container;
using Imml.Scene.Controls;

namespace MetroImml.Framework
{
    public static class ImmlExtensions
    {
        public static Color3 GetBackgroundColorOrDefault(this ImmlDocument document, Color3 defaultValue)
        {
            if(string.IsNullOrWhiteSpace(document.Background))
            {
                return defaultValue;
            }

            Background backgroundElement = null;

            if (document.TryGetElementByName<Background>(document.Background, out backgroundElement))
            {
                return backgroundElement.Colour;
            }

            throw new MarkupException("Document does not contain the specified background element");
        }
    }
}
