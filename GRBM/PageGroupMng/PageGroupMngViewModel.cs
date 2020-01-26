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
            

            GRSocketHandler.getDepts += GRSocketHandler_getDepts;
            GRSocketHandler.getUsers += GRSocketHandler_getUsers;
            GRSocketAPI.GetDepts();
            GRSocketAPI.GetUsers();
        }

        private WndMainViewModel wndMainVM { get; set; }

        #region SocketHandler

        private void GRSocketHandler_addDept(E_ResState state)
        {
            GRSocketHandler.addDept -= GRSocketHandler_addDept;
            switch (state)
            {
                case E_ResState.OK:
                    GRSocketHandler.getDepts += GRSocketHandler_getDepts;
                    GRSocketAPI.GetDepts();
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("添加部门失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_delDept(E_ResState state)
        {
            GRSocketHandler.delDept -= GRSocketHandler_delDept;
            switch (state)
            {
                case E_ResState.OK:
                    GRSocketHandler.getDepts += GRSocketHandler_getDepts;
                    GRSocketAPI.GetDepts();
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("删除部门失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_edtDept(E_ResState state)
        {
            GRSocketHandler.edtDept -= GRSocketHandler_edtDept;
            switch (state)
            {
                case E_ResState.OK:
                    GRSocketHandler.getDepts += GRSocketHandler_getDepts;
                    GRSocketAPI.GetDepts();
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("编辑部门失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_getDepts(E_ResState state, List<string> depts)
        {
            GRSocketHandler.getDepts -= GRSocketHandler_getDepts;
            switch (state)
            {
                case E_ResState.OK:
                    deptLst = depts;
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("获取部门列表失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_addUser(E_ResState state)
        {
            GRSocketHandler.addUser -= GRSocketHandler_addUser;
            switch (state)
            {
                case E_ResState.OK:
                    GRSocketHandler.getUsers += GRSocketHandler_getUsers;
                    GRSocketAPI.GetUsers();
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("添加用户失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_delUser(E_ResState state)
        {
            GRSocketHandler.delUser -= GRSocketHandler_delUser;
            switch (state)
            {
                case E_ResState.OK:
                    GRSocketHandler.getUsers += GRSocketHandler_getUsers;
                    GRSocketAPI.GetUsers();
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("删除用户失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_edtUser(E_ResState state, C_User user)
        {
            GRSocketHandler.edtUser -= GRSocketHandler_edtUser;
            switch (state)
            {
                case E_ResState.OK:
                    GRSocketHandler.getUsers += GRSocketHandler_getUsers;
                    GRSocketAPI.GetUsers();
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("编辑用户失败");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_getUsers(E_ResState state, List<C_User> users)
        {
            GRSocketHandler.getUsers -= GRSocketHandler_getUsers;
            switch (state)
            {
                case E_ResState.OK:
                    userLst = users;
                    break;
                case E_ResState.FAILED:
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
            GRSocketHandler.delDept += GRSocketHandler_delDept;
            GRSocketAPI.DelDept(deptName);
        }

        public void OperDeptOKCmd()
        {
            if(operDeptOKTextBd == "创建")
            {
                GRSocketHandler.addDept += GRSocketHandler_addDept;
                GRSocketAPI.AddDept(operDeptNameBd);
            }
            else if(operDeptOKTextBd == "保存")
            {
                GRSocketHandler.edtDept += GRSocketHandler_edtDept;
                GRSocketAPI.EdtDept(operDeptNameOld, operDeptNameBd);
            }
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
            GRSocketHandler.delUser += GRSocketHandler_delUser;
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
                GRSocketHandler.addUser += GRSocketHandler_addUser;
                operUserBd.Pwd = C_Md5.GetHash(operUserBd.Pwd);
                GRSocketAPI.AddUser(operUserBd);
            }
            else if (operUserOKTextBd == "保存")
            {
                GRSocketHandler.edtUser += GRSocketHandler_edtUser;
                if (operUserEditPwd != operUserBd.Pwd)
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
