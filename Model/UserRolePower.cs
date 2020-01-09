namespace Model
{
    /// <summary>
    /// 用户及其对应的角色，权限模型
    /// </summary>
    public class UserRolePower
    {
        public long Id { get; set; }
        //用户名字
        public string UserName { get; set; }
        //用户密码
        public string Password { get; set; }
        //角色名字
        public string RoleName { get; set; }
        //权限名字
        public string PowerName { get; set; }
    }
}
