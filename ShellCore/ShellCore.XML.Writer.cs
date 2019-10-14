using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

// ShellCore XML Writer
namespace Shell.Core
{
    partial class ShellCore
    {
		public XmlDocument XmlCreateFile(string path)
        {
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                FileStream fileStream = new FileStream(path, FileMode.Create);
                XmlDocument.Save(fileStream);
                return XmlDocument;
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return null;
            }

            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return null; // we failed
            }

            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return null;
            }

            catch (IOException)
            {
                ElmThrowException(29);
                return null;
            }
        }

        public XmlDocument XmlAddNode(XmlDocument xmlDocument, string name, string text = null) // Adds a node to an XmlDocument.
        {
            XmlNode xmlNode = xmlDocument.CreateElement(name);
            try
            {
                xmlNode = xmlDocument.AppendChild(xmlNode);
                if (text != null)
                {
                    xmlNode.AppendChild(xmlDocument.CreateTextNode(text));
                }
                return xmlDocument;
            }
            catch (XmlException)
            {
                ElmThrowException(71);
                return null;
            }

        }

        public XmlDocument XmlAddNode(XmlDocument xmlDocument, XmlNode theNode, string name, string text = null, bool? c72204635772 = null) // Adds a node to an XmlDocument.
        {
			XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, name, String.Empty); // create the node.
            if (text != null)
            {
                xmlNode.InnerText = text;
            }

            theNode.AppendChild(xmlNode);
            return xmlDocument;
        }

        public XmlDocument XmlRemoveNode(XmlDocument xmlDocument, XmlNode nodeToRemove) // Removes a node from an XmlDocument.
        {

            try
            {
                xmlDocument.RemoveChild(nodeToRemove);
                return xmlDocument;
            }
			catch (ArgumentException)
            {
                ElmThrowException(70);
                return null;
            }

        }

		public XmlDocument XmlAddAttribute(XmlDocument xmlDocument, XmlNode nodeToModify, string attributeName, string attributeValue)
        {

            XmlAttribute xmlAttribute = xmlDocument.CreateAttribute(attributeName);

			if (attributeValue == null)
            {
                ElmThrowException(68);
            }

            xmlAttribute.Value = attributeValue;

            nodeToModify.Attributes.Append(xmlAttribute);
            return xmlDocument;
        }

        public XmlDocument XmlRemoveAttribute(XmlDocument xmlDocument, XmlNode nodeToModify, string attributeNameToRemove) // removes an attribute
        {

            XmlAttributeCollection attributes = nodeToModify.Attributes;

            if (attributes.Count == 0)
            {
                ElmThrowException(63);
            }

			foreach (XmlAttribute attribute in attributes)
            {
				if (attribute.Name == attributeNameToRemove)
                {
                    nodeToModify.Attributes.Remove(attribute); // annihilate the attribute
                    return xmlDocument;
                }
				else
                {
                    ElmThrowException(69);
                    return null;
                }
            }
            return null;

        }

        public XmlDocument XmlSetText(XmlDocument xmlDocument, XmlNode nodeToModify, string text)
        {
            XmlNodeList xmlChildren = xmlDocument.ChildNodes;

			foreach (XmlNode xmlChild in xmlChildren)
            {
				if (xmlChild == nodeToModify)
                {
                    xmlChild.InnerText = text;
                    return xmlDocument;
                }
                else
                {
                    ElmThrowException(70);
                    return null;
                }
            }
            return null;
        }

        public XmlDocument XmlSetText(XmlDocument xmlDocument, XmlNode parentNode, XmlNode nodeToModify, string text)
        {
            XmlNodeList xmlChildren = parentNode.ChildNodes;

            foreach (XmlNode xmlChild in xmlChildren)
            {
                if (xmlChild == nodeToModify)
                {
                    xmlChild.InnerText = text;
                    return xmlDocument;
                }
                else
                {
                    ElmThrowException(70);
                    return null;
                }
            }
            return null;
        }

        public XmlDocument XmlSaveFile(XmlDocument xmlDocument, string path)
        {
            try
            {
                DeleteFileEx(path); // this is lazy
                FileStream fileStream = new FileStream(path , FileMode.Create);
                xmlDocument.Save(fileStream);
                return xmlDocument;
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return null;
            }

            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return null; // we failed
            }

            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return null;
            }

            catch (IOException)
            {
                ElmThrowException(29);
                return null;
            }
        }


    }
}
