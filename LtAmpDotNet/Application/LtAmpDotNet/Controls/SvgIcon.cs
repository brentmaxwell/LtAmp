using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LtAmpDotNet.Controls
{
    public class SvgIcon : IconElement
    {
        static SvgIcon()
        {
            AffectsRender<SvgIcon>(IconProperty);
        }

        public static readonly StyledProperty<string?> IconProperty =
            AvaloniaProperty.Register<SvgIcon, string?>(nameof(Icon));

        public static readonly StyledProperty<Geometry?> DataProperty =
            AvaloniaProperty.Register<SvgIcon, Geometry?>(nameof(Data));

        public Geometry? Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public string? Icon
        {
            get => GetValue(IconProperty);
            set
            {
                var uri = new Uri($"avares://LtAmpDotNet/Assets/Icons/{value}.svg");
                if (AssetLoader.Exists(uri))
                {
                    SetValue(IconProperty, value);
                    XmlDocument doc = new();
                    doc.Load(AssetLoader.Open(uri));
                    Data = Geometry.Parse(doc.GetElementsByTagName("path")[0].Attributes["d"].Value);
                }
            } 
        }

        static string FindXPath(XmlNode node)
        {
            StringBuilder builder = new StringBuilder();
            while (node != null)
            {
                switch (node.NodeType)
                {
                    case XmlNodeType.Attribute:
                        builder.Insert(0, "/@" + node.Name);
                        node = ((XmlAttribute)node).OwnerElement;
                        break;
                    case XmlNodeType.Element:
                        int index = FindElementIndex((XmlElement)node);
                        builder.Insert(0, "/" + node.Name + "[" + index + "]");
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.Document:
                        return builder.ToString();
                    default:
                        throw new ArgumentException("Only elements and attributes are supported");
                }
            }
            throw new ArgumentException("Node was not in a document");
        }

        static int FindElementIndex(XmlElement element)
        {
            XmlNode parentNode = element.ParentNode;
            if (parentNode is XmlDocument)
            {
                return 1;
            }
            XmlElement parent = (XmlElement)parentNode;
            int index = 1;
            foreach (XmlNode candidate in parent.ChildNodes)
            {
                if (candidate is XmlElement && candidate.Name == element.Name)
                {
                    if (candidate == element)
                    {
                        return index;
                    }
                    index++;
                }
            }
            throw new ArgumentException("Couldn't find element within parent");
        }
    }
}
