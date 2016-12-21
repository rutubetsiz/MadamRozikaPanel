using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Agencies
/// </summary>
public class M_Agencies
{
    public M_Agencies()
    {
    }
    public M_Agencies(DataRow dr)
    {
        AgencyId = Convert.ToInt32(dr["AgencyId"]);
        AgencyName = dr["AgencyName"].ToString();
        AgencyNameShort = dr["AgencyNameShort"].ToString();
        AgencyLogo = dr["AgencyLogo"].ToString();
    }
    #region Properties
    public int AgencyId { get; set; }
    public string AgencyName { get; set; }
    public string AgencyNameShort { get; set; }
    public string AgencyLogo { get; set; }
    #endregion
}