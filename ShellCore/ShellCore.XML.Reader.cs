using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

// ShellCore XML Services
// This may be shitty. Consider some form of rewrite
namespace Shell.Core
{
    partial class ShellCore
    {
        public XmlDocument XmlOpenFile(string path)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                return XmlDoc;
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(13);
                return null;
            }

            catch (DirectoryNotFoundException)
            {
                ElmThrowException(13);
                return null;
            }
            catch (XmlException ExceptionInformation)
            {
                ElmThrowException(10, ExceptionInformation.ToString()); // convert the exception information to a string and throw
                return null;
            }

        }

        public XmlNode XmlGetRootNode(XmlDocument document, bool checkName = false, string nameOfNodeToVerify = null, int? exceptionIdToThrow = null)
        {
            XmlNode XmlRootNode;
            XmlNodeList theKids = document.ChildNodes;
            // if its empty throw an error

            if (theKids.Count == 0)
            {
                ElmThrowException(59);
                return null;
            }

            XmlRootNode = theKids[0];
            foreach (XmlNode node in theKids)
            {
                while (node.Name == "#comment" | node.Name == "?xml")
                {
                    XmlRootNode = node.NextSibling;

                }

                // we're assuming here that the logic above here has selected the right node. otherwise, we are fucking idiots.

                if (checkName == true)
                {
                    if (nameOfNodeToVerify == null)
                    {
                        if (exceptionIdToThrow == null)
                        {
                            ElmThrowException(60); // default
                        }
                        else
                        {
                            ElmThrowException((int)exceptionIdToThrow); // convert from nullable int to int and throw
                        }
                         // Whoever called this method is an idiot.
                        return null;
                    }

                    // check and verify the name
                    if (node.Name != nameOfNodeToVerify)
                    {
                        if (exceptionIdToThrow == null)
                        {
                            ElmThrowException(60); // default
                        }
                        else
                        {
                            ElmThrowException((int)exceptionIdToThrow); // convert from nullable int to int and throw
                        }
                        return null;
                    }
                    else
                    {
                        return XmlRootNode; // first node that is NOT a comment.
                    }
                }

            }

            return null;
        }

        public bool XmlCheckForChildNodes(XmlDocument XmlDocument, bool? throwException = null, int? exceptionId = null)
        {
            
            if (!XmlDocument.HasChildNodes)
            {
                if (throwException == true) // do we want to throw an exception if it fails? apparently, in this case, hell yes!
                {
                    if (exceptionId == null)
                    {
                        ElmThrowException(62);
                    }
                    else
                    {
                        ElmThrowException((int)exceptionId);
                    }
                }

                return false;
            }
            else
            {
                return true;
            }

        }

        public XmlNodeList XmlGetChildren(XmlDocument XmlDocument)
        {

            XmlNodeList theNodes = XmlDocument.ChildNodes;

            if (theNodes.Count == 0)
            {
                ElmThrowException(62);
                return null;
            }

            return theNodes;
        }

        public XmlNodeList XmlGetChildren(XmlNode XmlNode)
        {
            XmlNodeList theNodes = XmlNode.ChildNodes;

            if (theNodes.Count == 0)
            {
                ElmThrowException(62);
                return null;
            }

            return theNodes;
        }

        public XmlNode XmlGetNode(XmlDocument XmlDocument, string xmlNodeName)
        {
            XmlNodeList theNodes = XmlDocument.ChildNodes;

            if (theNodes.Count == 0) // loop through all the nodes we got by using ChildNodes
            {
                ElmThrowException(62);
                return null;
            }

            foreach (XmlNode Node in theNodes)
            {
                if (Node.Name == xmlNodeName)
                {
                    return Node;
                }
            }

            ElmThrowException(63);
            return null;

            
        }

        public XmlNode XmlGetNode(XmlNode XmlNode, string xmlNodeName)
        {
            XmlNodeList theNodes = XmlNode.ChildNodes;

            if (theNodes.Count == 0) // loop through all the nodes we got by using ChildNodes
            {
                ElmThrowException(62);
                return null;
            }

            foreach (XmlNode Node in theNodes)
            {
                if (Node.Name == xmlNodeName)
                {
                    return Node;
                }
            }

            ElmThrowException(63);
            return null;


        }

        public XmlNode XmlGetNodeByText(XmlNode XmlNode, string xmlNodeText)
        {
            XmlNodeList theNodes = XmlNode.ChildNodes;

            if (theNodes.Count == 0) // loop through all the nodes we got by using ChildNodes
            {
                ElmThrowException(62);
                return null;
            }

            foreach (XmlNode Node in theNodes)
            {
                if (Node.InnerText == xmlNodeText)
                {
                    return Node;
                }
            }

            ElmThrowException(63);
            return null;


        }

        public XmlNode XmlGetNodeByAttribute(XmlNode XmlNode, string xmlAttributeName)
        {
            XmlNodeList theNodes = XmlNode.ChildNodes;

            if (theNodes.Count == 0) // loop through all the nodes we got by using ChildNodes
            {
                ElmThrowException(62);
                return null;
            }

            foreach (XmlNode Node in theNodes)
            {
                XmlAttributeCollection attributes = Node.Attributes; // get the attributes of each node

                foreach (XmlAttribute attribute in attributes) { 
                    if (attribute.Name == xmlAttributeName)
                    {
                        return Node;
                    }

                }
            }

            ElmThrowException(63);
            return null;


        }

        public XmlNode XmlGetNodeByAttributeValue(XmlNode XmlNode, string xmlAttributeValue)
        {
            XmlNodeList theNodes = XmlNode.ChildNodes;

            if (theNodes.Count == 0) // loop through all the nodes we got by using ChildNodes
            {
                ElmThrowException(62);
                return null;
            }

            foreach (XmlNode Node in theNodes)
            {
                XmlAttributeCollection attributes = Node.Attributes; // get the attributes of each node

                foreach (XmlAttribute attribute in attributes)
                {

                    if (attribute.Value == xmlAttributeValue)
                    {
                        return Node;
                    }

                }
                return Node;
            }

            ElmThrowException(63);
            return null;


        }

        public XmlNode XmlGetParent(XmlNode XmlNode)
        {
            XmlNode theParent = XmlNode.ParentNode;

            return theParent;
        }


        public string XmlGetText(XmlNode xmlNode) // Gets Inner Xml.
        {
            try
            {
                string InnerText = xmlNode.InnerXml;
                return InnerText;
            }
            catch (XmlException)
            {
                ElmThrowException(64);
                return null;
            }
            catch (InvalidOperationException)
            {
                ElmThrowException(65);
                return null;
            }
        }

        public XmlAttribute XmlGetAttribute(XmlNode xmlNode, string attributeName) // Gets Attribute.
        {
            XmlAttributeCollection xmlAttributes = xmlNode.Attributes;

            foreach (XmlAttribute attribute in xmlAttributes)
            {
                 // ok
                if (attribute.Name == attributeName)
                {
                    return attribute;
                }
                
            }

            ElmThrowException(66);
            return null;
        }

        public XmlAttribute XmlGetAttributeByValue(XmlNode xmlNode, string attributeValue) // Gets Attribute.
        {
            XmlAttributeCollection xmlAttributes = xmlNode.Attributes;

            foreach (XmlAttribute attribute in xmlAttributes)
            {
                // ok
                if (attribute.Value == attributeValue)
                {
                    return attribute;
                }

            }

            ElmThrowException(66);
            return null;
        }

        public XmlAttributeCollection XmlGetAttributes(XmlNode xmlNode, string attributeName) // Gets attribute list.
        {
            XmlAttributeCollection xmlAttributes = xmlNode.Attributes;

            if (xmlAttributes.Count == 0)
            {
                ElmThrowException(67);
                return null;
            }

            return xmlAttributes;

          
        }

        public XmlNode XmlGetSibling(XmlNode xmlNode, string nodeName)
        {
            XmlNode xmlParent = xmlNode.ParentNode;

            if (!xmlParent.HasChildNodes)
            {
                ElmThrowException(64); // what the fuck? (use error 12?)
            }

            XmlNodeList SiblingsList = xmlNode.ChildNodes;

            foreach (XmlNode XmlNode_Sibling in SiblingsList)
            {
                if (XmlNode_Sibling.Name == nodeName)
                {
                    return XmlNode_Sibling;
                } 
            }

            ElmThrowException(63);
            return null;

        }

        public XmlNode XmlGetSiblingByText(XmlNode xmlNode, string nodeText)
        {
            XmlNode xmlParent = xmlNode.ParentNode;

            if (!xmlParent.HasChildNodes)
            {
                ElmThrowException(64); // what the fuck? (use error 12?)
            }

            XmlNodeList SiblingsList = xmlNode.ChildNodes;

            foreach (XmlNode XmlNode_Sibling in SiblingsList)
            {
                if (XmlNode_Sibling.InnerText == nodeText)
                {
                    return XmlNode_Sibling;
                }
            }

            ElmThrowException(63);
            return null;

        }

        public XmlNode XmlGetSiblingByAttribute(XmlNode xmlNode, string xmlAttributeName)
        {
            XmlNode xmlParent = xmlNode.ParentNode;

            if (!xmlParent.HasChildNodes)
            {
                ElmThrowException(64); // what the fuck? (use error 12?)
            }

            XmlNodeList SiblingsList = xmlNode.ChildNodes;

            foreach (XmlNode XmlNode_Sibling in SiblingsList)
            {
                if (XmlNode_Sibling.InnerText == xmlAttributeName)
                {
                    XmlAttributeCollection attributes = XmlNode_Sibling.Attributes; // get the attributes of each node

                    foreach (XmlAttribute attribute in attributes)
                    {
                        if (attribute.Name == xmlAttributeName)
                        {
                            return XmlNode_Sibling;
                        }

                    }
                }
            }

            ElmThrowException(63);
            return null;

        }

        public XmlNode XmlGetSiblingByAttributeValue(XmlNode xmlNode, string xmlAttributeName)
        {
            XmlNode xmlParent = xmlNode.ParentNode;

            if (!xmlParent.HasChildNodes)
            {
                ElmThrowException(64); // what the fuck? (use error 12?)
            }

            XmlNodeList SiblingsList = xmlNode.ChildNodes;

            foreach (XmlNode XmlNode_Sibling in SiblingsList)
            {
                if (XmlNode_Sibling.InnerText == xmlAttributeName)
                {
                    XmlAttributeCollection attributes = XmlNode_Sibling.Attributes; // get the attribute value of each node

                    foreach (XmlAttribute attribute in attributes)
                    {
                        if (attribute.Value == xmlAttributeName)
                        {
                            return XmlNode_Sibling;
                        }

                    }
                }
            }

            ElmThrowException(63);
            return null;

        }


        public XmlNodeList XmlGetSiblings(XmlNode xmlNode)
        {
            XmlNode xmlParent = xmlNode.ParentNode;

            if (!xmlParent.HasChildNodes)
            {
                ElmThrowException(64); // what the fuck? (use error 12?)
            }

            XmlNodeList SiblingsList = xmlNode.ChildNodes;

            return SiblingsList;

        }

    }
}
