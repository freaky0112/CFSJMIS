using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace CFSJMIS {
  public abstract  class Load {
      /// <summary>
      /// 读取国土所
      /// </summary>
      /// <param name="node"></param>
      /// <returns></returns>
      public static List<string> gtxRead(string node) {
          //string[] nodes;
          XElement gtx = XElement.Load(Common.XMLTOWN);
          IEnumerable<XElement> elements =
              from el in gtx.Elements(node)
              //where (string)el.Attribute("name") == node
              select el;
          List<string> gtxs=new List<string>();
          foreach (XElement el in elements) {
              gtxs.Add((string)el.Attribute("NAME"));
          }
          return gtxs;
      }

      public static List<string> townRead(string node) {
          XElement gtx = XElement.Load(Common.XMLTOWN);
          IEnumerable<XElement> elements =
              from el in gtx.Elements("GTX")
              where (string)el.Attribute("NAME") == Common.table
              select el;
          IEnumerable<XElement> element =
              from el in elements.Elements(node)
              select el;
          List<string> towns = new List<string>();
          foreach (XElement el in element) {
              towns.Add((string)el.Attribute("NAME"));
          }
          return towns;
      }
    }
}
