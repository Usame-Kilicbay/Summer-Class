﻿using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using static Helper.Constant.SQLQuery;
using static Helper.Constant.TeacherColumn;
using static Helper.Types;

namespace DataAccessLayer
{
    #region CRUD

    public partial class DataAccessTeacher
    {
        public static int CreateTeacher(EntityTeacher entityTeacher)
        {
            SqlCommand sqlCommand = new SqlCommand($"{INSERT_INTO_TEACHERS} ({TEACHER_ID}, {TEACHER_NAME}, {TEACHER_BRANCH}) {VALUES} ('{entityTeacher.TeacherID}', '{entityTeacher.TeacherName}', {entityTeacher.TeacherBranch})", Connection.sqlConnection);
            ConnectionHelper.OpenConnectionIfNot(sqlCommand);

            return sqlCommand.ExecuteNonQuery();
        }

        public static EntityTeacher GetTeacher(int teacherID)
        {
            SqlCommand sqlCommand = new SqlCommand($"{SELECT_ALL_FROM_TEACHERS + WHERE} {TEACHER_ID}={teacherID}", Connection.sqlConnection);
            ConnectionHelper.OpenConnectionIfNot(sqlCommand);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (!sqlDataReader.Read())
                return null;

            EntityTeacher entityTeacher = GetTeacherEntity(sqlDataReader);

            sqlDataReader.Close();

            return entityTeacher;
        }

        public static bool UpdateTeacher(EntityTeacher entityTeacher)
        {
            string teacherIDQuery = $"{TEACHER_ID} = '{entityTeacher.TeacherID}'";
            string teacherNameQuery = $"{TEACHER_NAME} = '{entityTeacher.TeacherName}'";
            string teacherBranchQuery = $"{TEACHER_BRANCH} = '{entityTeacher.TeacherBranch}'";

            SqlCommand sqlCommand = new SqlCommand($"{UPDATE_TEACHERS_SET} {teacherNameQuery}, {teacherBranchQuery} {WHERE} {teacherIDQuery}", Connection.sqlConnection);
            ConnectionHelper.OpenConnectionIfNot(sqlCommand);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        public static bool ChangetTeacherStatus(int teacherID, RoleStatus roleStatus)
        {
            string teacherIDQuery = $"{TEACHER_ID} = '{teacherID}'";
            string teacherStatusQuery = $"{TEACHER_STATUS} = '{Convert.ToBoolean(roleStatus)}'";

            SqlCommand sqlCommand = new SqlCommand($"{UPDATE_TEACHERS_SET} {teacherStatusQuery} {WHERE} {teacherIDQuery}", Connection.sqlConnection);
            ConnectionHelper.OpenConnectionIfNot(sqlCommand);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        public static List<EntityTeacher> GetTeacherList()
        {
            List<EntityTeacher> entityTeachers = new List<EntityTeacher>();
            SqlCommand sqlCommand = new SqlCommand($"{SELECT_ALL_FROM_TEACHERS}", Connection.sqlConnection);
            ConnectionHelper.OpenConnectionIfNot(sqlCommand);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
                entityTeachers.Add(GetTeacherEntity(sqlDataReader));

            sqlDataReader.Close();

            return entityTeachers;
        }

        private static EntityTeacher GetTeacherEntity(SqlDataReader sqlDataReader)
        {
            return new EntityTeacher
            {
                TeacherID = Convert.ToInt32(sqlDataReader[TEACHER_ID].ToString()),
                TeacherName = sqlDataReader[TEACHER_NAME].ToString(),
                TeacherPassword = sqlDataReader[TEACHER_PASSWORD].ToString(),
                TeacherPhoto = sqlDataReader[TEACHER_PHOTO].ToString(),
                TeacherBranch = Convert.ToInt32(sqlDataReader[TEACHER_BRANCH].ToString()),
                TeacherStatus = (RoleStatus)Convert.ToInt32(sqlDataReader[TEACHER_STATUS])
            };
        }
    }

    #endregion

    #region SESSION

    public partial class DataAccessTeacher
    {
        public static bool TeacherSignIn(EntityTeacher entityTeacher)
        {
            string teacherNameQuery = $"{TEACHER_NAME} = '{entityTeacher.TeacherName}'";

            SqlCommand sqlCommand = new SqlCommand($"{SELECT} {TEACHER_NAME}, {TEACHER_PASSWORD} {FROM_TEACHERS_WHERE} {teacherNameQuery}", Connection.sqlConnection);
            ConnectionHelper.OpenConnectionIfNot(sqlCommand);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (!sqlDataReader.Read())
                return false;

            string teacherPassword = sqlDataReader[TEACHER_PASSWORD].ToString();
            
            sqlDataReader.Close();

            return String.Equals(entityTeacher.TeacherPassword, teacherPassword);
        }
    }

    #endregion
}