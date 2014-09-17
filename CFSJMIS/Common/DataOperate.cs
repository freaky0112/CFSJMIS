//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//               佛祖保佑         永无BUG
//
//
//

using System;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.OleDb;
using System.Collections.Generic;
using CFSJMIS.Collections;
using CFSJMIS.Biult;

namespace CFSJMIS {
    /// <summary>
    /// 数据操作
    /// </summary>
    public abstract class DataOperate {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool login(string name, string password) {
            int isLogined = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT count(*) from account where name=");
            sql.Append("@name");
            sql.Append(" and password=");
            sql.Append("@password");
            MySqlParameter[] pt = new MySqlParameter[]{
                new MySqlParameter("@name",name),
                new MySqlParameter("password",password)
            };
            
            string result = MySqlHelper.ExecuteScalar(Common.strConntection(), CommandType.Text, sql.ToString(), pt).ToString();
            isLogined = Int32.Parse(result);
            if (isLogined == 1) {
                return true;
            } else {
                return false;
            }
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data">数据</param>
        public static void insertData(Data data) {
            StringBuilder sql = new StringBuilder();
            string id = getID(Common.table);
            sql.Append("insert into ");
            sql.Append(Common.table);
            sql.Append(" (");
            sql.Append("ID,");
            sql.Append("户主,");
            sql.Append("身份证号,");
            sql.Append("乡镇,");
            sql.Append("户口人数,");
            sql.Append("土地座落,");
            sql.Append("控制区,");
            sql.Append("土地性质,");
            sql.Append("占地面积,");
            sql.Append("层数,");
            sql.Append("建成年月,");
            sql.Append("土地利用总体规划,");
            sql.Append("建房资格,");
            sql.Append("审批面积,");
            sql.Append("超建面积,");
            sql.Append("单价,");
            sql.Append("金额,");
            sql.Append("没收占地面积,");
            sql.Append("没收建筑面积,");
            sql.Append("没收单价,");
            sql.Append("没收金额,");
            sql.Append("建筑面积,");
            sql.Append("审批建筑面积,");
            sql.Append("非法建筑面积,");
            sql.Append("GUID");
            sql.Append(") values (");
            sql.Append("@ID,");
            sql.Append("@Name,");
            sql.Append("@CardID,");
            sql.Append("@Town,");
            sql.Append("@Accounts,");
            sql.Append("@Location,");
            sql.Append("@Control,");
            sql.Append("@LandOwner,");
            sql.Append("@Area,");
            sql.Append("@Layer,");
            sql.Append("@BuildDate,");
            sql.Append("@Conform,");
            sql.Append("@Available,");
            sql.Append("@LegalArea,");
            sql.Append("@IllegaArea,");
            sql.Append("@IllegaUnit,");
            sql.Append("@Price,");
            sql.Append("@ConfiscateFloorArea,");
            sql.Append("@ConfiscateArea,");
            sql.Append("@ConfiscateAreaUnit,");
            sql.Append("@ConfiscateAreaPrice,");
            sql.Append("@ConstructionArea,");
            sql.Append("@LegalConstructionArea,");
            sql.Append("@IllegalConstructionArea,");
            sql.Append("@Guid)");
            MySqlParameter[] pt = new MySqlParameter[]{
                    new MySqlParameter("ID",id),
                    new MySqlParameter("@Name",data.Name.ToString()),
                    new MySqlParameter("@CardID",data.CardID.ToString()),
                    new MySqlParameter("@Town",data.Town.ToString()),
                    new MySqlParameter("@Accounts",data.Accounts.ToString()),
                    new MySqlParameter("@Location",data.Location.ToString()),
                    new MySqlParameter("@Control",data.Control.ToString()),
                    new MySqlParameter("@LandOwner",data.LandOwner.ToString()),
                    new MySqlParameter("@Area",data.Area.ToString()),
                    new MySqlParameter("@Layer",data.Layer.ToString()),
                    new MySqlParameter("@BuildDate",data.BuildDate.ToString()),
                    new MySqlParameter("@Conform",data.Conform.ToString()),
                    new MySqlParameter("@Available",data.Available.ToString()),
                    new MySqlParameter("@LegalArea",data.LegalArea.ToString()),
                    new MySqlParameter("@IllegaArea",data.IllegaArea.ToString()),
                    new MySqlParameter("@IllegaUnit",data.IllegaUnit.ToString()),
                    new MySqlParameter("@Price",data.Price.ToString()),
                    new MySqlParameter("@ConfiscateFloorArea",data.ConfiscateFloorArea.ToString()),
                    new MySqlParameter("@ConfiscateArea",data.ConfiscateArea.ToString()),
                    new MySqlParameter("@ConfiscateAreaUnit",data.ConfiscateAreaUnit.ToString()),
                    new MySqlParameter("@ConfiscateAreaPrice",data.ConfiscateAreaPrice.ToString()),
                    new MySqlParameter("@ConstructionArea",data.ConstructionArea),
                    new MySqlParameter("@LegalConstructionArea",data.LegalConstructionArea),
                    new MySqlParameter("@IllegalConstructionArea",data.IllegalConstructionArea),
                    new MySqlParameter("@Guid",data.Guid.ToString())
            };
            try {
                MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
            } catch (Exception ex) {
                throw ex;
            }
            if (data.ConfiscateAreaPrice > 0) {
                id = getID(Common.tableConfiscate());
                sql = new StringBuilder();
                sql.Append("insert into ");
                sql.Append(Common.tableConfiscate());
                sql.Append(" (ID,GUID) values ");
                sql.Append("(@id,@guid)");
                pt = new MySqlParameter[]{                    
                        new MySqlParameter("@id",id),
                        new MySqlParameter("@guid",data.Guid)
                    };
                try {
                    MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 获取空缺ID
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>返回空缺ID，返回值为Null即向后新增</returns>
        public static string getID(string table) {
            StringBuilder sqlMax = new StringBuilder();
            string id = null;
            sqlMax.Append("select min(ID-1) from ");
            sqlMax.Append(table);
            sqlMax.Append(" where ID not in(select 1+id from ");
            sqlMax.Append(table);
            sqlMax.Append(") and id not in (select min(id) from ");
            sqlMax.Append(table);
            sqlMax.Append(")");
            try {
                id = MySqlHelper.ExecuteScalar(Common.strConntection(), CommandType.Text, sqlMax.ToString(), null).ToString();
            } catch (Exception ex) {
                throw ex;
            }
            if (string.IsNullOrEmpty(id)) {
                sqlMax = new StringBuilder();
                sqlMax.Append("select count(*)+1 from ");
                sqlMax.Append(table);
                try {
                    id = MySqlHelper.ExecuteScalar(Common.strConntection(), CommandType.Text, sqlMax.ToString(), null).ToString();
                } catch (Exception ex) {
                    throw ex;
                }
                //id = null;
            }
            
            
            
            return id;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static List<Data> query() {
            List<Data> dataList = new List<Data>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM ");
            sql.Append(Common.getView());
            MySqlDataReader reader;
            try {
                reader = MySqlHelper.ExecuteReader(Common.strConntection(), CommandType.Text, sql.ToString(), null);
            } catch (MySqlException ex) {
                throw ex;
            }
            while (reader.Read()) {
                Data data = readDB(reader);
                dataList.Add(data);
            }
            return dataList;
        }
        /// <summary>
        /// 将数据库reader赋值到data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static Data readDB(MySqlDataReader reader) {
            Data data = new Data();
            try {

                data.ID = Int32.Parse(reader[0].ToString());//处罚编号
                data.Name = reader[1].ToString();//户主
                //没收编号
                if (!string.IsNullOrEmpty(reader[2].ToString())) {
                    data.ConfiscateID = Int32.Parse(reader[2].ToString());
                } else {
                    data.ConfiscateID = null;
                }
                data.CardID = reader["身份证号"].ToString();
                data.Town = reader["乡镇"].ToString();
                data.Accounts = reader["户口人数"].ToString();
                data.Location = reader["土地座落"].ToString();
                data.Control = reader["控制区"].ToString();
                data.LandOwner = reader.GetString("土地性质");
                data.Area = reader.GetDouble("占地面积");
                if (!string.IsNullOrEmpty(reader["耕地面积"].ToString())) {
                    data.FarmArea = reader.GetDouble("耕地面积");
                } else {
                    data.FarmArea = 0;
                }
                data.Layer = reader.GetDouble("层数");
                data.BuildDate = reader.GetInt32("建成年月");
                data.LegalArea = reader.GetDouble("审批面积");
                data.IllegaArea = reader.GetDouble("超建面积");
                data.IllegaUnit = reader.GetDouble("单价");
                if (!string.IsNullOrEmpty(reader["耕地单价"].ToString())) {
                    data.FarmUnit = reader.GetDouble("耕地单价");
                } else {
                    data.FarmUnit = 0;
                }
                data.Price = reader.GetDouble("金额");
                data.ConfiscateFloorArea = reader.GetDouble("没收占地面积");
                data.ConfiscateArea = reader.GetDouble("没收建筑面积");
                data.ConfiscateAreaUnit = reader.GetDouble("没收单价");
                data.ConfiscateAreaPrice = reader.GetDouble("没收金额");
                if (!string.IsNullOrEmpty(reader["建筑面积"].ToString())) {
                    data.ConstructionArea = reader.GetDouble("建筑面积");
                }
                if (!string.IsNullOrEmpty(reader["非法建筑面积"].ToString())) {
                    data.IllegalConstructionArea = reader.GetDouble("非法建筑面积");
                }
                if (!string.IsNullOrEmpty(reader["审批建筑面积"].ToString())) {
                    data.LegalConstructionArea = reader.GetDouble("审批建筑面积");
                }
                data.Characters = new List<Character>();
                for (int i = 0; i < data.Names.Length; i++) {
                    Character character = new Character();
                    character.Name = data.Names[i];
                    character.Sex = data.Sex[i];
                    character.CardID = data.CardIDs[i];
                    data.Characters.Add(character);
                }
                if (!string.IsNullOrEmpty(reader["是否已处罚"].ToString())) {
                    data.Signed = reader.GetBoolean("是否已处罚");
                }
                data.Guid = reader.GetString("GUID");
            } catch {
                throw new Exception(data.ID.ToString());
            }
            return data;
        }
        /// <summary>
        /// 修改是否已处罚
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="signed">是否已处罚</param>
        public static void singedData(string guid, bool signed) {
            StringBuilder sql = new StringBuilder();
            sql.Append("update ");
            sql.Append(Common.table);
            sql.Append(" set ");
            sql.Append("是否已处罚=@Signed  ");
            sql.Append("where ");
            sql.Append("GUID= @Guid ");
            MySqlParameter[] pt = new MySqlParameter[]{
                new MySqlParameter("@GUID",guid),
                new MySqlParameter("@Signed",signed)
            };
            try {
                MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
            } catch (MySqlException ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="data"></param>
        public static Data modifyData(Data data) {

            StringBuilder sql = new StringBuilder();
            sql.Append("update ");
            sql.Append(Common.table);
            sql.Append(" set ");
            sql.Append("户主=@Name, ");
            sql.Append("身份证号=@CardID, ");
            sql.Append("乡镇=@Town, ");
            sql.Append("户口人数=@Accounts, ");
            sql.Append("土地座落=@Location, ");
            sql.Append("控制区=@Control, ");
            sql.Append("土地性质=@LandOwner, ");
            sql.Append("占地面积=@Area, ");
            sql.Append("层数=@Layer, ");
            sql.Append("建成年月=@BuildDate, ");
            //sql.Append("土地利用总体规划=@Conform, ");
            sql.Append("建房资格=@Available, ");
            sql.Append("审批面积=@LegalArea, ");
            sql.Append("超建面积=@IllegaArea, ");
            sql.Append("单价=@IllegaUnit, ");
            sql.Append("金额=@Price, ");
            sql.Append("没收占地面积=@ConfiscateFloorArea, ");
            sql.Append("没收建筑面积=@ConfiscateArea, ");
            sql.Append("没收单价=@ConfiscateAreaUnit, ");
            sql.Append("没收金额=@ConfiscateAreaPrice, ");
            sql.Append("建筑面积=@ConstructionArea, ");
            sql.Append("审批建筑面积=@LegalConstructionArea, ");
            sql.Append("非法建筑面积=@IllegalConstructionArea, ");
            sql.Append("耕地面积=@FarmArea, ");
            sql.Append("耕地单价=@FarmUnit ");
            sql.Append("where ");
            sql.Append("GUID= @Guid ");

            MySqlParameter[] pt = new MySqlParameter[]{
                    new MySqlParameter("@Name",data.Name),
                    new MySqlParameter("@CardID",data.CardID),
                    new MySqlParameter("@Town",data.Town),
                    new MySqlParameter("@Accounts",data.Accounts),
                    new MySqlParameter("@Location",data.Location),
                    new MySqlParameter("@Control",data.Control),
                    new MySqlParameter("@LandOwner",data.LandOwner),
                    new MySqlParameter("@Area",data.Area),
                    new MySqlParameter("@Layer",data.Layer),
                    new MySqlParameter("@BuildDate",data.BuildDate),
                    new MySqlParameter("@Conform",data.Conform),
                    new MySqlParameter("@Available",data.Available),
                    new MySqlParameter("@LegalArea",data.LegalArea),
                    new MySqlParameter("@IllegaArea",data.IllegaArea),
                    new MySqlParameter("@IllegaUnit",data.IllegaUnit),
                    new MySqlParameter("@Price",data.Price),
                    new MySqlParameter("@ConfiscateFloorArea",data.ConfiscateFloorArea),
                    new MySqlParameter("@ConfiscateArea",data.ConfiscateArea),
                    new MySqlParameter("@ConfiscateAreaUnit",data.ConfiscateAreaUnit),
                    new MySqlParameter("@ConfiscateAreaPrice",data.ConfiscateAreaPrice),
                    new MySqlParameter("@ConstructionArea",data.ConstructionArea),
                    new MySqlParameter("@LegalConstructionArea",data.LegalConstructionArea),
                    new MySqlParameter("@IllegalConstructionArea",data.IllegalConstructionArea),
                    new MySqlParameter("@FarmArea",data.FarmArea),
                    new MySqlParameter("@FarmUnit",data.FarmUnit),
                    new MySqlParameter("@Guid",data.Guid)
                 };
            try {
                MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
            } catch (MySqlException ex) {
                throw ex;
            }
            //没收编号
            string id;
            if (data.ConfiscateAreaPrice > 0) {
                id = getID(Common.tableConfiscate());
                sql = new StringBuilder();
                sql.Append("insert into ");
                sql.Append(Common.tableConfiscate());
                sql.Append(" (ID,GUID) values ");
                sql.Append("(@id,@guid)");
                pt = new MySqlParameter[]{                    
                        new MySqlParameter("@id",id),
                        new MySqlParameter("@guid",data.Guid)
                    };
                try {
                    MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
                } catch (MySqlException ex) {
                    if (!ex.Message.Contains("UNIQUE")) {
                        throw ex;
                    }
                }
            } else {
                sql = new StringBuilder();
                sql.Append("delete from ");
                sql.Append(Common.tableConfiscate());
                sql.Append(" where GUID = @Guid");
                try {
                    MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
                } catch (MySqlException ex) {
                    throw ex;
                }
            }

            return data;
        }
        /// <summary>
        /// 数据删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool deleteData(Data data) {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from ");
            sql.Append(Common.table);
            sql.Append(" where GUID = @Guid");
            MySqlParameter[] pt = new MySqlParameter[]{
                new MySqlParameter("@Guid",data.Guid)
            };
            try {
                MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
            } catch (MySqlException ex) {
                return false;
            }
            sql = new StringBuilder();
            sql.Append("delete from ");
            sql.Append(Common.tableConfiscate());
            sql.Append(" where GUID = @Guid");
            try {
                MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
            } catch (MySqlException ex) {
                return false;
            }
            return true;
        }

        public static void CharacterToData(Data data) {
            string name="";
            string cardid="";
            for (int i = 0; i < data.Characters.Count;i++ ) {
                Character character = data.Characters[i];
                name += character.Name;
                cardid += character.CardID;
                if (i < data.Characters.Count - 1) {
                    name += "、";
                    cardid += "、";
                }                
            }
            data.Name = name;
            data.CardID = cardid;
        }
    }
    /// <summary>
    /// excel操作
    /// </summary>
    public abstract class ExcelOperate {
        public static ArrayList getDataList(string path, string town) {
            ArrayList dataList = new ArrayList();
            DataSet dataSet = importExcelToDataSet(path);
            Data data = new Data();
            foreach (DataRow dr in dataSet.Tables[0].Rows) {
                try {
                    if (Common.IsNumber(dr[0].ToString())) {
                        data = new Data();//初始化数据
                        data.Name = dr[1].ToString();//户主姓名
                        if ((dr[5].ToString().Length != 6)&&Common.IsNumber(dr[5].ToString())) {
                            throw new Exception("年份有误");
                        }
                        data.BuildDate = Int32.Parse(dr[5].ToString());//建成年月
                        if (data.BuildDate > 198700) {//1987年以后
                            //data.ID = Int32.Parse(dr[0].ToString());
                            data.Name = dr[1].ToString();//户主姓名
                            data.CardID = dr[2].ToString().Replace("\n", "").Trim();//身份证
                            data.Accounts = dr[3].ToString().Replace("\n", "").Trim();//家庭人口
                            data.Location = dr[4].ToString();//座落地点
                            data.Area = double.Parse(dr[6].ToString());//实地占地面积
                            data.LegalArea = double.Parse(dr[7].ToString());//合法面积
                            //data.IllegaArea = double.Parse(dr[8].ToString());//超出面积
                            data.IllegaArea = data.Area - data.LegalArea;
                            data.IllegaUnit = double.Parse(dr[9].ToString());//单价
                            data.Price = double.Parse(dr[10].ToString());//处罚金额
                            data.Layer = double.Parse(dr[11].ToString());//建设层数
                            data.ConstructionArea = double.Parse(dr[13].ToString());//建筑面积
                            data.LegalConstructionArea = double.Parse(dr[14].ToString());//审批建筑面积
                            data.IllegalConstructionArea = AreaCalculate.getIlegalConstructionArea(data);
                            data.Conform = dr[22].ToString();//土地利用总体规划
                            data.Available = dr[21].ToString();//建房资格
                            data.Control = dr[29].ToString();//控制区
                            data.LandOwner = dr[28].ToString();//土地性质
                            //data.IsnotConfiscate = !string.IsNullOrEmpty(dr[23].ToString());//是否没收
                            //data.IsnotConfiscate = ConfiscateCalculate.isNotConfiscate(data);
                            //data.ConfiscateAreaPrice = double.Parse(dr[25].ToString());//总金额
                            data.Town = town;//所在乡镇
                            data.Accounts = dr[3].ToString();//户口人数
                            data.CardIDs = data.CardID.Split('、');
                            if (data.CardIDs.Length != data.Names.Length) {
                                throw new Exception("，" + data.Location + "处数据有误请核实");
                            }
                            //foreach (string cardid in data.CardIDs) {
                            //    if (cardid.Length != 18 && cardid.Length != 0) {
                            //        //MessageBox.Show(data.Name+"，"+data.Location+"处身份证号码数据有误请核实");
                            //        //Exception ex=new Exception();
                            //        //ex.Message=data.Name+"，"+data.Location+"处身份证号码数据有误请核实";
                            //        throw new Exception( "，" + data.Location + "处身份证号码数据有误请核实");
                            //    }
                            //}
                            data.Guid = System.Guid.NewGuid().ToString();//GUID生成
                            data = AreaCalculate.getConfiscateData(data);

                            if (data.IsnotConfiscate) {

                                //data.ConfiscateArea = double.Parse(dr[23].ToString());//没收面积
                                //data.ConfiscateAreaUnit = Int32.Parse(dr[24].ToString());//没收单价
                                //data.ConfiscateAreaPrice = double.Parse(dr[25].ToString());//没收金额
                                //data.ConfiscateFloorArea = data.ConfiscateArea / data.Layer;//没收占地面积
                            }
                            if (data.IllegaArea / data.Names.Length > 0) {//判断是否要处罚，平均每户小于1平方免于处罚
                                dataList.Add(data);
                            }
                        }
                    }
                } catch (Exception ex) {
                    dataList = new ArrayList();
                    throw new Exception(data.Name.ToString() + ex.Message);
                }
            }
            return dataList;
        }
        /// <summary>
        /// 导入excel数据
        /// </summary>
        /// <param name="path">excel文件路径</param>
        /// <returns></returns>
        private static DataSet importExcelToDataSet(string path) {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [处罚金额$A:AF]", strConn);
            DataSet myDataSet = new DataSet();
            try {
                myCommand.Fill(myDataSet);
            } catch (Exception ex) {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return myDataSet;
        }

        public static void exportDataToExcel(string savePath, List<Data> list) {
            if (System.IO.File.Exists(savePath)) {
                System.IO.File.Delete(savePath);
            }
            using (ExcelHelper excelHelper = new ExcelHelper(savePath)) {
                foreach (string table in getTable(list)) {
                    excelHelper.DataTablesToExcel(listToDataTable(list, table), table, true);
                }
                excelHelper.Write();
                excelHelper.Dispose();
            }
        }
        private static List<string> getTable(List<Data> list) {
            List<string> tables = new List<string>();
            foreach (Data data in list) {
                if (!tables.Contains(data.Town)) {
                    tables.Add(data.Town);
                }
            }
            return tables;

        }
        private static DataTable listToDataTable(List<Data> list, string table) {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("户主");
            dt.Columns.Add("没收编号");
            dt.Columns.Add("户口人数");
            dt.Columns.Add("土地座落");
            dt.Columns.Add("控制区");
            dt.Columns.Add("土地性质");
            dt.Columns.Add("占地面积");
            dt.Columns.Add("耕地面积");
            dt.Columns.Add("建筑面积");
            dt.Columns.Add("审批建筑面积");
            dt.Columns.Add("非法建筑面积");
            dt.Columns.Add("层数");
            dt.Columns.Add("建成年月");
            dt.Columns.Add("审批面积");
            dt.Columns.Add("超建面积");
            dt.Columns.Add("单价");
            dt.Columns.Add("耕地单价");
            dt.Columns.Add("金额");
            dt.Columns.Add("没收建筑面积");
            dt.Columns.Add("没收单价");
            dt.Columns.Add("没收金额");
            dt.Columns.Add("是否已处罚");
            dt.Columns.Add("总金额");
            foreach (Data data in list) {
                if (data.Town.Contains(table)) {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = data.ID;
                    dr["户主"] = data.Name;
                    dr["没收编号"] = data.ConfiscateID;
                    dr["户口人数"] = data.Accounts;
                    dr["土地座落"] = data.Location;
                    dr["控制区"] = data.Control;
                    dr["土地性质"] = data.LandOwner;
                    dr["占地面积"] = data.Area;
                    dr["耕地面积"] = data.FarmArea;
                    dr["建筑面积"] = data.ConstructionArea;
                    dr["审批建筑面积"] = data.LegalConstructionArea;
                    dr["非法建筑面积"] = data.IllegalConstructionArea;
                    dr["层数"] = data.Layer;
                    dr["建成年月"] = data.BuildDate;
                    dr["审批面积"] = data.LegalArea;
                    dr["超建面积"] = data.IllegaArea;
                    dr["单价"] = data.IllegaUnit;
                    dr["耕地单价"] = data.FarmUnit;
                    dr["金额"] = data.Price;
                    dr["没收建筑面积"] = data.ConfiscateArea;
                    dr["没收单价"] = data.ConfiscateAreaUnit;
                    dr["没收金额"] = data.ConfiscateAreaPrice;
                    if (data.Signed) {
                        dr["是否已处罚"] = "√";
                    }
                    dr["总金额"] = data.Price + data.ConfiscateAreaPrice;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
