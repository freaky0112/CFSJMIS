using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFSJMIS.Collections;

namespace CFSJMIS.Biult {
    public class  Generate {
        public Generate(string path, Data Data) {
            BiultReportForm brf = new BiultReportForm();
            try {
                brf.CreateAWord();
                brf.SaveWord(path);
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
