using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for DAL
/// </summary>
public partial class DAL
{
    public SqlDataAdapter da;
    Campus oo = new Campus();
    public DataTable dt = new DataTable();
    DLL dll = new DLL();
    List<SqlParameter> parameter;
    public static readonly DAL objDal=new DAL();

    public DataTable Get_DepositedUnclearedChequeorDD(BAL.DepositedUnclearedChequeorDD obj)
    {
        List<SqlParameter> parameter = new List<SqlParameter>();
        parameter.Add(new SqlParameter("@SessionName", obj.Session));
        parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
        parameter.Add(new SqlParameter("@FromDate", obj.FromDate));
        parameter.Add(new SqlParameter("@ToDate", obj.ToDate));
        dt = dll.Sp_SelectRecord_usingExecuteDataset("USP_ChequeDetails", parameter).Tables[0];
        return dt;
    }
    public string Set_Headertemplate(BAL.Set_Headertemplate obj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Id", obj.Id));
            parameter.Add(new SqlParameter("@Template", obj.Template));
            parameter.Add(new SqlParameter("@TemplateFor", obj.TemplateFor));
            parameter.Add(new SqlParameter("@IsActive", obj.IsAvtive));
            parameter.Add(new SqlParameter("@SessionName", obj.Session));
            parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
            parameter.Add(new SqlParameter("@LoginName", obj.LoginName));
            parameter.Add(new SqlParameter("@Isdisplaylogo", obj.Isdisplaylogo));
            SqlParameter msgParameter = new SqlParameter("@Msg", "");
            msgParameter.Direction = ParameterDirection.Output;
            msgParameter.Size = 0x100;
            parameter.Add(msgParameter);
            msg = dll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_SETtemplateSelection", parameter);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public Tuple<DataTable, string> Get_Headertemplate(BAL.Get_Headertemplate obj)
    {
        dt = new DataTable();
        string msg = "";
        try
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@TemplateFor", obj.TemplateFor));
            parameter.Add(new SqlParameter("@SessionName", obj.Session));
            parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
            dt=dll.Sp_SelectRecord_usingExecuteDataset("USP_GetHeadertemplate", parameter).Tables[0];

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<DataTable, string>(dt, msg);
    }
    public Tuple<DataSet, string> Get_Header(BAL.Get_Header obj)
    {
        string msg="";
        DataSet ds=null;
        try
        {
            List<SqlParameter> parameter=new List<SqlParameter>();
            parameter.Add(new SqlParameter("@TemplateFor", obj.TemplateFor));
            ds = dll.Sp_SelectRecord_usingExecuteDataset("Get_SetHeaderTemplate", parameter);
        }
        catch(Exception ex)
        {
            msg = (ex.Message);         
        }
        return new Tuple<DataSet, string>(ds, msg);
    }
    public string Set_DocumentName(BAL.Set_DocumentName obj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Type", obj.Type));
            parameter.Add(new SqlParameter("@Id", obj.Id));
            parameter.Add(new SqlParameter("@DocumentType", obj.DocumentType));
            //parameter.Add(new SqlParameter("@ClassId", obj.ClassId));
            //parameter.Add(new SqlParameter("@ClassName", obj.ClassName));
            //parameter.Add(new SqlParameter("@SessionName", obj.Session));
            parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
            parameter.Add(new SqlParameter("@LoginName", obj.LoginName));
            SqlParameter msgParameter = new SqlParameter("@Msg", "");
            msgParameter.Direction = ParameterDirection.Output;
            msgParameter.Size = 0x100;
            parameter.Add(msgParameter);
            msg = dll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_SetDocumentName", parameter);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public Tuple<DataTable, string> Get_DocumentName(BAL.Set_DocumentName obj)
    {
        dt = new DataTable();
        string msg = "";
        try
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Srno", obj.Srno));
            parameter.Add(new SqlParameter("@SessionName", obj.Session));
            parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
            //parameter.Add(new SqlParameter("@ClassId", obj.ClassId));
            parameter.Add(new SqlParameter("@Id", obj.Id));

            dt = dll.Sp_SelectRecord_usingExecuteDataset("USP_GetDocumentName", parameter).Tables[0];
          
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<DataTable, string>(dt, msg);
    }
    public Tuple<DataTable, string> Get_StaffDocumentType(BAL.Staff_Document obj)
    {
        dt = new DataTable();
        string msg = "";
        try
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Empid", obj.Empid));
            parameter.Add(new SqlParameter("@SessionName", obj.Session));
            parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
            parameter.Add(new SqlParameter("@Id", obj.Id));

            dt = dll.Sp_SelectRecord_usingExecuteDataset("USP_GetStaffDocumentType", parameter).Tables[0];

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<DataTable, string>(dt, msg);
    }
    public string Set_StudentDocumentRecord(BAL.Set_StudentDocumentRecord obj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@DocId", obj.DocId));
            parameter.Add(new SqlParameter("@DocName", obj.DocName));
            parameter.Add(new SqlParameter("@DocPath", obj.DocPath));
            parameter.Add(new SqlParameter("@Srno", obj.SrNo));
            parameter.Add(new SqlParameter("@StEnRCode", obj.StEnRCode));
            parameter.Add(new SqlParameter("@SessionName", obj.Session));
            parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
            parameter.Add(new SqlParameter("@LoginName", obj.LoginName));
            parameter.Add(new SqlParameter("@Softcopy", obj.Softcopy));
            parameter.Add(new SqlParameter("@Hardcopy", obj.Hardcopy));
            parameter.Add(new SqlParameter("@Remark", obj.Remark));
            parameter.Add(new SqlParameter("@Verified", obj.Varified));
            SqlParameter paramsg = new SqlParameter("@MSG", "");
            paramsg.Direction = ParameterDirection.Output;
            paramsg.Size = 0x100;
            parameter.Add(paramsg);
            msg=dll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_SetStudentDocumentRecord", parameter);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public string Set_StaffDocumentType(BAL.Staff_Document obj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Type", obj.Type));
            parameter.Add(new SqlParameter("@Id", obj.Id));
            parameter.Add(new SqlParameter("@DocumentType", obj.DocumentType));
            parameter.Add(new SqlParameter("@SessionName", obj.Session));
            parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
            parameter.Add(new SqlParameter("@LoginName", obj.LoginName));
            SqlParameter msgParameter = new SqlParameter("@Msg", "");
            msgParameter.Direction = ParameterDirection.Output;
            msgParameter.Size = 0x100;
            parameter.Add(msgParameter);
            msg = dll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_SetStaffDocumentType", parameter);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public string Set_StaffDocumentRecord(BAL.Staff_Document obj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@DocId", obj.DocId));
            parameter.Add(new SqlParameter("@DocName", obj.DocName));
            parameter.Add(new SqlParameter("@DocPath", obj.DocPath));
            parameter.Add(new SqlParameter("@Empid", obj.Empid));
            parameter.Add(new SqlParameter("@EmpCode", obj.EmpCode));
            parameter.Add(new SqlParameter("@SessionName", obj.Session));
            parameter.Add(new SqlParameter("@BranchCode", obj.BranchCode));
            parameter.Add(new SqlParameter("@LoginName", obj.LoginName));
            SqlParameter paramsg = new SqlParameter("@MSG", "");
            paramsg.Direction = ParameterDirection.Output;
            paramsg.Size = 0x100;
            parameter.Add(paramsg);
            msg = dll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_SetStaffDocumentRecord", parameter);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public Tuple<DataTable, string> GET_ClassGroupMaster(BAL.GET_ClassGroupMaster BalGetObj)
    {
        dt = new DataTable();
        string msg = "";
        try
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@GroupId", BalGetObj.GroupId));
            parameter.Add(new SqlParameter("@SessionName", BalGetObj.SessionName));
            parameter.Add(new SqlParameter("@BranchCode", BalGetObj.BranchCode));
            parameter.Add(new SqlParameter("@LoginName", BalGetObj.LoginName));
            dt = dll.Sp_SelectRecord_usingExecuteDataset("USP_GET_ClassGroupMaster", parameter).Tables[0];

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<DataTable, string>(dt, msg);
    }
    public string Set_ClassGroupMaster(BAL.SET_ClassGroupMaster BalSetObj)
    {
        string msg = "";
        try
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@GroupId", BalSetObj.GroupId));
            parameter.Add(new SqlParameter("@ClassId", BalSetObj.ClassId));
            parameter.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            parameter.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            parameter.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            parameter.Add(new SqlParameter("@IsActive", BalSetObj.IsActive));
            SqlParameter paramsg = new SqlParameter("@MSG", "");
            paramsg.Direction = ParameterDirection.Output;
            paramsg.Size = 0x100;
            parameter.Add(paramsg);
            msg = dll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_SET_ClassGroupMaster", parameter);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public Tuple<DataTable, string> Get_PromotedStudentRecord(BAL.GenralInfo BalGetobj)
    {
        dt = new DataTable();
        string msg = "";
        try
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@SrNo", BalGetobj.SrNo));
            parameter.Add(new SqlParameter("@StEnRCode", BalGetobj.StEnRCode));
            parameter.Add(new SqlParameter("@SessionName", BalGetobj.SessionName));
            parameter.Add(new SqlParameter("@BranchCode", BalGetobj.BranchCode));
            parameter.Add(new SqlParameter("@ClassName", BalGetobj.ClassName));
            parameter.Add(new SqlParameter("@SectionName", BalGetobj.SectionName));
            dt = dll.Sp_SelectRecord_usingExecuteDataset("USP_GET_PromotedStudentRecord", parameter).Tables[0];

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return new Tuple<DataTable, string>(dt, msg);
    }
    public string Set_StudentPromotionCancellation(BAL.GenralInfo BalSetObj)
    {
        string msg = "";
        try
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            parameter.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            parameter.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            parameter.Add(new SqlParameter("@SrNo", BalSetObj.SrNo));
            SqlParameter paramsg = new SqlParameter("@Msg", "");
            paramsg.Direction = ParameterDirection.Output;
            paramsg.Size = 0x100;
            parameter.Add(paramsg);
            msg = dll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_Set_StudentPromotionCancellation", parameter);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public string Set_DefaultName(BAL.Set_SetDefaultName BalSetObj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@replacewith", BalSetObj.replacewidth));
            param.Add(new SqlParameter("@replace", BalSetObj.replace));
            param.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("DefaultTextProc", param);
        }
        catch(Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public Tuple<string, object> Get_SetDefaultName(BAL.Set_SetDefaultName BalSetObj)
    {
        string msg = "";
        object record = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@queryFor", "S"));
            param.Add(new SqlParameter("@replacewith", BalSetObj.replacewidth));
            //dt = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("DefaultTextProc", param).Tables[0];
            record = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("DefaultTextProc", param);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<string, object>(msg, record);
    }
    public string Set_DefaultValue(BAL.Set_DefaultSelectedValue BalSetObj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@defaultvalueof", BalSetObj.defaultvalueof));
            param.Add(new SqlParameter("@defaultvalue", BalSetObj.defaultvalue));
            param.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("DefaultSelectedValueProc", param);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public Tuple<string, object> Get_SetDefaultValue(BAL.Set_DefaultSelectedValue BalSetObj)
    {
        string msg = "";
        object record = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@queryFor", "S"));
            param.Add(new SqlParameter("@defaultvalueof", BalSetObj.defaultvalueof));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            record = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("DefaultSelectedValueProc", param);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<string, object>(msg, record);
    }
    public string Set_RulesForShibling(BAL.Set_RulesForSibling BalSetObj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Rule1", BalSetObj.rule1));
            param.Add(new SqlParameter("@Rule2", BalSetObj.rule2));
            param.Add(new SqlParameter("@NoofSibling", BalSetObj.noofSibling));
            param.Add(new SqlParameter("@DiscountType", BalSetObj.discountType));
            param.Add(new SqlParameter("@DiscountValue", BalSetObj.discountValue));
            param.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("RulesForSiblingProc", param);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public string Set_DiscountHead(BAL.Set_DiscountHead BalSetObj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", BalSetObj.id));
            param.Add(new SqlParameter("@queryFor", BalSetObj.Queryfor));
            param.Add(new SqlParameter("@ParentHeadvalue", BalSetObj.ParentHeadvalue));
            param.Add(new SqlParameter("@HeadName", BalSetObj.HeadName));
            param.Add(new SqlParameter("@remark", BalSetObj.Remark));
            param.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("DiscountHeadProc", param);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public Tuple<string, DataTable> Get_SetDiscountHead(BAL.Set_DiscountHead BalSetObj)
    {
        string msg = "";
        object record = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@queryFor", "S"));
            param.Add(new SqlParameter("@ParentHeadvalue", BalSetObj.ParentHeadvalue));
            param.Add(new SqlParameter("@HeadName", BalSetObj.HeadName));
            param.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            dt = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("DiscountHeadProc", param).Tables[0];
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<string, DataTable>(msg, dt);
    }

    public string RulesForDiscount(BAL.RulesForDiscount BalSetObj)
    {
        string msg = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", BalSetObj.id));
            param.Add(new SqlParameter("@queryFor", BalSetObj.Queryfor));
            param.Add(new SqlParameter("@DiscountHeadId", BalSetObj.DiscountHeadId));
            param.Add(new SqlParameter("@ClassId", BalSetObj.ClassId));
            param.Add(new SqlParameter("@GenderValue", BalSetObj.GenderValue));
            param.Add(new SqlParameter("@CategoryValue", BalSetObj.CategoryValue));
            param.Add(new SqlParameter("@Remark", BalSetObj.Remark));
            param.Add(new SqlParameter("@Installment", BalSetObj.Installment));
            param.Add(new SqlParameter("@Amount", BalSetObj.Amount));
            param.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("RulesForDiscountProc", param);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    public Tuple<string, DataTable> FeeHeadCategoryMaster(BAL.FeeHeadCategoryMaster BalSetObj)
    {
        string msg = "";
        dt = new DataTable();
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", BalSetObj.id));
            param.Add(new SqlParameter("@queryFor", BalSetObj.Queryfor));
            param.Add(new SqlParameter("@FeeHeadCategory", BalSetObj.FeeHeadCategory));
            param.Add(new SqlParameter("@Remark", BalSetObj.Remark));
            param.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            if (BalSetObj.Queryfor != "S")
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("FeeHeadCategoryMasterProc", param);
            }
            else
            {
                dt = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("FeeHeadCategoryMasterProc", param).Tables[0];
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<string, DataTable>(msg,dt);
    }

    public Tuple<string, DataTable> Employmentform(BAL.Employmentform BalSetObj)
    {
        string msg = "";
        dt = new DataTable();
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@queryFor", BalSetObj.Queryfor));
            param.Add(new SqlParameter("@RecieptNo", BalSetObj.RecieptNo));
            param.Add(new SqlParameter("@EmpName", BalSetObj.EmpName));
            param.Add(new SqlParameter("@EmpFather", BalSetObj.EmpFather));
            param.Add(new SqlParameter("@EmpGender", BalSetObj.EmpGender));
            param.Add(new SqlParameter("@EmpContactNo", BalSetObj.EmpContactNo));
            param.Add(new SqlParameter("@EmpDesignation", BalSetObj.EmpDesignation));
            param.Add(new SqlParameter("@EmpAmount", BalSetObj.EmpAmount));
            param.Add(new SqlParameter("@EmployeeFormId", BalSetObj.EmployeeFormId));
            param.Add(new SqlParameter("@HuabandName", BalSetObj.HuabandName));
            param.Add(new SqlParameter("@EmploymenttypeId", BalSetObj.EmploymenttypeId));
            param.Add(new SqlParameter("@SubjectId", BalSetObj.SubjectIds));
            param.Add(new SqlParameter("@Email", BalSetObj.Email));
            param.Add(new SqlParameter("@EFDate", BalSetObj.EFDate));
            param.Add(new SqlParameter("@IsCancel", BalSetObj.IsCancel));
            param.Add(new SqlParameter("@SessionName", BalSetObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalSetObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalSetObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            if (BalSetObj.Queryfor != "S")
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("EmploymentformProc", param);
            }
            else
            {
                dt = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("EmploymentformProc", param).Tables[0];
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<string, DataTable>(msg, dt);
    }

    public Tuple<string, DataTable> EducationType(BAL.EducationType BalObj)
    {
        string msg = "";
        dt = new DataTable();
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", BalObj.id));
            param.Add(new SqlParameter("@QueryFor", BalObj.Queryfor));
            param.Add(new SqlParameter("@EducationType", BalObj.EducationTypes));
            param.Add(new SqlParameter("@FromClass", BalObj.FromClass));
            param.Add(new SqlParameter("@ToClass", BalObj.ToClass));
            param.Add(new SqlParameter("@SessionName", BalObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            if (BalObj.Queryfor != "S")
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("EducationTypeProc", param);
            }
            else
            {
                dt = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("EducationTypeProc", param).Tables[0];
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<string, DataTable>(msg, dt);
    }

    public Tuple<string, DataTable> SubjectForEmploymentForm(BAL.SubjectForEmploymentForm BalObj)
    {
        string msg = "";
        dt = new DataTable();
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", BalObj.id));
            param.Add(new SqlParameter("@QueryFor", BalObj.Queryfor));
            param.Add(new SqlParameter("@EducationType", BalObj.EducationTypes));
            param.Add(new SqlParameter("@Subject", BalObj.Subject));
            param.Add(new SqlParameter("@SessionName", BalObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            if (BalObj.Queryfor != "S")
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SubjectForEmploymentFormProc", param);
            }
            else
            {
                dt = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("SubjectForEmploymentFormProc", param).Tables[0];
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<string, DataTable>(msg, dt);
    }

    public Tuple<string, DataTable> AssociateOrganizatinDeails(BAL.AssociateOrganizatinDeails BalObj)
    {
        string msg = "";
        dt = new DataTable();
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Id", BalObj.id));
            param.Add(new SqlParameter("@QueryFor", BalObj.Queryfor));
            param.Add(new SqlParameter("@Organization", BalObj.OrganizationName));
            param.Add(new SqlParameter("@Remark", BalObj.Remark));
            param.Add(new SqlParameter("@SessionName", BalObj.SessionName));
            param.Add(new SqlParameter("@LoginName", BalObj.LoginName));
            param.Add(new SqlParameter("@BranchCode", BalObj.BranchCode));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            if (BalObj.Queryfor != "S")
            {
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("AssociateOrganizatinDeailsProc", param);
            }
            else
            {
                dt = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("AssociateOrganizatinDeailsProc", param).Tables[0];
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return new Tuple<string, DataTable>(msg, dt);
    }
}