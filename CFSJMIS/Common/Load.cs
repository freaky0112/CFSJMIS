﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;

namespace CFSJMIS {
  public abstract  class Load {
      /// <summary>
      /// 读取国土所
      /// </summary>
      /// <param name="node"></param>
      /// <returns></returns>
      public static List<string> gtxRead(string node) {
          //string[] nodes;
          XElement gtx = XElement.Load(Common.XMLTown);
          IEnumerable<XElement> elements =
              from el in gtx.Elements(node)
              //where (string)el.Attribute("name") == node
              select el;
          List<string> gtxs=new List<string>();
          gtxs.Add("请选择所在国土所");
          foreach (XElement el in elements) {
              gtxs.Add((string)el.Attribute("NAME"));
          }
          return gtxs;
      }
      /// <summary>
      /// 读取国土所对应乡镇
      /// </summary>
      /// <param name="node"></param>
      /// <returns></returns>
      public static List<string> townRead(string node) {
          XElement gtx = XElement.Load(Common.XMLTown);
          IEnumerable<XElement> elements =
              from el in gtx.Elements("GTX")
              where (string)el.Attribute("NAME") == Common.table
              select el;
          IEnumerable<XElement> element =
              from el in elements.Elements(node)
              select el;
          List<string> towns = new List<string>();
          towns.Add("请选择导入乡镇");
          foreach (XElement el in element) {
              towns.Add((string)el.Attribute("NAME"));
          }
          return towns;
      }
      public static List<string> ethnicRead() {
          XElement ethnic = XElement.Load(Common.XMLEthnic);
          IEnumerable<XElement> element =
              from el in ethnic.Elements("Ethnic")
              select el;
          List<string> Ethnics = new List<string>();
          foreach (XElement el in element) {
              Ethnics.Add(el.Value);
          }
          return Ethnics;
      }
      public static string getPath() {
          OpenFileDialog openFileDialog = new OpenFileDialog();
          openFileDialog.InitialDirectory = "c://";
          openFileDialog.Multiselect = false;
          openFileDialog.Filter = "xls文件|*.xls|xlsx文件|*.xlsx|所有文件|*.*";
          openFileDialog.RestoreDirectory = true;
          openFileDialog.FilterIndex = 1;
          if (openFileDialog.ShowDialog() == DialogResult.OK) {
              return openFileDialog.FileName;
          } else {
              return null;
          }
      }
    }
}
