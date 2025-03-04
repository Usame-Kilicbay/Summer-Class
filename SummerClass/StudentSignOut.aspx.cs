﻿using System;
using static Sidekick.Constant.PageName;
using static Sidekick.Types;
using static Sidekick.SessionManager;

namespace SummerClass
{
    public partial class StudentSignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChangeSessionStatus(SessionRole.STUDENT, SessionOperation.END);
            Response.Redirect(StudentPage.STUDENT_SIGN_IN);
        }
    }
}