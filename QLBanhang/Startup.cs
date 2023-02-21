﻿using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QLBanhang.Models;
[assembly: OwinStartupAttribute(typeof(QLBanhang.Startup))]
namespace QLBanhang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //InitUserRole();
        }
        private void InitUserRole()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            try
            {
                //tạo role Admin
                if (!roleManager.RoleExists("Admin"))//chưa có mới tạo
                {
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                    //tạo user
                    var user = new ApplicationUser();
                    user.UserName = "adminqlbh@gmail.com";
                    user.Email = "adminqlbh@gmail.com";
                    string pass = "Baobao2001@";//sau này login sẽ thay đổi pass
                    var chkUser = userManager.Create(user, pass);
                    //đưa user qlbh vào role Admin
                    if (chkUser.Succeeded)
                        userManager.AddToRole(user.Id, "Admin");
                }
            }
            catch { }
        }
    }
}
