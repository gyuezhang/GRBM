using GRModel;
using GRSocket;
using GRUtil;
using Stylet;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GRBM
{
    public class PageGroupMngViewModel : Screen
    {
        public PageGroupMngViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;

            GRSocketHandler.addDept += GRSocketHandler_addDept;
            GRSocketHandler.delDept += GRSocketHandler_delDept;
            GRSocketHandler.edtDept += GRSocketHandler_edtDept;
            GRSocketHandler.getDepts += GRSocketHandler_getDepts;
            GRSocketHandler.addUser += GRSocketHandler_addUser;
            GRSocketHandler.delUser += GRSocketHandler_delUser;
            GRSocketHandler.edtUser += GRSocketHandler_edtUser;
            GRSocketHandler.getUsers += GRSocketHandler_getUsers;

            GRSocketAPI.GetDepts();
            GRSocketAPI.GetUsers();
        }

        private WndMainViewModel wndMainVM { get; set; }

        #region SocketHandler

        private void GRSocketHandler_addDept(RES_STATE state)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    GRSocketAPI.GetDepts();
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("添加部门失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_delDept(RES_STATE state)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    GRSocketAPI.GetDepts();
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("删除部门失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_edtDept(RES_STATE state)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    GRSocketAPI.GetDepts();
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("编辑部门失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_getDepts(RES_STATE state, List<string> depts)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    deptLst = depts;
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("获取部门列表失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_addUser(RES_STATE state)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    GRSocketAPI.GetUsers();
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("添加用户失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_delUser(RES_STATE state)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    GRSocketAPI.GetUsers();
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("删除用户失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_edtUser(RES_STATE state)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    GRSocketAPI.GetUsers();
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("编辑用户失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_getUsers(RES_STATE state, List<C_User> users)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    userLst = users;
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("获取用户列表失败");
                    break;
                default:
                    break;
            }
        }
        
        #endregion SocketHandler

        #region Bindings        

        public int pageIndexBd { get; set; } = 0;
        public List<string> deptLst { get; set; }
        public List<C_User> userLst { get; set; }
        public Visibility VOperDeptBd { get; set; } = Visibility.Collapsed;
        public Visibility VOperUserBd { get; set; } = Visibility.Collapsed;
        public string operDeptNameBd { get; set; }
        public string operDeptTitleBd { get; set; }
        public string operDeptOKTextBd { get; set; }
        public string operUserNameBd { get; set; }
        public string operUserTitleBd { get; set; }
        public string operUserOKTextBd { get; set; }
        public C_User operUserBd { get; set; }

        #endregion Bindings

        #region Actions

        public void SelectPageCmd(string cmdPara)
        {
            wndMainVM.SelectPage((E_Page)Enum.Parse(typeof(E_Page), cmdPara, true));
        }

        //Dept
        public void AddDeptCmd()
        {
            operDeptTitleBd = "创建部门";
            operDeptOKTextBd = "创建";
            operDeptNameBd = "";
            VOperDeptBd = Visibility.Visible;
        }

        public void EdtDeptCmd(string deptName)
        {
            operDeptTitleBd = "编辑部门";
            operDeptOKTextBd = "保存";
            operDeptNameOld = deptName;
            operDeptNameBd = deptName;
            VOperDeptBd = Visibility.Visible;
        }

        public void DelDeptCmd(string deptName)
        {
            GRSocketAPI.DelDept(deptName);
        }

        public void OperDeptOKCmd()
        {
            if(operDeptOKTextBd == "创建")
                GRSocketAPI.AddDept(operDeptNameBd);
            else if(operDeptOKTextBd == "保存")
                GRSocketAPI.EdtDept(operDeptNameOld, operDeptNameBd);
            VOperDeptBd = Visibility.Collapsed;
        }

        public void OperDeptCancelCmd()
        {
            VOperDeptBd = Visibility.Collapsed;
        }

        //User
        public void AddUserCmd()
        {
            operUserTitleBd = "创建用户";
            operUserOKTextBd = "创建";
            operUserBd = new C_User();
            VOperUserBd = Visibility.Visible;
        }

        public void EdtUserCmd(C_User u)
        {
            operUserTitleBd = "编辑用户";
            operUserOKTextBd = "保存";
            operUserBd = u;
            operUserEditPwd = u.Pwd;
            VOperUserBd = Visibility.Visible;
        }

        public void DelUserCmd(C_User u)
        {
            GRSocketAPI.DelUser(u.Id);
        }

        public void OperUserOKCmd()
        {
            if(operUserBd.Pwd == null || operUserBd.Pwd == "")
            {
                wndMainVM.messageQueueBd.Enqueue("密码不能为空");
                return;
            }

            if (operUserOKTextBd == "创建")
            {
                
                operUserBd.Pwd = C_Md5.GetHash(operUserBd.Pwd);
                GRSocketAPI.AddUser(operUserBd);
            }
            else if (operUserOKTextBd == "保存")
            {
                if(operUserEditPwd != operUserBd.Pwd)
                    operUserBd.Pwd = C_Md5.GetHash(operUserBd.Pwd);
                GRSocketAPI.EdtUser(operUserBd);
            }
            VOperUserBd = Visibility.Collapsed;
        }

        public void OperUserCancelCmd()
        {
            VOperUserBd = Visibility.Collapsed;
        }

        #endregion Actions

        private string operDeptNameOld { get; set; }
        private string operUserEditPwd { get; set; }
    }
}
