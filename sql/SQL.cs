using Pharmacy.INST.DissolutionClient.common;
namespace Pharmacy.INST.DissolutionClient.sql
{
    public class SQL
    {
        //Table
        public static string T_SQLITESEQUENCE = "sqlite_sequence";
        public static string T_METHOD = "tbl_method";
        public static string T_TESTDATA = "tbl_testdata";
        public static string T_EXPCUPTEMP = "tbl_expcuptemp";
        public static string T_USER = "tbl_user";
        public static string T_ROLE = "tbl_role";
        public static string T_FUNCTION = "tbl_function";
        public static string T_PRIVELEGE = "tbl_privelege";
        public static string T_WORKLOG = "tbl_worklog";
        public static string T_MODULE = "tbl_module";
        public static string T_LOGTYPE = "tbl_logtype";
        public static string T_BEHAVIOR = "tbl_behavior";
        public static string T_TEMPCALI = "tbl_tempcali";
        public static string T_VERIFYDATA = "tbl_verifydata";


        //Globle Retriver seq
        public static string SQL_U_SEQBYTABLE = "UPDATE " + T_SQLITESEQUENCE
                                                   + " SET seq = 0 "
                                                   + " WHERE name = '{0}'";

        //Globe Ins and Upd Cmd
        public static string SQL_C_CREATEMETHOD = "INSERT INTO " + T_METHOD //方法创建
                                 + "(method_name,manufacturer,batch_number,test_department"
                                 + ",test_method,temp_setting,solvent_volume"
                                 + ",frontrow_speed_1,frontrow_speed_2,frontrow_starttime_1,frontrow_starttime_2"
                                 + ",backrow_speed_1,backrow_speed_2,backrow_starttime_1,backrow_starttime_2"
                                 + ",sample_times,sample_volume,auto_fluid_infusion,first_filter_volume,all_timespan"
                                 + ",sample_time_1,sample_time_2,sample_time_3,sample_time_4,sample_time_5,sample_time_6"
                                 + ",sample_time_7,sample_time_8,sample_time_9,sample_time_10,sample_time_11,sample_time_12"
                                 + ",sample_time_13,sample_time_14,sample_time_15,sample_time_16,sample_time_17,sample_time_18"
                                 + ",sample_time_19,sample_time_20,sample_time_21,sample_time_22,sample_time_23,sample_time_24"
                                 + ",sample_time_25,sample_time_26,sample_time_27,sample_time_28,sample_time_29,sample_time_30"
                                 + ",sample_time_31,sample_time_32,sample_time_33,sample_time_34,sample_time_35,sample_time_36"
                                 + ",sample_time_37,sample_time_38,sample_time_39,sample_time_40"
                                 + ",method_time,speed_mode,electricmotor_mode"
                                 + ",dilution_enabled,dilution_ratio"
                                 + ") VALUES ("
                                 + "'{0}','{1}','{2}','{3}'"
                                 + ",'{4}',{5},{6}"
                                 + ",{7},{8},{9},{10}"
                                 + ",{11},{12},{13},{14}"
                                 + ",{15},{16},'{17}',{18},{19}"
                                 + ",{20},{21},{22},{23},{24},{25}"
                                 + ",{26},{27},{28},{29},{30},{31}"
                                 + ",{32},{33},{34},{35},{36},{37}"
                                 + ",{38},{39},{40},{41},{42},{43}"
                                 + ",{44},{45},{46},{47},{48},{49}"
                                 + ",{50},{51},{52},{53},{54},{55}"
                                 + ",{56},{57},{58},{59}"
                                 + ",'{60}','{61}','{62}'"
                                 + ",{63},{64}"
                                 + ")";
        public static string SQL_R_FINDMETHODBYNAME = "SELECT * FROM " + T_METHOD + " WHERE method_name='{0}'"; //根据方法名称查询
        public static string SQL_R_FINDMETHOD = "SELECT * FROM " + T_METHOD;  //查询所有方法
        public static string SQL_R_METHOD = "SELECT * FROM " + T_METHOD       //根据条件查询方法
                                         + " WHERE {0}"
                                         + " ORDER BY method_time DESC";

        public static string SQL_U_UPDATEMETHOD = "UPDATE " + T_METHOD  //方法更新
                                 + " SET manufacturer='{0}',batch_number='{1}',test_department='{2}'"
                                 + ",test_method='{3}',temp_setting={4},solvent_volume={5}"
                                 + ",frontrow_speed_1={6},frontrow_speed_2={7},frontrow_starttime_1={8},frontrow_starttime_2={9}"
                                 + ",backrow_speed_1={10},backrow_speed_2={11},backrow_starttime_1={12},backrow_starttime_2={13}"
                                 + ",sample_times={14},sample_volume={15},auto_fluid_infusion='{16}',first_filter_volume={17},all_timespan={18}"
                                 + ",sample_time_1={19},sample_time_2={20},sample_time_3={21},sample_time_4={22},sample_time_5={23},sample_time_6={24}"
                                 + ",sample_time_7={25},sample_time_8={26},sample_time_9={27},sample_time_10={28},sample_time_11={29},sample_time_12={30}"
                                 + ",sample_time_13={31},sample_time_14={32},sample_time_15={33},sample_time_16={34},sample_time_17={35},sample_time_18={36}"
                                 + ",sample_time_19={37},sample_time_20={38},sample_time_21={39},sample_time_22={40},sample_time_23={41},sample_time_24={42}"
                                 + ",sample_time_25={43},sample_time_26={44},sample_time_27={45},sample_time_28={46},sample_time_29={47},sample_time_30={48}"
                                 + ",sample_time_31={49},sample_time_32={50},sample_time_33={51},sample_time_34={52},sample_time_35={53},sample_time_36={54}"
                                 + ",sample_time_37={55},sample_time_38={56},sample_time_39={57},sample_time_40={58}"
                                 + ",method_time='{59}',speed_mode='{60}',electricmotor_mode='{61}'"
                                 + ",dilution_enabled={62},dilution_ratio={63}"
                                 + " WHERE "
                                 + "method_name='{64}'";
        public static string SQL_D_METHODBYMETHODNAME = "DELETE FROM " + T_METHOD  //根据方法名称删除
                                 + " WHERE "
                                 + "method_name='{0}'";
        public static string SQL_D_METHOD = "DELETE FROM " + T_METHOD;             //删除所有方法

        #region 实验记录表
        public static string SQL_C_TESTDATA = "INSERT INTO "+ T_TESTDATA          //创建实验记录
             + "(status,starttime,endtime,login_name"
             + ",method_name,manufacturer,batch_no"
             + ",test_department,test_method,temp_waterbox,temp_cup"
             + ",temp_cup_1,temp_cup_2,temp_cup_3,temp_cup_4"
             + ",temp_cup_5,temp_cup_6,temp_cup_7,temp_cup_8"
             + ",temp_cup_9,temp_cup_10,temp_cup_11,temp_cup_12"
             + ",temp_cup_13,temp_cup_14,temp_cup_15,temp_cup_16"
             + ",frontrow_speed_1,frontrow_speed_2,backrow_speed_1,backrow_speed_2"
             + ",all_timespan,temp_setting,solvent_volume"
             + ",sample_time_1,sample_time_2,sample_time_3,sample_time_4"
             + ",sample_time_5,sample_time_6,sample_time_7,sample_time_8"
             + ",sample_time_9,sample_time_10,sample_time_11,sample_time_12"
             + ",sample_time_13,sample_time_14,sample_time_15,sample_time_16"
             + ",sample_time_17,sample_time_18,sample_time_19,sample_time_20"
             + ",sample_time_21,sample_time_22,sample_time_23,sample_time_24"
             + ",sample_time_25,sample_time_26,sample_time_27,sample_time_28"
             + ",sample_time_29,sample_time_30,sample_time_31,sample_time_32"
             + ",sample_time_33,sample_time_34,sample_time_35,sample_time_36"
             + ",sample_time_37,sample_time_38,sample_time_39,sample_time_40"
             + ",sample_times,sample_volume,auto_fluid_infusion"
             + ",first_filter_volume,method_time,speed_mode,electricmotor_mode"
             + ",frontrow_starttime_1,frontrow_starttime_2,backrow_starttime_1,backrow_starttime_2"
             + ",dilution_enabled,dilution_ratio"
             + ") VALUES ("
             + "'{0}','{1}','{2}','{3}'"
             + ",'{4}','{5}','{6}'"
             + ",'{7}','{8}','{9}',{10}"
             + ",'{11}','{12}','{13}','{14}'"
             + ",'{15}','{16}','{17}','{18}'"
             + ",'{19}','{20}','{21}','{22}'"
             + ",'{23}','{24}','{25}','{26}'"
             + ",{27},{28},{29},{30}"
             + ",{31},{32},{33}"
             + ",{34},{35},{36},{37}"
             + ",{38},{39},{40},{41}"
             + ",{42},{43},{44},{45}"
             + ",{46},{47},{48},{49}"
             + ",{50},{51},{52},{53}"
             + ",{54},{55},{56},{57}"
             + ",{58},{59},{60},{61}"
             + ",{62},{63},{64},{65}"
             + ",{66},{67},{68},{69}"
             + ",{70},{71},{72},{73}"
             + ",{74},{75},'{76}'"
             + ",{77},'{78}','{79}','{80}'"
             + ",{81},{82},{83},{84}"
             + ",{85},{86}"
             + ")";
        public static string SQL_U_TESTDATASTARTTIMEBYID = "UPDATE " + T_TESTDATA
                                                + " SET "
                                                + " starttime = '{0}'"
                                                + " WHERE "
                                                + " id = {1}";
        public static string SQL_U_TESTDATAENDTIMEBYID = "UPDATE " + T_TESTDATA
                                                + " SET "
                                                + " status='{0}',endtime = '{1}'"
                                                + " WHERE "
                                                + " id = {2}";
        public static string SQL_U_TESTDATAREVIEWERBYID = "UPDATE " + T_TESTDATA
                                                + " SET "
                                                + " reviewer='{0}'"
                                                + ",reviewer_name='{1}'"
                                                + ",review_content='{2}'"
                                                + ",review_time='{3}'"
                                                + " WHERE "
                                                + " id = {4}";
        public static string SQL_R_TESTDATA = "SELECT id as ID,status as Status,starttime as StartTime,endtime as EndTime" 
             +",login_name as LoginName,method_name as MethodName,manufacturer as Manufacturer,batch_no as BatchNo"
             + ",test_department as TestDepartment,test_method as TestMethod,temp_waterbox as TempWaterBox,temp_cup as TempCup"
             + ",temp_cup_1 as TempCup_1,temp_cup_2 as TempCup_2,temp_cup_3 as TempCup_3,temp_cup_4 as TempCup_4"
             + ",temp_cup_5 as TempCup_5,temp_cup_6 as TempCup_6,temp_cup_7 as TempCup_7,temp_cup_8 as TempCup_8"
             + ",temp_cup_9 as TempCup_9,temp_cup_10 as TempCup_10,temp_cup_11 as TempCup_11,temp_cup_12 as TempCup_12"
             + ",temp_cup_13 as TempCup_13,temp_cup_14 as TempCup_14,temp_cup_15 as TempCup_15,temp_cup_16 as TempCup_16"
             + ",frontrow_speed_1 as FrontRowSpeed_1,frontrow_speed_2 as FrontRowSpeed_2,backrow_speed_1 as BackRowSpeed_1,backrow_speed_2 as BackRowSpeed_2"
            +",all_timespan as AllTimeSpan,temp_setting as TempSetting,solvent_volume as SolventVolume"
            + ",sample_time_1 as SampleTime_1,sample_time_2 as SampleTime_2,sample_time_3 as SampleTime_3,sample_time_4 as SampleTime_4"
            + ",sample_time_5 as SampleTime_5,sample_time_6 as SampleTime_6,sample_time_7 as SampleTime_7,sample_time_8 as SampleTime_8"
            + ",sample_time_9 as SampleTime_9,sample_time_10 as SampleTime_10,sample_time_11 as SampleTime_11,sample_time_12 as SampleTime_12"
            + ",sample_time_13 as SampleTime_13,sample_time_14 as SampleTime_14,sample_time_15 as SampleTime_15,sample_time_16 as SampleTime_16"
            + ",sample_time_17 as SampleTime_17,sample_time_18 as SampleTime_18,sample_time_19 as SampleTime_19,sample_time_20 as SampleTime_20"
            + ",sample_time_21 as SampleTime_21,sample_time_22 as SampleTime_22,sample_time_23 as SampleTime_23,sample_time_24 as SampleTime_24"
            + ",sample_time_25 as SampleTime_25,sample_time_26 as SampleTime_26,sample_time_27 as SampleTime_27,sample_time_28 as SampleTime_28"
            + ",sample_time_29 as SampleTime_29,sample_time_30 as SampleTime_30,sample_time_31 as SampleTime_31,sample_time_32 as SampleTime_32"
            + ",sample_time_33 as SampleTime_33,sample_time_34 as SampleTime_34,sample_time_35 as SampleTime_35,sample_time_36 as SampleTime_36"
            + ",sample_time_37 as SampleTime_37,sample_time_38 as SampleTime_38,sample_time_39 as SampleTime_39,sample_time_40 as SampleTime_40"
            + ",sample_times as SampleTimes,sample_volume as SampleVolume,auto_fluid_infusion as Auto_Fluid_Infusion"
            + ",first_filter_volume as First_Filter_Volume,method_time as Method_time,speed_mode as SpeedMode,electricmotor_mode as ElectricMotorMode"
            + ",frontrow_starttime_1 as FrontRowStartTime_1,frontrow_starttime_2 as FrontRowStartTime_2"
            + ",backrow_starttime_1 as BackRowStartTime_1,backrow_starttime_2 as BackRowStartTime_2"
            + ",dilution_enabled as DilutionEnabled,dilution_ratio as DilutionRatio"
            + " FROM " + T_TESTDATA //查询实验记录
            + " WHERE {0} ORDER BY method_time DESC";
        public static string SQL_R_TESTDATABYSTARTTIME = "SELECT id FROM " + T_TESTDATA
                                           + " WHERE "
                                           + "starttime = '{0}'";

        public static string SQL_D_TESTDAT = "DELETE FROM " + T_TESTDATA;

        public static string SQL_R_EXPCUPTEMPBYEXPID = "SELECT * FROM " + T_EXPCUPTEMP
                                           + " WHERE exp_id={0}";
        public static string SQL_D_EXPCUPTEMP = "DELETE FROM " + T_EXPCUPTEMP;

        public static string SQL_C_EXPCUPTEMP = "INSERT INTO tbl_expcuptemp(exp_id,sampletime,sampletimepoint{0}) VALUES({1},{2},{3}{4})";
        #endregion


        #region 角色表
        public static string SQL_C_ROLE = "INSERT INTO " + T_ROLE
                                             +"(name,create_time,remark"
                                             + ") VALUES ("
                                             + "'{0}','{1}','{2}')";
        public static string SQL_Q_ROLE = "SELECT * FROM " + T_ROLE
                                          + " WHERE "
                                          + " name<>'超级管理员' AND name != 'Root'";
        public static string SQL_D_ROLE = "DELETE FROM " + T_ROLE
                                         + " WHERE id={0}";
        public static string SQL_R_FINDROLEBYNAME = "SELECT * FROM " + T_ROLE
                                             + " WHERE "
                                             + "name ='{0}'";
        #endregion

        public static string SQL_Q_FINDUSERBYKEY = "SELECT * FROM " + T_USER
                                      + " WHERE "
                                      + "login_name='{0}'";
        public static string SQL_Q_FINDUSERBYROLEIDKEY = "SELECT * FROM " + T_USER + " A," + T_ROLE + " B"
                                              + " WHERE "
                                              + "A.role_id=B.id AND A.login_name='{0}'";

        #region 用户表
        /// <summary>
        /// 用户表操作
        /// </summary>
        public static string SQL_C_USER = "INSERT INTO " + T_USER //创建用户
                                 + "(login_name,login_pwd,username"
                                 + ",mobile,email,role_id,status,create_time,valid_date,last_login_time,login_times,pwd_error_times,pwd_review_datetime"
                                 + ") VALUES ("
                                 + "'{0}','{1}','{2}'"
                                 + ",'{3}','{4}',{5},'{6}','{7}','{8}','{9}',{10},{11},'{12}'"
                                 + ")";
        public static string SQL_U_USER = "UPDATE " + T_USER      //更新用户（param id)
                                 + " SET "
                                 + "login_name ='{0}',login_pwd='{1}',username='{2}'"
                                 + ",mobile='{3}',email='{4}',role_id={5},status='{6}',valid_date='{7}',pwd_error_times={8}"
                                 + " WHERE "
                                 + " id = {9}";
        public static string SQL_R_USER = "SELECT "
                                          +"A.id as ID,A.login_name as LoginName,A.login_pwd as LoginPwd"
                                          +",A.username as UserName,A.mobile as Mobile,A.email as Email"
                                          + ",A.role_id as RoleID,B.name as RoleName,A.status as Status"
                                          +",A.create_time as CreateTime,A.valid_date as ValidDate"
                                          +",A.last_login_time as LastLoginTime,A.login_times as LoginTimes"
                                          +",A.pwd_error_times as PwdErrorTimes,A.pwd_review_datetime as PwdReviewDatetime"
                                          + " FROM " + T_USER + " A," + T_ROLE + " B"  //查询用户
                                              + " WHERE "
                                              + "A.role_id=B.id AND A.role_id<>1";
        public static string SQL_R_USERBYACCOUNT = "SELECT "
                                          + "A.id as ID,A.login_name as LoginName,A.login_pwd as LoginPwd"
                                          + ",A.username as UserName,A.mobile as Mobile,A.email as Email"
                                          + ",A.role_id as RoleID,B.name as RoleName,A.status as Status"
                                          + ",A.create_time as CreateTime,A.valid_date as ValidDate"
                                          + ",A.last_login_time as LastLoginTime,A.login_times as LoginTimes"
                                          + ",A.pwd_error_times as PwdErrorTimes,A.pwd_review_datetime as PwdReviewDatetime"
                                          + " FROM " + T_USER + " A," + T_ROLE + " B"  //查询用户
                                          + " WHERE "
                                          + "A.role_id=B.id AND A.login_name='{0}' AND A.login_pwd='{1}'";
        public static string SQL_R_USERBYROLEID = "SELECT id as ID FROM " + T_USER
                                          + " WHERE role_id={0}";
        public static string SQL_R_ACCOUNTIFEXIST = "SELECT "     //检查账户是否存在
                                          + "* "
                                          + "FROM " + T_USER
                                          + " WHERE "
                                          + "login_name='{0}'";
        public static string SQL_R_ACCOUNTIFEXPIRE = "SELECT "     //检查账户是否过期
                                          + "valid_date "
                                          + "FROM " + T_USER
                                          + " WHERE "
                                          + "login_name='{0}'";
        public static string SQL_D_ACCOUNTBYID = "DELETE FROM " + T_USER  //删除用户
                                 + " WHERE "
                                 + "id = {0} AND login_name NOT IN ('"+StaticParam.ROOT_LOGINNAME+"','"+StaticParam.SYSMANAGE_LOGINNAME+"','root')";
        public static string SQL_D_ALLACCOUNT = "DELETE FROM " + T_USER     //删除用户，但保留超级管理员和内置系统管理员账户
                                 + " WHERE "
                                 + " login_name NOT IN ('root','"+StaticParam.ROOT_LOGINNAME+"','"+StaticParam.SYSMANAGE_LOGINNAME+"')";

        public static string SQL_U_USERPWDBYACCOUNT = "UPDATE " + T_USER
                                          + " SET login_pwd='{0}',pwd_review_datetime='{1}'"
                                          + " WHERE login_name='{2}'"
                                          + " AND login_pwd='{3}'";
        public static string SQL_U_USERLASTLOGINTIME = "UPDATE " + T_USER
                                          + " SET last_login_time='{0}'"
                                          + " WHERE login_name = '{1}' AND login_pwd = '{2}'";
        public static string SQL_U_USERLOGINTIMES = "UPDATE " + T_USER
                                          + " SET login_times = login_times + 1"
                                          + " WHERE login_name='{0}' AND login_pwd='{1}'";
        public static string SQL_U_RETRIVERSYSMANAGE = "UPDATE " + T_USER
                                          + " SET mobile='',email='',last_login_time='',login_times = 0"
                                          + " WHERE login_name='sysmanage'";
        public static string SQL_U_USERSTATUS = "UPDATE " + T_USER
                                          + " SET status='{0}' WHERE login_name='{1}'";

        #endregion

        #region 模块表
        public static string SQL_R_MODULE = "SELECT * FROM " + T_MODULE
                                          + " WHERE "
                                          + " inner=0";
        public static string SQL_R_MODULEALL = "SELECT * FROM " + T_MODULE
                                          + " ORDER BY id ASC";

        #endregion

        #region 功能表
        public static string SQL_R_FUNCTiON = "SELECT * FROM " + T_FUNCTION
            + " WHERE "
            + " valid = true";
        #endregion


        #region  权限表
        public static string SQL_C_PRIVELEGE = "INSERT INTO "+ T_PRIVELEGE
                                 + "(role_id,privelege_name,type"
                                 + ") VALUES ("
                                 + "{0},'{1}',{2}"
                                 + ")";

        public static string SQL_R_PRIVELEGE = "SELECT id as ID,role_id as RoleID,privelege_name as PrivelegeName,type as Type FROM " + T_PRIVELEGE
                                 + " WHERE "
                                 + "role_id={0}";

        public static string SQL_D_PRIVELEGE = "DELETE FROM " + T_PRIVELEGE
                                 + " WHERE "
                                 + "role_id={0}";
        #endregion


        #region 工作日志表
        public static string SQL_C_WORKLOG = "INSERT INTO " + T_WORKLOG
                                 + "(account,type,behavior,actiontime,remark"
                                 + ") VALUES ("
                                 + "'{0}','{1}','{2}','{3}','{4}'"
                                 + ")";
        public static string SQL_R_WORKLOG = "SELECT rowid as SID,id as ID,account as LoginName,type as LogType,behavior as Behavior,actiontime as ActionTime"
                                 + ",remark as Remark"
                                 + " FROM " + T_WORKLOG
                                 + " WHERE {0} "
                                 + " ORDER BY actiontime DESC";
        public static string SQL_D_WORKLOG = "DELETE FROM " + T_WORKLOG;


        public static string SQL_R_WORKLOGBYACCOUNT = "SELECT id as ID,account as LoginName,type as LogType,behavior as Behavior,actiontime as ActionTime"
                                 + ",remark as Remark"
                                 + " FROM " + T_WORKLOG
                                 + " WHERE "
                                 + " account ='{0}' "
                                 + " ORDER BY actiontime DESC";
        public static string SQL_R_WORKLOGBYDATE = "SELECT id as ID,account as LoginName,type as LogType,behavior as Behavior,actiontime as ActionTime"
                                 + ",remark as Remark"
                                 + " FROM " + T_WORKLOG
                                 + " WHERE "
                                 + "datetime(actiontime) BETWEEN datatime('{0}') AND datatime('{1}')"
                                 + " ORDER BY actiontime DESC";

        public static string SQL_R_LOGTYPE = "SELECT id as ID,name as LogTypeName,remark as Remark"
                                 + " FROM " + T_LOGTYPE
                                 + " ORDER BY id ASC";
        public static string SQL_R_LOGTYPEBYNAME = "SELECT id as ID,name as LogTypeName,remark as Remark"
                                 + " FROM " + T_LOGTYPE
                                 + " WHERE "
                                 + " name='{0}'";

        public static string SQL_C_LOGTYPE = "INSERT INTO "+ T_LOGTYPE
                                         +"(name,remark"
                                         +") VALUES( "
                                         + "'{0}','{1}')";
        


        //行为类型
        public static string SQL_C_BEHAVIOR = "INSERT INTO " + T_BEHAVIOR
                                          + "(name,logid,remark"
                                          + ") VALUES("
                                          + "'{0}',{1},'{2}')";
        public static string SQL_R_BEHAVIOR = "SELECT id as ID,name as BehaviorName,logid as LogID,remark as Remark"
                                         + " FROM " + T_BEHAVIOR
                                         + " ORDER BY id ASC";
        public static string SQL_R_BEHAVIORBYNAME = "SELECT id as ID,name as BehaviorName,logid as LogID,remark as Remark"
                                         + " FROM " + T_BEHAVIOR
                                         + " WHERE "         
                                         + " name='{0}'";
        public static string SQL_R_BEHAVIORBYLOGID = "SELECT id as ID,name as BehaviorName,logid as LogID,remark as Remark"
                                         + " FROM " + T_BEHAVIOR
                                         + " WHERE "
                                         + " logid={0}";
        //标定
        public static string SQL_R_TEMPCALI = "SELECT cup_id as CupID,name as Name,cali as Cali"
                                            + " FROM "+ T_TEMPCALI
                                            + " WHERE type={0} ORDER BY cup_id ASC";    
        public static string SQL_U_TEMPCALI = "UPDATE " + T_TEMPCALI
                                             + " SET cali={0}"
                                             + " WHERE cup_id = {1} AND type = {2}";

        #endregion
        #region 仪器验证表

        public static string SQL_C_VERIFYDATA = "INSERT INTO " + T_VERIFYDATA
                                             + "(login_name,verifytime,nextverifytime,status,remark) "
                                             + "VALUES('{0}','{1}','{2}','{3}','{4}')";
        public static string SQL_R_VERIFYDATA = "SELECT id as ID,login_name as LoginName,verifytime as VerifyTime"
                                             + ",nextverifytime as NextVerifyTime,status as Status,remark as Remark"
                                             + " FROM " + T_VERIFYDATA
                                             + " ORDER BY id DESC";
        public static string SQL_D_VERIFYDATA = "DELETE FROM " + T_VERIFYDATA;
        #endregion
        public SQL() { }
    }
}
