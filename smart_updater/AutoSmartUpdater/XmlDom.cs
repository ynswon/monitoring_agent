using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace AutoSmartUpdater
{
    class XmlDom
    {
        private XmlDocument xmldoc;

        public XmlDom()
        {
            xmldoc = new XmlDocument();
        }
        public void ReadXMLFromFile(string path) 
        {
            try
            {
                xmldoc.Load(path);
            }
            catch
            {
                throw new Exception();
            }
        }
        public void ReadXMLFromStream(Stream instream)
        {
            try
            {
                xmldoc.Load(instream);                
            }
            catch
            {
                throw new Exception();
            }
        }

        public XmlNodeList GetElementsByTagName(String tag)
        {
            XmlElement rootElement = xmldoc.DocumentElement;
            

            return xmldoc.GetElementsByTagName(tag);
        }

        public XmlElement GetElementById(String elementId)
        {
            return xmldoc.GetElementById(elementId);
        }

        public String GetInnerTextByTagName(String tagName)
        {
            String value = null;
            XmlNodeList nodeList = xmldoc.GetElementsByTagName(tagName);
            foreach (XmlNode node in nodeList)
            {
                if (node.Name.Equals(tagName))
                {
                    value = node.InnerText;
                    break;
                }
            }
            return value;
        }

        public String GetAttributeValue(String tagName, String attrName)
        {
            String value = null;
            XmlNodeList nodeList = xmldoc.GetElementsByTagName(tagName);
            foreach (XmlNode node in nodeList)
            {                
               if (node.Name.Equals(tagName))
               {                    
                    value = node.Attributes[attrName].Value;
                    break;
                }
            }
            return value;
        }


        public List<string> GetItemListByTagName(string tagName)
        {
            List<string> itemList = new List<string>();

            XmlNodeList nodeList = xmldoc.GetElementsByTagName(tagName);
            foreach (XmlNode node in nodeList)
            {
                itemList.Add(node.InnerText);                
            }

            return itemList;
        }

    }
}
