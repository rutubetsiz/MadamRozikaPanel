using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;


public class M_Users
{
    public M_Users()
    {

    }
    public M_Users(DataRow dr)
    {
        UserId = Convert.ToInt32(dr["UserId"]);
        RoleId = dr["RoleId"].ToString();
        Name = dr["Name"].ToString();
        Email = dr["Email"].ToString();
        Password = dr["Password"].ToString();
        Active = Convert.ToByte(dr["Active"]);
    }

    #region Properties
    public int UserId { get; set; }
    public string RoleId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Active { get; set; }
    #endregion
}