namespace Pharmacy.INST.DissolutionClient.common
{
    public class LangPackage
    {
        private bool m_EngVer;
        #region 定义
        #region 通用
        public string TIP;
        public string ERROR;
        public string WARNING;
        public string BTN_CONFIRM;
        public string BTN_CANCEL;
        #endregion
        #region App
        public string TIP_TANK_TEMP_CALI_ERROR;
        public string TIP_CUP_TEMP_CALI_ERROR;
        public string TIP_SYSTEM_PROCESS_RUNNING;
        public string TIP_GETDBFILE_ERROR;
        public string TIP_INIT_FILE_ERROR;
        public string TIP_REGISTER_SUCCESS;
        public string TIP_REGISTER_FAILURE;
        public string TIP_SYSTEM_TIME_ERROR;
        public string TIP_SYSTEM_REGISTER_EXPIRE;
        public string TIP_MAINBOARD_COMM_ERROR;
        public string TIP_INITIALIZING;
        public string TIP_CLEANDB_OVER;
        public string TIP_APPLICATION_RUNNING;
        public string TIP_NO_FUNC_AUTHORITY;

        #endregion
        #region 登录
        public string AppVersionIcon;
        public string TIP_INPUT_ACCOUNT;
        public string TIP_ACCOUNT_EXSIT_ERRORCHAR;
        public string TIP_ACCOUNT_NOTEXSIT_INVALID;
        public string TIP_ACCOUNT_EXPIRE;
        public string TIP_PASSWORD_EXPIRE;
        public string TIP_ACCOUNT_LOCKED;
        public string TIP_HAVE_NTIMES_PASSWORD;
        #endregion
        #region 主窗口
        //主窗口
        public string LB_SYSTEM_TITLE;
        public string m_labelLoginNameTip;
        public string m_labelRoleNameTip;
        public string LB_SYS_STATUS_TIP;
        public string LB_DB_STATUS_TIP;
        public string LB_PRINTER_STATUS_TIP;
        public string LB_COUNT_STATUS_TIP;
        public string Tab_MainView;
        public string Tab_DataSearchView;
        public string Tab_WorkLogView;
        public string Tab_InstrumentVerifyView;
        public string Tab_DataBackupView;
        public string Tab_DeviceInfoView;
        public string Tab_TechSupportView;
        public string Tab_VideoMonitorView;
        public string Tab_FilterHeadExchView;
        public string Tab_SystemManageView;
        public string Tab_AboutView;
        //提示
        public string TIP_EXIT_CONFRIM;
        public string TIP_NO_EXIT;
        public string TIP_NO_OPERATE;
        public string TIP_NO_WASHINGSET;
        public string TIP_WASHING;
        public string TIP_LAUNCH_INIT_SUCCESS;
        public string TIP_INIT_FAILURE;
        public string TIP_MAINBOARD_CHECKING;
        public string TIP_DB_CHECKING;
        public string TIP_PRINT_CHECKING;
        public string TIP_OPERATE_IN_CONSOLE;
        public string TIP_SETTING_SUCCESS;
        public string TIP_TIMER_REMOVE;
        public string TIP_LAUNCH_HEAT;
        #endregion
        #region 主控台
        //主控台
        public string MainView_BasePara_GroupBox;                //基本参数面板
        public string LB_PARA1_METHODNAME;
        public string PARA1_METHODNAME;
        public string LB_PARA1_BATCHNO;
        public string PARA1_BATCHNO;
        public string LB_PARA1_MANUFACTURER;
        public string PARA1_MANUFACTURER;
        public string LB_PARA1_TESTDEPARTMENT;
        public string PARA1_TESTDEPARTMENT;
        public string MainView_DissolutionPara_GroupBox;         //溶出参数面板
        public string LB_PARA2_COMBO_DISSOLUTIONMETHODNAME;
        public string PARA2_COMBO_DISSOLUTIONMETHODNAME;
        public string LB_PARA2_SOLUTIONVOLUME;
        public string PARA2_SOLUTIONVOLUME;
        public string LB_PARA2_TEMPSETTING;
        public string PARA2_TEMPSETTING;
        public string LB_PARA2_BTN_SPEEDSETTING;
        public string LB_PARA2_ALLTIMESPAN;
        public string PARA2_ALLTIMESPAN;
        public string LB_PARA2_BTN_SAMPLETIME;
        public string PARA2_BTN_SPEEDSETTING;
        public string PARA2_BTN_SAMPLETIME;
        public string MainView_SamplePara_GroupBox;              //取样参数面板
        public string LB_PARA3_SAMPLEVOLUME;
        public string PARA3_SAMPLEVOLUME;
        public string LB_PARA3_SAMPLETIMES;
        public string PARA3_SAMPLETIMES;
        public string LB_PARA3_COMBO_AUTOSUPPLY;
        public string LB_PARA3_FIRSTFILTERVOLUME;
        public string PARA3_FIRSTFILTERVOLUME;
        public string LB_PARA3_CHK_DILUTIONENABLED;
        public string LB_PARA3_COMBO_DILUTIONRATIO;
        public string LB_PARA3_COMBO_DILUTIONRATIO_UNIT;
        public string LB_PARA3_SAMPLETIMES_UNIT;
        public string MainView_Operator_GroupBox;                //操控面板
        public string MainView_HDA_GroupBox;                     //机头升降
        public string MainView_SAMPLEPOINT_GroupBox;             //取样针升降
        public string MainView_Proprotor_GroupBox;               //搅拌桨旋转
        public string MainView_FillTablet_GroupBox;
        public string BTN_SAVEMETHOD;
        public string BTN_LAUNCHPARAM;
        public string BTN_CALLMETHOD;
        public string BTN_CALLCLEARMETHOD;
        public string BTN_EXPSTART;
        public string BTN_EXPPAUSE;
        public string BTN_CONT;
        public string BTN_EXPEND;
        public string BTN_EXPENDING;
        public string MainView_RunningStatus_GroupBox;           //工作状态面板
        public string LB_RT_METHODNAME;
        public string LB_RT_BATCHNO;
        public string LB_RT_MANUFACTURER;
        public string LB_RT_WATERBOXTEMP;
        public string LB_RT_DISSOLUTIONMETHODNAME;
        public string LB_RT_TESTDEPARTMENT;
        public string LB_RT_FRONTROW_SPEED_1;
        public string LB_RT_FRONTROW_SPEED_2;
        public string LB_RT_ALLTIMESPAN;
        public string LB_RT_BACKROW_SPEED_1;
        public string LB_RT_REMAINTIME;
        public string LB_RT_BACKROW_SPEED_2;
        public string LB_RT_CURRENT_SAMPLE_TIME;
        public string LB_RT_TEMPSETTING;
        public string LB_RT_NEXT_SAMPLE_TIME;
        public string LB_RT_SAMPLEVOLUME;
        public string LB_RT_SAMPLETIMES;
        public string LB_RT_SAMPLETIMES_1;
        public string LB_RT_SAMPLETIMES_2;
        public string LB_RT_SAMPLETIMES_3;
        public string LB_RT_AUTOFLUIDINFUSION;
        public string LB_RT_FIRSTFILTERVOLUME;
        public string LB_RT_SOLUTIONVOLUME;
        public string MainView_CupTemp_GroupBox;
        public string LB_RT_DILUTIONENABLED;
        public string LB_RT_DILUTIONRATIO;
        public string LB_TEST_STEP;
        public string LB_IMG_L_INIT;
        public string LB_IMG_L_UPTEMP;
        public string LB_IMG_L_WAITPUTPILL;
        public string LB_IMG_L_IMPELLERTURN;
        public string LB_IMG_L_DISSOLUTIONWORK;
        public string LB_IMG_L_SAMPLINGWORK;
        public string LB_IMG_L_COLLECTORWORK;
        public string LB_IMG_L_SAMPLINGOVER;
        public string LB_IMG_L_EXPERIMENTOVER;
        public string LB_TEST_STATUS;

        public string TIP_LOAD_METHOD_FAILURE;
        public string TIP_PARAMS_SEND_FAILURE;
        public string TIP_NOPARAMS_NO_OPERATE;
        public string TIP_SAMPLING_NO_PAUSE;
        public string TIP_TERMINATE_CONFIRM;
        public string TIP_TEMP_SETTING_OUTLINE;
        public string TIP_TEMP_SETTING_FORMAT_FAILURE;
        public string TIP_SOLVENT_SETTING_OUTLINE;
        public string TIP_SOLVENT_SETTING_FORMAT_FAILURE;
        public string TIP_SAMPLE_VOLUME_SETTING_OUTLINE;
        public string TIP_SAMPLE_VOLUME_SETTING_FORMAT_FAILURE;
        public string TIP_SAMPLE_TIMES_SETTING_OUTLINE;
        public string TIP_SAMPLE_TIMES_SETTING_FORMAT_FAILURE;
        public string TIP_FIRSTFILTER_SETTING_OUTLINE;
        public string TIP_FIRSTFILTER_SETTING_FORMAT_FAILURE;
        public string TIP_SAMPLE_TIME_SETTING_ERROR;
        public string TIP_ALLTIME_SETTING_OUTLINE;
        public string TIP_ALLTIME_SETTING_SCOPE_ERROR;
        public string TIP_ALLTIME_SETTING_FORMAT_FAILURE;
        public string TIP_ROTATESPEED_SETTING_EXSIT_NULL;
        public string TIP_ROTATESPEED_FRONTROW1_SETTING_NOTZORE;
        public string TIP_ALLTIME_SETTING_MISMATCHING_BACKROW_STARTTIME;
        public string TIP_SAMPLEVOLUME_ADD_FIRSTFILTER_OUTLINE;
        public string TIP_SAMPLEVOLUME_ADD_FIRSTFILTER_FORMAT_ERROR;
        public string TIP_DISOLUTION_RATIO_NULL;
        public string TIP_DISOLUTION_RATIO_OUTLINE;
        public string TIP_LAUNCH_DISOLUTION_SAMPLEVOLUME_SCOPE;
        public string TIP_ALLTIME_SETTING_MISMATCHING_DISOLUTION;
        public string TIP_CREATE_METHOD_CONFIRM;
        public string TIP_UPDATE_METHOD_CONFIRM;
        public string TIP_TEMPINCUP_UNQUALIFIED;
        public string TIP_TEMPINTANK_UNQUALIFIED;
        public string TIP_START_N_EXP_PREPARE;
        public string TIP_START_N_EXP_SAMPLE;
        public string TIP_START_INIT;
        public string TIP_START_INIT_FAILURE;
        public string TIP_INIT_SUCCESS;
        public string TIP_EXP_OVER;
        public string TIP_PREPARE_BEFORE_SAMPLE;
        public string TIP_PREPARE_BEFORE_SAMPLE_OVER;
        public string TIP_THIS_SAMPLE_OVER;
        public string TIP_START_DISOLUTION;
        public string TIP_START_N_DISOLUTION;
        public string TIP_THIS_DISOLUTION_OVER;
        public string TIP_OUTLET_LEVEL;
        public string TIP_CIRCLE_LEVEL;
        public string TIP_FLUID_INFUSION_LEVEL;
        public string TIP_SAMPLE_LEVEL;
        #endregion
        #region 数据检索
        public string DSV_GROUPBOX;
        public string LB_DSV_DP_METHODDATE_SATRT;
        public string LB_DSV_DP_METHODDATE_END;
        public string DSV_CKB_DATETIMEENABLE;
        public string LB_DSV_CKB_STATUS;
        public string DSV_CKB_PAUSEEXP;
        public string DSV_CKB_WHOLEEXP;
        public string LB_DSV_METHODNAME;
        public string LB_DSV_MANUFACTURER;
        public string LB_DSV_TB_ACCOUNT;
        public string LB_DSV_BATCHNO;
        public string LB_DSV_TESTDEPARTMENT;
        public string DSV_BTN_SEARCH;
        public string DSV_BTN_EMPTY;

        public string TIP_FILE_NOTEXSIT;
        public string TIP_SELECT_FILE;
        
        #endregion
        #region 工作日志
        public string WLV_GROUPBOX;
        public string LB_WLV_DP_METHODDATE_SATRT;
        public string LB_WLV_DP_METHODDATE_END;
        public string WLV_CKB_DATETIMEENABLE;
        public string LB_WLV_TB_ACCOUNT;
        public string LB_WLV_COMBO_LOGTYPE;
        public string LB_WLV_COMBO_BEHAVIOR;
        public string WLV_BTN_SEARCH;
        public string WLV_BTN_EMPTY;
        public string WLV_BTN_EXPORT;
        public string LV_WORKLOG_COL_ID;
        public string LV_WORKLOG_COL_USER;
        public string LV_WORKLOG_COL_LOGTYPE;
        public string LV_WORKLOG_COL_BEHAVIOR;
        public string LV_WORKLOG_COL_TIME;
        public string LV_WORKLOG_COL_NOTE;

        public string TIP_EXPORTFILE_TITLE;
        public string TIP_NODATA_EXPORT;
        public string TIP_EXPORT_LOG_SUCCESS;
        public string TIP_EXPORT_LOG_FAILURE;
        #endregion
        #region 仪器验证
        public string IVV_GROUPBOX;
        public string LB_IVV_RB_VERIFY;
        public string LB_IVV_DP_VERIFYDATE;
        public string LB_IVV_TB_VALIDDAYS;
        public string IVV_UNIT_DAYS;
        public string LB_IVV_TB_REMARK;
        public string IVV_RB_VERIFY;
        public string IVV_BTN_CONFIRM;
        public string IVV_BTN_SEARCH;
        public string IVV_BTN_EMPTY;
        public string TIP_IVV_VERIFY_SUCCESS;
        public string TIP_IVV_NOSET_VERIFYTIME;
        public string TIP_IVV_NOSET_VERIFYSTATUS;
        public string TIP_VERIFY_STATUS;
        public string TIP_UNVERIFY_STATUS;

        #endregion
        #region 数据备份
        public string DBV_GROUPBOX;
        public string DBV_BACKUP_GROUPBOX;
        public string DBV_RESTORE_GROUPBOX;
        public string LB_DBV_TB_BACKUPDIR;
        public string LB_DBV_TB_RESTOREFILE;
        public string DBV_BTN_BACKUP_EXPLORER;
        public string DBV_BTN_BACKUP;
        public string DBV_BTN_RESTORE_EXPLORER;
        public string DBV_BTN_RESTORE;
        public string TIP_DBV_RESTORE_WARNING;
        public string TIP_DBV_RESTORE_SUCCESS;
        public string TIP_DBV_BACKUP_FAILURE;
        public string TIP_DBV_RESTORE_FAILURE;
        public string TIP_DBV_BACKUP_SUCCESS;
        #endregion
        #region 设备信息
        public string DIV_TEMPCALI_GROUPBOX;
        public string DIV_GATHER_GROUPBOX;
        public string LB_DIV_WATERBOXTEMP;
        public string LB_DIV_TB_WATERBOXTEMP_CALI;
        public string DIV_BTN_GATH_WATERBOX_TEMP;
        public string DIV_BTN_CALIBRATION_WATERBOX_RESET;
        public string DIV_BTN_CALIBRATION_WATERBOX;
        public string LB_DIV_CUPTEMP;
        public string LB_DIV_GATHERVAL;
        public string LB_DIV_SURVEYVAL;
        public string DIV_BTN_GATHERTMP;
        public string DIV_BTN_CALIBRATION_CUP_RESET;
        public string DIV_BTN_CALIBRATION_CUP;
        public string DIV_BTN_VOLUMECALIBRATION_RESET;
        public string DIV_BTN_FIRSTSTEP;
        public string DIV_BTN_PUSHOUTLETWATER;
        public string DIV_BTN_SUCK;
        public string DIV_BTN_PUTVOLUME;
        public string DIV_BTN_OVER;
        public string TIP_DIV_CALI_RESET;
        public string TIP_DIV_SUCK_MAX;
        public string TIP_DIV_0P1ML;
        public string OP_VOLUME_INPUT;
        public string REAL_VOLUME_INPUT;
        public string TIP_DIV_GROUP1;
        public string TIP_DIV_GROUP2;
        public string TIP_DIV_STANDARD_COE;
        public string DIV_BTN_VOLUMECALIBRATION;
        public string NOTE_TITLE;
        public string NOTE_STEP_1;
        public string NOTE_STEP_2;
        public string NOTE_STEP_3;
        public string NOTE_STEP_4;

        public string TIP_DIV_TANK_TEMPCALI_RESET_SUCCESS;
        public string TIP_DIV_CALI_ISNULL;
        public string TIP_DIV_TANK_CALI_OUT_OF_LIMIT;
        public string TIP_DIV_TANK_CALI_SUCCESS;
        public string TIP_DIV_INPUT_FORMAT_ERROR;
        public string TIP_DIV_CUP_TEMPCALI_RESET_SUCCESS;
        public string TIP_DIV_CUP_TEMP_EXSIT_NULL;
        public string TIP_DIV_CUP_CALI_SUCCESS;
        public string TIP_VOLUME_CALI_RESET;
        public string TIP_WASTE_LIQUID_VOLUME_OUT_OF_LIMIT;
        public string TIP_OPERATING_VOLUME_OUT_OF_LIMIT;
        public string TIP_CALI_ISNOT_NULL;
        public string TIP_CALI_VOLUME_SUCCESS;
        #endregion
        #region 技术支持
        public string TSV_DISSOLUTION_GROUPBOX;
        public string TSV_SAMPLE_GROUPBOX;
        public string TSV_GATHER_GROUPBOX;
        public string LB_TSV_COMBO_DISSOLUTIONMETHODNAME;
        public string TSV_BTN_CALIBRATION_RESET;

        public string LB_TB_OPERATE_HEIGHT;
        public string LB_TB_REALITY_HEIGHT;
        public string LB_GROUP_1;
        public string LB_GROUP_2;

        public string BTN_CONFIRM_HEIGHT;
        public string BTN_SAMPLEPOINTDOWN;
        public string BTN_SAMPLEPOINTUP;
        public string BTN_SAVE_KB;
        public string LB_TB_CONTAINER_MARK;
        public string TSV_BTN_SAMPLEVERIFY;

        public string LB_DISSOLUTION_DU;
        public string LB_TB_SWIVEL_TEST;
        public string BTN_TEST_SWIVEL_START;
        public string BTN_TEST_SWIVEL_END;
        public string LB_TB_UPDOWN_TEST;
        public string LB_UNIT_CI_1;
        public string BTN_TEST_UPDOWN_START;
        public string BTN_TEST_UPDOWN_END;
        public string LB_TB_SAMPLE_POINT;
        public string LB_UNIT_CI_2;
        public string BTN_TEST_SAMPLE_POINT_START;
        public string BTN_TEST_SAMPLE_POINT_END;

        public string LB_SAMPLE_DU;
        public string LB_TB_PUSHSOLID_TEST;
        public string BTN_TEST_PUSHSOLID_START;
        public string BTN_TEST_PUSHSOLID_END;
        public string BTN_TEST_PULLSOLID_START;
        public string LB_TB_VALVE_TEST;
        public string LB_UNIT_HAO_1;
        public string BTN_TEST_VALVE_START;
        public string BTN_TEST_VALVE_END;

        public string LB_GATHER_DU;
        public string LB_TB_OUTPOINT_TEST;
        public string LB_UNIT_CI_3;
        public string BTN_TEST_OUTPOINT_START;
        public string BTN_TEST_OUTPOINT_END;
        public string LB_TB_SUPPORTARMPOS_TEST;
        public string LB_UNIT_HAO_2;
        public string BTN_TEST_SUPPORTARMPOS_START;
        public string BTN_TEST_SUPPORTARMPOS_END;
        public string LB_TB_SUPPORTARM_TEST;
        public string LB_UNIT_CI_4;
        public string BTN_TEST_SUPPORTARM_START;
        public string BTN_TEST_SUPPORTARM_END;
        public string TIP_TSV_PARAMS_INPUT_ERROR;
        public string TIP_TSV_ROTATE_SPEED_LIMIT;
        public string TIP_TSV_TESTTIMES_LIMIT;
        public string TIP_TSV_INPUT_REALHEIGHT_OPHEIGHT;
        public string TIP_TSV_SAVE_SUCCESS;
        public string TIP_TSV_VALVE_LIMIT;
        public string TIP_TSV_TEST_TIMES_LIMIT;
        public string TIP_TSV_SAMPLING_TEST_TIMES_LIMIT;
        public string TIP_TSV_UNSELECTED_METHOD;
        public string TIP_TSV_SOLVENT_VOLUME_OUT_LIMIT;
        public string TIP_TSV_METHOD_NULL;
        public string TIP_TSV_CALI_RESET_SUCCESS;
        #endregion
        #region 系统管理 
        //主窗口
        public string SMV_ACCOUNT_MANAGE;
        public string SMV_PRIVILEGE_MANAGE;
        public string SMV_SYSTEMSETTING_MANAGE;
        public string SMV_RETRIVER_MANAGE;
        //账户管理
        public string UMV_NEWUSER_GROUPBOX;
        public string LB_SMV_TB_LOGINNAME;
        public string LB_SMV_PB_LOGINPWD;
        public string LB_SMV_PB_LOGINPWD_TWICE;
        public string LB_SMV_COMBO_ROLE;
        public string LB_SMV_COMBO_STATUS;
        public string LB_SMV_DP_VALIDDATE;
        public string LB_SMV_TB_USERNAME;
        public string LB_SMV_TB_MOBILE;
        public string LB_SMV_TB_EMAIL;
        public string SMV_BTN_ADD;
        public string SMV_BTN_EMPTY;
        public string LB_SMV_REQUIRED_FIELD;
        public string UMV_USERLIST_GROUPBOX;

        public string TIP_EDIT_EMPTY_CONFIRM;
        public string TIP_ACCOUNT_FORMAT_ERROR;
        public string TIP_ACCOUNT_EXSIT;
        public string TIP_ACCOUNT_ADD_CONFIRM;
        public string TIP_ACCOUNT_SAVE_SUCCESS;
        public string TIP_ACCOUNT_SAVE_FAILURE;
        public string TIP_ACCOUNT_DEL_CONFIRM;
        public string TIP_ACCOUNT_DEL_FAILURE;

        //权限管理
        public string RMV_ROLELIST_GROUPBOX;
        public string SMV_BTN_SAVEPRIVELEGE;
        public string SMV_BTN_RETRIVERPRIVELEGE;
        public string SMV_BTN_ROLE_ADD;
        public string SMV_BTN_ROLE_DELETE;
        public string RMV_MODULELIST_GROUPBOX;
        public string RMV_FUNCTION_GROUPBOX;
        public string SMV_CHK_MODULE_ALL;
        public string SMV_CHK_FUNCTION_ALL;
        public string TIP_SELECT_ROLE;
        public string TIP_ROLE_REVIEW_CONFIRM;
        public string TIP_ROLE_NO_REVIEW;
        public string TIP_ROLE_EXSITROLE;
        public string TIP_ROLE_INPUT;
        public string TIP_ROLE_ADD_CONFIRM;
        public string TIP_ROLE_SAVE_RESTART;
        public string TIP_ROLE_SAVE_FAILURE;
        public string TIP_ROLE_EXSITUSER;
        public string TIP_ROLE_DEL_CONFIRM;
        public string TIP_ROLE_DEL_SUCCESS;
        public string TIP_ROLE_DEL_FAILURE;
        public string TIP_ROLE_SAVE_SUCCESS;
        //系统设置
        public string SMV_SYSTEMSETTING_GROUPBOX;
        public string SMV_MAINBOARD_GROUPBOX;
        public string LB_SMV_COMBO_COMPORT;
        public string LB_SMV_COMBO_COMBAUD;
        public string SMV_PRINTER_GROUPBOX;
        public string LB_SMV_COMBO_PRINTER;
        public string LB_SMV_COMBO_PRINTERPORT;
        public string LB_SMV_COMBO_PRINTCOMBAUD;
        public string SMV_CKB_WHOLEMODE_ENABLE;
        public string SMV_CKB_PUTPILL_ENABLE;
        public string SMV_CKB_SAMPLEPOINT_ENABLE;
        public string SMV_CKB_CUPTEMP_ENABLE;
        public string SMV_CKB_BEEP_ENABLE;
        public string SMV_CKB_DOUBLEMOTOR;
        public string SMV_CKB_AUTOHEAT_ENABLE;
        public string SMV_CKB_TEMP_ENABLE;
        public string LB_SMV_TB_TEMPOFFSET;
        public string LB_SMV_TB_DEFHEATTEMP;
        public string LB_SMV_TB_PWDVALIDDAYS;
        public string LB_SMV_TB_ROTATERATE;
        public string LB_SMV_TB_FIRSTFILTER_OFFSET;
        public string SMV_BTN_SAVESETTING;
        public string TIP_PROFILE_EMPTY;
        public string TIP_PROFILE_ERROR;
        public string TIP_SAVE_RESTART;
        public string LB_SMV_TB_SPEEDOFFSET;
        #endregion
        #region 恢复出厂设置
        public string RFV_LB_WARNING;
        public string SMV_BTN_RETRIVERFACTORY;
        public string TIP_RFV_RETRIVERFACTORY_CONFIRM;
        #endregion
        #region 关于
        public string AV_SYSINFO_GROUPBOX;
        public string AV_AUTHORIZEDINFO_GROUPBOX;
        public string AV_HELP_GROUPBOX;
        public string LB_AV_APPNAME;
        public string LB_AV_APPVERSION;
        public string LB_AV_APPOWNER;
        public string LB_AV_APPINSTALLDATE;
        public string LB_AV_APPDBSIZE;
        public string LB_AV_APPLOGSIZE;
        public string LB_AV_APPREPORTCOUNT;
        public string LB_AV_AuthorizedTo;
        public string LB_AV_TIMELIMIT;
        public string LB_AV_TIMELIMIT_UNIT;
        public string LB_AV_BTN_INPUTSERIALNO;
        public string AV_BTN_INPUTSERIALNO;

        #endregion
        #region print报告
        
        public string RPT_TITLE;
        public string RPT_CREATETIME;
        public string RPT_FILENAME;
        public string RPT_SOFTWARE;
        public string RPT_SOFTWARENAME;
        public string RPT_SOFTWAREVERSION;
        public string RPT_EXP_STATUS;
        public string RPT_STARTTIME;
        public string RPT_ENDTIME;
        public string RPT_USERID;
        public string RPT_METHODNAME;
        public string RPT_LOTNO;
        public string RPT_MANUFACTURER;
        public string RPT_TESTDEPARTMENT;
        public string RPT_TESTMETHOD;
        public string RPT_TANKTEMP;
        public string RPT_SOLVENTVOLUME;
        public string RPT_ALLTIMESPAN;
        public string RPT_FRONT_SPEED_1;
        public string RPT_FRONT_SPEED_2;
        public string RPT_BACK_SPEED_1;
        public string RPT_BACK_SPEED_2;
        public string RPT_SAMPLE_VOLUME;
        public string RPT_SAMPLE_TIME;
        public string RPT_AUTO_FLUID_INFUSION;
        public string RPT_FIRST_FILTER_VOLUME;

        public string RPT_SAMPLEPOINT;
        public string RPT_SAMPLETIME;
        public string RPT_CUPTEMP;
        public string RPT_REVIEWERID;
        public string RPT_REVIEWER;
        public string RPT_CONTENT;
        public string RPT_REPORTTIME;

        #endregion
        #region 实验流程
        public string TIP_TEMP_REACHED_STANDARD;
        public string TIP_TEMP_NOT_REACHED_STANDARD;
        #endregion

        #region 加热对话框
        public string TSM_MW;
        public string LB_TSM_TB_TEMPSETTING;
        public string TSM_TB_STARTHEAT;
        public string TSM_TB_ENDHEAT;
        public string TIP_TSM_TEMP_OUT_LIMIT;
        #endregion
        #region 清洗对话框
        public string WTM_MW;
        public string LB_WTM_TB_WASHINGTIMES;
        public string LB_WASHINGTIME_UNIT;
        public string WTM_BTN_TIMES_1;
        public string WTM_BTN_TIMES_2;
        public string WTM_BTN_TIMES_3;
        public string WTM_BTN_TIMES_5;
        public string WTM_WASHING_STATUS;
       
        public string TIP_WASHING_CONFIRM;
        public string TIP_WASHING_DO_NOT_EXIT;
        public string TIP_INPUT_ERROR;
        public string TIP_WASHING_OVER;
        public string TIP_WASHING_NOT_EXIT;
        public string WTM_NORMALTIMES_GROUPBOX;
        #endregion
        #region 温度监测对话框
        public string TMM_MW;
        public string LowLimit;
        public string NormalTEMP;
        public string UpperLimit;

        #endregion
        #region 报告对话框
        public string RM_MW;
        public string LB_RM_DatePicker;
        public string RM_CHK_ENABLEDATE;
        public string LB_RM_COMBO_REPORTTYPE;
        public string RM_BTN_QUERY;
        public string RM_BTN_PREVIEW;
        public string RM_BTN_EXPORT;
        public string LB_ZoomInButton;
        public string LB_ZoomOutButton;
        public string LB_NormalButton;
        public string LB_FitToHeightButton;
        public string LB_SinglePageButton;
        public string LB_FacingButton;
        public string LB_CloseWindow;
        public string TIP_NO_REPORT;
        public string TIP_SELECT_REPORT;
        public string TIP_EXPORT_ERROR;
        public string TIP_EXPORT_SUCCESS;
        public string TIP_SELECT_EXPORT_REPORT;
        public string TIP_TITLE_NAME_ISNULL;

        #endregion
        #region 倒计时对话框
        public string PTM_MW;
        public string LB_PTM_TB_PREPARETIME;
        public string LB_PTM_TB_PREPARETIME_UNIT;
        public string BTN_CONFRIM;
        public string BTN_CANCLE_PREPARETIME;
        public string BTN_CANCLE;
        public string TIP_SET_TIMER;
        public string TIP_SET_TIMER_ERROR;
        #endregion
        #region 预约时间对话框
        public string ATM_WM;
        public string LB_TB_APPOINTMENTTIME;
        public string LB_TB_APPOINTMENTTIME_UNIT;
        #endregion
        #region 倒计时对话框
        public string CM_WM;
        public string LB_LB_COUNTDOWNBULLETIN;
        public string TIP_COUNTDOWN_EXIT_CONFIRM;
        #endregion
        #region 注册对话框
        public string IM_MW;
        public string LB_INM_TB_SERIALNO;
        public string INM_REGISTER;
        public string INM_CLOSE;
        public string TIP_INM_SN_INVALID;
        #endregion
        #region 转速设置对话框
        public string SSM_MW;
        public string LB_Combo_Speed_Mode;
        public string LB_Combo_Electricmotor_Mode;
        public string LB_TB_Front_Speed_1;
        public string LB_TB_Front_StartTime_1;
        public string LB_TB_Front_Speed_2;
        public string LB_TB_Front_StartTime_2;
        public string LB_TB_Back_Speed_1;
        public string LB_TB_Back_StartTime_1;
        public string LB_TB_Back_Speed_2;
        public string LB_TB_Back_StartTime_2;
        public string Btn_Confirm;
        public string Btn_Close;
        public string TIP_NO_SELECT_SPEED_MODE;
        public string TIP_NO_SELECT_ELECTRICMOTOR_MODE;
        public string TIP_SPEED_STARTTIME_NULL;
        public string TIP_FR_SPEED1_GREATER_THEN_ONE;
        public string TIP_FR_SPEED1_AND_BK_SPEED1_GREATER_THEN_ONE;
        public string TIP_FR_SPEED1_AND_FR_SPEED2_GREATER_THEN_ONE;
        public string TIP_FR_STARTTIME2_GREATER_THEN_FR_STARTTIME1;
        public string TIP_ALL_SPEED_GREATER_THEN_ZERO;
        public string TIP_FR_BK_SPEED2_GRATER_THEN_SPEED1;
        public string TIP_SPEED_LIMITIED;
        public string TIP_SPEED_INPUT_LIMITIED;
        public string TIP_STARTTIME_LIMITIED;
        public string TIP_SSM_INPUT_FORMAT_ERROR;
        #endregion
        #region 取样时间对话框
        public string STM_MW;
        public string STM_SAMPLE_TIMES;
        public string STM_SAMPLE_POS_TIME;
        public string Btn_SaveSampleTime;
        public string Btn_SaveSampleTime_Return;
        public string TIP_STM_SAMPLE_POS_EXSIT_ZERO;
        public string TIP_STM_SAMPLE_INTERVAL_LESS_THEN;
        public string TIP_STM_INPUT_FORMAT_ERROR;
        #endregion
        #region 方法调用
        public string LMM_MW;
        public string LMM_SEARCH_COND_GROUPBOX;
        public string LB_DP_METHODDATE;
        public string CKB_DATETIMEENABLE;
        public string LB_LMM_METHODNAME;
        public string LB_LMM_MANUFACTURER;
        public string LB_LMM_BATCHNO;
        public string LB_LMM_TESTDEPARTMENT;
        public string LMM_BTN_SEARCH;
        public string LMM_BTN_EMPTY;
        public string LMM_BTN_CALLMETHOD;
        public string LMM_BTN_DELMETHOD;
        public string LMM_BTN_CANCEL;
        public string TIP_DELETE_METHOD_CONFIRM;
        public string TIP_DELETE_SUCCESS;
        public string TIP_DELETE_FAILURE;
        //表格变量
        public string LMM_GV_MethodName;
        public string LMM_GV_Manufacturer;
        public string LMM_GV_BatchNo;
        public string LMM_GV_TestDepartment;
        public string LMM_GV_DissolutionMethodName;
        public string LMM_GV_TempSetting;
        public string LMM_GV_SolutionVolume;
        public string LMM_GV_FrontRowSpeed_1;
        public string LMM_GV_FrontRowSpeed_2;
        public string LMM_GV_FrontRowStartTime_1;
        public string LMM_GV_FrontRowStartTime_2;
        public string LMM_GV_BackRowSpeed_1;
        public string LMM_GV_BackRowSpeed_2;
        public string LMM_GV_BackRowStartTime_1;
        public string LMM_GV_BackRowStartTime_2;
        public string LMM_GV_SampleTimes;
        public string LMM_GV_SampleVolume;
        public string LMM_GV_Auto_Fluid_Infusion;
        public string LMM_GV_First_filter_volume;
        public string LMM_GV_AllTimespan;
        public string LMM_GV_TimeValue1;
        public string LMM_GV_TimeValue2;
        public string LMM_GV_TimeValue3;
        public string LMM_GV_TimeValue4;
        public string LMM_GV_TimeValue5;
        public string LMM_GV_TimeValue6;
        public string LMM_GV_TimeValue7;
        public string LMM_GV_TimeValue8;
        public string LMM_GV_TimeValue9;
        public string LMM_GV_TimeValue10;
        public string LMM_GV_TimeValue11;
        public string LMM_GV_TimeValue12;
        public string LMM_GV_TimeValue13;
        public string LMM_GV_TimeValue14;
        public string LMM_GV_TimeValue15;
        public string LMM_GV_TimeValue16;
        public string LMM_GV_TimeValue17;
        public string LMM_GV_TimeValue18;
        public string LMM_GV_TimeValue19;
        public string LMM_GV_TimeValue20;
        public string LMM_GV_TimeValue21;
        public string LMM_GV_TimeValue22;
        public string LMM_GV_TimeValue23;
        public string LMM_GV_TimeValue24;
        public string LMM_GV_TimeValue25;
        public string LMM_GV_TimeValue26;
        public string LMM_GV_TimeValue27;
        public string LMM_GV_TimeValue28;
        public string LMM_GV_TimeValue29;
        public string LMM_GV_TimeValue30;
        public string LMM_GV_TimeValue31;
        public string LMM_GV_TimeValue32;
        public string LMM_GV_TimeValue33;
        public string LMM_GV_TimeValue34;
        public string LMM_GV_TimeValue35;
        public string LMM_GV_TimeValue36;
        public string LMM_GV_TimeValue37;
        public string LMM_GV_TimeValue38;
        public string LMM_GV_TimeValue39;
        public string LMM_GV_TimeValue40;
        public string LMM_GV_MethodTime;
        public string LMM_GV_SpeedMode;
        public string LMM_GV_ElectricmotorMmode;
        #endregion
        #region 签名对话框
        public string SLM_MW;
        public string LB_SLM_TB_LOGINNAME;
        public string LB_SLM_PB_LOGINPWD;
        public string LB_SLM_TB_SIGNCONTENT;
        public string LB_SLM_ONEKEYINPUT;
        public string SLM_BTN_PASS;
        public string SLM_BTN_FAIL;
        public string SLM_BTN_CONFIRM;
        public string SLM_BTN_CANCEL;
        public string TIP_SLM_INPUT_ACCOUNTORPASSWORD;
        public string TIP_SLM_ACCOUNT_CHAR_UNNORMAL;
        public string TIP_SLM_ACCOUNT_NOTEXSIT;
        public string TIP_SLM_ACCOUNT_EXPIRE;
        public string TIP_SLM_PASSWORD_EXPIRE;
        public string TIP_SLM_UPDATE_SIGN_CONFIRM;
        public string TIP_SLM_SIGN_LOGIN_FAILURE;
        public string TIP_SLM_SIGNED_REPORT_SUCCESS;
        public string TIP_SLM_ACCOUNT_ISNOTNULL;
        public string TIP_SLM_REVISION_ISNOTNULL;
        public string TIP_SLM_PLEASE_INPUT_SIGN;

        #endregion
        #region 重连对话框
        public string RMM_MW;
        public string RCM_BTN_CONNECT;
        public string RCM_BTN_EXIT;
        #endregion
        #region 修改口令对话框
        public string RPM_MW;
        public string LB_RPM_PB_LOGINPWD;
        public string LB_RPM_PB_LOGINPWD_TWICE;
        public string RPM_BTN_CONFIRM;
        public string RPM_BTN_CANCEL;
        public string TIP_RPM_TWICE_PASSWORD_CONSISTENT;
        public string TIP_RPM_PASSWORD_REVIEW_SUCCESS;
        #endregion
        #region 实验数据对话框
        public string EDM_MW;
        public string LB_EDM_EXP_STATUS;
        public string LB_EDM_LOGINNAME;
        public string LB_EDM_STARTTIME;
        public string LB_EDM_ENDTIME;
        public string LB_EDM_METHODNAME;
        public string LB_EDM_MANUFACTURER;
        public string LB_EDM_BANCHNO;
        public string LB_EDM_TESTDEPARTMENT;
        public string LB_EDM_DISSOLUTIONMETHODNAME;
        public string LB_EDM_WATERBOXTEMP;
        public string LB_EDM_SOLUTIONVOLUME;
        public string LB_EDM_ALLTIMESPAN;
        public string LB_EDM_FRONTSPEED_1;
        public string LB_EDM_FRONTSPEED_2;
        public string LB_EDM_BACKSPEED_1;
        public string LB_EDM_BACKSPEED_2;
        public string LB_EDM_FRONTSTARTTIME_1;
        public string LB_EDM_FRONTSTARTTIME_2;
        public string LB_EDM_BACKSTARTTIME_1;
        public string LB_EDM_BACKSTARTTIME_2;
        public string LB_EDM_SAMPLEVOLUME;
        public string LB_EDM_SAMPLETIMES;
        public string LB_EDM_AUTOSUPPLY;
        public string LB_EDM_FIRSTFILTERVOLUME;
        public string LB_EDM_Speed_Mode;
        public string LB_EDM_Electricmotor_Mode;
        public string LB_EDM_TEMPSETTING;
        public string LB_EDM_DILUTIONENABLED;
        public string LB_EDM_DILUTIONRATIO;
        public string EDM_BTN_CREATESIGNEDREPORT;
        public string EDM_BTN_CLOSE;
        public string EDM_SAMPLE_TIMES;
        public string EDM_SAMPLE_POINT_TIME;
        public string EDM_CUP_ID;
        #region 报告命名对话框
        public string NRM_MW;
        public string NRM_SELFDEFPARAMS_GROUPBOX;
        public string LB_NRM_REPORTTITLE;
        public string LB_NRM_REPORTNAME;
        #endregion
        #endregion
        #region 用户对话框
        public string UserModalView;
        public string UM_ACCOUNTINFO_GROUPBOX;
        public string LB_UM_TB_LOGINNAME;
        public string LB_UM_PB_LOGINPWD;
        public string LB_UM_PB_LOGINPWD_TWICE;
        public string LB_UM_COMBO_ROLE;
        public string LB_UM_COMBO_STATUS;
        public string LB_UM_DP_VALIDDATE;
        public string LB_UM_TB_USERNAME;
        public string LB_UM_TB_MOBILE;
        public string LB_UM_TB_EMAIL;
        public string TIP_PASSWORD_INCONSISTENCY;
        public string TIP_PASSWORD_FORMAT_ERROR;
        public string TIP_NOTSET_VALIDDATE;
        public string TIP_VALIDDATE_LESSTHAN_CURRENTDATE;
        public string TIP_ROLE_NULL;
        public string TIP_UPDATE_USER_CONFIRM;
        public string TIP_UPDATE_SUCCESS;
        public string TIP_SAVE_FAILURE;


        #endregion
        #endregion
        public bool EngVer { get => m_EngVer; set => m_EngVer = value; }
        public LangPackage() { }
        public LangPackage(bool engver)
        {
            m_EngVer = engver;
        }
        public bool Load()
        {
            if (App.g_EngVer)
            {
                StaticParam.Startup_status_Arr[0] = "OK";
                StaticParam.Startup_status_Arr[1] = "ERROR";

                StaticParam.DissolutionMethod_Arr[0] = "Paddle";
                StaticParam.DissolutionMethod_Arr[1] = "Basket";
                StaticParam.DissolutionMethod_Arr[2] = "Small cup";
                StaticParam.DissolutionMethod_Arr[3] = "Rotating cylinder";
                StaticParam.DissolutionMethod_Arr[4] = "Disk";

                StaticParam.Auto_fluid_infusion_Arr[0] = "Yes";
                StaticParam.Auto_fluid_infusion_Arr[1] = "No";

                StaticParam.Speed_Mode_Arr[0] = "Same RPM";
                StaticParam.Speed_Mode_Arr[1] = "Vary RPM";

                StaticParam.Electricmotor_Mode_Arr[0] = "One";
                StaticParam.Electricmotor_Mode_Arr[1] = "Two";

                StaticParam.ExpStatusType[0] = "Halfway EXP";
                StaticParam.ExpStatusType[1] = "Whole EXP";

                StaticParam.m_TestDataListHeadColumn[0] = "Status";
                StaticParam.m_TestDataListHeadColumn[1] = "Start Time";
                StaticParam.m_TestDataListHeadColumn[2] = "End Time";
                StaticParam.m_TestDataListHeadColumn[3] = "Login Name";
                StaticParam.m_TestDataListHeadColumn[4] = "Method Name";
                StaticParam.m_TestDataListHeadColumn[5] = "Lot No.";
                StaticParam.m_TestDataListHeadColumn[6] = "More...";
 
                StaticParam.m_VerifyDataListHeadColumn[0] = "ID";
                StaticParam.m_VerifyDataListHeadColumn[1] = "Login Name";
                StaticParam.m_VerifyDataListHeadColumn[2] = "Verify Time";
                StaticParam.m_VerifyDataListHeadColumn[3] = "Status";
                StaticParam.m_VerifyDataListHeadColumn[4] = "Next Verify Time";
                StaticParam.m_VerifyDataListHeadColumn[5] = "Remark";

                StaticParam.ReportViewHeadColumn[1] = "File Name";

                StaticParam.ReportSignType[0] = "Unsigned";
                StaticParam.ReportSignType[1] = "Signed";

                StaticParam.m_UserListHeadColumn[0] = "ID";
                StaticParam.m_UserListHeadColumn[1] = "Login Name";
                StaticParam.m_UserListHeadColumn[2] = "User Name";
                StaticParam.m_UserListHeadColumn[3] = "Mobile";
                StaticParam.m_UserListHeadColumn[4] = "Role";
                StaticParam.m_UserListHeadColumn[5] = "Status";
                StaticParam.m_UserListHeadColumn[6] = "Last LoginTime";
                StaticParam.m_UserListHeadColumn[7] = "Login Times";
                StaticParam.m_UserListHeadColumn[8] = "Expire Date";
                StaticParam.m_UserListHeadColumn[9] = "Edit";
                StaticParam.m_UserListHeadColumn[10] = "Del";

                StaticParam.EntitiesStatusArr[0] = "Normal";
                StaticParam.EntitiesStatusArr[1] = "Locked";
                StaticParam.EntitiesStatusArr[2] = "Invalid";

            }
            #region 通用
            TIP = m_EngVer ? "TIP" : "提示";
            ERROR = m_EngVer ? "ERROR" : "错误";
            WARNING = m_EngVer ? "WARNING" : "警告";
            BTN_CONFIRM = m_EngVer ? "OK" : "确定";
            BTN_CANCEL = m_EngVer ? "Cancel" : "取消";
            #endregion
            #region App
            TIP_TANK_TEMP_CALI_ERROR = m_EngVer ? "Initialization water tank temperature calibration error!" : "初始化水箱温度标定错误！";
            TIP_CUP_TEMP_CALI_ERROR = m_EngVer ? "Initialization water cup temperature calibration error!" : "初始化水杯温度标定错误！";
            TIP_SYSTEM_PROCESS_RUNNING = m_EngVer ? "The application is already running!" : "应用程序已经在运行！";
            TIP_GETDBFILE_ERROR = m_EngVer ? "Failed to obtain database file name!" : "获取数据库文件名失败！";
            TIP_INIT_FILE_ERROR = m_EngVer ? "Initialization file error" : "初始化文件错误";
            TIP_REGISTER_SUCCESS = m_EngVer ? "Registration successful, please restart!" : "注册成功，请重新启动！";
            TIP_REGISTER_FAILURE = m_EngVer ? "Registration failed. Please contact the administrator!, Error reason {0}" : "注册失败，请联系管理员！,错误原因{0}";
            TIP_SYSTEM_TIME_ERROR = m_EngVer ? "The system time has changed, and the software cannot continue to be used!" : "系统时间已改变，软件将无法继续使用！";
            TIP_SYSTEM_REGISTER_EXPIRE = m_EngVer ? "The system registration has expired, please contact the manufacturer!" : "系统注册已过期，请联系厂家！";
            TIP_MAINBOARD_COMM_ERROR = m_EngVer ? "Host communication is abnormal, please check!" : "主机通信异常，请检查！";
            TIP_INITIALIZING = m_EngVer ? "Starting initialization, please wait!" : "开机初始化中，请稍后！";
            TIP_CLEANDB_OVER = m_EngVer ? "Successfully cleaned {0} records. After confirmation, the system software will forcibly restart!" : "成功清理{0}条记录,确认后，系统软件将强制重启！";
            TIP_APPLICATION_RUNNING = m_EngVer ? "The application is already running!" : "应用程序已经在运行！";
            #endregion
            #region 登录
            AppVersionIcon = m_EngVer ? "Version: " : "版本：";
            TIP_INPUT_ACCOUNT = m_EngVer ? "Please enter an account or password!" : "请输入账户或口令!";
            TIP_ACCOUNT_EXSIT_ERRORCHAR = m_EngVer ? "Account has abnormal characters, please re-enter!" : "账户存在异常字符，请重新输入!";
            TIP_ACCOUNT_NOTEXSIT_INVALID = m_EngVer ? "The account does not exist or is locked, invalid. Please contact the system administrator!" : "账户不存在或已锁定,失效，请联系系统管理员!";
            TIP_ACCOUNT_EXPIRE = m_EngVer ? "The account has expired, please contact the system administrator!" : "账户已过期，请联系系统管理员!";
            TIP_PASSWORD_EXPIRE = m_EngVer ? "The password has expired, please reset it!" : "口令已过期，请重新设置口令!";
            TIP_ACCOUNT_LOCKED = m_EngVer ? "Your account has been locked, please contact" : "您的账户已锁定，请联系";
            TIP_HAVE_NTIMES_PASSWORD = m_EngVer ? "You have entered an incorrect password {0} times, and the account will be locked after 5 times!" : "您已输入{0}次错误口令，5次后账户将锁定!";
            TIP_NO_FUNC_AUTHORITY = m_EngVer ? "You do not have permission for this function!" : "没有该功能权限！";
            #endregion
            #region 主窗口 MainWindow
            //Tab_MainView = m_EngVer ? "Console" : "主控台";
            //Tab_DataSearchView = m_EngVer ? "Data Query" : "数据检索";
            //Tab_WorkLogView = m_EngVer ? "Worklog" : "工作日志";
            //Tab_InstrumentVerifyView = m_EngVer ? "Validation" : "仪器验证";
            //Tab_DataBackupView = m_EngVer ? "Backup" : "数据备份";
            //Tab_DeviceInfoView = m_EngVer ? "Device Info" : "设备信息";
            //Tab_TechSupportView = m_EngVer ? "Tech. Support" : "技术支持";
            //Tab_VideoMonitorView = m_EngVer ? "Video Monitor" : "视频监控";
            //Tab_FilterHeadExchView = m_EngVer ? "Filter" : "滤头更换";
            //Tab_SystemManageView = m_EngVer ? "Management" : "系统管理";
            //Tab_AboutView = m_EngVer ? "About" : "关于";


            LB_SYSTEM_TITLE = m_EngVer ? "AI Dissolution Workstation" : App.m_strAppTitle;
            m_labelLoginNameTip = m_EngVer ? "User: " : "账户：";
            m_labelRoleNameTip = m_EngVer ? "Role: " : "角色：";

            LB_SYS_STATUS_TIP = m_EngVer ? "Host COM: " : "主机通信：";
           

            LB_DB_STATUS_TIP = m_EngVer ? "Database: " : "数据库：";
            LB_PRINTER_STATUS_TIP = m_EngVer ? "Printer: " : "打印机：";
            LB_COUNT_STATUS_TIP = m_EngVer ? "Timer: " : "计时器：";

            //提示
            TIP_EXIT_CONFRIM = m_EngVer ? "Are you sure you want to exit the system?" : "确定要退出系统吗？";
            TIP_NO_EXIT = m_EngVer ? "In the experiment, you cannot exit!" : "实验中，无法退出！";
            TIP_NO_OPERATE = m_EngVer ? "Unable to operate during the experiment!" : "实验中，无法操作！";
            TIP_NO_WASHINGSET = m_EngVer ? "No cleaning mechanism!" : "无清洗机构！";
            TIP_WASHING = m_EngVer ? "Cleaning..." : "清洗中。。。";
            TIP_LAUNCH_INIT_SUCCESS = m_EngVer ? "Start initialization completed!" : "启动初始化完成！";
            TIP_INIT_FAILURE = m_EngVer ? "Failed to initialize the instrument!" : "初始化仪器失败！";
            TIP_MAINBOARD_CHECKING = m_EngVer ? "Host detection is in progress..." : "正在进行主机检测。。。";
            TIP_DB_CHECKING = m_EngVer ? "Database detection is in progress..." : "正在进行数据库检测。。。";
            TIP_PRINT_CHECKING = m_EngVer ? "Printer detection is in progress..." : "正在进行打印机检测。。。";
            TIP_OPERATE_IN_CONSOLE = m_EngVer ? "Please operate in the console!" : "请在主控台操作！";
            TIP_SETTING_SUCCESS = m_EngVer ? "Set successfully!" : "设置成功！";
            TIP_TIMER_REMOVE = m_EngVer ? "Timer canceled successfully!" : "定时器取消成功！";
            TIP_LAUNCH_HEAT = m_EngVer ? "Start heating!" : "启动加热！";
            #endregion

            #region 主控台
            //主窗口MainView
            //基本参数面板
            MainView_BasePara_GroupBox = m_EngVer ? "Base Params" : "基本参数";
            LB_PARA1_METHODNAME = m_EngVer ? "Name: " : "名      称：";
            LB_PARA1_BATCHNO = m_EngVer ? "Lot NO. : " : "批      号：";
            LB_PARA1_MANUFACTURER = m_EngVer ? "MFR. : " : "生产厂家：";
            LB_PARA1_TESTDEPARTMENT = m_EngVer ? "Test Dept. : " : "检测单位：";
            PARA1_METHODNAME = m_EngVer ? "Name" : "名称";
            PARA1_BATCHNO = m_EngVer ? "Lot NO." : "批号";
            PARA1_MANUFACTURER = m_EngVer ? "MFR." : "生产厂家";
            PARA1_TESTDEPARTMENT = m_EngVer ? "Test Dept." : "检测单位";
            //溶出参数面板
            MainView_DissolutionPara_GroupBox = m_EngVer ? "Dissolution Params" : "溶出参数";
            LB_PARA2_COMBO_DISSOLUTIONMETHODNAME = m_EngVer ? "Method: " : "溶出方法：";
            PARA2_COMBO_DISSOLUTIONMETHODNAME = m_EngVer ? "Method" : "溶出方法";
            LB_PARA2_SOLUTIONVOLUME = m_EngVer ? "SOL VOL: " : "溶媒体积：";
            LB_PARA2_TEMPSETTING = m_EngVer ? "TEMP Set: " : "设定温度：";
            PARA2_TEMPSETTING = m_EngVer ? "TEMP Set" : "设定温度";
            LB_PARA2_BTN_SPEEDSETTING = m_EngVer ? "Speed Set: " : "转速设置：";
            LB_PARA2_ALLTIMESPAN = m_EngVer ? "Duration: " : "总 时 长：";
            PARA2_ALLTIMESPAN = m_EngVer ? "Duration" : "总时长";
            LB_PARA2_BTN_SAMPLETIME = m_EngVer ? "SPL Time: " : "取样时间：";
            PARA2_BTN_SPEEDSETTING = m_EngVer ? "Speed Set" : "转速设置";
            PARA2_BTN_SAMPLETIME = m_EngVer ? "Sample Time" : "取样时间";
            //取样参数面板
            MainView_SamplePara_GroupBox = m_EngVer ? "Sample Params" : "取样参数";
            LB_PARA3_SAMPLEVOLUME = m_EngVer ? "SPL VOL: " : "取样体积：";
            PARA3_SAMPLEVOLUME = m_EngVer ? "Sample volume" : "取样体积";
            LB_PARA3_SAMPLETIMES = m_EngVer ? "SPL Times: " : "取样次数：";
            PARA3_SAMPLETIMES = m_EngVer ? "SPL Times" : "取样次数";
            LB_PARA3_SAMPLETIMES_UNIT = m_EngVer ? "freq" : "次";
            LB_PARA3_COMBO_AUTOSUPPLY = m_EngVer ? "Auto SUP: " : "自动补液：";
            LB_PARA3_FIRSTFILTERVOLUME = m_EngVer ? "Filter VOL: " : "初滤体积：";
            PARA3_FIRSTFILTERVOLUME = m_EngVer ? "Filter VOL" : "初滤体积";
            LB_PARA3_CHK_DILUTIONENABLED = m_EngVer ? "Dilute(?): " : "是否稀释：";
            LB_PARA3_COMBO_DILUTIONRATIO = m_EngVer ? "Dilute Rto: " : "稀释倍数：";
            LB_PARA3_COMBO_DILUTIONRATIO_UNIT = m_EngVer ? "M" : "倍";
            //操控面板
            MainView_Operator_GroupBox = m_EngVer ? "Operator Panel" : "操控面板";
            MainView_HDA_GroupBox = m_EngVer ? "Handpiece Move" : "机头升降";
            MainView_SAMPLEPOINT_GroupBox = m_EngVer ? "Sample Point Move" : "取样针升降";
            MainView_Proprotor_GroupBox = m_EngVer ? "Proprotor Rotate" : "搅拌桨旋转";
            MainView_FillTablet_GroupBox = m_EngVer ? "Fill tablets" : "投药";
            BTN_SAVEMETHOD = m_EngVer ? "Save" : "方法存储";
            BTN_LAUNCHPARAM = m_EngVer ? "Send Params" : "参数发送";
            BTN_CALLMETHOD = m_EngVer ? "Load" : "方法调用";
            BTN_CALLCLEARMETHOD = m_EngVer ? "Clear Params" : "参数清除";
            BTN_EXPSTART = m_EngVer ? "Start" : "开始实验";
            BTN_EXPPAUSE = m_EngVer ? "Pause" : "暂停";
            BTN_CONT = m_EngVer ? "CONT" : "继续";
            BTN_EXPEND = m_EngVer ? "End" : "终止";
            BTN_EXPENDING = m_EngVer ? "Ending" : "终止中";
            //工作状态面板
            MainView_RunningStatus_GroupBox = m_EngVer ? "Running Status" : "工作状态";
            LB_RT_METHODNAME = m_EngVer ? "Name: " : "名　　称：";
            LB_RT_BATCHNO = m_EngVer ? "Lot NO. " : "批　　号：";
            LB_RT_MANUFACTURER = m_EngVer ? "MFR. : " : "生产厂家：";
            LB_RT_WATERBOXTEMP = m_EngVer ? "WTk TEMP: " : "水箱温度：";
            LB_RT_DISSOLUTIONMETHODNAME = m_EngVer ? "Method: " : "溶出方法：";
            LB_RT_TESTDEPARTMENT = m_EngVer ? "Test Dept. : " : "检测单位：";
            LB_RT_FRONTROW_SPEED_1 = m_EngVer ? "FNT SPD 1: " : "前排转速1：";
            LB_RT_FRONTROW_SPEED_2 = m_EngVer ? "FNT SPD 2: " : "前排转速2：";
            LB_RT_ALLTIMESPAN = m_EngVer ? "Duration: " : "总  时  长：";
            LB_RT_BACKROW_SPEED_1 = m_EngVer ? "Back SPD 1: " : "后排转速1：";
            LB_RT_REMAINTIME = m_EngVer ? "Countdown: " : "倒  计  时：";
            LB_RT_BACKROW_SPEED_2 = m_EngVer ? "Back SPD 2: " : "后排转速2：";
            LB_RT_CURRENT_SAMPLE_TIME = m_EngVer ? "Current TM: " : "本次取样时间点：";
            LB_RT_TEMPSETTING = m_EngVer ? "TEMP Set: " : "设定温度：";
            LB_RT_NEXT_SAMPLE_TIME = m_EngVer ? "Next TM: " : "下次取样时间点：";
            LB_RT_SAMPLEVOLUME = m_EngVer ? "SPL VOL: " : "取样体积：";
            LB_RT_SAMPLETIMES = m_EngVer ? "SPL Times: " : "取样次数：";
            LB_RT_SAMPLETIMES_1 = m_EngVer ? " " : "共";
            LB_RT_SAMPLETIMES_2 = m_EngVer ? " ——" : "次，第";
            LB_RT_SAMPLETIMES_3 = m_EngVer ? " " : "次";
            LB_RT_AUTOFLUIDINFUSION = m_EngVer ? "Auto SUP: " : "自动补液：";
            LB_RT_FIRSTFILTERVOLUME = m_EngVer ? "Filter VOL: " : "初滤体积：";
            LB_RT_SOLUTIONVOLUME = m_EngVer ? "SOL VOL: " : "溶媒体积：";
            MainView_CupTemp_GroupBox = m_EngVer ? "TEMP in Cup（℃）" : "杯内温度（℃）";
            LB_RT_DILUTIONENABLED = m_EngVer ? "Dilute(?): " : "是否稀释";
            LB_RT_DILUTIONRATIO = m_EngVer ? "Dilute Rto: " : "稀释倍数";

            LB_TEST_STEP = m_EngVer ? "Step" : "步骤";
            LB_IMG_L_INIT = m_EngVer ? "INIT" : "初始化";
            LB_IMG_L_UPTEMP = m_EngVer ? "Heat" : "加热器工作";
            LB_IMG_L_WAITPUTPILL = m_EngVer ? "Fill Tablets" : "投药器工作";
            LB_IMG_L_IMPELLERTURN = m_EngVer ? "Proprotor" : "搅拌桨工作";
            LB_IMG_L_DISSOLUTIONWORK = m_EngVer ? "Dissolution" : "溶出仪工作";
            LB_IMG_L_SAMPLINGWORK = m_EngVer ? "Sampler" : "取样器工作";
            LB_IMG_L_COLLECTORWORK = m_EngVer ? "Collector" : "收集器工作";
            LB_IMG_L_SAMPLINGOVER = m_EngVer ? "Sample Over" : "取样完成";
            LB_IMG_L_EXPERIMENTOVER = m_EngVer ? "Finish" : "实验完成";
            LB_TEST_STATUS = m_EngVer ? "Load" : "状态";

            TIP_LOAD_METHOD_FAILURE = m_EngVer ? "Method call failed!" : "方法调用失败！";
            TIP_PARAMS_SEND_FAILURE = m_EngVer ? "Parameter sending failed!" : "参数发送失败！";
            TIP_NOPARAMS_NO_OPERATE = m_EngVer ? "No parameters sent, unable to experiment!" : "没有发送参数，无法进行实验！";
            TIP_SAMPLING_NO_PAUSE = m_EngVer ? "Sampling thread is running, unable to pause!" : "取样线程运行中，无法暂停！";
            TIP_TERMINATE_CONFIRM = m_EngVer ? "Are you sure you want to abort the experiment?" : "确实要中止实验吗？";
            TIP_TEMP_SETTING_OUTLINE = m_EngVer ? "Temperature setting is out of limit [{0} - {1}], please fill in again!" : "温度设置越限【{0}-{1}】，请重新填写！";
            TIP_TEMP_SETTING_FORMAT_FAILURE = m_EngVer ? "Incorrect temperature setting format, please check!" : "温度设置格式有误，请检查！";
            TIP_SOLVENT_SETTING_OUTLINE = m_EngVer ? "The volume of solvent exceeds the limit [{0} - {1}], please fill in again!" : "溶媒体积越限【{0}-{1}】，请重新填写！";
            TIP_SOLVENT_SETTING_FORMAT_FAILURE = m_EngVer ? "Incorrect format of solvent volume, please check!" : "溶媒体积格式有误，请检查！";
            TIP_SAMPLE_VOLUME_SETTING_OUTLINE = m_EngVer ? "The sampling volume range is [{0} - {1}] mL, please re-enter!" : "取样体积范围是【{0}-{1}】mL，请重新输入！";
            TIP_SAMPLE_VOLUME_SETTING_FORMAT_FAILURE = m_EngVer ? "The sampling volume format is incorrect, please check!" : "取样体积格式有误，请检查！";
            TIP_SAMPLE_TIMES_SETTING_OUTLINE = m_EngVer ? "Sampling times exceed the limit [{0} - {1}], please fill in again!" : "取样次数越限【{0}-{1}】，请重新填写！";
            TIP_SAMPLE_TIMES_SETTING_FORMAT_FAILURE = m_EngVer ? "Incorrect format of sampling times, please check!" : "取样次数格式有误，请检查！";
            TIP_FIRSTFILTER_SETTING_OUTLINE = m_EngVer ? "The primary filtration volume range is [{0} - {1}] mL, please re-enter!" : "初滤体积范围是【{0}-{1}】mL，请重新输入！";
            TIP_FIRSTFILTER_SETTING_FORMAT_FAILURE = m_EngVer ? "The format of primary filtration volume is incorrect, please check!" : "初滤体积格式有误，请检查！";
            TIP_SAMPLE_TIME_SETTING_ERROR = m_EngVer ? "There is an error or a time point of 0 in the sampling time range. Please check!" : "取样次数范围内，取样时间有错误或为0的时间点，请检查！";
            TIP_ALLTIME_SETTING_OUTLINE = m_EngVer ? "The total time exceeds the maximum limit, please re-enter!" : "总时长越最大限，请重新输入！";
            TIP_ALLTIME_SETTING_SCOPE_ERROR = m_EngVer ? "The total time must be more than five minutes longer than the last sampling point, please reset!" : "总时长必须比最后一个取样点时间长五分钟以上，请重新设置！";
            TIP_ALLTIME_SETTING_FORMAT_FAILURE = m_EngVer ? "The format of total time length is incorrect, please check!" : "总时长格式有误，请检查！";
            TIP_ROTATESPEED_SETTING_EXSIT_NULL = m_EngVer ? "There are unset items in the [Speed setting] parameter, please reset!" : "【转速设置】参数存在未设置项，请重新设置！";
            TIP_ROTATESPEED_FRONTROW1_SETTING_NOTZORE = m_EngVer ? "The front row speed 1 in the [Speed setting] parameter cannot be 0" : "【转速设置】参数中前排转速1不可为0";
            TIP_ALLTIME_SETTING_MISMATCHING_BACKROW_STARTTIME = m_EngVer ? "The total time does not meet the starting time of the rear row of propeller rotation, please reset!" : "总时长不满足转桨后排启动时间，请重新设置！";
            TIP_SAMPLEVOLUME_ADD_FIRSTFILTER_OUTLINE = m_EngVer ? "The sum of sampling volume and initial filtration volume cannot be greater than {0} mL!" : "取样体积与初滤体积之和不能大于{0}mL！";
            TIP_SAMPLEVOLUME_ADD_FIRSTFILTER_FORMAT_ERROR = m_EngVer ? "The format of sampling volume or primary filtration volume is incorrect, please check!" : "取样体积或初滤体积格式有误，请检查！";
            TIP_DISOLUTION_RATIO_NULL = m_EngVer ? "The dilution ratio is empty!" : "稀释倍数为空！";
            TIP_DISOLUTION_RATIO_OUTLINE = m_EngVer ? "The dilution ratio is too large, exceeding the maximum capacity, please re-enter!" : "稀释倍数过大，超出最大容量，请重新输入！";
            TIP_LAUNCH_DISOLUTION_SAMPLEVOLUME_SCOPE = m_EngVer ? "Enable the dilution function, the sampling volume range is [{0} - {1}] mL, please re-enter!" : "启用稀释功能，取样体积范围为【{0}-{1}】mL，请重新输入！";
            TIP_ALLTIME_SETTING_MISMATCHING_DISOLUTION = m_EngVer ? "The total time does not meet the requirements of dilution time, please re-enter!" : "总时长不满足稀释时间要求，请重新输入！";
            TIP_CREATE_METHOD_CONFIRM = m_EngVer ? "Are you sure to create this method?" : "确定创建该方法吗？";
            TIP_UPDATE_METHOD_CONFIRM = m_EngVer ? "The {0} method already exists. Are you sure you want to update it?" : "{0}方法已存在，确定要更新吗？";
            TIP_TEMPINCUP_UNQUALIFIED = m_EngVer ? "The temperature in the cup is not up to the standard or the temperature in the cup is not obtained!  \r\n Cause: r n1. Whether the heating is on. \r\n 2. Whether the sampling probe has fallen into place. \r\n r n [Yes] Continue the experiment [No] Exit the experiment" : "杯内温度未达标或未获取到杯内温度!\r\n原因：\r\n1、加热是否开启。\r\n2、取样针是否已下降到位。\r\n\r\n   【是】继续实验  【否】退出实验";
            TIP_TEMPINTANK_UNQUALIFIED = m_EngVer ? "The temperature in the tank is not up to the standard or the temperature in the tank is not obtained!  \r\n Cause: r n1. Whether the heating is on. \r\n 2. Whether the sampling probe has fallen into place. \r\n r n [Yes] Continue the experiment [No] Exit the experiment" : "水箱温度未达标或未获取到水箱温度!\r\n原因：\r\n1、加热是否开启。\r\n2、取样针是否已下降到位。\r\n\r\n   【是】继续实验  【否】退出实验";
            //TIP_START_N_EXP_PREPARE = m_EngVer ? "Finish" : "总时长格式有误，请检查！";
            //TIP_START_N_EXP_SAMPLE = m_EngVer ? "Finish" : "实验完成";
            //TIP_START_INIT = m_EngVer ? "Finish" : "实验完成";
            //TIP_START_INIT_FAILURE = m_EngVer ? "Finish" : "实验完成";
            //TIP_INIT_SUCCESS = m_EngVer ? "Finish" : "实验完成";
            //TIP_EXP_OVER = m_EngVer ? "Finish" : "实验完成";
            //TIP_PREPARE_BEFORE_SAMPLE = m_EngVer ? "Finish" : "实验完成";
            //TIP_PREPARE_BEFORE_SAMPLE_OVER = m_EngVer ? "Finish" : "实验完成";
            //TIP_THIS_SAMPLE_OVER = m_EngVer ? "Finish" : "实验完成";
            //TIP_START_DISOLUTION = m_EngVer ? "Finish" : "实验完成";
            //TIP_START_N_DISOLUTION = m_EngVer ? "Finish" : "实验完成";
            //TIP_THIS_DISOLUTION_OVER = m_EngVer ? "Finish" : "实验完成";
            TIP_OUTLET_LEVEL = m_EngVer ? "OUTLET LEVEL" : "出液位";
            TIP_CIRCLE_LEVEL = m_EngVer ? "CIRCLE LEVEL" : "循环位";
            TIP_FLUID_INFUSION_LEVEL = m_EngVer ? "FLUID INFUSION LEVEL" : "补液位";
            TIP_SAMPLE_LEVEL = m_EngVer ? "SAMPLE LEVEL" : "取样位";

            #endregion

            #region 数据检索

            DSV_GROUPBOX = m_EngVer ? "Search Condition" : "检索条件";
            LB_DSV_DP_METHODDATE_SATRT = m_EngVer ? "Start Time: " : "实验开始时间：";
            LB_DSV_DP_METHODDATE_END = m_EngVer ? "End Time: " : "实验结束时间：";
            DSV_CKB_DATETIMEENABLE = m_EngVer ? "Enable" : "启用";
            LB_DSV_CKB_STATUS = m_EngVer ? "Status: " : "状态：";
            DSV_CKB_PAUSEEXP = m_EngVer ? "Abort EXP" : "中途中止实验";
            DSV_CKB_WHOLEEXP = m_EngVer ? "COMPL EXP" : "完整实验";
            LB_DSV_METHODNAME = m_EngVer ? "Name: " : "名称：";
            LB_DSV_MANUFACTURER = m_EngVer ? "MFR.: " : "生产厂家：";
            LB_DSV_TB_ACCOUNT = m_EngVer ? "User: " : "账号：";
            LB_DSV_BATCHNO = m_EngVer ? "Batch No.: " : "批号：";
            LB_DSV_TESTDEPARTMENT = m_EngVer ? "Test Dept.: " : "检测单位：";
            DSV_BTN_SEARCH = m_EngVer ? "Search" : "检索";
            DSV_BTN_EMPTY = m_EngVer ? "Clear" : "重置";
            TIP_SELECT_FILE = m_EngVer ? "Please select an experiment record!" : "请选择实验记录！";
            TIP_FILE_NOTEXSIT = m_EngVer ? "File does not exist!" : "文件不存在！";

            #endregion

            #region 工作日志
            WLV_GROUPBOX = m_EngVer ? "Search Condition" : "检索条件";
            LB_WLV_DP_METHODDATE_SATRT = m_EngVer ? "Start Time: " : "起始时间：";
            LB_WLV_DP_METHODDATE_END = m_EngVer ? "End Time: " : "终止时间：";
            WLV_CKB_DATETIMEENABLE = m_EngVer ? "Enable" : "启用";
            LB_WLV_TB_ACCOUNT = m_EngVer ? "User: " : "账号：";
            LB_WLV_COMBO_LOGTYPE = m_EngVer ? "Log Type: " : "日志类别：";
            LB_WLV_COMBO_BEHAVIOR = m_EngVer ? "User Behavior: " : "用户行为：";
            WLV_BTN_SEARCH = m_EngVer ? "Search" : "检索";
            WLV_BTN_EMPTY = m_EngVer ? "Clear" : "清空";
            WLV_BTN_EXPORT = m_EngVer ? "Export" : "导出";
            LV_WORKLOG_COL_ID = m_EngVer ? "ID" : "序号";
            LV_WORKLOG_COL_USER = m_EngVer ? "User" : "账户";
            LV_WORKLOG_COL_LOGTYPE = m_EngVer ? "Log Type" : "日志类别";
            LV_WORKLOG_COL_BEHAVIOR = m_EngVer ? "Behavior" : "行为";
            LV_WORKLOG_COL_TIME = m_EngVer ? "Time" : "时间";
            LV_WORKLOG_COL_NOTE = m_EngVer ? "Note" : "说明";
            TIP_EXPORTFILE_TITLE = m_EngVer ? "Work Log Report" : "工作日志报表";
            TIP_NODATA_EXPORT = m_EngVer ? "No data can be exported!" : "无数据可以导出！";
            TIP_EXPORT_LOG_SUCCESS = m_EngVer ? "The log has been successfully exported to the desktop!" : "日志已成功导出至桌面！";
            TIP_EXPORT_LOG_FAILURE = m_EngVer ? "Log export failed!" : "日志导出失败！";
            #endregion

            #region 仪器验证
            IVV_GROUPBOX = m_EngVer ? "Instrument verification" : "仪器验证";
            LB_IVV_RB_VERIFY = m_EngVer ? "Status: " : "验证状态：";
            LB_IVV_DP_VERIFYDATE = m_EngVer ? "verification Date: " : "验证日期：";
            LB_IVV_TB_VALIDDAYS = m_EngVer ? "Valid Days" : "有效期：";
            IVV_UNIT_DAYS = m_EngVer ? "days" : "天";
            LB_IVV_TB_REMARK = m_EngVer ? "Remark: " : "备注：";
            IVV_RB_VERIFY = m_EngVer ? "Verified" : "已验证";
            IVV_BTN_CONFIRM = m_EngVer ? "Verify" : "验证";
            IVV_BTN_SEARCH = m_EngVer ? "Search" : "搜索";
            IVV_BTN_EMPTY = m_EngVer ? "Clear" : "清空";
            TIP_VERIFY_STATUS = m_EngVer ? "Verified" : "已验证";
            TIP_UNVERIFY_STATUS = m_EngVer ? "Unverified" : "未验证";
            TIP_IVV_NOSET_VERIFYTIME = m_EngVer ? "Validation time not set!" : "未设置验证时间！";
            TIP_IVV_NOSET_VERIFYSTATUS = m_EngVer ? "No verification status selected!" : "未选择验证状态！";
            TIP_IVV_VERIFY_SUCCESS = m_EngVer ? "Verification succeeded!" : "验证成功! ";
            #region 数据备份
            #endregion

            #endregion

            #region 设备信息
            DIV_TEMPCALI_GROUPBOX = m_EngVer ? "TEMP CAL" : "温度标定";
            DIV_GATHER_GROUPBOX = m_EngVer ? "Gather" : "收集器";
            LB_DIV_WATERBOXTEMP = m_EngVer ? "Tank TEMP" : "水箱温度";
            LB_DIV_TB_WATERBOXTEMP_CALI = m_EngVer ? "Measured VAL: " : "测量值：";
            DIV_BTN_GATH_WATERBOX_TEMP = m_EngVer ? "Read Tank TEMP" : "读取水箱温度";
            DIV_BTN_CALIBRATION_WATERBOX_RESET = m_EngVer ? "CAL Reset" : "重置校准值";
            DIV_BTN_CALIBRATION_WATERBOX = m_EngVer ? "CAL" : "校准";
            LB_DIV_CUPTEMP = m_EngVer ? "Cup TEMP" : "杯内温度";
            LB_DIV_GATHERVAL = m_EngVer ? "Gather VAL: " : "采集值：";
            LB_DIV_SURVEYVAL = m_EngVer ? "Measured VAL: " : "测量值：";

            DIV_BTN_GATHERTMP = m_EngVer ? "Read cup TEMP" : "读取杯内温度";
            DIV_BTN_CALIBRATION_CUP_RESET = m_EngVer ? "CAL Reset" : "重置校准值";
            DIV_BTN_CALIBRATION_CUP = m_EngVer ? "CAL" : "校准";
            DIV_BTN_VOLUMECALIBRATION_RESET = m_EngVer ? "CAL VAL Reset" : "校准值重置";
            DIV_BTN_FIRSTSTEP = m_EngVer ? "1、Suck" : "1、吸液";
            DIV_BTN_PUSHOUTLETWATER = m_EngVer ? "2、Push Outlet" : "2、打废液";
            DIV_BTN_SUCK = m_EngVer ? "3、Suck" : "3、倒吸";
            DIV_BTN_PUTVOLUME = m_EngVer ? "4、Put Volume" : "4、打操作体积";
            DIV_BTN_OVER = m_EngVer ? "5、Over" : "5、完成";
            TIP_DIV_CALI_RESET = m_EngVer ? "Tip: Reset Calibration\r\n before Calibrate" : "注：校准前请重置";
            TIP_DIV_SUCK_MAX = m_EngVer ? "Tip: Turn fluid infusion \r\n and suck maximum" : "注：至补液位，吸最大量程";
            TIP_DIV_0P1ML = m_EngVer ? "Tip: 0.1mL" : "注：0.1mL";
            OP_VOLUME_INPUT = m_EngVer ? "OP Volume Input: " : "操作体积输入：";
            REAL_VOLUME_INPUT = m_EngVer ? "REAL Volume Input: " : "实际体积输入：";
            TIP_DIV_GROUP1 = m_EngVer ? "Group 1: " : "第一组";
            TIP_DIV_GROUP2 = m_EngVer ? "Group 2: " : "第二组";
            TIP_DIV_STANDARD_COE = m_EngVer ? "CAL COE: " : "校准系数";
            DIV_BTN_VOLUMECALIBRATION = m_EngVer ? "Volume CAL" : "体积校准";
            NOTE_TITLE = m_EngVer ? "Note" : "说明：";
            NOTE_STEP_1 = m_EngVer ? "Step 1: Click [CAL Reset]." : "第一步：点击【校准值重置】。";
            NOTE_STEP_2 = m_EngVer ? "Step 2: Run twice follow this process." : "第二步：按流程运行两次。";
            NOTE_STEP_3 = m_EngVer ? "Step 3: Input Operation Volume and Reality Volume,then Click [Volume CAL]." : "第三步：输入操作体积和实际体积，点【体积校准】";
            NOTE_STEP_4 = m_EngVer ? "Step 4: Verify follow this process." : "第四步：按流程进行验证。";

            TIP_DIV_TANK_TEMPCALI_RESET_SUCCESS = m_EngVer ? "Water tank temperature calibration value reset completed!" : "水箱温度校准值重置完成！";
            TIP_DIV_CALI_ISNULL = m_EngVer ? "Calibration value is empty!" : "校准值为空！";
            TIP_DIV_TANK_CALI_OUT_OF_LIMIT = m_EngVer ? "The calibration value of water tank temperature is out of range. Please enter a value between - 2.5 and 2.5!" : "水箱温度校准值超出范围，请输入-2.5~2.5之间的数值！";
            TIP_DIV_TANK_CALI_SUCCESS = m_EngVer ? "Water tank temperature calibration completed!" : "水箱温度校准完成！";
            TIP_DIV_INPUT_FORMAT_ERROR = m_EngVer ? "The input format is incorrect!" : "输入格式有误！";
            TIP_DIV_CUP_TEMPCALI_RESET_SUCCESS = m_EngVer ? "Cup temperature calibration reset completed!" : "水杯温度校准重置完成！";
            TIP_DIV_CUP_TEMP_EXSIT_NULL = m_EngVer ? "The measured temperature in the cup has a null value!" : "实测杯内温度存在空值！";
            TIP_DIV_CUP_CALI_SUCCESS = m_EngVer ? "Cup temperature calibration completed!" : "水杯温度校准完成！";
            TIP_VOLUME_CALI_RESET = m_EngVer ? "Volume calibration value reset completed! Please perform volume calibration again!" : "体积校准值重置完成！请重新进行体积校准！";
            TIP_WASTE_LIQUID_VOLUME_OUT_OF_LIMIT = m_EngVer ? "Waste liquid volume out of range!" : "废液体积超出范围！";
            TIP_OPERATING_VOLUME_OUT_OF_LIMIT = m_EngVer ? "Operating volume out of range!" : "操作体积超出范围！";
            TIP_CALI_ISNOT_NULL = m_EngVer ? "Calibration data cannot be empty!" : "校准数据不能为空！";
            TIP_CALI_VOLUME_SUCCESS = m_EngVer ? "Volume correction completed!" : "体积校正完成！";
            #endregion

            #region 技术支持
            TSV_DISSOLUTION_GROUPBOX = m_EngVer ? "Disolution" : "溶出仪";
            LB_TSV_COMBO_DISSOLUTIONMETHODNAME = m_EngVer ? "Method: " : "溶出方法：";
            TSV_BTN_CALIBRATION_RESET = m_EngVer ? "Reset CAL" : "校准值重置";
            LB_TB_OPERATE_HEIGHT = m_EngVer ? "OP H: " : "操作高度：";
            LB_TB_REALITY_HEIGHT = m_EngVer ? "ACT H: " : "实际高度：";
            BTN_CONFIRM_HEIGHT = m_EngVer ? "OK" : "确定";
            LB_GROUP_1 = m_EngVer ? "Group 1: " : "第一组：";
            LB_GROUP_2 = m_EngVer ? "Group 2: " : "第二组：";
            BTN_SAMPLEPOINTDOWN = m_EngVer ? "SP Down" : "取样针下降";
            BTN_SAMPLEPOINTUP = m_EngVer ? "SP Up" : "取样针上升";
            BTN_SAVE_KB = m_EngVer ? "Save CAL" : "保存校准";
            LB_TB_CONTAINER_MARK = m_EngVer ? "Cup Pos: " : "杯内位置：";
            TSV_BTN_SAMPLEVERIFY = m_EngVer ? "Verify Down" : "验证下降";

            LB_DISSOLUTION_DU = m_EngVer ? "Dissolution DU Test" : "溶出仪耐久测试";
            LB_TB_SWIVEL_TEST = m_EngVer ? "Swivel Test: " : "轴承测试：";
            BTN_TEST_SWIVEL_START = m_EngVer ? "Start" : "开始";
            BTN_TEST_SWIVEL_END = m_EngVer ? "End" : "停止";
            LB_TB_UPDOWN_TEST = m_EngVer ? "UpDown \r\n Test: " : "升降测试：";
            LB_UNIT_CI_1 = m_EngVer ? "freq" : "次";
            BTN_TEST_UPDOWN_START = m_EngVer ? "Start" : "开始";
            BTN_TEST_UPDOWN_END = m_EngVer ? "End" : "停止";
            LB_TB_SAMPLE_POINT = m_EngVer ? "SP Test: " : "取样针测试：";
            LB_UNIT_CI_2 = m_EngVer ? "freq" : "次";
            BTN_TEST_SAMPLE_POINT_START = m_EngVer ? "Start" : "开始";
            BTN_TEST_SAMPLE_POINT_END = m_EngVer ? "End" : "停止";

            TSV_SAMPLE_GROUPBOX = m_EngVer ? "Sampler" : "取样器";
            LB_SAMPLE_DU = m_EngVer ? "Sampler DU Test" : "取样器耐久测试";
            LB_TB_PUSHSOLID_TEST = m_EngVer ? "Sample Test: " : "取样测试：";
            BTN_TEST_PUSHSOLID_START = m_EngVer ? "Start Push" : "开始吸液";
            BTN_TEST_PUSHSOLID_END = m_EngVer ? "End" : "停止";
            BTN_TEST_PULLSOLID_START = m_EngVer ? "Start Pull" : "开始出液";
            LB_TB_VALVE_TEST = m_EngVer ? "Valve Test: " : "阀体测试：";
            LB_UNIT_HAO_1 = m_EngVer ? "" : "号";
            BTN_TEST_VALVE_START = m_EngVer ? "Start" : "开始";
            BTN_TEST_VALVE_END = m_EngVer ? "End" : "停止";

            TSV_GATHER_GROUPBOX = m_EngVer ? "Gather" : "收集器";
            LB_GATHER_DU = m_EngVer ? "Gather DU Test" : "收集器耐久测试";
            LB_TB_OUTPOINT_TEST = m_EngVer ? "OutPoint \r\n Test: " : "出样针测试：";
            LB_UNIT_CI_3 = m_EngVer ? "freq" : "次";
            BTN_TEST_OUTPOINT_START = m_EngVer ? "Start" : "开始";
            BTN_TEST_OUTPOINT_END = m_EngVer ? "End" : "停止";
            LB_TB_SUPPORTARMPOS_TEST = m_EngVer ? "Supportarm \r\n Postion Test: " : "支臂位测试:";
            LB_UNIT_HAO_2 = m_EngVer ? "" : "号";
            BTN_TEST_SUPPORTARMPOS_START = m_EngVer ? "Start" : "开始";
            BTN_TEST_SUPPORTARMPOS_END = m_EngVer ? "End" : "停止";
            LB_TB_SUPPORTARM_TEST = m_EngVer ? "Supportarm \r\n Test: " : "支臂测试：";
            LB_UNIT_CI_4 = m_EngVer ? "freq" : "次";
            BTN_TEST_SUPPORTARM_START = m_EngVer ? "Start" : "开始";
            BTN_TEST_SUPPORTARM_END = m_EngVer ? "End" : "停止";

            TIP_TSV_PARAMS_INPUT_ERROR = m_EngVer ? "Parameter input error!" : "参数输入错误! ";
            TIP_TSV_ROTATE_SPEED_LIMIT = m_EngVer ? "The rotational speed range is 1 - {0}" : "转速范围为【1-{0}】！";
            TIP_TSV_TESTTIMES_LIMIT = m_EngVer ? "" : "测试次数的范围为【1-10】！";
            TIP_TSV_INPUT_REALHEIGHT_OPHEIGHT = m_EngVer ? "Please enter the operating height and actual height!" : "请输入操作高度和实际高度！";
            TIP_TSV_SAVE_SUCCESS = m_EngVer ? "Saved successfully!" : "保存成功！";
            TIP_TSV_VALVE_LIMIT = m_EngVer ? "The range of limit point times is [1 - 4], please re-enter!" : "限位点次数的范围为【1-4】，请重新输入！";
            TIP_TSV_TEST_TIMES_LIMIT = m_EngVer ? "The range of test times is [1-10], please re-enter!" : "测试次数的范围为【1-10】，请重新输入！";
            TIP_TSV_SAMPLING_TEST_TIMES_LIMIT = m_EngVer ? "The range of test times is [1-20], please re-enter!" : "测试次数的范围为【1-20】，请重新输入！";
            TIP_TSV_UNSELECTED_METHOD = m_EngVer ? "No dissolution method selected!" : "未选择溶出方法！";
            TIP_TSV_SOLVENT_VOLUME_OUT_LIMIT = m_EngVer ? "The solvent volume exceeds the limit [{0} - {1}], please fill in again!" : "溶媒体积越限【{0}-{1}】，请重新填写！";
            TIP_TSV_METHOD_NULL = m_EngVer ? "Dissolution method is empty!" : "溶出方法为空！";
            TIP_TSV_CALI_RESET_SUCCESS = m_EngVer ? "Calibration value reset completed! Please calibrate the sampling probe again!" : "校准值重置完成！请重新进行取样针校准！";
            #endregion

            #region 数据备份
            DBV_GROUPBOX = m_EngVer ? "Search Condition" : "数据备份";
            DBV_BACKUP_GROUPBOX = m_EngVer ? "Backup" : "备份";
            DBV_RESTORE_GROUPBOX = m_EngVer ? "Restore" : "还原";
            LB_DBV_TB_BACKUPDIR = m_EngVer ? "Tip: Click [Explorer...] button, select backup Directory, then click [DB Backup] button." : "提示：点击“浏览。。。”，选择备份目录后，点击“数据库备份”";
            LB_DBV_TB_RESTOREFILE = m_EngVer ? "Tip: Click [Explorer...] button, select backup file, then click [DB Restore] button." : "提示：点击“浏览。。。”，选择备份文件后，点击“数据库还原”";
            DBV_BTN_BACKUP_EXPLORER = m_EngVer ? "Explorer..." : "浏览...";
            DBV_BTN_BACKUP = m_EngVer ? "DB Backup" : "数据库备份";
            DBV_BTN_RESTORE_EXPLORER = m_EngVer ? "Explorer..." : "浏览...";
            DBV_BTN_RESTORE = m_EngVer ? "DB Restore" : "数据库还原";
            TIP_DBV_RESTORE_WARNING = m_EngVer ? "The restore operation will completely overwrite the existing data. Are you sure you want to restore?" : "还原操作将完全覆盖现有数据，确定要还原吗？";
            TIP_DBV_BACKUP_SUCCESS = m_EngVer ? "Backup Success!" : "备份成功！";
            TIP_DBV_BACKUP_FAILURE = m_EngVer ? "Backup Failure!" : "备份失败！";
            TIP_DBV_RESTORE_SUCCESS = m_EngVer ? "Restore Success!" : "还原成功，系统将关闭，退出后请自行重启！";
            TIP_DBV_RESTORE_FAILURE = m_EngVer ? "Restore Failure!" : "还原成失败！";
           
            #endregion

            #region 系统管理
            //主窗口
            SMV_ACCOUNT_MANAGE = m_EngVer ? "User Manage" : " 账号管理 ";
            SMV_PRIVILEGE_MANAGE = m_EngVer ? "Role&Privilege" : " 权限管理 ";
            SMV_SYSTEMSETTING_MANAGE = m_EngVer ? "Setting" : " 系统设置 ";
            SMV_RETRIVER_MANAGE = m_EngVer ? "Restore factory" : " 恢复出厂 ";
            //账户管理
            UMV_NEWUSER_GROUPBOX = m_EngVer ? "New Account" : "新建账号";
            LB_SMV_TB_LOGINNAME = m_EngVer ? "User ID: " : "账号：";
            LB_SMV_PB_LOGINPWD = m_EngVer ? "Password: " : "口令：";
            LB_SMV_PB_LOGINPWD_TWICE = m_EngVer ? "Password again: " : "重复输入口令：";
            LB_SMV_COMBO_ROLE = m_EngVer ? "Role: " : "角色：";
            LB_SMV_COMBO_STATUS = m_EngVer ? "Status: " : "状态：";
            LB_SMV_DP_VALIDDATE = m_EngVer ? "Valid Date: " : "使用期限：";
            LB_SMV_TB_USERNAME = m_EngVer ? "Name: " : "姓名：";
            LB_SMV_TB_MOBILE = m_EngVer ? "Phone: " : "电话：";
            LB_SMV_TB_EMAIL = m_EngVer ? "Email: " : "邮箱：";
            SMV_BTN_ADD = m_EngVer ? "Add" : "添加";
            SMV_BTN_EMPTY = m_EngVer ? "Clear" : "清空";
            LB_SMV_REQUIRED_FIELD = m_EngVer ? "* required field" : "* 为必填项";
            UMV_USERLIST_GROUPBOX = m_EngVer ? "Account List" : "账号列表";
            TIP_EDIT_EMPTY_CONFIRM = m_EngVer ? "Are you sure to clear the information being edited?" : "确定清空正在编辑的信息吗？";
            TIP_ACCOUNT_FORMAT_ERROR = m_EngVer ? "Account has abnormal characters, please re-enter!" : "账户存在异常字符，请重新输入!";
            TIP_ACCOUNT_EXSIT = m_EngVer ? "The login name exists. Please change it and try again! " : "该登录名存在，请更改后重试！";
            TIP_ACCOUNT_ADD_CONFIRM = m_EngVer ? "Are you sure to add this user?" : "确定添加该用户吗？";
            TIP_ACCOUNT_SAVE_SUCCESS = m_EngVer ? "Saved successfully!" : "保存成功！";
            TIP_ACCOUNT_SAVE_FAILURE = m_EngVer ? "Saving failed, please check!" : "保存失败，请检查！";
            TIP_ACCOUNT_DEL_CONFIRM = m_EngVer ? "Are you sure you want to delete the currently selected user?" : "确定要删除当前选中用户吗？";
            TIP_ACCOUNT_DEL_FAILURE = m_EngVer ? "Delete failed!" : "删除失败!";
            //权限管理
            RMV_ROLELIST_GROUPBOX = m_EngVer ? "Role List" : "角色列表";
            SMV_BTN_SAVEPRIVELEGE = m_EngVer ? "Save" : "保存";
            SMV_BTN_RETRIVERPRIVELEGE = m_EngVer ? "Retrive" : "撤销";
            SMV_BTN_ROLE_ADD = m_EngVer ? "Add" : "添加";
            SMV_BTN_ROLE_DELETE = m_EngVer ? "Delete" : "删除";
            RMV_MODULELIST_GROUPBOX = m_EngVer ? "Module List" : "模块列表";
            RMV_FUNCTION_GROUPBOX = m_EngVer ? "Function List" : "功能列表";
            SMV_CHK_MODULE_ALL = m_EngVer ? "Select All" : "全选";
            SMV_CHK_FUNCTION_ALL = m_EngVer ? "Select All" : "全选";
            TIP_SELECT_ROLE = m_EngVer ? "Please select a role!" : "请选择角色!";
            TIP_ROLE_REVIEW_CONFIRM = m_EngVer ? "Are you sure to modify the functional permissions of this role ?" : "确定修改该角色功能权限吗？";
            TIP_ROLE_NO_REVIEW = m_EngVer ? "{0} permission cannot be modified!" : "{0}权限无法修改！";
            TIP_ROLE_INPUT = m_EngVer ? "Please select a role!" : "请输入角色名称！";
            TIP_ROLE_EXSITROLE = m_EngVer ? "The role already exists, please change it and try again!" : "角色名已经存在，请更换后重试！";
            TIP_ROLE_ADD_CONFIRM = m_EngVer ? "Are you sure to add this role?" : "确定添加该角色吗？";
            TIP_ROLE_SAVE_RESTART = m_EngVer ? "Role saved successfully! It will take effect after restarting the software!" : "角色保存成功！重启软件后生效！";
            TIP_ROLE_SAVE_FAILURE = m_EngVer ? "Saving failed, please check!" : "保存失败，请检查！";
            TIP_ROLE_EXSITUSER = m_EngVer ? "There are users under this role, unable to delete!" : "该角色下存在用户，无法删除！";
            TIP_ROLE_DEL_CONFIRM = m_EngVer ? "Are you sure to delete this role?" : "确定删除该角色吗？";
            TIP_ROLE_DEL_SUCCESS = m_EngVer ? "Role deleted successfully!" : "角色删除成功！";
            TIP_ROLE_DEL_FAILURE = m_EngVer ? "Role deletion failed. Please check whether the role is in use!" : "角色删除失败，请检查角色是否在使用中！";
            TIP_ROLE_SAVE_SUCCESS = m_EngVer ? "Saved successfully!" : "保存成功！";
            //系统管理
            SMV_SYSTEMSETTING_GROUPBOX = m_EngVer ? "System Setting" : "系统设置";
            SMV_MAINBOARD_GROUPBOX = m_EngVer ? "Mainboard" : "主机";
            LB_SMV_COMBO_COMPORT = m_EngVer ? "COMM Port: " : "通信端口：";
            LB_SMV_COMBO_COMBAUD = m_EngVer ? "Baud: " : "波特率：";
            SMV_PRINTER_GROUPBOX = m_EngVer ? "Printer" : "打印机";
            LB_SMV_COMBO_PRINTER = m_EngVer ? "Printer Name: " : "名称：";
            LB_SMV_COMBO_PRINTERPORT = m_EngVer ? "Port: " : "端口：";
            LB_SMV_COMBO_PRINTCOMBAUD = m_EngVer ? "Baud: " : "波特率：";
            SMV_CKB_WHOLEMODE_ENABLE = m_EngVer ? "Sampler enabled" : "启用取样收集装置";
            SMV_CKB_PUTPILL_ENABLE = m_EngVer ? "Fill Tablets enabled" : "启用投药装置";
            SMV_CKB_SAMPLEPOINT_ENABLE = m_EngVer ? "SP enabled" : "启用取样针装置";
            SMV_CKB_CUPTEMP_ENABLE = m_EngVer ? "Cup TEMP enabled" : "启用杯内测温装置";
            SMV_CKB_BEEP_ENABLE = m_EngVer ? "Beep enabled" : "启用取样前蜂鸣";
            SMV_CKB_DOUBLEMOTOR = m_EngVer ? "Double Motor enabled" : "双电机";
            SMV_CKB_AUTOHEAT_ENABLE = m_EngVer ? "Auto heat" : "自动加热";
            SMV_CKB_TEMP_ENABLE = m_EngVer ? "Gather TEMP enabled" : "启用温度采集";
            LB_SMV_TB_TEMPOFFSET = m_EngVer ? "TEMP Offset: " : "温度偏差值：";
            LB_SMV_TB_DEFHEATTEMP = m_EngVer ? "Default heat TEMP: " : "默认加热温度：";
            LB_SMV_TB_PWDVALIDDAYS = m_EngVer ? "Password valid date: " : "口令有效期：";
            LB_SMV_TB_ROTATERATE = m_EngVer ? "Rotate Ratio: " : "转速倍率：";
            LB_SMV_TB_FIRSTFILTER_OFFSET = m_EngVer ? "First Filter Offset: " : "初滤液补偿：";
            SMV_BTN_SAVESETTING = m_EngVer ? "Save Setting" : "保存设置";
            TIP_PROFILE_EMPTY = m_EngVer ? "Save Setting" : "配置文件存在空值！";
            TIP_PROFILE_ERROR = m_EngVer ? "Exception occurred while saving configuration file!" : "保存配置文件出现异常！ ";
            TIP_SAVE_RESTART = m_EngVer ? "Successfully saved, restart takes effect!" : "保存成功，重启生效！";
            LB_SMV_TB_SPEEDOFFSET = m_EngVer ? "Speed Offset: " : "转速偏差值：";

            //恢复出厂设置
            RFV_LB_WARNING = m_EngVer ? "Warning: This operation will permanently clear the business data in the system and restore the factory state. Therefore, it is recommended to back up the data first! Please operate with caution!"
                : "警告：该操作将永久性清除系统中业务数据，恢复出厂时的状态。因此，建议先备份数据！请慎重操作！ ";
            SMV_BTN_RETRIVERFACTORY = m_EngVer ? "One-click to restore \r\n factory settings " : "一键恢复出厂设置";
            TIP_RFV_RETRIVERFACTORY_CONFIRM = m_EngVer ? "Are you sure you want to restore factory settings?" : "确定要恢复出厂设置吗？";
            #endregion

            #region 关于
            AV_SYSINFO_GROUPBOX = m_EngVer ? "System INFO" : "系统信息";
            AV_AUTHORIZEDINFO_GROUPBOX = m_EngVer ? "Authorized INFO" : "许可信息";
            AV_HELP_GROUPBOX = m_EngVer ? "Help" : "帮助";
            LB_AV_APPNAME = m_EngVer ? "App Name: " : "软件名称：";
            LB_AV_APPVERSION = m_EngVer ? "Version: " : "版本号：";
            LB_AV_APPOWNER = m_EngVer ? "App Owner: " : "版权所有：";
            LB_AV_APPINSTALLDATE = m_EngVer ? "Install Date: " : "安装日期：";
            LB_AV_APPDBSIZE = m_EngVer ? "DB Size: " : "数据库大小：";
            LB_AV_APPLOGSIZE = m_EngVer ? "Log Size: " : "日志大小：";
            LB_AV_APPREPORTCOUNT = m_EngVer ? "Report Number: " : "实验报告数量：";
            LB_AV_AuthorizedTo = m_EngVer ? "Authorized To: " : "被许可人：";
            LB_AV_TIMELIMIT = m_EngVer ? "Time Limit: " : "使用期限：";
            LB_AV_TIMELIMIT_UNIT = m_EngVer ? "by" : "截止至";
            LB_AV_BTN_INPUTSERIALNO = m_EngVer ? "SN: " : "序列号：";
            AV_BTN_INPUTSERIALNO = m_EngVer ? "Input SN" : "输入序列号";

            #endregion

            #region 加热对话框
            TSM_MW = m_EngVer ? "Heating" : "加热";
            LB_TSM_TB_TEMPSETTING = m_EngVer ? "heating temperature: " : "当前加热温度：";
            TSM_TB_STARTHEAT = m_EngVer ? "Start Heat" : "开始加热";
            TSM_TB_ENDHEAT = m_EngVer ? "End Heat" : "停止加热";
            TIP_TSM_TEMP_OUT_LIMIT = m_EngVer ? "The temperature exceeds the allowable range!" : "温度超过允许范围！";
            #endregion
            #region 清洗对话框
            WTM_MW = m_EngVer ? "Washing" : "清洗";
            LB_WTM_TB_WASHINGTIMES = m_EngVer ? "Washing times" : "清洗次数：";
            LB_WASHINGTIME_UNIT = m_EngVer ? "freq" : "次";
            WTM_BTN_TIMES_1 = m_EngVer ? "1" : "1次";
            WTM_BTN_TIMES_2 = m_EngVer ? "2" : "2次";
            WTM_BTN_TIMES_3 = m_EngVer ? "3" : "3次";
            WTM_BTN_TIMES_5 = m_EngVer ? "5" : "5次";
            WTM_WASHING_STATUS = m_EngVer ? "Input Washing times, Click [OK]" : "输入清洗次数，点确定";

            TIP_WASHING_CONFIRM = m_EngVer ? "Are you sure you want to clean it?" : "确定要清洗吗？";
            TIP_WASHING_DO_NOT_EXIT = m_EngVer ? "Cleaning in progress, please do not exit!" : "正在清洗，请不要退出！";
            TIP_INPUT_ERROR = m_EngVer ? "Input error!" : "输入有误！";
            TIP_WASHING_OVER = m_EngVer ? "Cleaning Over! " : "清洗完成！";
            TIP_WASHING_NOT_EXIT = m_EngVer ? "Cleaning in progress, unable to exit!" : "正在清洗，无法退出！";
            WTM_NORMALTIMES_GROUPBOX = m_EngVer ? "Common times" : "常用次数";
            #endregion
            #region 温度监测对话框
            TMM_MW = m_EngVer ? "temperature monitoring" : "温度监测";
            LowLimit = m_EngVer ? "Below the set temperature \r\n lower limit" : "低于设定温度下限值";
            NormalTEMP = m_EngVer ? "Normal temperature" : "正常温度";
            UpperLimit = m_EngVer ? "Above the set temperature \r\n upper limit value" : "高于设定温度上限值";
            #endregion 
            #region 报告对话框
            RM_MW = m_EngVer ? "Report" : "实验报告";
            LB_RM_DatePicker = m_EngVer ? "EXP Date: " : "实验日期：";
            RM_CHK_ENABLEDATE = m_EngVer ? "Enabled" : "启用";
            LB_RM_COMBO_REPORTTYPE = m_EngVer ? "Status: " : "状态：";
            RM_BTN_QUERY = m_EngVer ? "Search" : "检索";
            RM_BTN_PREVIEW = m_EngVer ? "Open" : "打开";
            RM_BTN_EXPORT = m_EngVer ? "Save as ..." : "另存为...";
            LB_ZoomInButton = m_EngVer ? "Zoom in" : "放大";
            LB_ZoomOutButton = m_EngVer ? "Zoom out" : "缩小";
            LB_NormalButton = m_EngVer ? "100%" : "100%";
            LB_FitToHeightButton = m_EngVer ? "Entire page" : "整页";
            LB_SinglePageButton = m_EngVer ? "Single page" : "单页";
            LB_FacingButton = m_EngVer ? "double page" : "双页";
            LB_CloseWindow = m_EngVer ? "Close Window" : "关闭窗口";
            TIP_NO_REPORT = m_EngVer ? "There is no report on this date!" : "该日期无报表！";
            TIP_SELECT_REPORT = m_EngVer ? "Please select an experimental report file!" : "请选择实验报告文件!";
            TIP_EXPORT_ERROR = m_EngVer ? "Export file failed!" : "导出文件失败！";
            TIP_EXPORT_SUCCESS = m_EngVer ? "Export completed!" : "导出完成！";
            TIP_SELECT_EXPORT_REPORT = m_EngVer ? "Please select the experimental report file to be exported!" : "请选中待导出实验报告文件!";
            TIP_TITLE_NAME_ISNULL = m_EngVer ? "Title or file name is empty!" : "标题或文件名为空！";
            #endregion
            #region 倒计时对话框
            PTM_MW = m_EngVer ? "Timer" : "定时";
            LB_PTM_TB_PREPARETIME = m_EngVer ? "Countdown time:" : "倒计时时间：";
            LB_PTM_TB_PREPARETIME_UNIT = m_EngVer ? "min" : "分钟";
            BTN_CONFRIM = m_EngVer ? "Ok" : "确定";
            BTN_CANCLE_PREPARETIME = m_EngVer ? "Cancel the countdown" : "取消倒计时";
            BTN_CANCLE = m_EngVer ? "Close" : "关闭";
            TIP_SET_TIMER = m_EngVer ? "Please set the countdown time!" : "请设置倒计时时间！";
            TIP_SET_TIMER_ERROR = m_EngVer ? "There is an error in the input time!" : "输入的时间有错误！";
            #endregion
            #region 预约时间对话框
            ATM_WM = m_EngVer ? "Time of appointment" : "预约时间";
            LB_TB_APPOINTMENTTIME = m_EngVer ? "Time of appointment:" : "预约时间";
            LB_TB_APPOINTMENTTIME_UNIT = m_EngVer ? "min" : "分钟";
            #endregion
            #region 倒计时对话框
            CM_WM = m_EngVer ? "count down" : "倒计时";
            LB_LB_COUNTDOWNBULLETIN = m_EngVer ? "Countdown in progress" : "倒计时中";
            TIP_COUNTDOWN_EXIT_CONFIRM = m_EngVer ? "Are you sure to exit the countdown?" : "确定退出倒计时？";
            #endregion
            #region 注册对话框
            IM_MW = m_EngVer ? "Register" : "注册";
            LB_INM_TB_SERIALNO = m_EngVer ? "Please enter the serial number:" : "请输入序列号：";
            INM_REGISTER = m_EngVer ? "Register" : "注册";
            INM_CLOSE = m_EngVer ? "Close" : "关闭";
            TIP_INM_SN_INVALID = m_EngVer ? "Invalid registration code!" : "注册码无效！";
            #endregion
            #region 重连对话框
            RMM_MW = m_EngVer ? "Reconnect" : "重连";
            RCM_BTN_CONNECT = m_EngVer ? "Reconnect" : "重新连接";
            RCM_BTN_EXIT = m_EngVer ? "Close" : "关闭";
            #endregion
            #region 修改口令对话框
            RPM_MW = m_EngVer ? "Change Password" : "修改口令";
            LB_RPM_PB_LOGINPWD = m_EngVer ? "Password: " : "口令：";
            LB_RPM_PB_LOGINPWD_TWICE = m_EngVer ? "Confirm Password: " : "确认口令：";
            RPM_BTN_CONFIRM = m_EngVer ? "Ok" : "确认口令：";
            RPM_BTN_CANCEL = m_EngVer ? "Close" : "关闭";
            TIP_RPM_TWICE_PASSWORD_CONSISTENT = m_EngVer ? "Close" : "新口令与原口令相同，请重新设置口令！";
            TIP_RPM_PASSWORD_REVIEW_SUCCESS = m_EngVer ? "Close" : "口令修改成功！";
            #endregion
            #region 转速设置对话框
            SSM_MW = m_EngVer ? "Speed setting" : "转速设置";
            LB_Combo_Speed_Mode = m_EngVer ? "Speed mode: " : "转速模式：";
            LB_Combo_Electricmotor_Mode = m_EngVer ? "Motor mode: " : "电机模式：";
            LB_TB_Front_Speed_1 = m_EngVer ? "Front row 1:" : "前排转速1：";
            LB_TB_Front_StartTime_1 = m_EngVer ? "Front row ST 1: " : "前排启动时间1：";
            LB_TB_Front_Speed_2 = m_EngVer ? "Front row 2:" : "前排转速2：";
            LB_TB_Front_StartTime_2 = m_EngVer ? "Front row ST 2: " : "前排启动时间2：";
            LB_TB_Back_Speed_1 = m_EngVer ? "Back row 1: " : "后排转速1：";
            LB_TB_Back_StartTime_1 = m_EngVer ? "Back row ST 1: " : "后排启动时间1：";
            LB_TB_Back_Speed_2 = m_EngVer ? "Back row 2: " : "后排转速2：";
            LB_TB_Back_StartTime_2 = m_EngVer ? "Back row ST 2: " : "后排启动时间2：";
            Btn_Confirm = m_EngVer ? "Ok" : "确定";
            Btn_Close = m_EngVer ? "Close" : "关闭";
            TIP_NO_SELECT_SPEED_MODE = m_EngVer ? "Rotation speed mode not selected!" : "未选择转速模式！";
            TIP_NO_SELECT_ELECTRICMOTOR_MODE = m_EngVer ? "Rotating motor mode not selected!" : "未选择转电机模式！";
            TIP_SPEED_STARTTIME_NULL = m_EngVer ? "Rotation speed and start time cannot be blank, please fill in [0] if not!" : "转速和启动时间不能为空,没有请填【0】！";
            TIP_FR_SPEED1_GREATER_THEN_ONE = m_EngVer ? "The front row motor speed 1 must be greater than 0 when closing!" : "关闭前排电机转速1必须大于0!";
            TIP_FR_SPEED1_AND_BK_SPEED1_GREATER_THEN_ONE = m_EngVer ? "Front row motor speed 1 and rear row motor speed 1 must be greater than 0!" : "前排电机转速1和后排电机转速1必须大于0！";
            TIP_FR_SPEED1_AND_FR_SPEED2_GREATER_THEN_ONE = m_EngVer ? "Front row motor speed 1 and front row motor speed 2 must be greater than 0!" : "前排电机转速1和前排电机转速2必须大于0！";
            TIP_FR_STARTTIME2_GREATER_THEN_FR_STARTTIME1 = m_EngVer ? "Front row start time 2 must be greater than front row start time 1!" : "前排启动时间2必须大于前排启动时间1！";
            TIP_ALL_SPEED_GREATER_THEN_ZERO = m_EngVer ? "Front row motor speed 1, front row motor speed 2, rear row motor speed 1, and rear row motor speed 2 must be greater than 0!" : "前排电机转速1、前排电机转速2，后排电机转速1、后排电机转速2必须大于0！";
            TIP_FR_BK_SPEED2_GRATER_THEN_SPEED1 = m_EngVer ? "The front start time 2 must be greater than the front start time 1, and the rear start time 2 must be greater than the rear start time 2!" : "前排启动时间2必须大于前排启动时间1,后排启动时间2必须大于后排启动时间2！";
            TIP_SPEED_LIMITIED = m_EngVer ? "The rotational speed cannot be greater than {0}!" : "转速不能大于{0}！";
            TIP_SPEED_INPUT_LIMITIED = m_EngVer ? "0-350, with a division of 1!" : "0-350,分度为1！";
            TIP_STARTTIME_LIMITIED = m_EngVer ? "0-100000, scale is 1, time cannot exceed the total duration!" : "0-100000,分度为1，时间不能超过总时长！";
            TIP_SSM_INPUT_FORMAT_ERROR = m_EngVer ? "The input format is incorrect. Please modify it and try again!" : "输入格式有误，请重新输入！";
            #endregion
            #region 取样时间对话框
            STM_MW = m_EngVer ? "Sampling time" : "取样时间";
            STM_SAMPLE_TIMES = m_EngVer ? "Sampling time (freq)" : "取样第次（次）";
            STM_SAMPLE_POS_TIME = m_EngVer ? "Sampling time point (min)" : "取样时间点（分钟）";
            Btn_SaveSampleTime = m_EngVer ? "Save" : "保存";
            Btn_SaveSampleTime_Return = m_EngVer ? "Close" : "返回";
            TIP_STM_SAMPLE_POS_EXSIT_ZERO = m_EngVer ? "The sampling time point interval exists at 0. Please modify it and try again!" : "取样时间点间隔存在0，请修改后重试！";
            TIP_STM_SAMPLE_INTERVAL_LESS_THEN = m_EngVer ? "The sampling time point interval cannot be less than {0} minutes. Please modify it and try again!" : "取样时间点间隔不能小于{0}分钟，请修改后重试！";
            TIP_STM_INPUT_FORMAT_ERROR = m_EngVer ? "The input format is incorrect. Please modify it and try again!" : "输入格式有误，请修改后重试！";

            #endregion
            #region 方法调用
            LMM_MW = m_EngVer ? "Method Exsits" : "已有方法";
            LMM_SEARCH_COND_GROUPBOX = m_EngVer ? "Search Cond" : "检索条件";
            LB_DP_METHODDATE = m_EngVer ? "Date: " : "时间：";
            CKB_DATETIMEENABLE = m_EngVer ? "Enabled" : "启用";
            LB_LMM_METHODNAME = m_EngVer ? "Method: " : "名称：";
            LB_LMM_MANUFACTURER = m_EngVer ? "MFR.: " : "生产厂家：";
            LB_LMM_BATCHNO = m_EngVer ? "Lot No: " : "批号：";
            LB_LMM_TESTDEPARTMENT = m_EngVer ? "Test Dept.: " : "检测单位：";
            LMM_BTN_SEARCH = m_EngVer ? "Query" : "检索";
            LMM_BTN_EMPTY = m_EngVer ? "Empty" : "清空";
            LMM_BTN_CALLMETHOD = m_EngVer ? "Load" : "调用";
            LMM_BTN_DELMETHOD = m_EngVer ? "Delete" : "删除";
            LMM_BTN_CANCEL = m_EngVer ? "Cancel" : "退出";
            TIP_DELETE_METHOD_CONFIRM = m_EngVer ? "Save" : "保存";
            TIP_DELETE_SUCCESS = m_EngVer ? "Save" : "删除";
            TIP_DELETE_FAILURE = m_EngVer ? "Save" : "保存";

            LMM_GV_MethodName = m_EngVer ? "Name" : "名称";
            LMM_GV_Manufacturer = m_EngVer ? "Manufacturer" : "生产厂商";
            LMM_GV_BatchNo = m_EngVer ? "Lot No" : "批号";
            LMM_GV_TestDepartment = m_EngVer ? "Tester" : "检测单位";
            LMM_GV_DissolutionMethodName = m_EngVer ? "Method" : "溶出方法";
            LMM_GV_TempSetting = m_EngVer ? "Temp" : "温度设置";
            LMM_GV_SolutionVolume = m_EngVer ? "Solution Volume" : "溶媒体积";
            LMM_GV_FrontRowSpeed_1 = m_EngVer ? "FrontRowSpeed_1" : "前排转速1";
            LMM_GV_FrontRowSpeed_2 = m_EngVer ? "FrontRowSpeed_2" : "前排转速2";
            LMM_GV_FrontRowStartTime_1 = m_EngVer ? "FrontRowStartTime_1" : "前排启动时间1";
            LMM_GV_FrontRowStartTime_2 = m_EngVer ? "FrontRowStartTime_2" : "前排启动时间2";
            LMM_GV_BackRowSpeed_1 = m_EngVer ? "BackRowSpeed_1" : "后排转速1";
            LMM_GV_BackRowSpeed_2 = m_EngVer ? "BackRowSpeed_2" : "后排转速2";
            LMM_GV_BackRowStartTime_1 = m_EngVer ? "BackRowStartTime_1" : "后排启动时间1";
            LMM_GV_BackRowStartTime_2 = m_EngVer ? "BackRowStartTime_2" : "后排启动时间2";
            LMM_GV_SampleTimes = m_EngVer ? "Sample Times" : "取样次数";
            LMM_GV_SampleVolume = m_EngVer ? "Sample Volume" : "取样体积";
            LMM_GV_Auto_Fluid_Infusion = m_EngVer ? "Auto fluid infusion" : "自动补液";
            LMM_GV_First_filter_volume = m_EngVer ? "First filter volume" : "初滤体积";
            LMM_GV_AllTimespan = m_EngVer ? "AllTimespan" : "总时长";
            LMM_GV_TimeValue1 = m_EngVer ? "Sample time 1" : "取样时间1";
            LMM_GV_TimeValue2 = m_EngVer ? "Sample time 2" : "取样时间2";
            LMM_GV_TimeValue3 = m_EngVer ? "Sample time 3" : "取样时间3";
            LMM_GV_TimeValue4 = m_EngVer ? "Sample time 4" : "取样时间4";
            LMM_GV_TimeValue5 = m_EngVer ? "Sample time 5" : "取样时间5";
            LMM_GV_TimeValue6 = m_EngVer ? "Sample time 6" : "取样时间6";
            LMM_GV_TimeValue7 = m_EngVer ? "Sample time 7" : "取样时间7";
            LMM_GV_TimeValue8 = m_EngVer ? "Sample time 8" : "取样时间8";
            LMM_GV_TimeValue9 = m_EngVer ? "Sample time 9" : "取样时间9";
            LMM_GV_TimeValue10 = m_EngVer ? "Sample time 10" : "取样时间10";
            LMM_GV_TimeValue11 = m_EngVer ? "Sample time 11" : "取样时间11";
            LMM_GV_TimeValue12 = m_EngVer ? "Sample time 12" : "取样时间12";
            LMM_GV_TimeValue13 = m_EngVer ? "Sample time 13" : "取样时间13";
            LMM_GV_TimeValue14 = m_EngVer ? "Sample time 14" : "取样时间14";
            LMM_GV_TimeValue15 = m_EngVer ? "Sample time 15" : "取样时间15";
            LMM_GV_TimeValue16 = m_EngVer ? "Sample time 16" : "取样时间16";
            LMM_GV_TimeValue17 = m_EngVer ? "Sample time 17" : "取样时间17";
            LMM_GV_TimeValue18 = m_EngVer ? "Sample time 18" : "取样时间18";
            LMM_GV_TimeValue19 = m_EngVer ? "Sample time 19" : "取样时间19";
            LMM_GV_TimeValue20 = m_EngVer ? "Sample time 20" : "取样时间20";
            LMM_GV_TimeValue21 = m_EngVer ? "Sample time 21" : "取样时间21";
            LMM_GV_TimeValue22 = m_EngVer ? "Sample time 22" : "取样时间22";
            LMM_GV_TimeValue23 = m_EngVer ? "Sample time 23" : "取样时间23";
            LMM_GV_TimeValue24 = m_EngVer ? "Sample time 24" : "取样时间24";
            LMM_GV_TimeValue25 = m_EngVer ? "Sample time 25" : "取样时间25";
            LMM_GV_TimeValue26 = m_EngVer ? "Sample time 26" : "取样时间26";
            LMM_GV_TimeValue27 = m_EngVer ? "Sample time 27" : "取样时间27";
            LMM_GV_TimeValue28 = m_EngVer ? "Sample time 28" : "取样时间28";
            LMM_GV_TimeValue29 = m_EngVer ? "Sample time 29" : "取样时间29";
            LMM_GV_TimeValue30 = m_EngVer ? "Sample time 30" : "取样时间30";
            LMM_GV_TimeValue31 = m_EngVer ? "Sample time 31" : "取样时间31";
            LMM_GV_TimeValue32 = m_EngVer ? "Sample time 32" : "取样时间32";
            LMM_GV_TimeValue33 = m_EngVer ? "Sample time 33" : "取样时间33";
            LMM_GV_TimeValue34 = m_EngVer ? "Sample time 34" : "取样时间34";
            LMM_GV_TimeValue35 = m_EngVer ? "Sample time 35" : "取样时间35";
            LMM_GV_TimeValue36 = m_EngVer ? "Sample time 36" : "取样时间36";
            LMM_GV_TimeValue37 = m_EngVer ? "Sample time 37" : "取样时间37";
            LMM_GV_TimeValue38 = m_EngVer ? "Sample time 38" : "取样时间38";
            LMM_GV_TimeValue39 = m_EngVer ? "Sample time 39" : "取样时间39";
            LMM_GV_TimeValue40 = m_EngVer ? "Sample time 40" : "取样时间40";
            LMM_GV_MethodTime = m_EngVer ? "Method Time" : "时间";
            LMM_GV_SpeedMode = m_EngVer ? "Speed Mode" : "转速模式";
            LMM_GV_ElectricmotorMmode = m_EngVer ? "Electric motor mode" : "电机模式";
            #endregion
            #region 签名对话框
            SLM_MW = m_EngVer ? "Sign in" : "签名登录";
            LB_SLM_TB_LOGINNAME = m_EngVer ? "Uid: " : "账号：";
            LB_SLM_PB_LOGINPWD = m_EngVer ? "Pwd: " : "口令：";
            LB_SLM_TB_SIGNCONTENT = m_EngVer ? "Audit content: " : "审核内容：";
            LB_SLM_ONEKEYINPUT = m_EngVer ? "One touch input: " : "一键输入：";
            SLM_BTN_PASS = m_EngVer ? "Pass" : "通过";
            SLM_BTN_FAIL = m_EngVer ? "Fail" : "不通过";
            SLM_BTN_CONFIRM = m_EngVer ? "Ok" : "确认";
            SLM_BTN_CANCEL = m_EngVer ? "Cancel" : "取消";

            TIP_SLM_INPUT_ACCOUNTORPASSWORD = m_EngVer ? "Method Please enter an account or password!" : "请输入账户或口令!";
            TIP_SLM_ACCOUNT_CHAR_UNNORMAL = m_EngVer ? "Account has abnormal characters, please re-enter!" : "账户存在异常字符，请重新输入!";
            TIP_SLM_ACCOUNT_NOTEXSIT = m_EngVer ? "The account does not exist or is locked, invalid. Please contact the system administrator!" : "账户不存在或已锁定,失效，请联系系统管理员!";
            TIP_SLM_ACCOUNT_EXPIRE = m_EngVer ? "The account has expired, please contact the system administrator!" : "账户已过期，请联系系统管理员!";
            TIP_SLM_PASSWORD_EXPIRE = m_EngVer ? "The password has expired, please reset it!" : "口令已过期，请重新设置口令!";
            TIP_SLM_UPDATE_SIGN_CONFIRM = m_EngVer ? "Are you sure you want to update the signature?" : "确定要更新签字吗？";
            TIP_SLM_SIGN_LOGIN_FAILURE = m_EngVer ? "Signing login failed!" : "签名登录失败！";
            TIP_SLM_SIGNED_REPORT_SUCCESS = m_EngVer ? "Signature report generation completed!" : "签名报告生成完毕！";
            TIP_SLM_ACCOUNT_ISNOTNULL = m_EngVer ? "Account or password cannot be empty!" : "账号或口令不能为空!";
            TIP_SLM_REVISION_ISNOTNULL = m_EngVer ? "Approval content cannot be empty!" : "审核内容不能为空!";
            TIP_SLM_PLEASE_INPUT_SIGN = m_EngVer ? "Please enter your signature!" : "请输入签名！";

            #endregion
            #region 实验数据对话框
            EDM_MW = m_EngVer ? "Experiment Detail" : "实验详情";
            LB_EDM_EXP_STATUS = m_EngVer ? "Status: " : "状       态：";
            LB_EDM_LOGINNAME = m_EngVer ? "Uid: " : "账号：";
            LB_EDM_STARTTIME = m_EngVer ? "Start Time: " : "开始时间：";
            LB_EDM_ENDTIME = m_EngVer ? "End Time: " : "结束时间：";
            LB_EDM_METHODNAME = m_EngVer ? "Name: " : "方法名称：";
            LB_EDM_MANUFACTURER = m_EngVer ? "MFR.: " : "生产厂家：";
            LB_EDM_BANCHNO = m_EngVer ? "Lot No. : " : "批       号：";
            LB_EDM_TESTDEPARTMENT = m_EngVer ? "Test Dept. :" : "检测单位：";
            LB_EDM_DISSOLUTIONMETHODNAME = m_EngVer ? "Method: " : "溶出方法：";
            LB_EDM_WATERBOXTEMP = m_EngVer ? "WTk TEMP: " : "水浴温度：";
            LB_EDM_SOLUTIONVOLUME = m_EngVer ? "SOL VOL: " : "溶媒体积：";
            LB_EDM_ALLTIMESPAN = m_EngVer ? "Duration: " : "总时长：";
            LB_EDM_FRONTSPEED_1 = m_EngVer ? "FNT SPD 1: " : "前排转速1：";
            LB_EDM_FRONTSPEED_2 = m_EngVer ? "FNT SPD 2: " : "前排转速2：";
            LB_EDM_BACKSPEED_1 = m_EngVer ? "BACK SPD 1: " : "后排转速1：";
            LB_EDM_BACKSPEED_2 = m_EngVer ? "BACK SPD 2: " : "后排转速2：";
            LB_EDM_FRONTSTARTTIME_1 = m_EngVer ? "Front row ST 1: " : "前排启动时间1：";
            LB_EDM_FRONTSTARTTIME_2 = m_EngVer ? "Front row ST 2: " : "前排启动时间2：";
            LB_EDM_BACKSTARTTIME_1 = m_EngVer ? "Back row ST 1: " : "后排启动时间1：";
            LB_EDM_BACKSTARTTIME_2 = m_EngVer ? "Back row ST 2: " : "后排启动时间2：";
            LB_EDM_SAMPLEVOLUME = m_EngVer ? "SPL VOL: " : "取样体积：";
            LB_EDM_SAMPLETIMES = m_EngVer ? "SPL Times: " : "取样次数：";
            LB_EDM_AUTOSUPPLY = m_EngVer ? "Auto SUP: " : "自动补液：";
            LB_EDM_FIRSTFILTERVOLUME = m_EngVer ? "Filter VOL: " : "初滤体积：";
            LB_EDM_Speed_Mode = m_EngVer ? "Speed M: " : "转速模式：";
            LB_EDM_Electricmotor_Mode = m_EngVer ? "Motor: " : "电机模式：";
            LB_EDM_TEMPSETTING = m_EngVer ? "TEMP Set: " : "设定温度：";
            LB_EDM_DILUTIONENABLED = m_EngVer ? "Dilute(?): " : "是否稀释：";
            LB_EDM_DILUTIONRATIO = m_EngVer ? "Dilute Rto: " : "稀释倍数：";
            EDM_BTN_CREATESIGNEDREPORT = m_EngVer ? "Electronic signature" : "实验报告电子签名";
            EDM_BTN_CLOSE = m_EngVer ? "Close" : "关闭";
            EDM_SAMPLE_TIMES = m_EngVer ? "Sampling order" : "取样次第";
            EDM_SAMPLE_POINT_TIME = m_EngVer ? "Sampling time point(min)" : "取样时间点（分钟）";
            EDM_CUP_ID = m_EngVer ? "Cup {0}" : "第{0}杯";
            #endregion
            #region 报告命名对话框
            NRM_MW = m_EngVer ? "Confirm generating the report..." : "确认生成报告。。。";
            NRM_SELFDEFPARAMS_GROUPBOX = m_EngVer ? "Custom Parameters" : "自定义参数";
            LB_NRM_REPORTTITLE = m_EngVer ? "Report Title: " : "报告标题：";
            LB_NRM_REPORTNAME = m_EngVer ? "File naming: " : "文件命名：";
            #endregion
            #region 用户对话框
            UserModalView = m_EngVer ? "Account editing" : "账户编辑";
            UM_ACCOUNTINFO_GROUPBOX = m_EngVer ? "Account Information" : "账户信息";
            LB_UM_TB_LOGINNAME = m_EngVer ? "Uid:" : "账号：";
            LB_UM_PB_LOGINPWD = m_EngVer ? "Pwd:" : "口令：";
            LB_UM_PB_LOGINPWD_TWICE = m_EngVer ? "Pwd again:" : "确认口令：";
            LB_UM_COMBO_ROLE = m_EngVer ? "Roles: " : "角色：";
            LB_UM_COMBO_STATUS = m_EngVer ? "Status: " : "状态：";
            LB_UM_DP_VALIDDATE = m_EngVer ? "Valid until: " : "有效期至：";
            LB_UM_TB_USERNAME = m_EngVer ? "Name: " : "姓名：";
            LB_UM_TB_MOBILE = m_EngVer ? "Phone: " : "电话：";
            LB_UM_TB_EMAIL = m_EngVer ? "Email: " : "邮箱：";
            TIP_PASSWORD_INCONSISTENCY = m_EngVer ? "The passwords entered twice are different!" : "两次输入口令不相同！";
            TIP_PASSWORD_FORMAT_ERROR = m_EngVer ? "The password does not comply with the rules. Please reset the password!" : "口令不符合规则，请重新设置口令！";
            TIP_NOTSET_VALIDDATE = m_EngVer ? "Usage period not set!" : "未设置使用期限！";
            TIP_VALIDDATE_LESSTHAN_CURRENTDATE = m_EngVer ? "The usage period cannot be earlier than the current date!" : "使用期限不能小于当前日期！";
            TIP_ROLE_NULL = m_EngVer ? "Role is empty, please select!" : "角色为空，请选择!";
            TIP_UPDATE_USER_CONFIRM = m_EngVer ? "Are you sure to update this user?" : "确定更新该用户吗？";
            TIP_UPDATE_SUCCESS = m_EngVer ? "Update succeeded!" : "更新成功！";
            TIP_SAVE_FAILURE = m_EngVer ? "Saving failed, please check!" : "保存失败，请检查！";
            #endregion     
            #region print报告
            RPT_TITLE = m_EngVer ? "Dissolution Test Report" : "溶出度试验报告";
            RPT_CREATETIME = m_EngVer ? "Created on:" : "创建日期：  ";
            RPT_FILENAME = m_EngVer ? "File Name: " : "文件名称：  ";
            RPT_SOFTWARE = m_EngVer ? "Software Information" : "软件信息";
            RPT_SOFTWARENAME = m_EngVer ? "Software Name: " : "软件名称：  ";
            RPT_SOFTWAREVERSION = m_EngVer ? "Software Version: " : "软件版本：  ";
            RPT_EXP_STATUS = m_EngVer ? "Experimental status:" : "实验状态：  ";
            RPT_STARTTIME = m_EngVer ? "Start time: " : "起始时间：  ";
            RPT_ENDTIME = m_EngVer ? "End Time:" : "终止时间：  ";
            RPT_USERID = m_EngVer ? "User ID: " : "   账户 ID：  ";
            RPT_METHODNAME = m_EngVer ? "Sample Name: " : "样品名称：  ";
            RPT_LOTNO = m_EngVer ? "Lot No. : " : "样品批号：  ";
            RPT_MANUFACTURER = m_EngVer ? "Manufacturer:" : "生产厂家：  ";
            RPT_TESTDEPARTMENT = m_EngVer ? "Test Dept. : " : "检测单位：  ";
            RPT_TESTMETHOD = m_EngVer ? "Dissolution method: {0}" : "溶出方法：  {0}";
            RPT_TANKTEMP = m_EngVer ? "Tank temperature: {0} ℃" : "水浴温度：  {0}℃";
            RPT_SOLVENTVOLUME = m_EngVer ? "Solvent volume: {0} mL" : "溶媒体积：  {0}mL";
            RPT_ALLTIMESPAN = m_EngVer ? "Total experiment duration: {0} min" : "实验总时长：  {0}min";
            RPT_FRONT_SPEED_1 = m_EngVer ? "Front Speed 1:  {0}rpm" : "前排梯度转速１：  {0}rpm";
            RPT_FRONT_SPEED_2 = m_EngVer ? "Front Speed 2:  {0}rpm" : "前排梯度转速2：  {0}rpm";
            RPT_BACK_SPEED_1 = m_EngVer ? "Back Speed 1:  {0}rpm" : "后排梯度转速１：  {0}rpm";
            RPT_BACK_SPEED_2 = m_EngVer ? "Back Speed 2:  {0}rpm" : "后排梯度转速2：  {0}rpm";
            RPT_SAMPLE_VOLUME = m_EngVer ? "Sampling volume: {0} mL" : "取样体积：  {0}mL";
            RPT_SAMPLE_TIME = m_EngVer ? "Sampling times: {0} times" : "取样次数：  {0}次";
            RPT_AUTO_FLUID_INFUSION = m_EngVer ? "Automatic fluid replacement: {0}" : "自动补液：  {0}";
            RPT_FIRST_FILTER_VOLUME = m_EngVer ? "Initial filtration volume: {0} mL" : "初滤体积：  {0}mL";

            RPT_SAMPLEPOINT = m_EngVer ? "Sampling time point {0}:" : "取样时间点{0}： ";
            RPT_SAMPLETIME = m_EngVer ? "The {0} minute" : "第{0}分钟";
            RPT_CUPTEMP = m_EngVer ? "Dissolution cup temperature:" : "溶出杯内温度：";

            RPT_REVIEWERID = m_EngVer ? "  Reviewer ID:  " : "    审核人ID：  ";
            RPT_REVIEWER = m_EngVer ? "  Reviewed by:  " : "    审  核  人 ：  ";
            RPT_CONTENT = m_EngVer ? " Audit content: " : "    审核内容：  ";
            RPT_REPORTTIME = m_EngVer ? "  Audit time:  " : "    审核时间：  ";

            #endregion
            #region 实验流程
            TIP_TEMP_REACHED_STANDARD = m_EngVer ? "The water tank temperature has reached the standard!" : "水箱温度已达标！";
            TIP_TEMP_NOT_REACHED_STANDARD = m_EngVer ? "The water tank temperature has not reached the standard!" : "水箱温度未达标！";
            #endregion

            return true;
        }

        
    }
}
