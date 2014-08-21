﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFSJMIS.Collections;
using System.IO;

namespace CFSJMIS.Biult {
    public abstract class  WordBiult {
        public static void Generate(string path, Data data) {
            string generatePath = System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town;
            if (!Directory.Exists(generatePath)) {//判断文件目录是否已经存在
                Directory.CreateDirectory(generatePath);//创建文件夹
            }
            BiultReportForm brf = new BiultReportForm();
            try {
                brf.CreateAWord();
                Index.biult(brf, data);
                CFGZS.biult(brf, data);
                CFGZS.biult(brf, data);
                CFJDS.biult(brf, data);
                CFJDS.biult(brf, data);
                if (data.ConfiscateAreaPrice > 0) {
                    CLJD.biult(brf, data);
                    CLJD.biult(brf, data);
                }
                JKTZ.biult(brf, data);
                brf.TypeBackspace();
                brf.TypeBackspace();
                brf.SaveWord(path);
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}