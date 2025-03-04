﻿using System;
using static Sidekick.Constant.PageName;
using static Sidekick.Types;
using static Sidekick.SessionManager;

namespace SummerClass
{
    public partial class TeacherSignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChangeSessionStatus(SessionRole.TEACHER, SessionOperation.END);
            Response.Redirect(TeacherPage.TEACHER_SIGN_IN);
        }
    }
}