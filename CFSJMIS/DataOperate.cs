using System;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.OleDb;

namespace CFSJMIS {
    /// <summary>
    /// 数据操作
    /// </summary>
    public abstract class DataOperate {

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
            sql.Append("@Guid)");
            MySqlParameter[] pt=new MySqlParameter[]{
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
                    new MySqlParameter("@Guid",data.Guid.ToString())
            };
            try {
                MySqlHelper.ExecuteNonQuery(Common.strConntection(), CommandType.Text, sql.ToString(), pt);
            } catch(Exception ex) {
                throw ex;
            }
            if (data.ConfiscateAreaPrice > 0) {
                id = getID(Common.tableConfiscate());
                sql = new StringBuilder();
                sql.Append("insert into 鹤城所没收 ");
                sql.Append("(ID,GUID) values ");
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
            return id;
        }
    }
    /// <summary>
    /// excel操作
    /// </summary>
    public abstract class ExcelOperate {
        public static ArrayList dataList(string path,string town) {
            ArrayList dataList = new ArrayList();
            DataSet dataSet = importExcelToDataSet(path);
            Data data = new Data();
            foreach (DataRow dr in dataSet.Tables[0].Rows) {
                try {
                    if (Common.IsNumber(dr[0].ToString())) {
                        data = new Data();//初始化数据
                        //data.ID = Int32.Parse(dr[0].ToString());
                        data.Name = dr[1].ToString();//户主姓名
                        data.CardID = dr[2].ToString().Replace("\n", "").Trim();//身份证
                        data.Accounts = dr[3].ToString().Replace("\n", "").Trim();//家庭人口
                        data.Location = dr[4].ToString();//座落地点
                        data.BuildDate = Int32.Parse(dr[5].ToString());//建成年月
                        data.Area = double.Parse(dr[6].ToString());//实地占地面积
                        data.LegalArea = double.Parse(dr[7].ToString());//合法面积
                        data.IllegaArea = double.Parse(dr[8].ToString());//超出面积
                        data.IllegaUnit = double.Parse(dr[9].ToString());//单价
                        data.Price = double.Parse(dr[10].ToString());//处罚金额
                        data.Layer = double.Parse(dr[11].ToString());//建设层数
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
                        data = ConfiscateCalculate.getConfiscateData(data);

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
                } catch (Exception ex) {
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
    }
}
