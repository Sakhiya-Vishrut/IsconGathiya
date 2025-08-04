namespace IsconGathiya.Common
{
    public static class StoredProcedureNames
    {
        public static string SelectAllFromMenu => "SELECT * FROM select_all_from_menu();";
        public static string RegisterUser => "SELECT register_user(@p_AspNetUserId,@p_twofactorkey,@p_username, @p_email, @p_password_hash, @p_sidebar_options);";
        public static string GetActiveUsersByUsernameEmail => "SELECT * FROM get_active_users_by_username_email(@p_username ,@p_email );";
        public static string GetAllActiveUsers => "SELECT * FROM get_all_active_users();";
        public static string GetUserDataWithMenus => "SELECT * FROM get_user_data_with_menus(@p_user_id);";
        public static string GetUserDetailsByEmail => "SELECT * from sp_getuserbyemail(@email_param);";
        public static string GetUpdateUserPasswordByEmail => "SELECT * FROM sp_updateuserpasswordbyemail(@useremail, @newpassword, @success);";
        public static string GetManageUserTask => "SELECT * FROM manage_user_tasks(@action, @useremail, @newpassword);";
        public static string UpdateUser => "SELECT update_user(@p_userid,@p_username,@p_sidebar_options);";
        public static string DeleteUser => "SELECT delete_user(@p_userid);";

    }
}
