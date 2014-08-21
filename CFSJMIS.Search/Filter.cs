﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFSJMIS.Collections;

namespace CFSJMIS.Search
{
    public class Filter
    {
        public static List<Data> searchList(List<Data> dataList, string keyword) {
            //List<Data> datas = new List<Data>();
            IEnumerable<Data> datas = from data in dataList
                    where data.ID.ToString().Contains(keyword)
                    || data.Name.Contains(keyword)
                    || data.Location.Contains(keyword)
                    || data.Town.Contains(keyword)
                    select data;
            dataList = new List<Data>();
            foreach (Data data in datas) {
                dataList.Add(data);
            }
            return dataList;
        }
    }
}